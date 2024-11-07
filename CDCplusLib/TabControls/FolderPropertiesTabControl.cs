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
using System.Xml;
using CDCplusLib.Common;
using CDCplusLib.Interfaces;
using CDCplusLib.Messages;
using C4ObjectApi.Interfaces;
using C4ObjectApi.Repository;
using C4ObjectApi.Helpers;
using C4ServerConnector.Assets;

namespace CDCplusLib.TabControls
{
    public partial class FolderPropertiesTabControl : UserControl, IGenericControl
    {
        private bool _editable;
        private bool _readable;
        private bool _enableEvents;
        private CmnFolder _f;
        private GlobalApplicationData _gad;
        private XmlElement _configEl;
        private ToolTip _tt;

        //public ISynchronizeInvoke EventSyncInvoke { get; set; }
        public event IGenericControl.MessageSentEventHandler MessageSent;

        public FolderPropertiesTabControl()
        {
            InitializeComponent();
            _enableEvents = false;
            _f = null;
            _enableEvents = true;
            LocalizeGui();
            SetControlsEnabledState(false, false);
        }
        public bool HasSelection { get { return false; } }
        public Dictionary<long, IRepositoryNode> Selection { get { return null; } set { } }
        public bool AutoRefresh { get { return true; } }
        public bool ListContext { get { return true; } }

        private void LocalizeGui()
        {
            lblFolderId.Text = Properties.Resources.lblParentFolderId;
            lblFolderPath.Text = Properties.Resources.lblPath;
            lblFolderType.Text = Properties.Resources.lblFolderType;
            lblId.Text = Properties.Resources.lblId;
            lblName.Text = Properties.Resources.lblName;
            lblOwner.Text = Properties.Resources.lblOwner;
            _tt = new ToolTip();
            _tt.SetToolTip(cmdSave, Properties.Resources.ttSave);
        }
        private void SetControlsEnabledState(bool dirty, bool writable)
        {
            if (_enableEvents)
            {
                IsDirty = dirty;
                cmdSave.Enabled = IsDirty && writable;
            }
        }

        public string GetTabText()
        {
            return Properties.Resources.lblProperties;
        }

        private void InitFromConfig()
        {
            cboFolderType.Items.Clear();
            foreach (C4FolderType ft in _f.Session.SessionConfig.C4Sc.FolderTypesById.Values)
            {
                cboFolderType.Items.Add(ft);
            }
            cboFolderType.SelectedIndex = 0;
            cboOwner.Items.Clear();
            foreach (C4User u in _f.Session.SessionConfig.C4Sc.UsersById.Values)
            {
                cboOwner.Items.Add(u);
            }
            cboOwner.SelectedIndex = 0;
        }

        private void ClearGui()
        {
            cboFolderType.SelectedItem = null;
            cboOwner.SelectedItem = null;
            txtName.Text = "";
            lblId.Text = "";
            lblFolderId.Text = "";
            lnkParentPath.Text = "";
        }

        private void SetGui()
        {
            cboFolderType.SelectedItem = _f.FolderType;
            cboOwner.SelectedItem = _f.Owner;
            txtName.Text = _f.Name;
            txtId.Text = _f.Id.ToString();
            txtParentId.Text = _f.Parent == null ? "" : _f.Parent.Id.ToString();
            lnkParentPath.Text = _f.FolderPath;
        }
        public void Init(Dictionary<long, IRepositoryNode> dict, IClientMessage msg)
        {
            IsDirty = false;
            _f = DictionaryHelper.GetSingleFolder(dict);
            if (_f == null)
            {
                _editable = false;
                _readable = false;
                ClearGui();
            }
            else
            {
                InitFromConfig();
                // TODO: enable / disable metadata fields according to individual permissions
                _editable = (_f.Permissions.Node_Type_Write && _f.Permissions.Node_Name_Write && _f.Permissions.Node_Owner_Write);
                _readable = (_f.Permissions.Node_Browse);

                if (_readable)
                {
                    SetGui();
                }
                else
                {
                    ClearGui();
                }
            }
            if (msg != null) MessageReceived(msg);
            _enableEvents = true;
            SetControlsEnabledState(false, _editable);
        }

        public bool IsDirty { get; private set; }

        public bool IsValid(Dictionary<long, IRepositoryNode> dict, IGenericControl.ContextType ct)
        {
            if (ct != IGenericControl.ContextType.Folder) return false;
            return DictionaryHelper.GetSingleFolder(dict) != null;
        }
        public void MessageReceived(IClientMessage msg)
        {
            // nothing to do
        }


        public void ReInit()
        {
            //ClearMetadataGui();
            IsDirty = false;
        }

        public void Reset(CmnSession s, GlobalApplicationData globalAppData, System.Xml.XmlElement configEl)
        {
            _gad = globalAppData;
            _configEl = configEl;
        }

        public void Save()
        {
            _f.Session.CommandSession.UpdateFolder(_f.Id,
                                                   null,
                                                   (_f.Name != txtName.Text) ? txtName.Text : null,
                                                   (_f.Owner != (C4User)cboOwner.SelectedItem) ? ((C4User)cboOwner.SelectedItem).Id : null,
                                                   (_f.FolderType != (C4FolderType)cboFolderType.SelectedItem) ? ((C4FolderType)cboFolderType.SelectedItem).Id : null,
                                                   null);
            IsDirty = false;
            ObjectsModifiedMessage msg = new ObjectsModifiedMessage();
            msg.ModifiedObjects.Add(_f.Id, _f);
            MessageSent?.Invoke(msg);
            SetControlsEnabledState(false, _editable);
        }


        private void cmdSave_Click(object sender, EventArgs e)
        {
            Save();
        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {
            SetControlsEnabledState(true, _editable);
        }

        private void cboFolderType_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetControlsEnabledState(true, _editable);
        }

        private void cboOwner_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetControlsEnabledState(true, _editable);
        }

        private void FolderPropertiesTabControl_Paint(object sender, PaintEventArgs e)
        {
            pNameIcon.Width = pNameIcon.Height;
            pFolderTypeIcon.Width = pFolderTypeIcon.Height;
            pOwnerIcon.Width = pOwnerIcon.Height;
            pIdsIcon.Width = pIdsIcon.Height;
        }
    }
}
