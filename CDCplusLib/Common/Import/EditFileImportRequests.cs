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
using C4GeneralGui.GuiElements;
using C4ObjectApi.Repository;
using C4ServerConnector.Assets;
using CDCplusLib.Interfaces;
using System.Diagnostics;
using System.Xml;

namespace CDCplusLib.Common.Import
{
    public partial class EditFileImportRequests : Form
    {
        private CmnFolder _f;
        GlobalApplicationData _gad;
        private XmlElement _selectObjectsConfigEl;
        private ListViewSort _lvwSort;
        private Dictionary<string, FileImportRequest> _FIRs;
        public EditFileImportRequests(CmnFolder f, Dictionary<string, FileImportRequest> firsByAbsFn, GlobalApplicationData gad, XmlElement selectObjectsConfigEl)
        {
            InitializeComponent();
            _f = f;
            _gad = gad;
            _selectObjectsConfigEl = selectObjectsConfigEl;
            _FIRs = firsByAbsFn;    // Initial display of control: use the list that was passed

            Text = Properties.Resources.lblEditFileImportRequests;
            colFilename.Text = Properties.Resources.lblFilename;
            colObjName.Text = Properties.Resources.lblObjectName;
            colObjType.Text = Properties.Resources.lblObjectType;
            colFormat.Text = Properties.Resources.lblFormat;
            colLanguage.Text = Properties.Resources.lblLanguage;
            colLcState.Text = Properties.Resources.lblLifecycleState;
            colTargetFolder.Text = Properties.Resources.lblTargetFolder;
            cmdImport.Text = Properties.Resources.lblImport;
            cmdCancel.Text = Properties.Resources.lblCancel;
            cmdReload.Text = Properties.Resources.lblReload;
        }

        public Dictionary<string, FileImportRequest> FIRs 
        { 
            get
            {
                return _FIRs;
            } 
            set 
            {
                // on subsequent calls, add and remove items to keep the settings in the items that persist
                HashSet<string> delFiles = new HashSet<string>();
                foreach(string fn in _FIRs.Keys)
                {
                    if (!value.ContainsKey(fn)) delFiles.Add(fn);
                }
                foreach (string fn in delFiles) _FIRs.Remove(fn);
                foreach(string fn in value.Keys)
                {
                    if (!_FIRs.ContainsKey(fn)) _FIRs.Add(fn,value[fn]);
                }
            } 
        }
        private void EditFileImportRequests_Shown(object sender, EventArgs e)
        {
            lvwFirs.Items.Clear();
            bool showMessage = false;
            foreach(FileImportRequest fir in FIRs.Values)
            {
                ListViewItem lvi = lvwFirs.Items.Add(fir.AbsoluteFilename);
                switch(fir.Status)
                {
                    case FileImportRequest.StatusValues.Failure:
                        lvi.ImageKey = "Failure";
                        showMessage = true;
                        break;
                    case FileImportRequest.StatusValues.OK:
                        lvi.ImageKey = "OK";
                        break;
                    case FileImportRequest.StatusValues.ReferencesNotFound:
                        lvi.ImageKey = "ReferencesNotFound";
                        showMessage = true;
                        break;
                    case FileImportRequest.StatusValues.UnknownFormat:
                        lvi.ImageKey = "UnknownFormat";
                        showMessage = true;
                        break;
                    default:
                        break;
                }
                if (fir.StatusMessage.Length > 0) lvi.ToolTipText = fir.StatusMessage;
                lvi.SubItems.Add(fir.ObjectName);
                lvi.SubItems.Add(fir.ObjectType.ToString());
                lvi.SubItems.Add(fir.Format == null ? "" : fir.Format.ToString());
                lvi.SubItems.Add(fir.Language.ToString());
                lvi.SubItems.Add(fir.LifecycleState==null?"": fir.LifecycleState.ToString());
                lvi.SubItems.Add(fir.TargetPath);
                lvi.Tag = fir;
            }
            if (_lvwSort == null) _lvwSort = new ListViewSort(lvwFirs, true);
            _lvwSort.Sort(0, SortOrder.Ascending);

            if (showMessage) StandardMessage.ShowMessage(Properties.Resources.msgImportErrorsReadToolTips, StandardMessage.Severity.WarningMessage,this);
        }
        private ToolStripItem AddContextMenuItem(ContextMenuStrip cmn, string itemText, string itemName, bool itemEnabled)
        {
            ToolStripItem result = cmn.Items.Add(itemText);
            result.Name = itemName;
            result.Enabled = itemEnabled;
            return result;
        }

        private ContextMenuStrip BuildContextMenu()
        {
            ContextMenuStrip result = new ContextMenuStrip();
            ToolStripItem tsi = null;
            if(lvwFirs.SelectedItems.Count>0)
            {
                tsi = AddContextMenuItem(result, Properties.Resources.lblEditSelectedNodeParas, "editparameters", true);
                tsi.Click += new EventHandler(HandleEditImportSettings);
                result.Items["editparameters"].Enabled = (lvwFirs.SelectedItems.Count > 0);
            }
            if (lvwFirs.SelectedItems.Count == 1)
            {
                tsi = AddContextMenuItem(result, Properties.Resources.lblEditSelectedNodeContent, "editcontent", true);
                tsi.Click += new EventHandler(HandleEditImportSettings);
                result.Items["editcontent"].Enabled = (lvwFirs.SelectedItems.Count > 0);
            }
            return result;
        }

        private void HandleEditImportSettings(object sender, System.EventArgs e)
        {
            switch (((ToolStripMenuItem)sender).Name)
            {
                case "editparameters":
                    EditParameters();
                    break;
                case "editcontent":
                    // open file in editor
                    ListViewItem lvi = lvwFirs.SelectedItems[0];
                    FileImportRequest fir = (FileImportRequest)lvi.Tag;
                    ProcessStartInfo psi = new ProcessStartInfo
                    {
                        FileName = fir.AbsoluteFilename,
                        UseShellExecute = true
                    };
                    Process.Start(psi);
                    break;
            }

        }

        private void EditParameters()
        {
            string objName = null;
            C4ObjectType objType = null;
            C4Format format = null;
            C4Language language = null;
            C4LifecycleState lcState = null;
            string targetPath = null;

            bool objNameSet = false;
            bool objTypeSet = false;
            bool formatSet = false;
            bool languageSet = false;
            bool lcStateSet = false;
            bool targetPathSet = false;

            foreach(ListViewItem lvi in lvwFirs.SelectedItems)
            {
                FileImportRequest fir = (FileImportRequest)lvi.Tag;
                if(!objNameSet)
                {
                    if (objName == null) objName = fir.ObjectName;
                    else if(objName!=fir.ObjectName)
                    {
                        objName = null;
                        objNameSet = true;
                    }
                }
                if (!objTypeSet)
                {
                    if (objType == null) objType = fir.ObjectType;
                    else if (objType != fir.ObjectType)
                    {
                        objType = null;
                        objTypeSet = true;
                    }
                }
                if (!formatSet)
                {
                    if (format == null) format = fir.Format;
                    else if (format != fir.Format)
                    {
                        format = null;
                        formatSet = true;
                    }
                }
                if (!languageSet)
                {
                    if (language == null) language = fir.Language;
                    else if (language != fir.Language)
                    {
                        language = null;
                        languageSet = true;
                    }
                }
                if (!lcStateSet)
                {
                    if (lcState == null) lcState = fir.LifecycleState;
                    else if (lcState != fir.LifecycleState)
                    {
                        lcState = null;
                        lcStateSet = true;
                    }
                }
                if (!targetPathSet)
                {
                    if (targetPath == null) targetPath = fir.TargetPath;
                    else if (targetPath != fir.TargetPath)
                    {
                        targetPath = null;
                        targetPathSet = true;
                    }
                }
            }

            EditSelectedFileImportRequests esFirs = new EditSelectedFileImportRequests(_f.Session, _f, _gad, _selectObjectsConfigEl);
            bool updateName = (lvwFirs.SelectedItems.Count == 1);
            esFirs.Init(updateName?objName:null, objType, format, language, lcState, targetPath);
            if(esFirs.ShowDialog()==DialogResult.OK)
            {
                foreach(ListViewItem lvi in lvwFirs.SelectedItems)
                {
                    FileImportRequest fir = (FileImportRequest)lvi.Tag;
                    if (updateName && esFirs.ObjectName != null) fir.ObjectName = esFirs.ObjectName;
                    if (esFirs.ObjectType != null) fir.ObjectType = esFirs.ObjectType;
                    if (esFirs.Format != null) fir.Format = esFirs.Format;
                    if (esFirs.Language != null) fir.Language = esFirs.Language;
                    if (esFirs.LifecycleState != null) fir.LifecycleState = esFirs.LifecycleState;
                    if (esFirs.TargetPath != null) fir.TargetPath = esFirs.TargetPath;
                    if(updateName) lvi.SubItems[1].Text = fir.ObjectName;
                    lvi.SubItems[2].Text = fir.ObjectType.ToString();
                    lvi.SubItems[3].Text = fir.Format.ToString();
                    lvi.SubItems[4].Text = fir.Language.ToString();
                    lvi.SubItems[5].Text = fir.LifecycleState == null ? "":fir.LifecycleState.ToString();
                    lvi.SubItems[6].Text = fir.TargetPath;
                }
                //ValidateImportParameters()
                //    ActivateControls()
            }

        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void cmdImport_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void lvwFirs_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                ListViewItem clickedItem = lvwFirs.HitTest(e.Location).Item;
                if (clickedItem == null)
                {
                    if(!clickedItem.Selected)
                    {
                        lvwFirs.SelectedItems.Clear();
                        clickedItem.Selected = true;
                    }
                }
                else
                {
                    //lvwFirs.SelectedItems.Clear();
                }
                BuildContextMenu().Show(lvwFirs, e.Location);
            }
        }

        private void cmdReload_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Yes;
            Close();
        }
    }
}
