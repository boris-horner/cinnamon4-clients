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
using C4ObjectApi.Repository;
using C4ServerConnector.Assets;

namespace CDCplusLib.Common.GUI
{
    public partial class LifecycleStateSelector : Form
    {
        public LifecycleStateSelector()
        {
            InitializeComponent();
        }
        public void Init(CmnSession s, C4LifecycleState lcs)
        {
            cLifecycleState.Init(s, true, lcs);
            LocalizeGui();
            ActivateControls();
        }

        public C4LifecycleState SelectedLifecycleState
        {
            get
            {
                return cLifecycleState.SelectedLifecycleState;
            }
        }

        private void cmdOk_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void LocalizeGui()
        {
            //this.Text = Properties.Resources.lblLifecycle;
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void ActivateControls()
        {
            //if (lcs_ is null)
            //{
            //    if (this.cLifeCycleState.SelectedLifecycleState is null)
            //    {
            //        this.cmdOk.Enabled = false;
            //    }
            //    else
            //    {
            //        this.cmdOk.Enabled = true;
            //    }
            //}
            //else if (this.cLifeCycleState.SelectedLifecycleState is null)
            //{
            //    this.cmdOk.Enabled = true;
            //}
            //else
            //{
            //    this.cmdOk.Enabled = lcs_ != this.cLifeCycleState.SelectedLifecycleState;
            //}
        }
    }
}
