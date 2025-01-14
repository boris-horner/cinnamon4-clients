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
    public partial class RelationTypesControl : UserControl, ITabView
    {
        private ListViewSort _lvwSort;
        private C4Session _s;
        public RelationTypesControl()
        {
            InitializeComponent();
        }
        public void Init(C4Session s)
        {
            _s = s;
            Dictionary<long, C4RelationType> assets = _s.ListRelationTypes();
            lvw.Items.Clear();
            foreach(C4RelationType a in assets.Values)
            {
                ListViewItem lvi = lvw.Items.Add(a.Id.ToString());
                lvi.SubItems.Add(a.Name);
                lvi.SubItems.Add(a.LeftObjectProtected?"X":"");
                lvi.SubItems.Add(a.RightObjectProtected ? "X" : "");
                lvi.SubItems.Add(a.CloneOnLeftCopy ? "X" : "");
                lvi.SubItems.Add(a.CloneOnRightCopy ? "X" : "");
                lvi.SubItems.Add(a.CloneOnLeftVersion ? "X" : "");
                lvi.SubItems.Add(a.CloneOnRightVersion ? "X" : "");
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
                    tsi = GuiHelper.AppendToolStripItem(ref cm, lvw, "Add relation type", "add", true);
                    tsi.Click += HandleContextMenuClick;
                }
                if (lvw.SelectedItems.Count == 1)
                {
                    tsi = GuiHelper.AppendToolStripItem(ref cm, lvw, "Edit relation type", "edit", true);
                    tsi.Click += HandleContextMenuClick;
                    tsi = GuiHelper.AppendToolStripItem(ref cm, lvw, "Replace relation type", "replace", true);
                    tsi.Click += HandleContextMenuClick;
                }
                if (lvw.SelectedItems.Count > 0)
                {
                    tsi = GuiHelper.AppendToolStripItem(ref cm, lvw, "Delete relation type(s)", "delete", true);
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
                            EditRelationType ea=new EditRelationType("New relation type", null,false);
                            ea.StartPosition = FormStartPosition.Manual;
                            ea.Location = MousePosition;
                            if (ea.ShowDialog(this)==DialogResult.OK)
                            {
                                HashSet<C4RelationType> relationTypes = new HashSet<C4RelationType>();
                                relationTypes.Add(ea.RelationType);
                                XmlDocument result = _s.CreateRelationTypes(relationTypes);
                                Init(_s);
                            }
                            break;
                        } 
                    case "edit":
                        {
                            ListViewItem lvi = lvw.SelectedItems[0];
                            EditRelationType ea = new EditRelationType("Edit relation type", (C4RelationType)lvi.Tag, true);
                            ea.StartPosition = FormStartPosition.Manual;
                            ea.Location = MousePosition;
                            if (ea.ShowDialog(this) == DialogResult.OK)
                            {
                                HashSet<C4RelationType> relationTypes = new HashSet<C4RelationType>();
                                if(ea.SaveAs)
                                {
                                    relationTypes.Add(ea.RelationType);
                                    XmlDocument result = _s.CreateRelationTypes(relationTypes);
                                }
                                else
                                {
                                    relationTypes.Add(ea.RelationType);
                                    XmlDocument result = _s.UpdateRelationTypes(relationTypes);
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
                            XmlDocument result = _s.DeleteRelationTypes(ids);
                            if (result.DocumentElement.SelectSingleNode("success[.='false']") != null) StandardMessage.ShowMessage("Failure deleting relation type(s).", StandardMessage.Severity.ErrorMessage, this, null);
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
