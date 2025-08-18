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
using CDCplusLib.Common;
using CDCplusLib.Interfaces;
using System.Xml;
using C4ObjectApi.Interfaces;
using C4ObjectApi.Repository;
using C4ServerConnector.Assets;
using System.Text;
using CDCplusLib.EventData;

namespace CDCplusLib.TabControls
{
    public partial class PermissionsTabControl : UserControl, IGenericControl
    {
        private CmnSession _s;
        private GlobalApplicationData _gad;
        private bool _initCompleted = false;
        private bool _dirty;
        private Dictionary<long, IRepositoryNode> _dict;

        public event SessionWindowRequestEventHandler SessionWindowRequest;
        public event ListSelectionChangedEventHandler ListSelectionChanged;
        public event TreeSelectionChangedEventHandler TreeSelectionChanged;
        public event ContextMenuRequestEventHandler ContextMenuRequest;
        public event FunctionRequestEventHandler FunctionRequest;
        public event NodesModifiedEventHandler NodesModified;
        public event KeyPressedEventHandler KeyPressedEvent;
        public event RefreshRequestEventHandler RefreshRequest;

        public PermissionsTabControl()
        {
            InitializeComponent();
            LocalizeGUI();
            InitCurrentPermissionsLv();
        }


        public bool HasSelection
        {
            get
            {
                return false;
            }
        }
        public Dictionary<long, IRepositoryNode> Selection
        {
            get
            {
                return null;
            }
            set
            {

            }
        }
        public bool AutoRefresh
        {
            get
            {
                return true;
            }
        }
        public bool ListContext
        {
            get
            {
                return true;
            }
        }
        private void LocalizeGUI()
        {
            gbAcl.Text = Properties.Resources.lblPermissions;
            lblAcl.Text = Properties.Resources.lblAclName;
            lblAclEntries.Text = Properties.Resources.lblAclEntries;
            lblCurrentPermissions.Text = Properties.Resources.lblCurrentPermissions;
            colName.Text = Properties.Resources.lblName;
            colType.Text = Properties.Resources.lblType;
            colPermissions.Text = Properties.Resources.lblPermissions;
            colMyPermissions.Text = Properties.Resources.lblPermissions;
            ttPermissions.SetToolTip(cmdSave, Properties.Resources.ttSave);
        }
        private void InitCurrentPermissionsLv()
        {
            lvwCurrentPermissions.Items.Clear();
            lvwCurrentPermissions.Groups.Add("common", Properties.Resources.lblCommonPermissions);   
            lvwCurrentPermissions.Groups.Add("object", Properties.Resources.lblObjectPermissions);   
            lvwCurrentPermissions.Groups.Add("folder", Properties.Resources.lblFolderPermissions);
            lvwCurrentPermissions.Groups.Add("relation", Properties.Resources.lblRelationPermissions);
            lvwCurrentPermissions.Groups.Add("link", Properties.Resources.lblLinkPermissions);

            // common
            lvwCurrentPermissions.Items.Add("Node_Browse", Properties.Resources.permNodeBrowse, "inactive").Group = lvwCurrentPermissions.Groups["common"];
            lvwCurrentPermissions.Items.Add("Node_Acl_Write", Properties.Resources.permNodeAclWrite, "inactive").Group = lvwCurrentPermissions.Groups["common"];
            lvwCurrentPermissions.Items.Add("Node_Name_Write", Properties.Resources.permNodeNameWrite, "inactive").Group = lvwCurrentPermissions.Groups["common"];
            lvwCurrentPermissions.Items.Add("Node_Owner_Write", Properties.Resources.permNodeOwnerWrite, "inactive").Group = lvwCurrentPermissions.Groups["common"];
            lvwCurrentPermissions.Items.Add("Node_Parent_Folder_Write", Properties.Resources.permNodeMove, "inactive").Group = lvwCurrentPermissions.Groups["common"];
            lvwCurrentPermissions.Items.Add("Node_Type_Write", Properties.Resources.permNodeTypeWrite, "inactive").Group = lvwCurrentPermissions.Groups["common"];
            lvwCurrentPermissions.Items.Add("Node_Metadata_Read", Properties.Resources.permNodeMetadataRead, "inactive").Group = lvwCurrentPermissions.Groups["common"];
            lvwCurrentPermissions.Items.Add("Node_Metadata_Write", Properties.Resources.permNodeMetadataWrite, "inactive").Group = lvwCurrentPermissions.Groups["common"];
            lvwCurrentPermissions.Items.Add("Node_Summary_Write", Properties.Resources.permNodeSummaryWrite, "inactive").Group = lvwCurrentPermissions.Groups["common"];
            lvwCurrentPermissions.Items.Add("Node_Delete", Properties.Resources.permNodeDelete, "inactive").Group = lvwCurrentPermissions.Groups["common"];

            // object
            lvwCurrentPermissions.Items.Add("Object_Lock", Properties.Resources.permObjectLock, "inactive").Group = lvwCurrentPermissions.Groups["object"];
            lvwCurrentPermissions.Items.Add("Object_Content_Read", Properties.Resources.permObjectContentRead, "inactive").Group = lvwCurrentPermissions.Groups["object"];
            lvwCurrentPermissions.Items.Add("Object_Content_Write", Properties.Resources.permObjectContentWrite, "inactive").Group = lvwCurrentPermissions.Groups["object"];
            lvwCurrentPermissions.Items.Add("Object_Language_Write", Properties.Resources.permObjectLanguageWrite, "inactive").Group = lvwCurrentPermissions.Groups["object"];
            lvwCurrentPermissions.Items.Add("Object_LifecycleState_Write", Properties.Resources.permObjectLifecycleStateWrite, "inactive").Group = lvwCurrentPermissions.Groups["object"];
            lvwCurrentPermissions.Items.Add("Object_Version", Properties.Resources.permObjectVersion, "inactive").Group = lvwCurrentPermissions.Groups["object"];

            // folder
            lvwCurrentPermissions.Items.Add("Folder_Create_Object", Properties.Resources.permFolderCreateObject, "inactive").Group = lvwCurrentPermissions.Groups["folder"];
            lvwCurrentPermissions.Items.Add("Folder_Create_Folder", Properties.Resources.permFolderCreateFolder, "inactive").Group = lvwCurrentPermissions.Groups["folder"];

            // relation
            lvwCurrentPermissions.Items.Add("Relation_Child_Add", Properties.Resources.permRelationChildAdd, "inactive").Group = lvwCurrentPermissions.Groups["relation"];
            lvwCurrentPermissions.Items.Add("Relation_Child_Remove", Properties.Resources.permRelationChildRemove, "inactive").Group = lvwCurrentPermissions.Groups["relation"];
            lvwCurrentPermissions.Items.Add("Relation_Parent_Add", Properties.Resources.permRelationParentAdd, "inactive").Group = lvwCurrentPermissions.Groups["relation"];
            lvwCurrentPermissions.Items.Add("Relation_Parent_Remove", Properties.Resources.permRelationParentRemove, "inactive").Group = lvwCurrentPermissions.Groups["relation"];

            // link
            lvwCurrentPermissions.Items.Add("Link_Target_Write", Properties.Resources.permLinkTargetWrite, "inactive").Group = lvwCurrentPermissions.Groups["link"];
        }
        public void Reset(CmnSession s, GlobalApplicationData globalAppData, XmlElement configEl)
        {
            _gad = globalAppData;
            _s = s;
            cboAcl.Items.Clear();
            foreach (C4Acl acl in _s.SessionConfig.C4Sc.AclsById.Values) cboAcl.Items.Add(acl);
        }

        public string GetTabText()
        {
            return Properties.Resources.lblPermissions;
        }

        private void ActivateControls(bool dirty)
        {
            if (_initCompleted)
            {
                IRepositoryNode ow = _dict.First().Value;
                _dirty = dirty;
                bool writable = ow.Permissions.Node_Acl_Write && (ow.GetType()!=typeof(CmnObject) || ((CmnObject)ow).Locked==null);
                cmdSave.Enabled = writable & dirty;
                cboAcl.Enabled = writable;
            }
        }
        public void Init(Dictionary<long, IRepositoryNode> dict)
        {
            _initCompleted = false;
            _dict = dict;
            SetCurrentPermissions();
            _initCompleted = true;
            ActivateControls(false);
        }

        public bool IsValid(Dictionary<long, IRepositoryNode> dict, IGenericControl.ContextType ct)
        {
            return dict.Count == 1;
        }
        private void SetCurrentPermissionLabel(bool owner, bool superuser)
        {
            if (owner)
            {
                if (superuser)
                {
                    lblCurrentPermissions.Text = Properties.Resources.lblCurrentPermissions + " (" + Properties.Resources.lblSuperuser + ", " + Properties.Resources.lblOwner + ")";
                }
                else
                {
                    lblCurrentPermissions.Text = Properties.Resources.lblCurrentPermissions + " (" + Properties.Resources.lblOwner + ")";
                }
            }
            else if (superuser)
            {
                lblCurrentPermissions.Text = Properties.Resources.lblCurrentPermissions + " (" + Properties.Resources.lblSuperuser + ")";
            }
            else
            {
                lblCurrentPermissions.Text = Properties.Resources.lblCurrentPermissions;
            }
        }
        private void SetCurrentPermissions()
        {
            IRepositoryNode ow = _dict.First().Value;

            bool cboAclEnabled = cboAcl.Enabled;
            cboAcl.Enabled = true;
            //cboAcl.SelectedItem = ow.Acl;
            cboAcl.Text = ow.Acl.ToString();
            cboAcl.Enabled = cboAclEnabled;

            bool owner = _s.User == ow.Owner;
            bool superuser = _s.IsSuperuser;
            SetCurrentPermissionLabel(owner, superuser);

            // common
            lvwCurrentPermissions.Items["Node_Browse"].ImageKey = ow.Permissions.Node_Browse ? "active" : "inactive";
            lvwCurrentPermissions.Items["Node_Metadata_Read"].ImageKey = ow.Permissions.Node_Metadata_Read ? "active" : "inactive";
            lvwCurrentPermissions.Items["Node_Acl_Write"].ImageKey = ow.Permissions.Node_Acl_Write ? "active" : "inactive";
            lvwCurrentPermissions.Items["Node_Name_Write"].ImageKey = ow.Permissions.Node_Name_Write ? "active" : "inactive";
            lvwCurrentPermissions.Items["Node_Owner_Write"].ImageKey = ow.Permissions.Node_Owner_Write ? "active" : "inactive";
            lvwCurrentPermissions.Items["Node_Parent_Folder_Write"].ImageKey = ow.Permissions.Node_Parent_Folder_Write ? "active" : "inactive";
            lvwCurrentPermissions.Items["Node_Type_Write"].ImageKey = ow.Permissions.Node_Type_Write ? "active" : "inactive";
            lvwCurrentPermissions.Items["Node_Metadata_Write"].ImageKey = ow.Permissions.Node_Metadata_Write ? "active" : "inactive";
            lvwCurrentPermissions.Items["Node_Summary_Write"].ImageKey = ow.Permissions.Node_Summary_Write ? "active" : "inactive";
            lvwCurrentPermissions.Items["Node_Delete"].ImageKey = ow.Permissions.Node_Delete ? "active" : "inactive";

            // object
            lvwCurrentPermissions.Items["Object_LifecycleState_Write"].ImageKey = ow.Permissions.Object_LifecycleState_Write ? "active" : "inactive";
            lvwCurrentPermissions.Items["Object_Language_Write"].ImageKey = ow.Permissions.Object_Language_Write ? "active" : "inactive";
            lvwCurrentPermissions.Items["Object_Content_Read"].ImageKey = ow.Permissions.Object_Content_Read ? "active" : "inactive";
            lvwCurrentPermissions.Items["Object_Content_Write"].ImageKey = ow.Permissions.Object_Content_Write ? "active" : "inactive";
            lvwCurrentPermissions.Items["Object_Lock"].ImageKey = ow.Permissions.Object_Lock ? "active" : "inactive";
            lvwCurrentPermissions.Items["Object_Version"].ImageKey = ow.Permissions.Object_Version ? "active" : "inactive";

            // folder
            lvwCurrentPermissions.Items["Folder_Create_Folder"].ImageKey = ow.Permissions.Folder_Create_Folder ? "active" : "inactive";
            lvwCurrentPermissions.Items["Folder_Create_Object"].ImageKey = ow.Permissions.Folder_Create_Object ? "active" : "inactive";

            // object
            lvwCurrentPermissions.Items["Relation_Child_Add"].ImageKey = ow.Permissions.Relation_Child_Add ? "active" : "inactive";
            lvwCurrentPermissions.Items["Relation_Child_Remove"].ImageKey = ow.Permissions.Relation_Child_Remove ? "active" : "inactive";
            lvwCurrentPermissions.Items["Relation_Parent_Add"].ImageKey = ow.Permissions.Relation_Parent_Add ? "active" : "inactive";
            lvwCurrentPermissions.Items["Relation_Parent_Remove"].ImageKey = ow.Permissions.Relation_Parent_Remove ? "active" : "inactive";

			// link
			lvwCurrentPermissions.Items["Link_Target_Write"].ImageKey = ow.Permissions.Link_Target_Write ? "active" : "inactive";

        }

        private void AddString(StringBuilder sb, string text)
        {
            if (sb.Length>0) sb.Append(", ");
            sb.Append(text);
        }

        private string GetPermissionString(C4Permissions perm)
        {
            StringBuilder result = new StringBuilder();

            if (perm.Node_Browse) AddString(result, Properties.Resources.permNodeBrowse);
            if (perm.Node_Metadata_Read) AddString(result, Properties.Resources.permNodeMetadataRead);
            if (perm.Node_Acl_Write) AddString(result, Properties.Resources.permNodeAclWrite);
            if (perm.Node_Name_Write) AddString(result, Properties.Resources.permNodeNameWrite);
            if (perm.Node_Owner_Write) AddString(result, Properties.Resources.permNodeOwnerWrite);
            if (perm.Node_Parent_Folder_Write) AddString(result, Properties.Resources.permNodeMove);
            if (perm.Node_Type_Write) AddString(result, Properties.Resources.permNodeTypeWrite);
            if (perm.Node_Metadata_Write) AddString(result, Properties.Resources.permNodeMetadataWrite);
            if (perm.Node_Summary_Write) AddString(result, Properties.Resources.permNodeSummaryWrite);
            if (perm.Node_Delete) AddString(result, Properties.Resources.permNodeDelete);
            if (perm.Object_Language_Write) AddString(result, Properties.Resources.permObjectLanguageWrite);
            if (perm.Object_LifecycleState_Write) AddString(result, Properties.Resources.permObjectLifecycleStateWrite);
            if (perm.Object_Content_Read) AddString(result, Properties.Resources.permObjectContentRead);
            if (perm.Object_Content_Write) AddString(result, Properties.Resources.permObjectContentWrite);
            if (perm.Object_Lock) AddString(result, Properties.Resources.permObjectLock);
            if (perm.Object_Version) AddString(result, Properties.Resources.permObjectVersion);
            if (perm.Folder_Create_Folder) AddString(result, Properties.Resources.permFolderCreateFolder);
            if (perm.Folder_Create_Object) AddString(result, Properties.Resources.permFolderCreateObject);
            if (perm.Relation_Child_Add) AddString(result, Properties.Resources.permRelationChildAdd);
            if (perm.Relation_Child_Remove) AddString(result, Properties.Resources.permRelationChildRemove);
            if (perm.Relation_Parent_Add) AddString(result, Properties.Resources.permRelationParentAdd);
            if (perm.Relation_Parent_Remove) AddString(result, Properties.Resources.permRelationParentRemove);
            if (perm.Link_Target_Write) AddString(result, Properties.Resources.permLinkTargetWrite);




            return result.ToString();
        }

        private void cboAcl_SelectedIndexChanged(object sender, EventArgs e)
        {
            C4Acl selAcl = (C4Acl)cboAcl.SelectedItem;
            lvwAclEntries.Items.Clear();
            foreach(C4AclGroup aclGroup in selAcl.AclGroups.Values)
            {
                ListViewItem li = lvwAclEntries.Items.Add(_s.SessionConfig.C4Sc.GroupsById[aclGroup.GroupId].LocalizedName);
                li.Tag = aclGroup;
                li.SubItems.Add(Properties.Resources.lblGroup);     // TODO: obsolete
                li.SubItems.Add(GetPermissionString(aclGroup.Permissions));     // Permissions
            }
            ActivateControls(true);
        }
        private void cmdSave_Click(object sender, EventArgs e)
        {
            Save();
        }
        public bool IsDirty
        {
            get
            {
                return _dirty;
            }
        }

        public void Save()
        {
            if(cboAcl.SelectedItem!=null)
            {
                IRepositoryNode ow = _dict.First().Value;
                if(ow.GetType()==typeof(CmnObject))
                {
                    // object
                    CmnObject o = (CmnObject)ow;
                    o.Lock();
                    o.Session.CommandSession.UpdateObject(o.Id,
														null,
														null,
														null,
														((C4Acl)cboAcl.SelectedItem).Id);
                    o.Unlock();
                }
                else
                {
                    // folder
                    CmnFolder f = (CmnFolder)ow;
                    _s.CommandSession.UpdateFolder(f.Id, null, null, null, null, ((C4Acl)cboAcl.SelectedItem).Id);
                }
                ActivateControls(false);
                WindowSelectionData wsd = new WindowSelectionData();
                wsd.Selection.Add(ow.Id, ow);
                wsd.Modification.Add(ow.Id, ow);
                NodesModified?.Invoke(wsd);
            }
        }

        public void ReInit()
        {
            Init(_dict);
        }
    }
}
