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
    public partial class Checkin : Form
    {
        private readonly string _checkoutPath;
        private readonly bool _checkoutFileDefaultOverwrite;
        private readonly bool _otherFileDefaultOverwrite;
        private readonly CmnObject _o;
        private readonly C4Permissions _perm;
        private readonly bool _activeEvents = false;

        public Checkin(CmnObject o, string checkoutPath, bool checkoutFileDefaultOverwrite, bool checkoutFileOtherOverwrite)
        {
            InitializeComponent();
            _o = o;
            _perm = _o.Permissions;
            if (!_perm.Object_Content_Write & !_perm.Object_Version)
                throw new ApplicationException("The user has no permission to check in.");
            _checkoutPath = checkoutPath;
            _checkoutFileDefaultOverwrite = checkoutFileDefaultOverwrite;
            _otherFileDefaultOverwrite = checkoutFileOtherOverwrite;
            LocalizeGUI();
            InitComboBoxes();
            SetControlState();
            _activeEvents = true;
        }
        
        private void LocalizeGUI()
        {
            Text = Properties.Resources.lblCheckin;
            cmdCancel.Text = Properties.Resources.lblCancel;
            cmdOk.Text = Properties.Resources.lblOk;
            chkNewVersion.Text = Properties.Resources.lblCreateNewVersion;
            chkUpdateReferences.Text = Properties.Resources.lblUpdateToLatestInHead;
            lblFormat.Text = Properties.Resources.lblFormat;
            optCheckinFromCheckoutPath.Text = Properties.Resources.lblCheckinFromCheckoutPath;
            optCheckinFromOtherFile.Text = Properties.Resources.lblCheckinFromOtherFile;

        }

        private void InitComboBoxes()
        {
            cboFormat.Items.Clear();
            C4Format selFmt = null;
            foreach (C4Format fmt in _o.Session.SessionConfig.C4Sc.FormatsByName.Values)
            {
                cboFormat.Items.Add(fmt);
                if (fmt == _o.Format)
                    selFmt = fmt;
            }
            if (selFmt is not null)
            {
                cboFormat.SelectedItem = selFmt;
            }
            else
            {
                cboFormat.SelectedIndex = 0;
            }
        }
        private void SetFormatCombo(string fn)
        {
            string ext = Path.GetExtension(fn).Substring(1);
            foreach (C4Format fmt in _o.Session.SessionConfig.C4Sc.FormatsByName.Values)
            {
                if ((fmt.Extension ?? "") == (ext ?? ""))
                {
                    cboFormat.SelectedItem = fmt;
                    break;
                }
            }
        }
        private void SetControlState()
        {
            if (_activeEvents)
            {
                txtCheckinFile.Enabled = optCheckinFromOtherFile.Checked;
                cboFormat.Enabled = optCheckinFromOtherFile.Checked;
                cmdSelectFile.Enabled = optCheckinFromOtherFile.Checked;
                cmdOk.Enabled = optCheckinFromCheckoutPath.Checked | File.Exists(txtCheckinFile.Text);
            }
            else if (_perm is not null)
            {
                if (optCheckinFromCheckoutPath.Checked)
                {
                    if (_checkoutFileDefaultOverwrite)
                    {
                        chkNewVersion.Checked = !_perm.Object_Content_Write;
                    }
                    else
                    {
                        chkNewVersion.Checked = _perm.Object_Version;
                    }
                }
                else if (_otherFileDefaultOverwrite)
                {
                    chkNewVersion.Checked = !_perm.Object_Content_Write;
                }
                else
                {
                    chkNewVersion.Checked = _perm.Object_Version;
                }

                // chkNewVersion.Checked = If((optCheckinFromCheckoutPath.Checked And checkoutFileDefaultOverwrite_) Or (optCheckinFromOtherFile.Checked And otherFileDefaultOverwrite_),
                // Not perm_.Version, perm_.Version)
                chkNewVersion.Enabled = _perm.Object_Version & _perm.Object_Content_Write;



            }
        }

        private void optCheckinFromCheckoutPath_CheckedChanged(object sender, EventArgs e)
        {
            SetControlState();
        }

        private void txtCheckinFile_TextChanged(object sender, EventArgs e)
        {
            SetControlState();
        }

        private void cmdSelectFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.InitialDirectory = Environment.CurrentDirectory;
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                optCheckinFromOtherFile.Checked = true;
                txtCheckinFile.Text = ofd.FileName;
                SetFormatCombo(ofd.FileName);
                Environment.CurrentDirectory = Path.GetDirectoryName(ofd.FileName);
            }
        }
        public bool CheckinFromCheckedOutFile
        {
            get
            {
                return optCheckinFromCheckoutPath.Checked;
            }
            set
            {
                optCheckinFromCheckoutPath.Checked = value;
            }
        }
        public string CheckinFile
        {
            get
            {
                return txtCheckinFile.Text;
            }
            set
            {
                txtCheckinFile.Text = value;
            }
        }
        public C4Format Format
        {
            get
            {
                return (C4Format)cboFormat.SelectedItem;
            }
            set
            {
                cboFormat.SelectedItem = value;
            }
        }
        public bool NewVersion
        {
            get
            {
                return chkNewVersion.Checked;
            }
            set
            {
                chkNewVersion.Checked = value;
            }
        }
        public CmnObject.ChildLinkUpdateBehaviour ChildUpdateBehavior
        {
            get
            {
                if (chkUpdateReferences.Checked)
                {
                    return CmnObject.ChildLinkUpdateBehaviour.UpdateToLatestInHead;
                }
                else
                {
                    return CmnObject.ChildLinkUpdateBehaviour.DontUpdate;
                }
            }
        }
    }
}
