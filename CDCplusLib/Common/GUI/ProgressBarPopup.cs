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
    public partial class ProgressBarPopup : Form
    {
        public event CancelEventHandler Cancel;

        public delegate void CancelEventHandler();

        public ProgressBarPopup(string title, int minimum, int maximum, int value, string status)
        {
            InitializeComponent();
            Text = title;
            ResetProgressBar(minimum, maximum, value, status);
            Refresh();
        }

        public void ResetProgressBar(int minimum, int maximum, int value, string status)
        {
            pbProgress.Minimum = minimum;
            pbProgress.Maximum = maximum;
            pbProgress.Value = value;
            lblStatus.Text = status;
            Refresh();
        }

        public void UpdateProgressBar(int value, string status = null)
        {
            pbProgress.Value = value;
            if (status is object)
                lblStatus.Text = status;
            Refresh();
        }
        private void ProgressBarPopup_FormClosing(object sender, FormClosingEventArgs e)
        {
            // TODO: raise cancel event
        }
    }
}
