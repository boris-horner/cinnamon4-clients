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
using System.Collections;
using C4ObjectApi.Repository;

namespace CDCplusLib.Common.GUI
{
    public partial class SelectVersion : Form
    {
        public SelectVersion(CmnObject o)
        {
            InitializeComponent();
            LocalizeGui();
            InitControls(o);
        }
        private void InitControls(CmnObject o)
        {
            Dictionary<long, CmnObject> allVersions = o.Session.SearchObjects("<ExactPointQuery type=\"long\" fieldName=\"root\" value=\"" + o.RootId.ToString() + "\"/>");
            lvwVersion.Items.Clear();
            SortedList l = new SortedList();
            foreach (CmnObject verO in allVersions.Values)
                l.Add(verO.Version, verO);
            foreach (CmnObject verO in l.Values)
            {
                ListViewItem lvi = lvwVersion.Items.Add(verO.Version);
                lvi.SubItems.Add(verO.Name);
                lvi.Tag = verO;
                lvi.Name = verO.Version;
            }
            if (o.Link.LatestVersion)
            {
                optLatestVersion.Checked = true;
            }
            else
            {
                lvwVersion.Items[o.Version].Selected = true;
                optFixedVersion.Checked = true;
            }
        }
        private void LocalizeGui()
        {
            Text = Properties.Resources.lblVersionSelector;
            optLatestVersion.Text = Properties.Resources.lblLatestVersion;
            optFixedVersion.Text = Properties.Resources.lblFixedVersion;
            colName.Text = Properties.Resources.lblName;
            colVersion.Text = Properties.Resources.lblVersion;
        }
        private void cmdOk_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        public CmnObject SelectedVersion
        {
            get
            {
                if (lvwVersion.SelectedItems.Count == 1)
                {
                    return (CmnObject)lvwVersion.SelectedItems[0].Tag;
                }
                else
                {
                    return null;
                }
            }
            set
            {
                lvwVersion.Items[value.Version].Selected = true;
            }
        }
        public bool FixedVersion
        {
            get
            {
                return optFixedVersion.Checked;
            }
            set
            {
                optFixedVersion.Checked = value;
            }
        }

        private void optLatestVersion_CheckedChanged(object sender, EventArgs e)
        {
            ActivateControls();
        }

        private void optFixedVersion_CheckedChanged(object sender, EventArgs e)
        {
            ActivateControls();
        }
        private void ActivateControls()
        {
            lvwVersion.Enabled = optFixedVersion.Checked;
            cmdOk.Enabled = optLatestVersion.Checked | lvwVersion.SelectedItems.Count == 1;
        }

        private void lvwVersion_SelectedIndexChanged(object sender, EventArgs e)
        {
            ActivateControls();
        }
    }
}
