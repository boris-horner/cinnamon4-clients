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
using System.Xml;
using CDCplusLib.Interfaces;
using CDCplusLib.DataModel;
using C4ObjectApi.Repository;
using C4GeneralGui.GuiElements;

namespace CDCplusLib.Common
{
    public partial class ListViewSettingsEditor : UserControl
    {
        private XmlElement _rldConfigEl;
        private XmlElement _tabConfigEl;
        private INodeDataProvider _ndp;
        private bool _settingsDirty;
        public string DefaultConfigLabel;
        private CmnSession _s;
        private string _userSettingsName;
        public string DefaultConfigName;


        public event EventHandler Apply;
        public ListViewSettingsEditor()
        {
            InitializeComponent();
        }

        public void Init(XmlElement rldConfigEl, INodeDataProvider ndp, CmnSession s, XmlElement tabConfigEl, string selectedConfigLabel, string userSettingsName)
        {
            _rldConfigEl = rldConfigEl;
            _ndp = ndp;
            _s = s;
            _userSettingsName = userSettingsName;
            cboAvailableConfig.Items.Clear();
            _tabConfigEl = tabConfigEl;
            DefaultConfigName = _tabConfigEl.SelectSingleNode("custom/default_configuration/@name").InnerText;

            //cboColumnSettings.Items.Clear();
            ColumnConfigurations = new Dictionary<string, RldColumnConfiguration>();
            foreach (XmlElement configurationEl in _rldConfigEl.SelectNodes("configurations/configuration"))
            {
                RldColumnConfiguration cc = new RldColumnConfiguration(configurationEl, _s, _ndp, true);
                cboAvailableConfig.Items.Add(cc);
                //cboColumnSettings.Items.Add(cc);
                ColumnConfigurations.Add(cc.Name, cc);
            }

            XmlElement usEl = _s.UserSettings.GetUserSetting(_userSettingsName);
            foreach (XmlElement configurationEl in usEl.SelectNodes("configurations/configuration"))
            {
                RldColumnConfiguration cc = new RldColumnConfiguration(configurationEl, _s, _ndp, false);
                cboAvailableConfig.Items.Add(cc);
                //cboColumnSettings.Items.Add(cc);
                ColumnConfigurations.Add(cc.Name, cc);
            }
            XmlAttribute userDefaultConfig = (XmlAttribute)usEl.SelectSingleNode("configurations/@default");
            if (userDefaultConfig != null)
            {
                if (ColumnConfigurations.ContainsKey(userDefaultConfig.InnerText))
                {
                    RldColumnConfiguration cc = ColumnConfigurations[userDefaultConfig.InnerText];
                    DefaultConfigName = cc.Name;
                    DefaultConfigLabel = cc.ToString();
                }
                else
                {
                    DefaultConfigLabel = ColumnConfigurations[DefaultConfigName].ToString();
                }
            }
            // TODO: load user config
            // add user configurations
            // get default from user config if it exists
            // set value

            lvceColumnSettings.Init(s, _ndp);
            //DefaultConfigLabel = selectedConfigLabel;
            cboAvailableConfig.SelectedItem = ColumnConfigurations[DefaultConfigName];
            //cboColumnSettings.SelectedItem = _columnConfigurations[configName];
            ActivateControls(false);

        }

        private void ActivateControls(bool dirty)
        {
            
            _settingsDirty = _settingsDirty || dirty;
            cmdAddConfig.Enabled = _settingsDirty && !ColumnConfigurations.ContainsKey(txtName.Text.Trim());
            txtName.Enabled = _settingsDirty;
            RldColumnConfiguration cc = (RldColumnConfiguration)cboAvailableConfig.SelectedItem;
            if (!_settingsDirty) txtName.Text = cboAvailableConfig.Text;
            cmdDeleteConfig.Enabled = cc != null && !cc.GlobalConfig;
            cmdApplyConfig.Enabled = true;
            cmdSetConfigAsDefault.Enabled = cc != null;
            cmdSaveConfig.Enabled = _settingsDirty && cc != null && !cc.GlobalConfig && cboAvailableConfig.Text == txtName.Text.Trim();
        }

        public Dictionary<string, RldColumnConfiguration> ColumnConfigurations { get; private set; }
        private void cboAvailableConfig_SelectedIndexChanged(object sender, EventArgs e)
        {
            RldColumnConfiguration selCc = (RldColumnConfiguration)cboAvailableConfig.SelectedItem;
            RldColumnConfiguration lookedUpCc = ColumnConfigurations[selCc.Name];
            lvceColumnSettings.ColumnConfiguration = ColumnConfigurations[((RldColumnConfiguration)cboAvailableConfig.SelectedItem).Name];
            txtName.Text = cboAvailableConfig.Text;
            lvceColumnSettings.Dirty = false;
            _settingsDirty = false;
            ActivateControls(false);
        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {
            ActivateControls(false);
        }

        private void cmdAddConfig_Click(object sender, EventArgs e)
        {
            Dictionary<string, RldColumnDefinition> columnDefinitions = lvceColumnSettings.GetCurrentColumnSettings();

            ColumnConfigurations.Add(txtName.Text.Trim(), new RldColumnConfiguration(txtName.Text.Trim(), columnDefinitions, _s, _ndp, false));
            SaveUserSettings();
            Init(_rldConfigEl, _ndp, _s, _tabConfigEl, DefaultConfigLabel, _userSettingsName);
            Apply?.Invoke(this, new EventArgs());
            ActivateControls(false);
        }

        public RldColumnConfiguration SelectedColumnConfiguration
        {
            get
            {
                return (RldColumnConfiguration)cboAvailableConfig.SelectedItem;
            }
        }

        private void SaveUserSettings()
        {
            try
            {
                // write user settings
                XmlElement usEl = _s.UserSettings.GetUserSetting(_userSettingsName);
                usEl.RemoveAll();
                usEl.SetAttribute("name", _userSettingsName);

                XmlElement configurationsEl = (XmlElement)usEl.AppendChild(usEl.OwnerDocument.CreateElement("configurations"));
                foreach (RldColumnConfiguration cc in ColumnConfigurations.Values)
                {
                    if(!cc.GlobalConfig)
                    {
                        XmlElement configurationEl = (XmlElement)configurationsEl.AppendChild(usEl.OwnerDocument.CreateElement("configuration"));
                        configurationEl.SetAttribute("name", cc.Name);
                        XmlElement columnsEl = (XmlElement)configurationEl.AppendChild(usEl.OwnerDocument.CreateElement("columns"));
                        foreach (RldColumnDefinition cd in cc.ColumnDefinitions.Values)
                        {
                            cd.AppendXmlSerialization(columnsEl);
                        }
                    }
                }
                configurationsEl.SetAttribute("default", DefaultConfigName);
                _s.UserSettings.SetUserSetting(_userSettingsName, usEl, true);
                _settingsDirty = false;
            }
            catch(Exception ex)
            {
                StandardMessage.ShowMessage("Failure writing user settings to metaset 'user_settings'.", StandardMessage.Severity.ErrorMessage, this, ex);
            }
        }

        private void cmdSaveConfig_Click(object sender, EventArgs e)
        {
            Dictionary<string, RldColumnDefinition> columnDefinitions = lvceColumnSettings.GetCurrentColumnSettings();

            ColumnConfigurations[((RldColumnConfiguration)cboAvailableConfig.SelectedItem).Name] = new RldColumnConfiguration(txtName.Text.Trim(), columnDefinitions, _s, _ndp, false);
            SaveUserSettings();
            Init(_rldConfigEl, _ndp, _s, _tabConfigEl, DefaultConfigLabel, _userSettingsName);
            Apply?.Invoke(this, new EventArgs());
            ActivateControls(false);
        }

        private void cmdDeleteConfig_Click(object sender, EventArgs e)
        {
            ColumnConfigurations.Remove(((RldColumnConfiguration)cboAvailableConfig.SelectedItem).Name);
            SaveUserSettings();
            Init(_rldConfigEl, _ndp, _s, _tabConfigEl, DefaultConfigLabel, _userSettingsName);
            Apply?.Invoke(this, new EventArgs());
            ActivateControls(false);
        }

        private void cmdApplyConfig_Click(object sender, EventArgs e)
        {
            Apply?.Invoke(this, new EventArgs());
        }

        private void cmdSetConfigAsDefault_Click(object sender, EventArgs e)
        {
            DefaultConfigName=((RldColumnConfiguration)cboAvailableConfig.SelectedItem).Name;
            SaveUserSettings();
            Init(_rldConfigEl, _ndp, _s, _tabConfigEl, DefaultConfigLabel, _userSettingsName);
        }

        private void lvceColumnSettings_Changed(object sender, EventArgs e)
        {
            ActivateControls(true);
        }
    }
}
