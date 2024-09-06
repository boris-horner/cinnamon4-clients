// Copyright 2012,2024 texolution GmbH
// 
// Licensed under the Apache License, Version 2.0 (the "License"); you may not 
// use this file except in compliance with the License. You may obtain a copy of 
// the License at
// 
// http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software 
// distributed under the License is distributed on an "AS IS" BASIS, WITHOUT 
// WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. See the 
// License for the specific language governing permissions and limitations under 
// the License.
using System.Linq;
using System.Collections.Generic;
using System;
using System.Xml;
using C4ObjectApi.Interfaces;
using System.Runtime.Serialization;
using ContentAwareness;
using System.Globalization;
using System.Threading;
using C4ObjectApi.Operations.Search;
using C4ServerConnector;
using C4ObjectApi.Global;
using C4ServerConnector.Assets;
using System.Reflection;
using System.Diagnostics;
using System.IO;

namespace C4ObjectApi.Repository
{
    [DataContract()]
    public class CmnSession : IEquatable<CmnSession>
    {
        public enum UserConfigTypes
        {
            Standard,
            Named,
            Basic
        }

        private const long SEARCH_BATCH_SIZE = 500L;
        private CmnFolder _rootFolder;
        private readonly string _userName;
        private Dictionary<string, IContent> _cams;
        private readonly Dictionary<string, ISessionExtension> _sessionExtensions; // by assembly.type
        //private CmnDeserializationEngine _deserializationEngine;
        private Dictionary<string, XmlDocument> _configEntries;
        public LocksManager LocksMgr { get; }
        public  Dictionary<long, CmnFolder> CachedFolders { get; private set; }
        public C4Session CommandSession { get; private set; }
        public CmnServer Server { get; private set; }
        public UserConfigTypes UserConfigType { get; private set; }

        public enum Versions
        {
            All,
            Branch,
            Head
        }

        public bool IsSuperuser 
        { 
            get 
            {
                return User.GroupIds.Contains((long)SessionConfig.C4Sc.GroupsByName["_superusers"].Id);
            } 
        }

        public CmnSession(string userName, string pwd, string ticket, CmnServer svr, string namedUserConfig = null)
        {
            if(pwd!=null)
            {
                Server = svr;
                _userName = userName;

                _sessionExtensions = new Dictionary<string, ISessionExtension>();
                CachedFolders = new Dictionary<long, CmnFolder>();

                CommandSession = new C4Session(Server.Url, userName, pwd, svr.CdnUrl, svr.LocalCertFile, false, Path.GetTempPath());
                Ticket = CommandSession.Ticket;
                Dictionary<long, C4Group> groups = CommandSession.ListGroups();
                SessionConfig = new CmnSessionConfiguration(this, userName, groups);
                User = GetUserByName(userName);
                UserConfig = BuildUserConfig(groups, namedUserConfig);
                AddSessionExtensions();
                UserSettings = new CmnUserSettings(this);

                XmlNode branchN = UserConfig.DocumentElement.SelectSingleNode("global_settings/branching/@allow");
                if (branchN == null)
                    AllowBranching = true;    // default: branching enabled
                else
                    AllowBranching = (branchN.InnerText ?? "") == "true" ? true : false;

                InitContentAwareness();

                //RegistryHelper.InitRegistry();
                LocksMgr = new LocksManager(Server.LocksDir, Server.Id);
                UpdateLocks();
            }
            else if(ticket!=null)
            {
                Server = svr;
                Ticket = ticket;
                CachedFolders = new Dictionary<long, CmnFolder>();

                CommandSession = new C4Session(Server.Url, Ticket,svr.CdnUrl, svr.LocalCertFile, false, Path.GetTempPath());
                _sessionExtensions = new Dictionary<string, ISessionExtension>();
                _configEntries = new Dictionary<string, XmlDocument>();
                Dictionary<long, C4Group> groups = CommandSession.ListGroups();
                SessionConfig = new CmnSessionConfiguration(this, userName, groups);
                User = GetUserByName(userName);
                UserConfig = BuildUserConfig(groups, namedUserConfig);
                AddSessionExtensions();
                XmlNode branchN = UserConfig.DocumentElement.SelectSingleNode("global_settings/branching/@allow");
                if (branchN == null)
                    AllowBranching = true;    // default: branching enabled
                else
                    AllowBranching = (branchN.InnerText ?? "") == "true" ? true : false;
                InitContentAwareness();
            }
        }

        public void ReloadSettings()
        {
            UserSettings = new CmnUserSettings(this);
            Dictionary<long, C4Group> groups = CommandSession.ListGroups();
            SessionConfig = new CmnSessionConfiguration(this, _userName, groups);
        }

        private void InitContentAwareness()
        {
            _cams = new Dictionary<string, IContent>();
            foreach (XmlElement contentEl in UserConfig.DocumentElement.SelectNodes("classes/content_awareness/content"))
            {
                XmlElement implEl = (XmlElement)contentEl.SelectSingleNode("implementation");
                System.Reflection.Assembly a = SessionConfig.GetAssembly(implEl.GetAttribute("assembly"));
                IContent impl = (IContent)a.CreateInstance(implEl.GetAttribute("assembly") + "." + implEl.GetAttribute("type"));
                impl.Init(contentEl, false);
                foreach (XmlNode ffN in contentEl.SelectNodes("format_filter"))
                {
                    if (!_cams.ContainsKey(ffN.InnerText))
                        _cams.Add(ffN.InnerText, impl);
                }
            }
        }


        private XmlDocument BuildUserConfig(Dictionary<long, C4Group> groups, string namedUserConfig = null)
        {
            XmlDocument result = new XmlDocument();
            if (namedUserConfig == null)
                UserConfigType = UserConfigTypes.Standard;
            else
                UserConfigType = UserConfigTypes.Named;

            string ceName = string.Concat("appconfig.", UserConfigType == UserConfigTypes.Standard ? "default" : namedUserConfig);
            XmlDocument ce = GetConfigEntry(ceName);
            if(ce==null) throw new ApplicationException(string.Format("Failure loading config entry {0}.", ceName));
            else
            {
                XmlNode configN = ce.DocumentElement.SelectSingleNode("app/cinnamon_client[match/user/@name='" + _userName + "'][1]");

                // superuser-based selection
                if (configN == null && User.GroupIds.Contains((long)SessionConfig.C4Sc.GroupsByName["_superusers"].Id))
                    configN = ce.DocumentElement.SelectSingleNode("app/cinnamon_client[match/superuser][1]");

                // group based selection
                // cycle through groups
                if (configN == null)
                {
                    foreach(long groupId in User.GroupIds)
                    {
                        configN = ce.DocumentElement.SelectSingleNode("app/cinnamon_client[match/group/@sysname='" + SessionConfig.C4Sc.GroupsById[groupId].Name + "'][1]");
                        if (configN != null) break;
                    }
                }

                // default
                if (configN == null)
                    configN = ce.DocumentElement.SelectSingleNode("app/cinnamon_client[match/default][1]");

                if (ce == null) throw new ApplicationException(string.Format("Failure loading default configuration from config entry {0}.", ceName));
                else result.AppendChild(result.ImportNode(configN.CloneNode(true), true));


                XmlNode cultureN = result.DocumentElement.SelectSingleNode("global_settings/culture_name");

                if (cultureN == null)
                {
                    // use settings from UI definition
                    Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo(SessionConfig.C4Sc.UILanguagesById[User.UiLanguageId].Name);
                    Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo(SessionConfig.C4Sc.UILanguagesById[User.UiLanguageId].Name);
                }
                else
                {
                    // use configured value for user group
                    Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo(cultureN.InnerText);
                    Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo(cultureN.InnerText);
                }
            }
            return result;
        }



        public string Ticket { get; private set; }

        private void AddSessionExtension(string key, ISessionExtension ext)
        {
            if (!_sessionExtensions.ContainsKey(key))
                _sessionExtensions.Add(key, ext);
        }
        public void AddSessionExtensions()
        {
            foreach (XmlElement seEl in UserConfig.DocumentElement.SelectNodes("classes/session_extensions/session_extension"))
            {
                string asm = seEl.GetAttribute("assembly");
                string tp = seEl.GetAttribute("type");
                Assembly a = SessionConfig.GetAssembly(asm);
                if (a != null)
                {
                    ISessionExtension ext = (ISessionExtension)SessionConfig.GetAssembly(asm).CreateInstance(asm + "." + tp);
                    ext.Init(this, seEl);
                    string key = seEl.HasAttribute("key") ? seEl.GetAttribute("key") : asm + "." + tp;
                    AddSessionExtension(key, ext);
                }
                else 
                {
                    // TODO: log
                    // throw new ApplicationException(string.Format("Session extension {0}.{1} has not been found.", asm, tp));
                    Debug.Print(string.Format("Session extension {0}.{1} has not been found.", asm, tp));
                }
            }
        }

        public ISessionExtension GetSessionExtension(string key)
        {
            if (_sessionExtensions.ContainsKey(key))
                return _sessionExtensions[key];
            else
                return null;
        }

        private void UpdateLocks()
        {
            try
            {
                HashSet<long> locksRep = SearchObjectIds("<ExactPointQuery type=\"long\" fieldName=\"locker\" value=\"" + User.Id.ToString() + "\"/>");
                foreach(long lockId in LocksMgr.Locks.Keys) if (!locksRep.Contains(lockId)) LocksMgr.RemoveLock(lockId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public IContent GetContentAwarenessModule(C4Format fmt)
        {
            if (!(fmt == null) && _cams.ContainsKey(fmt.Name))
                return _cams[fmt.Name];
            else
                return null;
        }
        public void Close()
        {
            CommandSession.Disconnect();
            Ticket = null;
            User = null;
            //RepositoryName = null;
        }

        public CmnObject SearchSingleObject(string query, bool throwException)
        {
            SearchObjectsOperation soo = new SearchObjectsOperation(this, query);
            if (soo.TotalResultCount == 1)
                // If dict.TotalResultCount = 1 Then
                return soo.GetObjects(0, 1, SEARCH_BATCH_SIZE).Values.First();
            else if (throwException)
                throw new ApplicationException("Query returned count unequal to one");
            else
                return null;
        }
        public Dictionary<long, CmnObject> SearchObjects(string query, long pageSize = -1, long page = -1)
        {
            SearchObjectsOperation soo = new SearchObjectsOperation(this, query);
            return soo.GetObjects(page, pageSize, SEARCH_BATCH_SIZE);
        }
        //
        public HashSet<long> SearchObjectIds(string query)
        {
            return CommandSession.SearchObjectIds(query);
        }
        public CmnFolder SearchSingleFolder(string query)
        {
            SearchResults res = SearchFolders(query);
            if (res.Folders.Count == 1)
                return res.Folders.Values.First();
            else
                throw new ApplicationException("Query returned count unequal to one");
        }

        public HashSet<long> SearchFolderIds(string query)
        {
            return CommandSession.SearchFolderIds(query);
        }
        public SearchResults SearchFolders(string query, long? pageSize = null, long? pageNum = null)
        {
            // TODO: implement SearchFoldersOperation like SearchObjectsOperation
            return new SearchResults(null, WrapC4Folders(CommandSession.GetFoldersById(CommandSession.SearchFolderIds(query))));
        }
        public Dictionary<long, CmnFolder> WrapC4Folders(Dictionary<long, C4Folder> c4Folders)
        {
            Dictionary<long, CmnFolder> result = new Dictionary<long, CmnFolder>();
            foreach (long id in c4Folders.Keys) result.Add(id, new CmnFolder(this, c4Folders[id]));
            return result;
        }
        public Dictionary<long, CmnObject> WrapC4Objects(Dictionary<long, C4Object> c4Objects)
        {
            Dictionary<long, CmnObject> result = new Dictionary<long, CmnObject>();
            foreach (long id in c4Objects.Keys) result.Add(id, new CmnObject(this, c4Objects[id]));
            return result;
        }
        public CmnSessionConfiguration SessionConfig { get; private set; }
        public CmnUserSettings UserSettings { get; private set; }
        public XmlDocument UserConfig { get; private set; }
        public string GetHelpUrl(int errorCode=0)
        {
            // TODO: make configurable 
            if (errorCode == 0) return "https://cinnamon-cms.de/trac/wiki/Public/Docs";
            else return "https://cinnamon-cms.de/trac/wiki/Public/Docs/Errors/DataModelError"+errorCode.ToString();
        }
        public Dictionary<long, CmnFolder> GetFolders(HashSet<long> ids)
        {
            Dictionary<long, C4Folder> c4fs = CommandSession.GetFoldersById(ids);
            Dictionary<long, CmnFolder> result = new Dictionary<long, CmnFolder>();
            foreach(long id in c4fs.Keys) result[id] = new CmnFolder(this, c4fs[id]);
            return result;
        }

        public Dictionary<long, CmnObject> GetObjects(CmnFolder parent, Versions ver)
        {
            Dictionary<long, CmnObject> result = new Dictionary<long, CmnObject>();
            C4Session.VersionMode v=C4Session.VersionMode.All;
            switch(ver)
            {
                case Versions.Head: v = C4Session.VersionMode.Head; break;
                case Versions.Branch: v = C4Session.VersionMode.Branch; break;
                case Versions.All: v = C4Session.VersionMode.All; break;
            }
            Dictionary<long, C4Object> objs = CommandSession.GetObjectsByFolderId(parent.Id, false, v);
            foreach (C4Object c4o in objs.Values)
            {
                CmnObject o = new CmnObject(this, c4o);
                result.Add(c4o.Id, o);
            }
            return result;
        }

        public Dictionary<long, CmnFolder> GetSubfolders(CmnFolder parent)
        {
            //if (parent == null) return new Dictionary<long, CmnFolder>();
            Dictionary<long, C4Folder> c4fs = parent==null || parent.Id==0? CommandSession.GetSubFolders(RootFolder.Id) :CommandSession.GetSubFolders(parent.Id);
            Dictionary<long, CmnFolder> result = new Dictionary<long, CmnFolder>();
            foreach (long id in c4fs.Keys) result[id] = new CmnFolder(this, c4fs[id]);
            return result;
        }
        public CmnFolder GetFolder(string path)
        {
            if ((path ?? "") == "/")
                return _rootFolder;  // session
            else
            {
                C4Folder c4f = CommandSession.GetFolderByPath(path);
                return c4f==null?null:new CmnFolder(this, c4f);
                ////result = new CmnFolder(this, );
                //C4Folder currFolder = c4f;
                
                //while (currFolder != null)
                //{
                //    if (CachedFolders.ContainsKey(currFolder.Id)) CachedFolders[currFolder.Id] = currFolder;
                //    else CachedFolders.Add(currFolder.Id, currFolder);
                //    currFolder = currFolder.Parent;
                //}
                //CmnFolder result = null;
                //return result;
            }
        }
        public CmnFolder GetFolder(long id, bool forceReload)
        {
            if (forceReload || CachedFolders==null || !CachedFolders.ContainsKey(id))
            {
                HashSet<long> ids = new HashSet<long>();
                ids.Add(id);
                Dictionary<long, C4Folder> c4fs = CommandSession.GetFoldersById(ids);
                if(c4fs.Count==0) throw new ApplicationException("Folder not found: "+id.ToString());
                C4Folder c4f = CommandSession.GetFoldersById(ids).Values.First();
                CmnFolder result = new CmnFolder(this, c4f);
                CmnFolder currFolder = result;
                while(currFolder!=null)
                {
                    if (CachedFolders.ContainsKey(currFolder.Id)) CachedFolders[currFolder.Id] = currFolder;
                    else CachedFolders.Add(currFolder.Id, currFolder);
                    currFolder = currFolder.Parent;
                }
                return result;
            }
            else return CachedFolders[id];
        }
        public Dictionary<long, C4Relation> GetRelations(bool orMode, C4RelationType relationType = null, long? leftId = null, long? rightId = null)
        {

            HashSet<long> typeIds = null;
            if(relationType != null)
            {
                typeIds = new HashSet<long>();
                typeIds.Add((long)relationType.Id);
            }
            HashSet<long> leftIds = null;
            if (leftId != null)
            {
                leftIds = new HashSet<long>();
                leftIds.Add((long)leftId);
            }
            HashSet<long> rightIds = null;
            if (rightId != null)
            {
                rightIds = new HashSet<long>();
                rightIds.Add((long)rightId);
            }
            //return CommandSession.SearchRelations(true, true, null, leftIds, rightIds);
            return CommandSession.SearchRelations(true, orMode, typeIds, leftIds, rightIds);
        }

        public C4Folder GetOrCreateFolder(string path, C4FolderType assignFolderType = null, C4Acl assignAcl = null)
        {
            C4Folder c4f = CommandSession.GetFolderByPath(path);
            if (c4f==null)
            {
                string[] pathSegs = path.Split('/',StringSplitOptions.RemoveEmptyEntries);
                string parentPath = path.Substring(0, path.Length - pathSegs[pathSegs.Length - 1].Length - 1);
                long parentId = GetOrCreateFolder(parentPath, assignFolderType, assignAcl).ParentId;

                HashSet<C4Folder> folders = new HashSet<C4Folder>();
                folders.Add(new C4Folder(0, pathSegs[pathSegs.Length - 1], (long)User.Id, parentId, (long)(assignAcl == null ? SessionConfig.C4Sc.FolderTypesByName["_default_folder_type"].Id:assignFolderType.Id), (long)(assignAcl==null?SessionConfig.C4Sc.AclsByName["_default_acl"].Id:assignAcl.Id)));
                return CommandSession.CreateFolders(folders).Values.First();
            }
            else
            {
                return c4f;
            }
        }

        public CmnObject Create(long parentId, string name, string filename = null, long? formatId = null, long? languageId = null, long? typeId = null, long? lifecycleStateId = null, long? aclId=null)
        {
            C4Object c4o = CommandSession.CreateObject(parentId, 
                                                        name, 
                                                        (long)User.Id,
														(long)(aclId == null ? SessionConfig.C4Sc.AclsByName["_default_acl"].Id : aclId),
														(long)(typeId == null ? SessionConfig.C4Sc.ObjectTypesByName["_default_objtype"].Id : typeId),
														(long)(languageId == null ? SessionConfig.C4Sc.LanguagesByName["und"].Id : languageId),
                                                        lifecycleStateId,
                                                        formatId, 
                                                        filename);

            return new CmnObject(this, c4o);
        }

        public CmnFolder CreateSubfolder(CmnFolder parent, string name, C4FolderType type = null)
        {
            HashSet<C4Folder> folders = new HashSet<C4Folder>();
            folders.Add(new C4Folder(0, name, (long)User.Id, parent == null ? RootFolder.Id : parent.Id, type == null ? (long)SessionConfig.C4Sc.FolderTypesByName["_default_folder_type"].Id : (long)type.Id, (long)SessionConfig.C4Sc.AclsByName["_default_acl"].Id));
            return new CmnFolder(this, CommandSession.CreateFolders(folders).Values.First());
        }
        public CmnFolder RootFolder
        {
            get
            {
                if (_rootFolder == null)
                {
                    C4Folder subFolder = null;
                    try
                    {
                        subFolder = CommandSession.GetFolderByPath("/.data");
                    }
                    catch (Exception)
                    {
                        // nothing to do
                    }
                    if(subFolder == null)
                    {
                        try
                        {
                            subFolder = CommandSession.GetFolderByPath("/system");
                        }
                        catch (Exception)
                        {
                            // nothing to do
                        }
                    }
                    if (subFolder == null) throw new ApplicationException("Failure getting root folder.");
                    _rootFolder = new CmnFolder(this, subFolder).Parent;
                }
                //_rootFolder = new CmnFolder(this, new C4Folder(0, "", (long)SessionConfig.UsersByName["admin"].Id, 0, (long)SessionConfig.FolderTypesByName["_default_folder_type"].Id, (long)SessionConfig.AclsByName["_default_acl"].Id));
                return _rootFolder;
            }
        }

        public C4User User { get; private set; }

        private C4User GetUserByName(string name)
        {
            foreach (C4User u in SessionConfig.C4Sc.UsersById.Values)
            {
                if ((u.Name ?? "") == (name ?? ""))
                    return u;
            }
            return null;
        }



        public XmlDocument GetConfigEntry(string configEntryName)
        {
            if (_configEntries==null)
            {
                try
                {
                    Dictionary<long, C4ConfigEntry> ceList  = CommandSession.ListConfigEntries();
                    _configEntries = new Dictionary<string, XmlDocument>();
                    foreach (C4ConfigEntry ce in ceList.Values) _configEntries.Add(ce.Name, ce.Config);
                }
                catch (Exception)
                {
                    // TODO: throw exception
                    _configEntries.Add(configEntryName, null);
                }
            }
            if(_configEntries.ContainsKey(configEntryName)) return _configEntries[configEntryName];
            else return null;
        }

        public CmnObject GetObject(long id, bool includeCustomMetadata)
        {
            HashSet<long> ids = new HashSet<long>();
            ids.Add(id);
            return new CmnObject(this, CommandSession.GetObjectsById(ids, includeCustomMetadata).Values.First());
        }
        public Dictionary<long, CmnObject> GetObjects(HashSet<long> ids, bool includeCustomMetadata)
        {
            Dictionary<long, CmnObject> result = new Dictionary<long, CmnObject>();
            Dictionary<long, C4Object> c4os = CommandSession.GetObjectsById(ids, includeCustomMetadata);
            foreach (C4Object c4o in c4os.Values)
            {
                result.Add(c4o.Id, new CmnObject(this, c4o));
            }
            return result;
        }
        public bool AllowBranching { get; private set; }

        //public XmlDocument ExecuteApiCommand(string cmd, Dictionary<string, string> stringParameters, int timeout)
        //{
        //    Dictionary<string, string> fields = new Dictionary<string, string>();
        //    fields.Add("command", cmd);
        //    foreach (string p in stringParameters.Keys)
        //        fields.Add(p, stringParameters[p]);
        //    fields.Add("ticket", Ticket);
        //    XmlDocument respDoc = Server.ServerAdaptor.PostRequest(Server.GetServerUrl(fields["command"]), fields, timeout);
        //    return respDoc;
        //}
        
        public bool Equals(CmnSession otherSession)
        {
            if (otherSession == null)
                return false;
            else
                return (Ticket ?? "") == (otherSession.Ticket ?? "");
        }
        public static bool operator ==(CmnSession session1, CmnSession session2)
        {
            if (session1 is null)
                return session2 is null;
            else
                return session1.Equals(session2);
        }
        public static bool operator !=(CmnSession session1, CmnSession session2)
        {
            return !(session1 == session2);
        }

        ~CmnSession()
        {
        }
    }
}

