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
using System;
using System.Windows.Forms;
using C4ObjectApi.Repository;
using CDCplusLib.Interfaces;

namespace CDCplus.Controls
{
    public partial class WindowEntry : UserControl
    {
        private CmnSession _s;
        bool _admin;

        public WindowEntry()
        {
            InitializeComponent();
        }
        public void InitWindowEntry(CmnSession s, ISessionWindow sw, string path, bool admin)
        {
            _s = s;
            SessionWindow = sw;
            Path = path;
            _admin = admin;
            pbAdmin.Visible = admin;
            pbDesktop.Visible = !admin;
        }

        public string Path
        {
            get { return lblPath.Text; }
            set
            {
                lblPath.Text = (value.Length > 80 ? value.Substring(0, 80) + " ..." : value);
            }
        }

        public ISessionWindow SessionWindow { get; private set; }


        private void cmdBringToFront_Click(object sender, EventArgs e)
        {
            SessionWindow.WindowTop();
        }

        private void cmdCloseWindow_Click(object sender, EventArgs e)
        {
            SessionWindow.CloseWindow();
        }
    }
}
