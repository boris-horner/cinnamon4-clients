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
            if (_uiLanguageId != null)
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
            Dictionary<long, C4User> usersById;
            Dictionary<string, C4User> usersByName;

            usersById = _c4s.ListUsers();
            usersByName = new Dictionary<string, C4User>();
            foreach (C4User u in usersById.Values) if (!usersByName.ContainsKey(u.Name)) usersByName.Add(u.Name, u);

            _usersById = usersById;
            _usersByName = usersByName;
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
            Dictionary<long, C4Group> groupsById;
            Dictionary<string, C4Group> groupsByName;

            groupsById = _c4s.ListGroups();
            groupsByName = new Dictionary<string, C4Group>();
            foreach (C4Group g in groupsById.Values) if (!groupsByName.ContainsKey(g.Name))
                {
                    g.LocalizedName = GetLocalizedLabel(g.Name, "group");
                    groupsByName.Add(g.Name, g);
                }

            _groupsById = groupsById;
            _groupsByName = groupsByName;
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
            Dictionary<long, C4Lifecycle> lifecyclesById;
            Dictionary<string, C4Lifecycle> lifecyclesByName;
            Dictionary<long, C4LifecycleState> lifecycleStatesById;
            Dictionary<string, C4LifecycleState> lifecycleStatesByName;

            lifecyclesById = _c4s.ListLifecycles();
            lifecyclesByName = new Dictionary<string, C4Lifecycle>();
            lifecycleStatesById = new Dictionary<long, C4LifecycleState>();
            lifecycleStatesByName = new Dictionary<string, C4LifecycleState>();
            foreach (C4Lifecycle lc in lifecyclesById.Values) if (!lifecyclesByName.ContainsKey(lc.Name))
                {
                    lc.LocalizedName = GetLocalizedLabel(lc.Name, "lifecycle");
                    lifecyclesByName.Add(lc.Name, lc);
                    foreach (C4LifecycleState lcs in lc.States.Values) if (!lifecycleStatesByName.ContainsKey(lcs.Name))
                        {
                            lcs.LocalizedName = GetLocalizedLabel(lcs.Name, "lifecycle_state");
                            lifecycleStatesById.Add((long)lcs.Id, lcs);
                            lifecycleStatesByName.Add(lcs.Name, lcs);
                        }
                }

            _lifecyclesById = lifecyclesById;
            _lifecyclesByName = lifecyclesByName;
            _lifecycleStatesById = lifecycleStatesById;
            _lifecycleStatesByName = lifecycleStatesByName;
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
            Dictionary<long, C4Acl> aclsById;
            Dictionary<string, C4Acl> aclsByName;

            aclsById = _c4s.ListAcls();
            aclsByName = new Dictionary<string, C4Acl>();
            foreach (C4Acl a in aclsById.Values) if (!aclsByName.ContainsKey(a.Name))
                {
                    a.LocalizedName = GetLocalizedLabel(a.Name, "acl");
                    aclsByName.Add(a.Name, a);
                }

            _aclsById = aclsById;
            _aclsByName = aclsByName;
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
            Dictionary<long, C4RelationType> relTypesById;
            Dictionary<string, C4RelationType> relTypesByName;

            relTypesById = _c4s.ListRelationTypes();
            relTypesByName = new Dictionary<string, C4RelationType>();
            foreach (C4RelationType rt in relTypesById.Values) if (!relTypesByName.ContainsKey(rt.Name))
                {
                    rt.LocalizedName = GetLocalizedLabel(rt.Name, "relation_type");
                    relTypesByName.Add(rt.Name, rt);
                }

            _relTypesById = relTypesById;
            _relTypesByName = relTypesByName;
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
            Dictionary<long, C4ObjectType> objectTypesById;
            Dictionary<string, C4ObjectType> objectTypesByName;

            objectTypesById = _c4s.ListObjectTypes();
            objectTypesByName = new Dictionary<string, C4ObjectType>();
            foreach (C4ObjectType ot in objectTypesById.Values) if (!objectTypesByName.ContainsKey(ot.Name))
                {
                    ot.LocalizedName = GetLocalizedLabel(ot.Name, "object_type");
                    objectTypesByName.Add(ot.Name, ot);
                }

            _objectTypesById = objectTypesById;
            _objectTypesByName = objectTypesByName;
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
            Dictionary<long, C4FolderType> folderTypesById;
            Dictionary<string, C4FolderType> folderTypesByName;

            folderTypesById = _c4s.ListFolderTypes();
            folderTypesByName = new Dictionary<string, C4FolderType>();
            foreach (C4FolderType ft in folderTypesById.Values) if (!folderTypesByName.ContainsKey(ft.Name))
                {
                    ft.LocalizedName = GetLocalizedLabel(ft.Name, "folder_type");
                    folderTypesByName.Add(ft.Name, ft);
                }

            _folderTypesById = folderTypesById;
            _folderTypesByName = folderTypesByName;
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
            Dictionary<long, C4MetasetType> metasetTypesById;
            Dictionary<string, C4MetasetType> metasetTypesByName;

            metasetTypesById = _c4s.ListMetasetTypes();
            metasetTypesByName = new Dictionary<string, C4MetasetType>();
            foreach (C4MetasetType mst in metasetTypesById.Values) if (!metasetTypesByName.ContainsKey(mst.Name)) metasetTypesByName.Add(mst.Name, mst);

            _metasetTypesById = metasetTypesById;
            _metasetTypesByName = metasetTypesByName;
        }




        // *** Index items ***
        public Dictionary<string, C4IndexItem> IndexItemsByName
        {
            get
            {
                if (_indexItemsByName == null)
                {
                    Dictionary<string, C4IndexItem> indexItemsByName = _c4s.ListIndexItems(true);
                    foreach (C4IndexItem ii in indexItemsByName.Values)
                    {
                        ii.LocalizedName = GetLocalizedLabel(ii.Name, "index_item");
                    }
                    _indexItemsByName = indexItemsByName;
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
            Dictionary<long, C4Language> languagesById;
            Dictionary<string, C4Language> languagesByName;

            languagesById = _c4s.ListLanguages();
            languagesByName = new Dictionary<string, C4Language>();
            foreach (C4Language l in languagesById.Values) if (!languagesByName.ContainsKey(l.Name))
                {
                    l.LocalizedName = GetLocalizedLabel(l.Name, "language");
                    languagesByName.Add(l.Name, l);
                }

            _languagesById = languagesById;
            _languagesByName = languagesByName;
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
            Dictionary<long, C4Language> uiLanguagesById;
            Dictionary<string, C4Language> uiLanguagesByName;

            uiLanguagesById = _c4s.ListUiLanguages();
            uiLanguagesByName = new Dictionary<string, C4Language>();
            foreach (C4Language l in uiLanguagesById.Values) if (!uiLanguagesByName.ContainsKey(l.Name))
                {
                    l.LocalizedName = GetLocalizedLabel(l.Name, "language");
                    uiLanguagesByName.Add(l.Name, l);
                }

            _uiLanguagesById = uiLanguagesById;
            _uiLanguagesByName = uiLanguagesByName;
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
            Dictionary<long, C4Format> formatsById;
            Dictionary<string, C4Format> formatsByName;
            Dictionary<string, C4Format> formatsByExt;

            formatsById = _c4s.ListFormats();
            formatsByName = new Dictionary<string, C4Format>();
            formatsByExt = new Dictionary<string, C4Format>();
            foreach (C4Format fmt in formatsById.Values) if (!formatsByName.ContainsKey(fmt.Name) && !formatsByExt.ContainsKey(fmt.Extension))
                {
                    fmt.LocalizedName = GetLocalizedLabel(fmt.Name, "format");
                    formatsByExt.Add(fmt.Extension, fmt);
                    formatsByName.Add(fmt.Name, fmt);
                }

            _formatsById = formatsById;
            _formatsByName = formatsByName;
            _formatsByExt = formatsByExt;
        }


        // *** notifications ***
        public C4Folder NotificationFolder
        {
            get
            {
                if (_notificationFolder == null)
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
                    Dictionary<string, XmlDocument> configEntries = new Dictionary<string, XmlDocument>();
                    foreach (C4ConfigEntry ce in cesById.Values)
                    {
                        configEntries.Add(ce.Name, ce.Config);
                    }
                    _configEntries = configEntries;
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

