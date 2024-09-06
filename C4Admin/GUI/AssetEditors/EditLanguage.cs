using C4ServerConnector.Assets;
using C4Admin.Global;

namespace C4Admin.GUI.AssetEditors
{
    public partial class EditLanguage : Form
    {
        public C4Language Language { get; private set; }
        public bool SaveAs { get; private set; }
        private string _origName;
        public EditLanguage(string title, C4Language language, bool showSaveAs)
        {
            InitializeComponent();
            Text = title;
            Language = language;
            if (Language == null)
            {
                txtId.Text = Definitions.NEW;
                vtxtName.Text = "";
                _origName = null;
            }
            else
            {
                txtId.Text = Language.Id == null ? Definitions.NEW : Language.Id.ToString();
                vtxtName.Text = Language.Name;
                _origName = Language.Name;
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
            Language = new C4Language(vtxtName.Text, id);
            Close();
        }

        private void cmdSaveAs_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            SaveAs = true;
            long? id = null;
            if (txtId.Text != Definitions.NEW) id = long.Parse(txtId.Text);
            Language = new C4Language(vtxtName.Text, id);
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

        private void EditLanguage_Load(object sender, EventArgs e)
        {
            ActiveControl = vtxtName;
        }
    }
}
