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
using C4ObjectApi.Repository;

namespace CDCplusLib.Common.GUI
{
    public partial class SelectObjectLinkResolver : Form
    {
        CmnSession _s;
        public SelectObjectLinkResolver(CmnSession s, bool latest)
        {
            InitializeComponent();
            LocalizeGui();
            _s = s;
            optLatestVersion.Checked = latest;
        }
        private void LocalizeGui()
        {
            Text = Properties.Resources.lblVersionSelector;
            optLatestVersion.Text = Properties.Resources.lblLatestVersion;
            optFixedVersion.Text = Properties.Resources.lblFixedVersion;
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
    }
}
