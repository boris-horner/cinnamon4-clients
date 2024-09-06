using C4ServerConnector.Assets;
using C4Admin.Global;

namespace C4Admin.GUI.AssetEditors
{
    public partial class EditMetasetType : Form
    {
        public C4MetasetType MetasetType { get; private set; }
        public bool SaveAs { get; private set; }
        private string _origName;
        public EditMetasetType(string title, C4MetasetType metasetType, bool showSaveAs)
        {
            InitializeComponent();
            Text = title;
            MetasetType = metasetType;
            if (metasetType == null)
            {
                txtId.Text = Definitions.NEW;
                vtxtName.Text = "";
                chkUnique.Checked = false;
                _origName = null;
            }
            else
            {
                txtId.Text = MetasetType.Id == null ? Definitions.NEW : MetasetType.Id.ToString();
                vtxtName.Text = MetasetType.Name;
                chkUnique.Checked = MetasetType.Unique;
                _origName = MetasetType.Name;
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
            MetasetType = new C4MetasetType(vtxtName.Text, chkUnique.Checked, id);
            Close();
        }

        private void cmdSaveAs_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            SaveAs = true;
            long? id = null;
            if (txtId.Text != Definitions.NEW) id = long.Parse(txtId.Text);
            MetasetType = new C4MetasetType(vtxtName.Text, chkUnique.Checked, id);
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

        private void EditMetasetType_Load(object sender, EventArgs e)
        {
            ActiveControl = vtxtName;
        }
    }
}
