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
using CDCplusLib.Common.GUI;
using System.Xml;
using C4ObjectApi.Repository;
using C4ServerConnector.Assets;
using CDCplusLib.Properties;

namespace CDCplusLib.Common.Import
{
    public partial class EditSelectedFileImportRequests : Form
    {
        private CmnSession _s;
        private CmnFolder _initialFolder;
        private GlobalApplicationData _gad;
        private XmlElement _selectObjectsConfigEl;
        public EditSelectedFileImportRequests(CmnSession s, CmnFolder initialFolder, GlobalApplicationData globalAppData, XmlElement selectObjectsConfigEl)
        {
            InitializeComponent();
            _s = s;
            _initialFolder = initialFolder;
            _gad = globalAppData;
            _selectObjectsConfigEl = selectObjectsConfigEl;
            LocalizeGUI();
            InitComboBoxes();
        }
        public void Init(string name, C4ObjectType objType, C4Format format, C4Language lang, C4LifecycleState lcState, string targetPath)
        {
            txtName.Text = name ?? "";
            txtName.ReadOnly = (name == null);
            cboObjectType.Text = objType is null ? "" : objType.ToString();
            cboFormat.Text = format is null ? "" : format.ToString();
            cboLanguage.Text = lang is null ? "" : lang.ToString();
            LifecycleState = lcState;
            txtLifecycleState.Text = LifecycleState==null?"":LifecycleState.ToString();
            txtTargetPath.Text = targetPath;
            ActivateControls();
        }

        private void LocalizeGUI()
        {
            Text = Resources.lblEditFileImportRequests;
            cmdCancel.Text = Resources.lblCancel;
            cmdOk.Text = Resources.lblOk;
            lblName.Text = Resources.lblName;
            lblObjType.Text = Resources.lblObjectType;
            lblLanguage.Text = Resources.lblLanguage;
            lblFormat.Text = Resources.lblFormat;
            lblTargetFolder.Text = Resources.lblTargetFolder;
        }

        private void ActivateControls()
        {
            cmdOk.Enabled = true;    // TODO: implement correct logic
        }

        private void InitComboBoxes()
        {
            cboObjectType.Items.Clear();
            foreach (C4ObjectType ot in _s.SessionConfig.C4Sc.ObjectTypesById.Values) cboObjectType.Items.Add(ot);
            cboFormat.Items.Clear();
            foreach (C4Format fmt in _s.SessionConfig.C4Sc.FormatsByName.Values) cboFormat.Items.Add(fmt);
            cboLanguage.Items.Clear();
            foreach (C4Language lang in _s.SessionConfig.C4Sc.LanguagesByName.Values) cboLanguage.Items.Add(lang);
        }

        private void optNoContent_CheckedChanged(object sender, EventArgs e)
        {
            ActivateControls();
        }

        private void txtImportFile_TextChanged(object sender, EventArgs e)
        {
            ActivateControls();
        }

        public C4ObjectType ObjectType
        {
            get
            {
                return (C4ObjectType)cboObjectType.SelectedItem;
            }

            set
            {
                cboObjectType.SelectedItem = value;
                cboObjectType.Enabled = false;
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
                cboFormat.Enabled = false;
            }
        }

        public C4Language Language
        {
            get
            {
                return (C4Language)cboLanguage.SelectedItem;
            }

            set
            {
                cboLanguage.Text = value.ToString();
            }
        }

        public C4LifecycleState LifecycleState { get; set; }
        public string ObjectName
        {
            get
            {
                return txtName.Text;
            }

            set
            {
                txtName.Text = value;
            }
        }

        public string TargetPath
        {
            get
            {
                return txtTargetPath.Text;
            }

            set
            {
                txtTargetPath.Text = value;
            }
        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {
            ActivateControls();
        }

        private void cmdBrowseTargetFolder_Click(object sender, EventArgs e)
        {
            SelectObjectsDialog sd = new SelectObjectsDialog(_s, _selectObjectsConfigEl, SelectObjectsDialog.SelectionModes.SelectFolder, _initialFolder, Properties.Resources.lblTargetFolder, _gad);
            CmnFolder targetFolder = _s.GetFolder(TargetPath);
            sd.Folder = targetFolder ?? _initialFolder;
            if (sd.ShowSelectorDialog() == DialogResult.OK)
            {
                targetFolder = (CmnFolder)sd.Selection.First().Value;
                txtTargetPath.Text = targetFolder==null ? "" : targetFolder.FolderPath;
            }
        }

        private void cmdSelectLifecycleState_Click(object sender, EventArgs e)
        {
            LifecycleStateSelector lcSel = new LifecycleStateSelector();
            lcSel.Init(_s, LifecycleState);
            if (lcSel.ShowDialog() == DialogResult.OK)
            {
                LifecycleState = lcSel.SelectedLifecycleState;
                UpdateLifecycleDisplay();
                //SetControlsEnabledState(true, _editable);
            }
        }
        private void UpdateLifecycleDisplay()
        {
            if (LifecycleState == null) txtLifecycleState.Text = Properties.Resources.lblNoLifecycle;
            else txtLifecycleState.Text = LifecycleState.ToString();
        }

        private void cmdOk_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
