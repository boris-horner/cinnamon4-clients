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
using System.Diagnostics;
using System.Windows.Forms;

namespace C4GeneralGui.GuiElements
{
    public partial class StandardMessage : Form
    {
        private readonly int _fullHeight;
        private readonly int _collapsedHeight;
        private readonly string _helpUrl;

        public string ExitButtonKey { get; private set; }

        public StandardMessage(string message, Severity sev, Exception ex, string helpUrl)
        {
            InitializeComponent();
            _helpUrl = helpUrl;
            switch (sev)
            {
                case Severity.InfoMessage:
                    {
                        Text = "Information";   // TODO: i18n
                        pbInfo.Visible = true;
                        pbWarning.Visible = false;
                        pbError.Visible = false;
                        pbCritical.Visible = false;
                        break;
                    }

                case Severity.WarningMessage:
                    {
                        Text = "Warning";   // TODO: i18n
                        pbInfo.Visible = false;
                        pbWarning.Visible = true;
                        pbError.Visible = false;
                        pbCritical.Visible = false;
                        break;
                    }

                case Severity.ErrorMessage:
                    {
                        Text = "Error";   // TODO: i18n
                        pbInfo.Visible = false;
                        pbWarning.Visible = false;
                        pbError.Visible = true;
                        pbCritical.Visible = false;
                        break;
                    }

                case Severity.FatalMessage:
                    {
                        Text = "Fatal";   // TODO: i18n
                        pbInfo.Visible = false;
                        pbWarning.Visible = false;
                        pbError.Visible = false;
                        pbCritical.Visible = true;
                        break;
                    }
            }

            pException.Visible = ex is object;
            pHelp.Visible = _helpUrl is object;
            string msg = message;
            if (ex is object)
            {
                txtExType.Text = ex.GetType().ToString();
                txtExMessage.Text = ex.Message;
                txtStackTrace.Text = ex.StackTrace;
                if (ex.Data is object)
                {
                    if (ex.Data.Contains("server_message"))
                    {
                        msg += "\n" + ex.Data["server_message"].ToString();
                    }
                }
            }

            txtMessage.Lines = msg.Split('\n');
            _fullHeight = Height;
            _collapsedHeight = _fullHeight - 300;
            if (ex is object) Expand();
            else Collapse();
        }
        public void AddButton(string key, string label, bool cancelButton, bool acceptButton)
        {
            Panel p = new Panel();
            p.Dock = DockStyle.Left;
            p.Padding = new Padding(8);
            p.Width = 120;
            pButtons.Controls.Add(p);
            Button b = new Button();
            b.Tag = key;
            b.Text = label;
            b.Dock = DockStyle.Fill;
            p.Controls.Add(b);
            if (cancelButton) CancelButton = b;
            else if (acceptButton) AcceptButton = b;
            b.Click += new EventHandler(b_Click);
        }

        public enum Severity
        {
            InfoMessage,
            WarningMessage,
            ErrorMessage,
            FatalMessage
        }

        public static void ShowMessage(string message, Severity sev, System.Windows.Forms.IWin32Window owner = null, Exception ex = null, string helpUrl = null)
        {
            StandardMessage sm = new StandardMessage(message, sev, ex, helpUrl);
            sm.AddButton("close", "Close", false, true);
            if (owner is null)
            {
                sm.ShowDialog();
            }
            else
            {
                sm.ShowDialog(owner);
            }
        }

        private void cmdDetails_Click(object sender, EventArgs e)
        {
            if (pException.Visible)
            {
                // in expanded mode - going to collapse
                Collapse();
            }
            else
            {
                // in collapsed mode - going to expand
                Expand();
            }
        }

        //private void SetButtonVPos(Button b, bool collapsed)
        //{
        //    b.Location = new System.Drawing.Point(b.Location.X, _fullHeight - 70);
        //}

        private void Expand()
        {
            Height = _fullHeight;
            //SetButtonVPos(cmdDetails, false);
            //SetButtonVPos(cmdClose, false);
            //SetButtonVPos(cmdHelp, false);
            pException.Visible = true;
        }

        private void Collapse()
        {
            pException.Visible = false;
            Height = _collapsedHeight;
            //SetButtonVPos(cmdDetails, true);
            //SetButtonVPos(cmdClose, true);
            //SetButtonVPos(cmdHelp, true);
        }

        private void b_Click(object sender, EventArgs e)
        {
            ExitButtonKey = (string)(((Button)sender).Tag);
            Close();
        }

        private void CmdHelp_Click(object sender, EventArgs e)
        {
            Process.Start(_helpUrl);
        }
    }
}