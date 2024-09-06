using C4ServerConnector.Assets;
using C4Admin.Interfaces;
using C4GeneralGui.Helpers;
using C4ServerConnector;
using System.Xml;
using C4GeneralGui.GuiElements;
using C4Admin.GUI.AssetEditors;
using CDCplusLib.Common;

namespace C4Admin.GUI.AssetLists
{
    public partial class LifecyclesControl : UserControl, ITabView
    {
        private ListViewSort _lvwSort;
        private C4Session _s;
        public LifecyclesControl()
        {
            InitializeComponent();
        }
        public void Init(C4Session s)
        {
            _s = s;
            Dictionary<long, C4Lifecycle> assets = _s.ListLifecycles();
            lvw.Items.Clear();
            foreach(C4Lifecycle a in assets.Values)
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
                    tsi = GuiHelper.AppendToolStripItem(ref cm, lvw, "Add lifecycle", "add", true);
                    tsi.Click += HandleContextMenuClick;
                }
                if (lvw.SelectedItems.Count == 1)
                {
                    tsi = GuiHelper.AppendToolStripItem(ref cm, lvw, "Edit lifecycle", "edit", true);
                    tsi.Click += HandleContextMenuClick;
                    tsi = GuiHelper.AppendToolStripItem(ref cm, lvw, "Replace lifecycle", "replace", true);
                    tsi.Click += HandleContextMenuClick;
                }
                if (lvw.SelectedItems.Count > 0)
                {
                    tsi = GuiHelper.AppendToolStripItem(ref cm, lvw, "Delete lifecycle(s)", "delete", true);
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
                            EditLifecycle ea = new EditLifecycle("New lifecycle", _s, null, false);
                            ea.StartPosition = FormStartPosition.Manual;
                            ea.Location = MousePosition;
                            if (ea.ShowDialog(this) == DialogResult.OK)
                            {
                                HashSet<C4Lifecycle> lifecycles = new HashSet<C4Lifecycle>();
                                lifecycles.Add(ea.Lifecycle);
                                XmlDocument result = _s.CreateLifecycles(lifecycles);
                                Init(_s);
                            }
                            break;
                        }
                    case "edit":
                        {
                            ListViewItem lvi = lvw.SelectedItems[0];
                            EditLifecycle ea = new EditLifecycle("Edit lifecycle", _s, (C4Lifecycle)lvi.Tag, true);
                            ea.StartPosition = FormStartPosition.Manual;
                            ea.Location = MousePosition;
                            if (ea.ShowDialog(this) == DialogResult.OK)
                            {
                                HashSet<C4Lifecycle> lifecycles = new HashSet<C4Lifecycle>();
                                if (ea.SaveAs)
                                {
                                    lifecycles.Add(ea.Lifecycle);
                                    XmlDocument result = _s.CreateLifecycles(lifecycles);
                                }
                                else
                                {
                                    lifecycles.Add(ea.Lifecycle);
                                    XmlDocument result = _s.UpdateLifecycles(lifecycles);
                                }
                                Init(_s);
                            }
                            break;
                        }
                    //case "replace":
                    //    {
                    //        StandardMessage.ShowMessage("This function is not yet implemented.", StandardMessage.Severity.ErrorMessage, this, null);
                    //        break;
                    //    }
                    case "delete":
                        {
                            HashSet<long> ids = new HashSet<long>();
                            foreach (ListViewItem lvi in lvw.SelectedItems)
                            {
                                ids.Add(long.Parse(lvi.Text));
                            }
                            XmlDocument result = _s.DeleteLifecycles(ids);
                            if (result.DocumentElement.SelectSingleNode("success[.='false']") != null) StandardMessage.ShowMessage("Failure deleting lifecycle(s).", StandardMessage.Severity.ErrorMessage, this, null);
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
