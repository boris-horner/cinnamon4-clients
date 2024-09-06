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
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Reflection;
using System;
using C4ServerConnector.Assets;
using C4ServerConnector;

namespace C4ObjectApi.Repository
{
    public class CmnSessionConfiguration
    {
        private readonly Dictionary<string, Assembly> _assemblies;
        private readonly XmlDocument _localizedLabels;
        private readonly string _uiLanguageSysname;
        private CmnFolder _homeFolder;
        private CmnFolder _searchesFolder;
        private C4AllPermissions _allPermissions;
        private Dictionary<long, C4Permissions> _permissionsByAclId;
        private Dictionary<long, C4Permissions> _ownerPermissionsByAclId;
        private C4Group _ownerGroup;

        private readonly CmnSession _s;
        public readonly C4SessionConfiguration C4Sc;

        public HashSet<long> CopyMetasetTypeIds { get; private set; }
        public CmnSessionConfiguration(CmnSession s, string userName, Dictionary<long, C4Group> groups)
        {
            _s = s;
            _assemblies = new Dictionary<string, Assembly>();
            _localizedLabels = _s.GetConfigEntry("server.i18n");
            C4User u=_s.CommandSession.GetUser(userName);
            // TODO: softcode temp path
            C4Sc = new C4SessionConfiguration(_s.CommandSession, userName, Path.GetTempPath(), _localizedLabels, u.UiLanguageId);
            _allPermissions = _s.CommandSession.ListPermissions();
            _ownerGroup = C4Sc.GroupsByName["_owner"];
            _permissionsByAclId = new Dictionary<long, C4Permissions>();
            _ownerPermissionsByAclId = new Dictionary<long, C4Permissions>();
            CopyMetasetTypeIds = new HashSet<long>();
            foreach (string msTypeName in C4Sc.MetasetTypesByName.Keys) 
                if (msTypeName != "tika" && msTypeName != "html_preview") 
                    CopyMetasetTypeIds.Add((long)C4Sc.MetasetTypesByName[msTypeName].Id);
        }

        public C4Permissions GetCombinedPermissions(C4Acl acl, bool owner, bool superuser)
        {
            if(superuser) return new C4Permissions(true, _allPermissions);
            if (!_permissionsByAclId.ContainsKey((long)acl.Id))
            {
                _permissionsByAclId.Add((long)acl.Id, _s.CommandSession.GetUserPermissions((long)_s.User.Id,(long)acl.Id));
                C4AclGroup ownerAclGroup = null;
                foreach(C4AclGroup aclGroup in acl.AclGroups.Values) if (aclGroup.GroupId == _ownerGroup.Id)
                {
                    ownerAclGroup = aclGroup;
                    break;
                }
                _ownerPermissionsByAclId.Add((long)acl.Id, ownerAclGroup!=null ? ownerAclGroup.Permissions : new C4Permissions(false, _allPermissions));
                //foreach (long permissionId in _permissionsByAclId[(long)acl.Id].PermissionNamesById.Keys) 
                //    if (!_ownerPermissionsByAclId[(long)acl.Id].PermissionNamesById.ContainsKey(permissionId)) _ownerPermissionsByAclId[(long)acl.Id].SetPermission(_permissionsByAclId[(long)acl.Id].PermissionNamesById[permissionId], true);
            }   
            return owner? _ownerPermissionsByAclId[(long)acl.Id] : _permissionsByAclId[(long)acl.Id];
        }

        public string GetLocalizedLabel(string sysName, string category)
        {
            if (_localizedLabels == null)
                return sysName;
            XmlNode translationEl = _localizedLabels.SelectSingleNode("/config/i18n/messages[@isoCode='" + _uiLanguageSysname + "']/message[@category='" + category + "' and id='" + sysName + "']/translation");
            if (!(translationEl == null))
                return translationEl.InnerText;
            translationEl = _localizedLabels.SelectSingleNode("/config/i18n/messages[@default='true']/message[@category='" + category + "' and id='" + sysName + "']/translation");
            if (!(translationEl == null))
                return translationEl.InnerText;
            return sysName;
        }

        public CmnFolder HomeFolder
        {
            get
            {
                if (_homeFolder == null)
                    _homeFolder = _s.GetFolder(string.Concat("/.data/users/", _s.User.Name, "/home"));
                if(_homeFolder==null)
                {
                    CmnFolder usersFolder= _s.GetFolder(string.Concat("/.data/users/", _s.User.Name));
                    if(usersFolder!=null) _homeFolder = usersFolder.CreateSubfolder("home");
                }
                return _homeFolder;
            }
        }

        public CmnFolder SearchesFolder
        {
            get
            {
                if (_searchesFolder == null)
                    _searchesFolder = _s.GetFolder(string.Concat("/.data/users/" + _s.User.Name, "/searches"));
                if (_searchesFolder == null)
                {
                    CmnFolder usersFolder = _s.GetFolder(string.Concat("/.data/users/", _s.User.Name));
                    if (usersFolder != null) _searchesFolder = usersFolder.CreateSubfolder("searches");
                }
                return _searchesFolder;
            }
        }

        public Assembly GetAssembly(string name)
        {
            if (!_assemblies.ContainsKey(name))
            {
                try
                {
                    string asmPath= Path.GetDirectoryName(new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath);
                    Assembly asm = Assembly.LoadFrom(Path.Combine(asmPath, name + ".dll"));
                    _assemblies.Add(name, asm);
                }
                catch(FileNotFoundException ex)
                {
                    return null;    // throw new ApplicationException(string.Format("Failure loading assembly {0}.", name));
                }
            }
            return _assemblies[name];
        }


    }
}

