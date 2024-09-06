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
using C4ObjectApi.Repository;
using C4ServerConnector.Assets;

namespace CDCplusLib.Common.GUI
{
    public partial class RelationTypeSelector : Form
    {
        private void OK_Button_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void Cancel_Button_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        public RelationTypeSelector(CmnSession s, XmlElement restrictRtsEl, C4RelationType defaultRt = null)
        {

            // This call is required by the Windows Form Designer.
            InitializeComponent();
            LocalizeGUI();

            // Add any initialization after the InitializeComponent() call.
            cboRelType.Items.Clear();
            foreach (C4RelationType rt in s.SessionConfig.C4Sc.RelationTypesByName.Values)
            {
                if (restrictRtsEl is null || restrictRtsEl.GetAttribute("superuser") == "all" || restrictRtsEl.SelectSingleNode("allow_type[text()='" + rt.Name + "']") is not null)
                    cboRelType.Items.Add(rt);
            }
            if (defaultRt is null)
            {
                cboRelType.SelectedIndex = 0;
            }
            else
            {
                cboRelType.SelectedItem = defaultRt;
            }

        }

        private void LocalizeGUI()
        {
            Text = Properties.Resources.lblSelectRelation;
            lblRelType.Text = Properties.Resources.lblRelationType;
            cmdOk.Text = Properties.Resources.lblOk;
            cmdCancel.Text = Properties.Resources.lblCancel;
        }

        public C4RelationType SelectedRelationType
        {
            get
            {
                return (C4RelationType)cboRelType.SelectedItem;
            }
        }
    }
}
