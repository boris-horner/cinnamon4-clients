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
    public partial class ConfigEntriesControl : UserControl, ITabView
    {
        private ListViewSort _lvwSort;
        private C4Session _s;
        public ConfigEntriesControl()
        {
            InitializeComponent();
        }
        public void Init(C4Session s)
        {
            _s = s;
            Dictionary<long, C4ConfigEntry> assets = _s.ListConfigEntries();
            lvw.Items.Clear();
            foreach(C4ConfigEntry a in assets.Values)
            {
                ListViewItem lvi = lvw.Items.Add(a.Id.ToString());
                lvi.SubItems.Add(a.Name);
                lvi.SubItems.Add(a.PubliclyVisible ? "X" : "");
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
                    tsi = GuiHelper.AppendToolStripItem(ref cm, lvw, "Add config entry", "add", true);
                    tsi.Click += HandleContextMenuClick;
                }
                if (lvw.SelectedItems.Count == 1)
                {
                    tsi = GuiHelper.AppendToolStripItem(ref cm, lvw, "Edit config entry", "edit", true);
                    tsi.Click += HandleContextMenuClick;
                    tsi = GuiHelper.AppendToolStripItem(ref cm, lvw, "Replace config entry", "replace", true);
                    tsi.Click += HandleContextMenuClick;
                }
                if (lvw.SelectedItems.Count > 0)
                {
                    tsi = GuiHelper.AppendToolStripItem(ref cm, lvw, "Delete config entry / entries", "delete", true);
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
                            EditConfigEntry ea = new EditConfigEntry("New config entry", null, false);
                            ea.StartPosition = FormStartPosition.Manual;
                            ea.Location = MousePosition;
                            if (ea.ShowDialog(this) == DialogResult.OK)
                            {
                                HashSet<C4ConfigEntry> configEntries = new HashSet<C4ConfigEntry>();
                                configEntries.Add(ea.ConfigEntry);
                                XmlDocument result = _s.CreateConfigEntries(configEntries);
                                Init(_s);
                            }
                            break;
                        }
                    case "edit":
                        {
                            ListViewItem lvi = lvw.SelectedItems[0];
                            EditConfigEntry ea = new EditConfigEntry("Edit config entry", (C4ConfigEntry)lvi.Tag, true);
                            ea.StartPosition = FormStartPosition.Manual;
                            ea.Location = MousePosition;
                            if (ea.ShowDialog(this) == DialogResult.OK)
                            {
                                HashSet<C4ConfigEntry> configEntries = new HashSet<C4ConfigEntry>();
                                if (ea.SaveAs)
                                {
                                    configEntries.Add(ea.ConfigEntry);
                                    XmlDocument result = _s.CreateConfigEntries(configEntries);
                                }
                                else
                                {
                                    configEntries.Add(ea.ConfigEntry);
                                    XmlDocument result = _s.UpdateConfigEntries(configEntries);
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
                            XmlDocument result = _s.DeleteConfigEntries(ids);
                            if (result.DocumentElement.SelectSingleNode("success[.='false']") != null) StandardMessage.ShowMessage("Failure deleting config entry / entries.", StandardMessage.Severity.ErrorMessage, this, null);
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
