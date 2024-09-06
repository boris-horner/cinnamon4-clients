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
using C4ObjectApi.Operations.Import;
using C4ObjectApi.Repository;
using C4ServerConnector.Assets;

namespace CDCplusLib.Common.GUI
{
    public partial class ObjectWithoutContent : Form
    {
        private readonly string _checkoutPath;
        private readonly CmnSession _s;

        public ObjectWithoutContent(string checkoutPath, CmnSession s)
        {
            _checkoutPath = checkoutPath;
            _s = s;
            InitializeComponent();
            LocalizeGUI();
            InitComboBoxes();
            SetControlState();
        }

        private void LocalizeGUI()
        {
            Text = Properties.Resources.lblNewObjectWithoutContent;
            cmdCancel.Text = Properties.Resources.lblCancel;
            cmdOk.Text = Properties.Resources.lblOk;
            lblName.Text = Properties.Resources.lblName;
            lblObjType.Text = Properties.Resources.lblObjectType;
            lblLanguage.Text = Properties.Resources.lblLanguage;
        }

        private void SetControlState()
        {
            cmdOk.Enabled = txtName.Text.Length > 0;
        }

        private void InitComboBoxes()
        {
            cboObjectType.Items.Clear();
            XmlDocument ce = null;
            //if (!_s.User.IsSuperuser)
            //{
            //    // superusers always see unfiltered lifecycles
            //    try
            //    {
            //        ce = _s.GetConfigEntry("client.behaviour.config");
            //    }
            //    catch (Exception ex)
            //    {
            //        // nothing to do
            //    }
            //}

            // TODO: clean this up
            XmlElement otEl = ce is null ? null : (XmlElement)ce.SelectSingleNode("/config/config_sets/config_set[@name='_default']/filters/objecttype");
            Dictionary<long, C4Lifecycle> lcs = new Dictionary<long, C4Lifecycle>();
            ImportRules ir_ = new ImportRules(_s);
            foreach (C4ObjectType ot in GetFilteredObjTypes(_s, null, ir_).Values)
            {
                if (otEl is null || otEl.SelectSingleNode("no_assign[.='" + ot.Name + "']") is null)
                {
                    cboObjectType.Items.Add(ot);
                }
            }
            cboObjectType.Text = _s.SessionConfig.C4Sc.ObjectTypesByName["_default_objtype"].ToString();

            cboLanguage.Items.Clear();
            foreach (C4Language lang in _s.SessionConfig.C4Sc.LanguagesByName.Values)
                cboLanguage.Items.Add(lang);
            cboLanguage.SelectedIndex = 0;
        }
        private Dictionary<string, C4ObjectType> GetFilteredObjTypes(CmnSession s, C4Format fmt, ImportRules ir)
        {
            Dictionary<string, C4ObjectType> result = new Dictionary<string, C4ObjectType>();
            foreach (C4ObjectType ot in s.SessionConfig.C4Sc.ObjectTypesByName.Values)
            {
                if (ir.GetRulesResult(fmt, ot, null, null).Count == 0)
                    result.Add(ot.Name, ot);
            }
            return result;
        }
        public C4ObjectType ObjectType
        {
            get
            {
                return (C4ObjectType)cboObjectType.SelectedItem;
            }
            set
            {
                cboObjectType.SelectedItem = value;
                cboObjectType.Enabled = false;
            }
        }
        public C4Language Language
        {
            get
            {
                return (C4Language)cboLanguage.SelectedItem;
            }
            set
            {
                cboLanguage.Text = value.ToString();
            }
        }
        public string ObjectName
        {
            get
            {
                return txtName.Text;
            }
            set
            {
                txtName.Text = value;
            }
        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {
            SetControlState();
        }
    }
}
