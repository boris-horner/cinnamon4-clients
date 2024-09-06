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
    public partial class GroupsControl : UserControl, ITabView
    {
        private ListViewSort _lvwSort;
        private C4Session _s;
        private Dictionary<long, C4Group> _assets;
        public GroupsControl()
        {
            InitializeComponent();
        }
        public void Init(C4Session s)
        {
            _s = s;
            _assets = _s.ListGroups();
            lvw.Items.Clear();
            foreach(C4Group a in _assets.Values)
            {
                ListViewItem lvi = lvw.Items.Add(a.Id.ToString());
                lvi.SubItems.Add(a.Name);
                lvi.SubItems.Add(a.ParentId==null?"":_assets[(long)a.ParentId].ToString());
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
                    tsi = GuiHelper.AppendToolStripItem(ref cm, lvw, "Add group", "add", true);
                    tsi.Click += HandleContextMenuClick;
                }
                if (lvw.SelectedItems.Count == 1)
                {
                    tsi = GuiHelper.AppendToolStripItem(ref cm, lvw, "Edit group", "edit", true);
                    tsi.Click += HandleContextMenuClick;
                    tsi = GuiHelper.AppendToolStripItem(ref cm, lvw, "Replace group", "replace", true);
                    tsi.Click += HandleContextMenuClick;
                }
                if (lvw.SelectedItems.Count > 0)
                {
                    tsi = GuiHelper.AppendToolStripItem(ref cm, lvw, "Delete group(s)", "delete", true);
                    tsi.Click += HandleContextMenuClick;
                    tsi = GuiHelper.AppendToolStripItem(ref cm, lvw, "Delete group(s) and children recursively", "delete_recursive", true);
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
                            EditGroup ea = new EditGroup("New group", null, _assets, false);
                            ea.StartPosition = FormStartPosition.Manual;
                            ea.Location = MousePosition;
                            if (ea.ShowDialog(this) == DialogResult.OK)
                            {
                                HashSet<C4Group> groups = new HashSet<C4Group>();
                                groups.Add(ea.Group);
                                XmlDocument result = _s.CreateGroups(groups);
                                Init(_s);
                            }
                            break;
                        }
                    case "edit":
                        {
                            ListViewItem lvi = lvw.SelectedItems[0];
                            EditGroup ea = new EditGroup("Edit group", (C4Group)lvi.Tag, _assets, true);
                            ea.StartPosition = FormStartPosition.Manual;
                            ea.Location = MousePosition;
                            if (ea.ShowDialog(this) == DialogResult.OK)
                            {
                                HashSet<C4Group> groups = new HashSet<C4Group>();
                                if (ea.SaveAs)
                                {
                                    groups.Add(ea.Group);
                                    XmlDocument result = _s.CreateGroups(groups);
                                }
                                else
                                {
                                    groups.Add(ea.Group);
                                    XmlDocument result = _s.UpdateGroups(groups);
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
                    case "delete_recursive":
                        {
                            // nuild HashSet of group ids
                            HashSet<long> ids = new HashSet<long>();
                            foreach (ListViewItem lvi in lvw.SelectedItems)
                            {
                                ids.Add(long.Parse(lvi.Text));
                            }

                            // TODO: check if groups have groups to be deleted as parents - return error message
                            Dictionary<long,C4Group> groups = _s.ListGroups();
                            string msg = null;
                            foreach (C4Group g in groups.Values)
                            {
                                if (g.ParentId!=null && ids.Contains((long)g.ParentId))
                                {
                                    msg=msg==null?msg=g.Name:string.Concat(msg,"\n",g.Name);
                                }
                            }
                            if(msg!=null)
                            {
                                StandardMessage.ShowMessage(string.Concat("The following groups have parent groups to be deleted.\n",
                                                                          "Modify or delete these groups first.\n\n",
                                                                          msg), StandardMessage.Severity.ErrorMessage, this, null);
                                break;
                            }



                            // TODO: check whether there are users in these groups - ask whether the memberships can be deleted

                            XmlDocument result = _s.DeleteGroups(ids, tsmi.Name=="delete_recursive");
                            if (result.DocumentElement.SelectSingleNode("success[.='false']") != null) StandardMessage.ShowMessage("Failure deleting groups(s).", StandardMessage.Severity.ErrorMessage, this, null);
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
