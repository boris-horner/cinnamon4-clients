// Copyright 2012,2024 texolution GmbH
// 
// Licensed under the Apache License, Version 2.0 (the "License"); you may not 
// use this file except in compliance with the License. You may obtain a copy of 
// the License at
// 
// http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software 
// distributed under the License is distributed on an "AS IS" BASIS, WITHOUT 
// WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. See the 
// License for the specific language governing permissions and limitations under 
// the License.
using CDCplusLib.Interfaces;
using System.Xml;
using CDCplusLib.Common;
using CDCplusLib.DataModel;
using C4ObjectApi.Interfaces;
using C4ObjectApi.Repository;
using C4ObjectApi.Helpers;
using CDCplusLib.EventData;

namespace CDCplusLib.TabControls
{
    public partial class ContentsFolderControl : UserControl, IGenericControl
    {
        private const string USER_SETTINGS_NAME = "column_setting.folder_contents";

        private CmnFolder _f;
        private GlobalApplicationData _gad;
        private XmlElement _configEl;
        private XmlElement _rldConfigEl;
        private bool _initializing;
        private Dictionary<string, string> _allowedCustomFields;
        private string _defaultConfigName;
        private CmnSession _s;
        private Dictionary<long, IRepositoryNode> _dict;
        private readonly ToolTip _tt;
        private IFolderFilter _allVersionFilter;
        private ITranslationsView _translationsView;
        //private readonly KeyEventTable _keyEvents;
        //private Dictionary<string, RldColumnConfiguration> _columnConfigurations;
        //private bool _settingsDirty;

        public event SessionWindowRequestEventHandler SessionWindowRequest;
        public event ListSelectionChangedEventHandler ListSelectionChanged;
        public event TreeSelectionChangedEventHandler TreeSelectionChanged;
        public event ContextMenuRequestEventHandler ContextMenuRequest;
        public event FunctionRequestEventHandler FunctionRequest;
        public event NodesModifiedEventHandler NodesModified;
        public event KeyPressedEventHandler KeyPressedEvent;
        public event RefreshRequestEventHandler RefreshRequest;

        public ContentsFolderControl()
        {
            InitializeComponent();
            _initializing = true;
            lvseSettings.Visible = false;
            splTranslations.Panel2Collapsed = true;
            _defaultConfigName = null;
            _tt = new ToolTip();
            _tt.SetToolTip(cmdSettings, Properties.Resources.lblShowSettings);
            _tt.SetToolTip(cmdTranslations, Properties.Resources.lblShowTranslations);
        }

        private void InitViewCombo()
        {
            cboView.Items.Clear();
            cboView.Items.Add(Properties.Resources.lblViewLargeIcons);
            cboView.Items.Add(Properties.Resources.lblViewSmallIcons);
            cboView.Items.Add(Properties.Resources.lblViewDetails);
            cboView.SelectedIndex = 2;
        }
        public bool HasSelection { get { return true; } }

        public Dictionary<long, IRepositoryNode> Selection
        {
            get
            {
                return rldNodes.Selection;
            }
            set
            {
                rldNodes.Selection = value;
            }
        }
        public bool AutoRefresh { get { return true; } }

        public bool ListContext { get { return false; } }

        public bool IsDirty
        {
            get { return false; }
            
        }


        public string GetTabText()
        {
            return Properties.Resources.tabShowFolderContents;
        }

        public void Init(Dictionary<long, IRepositoryNode> dict)
        {
            _dict = dict;
            CmnFolder f = DictionaryHelper.GetSingleFolder(_dict);
            if (f != null)
            {
                Cursor = Cursors.WaitCursor;
                rldNodes.EventsActive = false;
                _initializing = true;
                _f = f;
                IFolderFilter filter = (IFolderFilter)cboVersionDisplay.SelectedItem;
                rldNodes.NodeList = filter.GetNodeList(f);
                _initializing = false;
                rldNodes.EventsActive = true;
                Cursor = null;
            }
        }

        public bool IsValid(Dictionary<long, IRepositoryNode> dict, IGenericControl.ContextType ct)
        {
            if (ct != IGenericControl.ContextType.Folder) return false;
            _dict = dict;
            CmnFolder f = DictionaryHelper.GetSingleFolder(dict);
            return (f != null);
        }

        public void ReInit()
        {
            Init(_dict);
        }

        public void Reset(CmnSession s, GlobalApplicationData gad, XmlElement configEl)
        {
            _gad = gad;
            _configEl = configEl;
            _rldConfigEl = (XmlElement)(s.UserConfig.DocumentElement.SelectSingleNode("classes/result_list_display"));
            _allowedCustomFields = new Dictionary<string, string>();
            _s = s;

            XmlElement transListViewEl = (XmlElement)(s.UserConfig.DocumentElement.SelectSingleNode("classes/translation_list_view"));
            if(transListViewEl!=null)
            {
                // new tlv late-bounded
                _translationsView = (ITranslationsView)(s.SessionConfig.GetAssembly("EnterprisePackage").CreateInstance("EnterprisePackage.TabControls.ContentsControl.TranslationsListView"));
                UserControl ucTranslationsView = (UserControl)_translationsView;

                // append control to panel
                pTranslationContainer.Controls.Clear();
                pTranslationContainer.Controls.Add(ucTranslationsView);
                ucTranslationsView.Dock = DockStyle.Fill;

                _translationsView.Reset(s, transListViewEl);
                if (!_translationsView.SupportVariants) cmdTranslations.Visible = false;
            }
            else
            {
                cmdTranslations.Visible = false;
            }

            // get node data provider
            string ndpAsm = _rldConfigEl.SelectSingleNode("node_data_provider/@assembly").InnerText;
            string ndpTp = _rldConfigEl.SelectSingleNode("node_data_provider/@type").InnerText;

            RldColumnConfiguration lastSelected = (RldColumnConfiguration)cboColumnSettings.SelectedItem;
            string configName = null;
            if(lastSelected==null)  // first init of control
            {
                XmlNode defaultConfigN = s.UserSettings.GetUserSetting(USER_SETTINGS_NAME).SelectSingleNode("configurations/@default");
                if (defaultConfigN == null) defaultConfigN = configEl.SelectSingleNode("custom/default_configuration/@name");
                _defaultConfigName = defaultConfigN.InnerText;
                configName = _defaultConfigName;
            }
            else
            {
                configName = lastSelected.Name;
            }

            XmlElement columnConfigEl = (XmlElement)s.UserSettings.GetUserSetting(USER_SETTINGS_NAME).SelectSingleNode("configurations/configuration[@name='" + configName + "']");
            if(columnConfigEl==null) columnConfigEl= (XmlElement)_rldConfigEl.SelectSingleNode("configurations/configuration[@name='" + configName + "']");
            if (columnConfigEl == null) columnConfigEl = (XmlElement)_rldConfigEl.SelectSingleNode("configurations/configuration[@name='" + _defaultConfigName + "']");
            configName = columnConfigEl.GetAttribute("name");
            if(lastSelected!=null) lastSelected = lvseSettings.ColumnConfigurations[configName];

            INodeDataProvider nodeDataProvider = (INodeDataProvider)(s.SessionConfig.GetAssembly(ndpAsm).CreateInstance(ndpAsm + "." + ndpTp));
            nodeDataProvider.Init(s, columnConfigEl, _rldConfigEl);
            rldNodes.Init(s, columnConfigEl, _rldConfigEl, nodeDataProvider);

            // load allowed custom fields
            foreach(XmlElement field in _rldConfigEl.SelectNodes("allowed_custom_fields/field"))
            {
                _allowedCustomFields.Add(field.InnerText, s.SessionConfig.C4Sc.GetLocalizedLabel("listview_custom_column." + field.InnerText, "other"));
            }
            lvseSettings.Init(_rldConfigEl, nodeDataProvider, s, _configEl, _defaultConfigName, USER_SETTINGS_NAME);
            cboColumnSettings.Items.Clear();
            foreach(RldColumnConfiguration cc in lvseSettings.ColumnConfigurations.Values)
            {
                cboColumnSettings.Items.Add(cc);
            }
            if (lastSelected == null) cboColumnSettings.SelectedItem = lvseSettings.ColumnConfigurations[_defaultConfigName];
            else cboColumnSettings.Text = lastSelected.ToString();

            InitFilterSettings(s);
            InitViewCombo();
        }

        private void InitFilterSettings(CmnSession s)
        {
            XmlElement filtersEl = (XmlElement)_configEl.SelectSingleNode("custom/filters");
            string defaultFilterKey = filtersEl.HasAttribute("default") ? filtersEl.GetAttribute("default") : null;
            string defaultFilterLabel = null;
            cboVersionDisplay.Items.Clear();
            foreach (XmlElement filterEl in filtersEl.SelectNodes("filter"))
            {
                string asm = filterEl.GetAttribute("assembly");
                string tp = filterEl.GetAttribute("type");
                IFolderFilter flt = (IFolderFilter)s.SessionConfig.GetAssembly(asm).CreateInstance(asm + "." + tp);
                flt.Init(filterEl);
                cboVersionDisplay.Items.Add(flt);
                if (defaultFilterKey != null && defaultFilterKey == flt.GetKey()) defaultFilterLabel = flt.ToString();
                if (flt.ShowsAllVersions) _allVersionFilter = flt;
            }
            if (defaultFilterKey == null) cboVersionDisplay.SelectedIndex = 0;
            else cboVersionDisplay.Text = defaultFilterLabel;
        }
        public void Save()
        {
            // nothing to do
        }

        private void cboColumnSettings_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rldNodes.EventsActive && !_initializing)
            {
                rldNodes.EventsActive = false;
                Reset(_s, _gad, _configEl);
                ReInit();
                rldNodes.EventsActive = true;
            }
        }

        private void cboVersionDisplay_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rldNodes.EventsActive) Init(_dict);
        }

        private void cboView_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch(cboView.SelectedIndex)
            {
                case 0:
                    rldNodes.View = View.LargeIcon;
                    break;
                case 1:
                    rldNodes.View = View.SmallIcon;
                    break;
                case 2:
                    rldNodes.View = View.Details;
                    break;
            }
        }
        private void rldNodes_ListSelectionChanged(WindowSelectionData wsd)
        {
            if (!splTranslations.Panel2Collapsed) UpdateTranslations(wsd.Selection);
            ListSelectionChanged?.Invoke(wsd);
        }
        private void rldNodes_TreeSelectionChanged(WindowSelectionData wsd, ISessionWindow sw)
        {
            TreeSelectionChanged?.Invoke(wsd, null);
        }
        private void rldNodes_ContextMenuRequest(WindowSelectionData wsd, Point position)
        {
            if (wsd.Selection.Count() == 0) wsd.Selection.Add(_f.Id, _f);
            ContextMenuRequest?.Invoke(wsd, position);
        }
        private void rldNodes_FunctionRequest(WindowSelectionData wsd, string assembly, string type)
        {
            FunctionRequest?.Invoke(wsd, assembly, type);
        }
        private void rldNodes_KeyPressedEvent(WindowSelectionData wsd, Keys key, bool shift, bool ctrl, bool alt)
        {
            KeyPressedEvent?.Invoke(wsd, key, shift, ctrl, alt);
        }
        private void rldNodes_RefreshRequest()
        {
            RefreshRequest?.Invoke();
        }

        private void cmdSettings_Click(object sender, EventArgs e)
        {
            lvseSettings.Visible = !lvseSettings.Visible;
            _tt.SetToolTip(cmdSettings, lvseSettings.Visible?Properties.Resources.lblHideSettings: Properties.Resources.lblShowSettings);

        }

        private void lvseSettings_Apply(object sender, EventArgs e)
        {
            _defaultConfigName = lvseSettings.DefaultConfigName;
            cboColumnSettings.SelectedItem = lvseSettings.SelectedColumnConfiguration;
            Reset(_f.Session, _gad, _configEl);
            ReInit();
        }

        private void UpdateTranslations(Dictionary<long, IRepositoryNode> dict)
        {
            if (!splTranslations.Panel2Collapsed)
            {
                CmnObject o = null;
                if (dict.Count==1)
                {
                    IEnumerator<IRepositoryNode> enumerator = dict.Values.GetEnumerator();
                    enumerator.MoveNext();
                    IRepositoryNode ow = enumerator.Current;
                    if(ow.GetType()==typeof(CmnObject))
                    {
                        o = (CmnObject)ow;
                        lblTranslations.Text = String.Format(Properties.Resources.lblTranslationsTitle,o.Language.ToString());
                        _translationsView.Init(o);
                    }
                }

            }
        }
        private void CmdTranslations_Click(object sender, EventArgs e)
        {
            splTranslations.Panel2Collapsed = !splTranslations.Panel2Collapsed;
            if (splTranslations.Panel2Collapsed)
            {
                lvseSettings.Visible = false;
                _tt.SetToolTip(cmdSettings, lvseSettings.Visible ? Properties.Resources.lblHideSettings : Properties.Resources.lblShowSettings);
            }
            _tt.SetToolTip(cmdTranslations, pTranslations.Visible ? Properties.Resources.lblHideTranslations : Properties.Resources.lblShowTranslations);
            UpdateTranslations(rldNodes.Selection);
        }
    }
}
