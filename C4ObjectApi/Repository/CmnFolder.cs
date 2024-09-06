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
using System.Diagnostics;
using System.Linq;
using System.Collections.Generic;
using System;
using System.Xml;
using C4ObjectApi.Interfaces;
using C4ObjectApi.Exceptions;
using System.IO;
using ContentAwareness;
using C4ServerConnector.Assets;
using C4ServerConnector;

namespace C4ObjectApi.Repository
{
    public class CmnFolder : IRepositoryNode, IEquatable<CmnFolder>
    {
        private string _name;
        //protected CmnFolder _parent;
        private C4Folder _c4f;
        //private C4Acl _acl;
        //private C4User _owner;
        //private XmlDocument _metadata;
        //private C4FolderType _type;
        //private C4Permissions _permissions;
        //private bool _metadataChanged;
        //private XmlDocument _summary;
        //private XmlElement _source;
        private string _folderPath;

        public enum FolderMetadata
        {
            AclId,
            Name,
            ParentFolderId,
            OwnerId,
            Metadata,
            TypeId
        }

        public CmnFolder(CmnSession session, C4Folder c4f)
        {
            Session = session;
            _c4f = c4f;
            Permissions = Session.SessionConfig.GetCombinedPermissions(Session.SessionConfig.C4Sc.AclsById[_c4f.AclId], (long)Session.User.Id == c4f.OwnerId, Session.IsSuperuser);
        }

        public long Id { get { return _c4f.Id; } }
        public string Name
        {
            get
            {
                return _c4f.Name;
            }
        }
        public C4User Owner 
        { 
            get 
            { 
                return Session.SessionConfig.C4Sc.UsersById[_c4f.OwnerId]; 
            }
        }  
        public C4Acl Acl
        {
            get
            {
                return Session.SessionConfig.C4Sc.AclsById[_c4f.AclId];
            }
        }     // make this writable
        public long ParentId
        {
            get
            {
                return _c4f.ParentId;
            }
        }
        public CmnFolder Parent
        {
            get
            {
                return ParentId==0?null:Session.GetFolder(ParentId, false);
            }
        }
        public string FolderPath
        {
            get
            {
                return Parent==null?"":string.Concat(Parent.FolderPath, "/",Name);
            }
        }
        public C4FolderType FolderType
        {
            get
            {
                return Session.SessionConfig.C4Sc.FolderTypesById[_c4f.TypeId];
            }
            set
            {
                Session.CommandSession.UpdateFolder(Id, null, null, null, value.Id);
                _c4f.TypeId = (long)value.Id;
            }
        }     
        public bool MetadataChanged 
        { 
            get 
            { 
                return _c4f.MetadataChanged; 
            }
            //set
            //{
            //    Session.CommandSession.UpdateFolder(Id, null, null, null, value.Id);
            //    _c4f.TypeId = (long)value.Id;
            //}
        }      // make this writable by admin
        public XmlElement Summary 
        { 
            get 
            { 
                return _c4f.Summary; 
            }
            set
            {
                Session.CommandSession.SetFolderSummary(Id, value);
                _c4f.Summary = value;
            }
        }      
        public C4Permissions Permissions { get; }
        public CmnSession Session { get; }

        public bool HasSubfolders
        {
            get
            {
                return _c4f.HasSubfolders;
            }
        }


public void SetChangedStatus(bool metadataChanged)
        {
            if (Session.User.ChangeTracking)
                throw new ApplicationException("Change status flags can only be set by users without automatic change tracking.");
            Dictionary<string, string> fields = new Dictionary<string, string>();
            Session.CommandSession.UpdateFolder(Id, null, null, null, null, null, metadataChanged);
        }
        //public C4Metadata Metadata
        //{
        //    get
        //    {
        //        return Session.CommandSession.GetFolderMeta(Id);
        //    }
        //    set
        //    {
        //        ApiHelper.SetFolderMeta(Session.CommandSession, Id, value.LegacyXml.DocumentElement, Session.SessionConfig.C4Sc.MetasetTypesByName);
        //    }
        //}
        /// <summary>
        /// Return all folders inside a folder.
        /// </summary>
        /// <remarks>Causes a command to be send to the server</remarks>
        public Dictionary<long, CmnFolder> GetSubfolders()
        {
            return Session.GetSubfolders(this);
        }

        /// <summary>
        /// Creates a subfolder to this folder in the repository.
        /// </summary>
        /// <param name="name">the name of the subfolder</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public CmnFolder CreateSubfolder(string name)
        {
            return Session.CreateSubfolder(this, name);
        }


        /// <summary>
        /// Delete this folder in the repository.
        /// </summary>
        /// <remarks>empty folders only</remarks>
        private void DeleteFolder() // empty folders only
        {
            HashSet<long> ids = new HashSet<long>();
            ids.Add(Id);
            Session.CommandSession.DeleteFolders(ids, false, false);
            if (Session.CachedFolders.ContainsKey(Id)) Session.CachedFolders.Remove(Id);
        }

        /// <summary>
        /// Deletes a folder and its content recursively
        /// </summary>
        /// <param name="deleteContents"><c>true</c> - delete folder content recursively</param>
        /// <remarks></remarks>
        public void Delete(bool deleteContents)
        {
            if (deleteContents)
            {
                Dictionary<long, CmnObject> rootObjects = new Dictionary<long, CmnObject>();
                foreach (CmnObject o in GetObjects(CmnSession.Versions.All).Values)
                {
                    if (!rootObjects.ContainsKey(o.RootId))
                        rootObjects.Add(o.RootId, o);
                }
                foreach (CmnObject o in rootObjects.Values)
                {
                    if (o.Link == null)
                        o.Delete(true, false);
                    else
                        o.Link.Delete();
                }
                foreach (CmnFolder f in GetSubfolders().Values)
                {
                    if (f.Link == null)
                        f.Delete(true);
                    else
                        f.Link.Delete();
                }
                DeleteFolder();
            }
            else
                DeleteFolder();
        }
        public Dictionary<long, CmnObject> GetObjects(CmnSession.Versions ver)
        {
            return Session.GetObjects(this, ver);
        }

        private void CollectChildren(IContent c, string filename, List<string> children)
        {
            HashSet<string> refs = c.GetReferences(filename, Session.CommandSession, Session.SessionConfig.C4Sc);
            string rootPath = Path.GetDirectoryName(filename); // filename.Substring(0, filename.Length - Path.GetFileName(filename).Length)
            foreach (string childFn in refs)
            {
                string fullChildFn = childFn.Contains(":") | childFn.StartsWith(Path.DirectorySeparatorChar.ToString()) ? Path.GetFullPath(childFn) : Path.Combine(rootPath, childFn);
                // If fullFn.Contains("/") Then
                // Debug.Print("fullFn contains '/'")
                // End If
                // fullFn = Replace(fullFn, "/", Path.DirectorySeparatorChar) ' TODO: softcode separator to make compatible with Mono
                if (!children.Contains(fullChildFn))
                {
                    string ext = Path.GetExtension(fullChildFn).Trim().Substring(1).ToLower();
                    C4Format format = Session.SessionConfig.C4Sc.FormatsByExtension[ext];
                    IContent subC = Session.GetContentAwarenessModule(format);
                    children.Add(fullChildFn);
                    if (!(subC == null))
                        CollectChildren(subC, fullChildFn, children);
                }
            }
        }


        public CmnObject ImportObject(string fn, string name, C4ObjectType objType = null, C4Language language = null, C4Format format = null)
        {

            // TODO: this function must be changed to use the RootImportFileNode class

            // refresh content adaptor container in session
            IContent c = Session.GetContentAwarenessModule(Session.SessionConfig.C4Sc.FormatsByExtension[Path.GetExtension(fn).Substring(1).ToLower()]);
            if (c == null)
            {
                // single object only
                name = name.Length > 0 ? name : Path.GetFileNameWithoutExtension(fn);

                CmnObject o = Session.Create(Id, name, fn, format.Id, language==null?null:language.Id, objType==null?null:objType.Id,null, Session.SessionConfig.C4Sc.AclsByName["_default_acl"].Id);
                return o;
            }
            else
            {
                List<string> children = new List<string>();
                CollectChildren(c, fn, children);

                // get unresolved references
                List<string> unresolvedReferences = new List<string>();
                foreach (string fullChildFn in children)
                {
                    if (!File.Exists(fullChildFn))
                    {
                        string key = Path.GetFileName(fullChildFn);
                        if (!unresolvedReferences.Contains(key))
                        {
                            unresolvedReferences.Add(key);
                            Debug.Print(key);
                        }
                    }
                }
                if (unresolvedReferences.Count > 0)
                {
                    CmnDataModelException ex = new CmnDataModelException(DataModelErrorCodes.UNRESOLVED_INBOUND_REFERENCES, "Unresolved inbound references");
                    ex.Data["unresolved_refs"] = unresolvedReferences;
                    throw ex;
                }


                // create objects for the dependencies
                Dictionary<string, CmnObject> fnToObj = new Dictionary<string, CmnObject>();
                CmnObject o;
                foreach (string fullChildFn in children)
                {
                    o = Session.Create(Id, Path.GetFileNameWithoutExtension(fullChildFn), null, null, language.Id, objType.Id,null, Session.SessionConfig.C4Sc.AclsByName["_default_acl"].Id);
                    fnToObj.Add(Path.GetFileName(fullChildFn), o);
                }

                name = name.Length > 0 ? name : Path.GetFileNameWithoutExtension(fn);
                o = Session.Create(Id, name, null, null, language.Id, objType.Id, null, Session.SessionConfig.C4Sc.AclsByName["_default_acl"].Id);
                fnToObj.Add(Path.GetFileName(fn), o);


                foreach (string fullChildFn in children)
                {
                    SetDependencies(fullChildFn, fnToObj);
                    o = fnToObj[Path.GetFileName(fullChildFn)];
                    o.Lock();
                    o.SetContent(fullChildFn, Session.SessionConfig.C4Sc.FormatsByExtension[Path.GetExtension(fullChildFn).Substring(1).ToLower()]);
                    o.Unlock();
                }
                SetDependencies(fn, fnToObj);
                o = fnToObj[Path.GetFileName(fn)];
                o.Lock();
                o.SetContent(fn, format);
                o.Unlock();
                return o;
            }
        }


        /// <summary>
        /// Set the dependencies for a file.
        /// </summary>
        /// <param name="fnToObj">a dictionary containing all dependencies for the file</param>
        /// <remarks></remarks>
        private void SetDependencies(string fn, Dictionary<string, CmnObject> fnToObj)
        {
            // delete relations
            CmnObject obj = fnToObj[Path.GetFileName(fn)];

            // create relations
            IContent c = Session.GetContentAwarenessModule(Session.SessionConfig.C4Sc.FormatsByExtension[Path.GetExtension(fn).Substring(1).ToLower()]);
            foreach (string childRef in c.GetReferences(fn, Session.CommandSession, Session.SessionConfig.C4Sc))
            {
                CmnObject subObj = fnToObj[Path.GetFileName(childRef)];
                // Dim subObj As CmnObject = CType(fnToObj(dd), CmnObject)
                XmlDocument metadata = new XmlDocument();
                XmlElement metaEl = (XmlElement)metadata.AppendChild(metadata.CreateElement("metadata"));
                XmlElement filenameEl = (XmlElement)metaEl.AppendChild(metadata.CreateElement("filename"));
                filenameEl.InnerText = childRef;
                HashSet<C4Relation> relations = new HashSet<C4Relation>();
                relations.Add(new C4Relation((long)Session.SessionConfig.C4Sc.RelationTypesByName[Constants.CHILD_CONTENT_RELATION].Id, obj.Id, subObj.Id, metadata));
                Session.CommandSession.CreateRelations(relations);
            }
        }

        /// <summary>
        /// Set name and or description of an folder.
        /// currently accepted fields
        /// <list type="bullet">
        /// <item><description>[parentid] = new parent id</description></item>
        /// <item><description>[name]= new name</description></item>
        /// </list>
        /// </summary>
        /// <exception cref="System.Exception">if the command failed</exception>
        /// <remarks></remarks>
        public void CreateLinks(Dictionary<long,IRepositoryNode> targets, C4Acl linkAcl=null)
        {
            if (linkAcl == null) linkAcl = Session.SessionConfig.C4Sc.AclsByName["_default_acl"];
            HashSet<C4Link> links = new HashSet<C4Link>();
            foreach(IRepositoryNode target in targets.Values)
            {
                if(target.GetType() == typeof(CmnObject)) links.Add(new C4Link(Id, (long)linkAcl.Id, (long)Session.User.Id, C4Link.LinkTypes.Object, target.Id));
                else links.Add(new C4Link(Id, (long)linkAcl.Id, (long)Session.User.Id, C4Link.LinkTypes.Folder, target.Id));
            }
            Dictionary<long, C4Link> createdLinks = Session.CommandSession.CreateLinks(links);
        }

        public bool Equals(CmnFolder otherFolder)
        {
            if (otherFolder == null)
                return false;
            else
                return Id == otherFolder.Id;
        }

        public static bool operator ==(CmnFolder folder1, CmnFolder folder2)
        {
            if (folder1 is null)
                return folder2 is null;
            else
                return folder1.Equals(folder2);
        }

        public static bool operator !=(CmnFolder folder1, CmnFolder folder2)
        {
            return !(folder1 == folder2);
        }

        public void Refresh()
        {
            HashSet<long> ids = new HashSet<long>();
            ids.Add(Id);
            _c4f = Session.CommandSession.GetFoldersById(ids).Values.First();
        }
        public CmnLink Link { get; private set; }
    }
}
