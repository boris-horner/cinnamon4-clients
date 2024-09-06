using C4ServerConnector.Assets;
using C4Admin.Global;

namespace C4Admin.GUI.AssetEditors
{
    public partial class EditObjectType : Form
    {
        public C4ObjectType ObjectType { get; private set; }
        public bool SaveAs { get; private set; }
        private string _origName;
        public EditObjectType(string title, C4ObjectType objectType, bool showSaveAs)
        {
            InitializeComponent();
            Text = title;
            ObjectType = objectType;
            if (objectType == null)
            {
                txtId.Text = Definitions.NEW;
                vtxtName.Text = "";
                _origName = null;
            }
            else
            {
                txtId.Text = ObjectType.Id == null ? Definitions.NEW : ObjectType.Id.ToString();
                vtxtName.Text = ObjectType.Name;
                _origName = ObjectType.Name;
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
            ObjectType = new C4ObjectType(vtxtName.Text, id);
            Close();
        }

        private void cmdSaveAs_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            SaveAs = true;
            long? id = null;
            if (txtId.Text != Definitions.NEW) id = long.Parse(txtId.Text);
            ObjectType = new C4ObjectType(vtxtName.Text, id);
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

        private void EditObjectType_Load(object sender, EventArgs e)
        {
            ActiveControl = vtxtName;
        }
    }
}
