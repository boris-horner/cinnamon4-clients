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
    public partial class MetasetTypesControl : UserControl, ITabView
    {
        private ListViewSort _lvwSort;
        private C4Session _s;
        public MetasetTypesControl()
        {
            InitializeComponent();
        }
        public void Init(C4Session s)
        {
            _s = s;
            Dictionary<long, C4MetasetType> assets = _s.ListMetasetTypes();
            lvw.Items.Clear();
            foreach(C4MetasetType a in assets.Values)
            {
                ListViewItem lvi = lvw.Items.Add(a.Id.ToString());
                lvi.SubItems.Add(a.Name);
                lvi.SubItems.Add(a.Unique?"X":"");
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
                    tsi = GuiHelper.AppendToolStripItem(ref cm, lvw, "Add metaset type", "add", true);
                    tsi.Click += HandleContextMenuClick;
                }
                if (lvw.SelectedItems.Count == 1)
                {
                    tsi = GuiHelper.AppendToolStripItem(ref cm, lvw, "Edit metaset type", "edit", true);
                    tsi.Click += HandleContextMenuClick;
                    tsi = GuiHelper.AppendToolStripItem(ref cm, lvw, "Replace metaset type", "replace", true);
                    tsi.Click += HandleContextMenuClick;
                }
                if (lvw.SelectedItems.Count > 0)
                {
                    tsi = GuiHelper.AppendToolStripItem(ref cm, lvw, "Delete metaset type(s)", "delete", true);
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
                            EditMetasetType ea=new EditMetasetType("New metaset type", null,false);
                            ea.StartPosition = FormStartPosition.Manual;
                            ea.Location = MousePosition;
                            if (ea.ShowDialog(this)==DialogResult.OK)
                            {
                                HashSet<C4MetasetType> metasetTypes = new HashSet<C4MetasetType>();
                                metasetTypes.Add(ea.MetasetType);
                                XmlDocument result = _s.CreateMetasetTypes(metasetTypes);
                                Init(_s);
                            }
                            break;
                        } 
                    case "edit":
                        {
                            ListViewItem lvi = lvw.SelectedItems[0];
                            EditMetasetType ea = new EditMetasetType("Edit metaset type", (C4MetasetType)lvi.Tag, true);
                            ea.StartPosition = FormStartPosition.Manual;
                            ea.Location = MousePosition;
                            if (ea.ShowDialog(this) == DialogResult.OK)
                            {
                                HashSet<C4MetasetType> metasetTypes = new HashSet<C4MetasetType>();
                                if(ea.SaveAs)
                                {
                                    metasetTypes.Add(ea.MetasetType);
                                    XmlDocument result = _s.CreateMetasetTypes(metasetTypes);
                                }
                                else
                                {
                                    metasetTypes.Add(ea.MetasetType);
                                    XmlDocument result = _s.UpdateMetasetTypes(metasetTypes);
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
                            foreach(ListViewItem lvi in lvw.SelectedItems)
                            {
                                ids.Add(long.Parse(lvi.Text));
                            }
                            XmlDocument result = _s.DeleteMetasetTypes(ids);
                            if (result.DocumentElement.SelectSingleNode("success[.='false']") != null) StandardMessage.ShowMessage("Failure deleting metaset type(s).", StandardMessage.Severity.ErrorMessage, this, null);
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
