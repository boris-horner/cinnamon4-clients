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
namespace CDCplusLib.Common.GUI
{
    public partial class ProgressNumericPopup : Form
    {
        public event CancelEventHandler Cancel;

        public delegate void CancelEventHandler();

        public ProgressNumericPopup(string title, string message)
        {
            InitializeComponent();
            Text = title;
            lblMessage.Text=message;
            Refresh();
        }
        public void SetMessage(string message)
        {
            lblMessage.Text = message;
            Refresh();
        }
        private void ProgressBarPopup_FormClosing(object sender, FormClosingEventArgs e)
        {
            // TODO: raise cancel event
        }
    }
}
