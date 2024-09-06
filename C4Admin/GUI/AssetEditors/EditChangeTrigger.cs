using C4ServerConnector.Assets;
using C4Admin.Global;
using System.Xml;

namespace C4Admin.GUI.AssetEditors
{
    public partial class EditChangeTrigger : Form
    {
        public C4ChangeTrigger ChangeTrigger { get; private set; }
        public bool SaveAs { get; private set; }
        private string _origName;
        public EditChangeTrigger(string title, C4ChangeTrigger changeTrigger, bool showSaveAs)
        {
            InitializeComponent();
            Text = title;
            ChangeTrigger = changeTrigger;
            if (changeTrigger == null)
            {
                txtId.Text = Definitions.NEW;
                vtxtName.Text = "";
                vtxtTriggerType.Text = "MICROSERVICE";
                vtxtController.Text = "";
                vtxtAction.Text = "";
                chkActive.Checked = false;
                chkPreTrigger.Checked = false;
                chkPostTrigger.Checked = false;
                chkPostCommitTrigger.Checked = false;
                chkCopyFileContent.Checked = false;
                vtxtRanking.Text = "1";
                xtxtConfig.SetText("<config/>");
                _origName = null;
            }
            else
            {
                txtId.Text = ChangeTrigger.Id == null ? Definitions.NEW : ChangeTrigger.Id.ToString();
                vtxtName.Text = ChangeTrigger.Name;
                vtxtTriggerType.Text = ChangeTrigger.TriggerType;
                vtxtController.Text = ChangeTrigger.Controller;
                vtxtAction.Text = ChangeTrigger.Action;
                chkActive.Checked = ChangeTrigger.Active;
                chkPreTrigger.Checked = ChangeTrigger.PreTrigger;
                chkPostTrigger.Checked = ChangeTrigger.PostTrigger;
                chkPostCommitTrigger.Checked = ChangeTrigger.PostCommitTrigger;
                chkCopyFileContent.Checked = ChangeTrigger.CopyFileContent;
                vtxtRanking.Text = ChangeTrigger.Ranking.ToString();
                xtxtConfig.SetText(ChangeTrigger.Config.OuterXml);
                _origName = ChangeTrigger.Name;
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

            XmlDocument config = new XmlDocument();
            config.LoadXml(xtxtConfig.Text);
            ChangeTrigger = new C4ChangeTrigger(vtxtName.Text,
                                                vtxtTriggerType.Text,
                                                chkActive.Checked,
                                                chkPreTrigger.Checked,
                                                chkPostTrigger.Checked,
                                                chkPostCommitTrigger.Checked,
                                                chkCopyFileContent.Checked,
                                                long.Parse(vtxtRanking.Text),
                                                vtxtController.Text,
                                                vtxtAction.Text,
                                                config,
                                                id);
            Close();
        }

        private void cmdSaveAs_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            SaveAs = true;
            long? id = null;
            if (txtId.Text != Definitions.NEW) id = long.Parse(txtId.Text);

            XmlDocument config = new XmlDocument();
            config.LoadXml(xtxtConfig.Text);
            ChangeTrigger = new C4ChangeTrigger(vtxtName.Text,
                                                vtxtTriggerType.Text,
                                                chkActive.Checked,
                                                chkPreTrigger.Checked,
                                                chkPostTrigger.Checked,
                                                chkPostCommitTrigger.Checked,
                                                chkCopyFileContent.Checked,
                                                long.Parse(vtxtRanking.Text),
                                                vtxtController.Text,
                                                vtxtAction.Text,
                                                config,
                                                id);
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

        private void EditChangeTrigger_Load(object sender, EventArgs e)
        {
            ActiveControl = vtxtName;
        }
    }
}
