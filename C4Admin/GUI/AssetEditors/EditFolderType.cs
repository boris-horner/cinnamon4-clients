using C4ServerConnector.Assets;
using C4Admin.Global;

namespace C4Admin.GUI.AssetEditors
{
    public partial class EditFolderType : Form
    {
        public C4FolderType FolderType { get; private set; }
        public bool SaveAs { get; private set; }
        private string _origName;
        public EditFolderType(string title, C4FolderType folderType, bool showSaveAs)
        {
            InitializeComponent();
            Text = title;
            FolderType = folderType;
            if (folderType == null)
            {
                txtId.Text = Definitions.NEW;
                vtxtName.Text = "";
                _origName = null;
            }
            else
            {
                txtId.Text = FolderType.Id == null ? Definitions.NEW : FolderType.Id.ToString();
                vtxtName.Text = FolderType.Name;
                _origName = FolderType.Name;
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
            FolderType = new C4FolderType(vtxtName.Text, id);
            Close();
        }

        private void cmdSaveAs_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            SaveAs = true;
            long? id = null;
            if (txtId.Text != Definitions.NEW) id = long.Parse(txtId.Text);
            FolderType = new C4FolderType(vtxtName.Text, id);
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

        private void EditFolderType_Load(object sender, EventArgs e)
        {
            ActiveControl = vtxtName;
        }
    }
}
