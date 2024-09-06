using C4ServerConnector.Assets;
using C4Admin.Global;

namespace C4Admin.GUI.AssetEditors
{
    public partial class EditGroup : Form
    {
        private const string NOPARENTGROUP = "[no parent group]";
        public C4Group Group { get; private set; }
        public bool SaveAs { get; private set; }
        private string _origName;
        public EditGroup(string title, C4Group group, Dictionary<long, C4Group> groups, bool showSaveAs)
        {
            InitializeComponent();
            Text = title;
            Group = group;
            cboParentGroup.Items.Clear();
            cboParentGroup.Items.Add(NOPARENTGROUP);
            foreach (C4Group g in groups.Values) cboParentGroup.Items.Add(g);
            if (group == null)
            {
                txtId.Text = Definitions.NEW;
                vtxtName.Text = "";
                cboParentGroup.SelectedItem = NOPARENTGROUP;
                _origName = null;
            }
            else
            {
                txtId.Text = Group.Id == null ? Definitions.NEW : Group.Id.ToString();
                vtxtName.Text = Group.Name;if(Group.ParentId==null) cboParentGroup.SelectedItem = NOPARENTGROUP;
                else cboParentGroup.SelectedItem = groups[(long)Group.ParentId];
                _origName = Group.Name;
            }


            GuiActions();
            pSaveAs.Visible = showSaveAs;
        }

        private void cmdOk_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            SaveAs = false;
            long? id = null;
            if (txtId.Text != Definitions.NEW) id = long.Parse(txtId.Text);
            long? parentId = null;
            if (cboParentGroup.SelectedItem.GetType() == typeof(C4Group)) parentId = ((C4Group)cboParentGroup.SelectedItem).Id;
            Group = new C4Group(vtxtName.Text, parentId, id);
            Close();
        }

        private void cmdSaveAs_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            SaveAs = true;
            long? id = null;
            if (txtId.Text != Definitions.NEW) id = long.Parse(txtId.Text);
            long? parentId = null;
            if (cboParentGroup.SelectedItem.GetType() == typeof(C4Group)) parentId = ((C4Group)cboParentGroup.SelectedItem).ParentId;
            Group = new C4Group(vtxtName.Text, parentId, id);
            Close();
        }
        private void cmdCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void GuiActions()
        {
            bool valid = vtxtName.IsValid;
            cmdOk.Enabled = valid;
            cmdSaveAs.Enabled = (vtxtName.Text != _origName && valid);
        }
        private void vtxtName_TextChanged(object sender, EventArgs e)
        {
            GuiActions();
        }

        private void EditGroup_Load(object sender, EventArgs e)
        {
            ActiveControl = vtxtName;
        }
    }
}
