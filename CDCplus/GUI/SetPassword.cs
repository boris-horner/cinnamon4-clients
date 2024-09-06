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

namespace CDCplus.GUI
{
    public partial class SetPassword : Form
    {
        private const char PWDCHAR = '•';
        public SetPassword()
        {
            InitializeComponent();
            txtPassword.PasswordChar = PWDCHAR;
            lblPassword.Text = Resources.lblPassword;
            chkSaveCredentials.Text = Resources.lblSaveCredentials;
            cmdOk.Text = Resources.lblOk;
            cmdCancel.Text = Resources.lblCancel;
            Text = Resources.lblChangePassword;
        }
        public string Password { get { return txtPassword.Text; } }
        public bool SaveCredentials { get { return chkSaveCredentials.Checked; } }

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

        private void cmdSetPassword_MouseDown(object sender, MouseEventArgs e)
        {
            txtPassword.PasswordChar = '\0';
        }

        private void cmdSetPassword_MouseUp(object sender, MouseEventArgs e)
        {
            txtPassword.PasswordChar = PWDCHAR;
        }
    }
}
