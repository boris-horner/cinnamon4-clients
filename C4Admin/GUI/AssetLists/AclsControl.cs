using C4ServerConnector.Assets;
using C4Admin.GUI.AssetEditors;
using C4Admin.Interfaces;
using C4GeneralGui.Helpers;
using C4ServerConnector;
using C4GeneralGui.GuiElements;
using System.Xml;
using CDCplusLib.Common;

namespace C4Admin.GUI.AssetLists
{
    public partial class AclsControl : UserControl, ITabView
    {
        private ListViewSort _lvwSort;
        private C4Session _s;
        public AclsControl()
        {
            InitializeComponent();
        }
        public void Init(C4Session s)
        {
            _s = s;
            Dictionary<long, C4Acl> assets = _s.ListAcls();
            lvw.Items.Clear();
            foreach(C4Acl a in assets.Values)
            {
                ListViewItem lvi = lvw.Items.Add(a.Id.ToString());
                lvi.SubItems.Add(a.Name);
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
                    tsi = GuiHelper.AppendToolStripItem(ref cm, lvw, "Add ACL", "add", true);
                    tsi.Click += HandleContextMenuClick;
                }
                if (lvw.SelectedItems.Count == 1)
                {
                    tsi = GuiHelper.AppendToolStripItem(ref cm, lvw, "Edit ACL", "edit", true);
                    tsi.Click += HandleContextMenuClick;
                    tsi = GuiHelper.AppendToolStripItem(ref cm, lvw, "Replace ACL", "replace", true);
                    tsi.Click += HandleContextMenuClick;
                }
                if (lvw.SelectedItems.Count > 0)
                {
                    tsi = GuiHelper.AppendToolStripItem(ref cm, lvw, "Delete ACL(s)", "delete", true);
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
                            EditAcl ea = new EditAcl("New ACL", _s, null, false);
                            ea.StartPosition = FormStartPosition.Manual;
                            ea.Location = MousePosition;
                            if (ea.ShowDialog(this) == DialogResult.OK)
                            {
                                HashSet<C4Acl> acls = new HashSet<C4Acl>();
                                acls.Add(ea.Acl);
                                XmlDocument result = _s.CreateAcls(acls);
                                Init(_s);
                            }
                            break;
                        }
                    case "edit":
                        {
                            ListViewItem lvi = lvw.SelectedItems[0];
                            EditAcl ea = new EditAcl("Edit ACL", _s, (C4Acl)lvi.Tag, true);
                            ea.StartPosition = FormStartPosition.Manual;
                            ea.Location = MousePosition;
                            if (ea.ShowDialog(this) == DialogResult.OK)
                            {
                                HashSet<C4Acl> acls = new HashSet<C4Acl>();
                                if (ea.SaveAs)
                                {
                                    acls.Add(ea.Acl);
                                    XmlDocument result = _s.CreateAcls(acls);
                                }
                                else
                                {
                                    acls.Add(ea.Acl);
                                    XmlDocument result = _s.UpdateAcls(acls);
                                }
                                Init(_s);
                            }
                            break;
                        }
                    case "replace":
                        {
                            StandardMessage.ShowMessage("This function is not yet implemented.", StandardMessage.Severity.ErrorMessage, this, null);
                            break;
                        }
                    case "delete":
                        {
                            HashSet<long> ids = new HashSet<long>();
                            foreach (ListViewItem lvi in lvw.SelectedItems)
                            {
                                ids.Add(long.Parse(lvi.Text));
                            }
                            XmlDocument result = _s.DeleteAcls(ids);
                            if (result.DocumentElement.SelectSingleNode("success[.='false']") != null) StandardMessage.ShowMessage("Failure deleting ACL(s).", StandardMessage.Severity.ErrorMessage, this, null);
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
