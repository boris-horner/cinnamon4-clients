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
    public partial class LanguagesControl : UserControl, ITabView
    {
        private ListViewSort _lvwSort;
        private C4Session _s;
        public bool UiLanguageMode { get; set; }
        public LanguagesControl()
        {
            InitializeComponent();
        }
        public void Init(C4Session s)
        {
            _s = s;
            Dictionary<long, C4Language> assets = UiLanguageMode? _s.ListUiLanguages() : _s.ListLanguages();
            lvw.Items.Clear();
            foreach(C4Language a in assets.Values)
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
                    tsi = GuiHelper.AppendToolStripItem(ref cm, lvw, UiLanguageMode ? "Add user interface language" : "Add language", "add", true);
                    tsi.Click += HandleContextMenuClick;
                }
                if (lvw.SelectedItems.Count == 1)
                {
                    tsi = GuiHelper.AppendToolStripItem(ref cm, lvw, UiLanguageMode ? "Edit user interface language" : "Edit language", "edit", true);
                    tsi.Click += HandleContextMenuClick;
                    tsi = GuiHelper.AppendToolStripItem(ref cm, lvw, UiLanguageMode ? "Replace user interface language" : "Replace language", "replace", true);
                    tsi.Click += HandleContextMenuClick;
                }
                if (lvw.SelectedItems.Count > 0)
                {
                    tsi = GuiHelper.AppendToolStripItem(ref cm, lvw, UiLanguageMode ? "Delete user interface language(s)" : "Delete language(s)", "delete", true);
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
                            EditLanguage ea=new EditLanguage(UiLanguageMode? "New user interface language":"New language", null,false);
                            ea.StartPosition = FormStartPosition.Manual;
                            ea.Location = MousePosition;
                            if (ea.ShowDialog(this)==DialogResult.OK)
                            {
                                HashSet<C4Language> objectTypes = new HashSet<C4Language>();
                                objectTypes.Add(ea.Language);
                                XmlDocument result = UiLanguageMode ? _s.CreateUiLanguages(objectTypes):_s.CreateLanguages(objectTypes);
                                Init(_s);
                            }
                            break;
                        } 
                    case "edit":
                        {
                            ListViewItem lvi = lvw.SelectedItems[0];
                            EditLanguage ea = new EditLanguage(UiLanguageMode ? "Edit user interface language" : "Edit language", (C4Language)lvi.Tag, true);
                            ea.StartPosition = FormStartPosition.Manual;
                            ea.Location = MousePosition;
                            if (ea.ShowDialog(this) == DialogResult.OK)
                            {
                                HashSet<C4Language> objectTypes = new HashSet<C4Language>();
                                if(ea.SaveAs)
                                {
                                    objectTypes.Add(ea.Language);
                                    XmlDocument result = UiLanguageMode ? _s.CreateUiLanguages(objectTypes) : _s.CreateLanguages(objectTypes);
                                }
                                else
                                {
                                    objectTypes.Add(ea.Language);
                                    XmlDocument result = UiLanguageMode ? _s.UpdateUiLanguages(objectTypes) : _s.UpdateLanguages(objectTypes);
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
                            XmlDocument result = UiLanguageMode ? _s.DeleteUiLanguages(ids) : _s.DeleteLanguages(ids);
                            if (result.DocumentElement.SelectSingleNode("success[.='false']") != null) StandardMessage.ShowMessage(UiLanguageMode ? "Failure deleting user interface language(s)." : "Failure deleting language(s).", StandardMessage.Severity.ErrorMessage, this, null);
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
