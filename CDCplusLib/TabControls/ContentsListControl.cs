﻿// Copyright 2012,2024 texolution GmbH
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
using CDCplusLib.DataModel;
using CDCplusLib.Common;
using C4ObjectApi.Interfaces;
using C4ObjectApi.Repository;
using CDCplusLib.EventData;

namespace CDCplusLib.TabControls
{
    public partial class ContentsListControl : UserControl, IGenericControl
    {
        private const string USER_SETTINGS_NAME = "column_setting.list_contents";

        private GlobalApplicationData _gad;
        private XmlElement _configEl;
        private bool _initializing;
        private XmlElement _rldConfigEl;
        private Dictionary<string, string> _allowedCustomFields;
        private string _defaultConfigName;
        private CmnSession _s;
        private Dictionary<long, IRepositoryNode> _dict;
        private readonly ToolTip _tt;
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

        public ContentsListControl()
        {
            InitializeComponent();
            _initializing = true;
            lvseSettings.Visible = false;
            _defaultConfigName = null;
            _tt = new ToolTip();
            _tt.SetToolTip(cmdSettings, Properties.Resources.lblShowSettings);
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
            if(dict!=null)
            {
                Cursor = Cursors.WaitCursor;
                _dict = dict;
                rldNodes.EventsActive = false;
                _initializing = true;
                CmnNodeList nl = new CmnNodeList(_s, null);
                foreach (IRepositoryNode ow in dict.Values) nl.List.Add(ow.Id, ow);
                rldNodes.NodeList = nl;
                _initializing = false;
                rldNodes.EventsActive = true;
                Cursor = null;
            }
        }
        public bool IsValid(Dictionary<long, IRepositoryNode> dict, IGenericControl.ContextType ct)
        {
            if (ct != IGenericControl.ContextType.List) return false;
            return dict !=null;
        }

        public void ReInit()
        {
            Init(_dict);
        }

        public void Reset(CmnSession s, GlobalApplicationData gad, XmlElement configEl)
        {
            _gad = gad;
            _configEl = configEl;
            //_includeSummary = (s.UserConfig.DocumentElement.SelectSingleNode("global_settings/summary[@include_summary='true']") != null);
            _rldConfigEl = (XmlElement)(s.UserConfig.DocumentElement.SelectSingleNode("classes/result_list_display"));
            _allowedCustomFields = new Dictionary<string, string>();
            _s = s;

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
            InitViewCombo();

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
            ListSelectionChanged?.Invoke(wsd);
        }
        private void rldNodes_TreeSelectionChanged(WindowSelectionData wsd, ISessionWindow sw)
        {
            TreeSelectionChanged?.Invoke(wsd, null);
        }
        private void rldNodes_ContextMenuRequest(WindowSelectionData wsd, Point position)
        {
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
            Reset(_s, _gad, _configEl);
            ReInit();
        }
    }
}
