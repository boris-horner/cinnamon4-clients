using C4Admin.Global;
using C4ServerConnector;
using C4ServerConnector.Assets;

namespace C4Admin.GUI.AssetEditors
{
    public partial class EditAclGroup : Form
    {
        private C4Session _s;
        private C4AllPermissions _allPermissions;
        public C4AclGroup AclGroup { get; private set; }
        public bool SaveAs { get; private set; }
        private Dictionary<long, C4Group> _groups;
        private Dictionary<long, C4Acl> _acls;
        public EditAclGroup(string title, C4Session s, C4AclGroup aclGroup, bool showSaveAs, long? aclId, long? groupId)
        {
            InitializeComponent();
            Text = title;
            AclGroup = aclGroup;
            _s = s;
            _allPermissions = _s.ListPermissions();
            _groups = s.ListGroups();
            _acls = s.ListAcls();
            InitLists();
            if (AclGroup == null)
            {
                txtId.Text = Definitions.NEW;
                cboAcl.Text = "";
                cboGroup.Text = "";
                foreach (PermissionEntry item in clbPermissions.Items)
                {
                    clbPermissions.SetItemChecked(clbPermissions.Items.IndexOf(item), false);
                }
            }
            else
            {
                txtId.Text = AclGroup.Id == null ? Definitions.NEW : AclGroup.Id.ToString();
                cboAcl.Text = _acls[AclGroup.AclId].ToString();
                cboGroup.Text = _groups[AclGroup.GroupId].ToString();
                Dictionary<int, bool> checkStateByIndex = new Dictionary<int, bool>();
                foreach (PermissionEntry item in clbPermissions.Items)
                {
                    checkStateByIndex.Add(clbPermissions.Items.IndexOf(item), AclGroup.Permissions.GetPermission(item.Name));
                }
                foreach (int index in checkStateByIndex.Keys)
                {
                    clbPermissions.SetItemChecked(index, checkStateByIndex[index]);
                }
            }

            cboAcl.Enabled = aclId == null;
            if (aclId != null) cboAcl.SelectedItem = _acls[(long)aclId];
            cboGroup.Enabled = groupId == null;
            if (groupId != null) cboGroup.SelectedItem = _groups[(long)groupId];

            GuiActions();
            pSaveAs.Visible = showSaveAs;
        }

        private void InitLists()
        {
            clbPermissions.Items.Clear();

            // common
            clbPermissions.Items.Add(GetPermissionEntry("node.browse"));
            clbPermissions.Items.Add(GetPermissionEntry("node.acl.write"));
            clbPermissions.Items.Add(GetPermissionEntry("node.name.write"));
            clbPermissions.Items.Add(GetPermissionEntry("node.owner.write"));
            clbPermissions.Items.Add(GetPermissionEntry("node.parent_folder.write"));
            clbPermissions.Items.Add(GetPermissionEntry("node.type.write"));
            clbPermissions.Items.Add(GetPermissionEntry("node.sysmetadata.read"));      // obsolete?
            clbPermissions.Items.Add(GetPermissionEntry("node.metadata.read"));
            clbPermissions.Items.Add(GetPermissionEntry("node.metadata.write"));
            clbPermissions.Items.Add(GetPermissionEntry("node.summary.write"));
            clbPermissions.Items.Add(GetPermissionEntry("node.delete"));

            // object
            clbPermissions.Items.Add(GetPermissionEntry("object.lock"));
            clbPermissions.Items.Add(GetPermissionEntry("object.content.read"));
            clbPermissions.Items.Add(GetPermissionEntry("object.content.write"));
            clbPermissions.Items.Add(GetPermissionEntry("object.language.write"));
            clbPermissions.Items.Add(GetPermissionEntry("object.lifecyclestate.write"));
            clbPermissions.Items.Add(GetPermissionEntry("object.version"));

            // folder
            clbPermissions.Items.Add(GetPermissionEntry("folder.create.object"));
            clbPermissions.Items.Add(GetPermissionEntry("folder.create.folder"));

            // relation
            clbPermissions.Items.Add(GetPermissionEntry("relation.child.add"));
            clbPermissions.Items.Add(GetPermissionEntry("relation.child.remove"));
            clbPermissions.Items.Add(GetPermissionEntry("relation.parent.add"));
            clbPermissions.Items.Add(GetPermissionEntry("relation.parent.remove"));

            // link
            clbPermissions.Items.Add(GetPermissionEntry("link.target.write"));

            cboAcl.Items.Clear();
            foreach (C4Acl acl in _acls.Values)
            {
                cboAcl.Items.Add(acl);
            }

            cboGroup.Items.Clear();
            foreach (C4Group group in _groups.Values)
            {
                cboGroup.Items.Add(group);
            }
        }
        private PermissionEntry GetPermissionEntry(string name)
        {
            return new PermissionEntry(_allPermissions.PermissionIdsByName[name], name);
        }

        private void cmdOk_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            SaveAs = false;
            long? id = null;
            if (txtId.Text != Definitions.NEW) id = long.Parse(txtId.Text);
            AclGroup = new C4AclGroup((long)((C4Acl)cboAcl.SelectedItem).Id, (long)((C4Group)cboGroup.SelectedItem).Id, GetPermissions(), _allPermissions, id);
            Close();
        }

        private void cmdSaveAs_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            SaveAs = true;
            long? id = null;
            if (txtId.Text != Definitions.NEW) id = long.Parse(txtId.Text);
            AclGroup = new C4AclGroup((long)((C4Acl)cboAcl.SelectedItem).Id, (long)((C4Group)cboGroup.SelectedItem).Id, GetPermissions(), _allPermissions, id);
            Close();
        }
        private C4Permissions GetPermissions()
        {
            C4Permissions permissions = new C4Permissions(false, _allPermissions);
            foreach (PermissionEntry item in clbPermissions.CheckedItems)
            {
                switch (item.Name)
                {
                    case "folder.create.folder":
                        permissions.Folder_Create_Folder = true;
                        break;
                    case "folder.create.object":
                        permissions.Folder_Create_Object = true;
                        break;
                    case "link.target.write":
                        permissions.Link_Target_Write = true;
                        break;
                    case "node.acl.write":
                        permissions.Node_Acl_Write = true;
                        break;
                    case "node.browse":
                        permissions.Node_Browse = true;
                        break;
                    case "node.delete":
                        permissions.Node_Delete = true;
                        break;
                    case "node.metadata.read":
                        permissions.Node_Metadata_Read = true;
                        break;
                    case "node.metadata.write":
                        permissions.Node_Metadata_Write = true;
                        break;
                    case "node.name.write":
                        permissions.Node_Name_Write = true;
                        break;
                    case "node.owner.write":
                        permissions.Node_Owner_Write = true;
                        break;
                    case "node.parent_folder.write":
                        permissions.Node_Parent_Folder_Write = true;
                        break;
                    case "node.summary.write":
                        permissions.Node_Summary_Write = true;
                        break;
                    case "node.sysmetadata.read":
                        permissions.Node_SysMetadata_Read = true;
                        break;
                    case "node.type.write":
                        permissions.Node_Type_Write = true;
                        break;
                    case "object.content.read":
                        permissions.Object_Content_Read = true;
                        break;
                    case "object.content.write":
                        permissions.Object_Content_Write = true;
                        break;
                    case "object.language.write":
                        permissions.Object_Language_Write = true;
                        break;
                    case "object.lifecyclestate.write":
                        permissions.Object_LifecycleState_Write = true;
                        break;
                    case "object.lock":
                        permissions.Object_Lock = true;
                        break;
                    case "object.version":
                        permissions.Object_Version = true;
                        break;
                    case "relation.child.add":
                        permissions.Relation_Child_Add = true;
                        break;
                    case "relation.child.remove":
                        permissions.Relation_Child_Remove = true;
                        break;
                    case "relation.parent.add":
                        permissions.Relation_Parent_Add = true;
                        break;
                    case "relation.parent.remove":
                        permissions.Relation_Parent_Remove = true;
                        break;
                }
            }
            return permissions;
        }
        private void cmdCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void GuiActions()
        {
            bool valid = cboAcl.SelectedItem != null && cboGroup.SelectedItem != null;
            cmdOk.Enabled = valid;
            cmdSaveAs.Enabled = valid;
        }
        private void EditAclGroup_Load(object sender, EventArgs e)
        {
            ActiveControl = cboGroup;
        }

        private void cboAcl_SelectedIndexChanged(object sender, EventArgs e)
        {
            GuiActions();
        }

        private void cboGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            GuiActions();
        }

        private void clbPermissions_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            //long newCheckedCount = e.NewValue == CheckState.Checked ? clbPermissions.CheckedItems.Count + 1 : clbPermissions.CheckedItems.Count - 1;
            bool valid = cboAcl.SelectedItem != null && cboGroup.SelectedItem != null;
            cmdOk.Enabled = valid;
            cmdSaveAs.Enabled = valid;
        }
    }

    internal class PermissionEntry
    {
        public string Name { get; set; }
        public long Id { get; set; }
        public PermissionEntry(long id, string name)
        {
            Id = id;
            Name = name;
        }
        public override string ToString()
        {
            return Name;
        }
    }
}
