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
using CDCplusLib.Interfaces;
using CDCplusLib.Common;
using CDCplusLib.Messages.SessionWindowRequestData;
using CDCplusLib.Messages;
using CDCplusLib.Common.GUI;
using C4ObjectApi.Interfaces;
using C4ObjectApi.Repository;
using C4ServerConnector;
using C4ServerConnector.Assets;
using C4GeneralGui.GuiElements;

namespace CDCplusLib.TabControls
{
    public partial class ListPropertiesTabControl : UserControl, IGenericControl
    {
        public ListPropertiesTabControl()
        {
            InitializeComponent();
            LocalizeGUI();
            _initCompleted = false;
        }

        private CmnSession _s;
        private bool _initCompleted;
        private CmnFolder _f;
        private ToolTip _tt;
        private C4LifecycleState _lcs;
        private Dictionary<long, IRepositoryNode> _dict;
        //private XmlElement _configEl;
        private C4LifecycleState _selectedLcState;
        private bool _lcDirty;
        //private XmlElement _configSet;

        public event IGenericControl.MessageSentEventHandler MessageSent;

        public delegate void MessageSentEventHandler(IClientMessage msg);

        public bool HasSelection { get { return false; } }
        public Dictionary<long, IRepositoryNode> Selection { get { return null; } set { } }
        public bool AutoRefresh { get { return true; } }
        public bool ListContext { get { return true; } }
        private void LocalizeGUI()
        {
            lblFolderId.Text = Properties.Resources.lblFolderId;
            lblFolderPath.Text = Properties.Resources.lblFolderPath;
            lblTotalSize.Text = Properties.Resources.lblTotalSize;
            lblFormat.Text = Properties.Resources.lblFormat;
            lblLanguage.Text = Properties.Resources.lblLanguage;
            lblOwner.Text = Properties.Resources.lblOwner;
            lblObjType.Text = Properties.Resources.lblObjectType;
            lblLifecycleAndState.Text = Properties.Resources.lblLifecycleAndState;
            lblCount.Text = Properties.Resources.lblCount;

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
        //private void InitConfig(string configSetName)
        //{
        //    if (_configSet == null)
        //    {
        //        try
        //        {
        //            XmlDocument ce = _s.GetConfigEntry("client.behaviour.config");
        //            _configSet = (XmlElement)ce.SelectSingleNode("/config/config_sets/config_set[@name='" + configSetName + "']");
        //            cboObjType.Items.Clear();
        //            cboObjType.Items.Add("");
        //            XmlElement otEl = ce == null ? null : (XmlElement)_configSet.SelectSingleNode("filters/objecttype");
        //            foreach (C4ObjectType ot in _s.SessionConfig.C4Sc.ObjectTypesByName.Values)
        //            {
        //                if (_s.User.IsSuperuser | (otEl == null || otEl.SelectSingleNode("no_assign[.='" + ot.Name + "']") == null))
        //                {
        //                    cboObjType.Items.Add(ot);
        //                }
        //            }

        //            cboOwner.Items.Clear();
        //            cboOwner.Items.Add("");
        //            foreach (C4User u in _s.SessionConfig.C4Sc.UsersById.Values)
        //                cboOwner.Items.Add(u);
        //            cboLanguage.Items.Clear();
        //            cboLanguage.Items.Add("");
        //            foreach (C4Language l in _s.SessionConfig.C4Sc.LanguagesByName.Values)
        //                cboLanguage.Items.Add(l);
        //        }
        //        catch (ApplicationException apEx)
        //        {
        //            throw apEx;
        //        }
        //        catch (Exception ex)
        //        {
        //            throw new ApplicationException("Missing or invalid configuration entry 'client.behaviour.config' - please contact your system administrator.");
        //        }
        //    }
        //}

        private void ActivateControls(bool dirty)
        {
            if (_initCompleted)
            {
                IsDirty = dirty;
                cmdSave.Enabled = dirty;

                // Dim writable As Boolean = False
                // If Not o_ Is Nothing Then
                // writable = (o_.Locked Is Nothing) And (o_.Permissions.WriteObjectSysmeta)
                // End If
                // cmdSave.Enabled = writable And dirty
                // cboObjType.Enabled = writable And Not lockObjType_
                // cboOwner.Enabled = writable
                // cboLanguage.Enabled = writable
            }
        }

        public void Reset(CmnSession s, GlobalApplicationData globalAppData, XmlElement configEl)
        {
            _s = s;
            InitLists();
        }

        public string GetTabText()
        {
            return Properties.Resources.lblProperties;
        }

        public void Init(Dictionary<long, IRepositoryNode> dict, IClientMessage msg)
        {
            _initCompleted = false;
            _dict = dict;
            _lcDirty = false;
            //InitConfig("_default");
            if (dict.Count > 0)
            {
                // control is only valid for pure CmnObject dicts with at least one value, therefore only these are considered here
                long totalSize = 0;
                C4ObjectType ot = null;
                bool otDiffs = false;
                C4User own = null;
                bool ownDiffs = false;
                C4Language lng = null;
                bool lngDiffs = false;
                _lcs = null;
                bool lcsHasValue = false;
                bool lcsDiffs = false;
                bool lcDiffs = false;
                C4Format fmt = null;
                bool fmtDiffs = false;
                bool fmtHasValue = false;
                _f = null;
                bool fDiffs = false;
                foreach (IRepositoryNode ow in dict.Values)
                {
                    CmnObject o = (CmnObject)ow;
                    totalSize += o.ContentSize;
                    if (!fDiffs)
                    {
                        // TODO: does this logic make sense? Isn't the second one always true?
                        if (_f == null)
                            _f = o.Parent;
                        if (_f != o.Parent)
                            fDiffs = true;
                    }

                    if (!otDiffs)
                    {
                        if (ot == null)
                            ot = o.ObjectType;
                        if (ot != o.ObjectType)
                            otDiffs = true;
                    }

                    if (!ownDiffs)
                    {
                        if (own == null)
                            own = o.Owner;
                        if (own != o.Owner)
                            ownDiffs = true;
                    }

                    if (!lngDiffs)
                    {
                        if (lng == null)
                            lng = o.Language;
                        if (lng != o.Language)
                            lngDiffs = true;
                    }

                    if (!lngDiffs)
                    {
                        if (lng == null)
                            lng = o.Language;
                        if (lng != o.Language)
                            lngDiffs = true;
                    }

                    if (!fmtDiffs)
                    {
                        if (fmtHasValue)
                        {
                            if (fmt == null)
                            {
                                if (o.Format is object)
                                    fmtDiffs = true;
                            }
                            else if (o.Format == null)
                            {
                                fmtDiffs = true;
                            }
                            else if (fmt != o.Format)
                                fmtDiffs = true;
                        }
                        else
                        {
                            fmt = o.Format;
                            fmtHasValue = true;
                        }
                    }

                    if (!lcsDiffs)
                    {
                        if (lcsHasValue)
                        {
                            if (_lcs == null)
                            {
                                if (o.LifecycleState is object)
                                {
                                    lcsDiffs = true;
                                    lcDiffs = true;
                                }
                            }
                            else if (o.LifecycleState == null)
                            {
                                lcsDiffs = true;
                                lcDiffs = true;
                            }
                            else if (_lcs != o.LifecycleState)
                            {
                                lcsDiffs = true;
                                if (_lcs.LifecycleId != o.LifecycleState.LifecycleId)
                                {
                                    lcDiffs = true;
                                }
                            }
                        }
                        else
                        {
                            _lcs = o.LifecycleState;
                            lcsHasValue = true;
                        }
                    }
                }

                txtTotalSize.Text = totalSize.ToString("N0") + " " + Properties.Resources.lblBytes;
                txtCount.Text = _dict.Count.ToString("N0");
                cboObjType.Text = otDiffs ? "" : ot.ToString();
                cboOwner.Text = ownDiffs ? "" : own.ToString();
                cboLanguage.Text = lngDiffs ? "" : lng.ToString();
                txtFormat.Text = fmtDiffs ? "" : fmt == null ? Properties.Resources.lblNoFormat : fmt.ToString();
                if (fDiffs)
                {
                    _f = null;
                    lnkParentPath.Text = "";
                    txtParentId.Text = "";
                }
                else
                {
                    lnkParentPath.Text = _f.FolderPath;
                    txtParentId.Text = _f.Id.ToString();
                }

                if (lcsDiffs)
                    _lcs = null;
                _selectedLcState = _lcs;
                UpdateLifecycleDisplay(lcsDiffs);
            }
            else
            {
                txtParentId.Text = "";
                lnkParentPath.Text = "";
                cboObjType.Text = "";
                cboOwner.Text = "";
                cboLanguage.Text = "";
                txtFormat.Text = "";
                txtLifecycleAndState.Text = "";
                _selectedLcState = null;
            }
            if (msg != null) MessageReceived(msg);
            _initCompleted = true;
            ActivateControls(false);
        }

        public bool IsValid(Dictionary<long, IRepositoryNode> dict, IGenericControl.ContextType ct)
        {
            if (ct != IGenericControl.ContextType.List) return false;
            if (dict == null)
                return false;
            if (dict.Count == 0)
                return false;
            foreach (IRepositoryNode ow in dict.Values)
            {
                if (ow is CmnFolder)
                {
                    return false;
                }
            }

            return true;
        }

        private void cmdSave_Click(object sender, EventArgs e)
        {
            Save();
        }

        public bool IsDirty { get; private set; }

        public void Save()
        {
            Cursor = Cursors.WaitCursor;
            ObjectsModifiedMessage msg = new ObjectsModifiedMessage();
            msg.ModificationType = ObjectsModifiedMessage.ModificationTypes.SystemMetadataChanged;
            // msg.Source = Me.InstanceName

            Dictionary<long, CmnObject> failed = new Dictionary<long, CmnObject>();
            C4User origLock = null;
            foreach (CmnObject o in _dict.Values)
            {
                try
                {
                    if (o.Locked == null || o.Locked == _s.User)
                    {
                        origLock = o.Locked;
                        if (origLock == null)
                            o.Lock();
                        if (o.Permissions.Node_Owner_Write && cboOwner.SelectedIndex > 0)
                        {
                            C4User selOwner = (C4User)cboOwner.SelectedItem;
                            if (o.Owner != selOwner)
								o.Session.CommandSession.UpdateObject(o.Id,
																	null,
																	null,
																	selOwner.Id);
                        }

                        if (o.Permissions.Node_Type_Write && cboObjType.SelectedIndex > 0)
                        {
                            C4ObjectType selObjType = (C4ObjectType)cboObjType.SelectedItem;
                            if (o.ObjectType != selObjType)
								o.Session.CommandSession.UpdateObject(o.Id,
																	null,
																	null,
																	null,
                                                                    null,
                                                                    selObjType.Id);
                        }

                        if (o.Permissions.Object_Language_Write && cboLanguage.SelectedIndex > 0)
                        {
                            C4Language selLanguage = (C4Language)cboLanguage.SelectedItem;
                            if (o.Language != selLanguage)
								o.Session.CommandSession.UpdateObject(o.Id,
																	null,
																	null,
																	null,
																	null,
																	null,
																	selLanguage.Id);
                        }

                        if (_lcDirty)
                        {
                            if (o.Permissions.Object_LifecycleState_Write)
                            {
                                o.LifecycleState=_selectedLcState;
                            }
                            else
                            {
                                failed.Add(o.Id, o);
                            }
                        }

                    }
                    else
                    {
                        failed.Add(o.Id, o);
                    }
                }
                catch (ChangeTriggerException ex)
                {
                    System.Diagnostics.Debug.Print("Change trigger exception on command " + ex.CommandFields["command"]);
                    PreChangeTriggerActorsCollection pctas = (PreChangeTriggerActorsCollection)o.Session.GetSessionExtension("pre_change_trigger_actors");
                    if (pctas != null)
                    {
                        if (pctas.ActorByCommand.ContainsKey(ex.CommandFields["command"]))
                        {
                            IPreChangeTriggerActor pcta = pctas.ActorByCommand[ex.CommandFields["command"]];
                            pcta.Process(ex.CommandFields);
                        }
                    }
                    failed.Add(o.Id, o);
                }
                catch (Exception ex)
                {
                    failed.Add(o.Id, o);
                }
                finally
                {
                    msg.ModifiedObjects.Add(o.Id, o);
                    if (origLock == null)
                        o.Unlock();
                }
            }

            _lcDirty = false;
            if (failed.Count > 0)
            {
                StandardMessage.ShowMessage(string.Format(Properties.Resources.msgFailureApplyingProperties, failed.Count.ToString()), StandardMessage.Severity.ErrorMessage, this, null, null);
            }

            ActivateControls(false);
            MessageSent?.Invoke(msg);
            Cursor = Cursors.Arrow;
        }

        public void ReInit()
        {
            Init(_dict, null);
        }

        private void lnkParentPath_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (_f is object)
            {
                BrowserSessionWindowRequestData msgData = new BrowserSessionWindowRequestData();
                msgData.Folder = _f;
                SessionWindowRequestMessage msg = new SessionWindowRequestMessage();
                msg.SessionWindowRequestData = msgData;
                // msg.Source = Me.InstanceName
                msg.Session = _f.Session;
                MessageSent?.Invoke(msg);
            }
        }
        private void UpdateLifecycleDisplay(bool diffs)
        {
            if (_lcs == null)
            {
                if (diffs)
                {
                    txtLifecycleAndState.Text = "";
                }
                else
                {
                    txtLifecycleAndState.Text = Properties.Resources.lblNoLifecycle;
                }
            }
            else
            {
                txtLifecycleAndState.Text = _lcs.Name + " [" + _s.SessionConfig.C4Sc.LifecyclesById[_lcs.LifecycleId].Name + "]";
            }
        }

        public void MessageReceived(IClientMessage msg)
        {
            // Nothing to do
        }

        private void CboObjType_SelectedIndexChanged(object sender, EventArgs e)
        {
            ActivateControls(true);
        }

        private void CboOwner_SelectedIndexChanged(object sender, EventArgs e)
        {
            ActivateControls(true);
        }

        private void CboLanguage_SelectedIndexChanged(object sender, EventArgs e)
        {
            ActivateControls(true);
        }

        private void CmdSelectLifecycle_Click(object sender, EventArgs e)
        {
            if (_s.SessionConfig.C4Sc.LifecyclesById.Count > 0)
            {
                LifecycleStateSelector lcSel = new LifecycleStateSelector();
                lcSel.Init(_s, _lcs);
                if (lcSel.ShowDialog() == DialogResult.OK)
                {
                    _selectedLcState = lcSel.SelectedLifecycleState;
                    _lcs = _selectedLcState;
                    _lcDirty = true;
                    UpdateLifecycleDisplay(false);
                    ActivateControls(true);
                }
            }
            else
            {
                StandardMessage.ShowMessage(Properties.Resources.msgNoLifecyclesDefined, StandardMessage.Severity.ErrorMessage, this, null, null);
                // TODO: better messaging
            }
        }
    }
}
