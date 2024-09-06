using C4ServerConnector.Assets;
using C4Admin.Global;

namespace C4Admin.GUI.AssetEditors
{
    public partial class EditRelationType : Form
    {
        public C4RelationType RelationType { get; private set; }
        public bool SaveAs { get; private set; }
        private string _origName;
        public EditRelationType(string title, C4RelationType relationType, bool showSaveAs)
        {
            InitializeComponent();
            Text = title;
            RelationType = relationType;
            if (relationType == null)
            {
                txtId.Text = Definitions.NEW;
                vtxtName.Text = "";
                chkLeftObjectProtected.Checked = false;
                chkRightObjectProtected.Checked = false;
                chkCloneLeftCopy.Checked = false;
                chkCloneRightCopy.Checked = false;
                chkCloneLeftVersion.Checked = false;
                chkCloneRightVersion.Checked = false;
                _origName = null;
            }
            else
            {
                txtId.Text = RelationType.Id == null ? Definitions.NEW : RelationType.Id.ToString();
                vtxtName.Text = RelationType.Name;
                chkLeftObjectProtected.Checked = relationType.LeftObjectProtected;
                chkRightObjectProtected.Checked = relationType.RightObjectProtected;
                chkCloneLeftCopy.Checked = relationType.CloneOnLeftCopy;
                chkCloneRightCopy.Checked = relationType.CloneOnRightCopy;
                chkCloneLeftVersion.Checked = relationType.CloneOnLeftVersion;
                chkCloneRightVersion.Checked = relationType.CloneOnRightVersion;
                _origName = RelationType.Name;
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
            RelationType = new C4RelationType(vtxtName.Text,chkLeftObjectProtected.Checked, chkRightObjectProtected.Checked, chkCloneLeftCopy.Checked, chkCloneRightCopy.Checked, chkCloneLeftVersion.Checked, chkCloneRightVersion.Checked, id);
            Close();
        }

        private void cmdSaveAs_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            SaveAs = true;
            long? id = null;
            if (txtId.Text != Definitions.NEW) id = long.Parse(txtId.Text);
            RelationType = new C4RelationType(vtxtName.Text, chkLeftObjectProtected.Checked, chkRightObjectProtected.Checked, chkCloneLeftCopy.Checked, chkCloneRightCopy.Checked, chkCloneLeftVersion.Checked, chkCloneRightVersion.Checked, id);
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

        private void EditRelationType_Load(object sender, EventArgs e)
        {
            ActiveControl = vtxtName;
        }
    }
}
