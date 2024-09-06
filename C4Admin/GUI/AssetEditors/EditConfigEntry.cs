using C4ServerConnector.Assets;
using C4Admin.Global;
using C4GeneralGui.GuiElements;
using System.Xml;

namespace C4Admin.GUI.AssetEditors
{
    public partial class EditConfigEntry : Form
    {
        public C4ConfigEntry ConfigEntry { get; private set; }
        public bool SaveAs { get; private set; }
        private string _origName;
        public EditConfigEntry(string title, C4ConfigEntry configEntry, bool showSaveAs)
        {
            InitializeComponent();
            Text = title;
            ConfigEntry = configEntry;
            if (configEntry == null)
            {
                txtId.Text = Definitions.NEW;
                vtxtName.Text = "";
                _origName = null;
                xtxtConfig.SetText("<config/>");
                chkPublic.Checked = false;
            }
            else
            {
                txtId.Text = ConfigEntry.Id == null ? Definitions.NEW : ConfigEntry.Id.ToString();
                vtxtName.Text = ConfigEntry.Name;
                _origName = ConfigEntry.Name;
                xtxtConfig.SetText(ConfigEntry.Config.OuterXml);
                chkPublic.Checked = ConfigEntry.PubliclyVisible;
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
            XmlDocument xdoc = new XmlDocument();
            try
            {
                xdoc.LoadXml(xtxtConfig.Text);
                ConfigEntry = new C4ConfigEntry(vtxtName.Text, xdoc, chkPublic.Checked, id);
                Close();
            }
            catch
            {
                StandardMessage.ShowMessage("The XML code is not well-formed.", StandardMessage.Severity.ErrorMessage, this, null, null);
            }
        }

        private void cmdSaveAs_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            SaveAs = true;
            long? id = null;
            if (txtId.Text != Definitions.NEW) id = long.Parse(txtId.Text);
            XmlDocument xdoc = new XmlDocument();
            try
            {
                xdoc.LoadXml(xtxtConfig.Text);
                ConfigEntry = new C4ConfigEntry(vtxtName.Text, xdoc, chkPublic.Checked, id);
                Close();
            }
            catch
            {
                StandardMessage.ShowMessage("The XML code is not well-formed.", StandardMessage.Severity.ErrorMessage, this, null, null);
            }
        }
        private void cmdCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void GuiActions()
        {
            bool valid = vtxtName.IsValid && xtxtConfig.Text.Trim().Length > 0;
            cmdOk.Enabled = valid;
            cmdSaveAs.Enabled = (vtxtName.Text != _origName && valid);
        }
        private void vtxtName_TextChanged(object sender, EventArgs e)
        {
            GuiActions();
        }

        private void EditConfigEntry_Load(object sender, EventArgs e)
        {
            ActiveControl = vtxtName;
        }

        private void xtxtConfig_TextChanged(object sender, EventArgs e)
        {
            GuiActions();
        }
    }
}
