using C4ServerConnector.Assets;
using C4Admin.Interfaces;
using C4GeneralGui.Helpers;
using C4ServerConnector;
using C4GeneralGui.GuiElements;
using System.Xml;
using C4Admin.GUI.AssetEditors;
using CDCplusLib.Common;

namespace C4Admin.GUI.AssetLists
{
    public partial class UsersControl : UserControl, ITabView
    {
        private ListViewSort _lvwSort;
        private C4Session _s;
        Dictionary<long, C4User> _users;
        long _adminUserId;
        public UsersControl()
        {
            InitializeComponent();
        }
        public void Init(C4Session s)
        {
            _s = s;
            _users = _s.ListUsers();
            foreach (C4User u in _users.Values)
            {
                if (u.Name == "admin")
                {
                    _adminUserId = (long)u.Id;
                    break;
                }
            }
            lvw.Items.Clear();
            foreach (C4User a in _users.Values)
            {
                ListViewItem lvi = lvw.Items.Add(a.Id.ToString());
                lvi.SubItems.Add(a.Name);
                lvi.SubItems.Add(a.FullName);
                lvi.SubItems.Add(a.UiLanguageId.ToString());
                lvi.SubItems.Add(a.LoginType);
                lvi.SubItems.Add(a.Email);
                lvi.SubItems.Add(a.Active ? "X" : "");
                lvi.SubItems.Add(a.ActivateTriggers ? "X" : "");
                lvi.SubItems.Add(a.ChangeTracking ? "X" : "");
                lvi.SubItems.Add(a.Locked ? "X" : "");
                lvi.SubItems.Add(a.PasswordExpired ? "X" : "");
                lvi.Tag = a;
            }
            if (_lvwSort == null) _lvwSort = new ListViewSort(lvw);
            _lvwSort.Sort(_lvwSort.LastColumn,_lvwSort.ColumnSortOrder);
        }

        private void lvw_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                ContextMenuStrip cm = new ContextMenuStrip();
                ToolStripItem tsi = null;
                if (lvw.SelectedItems.Count == 0)
                {
                    tsi = GuiHelper.AppendToolStripItem(ref cm, lvw, "Add user", "add", true);
                    tsi.Click += HandleContextMenuClick;
                }
                if (lvw.SelectedItems.Count == 1)
                {
                    tsi = GuiHelper.AppendToolStripItem(ref cm, lvw, "Edit user", "edit", true);
                    tsi.Click += HandleContextMenuClick;
                }
                if (lvw.SelectedItems.Count == 1)
                {
                    tsi = GuiHelper.AppendToolStripItem(ref cm, lvw, "Delete user", "delete", true);
                    tsi.Click += HandleContextMenuClick;
                }

                cm.Show(lvw, e.Location);
            }
        }
        private void HandleContextMenuClick(object sender, System.EventArgs e)
        {
            ToolStripMenuItem tsmi = (ToolStripMenuItem)sender;
            try
            {
                switch (tsmi.Name)
                {
                    case "add":
                        {
                            Dictionary<long, C4Group> groups = _s.ListGroups();
                            Dictionary<long, C4Language> uiLanguages = _s.ListUiLanguages();
                            EditUser ea = new EditUser("New user", null, groups, uiLanguages, false, _s);
                            ea.StartPosition = FormStartPosition.Manual;
                            ea.Location = MousePosition;
                            if (ea.ShowDialog(this) == DialogResult.OK)
                            {
                                HashSet<C4User> users = new HashSet<C4User>();
                                users.Add(ea.User);
                                XmlDocument result = _s.CreateUsers(users);
                                Init(_s);
                            }
                            break;
                        }
                    case "edit":
                        {
                            Dictionary<long, C4Group> groups = _s.ListGroups();
                            Dictionary<long, C4Language> uiLanguages = _s.ListUiLanguages();
                            ListViewItem lvi = lvw.SelectedItems[0];
                            EditUser ea = new EditUser("Edit user", (C4User)lvi.Tag, groups, uiLanguages, true, _s);
                            ea.StartPosition = FormStartPosition.Manual;
                            ea.Location = MousePosition;
                            if (ea.ShowDialog(this) == DialogResult.OK)
                            {
                                HashSet<C4User> users = new HashSet<C4User>();
                                if (ea.SaveAs)
                                {
                                    users.Add(ea.User);
                                    XmlDocument result = _s.CreateUsers(users);
                                }
                                else
                                {
                                    users.Add(ea.User);
                                    XmlDocument result = _s.UpdateUsers(users);
                                }
                                Init(_s);
                            }
                            break;
                        }
                    case "delete":
                        {
                            HashSet<long> ids = new HashSet<long>();
                            foreach (ListViewItem lvi in lvw.SelectedItems)
                            {
                                ids.Add(long.Parse(lvi.Text));
                            }
                            XmlDocument result = _s.DeleteUser(long.Parse(lvw.SelectedItems[0].Text),_adminUserId);
                            if (result.DocumentElement.SelectSingleNode("success[.='false']") != null) StandardMessage.ShowMessage("Failure deleting user(s).", StandardMessage.Severity.ErrorMessage, this, null);
                            Init(_s);
                            break;
                        }
                }
            }
            catch (Exception ex)
            {
                StandardMessage.ShowMessage("Caught Exception", StandardMessage.Severity.ErrorMessage, this, ex);
            }

        }
    }
}
