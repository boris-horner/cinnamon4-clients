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
namespace CDCplusLib.Common.GUI
{
    public partial class SelectDeleteVersions : Form
    {
        public SelectDeleteVersions(bool allEnabled, bool selectedEnabled)
        {
            InitializeComponent();
            Text = Properties.Resources.lblDelete;
            cmdCancel.Text = Properties.Resources.lblCancel;
            cmdOk.Text = Properties.Resources.lblOk;
            optAll.Text = Properties.Resources.lblDeleteAllVersions;
            optSelectedOnly.Text = Properties.Resources.lblDeleteSelectedVersions;
            if(!allEnabled)
            {
                optAll.Checked = true;
                optSelectedOnly.Enabled = false;
            }
            if (!selectedEnabled)
            {
                optSelectedOnly.Checked = true;
                optAll.Enabled = false;
            }
            chkDeleteProtectedRelations.Text = Properties.Resources.lblDeleteProtectedRelations;
        }

        public bool AllVersions
        {
            get
            {
                return optAll.Checked;
            }
        }
        public bool DeleteProtectedRelations
        {
            get
            {
                return chkDeleteProtectedRelations.Checked;
            }
        }
    }
}
