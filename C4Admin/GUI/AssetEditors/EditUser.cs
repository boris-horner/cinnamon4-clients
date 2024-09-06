using C4ServerConnector.Assets;
using C4Admin.Global;
using System.Xml;
using C4ServerConnector;

namespace C4Admin.GUI.AssetEditors
{
    public partial class EditUser : Form
    {
        public C4User User { get; private set; }
        public bool SaveAs { get; private set; }
        private string _origName;
        private C4Session _s;
        public EditUser(string title, C4User user, Dictionary<long, C4Group> groups, Dictionary<long, C4Language> uiLanguages, bool showSaveAs, C4Session s)
        {
            InitializeComponent();
            _s = s;
            Text = title;
            User = user;
            cboUiLanguage.Items.Clear();
            foreach (C4Language l in uiLanguages.Values) cboUiLanguage.Items.Add(l);
            clbGroups.Items.Clear();
            foreach (C4Group g in groups.Values)
            {
                clbGroups.Items.Add(g);
                if (g.Name == "_everyone") clbGroups.SetItemChecked(clbGroups.Items.IndexOf(g), true);
            }
            cboLoginType.Items.Clear(); // softcode?
            cboLoginType.Items.Add("CINNAMON");
            cboLoginType.Items.Add("LDAP");
            cboLoginType.Items.Add("AZURE");
            if (user == null)
            {
                txtId.Text = Definitions.NEW;
                vtxtName.Text = "";
                vtxtFullName.Text = "";
                cboLoginType.SelectedItem = "CINNAMON";
                cboUiLanguage.SelectedIndex = 0;
                chkActive.Checked = true;
                chkChangeTriggers.Checked = true;
                chkChangeTracking.Checked = true;
                chkLocked.Checked = false;
                chkPasswordExpired.Checked = false;
                xtxtConfig.SetText("<config/>");
                vtxtEmail.Text = "";
                _origName = null;
            }
            else
            {
                txtId.Text = User.Id == null ? Definitions.NEW : User.Id.ToString();
                vtxtName.Text = User.Name;
                vtxtFullName.Text = User.FullName;
                cboLoginType.SelectedItem = User.LoginType;
                cboUiLanguage.SelectedItem = uiLanguages[User.UiLanguageId];
                foreach (C4Group g in groups.Values)
                {
                    if(g.Name == "_owner") clbGroups.SetItemChecked(clbGroups.Items.IndexOf(g), false);
                    else if (g.Name == "_everyone") clbGroups.SetItemChecked(clbGroups.Items.IndexOf(g), true);
                    else if (User.GroupIds.Contains((long)g.Id)) clbGroups.SetItemChecked(clbGroups.Items.IndexOf(g), true);
                }
                chkActive.Checked = User.Active;
                chkChangeTriggers.Checked = User.ActivateTriggers;
                chkChangeTracking.Checked = User.ChangeTracking;
                chkLocked.Checked = User.Locked;
                chkPasswordExpired.Checked = User.PasswordExpired;
                xtxtConfig.SetText(User.UserConfig.OuterXml);
                vtxtEmail.Text = User.Email;
                _origName = null;
                _origName = User.Name;
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
            XmlDocument userConfig = new XmlDocument();
            userConfig.LoadXml(xtxtConfig.Text);
            HashSet<long> groupIds = new HashSet<long>();
            foreach (C4Group g in clbGroups.CheckedItems) groupIds.Add((long)g.Id);
            User = new C4User(vtxtName.Text,
                              vtxtPassword.Text,
                              id,
                              vtxtFullName.Text,
                              (long)((C4Language)cboUiLanguage.SelectedItem).Id,
                              cboLoginType.Text,
                              vtxtEmail.Text,
                              chkActive.Checked,
                              chkChangeTriggers.Checked,
                              chkLocked.Checked,
                              chkChangeTracking.Checked,
                              chkPasswordExpired.Checked,
                              groupIds,
                              userConfig,
                              _s);
            Close();
        }

        private void cmdSaveAs_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            SaveAs = true;
            long? id = null;
            if (txtId.Text != Definitions.NEW) id = long.Parse(txtId.Text);
            XmlDocument userConfig = new XmlDocument();
            userConfig.LoadXml(xtxtConfig.Text);
            HashSet<long> groupIds = new HashSet<long>();
            foreach (C4Group g in clbGroups.CheckedItems) groupIds.Add((long)g.Id);
            User = new C4User(vtxtName.Text,
                              vtxtPassword.Text,
                              id,
                              vtxtFullName.Text,
                              (long)((C4Language)cboUiLanguage.SelectedItem).Id,
                              cboLoginType.Text,
                              vtxtEmail.Text,
                              chkActive.Checked,
                              chkChangeTriggers.Checked,
                              chkLocked.Checked,
                              chkChangeTracking.Checked,
                              chkPasswordExpired.Checked,
                              groupIds,
                              userConfig,
                              _s);
            Close();
        }
        private void cmdCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void GuiActions()
        {
            bool valid = vtxtName.IsValid && vtxtFullName.IsValid && vtxtPassword.IsValid;
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

        private void vtxtPassword_TextChanged(object sender, EventArgs e)
        {
            GuiActions();
        }

        private void vtxtFullName_TextChanged(object sender, EventArgs e)
        {
            GuiActions();
        }

        private void clbGroups_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            CheckedListBox clb = sender as CheckedListBox;
            if (clb != null)
            {
                if (clb.Items[e.Index].ToString() == "_owner" && e.NewValue == CheckState.Checked) e.NewValue = CheckState.Unchecked;
                else if (clb.Items[e.Index].ToString() == "_everyone" && e.NewValue != CheckState.Checked) e.NewValue = CheckState.Checked;
            }
        }
    }
}
