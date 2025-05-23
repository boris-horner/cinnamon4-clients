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
using System.Xml;
using CDCplusLib.Interfaces;
using CDCplusLib.Common;
using C4ObjectApi.Interfaces;
using C4ObjectApi.Repository;
using C4ServerConnector;
using C4ServerConnector.Assets;
using C4GeneralGui.GuiElements;
using CDCplusLib.EventData;
using CDCplusLib.Common.GUI;

namespace CDCplusLib.TabControls
{
    public partial class ObjectPropertiesTabControl : UserControl, IGenericControl
    {
        //private bool _editable;
        private bool _readable;
        private bool _enableEvents;
        private CmnObject _o;
        private CmnSession _s;
        private bool _initCompleted;
        private GlobalApplicationData _gad;
        private XmlElement _configEl;
        private ToolTip _tt;
        //private XmlDocument _ce;    // TODO: client.behaviour.config config entry should be moved to general client configuration
        private XmlElement _configSet;
        private bool _lockObjType;
        private C4LifecycleState _selectedLcState;
        private Dictionary<long, IRepositoryNode> _dict;

        //public ISynchronizeInvoke EventSyncInvoke { get; set; }
        public event SessionWindowRequestEventHandler SessionWindowRequest;
        public event ListSelectionChangedEventHandler ListSelectionChanged;
        public event TreeSelectionChangedEventHandler TreeSelectionChanged;
        public event ContextMenuRequestEventHandler ContextMenuRequest;
        public event FunctionRequestEventHandler FunctionRequest;
        public event NodesModifiedEventHandler NodesModified;
        public event KeyPressedEventHandler KeyPressedEvent;
        public event RefreshRequestEventHandler RefreshRequest;

        public ObjectPropertiesTabControl()
        {
            InitializeComponent();
            _enableEvents = false;
            _o = null;
            _initCompleted = false;
            LocalizeGui();
            //SetControlsEnabledState(false);
            _enableEvents = true;
        }

        public bool HasSelection { get { return false; } }
        public Dictionary<long, IRepositoryNode> Selection { get { return null; } set { } }
        public bool AutoRefresh { get { return true; } }
        public bool ListContext { get { return true; } }

        private void LocalizeGui()
        {
            lblId.Text = Properties.Resources.lblId;
            lblFolderId.Text = Properties.Resources.lblFolderId;
            lblRootId.Text = Properties.Resources.lblRootId;
            lblFolderPath.Text = Properties.Resources.lblFolderPath;
            lblSize.Text = Properties.Resources.lblSize;
            lblName.Text = Properties.Resources.lblName;
            lblFormat.Text = Properties.Resources.lblFormat;
            lblLanguage.Text = Properties.Resources.lblLanguage;
            lblCreatedBy.Text = Properties.Resources.lblCreatedBy;
            lblCreationDate.Text = Properties.Resources.lblCreationDate;
            lblModifiedBy.Text = Properties.Resources.lblModifiedBy;
            lblModificationDate.Text = Properties.Resources.lblModificationDate;
            lblOwner.Text = Properties.Resources.lblOwner;
            lblObjType.Text = Properties.Resources.lblObjectType;
            lblLockedBy.Text = Properties.Resources.lblLockedBy;
            lblLifecycleAndState.Text = Properties.Resources.lblLifecycleAndState;
            lblChangedFlags.Text = Properties.Resources.lblChangedFlags;
            chkContentChanged.Text = Properties.Resources.lblContentChanged;
            chkMetadataChanged.Text = Properties.Resources.lblMetadataChanged;

            _tt = new ToolTip();
            _tt.SetToolTip(cmdSave, Properties.Resources.ttSave);
        }
        private void InitLists()
        {
            // TODO: convert this code from using client.behaviour.config to general client configuration
            if (!_initCompleted)
            {
                cboOwner.Items.Clear();
                foreach (C4User u in _s.SessionConfig.C4Sc.UsersById.Values) cboOwner.Items.Add(u);
                cboLanguage.Items.Clear();
                foreach (C4Language l in _s.SessionConfig.C4Sc.LanguagesByName.Values) cboLanguage.Items.Add(l);
                cboObjType.Items.Clear();
                foreach (C4ObjectType ot in _s.SessionConfig.C4Sc.ObjectTypesByName.Values) cboObjType.Items.Add(ot);
                _initCompleted = true;
            }
        }

        private void SetControlsEnabledState(bool dirty)
        {
            if (_enableEvents)
            {
                bool writable = _o == null ? false : (_o.Permissions.Object_Lock && (_o.Permissions.Node_Name_Write || _o.Permissions.Node_Type_Write || _o.Permissions.Node_Owner_Write || _o.Permissions.Node_Type_Write || _o.Permissions.Object_Language_Write || _o.Permissions.Object_LifecycleState_Write));
                IsDirty = dirty;
                cmdSave.Enabled = writable && IsDirty;
                txtName.Enabled = writable && _o.Permissions.Node_Name_Write;
                cboObjType.Enabled = writable && !_lockObjType && _o.Permissions.Node_Type_Write;
                cboOwner.Enabled = writable && _o.Permissions.Node_Owner_Write;
                cboLanguage.Enabled = writable && _o.Permissions.Object_Language_Write;
                cmdSelectLifecycle.Enabled = writable && _o.Permissions.Object_LifecycleState_Write;
                chkContentChanged.Enabled = !_s.User.ChangeTracking;
                chkMetadataChanged.Enabled = !_s.User.ChangeTracking;
            }
        }

        public string GetTabText()
        {
            return Properties.Resources.lblProperties;
        }
        private void ClearGui()
        {
            txtSize.Text = "0 " + Properties.Resources.lblBytes;
            txtId.Text = "";
            txtParentId.Text = "";
            txtRootId.Text = "";
            lnkParentPath.Text = "";
            txtName.Text = "";
            cboObjType.Text = "";
            cboOwner.Text = "";
            cboLanguage.Text = "";
            txtLockedBy.Text = "";
            txtFormat.Text = "";
            txtCreatedBy.Text = "";
            txtCreationDate.Text = "";
            txtModifiedBy.Text = "";
            txtModificationDate.Text = "";
            txtLifecycleAndState.Text = "";
            chkContentChanged.Checked = false;
            chkMetadataChanged.Checked = false;
            _selectedLcState = null;
        }

        private void SetGui()
        {
            txtSize.Text = _o.ContentSize.ToString("N0") + " " + Properties.Resources.lblBytes;
            txtId.Text = _o.Id.ToString();
            txtParentId.Text = _o.Parent.Id.ToString();
            txtRootId.Text = _o.RootId.ToString();
            lnkParentPath.Text = _o.Parent.FolderPath;
            txtName.Text = _o.Name;
            cboObjType.Text = _o.ObjectType.ToString();
            if (cboObjType.Text.Length > 0) _lockObjType = false;
            else
            {
                cboObjType.Items.Add(_o.ObjectType);
                _lockObjType = true;
            }
            cboOwner.Text = _o.Owner.ToString();
            cboLanguage.Text = _o.Language.ToString();

            _selectedLcState = _o.LifecycleState;
            UpdateLifecycleDisplay();

            if (_o.Locked == null) txtLockedBy.Text = "";
            else txtLockedBy.Text = _o.Locked.ToString();
            if (_o.Format == null) txtFormat.Text = Properties.Resources.lblNoFormat;
            else txtFormat.Text = _o.Format.ToString();

            txtCreatedBy.Text = _o.Creator.ToString();
            txtCreationDate.Text = _o.Created.ToString();
            txtModifiedBy.Text = _o.Modifier.ToString();
            txtModificationDate.Text = _o.Modified.ToString();
            chkContentChanged.Checked = _o.ContentChanged;
            chkMetadataChanged.Checked = _o.MetadataChanged;
        }
        public void Init(Dictionary<long, IRepositoryNode> dict)
        {
            _enableEvents = false;
            _dict = dict;
            _o = (CmnObject)((dict != null && dict.Count > 0) ? dict.Values.First() : null);
            IsDirty = false;
            if (_o == null)
            {
                //_editable = false;
                _readable = false;
                ClearGui();
            }
            else
            {
                // TODO: enable / disable metadata fields according to individual permissions
                _readable = (_o.Permissions.Node_Browse);

                if (_readable)
                {
                    SetGui();
                }
                else
                {
                    ClearGui();
                }
            }
            _enableEvents = true;
            SetControlsEnabledState(false);
        }
        public bool IsDirty { get; private set; }
        public bool IsValid(Dictionary<long, IRepositoryNode> dict, IGenericControl.ContextType ct)
        {
            if(dict.Count()!=1) return false;
            if(!(dict.Values.First() is CmnObject)) return false;
            if (ct != IGenericControl.ContextType.Object) return false;
            return true;
        }
        public void ReInit()
        {
            Init(_dict);
        }

        public void Reset(CmnSession s, GlobalApplicationData globalAppData, System.Xml.XmlElement configEl)
        {
            _s = s;
            _gad = globalAppData;
            _configEl = configEl;
            InitLists();
        }

        public void Save()
        {
            C4User origLock = _o.Locked;
            try
            {
                if (_o.Locked == null || _o.Locked == _o.Session.User)
                {
                    if (origLock == null) _o.Lock();

                    _s.CommandSession.UpdateObject(_o.Id,
                                                   null,
                                                   _o.Name == txtName.Text ? null : txtName.Text,
                                                   _o.Owner == (C4User)cboOwner.SelectedItem ? null : ((C4User)cboOwner.SelectedItem).Id,
                                                   null,
                                                   _o.ObjectType == (C4ObjectType)cboObjType.SelectedItem ? null : ((C4ObjectType)cboObjType.SelectedItem).Id,
                                                   _o.Language.Id == ((C4Language)cboLanguage.SelectedItem).Id ? null : ((C4Language)cboLanguage.SelectedItem).Id,
                                                   chkMetadataChanged.Checked != _o.MetadataChanged ? chkMetadataChanged.Checked : null,
                                                   chkContentChanged.Checked != _o.ContentChanged ? chkContentChanged.Checked : null);


                    C4LifecycleState oldState = _o.LifecycleState;
                    //_basicHookContainer.BeforeObjectLcChange(_o, oldState, _selectedLcState);
                    SetLifecycleState(_o.LifecycleState, _selectedLcState);
                    //_basicHookContainer.AfterObjectLcChange(_o, oldState, _selectedLcState);
                    //IsDirty = false;
                }
            }
            catch (ChangeTriggerException ex)
            {
                System.Diagnostics.Debug.Print("Change trigger exception on command " + ex.CommandFields["command"]);
                PreChangeTriggerActorsCollection pctas = (PreChangeTriggerActorsCollection)_o.Session.GetSessionExtension("pre_change_trigger_actors");
                if (pctas != null)
                {
                    if (pctas.ActorByCommand.ContainsKey(ex.CommandFields["command"]))
                    {
                        IPreChangeTriggerActor pcta = pctas.ActorByCommand[ex.CommandFields["command"]];
                        pcta.Process(ex.CommandFields);
                    }
                }
            }
            catch (ApplicationException ex)
            {
                StandardMessage.ShowMessage("An application exception has occurred", StandardMessage.Severity.ErrorMessage, this, ex);
            }
            catch (Exception ex)
            {
                StandardMessage.ShowMessage("An exception has occurred", StandardMessage.Severity.ErrorMessage, this, ex);
            }
            finally
            {
                if (origLock == null) _o.Unlock();
                SetControlsEnabledState(false);
                WindowSelectionData wsd = new WindowSelectionData();
                wsd.Selection.Add(_o.Id, _o);
                wsd.Modification.Add(_o.Id, _o);
                NodesModified?.Invoke(wsd);
            }
        }

        private void SetLifecycleState(C4LifecycleState oldValue, C4LifecycleState newValue)
        {
            _o.LifecycleState = _selectedLcState;

        }

        private void cmdSave_Click(object sender, EventArgs e)
        {
            Save();
        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {
            SetControlsEnabledState(true);
        }

        private void cboOwner_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetControlsEnabledState(true);
        }

        private void cboObjType_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetControlsEnabledState(true);
        }

        private void cboLanguage_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetControlsEnabledState(true);
        }

        private void lnkParentPath_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            WindowSelectionData wsd = new WindowSelectionData();
            wsd.RootNodeType = SessionTree.RootNodeTypes.Session;
            wsd.Selection.Add(_o.Id, _o);
            wsd.SelectedFolder = _o.Parent;
            SessionWindowRequest?.Invoke(wsd);
        }

        private void cmdSelectLifecycle_Click(object sender, EventArgs e)
        {
            CDCplusLib.Common.GUI.LifecycleStateSelector lcSel = new CDCplusLib.Common.GUI.LifecycleStateSelector();
            lcSel.Init(_o.Session, _o.LifecycleState);
            if (lcSel.ShowDialog() == DialogResult.OK)
            {
                _selectedLcState = lcSel.SelectedLifecycleState;
                UpdateLifecycleDisplay();
                SetControlsEnabledState(true);
            }
        }
        private void UpdateLifecycleDisplay()
        {
            if (_selectedLcState == null) txtLifecycleAndState.Text = Properties.Resources.lblNoLifecycle;
            else txtLifecycleAndState.Text = _selectedLcState.ToString() + " [" + _o.Session.SessionConfig.C4Sc.LifecyclesById[_selectedLcState.LifecycleId].ToString() + "]";
        }

        private void chkContentChanged_CheckedChanged(object sender, EventArgs e)
        {
            SetControlsEnabledState(true);
        }

        private void chkMetadataChanged_CheckedChanged(object sender, EventArgs e)
        {
            SetControlsEnabledState(true);
        }

        private void ObjectPropertiesTabControl_Paint(object sender, PaintEventArgs e)
        {
            pNameIcon.Width = pNameIcon.Height;
            pObjTypeIcon.Width = pObjTypeIcon.Height;
            pOwnerIcon.Width = pOwnerIcon.Height;
            pLanguageIcon.Width = pLanguageIcon.Height;
            pLcStateIcon.Width = pNameIcon.Height;
            pChangedFlagsIcon.Width = pNameIcon.Height;
            pFlags.Height = txtName.Height;     // set it to the reference height of another object
            pIdsIcon.Width = pIdsIcon.Height;
            pCreatedIcon.Width = pCreatedIcon.Height;
            pModifiedIcon.Width = pModifiedIcon.Height;
            pLockedIcon.Width = pLockedIcon.Height;
            pFormatIcon.Width = pFormatIcon.Height;
            pSizeIcon.Width = pSizeIcon.Height;
        }
    }
}
