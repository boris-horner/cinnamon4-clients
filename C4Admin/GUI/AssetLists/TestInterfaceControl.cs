using C4Admin.Interfaces;
using C4ServerConnector;
using C4GeneralGui.GuiElements;
using System.Xml;

namespace C4Admin.GUI.AssetLists
{
    public partial class TestInterfaceControl : UserControl, ITabView
    {
        private C4Session _s;
        public TestInterfaceControl()
        {
            InitializeComponent();
        }
        public void Init(C4Session s)
        {
            _s = s;
        }
        private void cmdSendCommand_Click(object sender, EventArgs e)
        {
            try
            {
                XmlDocument bodyText = new XmlDocument();
                bodyText.LoadXml(txtBodyText.Text);

                StringWriter sw = new StringWriter();
                XmlDocument resp = _s.TestInterface(bodyText, txtCommandPath.Text);
                resp.Save(sw);
                
                txtResult.Text = sw.ToString();
            }
            catch(Exception ex)
            {
                StandardMessage.ShowMessage("Caught Exception", StandardMessage.Severity.ErrorMessage, this, ex);
            }
        }
    }
}
