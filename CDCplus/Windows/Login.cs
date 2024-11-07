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
using CDCplus.Properties;
using System;
using System.Windows.Forms;

namespace CDCplus.Windows
{
    public partial class Login : Form
    {
        private readonly bool _showSaveCredentials;
        public Login(string serverName, bool showSaveCredentials)
        {
            InitializeComponent();
            _showSaveCredentials = showSaveCredentials;
            chkSaveCredentials.Visible = _showSaveCredentials;
            Text = Resources.lblLogin + " " + serverName;
            cmdCancel.Text = Resources.lblCancel;
            cmdOk.Text = Resources.lblOk;
            lblPassword.Text = Resources.lblPassword;
            lblUser.Text = Resources.lblUsername;
            cmdOk.Enabled = false;
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

        public string Username { get { return txtUser.Text; } }
        public string Password { get { return txtPassword.Text; } }
        public bool SaveCredentials { get { return chkSaveCredentials.Checked; } }

        private void txtUser_TextChanged(object sender, EventArgs e)
        {
            cmdOk.Enabled = txtUser.Text.Length > 0 && txtPassword.Text.Length > 0;
        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {
            cmdOk.Enabled = txtUser.Text.Length > 0 && txtPassword.Text.Length > 0;
        }

        private void Login_Paint(object sender, PaintEventArgs e)
        {
            pLogo.Width = pLogo.Height / 3;
        }
    }
}
