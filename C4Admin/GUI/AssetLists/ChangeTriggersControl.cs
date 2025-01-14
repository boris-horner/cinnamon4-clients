using C4ServerConnector.Assets;
using C4Admin.GUI.AssetEditors;
using C4Admin.Interfaces;
using C4GeneralGui.Helpers;
using C4ServerConnector;
using C4GeneralGui.GuiElements;
using System.Xml;
using CDCplusLib.Common;

namespace C4Admin.GUI
{
    public partial class ChangeTriggersControl : UserControl, ITabView
    {
        private ListViewSort _lvwSort;
        private C4Session _s;
        public ChangeTriggersControl()
        {
            InitializeComponent();
        }
        public void Init(C4Session s)
        {
            _s = s;
            Dictionary<long, C4ChangeTrigger> assets = _s.ListChangeTriggers();
            lvw.Items.Clear();
            foreach (C4ChangeTrigger a in assets.Values)
            {
                ListViewItem lvi = lvw.Items.Add(a.Id.ToString());
                lvi.SubItems.Add(a.Name);
                lvi.SubItems.Add(a.TriggerType);
                lvi.SubItems.Add(a.Active ? "X" : "");
                lvi.SubItems.Add(a.PreTrigger ? "X" : "");
                lvi.SubItems.Add(a.PostTrigger ? "X" : "");
                lvi.SubItems.Add(a.PostCommitTrigger ? "X" : "");
                lvi.SubItems.Add(a.CopyFileContent ? "X" : "");
                lvi.SubItems.Add(a.Ranking.ToString());
                lvi.SubItems.Add(a.Controller);
                lvi.SubItems.Add(a.Action);
                lvi.Tag = a;
            }
            if (_lvwSort == null) _lvwSort = new ListViewSort(lvw, false);
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
                    tsi = GuiHelper.AppendToolStripItem(ref cm, lvw, "Add change trigger", "add", true);
                    tsi.Click += HandleContextMenuClick;
                }
                if (lvw.SelectedItems.Count == 1)
                {
                    tsi = GuiHelper.AppendToolStripItem(ref cm, lvw, "Edit change trigger", "edit", true);
                    tsi.Click += HandleContextMenuClick;
                    tsi = GuiHelper.AppendToolStripItem(ref cm, lvw, "Replace change trigger", "replace", true);
                    tsi.Click += HandleContextMenuClick;
                }
                if (lvw.SelectedItems.Count > 0)
                {
                    tsi = GuiHelper.AppendToolStripItem(ref cm, lvw, "Delete change trigger(s)", "delete", true);
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
                            EditChangeTrigger ea = new EditChangeTrigger("New change trigger", null, false);
                            ea.StartPosition = FormStartPosition.Manual;
                            ea.Location = MousePosition;
                            if (ea.ShowDialog(this) == DialogResult.OK)
                            {
                                HashSet<C4ChangeTrigger> relationTypes = new HashSet<C4ChangeTrigger>();
                                relationTypes.Add(ea.ChangeTrigger);
                                XmlDocument result = _s.CreateChangeTriggers(relationTypes);
                                Init(_s);
                            }
                            break;
                        }
                    case "edit":
                        {
                            ListViewItem lvi = lvw.SelectedItems[0];
                            EditChangeTrigger ea = new EditChangeTrigger("Edit change trigger", (C4ChangeTrigger)lvi.Tag, true);
                            ea.StartPosition = FormStartPosition.Manual;
                            ea.Location = MousePosition;
                            if (ea.ShowDialog(this) == DialogResult.OK)
                            {
                                HashSet<C4ChangeTrigger> relationTypes = new HashSet<C4ChangeTrigger>();
                                if (ea.SaveAs)
                                {
                                    relationTypes.Add(ea.ChangeTrigger);
                                    XmlDocument result = _s.CreateChangeTriggers(relationTypes);
                                }
                                else
                                {
                                    relationTypes.Add(ea.ChangeTrigger);
                                    XmlDocument result = _s.UpdateChangeTriggers(relationTypes);
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
                            XmlDocument result = _s.DeleteChangeTriggers(ids);
                            if (result.DocumentElement.SelectSingleNode("success[.='false']") != null) StandardMessage.ShowMessage("Failure deleting change trigger(s).", StandardMessage.Severity.ErrorMessage, this, null);
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
