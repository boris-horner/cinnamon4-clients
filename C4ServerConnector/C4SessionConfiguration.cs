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
using System;
using System.Collections.Generic;
using System.Xml;
using C4ServerConnector.Assets;

namespace C4ServerConnector
{
    public class C4SessionConfiguration
    {
        public const string ACL_TYPE_DEFAULT = "_default_acl";
        private Dictionary<string, C4ObjectType> _objectTypesByName;
        private Dictionary<long, C4ObjectType> _objectTypesById;
        private Dictionary<string, C4FolderType> _folderTypesByName;
        private Dictionary<long, C4FolderType> _folderTypesById;
        private Dictionary<string, C4MetasetType> _metasetTypesByName;
        private Dictionary<long, C4MetasetType> _metasetTypesById;
        private Dictionary<long, C4Format> _formatsById;
        private Dictionary<string, C4Format> _formatsByName;
        private Dictionary<string, C4Format> _formatsByExt;
        private Dictionary<long, C4User> _usersById;
        private Dictionary<string, C4User> _usersByName;
        private Dictionary<string, C4RelationType> _relTypesByName;
        private Dictionary<long, C4RelationType> _relTypesById;
        private Dictionary<long, C4Acl> _aclsById;
        private Dictionary<string, C4Acl> _aclsByName;
        private Dictionary<long, C4Group> _groupsById;
        private Dictionary<string, C4Group> _groupsByName;
        private Dictionary<string, C4IndexItem> _indexItemsByName;
        private Dictionary<long, C4Language> _languagesById;
        private Dictionary<string, C4Language> _languagesByName;
        private Dictionary<long, C4Language> _uiLanguagesById;
        private Dictionary<string, C4Language> _uiLanguagesByName;
        private Dictionary<long, C4Lifecycle> _lifecyclesById;
        private Dictionary<string, C4Lifecycle> _lifecyclesByName;
        private Dictionary<long, C4LifecycleState> _lifecycleStatesById;
        private Dictionary<string, C4LifecycleState> _lifecycleStatesByName;
        private C4Folder _notificationFolder;
        private long? _uiLanguageId;
        private string _uiLanguageIso;

        private Dictionary<string, XmlDocument> _configEntries;

        private readonly C4Session _c4s;
        public string Username { get; private set; }
        public string LocalTempFolder { get; private set; }
        public XmlDocument LocalizedLabels { get; private set; }
        public Dictionary<string, C4Folder> CachedFolders { get; private set; } = new Dictionary<string, C4Folder>();
        public C4SessionConfiguration(C4Session c4s, string username, string localTempFolder, XmlDocument localizedLabels, long? uiLanguageId)
        {
            _c4s = c4s;
            Username = username;
            LocalTempFolder = localTempFolder;
            LocalizedLabels = localizedLabels;
            _uiLanguageId = uiLanguageId;
            if(_uiLanguageId != null)
            {
                _uiLanguageIso = UILanguagesById[(long)_uiLanguageId].Name;
            }
        }

        // *** Users and Groups ***
        public Dictionary<long, C4User> UsersById
        {
            get
            {
                if (_usersById == null) InitUsers();
                return _usersById;
            }
        }

        public Dictionary<string, C4User> UsersByName
        {
            get
            {
                if (_usersByName == null) InitUsers();
                return _usersByName;
            }
        }

        public void InitUsers()     // this is now public because in change triggers, it makes sense to upload the user list after a new user has been created
        {
            _usersById = _c4s.ListUsers();
            _usersByName = new Dictionary<string, C4User>();
            foreach (C4User u in _usersById.Values) _usersByName.Add(u.Name, u);
        }

        public Dictionary<long, C4Group> GroupsById
        {
            get
            {
                if (_groupsById == null) InitGroups();
                return _groupsById;
            }
        }

        public Dictionary<string, C4Group> GroupsByName
        {
            get
            {
                if (_groupsByName == null) InitGroups();
                return _groupsByName;
            }
        }

        private void InitGroups()
        {
            _groupsById = _c4s.ListGroups();
            _groupsByName = new Dictionary<string, C4Group>();
            foreach (C4Group g in _groupsById.Values)
            {
                g.LocalizedName = GetLocalizedLabel(g.Name, "group");
                _groupsByName.Add(g.Name, g);
            }
        }



        // *** Lifecycles and states ***
        public Dictionary<long, C4Lifecycle> LifecyclesById
        {
            get
            {
                if (_lifecyclesById == null) InitLifecycles();
                return _lifecyclesById;
            }
        }

        public Dictionary<string, C4Lifecycle> LifecyclesByName
        {
            get
            {
                if (_lifecyclesByName == null) InitLifecycles();
                return _lifecyclesByName;
            }
        }

        public Dictionary<long, C4LifecycleState> LifecycleStatesById
        {
            get
            {
                if (_lifecycleStatesById == null) InitLifecycles();
                return _lifecycleStatesById;
            }
        }

        public Dictionary<string, C4LifecycleState> LifecycleStatesByName
        {
            get
            {
                if (_lifecycleStatesByName == null) InitLifecycles();
                return _lifecycleStatesByName;
            }
        }

        public void InitLifecycles()
        {
            _lifecyclesById = _c4s.ListLifecycles();
            _lifecyclesByName = new Dictionary<string, C4Lifecycle>();
            _lifecycleStatesById = new Dictionary<long, C4LifecycleState>();
            _lifecycleStatesByName = new Dictionary<string, C4LifecycleState>();
            foreach (C4Lifecycle lc in _lifecyclesById.Values)
            {
                lc.LocalizedName = GetLocalizedLabel(lc.Name, "lifecycle");
                _lifecyclesByName.Add(lc.Name, lc);
                foreach (C4LifecycleState lcs in lc.States.Values)
                {
                    lcs.LocalizedName = GetLocalizedLabel(lcs.Name, "lifecycle_state");
                    _lifecycleStatesById.Add((long)lcs.Id, lcs);
                    _lifecycleStatesByName.Add(lcs.Name, lcs);
                }
            }
        }




        // *** ACLs ***
        public Dictionary<long, C4Acl> AclsById
        {
            get
            {
                if (_aclsById == null) InitAcls();
                return _aclsById;
            }
        }

        public Dictionary<string, C4Acl> AclsByName
        {
            get
            {
                if (_aclsByName == null) InitAcls();
                return _aclsByName;
            }
        }

        private void InitAcls()
        {
            _aclsById = _c4s.ListAcls();
            _aclsByName = new Dictionary<string, C4Acl>();
            foreach (C4Acl a in _aclsById.Values)
            {
                a.LocalizedName = GetLocalizedLabel(a.Name, "acl");
                _aclsByName.Add(a.Name, a);
            }
        }


        // *** Relation types ***
        public Dictionary<string, C4RelationType> RelationTypesByName
        {
            get
            {
                if (_relTypesByName == null) InitRelationTypes();
                return _relTypesByName;
            }
        }
        public Dictionary<long, C4RelationType> RelationTypesById
        {
            get
            {
                if (_relTypesById == null) InitRelationTypes();
                return _relTypesById;
            }
        }


        private void InitRelationTypes()
        {
            _relTypesById = _c4s.ListRelationTypes();
            _relTypesByName = new Dictionary<string, C4RelationType>();
            foreach (C4RelationType rt in _relTypesById.Values)
            {
                rt.LocalizedName = GetLocalizedLabel(rt.Name, "relation_type");
                _relTypesByName.Add(rt.Name, rt);
            }
        }




        // *** Object and folder types ***
        public Dictionary<string, C4ObjectType> ObjectTypesByName
        {
            get
            {
                if (_objectTypesByName == null) InitObjectTypes();
                return _objectTypesByName;
            }
        }
        public Dictionary<long, C4ObjectType> ObjectTypesById
        {
            get
            {
                if (_objectTypesById == null) InitObjectTypes();
                return _objectTypesById;
            }
        }
        private void InitObjectTypes()
        {
            _objectTypesById = _c4s.ListObjectTypes();
            _objectTypesByName = new Dictionary<string, C4ObjectType>();
            foreach (C4ObjectType ot in _objectTypesById.Values)
            {
                ot.LocalizedName = GetLocalizedLabel(ot.Name, "object_type");
                _objectTypesByName.Add(ot.Name, ot);
            }
        }

        public Dictionary<string, C4FolderType> FolderTypesByName
        {
            get
            {
                if (_folderTypesByName == null) InitFolderTypes();
                return _folderTypesByName;
            }
        }

        public Dictionary<long, C4FolderType> FolderTypesById
        {
            get
            {
                if (_folderTypesById == null) InitFolderTypes();
                return _folderTypesById;
            }
        }

        private void InitFolderTypes()
        {
            _folderTypesById = _c4s.ListFolderTypes();
            _folderTypesByName = new Dictionary<string, C4FolderType>();
            foreach (C4FolderType ft in _folderTypesById.Values)
            {
                ft.LocalizedName = GetLocalizedLabel(ft.Name, "folder_type");
                _folderTypesByName.Add(ft.Name, ft);
            }
        }




        // *** Metaset types ***
        public Dictionary<string, C4MetasetType> MetasetTypesByName
        {
            get
            {
                if (_metasetTypesByName == null) InitMetasetTypes();
                return _metasetTypesByName;
            }
        }

        public Dictionary<long, C4MetasetType> MetasetTypesById
        {
            get
            {
                if (_metasetTypesById == null) InitMetasetTypes();
                return _metasetTypesById;
            }
        }

        private void InitMetasetTypes()
        {
            _metasetTypesById = _c4s.ListMetasetTypes();
            _metasetTypesByName = new Dictionary<string, C4MetasetType>();
            foreach (C4MetasetType mst in _metasetTypesById.Values) _metasetTypesByName.Add(mst.Name, mst);
        }




        // *** Index items ***
        public Dictionary<string, C4IndexItem> IndexItemsByName
        {
            get
            {
                if (_indexItemsByName == null)
                {
                    _indexItemsByName = _c4s.ListIndexItems(true);
                    foreach (C4IndexItem ii in _indexItemsByName.Values)
                    {
                        ii.LocalizedName = GetLocalizedLabel(ii.Name, "index_item");
                    }
                }
                return _indexItemsByName;
            }
        }

        // *** Languages ***
        public Dictionary<long, C4Language> LanguagesById
        {
            get
            {
                if (_languagesById == null) InitLanguages();
                return _languagesById;
            }
        }
        private void InitLanguages()
        {
            _languagesById = _c4s.ListLanguages();
            _languagesByName = new Dictionary<string, C4Language>();
            foreach (C4Language l in _languagesById.Values)
            {
                l.LocalizedName = GetLocalizedLabel(l.Name, "language");
                _languagesByName.Add(l.Name, l);
            }
        }
        public Dictionary<string, C4Language> LanguagesByName
        {
            get
            {
                if (_languagesByName == null) InitLanguages();
                return _languagesByName;
            }
        }

        public Dictionary<long, C4Language> UILanguagesById
        {
            get
            {
                if (_uiLanguagesById == null) InitUiLanguages();
                return _uiLanguagesById;
            }
        }
        public Dictionary<string, C4Language> UILanguagesByName
        {
            get
            {
                if (_uiLanguagesByName == null) InitUiLanguages();
                return _uiLanguagesByName;
            }
        }
        private void InitUiLanguages()
        {
            _uiLanguagesById = _c4s.ListUiLanguages();
            _uiLanguagesByName = new Dictionary<string, C4Language>();
            foreach (C4Language l in _uiLanguagesById.Values)
            {
                l.LocalizedName = GetLocalizedLabel(l.Name, "language");
                _uiLanguagesByName.Add(l.Name, l);
            }
        }


        // *** Formats ***
        public Dictionary<string, C4Format> FormatsByName
        {
            get
            {
                if (_formatsByName == null) InitFormats();
                return _formatsByName;
            }
        }

        public Dictionary<string, C4Format> FormatsByExtension
        {
            get
            {
                if (_formatsByExt == null) InitFormats();
                return _formatsByExt;
            }
        }

        public Dictionary<long, C4Format> FormatsById
        {
            get
            {
                if (_formatsById == null) InitFormats();
                return _formatsById;
            }
        }

        private void InitFormats()
        {
            _formatsById = _c4s.ListFormats();
            _formatsByName = new Dictionary<string, C4Format>();
            _formatsByExt = new Dictionary<string, C4Format>();
            foreach (C4Format fmt in _formatsById.Values)
            {
                fmt.LocalizedName = GetLocalizedLabel(fmt.Name, "format");
                _formatsByExt.Add(fmt.Extension, fmt);
                _formatsByName.Add(fmt.Name, fmt);
            }
        }


        // *** notifications ***
        public C4Folder NotificationFolder
        {
            get
            {
                if(_notificationFolder==null)
                {
                    _notificationFolder = _c4s.GetFolderByPath("/.data/transient/notifications"); 
                    if (_notificationFolder == null) throw new ApplicationException("Failure getting notification folder.");
                }
                return _notificationFolder;
            }
        }
        // I18N
        public Dictionary<string, XmlDocument> ConfigEntries
        {
            get
            {
                if (_configEntries == null)
                {
                    Dictionary<long, C4ConfigEntry> cesById = _c4s.ListConfigEntries();
                    _configEntries = new Dictionary<string, XmlDocument>();
                    foreach(C4ConfigEntry ce in cesById.Values)
                    {
                        _configEntries.Add(ce.Name, ce.Config);
                    }
                }
                return _configEntries;
            }
        }
        public void ResetConfigEntries()
        {
            _configEntries = null;
        }

        public string GetLocalizedLabel(string sysName, string category)
        {
            if (LocalizedLabels == null)
                return sysName;
            XmlNode translationEl = LocalizedLabels.SelectSingleNode("/config/i18n/messages[@isoCode='" + _uiLanguageIso + "']/message[@category='" + category + "' and id='" + sysName + "']/translation");
            if (!(translationEl == null))
                return translationEl.InnerText;
            translationEl = LocalizedLabels.SelectSingleNode("/config/i18n/messages[@default='true']/message[@category='" + category + "' and id='" + sysName + "']/translation");
            if (!(translationEl == null))
                return translationEl.InnerText;
            return sysName;
        }
    }
}

