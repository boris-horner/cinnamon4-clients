using C4Admin.Interfaces;
using C4ObjectApi.Repository;
using C4ServerConnector;
using CDCplusLib.Common;
using CDCplusLib.EventData;
using CDCplusLib.Interfaces;

namespace C4Admin.GUI
{
    public partial class C4AMainWindow : Form, ISessionWindow
    {
        private C4Session _c4s;
        private string _guid;

        public string Guid
        {
            get
            {
                if (_guid == null) _guid = System.Guid.NewGuid().ToString();
                return _guid;
            }
        }

        public event WindowClosedEventHandler WindowClosed;
        public event SessionWindowRequestEventHandler SessionWindowRequest;
        public event TreeSelectionChangedEventHandler TreeSelectionChanged;
        public event ContextMenuRequestEventHandler ContextMenuRequest;
        public event ListSelectionChangedEventHandler ListSelectionChanged;
        public event FunctionRequestEventHandler FunctionRequest;
        public event NodesModifiedEventHandler NodesModified;
        public event KeyPressedEventHandler KeyPressedEvent;
        public event RefreshRequestEventHandler RefreshRequest;
        public string WindowTitle
        {
            get
            {
                return Text;
            }
        }

        public C4AMainWindow()
        {
            InitializeComponent();
        }

        private void cboServers_SelectedIndexChanged(object sender, EventArgs e)
        {
            GuiActions();
        }

        private void GuiActions()
        {
            bool connected = _c4s!=null;
            tbAdmin.Visible = connected;
        }

        private void tbAdmin_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_c4s != null) InitSelectedTab();
        }
        private void InitSelectedTab()
        {
            if (tbAdmin.SelectedTab != null)
            {
                ((ITabView)tbAdmin.SelectedTab.Controls[0]).Init(_c4s);
            }
        }

        public void ShowSessionWindow(CmnSession s, GlobalApplicationData globalAppData, WindowSelectionData wrd = null)
        {
            _c4s = s.CommandSession;
            Show();
            if (_c4s != null) InitSelectedTab();
        }

        public void CloseWindow()
        {
            Close();
        }

        public void WindowTop()
        {
            if (WindowState == FormWindowState.Minimized) WindowState = FormWindowState.Normal;
            BringToFront();
        }

        private void C4AMainWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            WindowClosed?.Invoke(this);
        }
    }
}
