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
using System.ComponentModel;
using System.Xml;
using CDCplusLib.Interfaces;
using CDCplusLib.Common;
using CDCplusLib.Common.GUI;
using CDCplusLib.DataModel;
using C4ObjectApi.Interfaces;
using C4ObjectApi.Repository;
using C4ObjectApi.Exceptions;
using C4ObjectApi.Helpers;
using C4ServerConnector.Assets;
using C4GeneralGui.GuiElements;
using CDCplusLib.Properties;
using CDCplusLib.EventData;

namespace CDCplusLib.TabControls
{
    public partial class RelationsTabControl : UserControl, IGenericControl
    {
        private bool _editable;
        private bool _readable;
        private bool _allowRelationEditOnLockedObjects;
        private bool _enableEvents;
        private CmnObject _o;
        private Dictionary<long, IRepositoryNode> _dict;
        private GlobalApplicationData _gad;
        private XmlElement _configEl;
        private IIconService _iconService;
        private ListViewSort _lvwParentsSort, _lvwChildrenSort;
        private readonly HashSet<long> _delRelIds;
        private Dictionary<long, C4Relation> _childRels;
        private Dictionary<long, C4Relation> _parentRels;

        public ISynchronizeInvoke EventSyncInvoke { get; set; }
        public event SessionWindowRequestEventHandler SessionWindowRequest;
        public event ListSelectionChangedEventHandler ListSelectionChanged;
        public event TreeSelectionChangedEventHandler TreeSelectionChanged;
        public event ContextMenuRequestEventHandler ContextMenuRequest;
        public event FunctionRequestEventHandler FunctionRequest;
        public event NodesModifiedEventHandler NodesModified;
        public event KeyPressedEventHandler KeyPressedEvent;
        public event RefreshRequestEventHandler RefreshRequest;

        public RelationsTabControl()
        {
            InitializeComponent();
            _enableEvents = false;
            //lblAuthor.Text = Resources.lblAuthor;
            _o = null;
            _dict = null;
            _delRelIds = new HashSet<long>();

            _enableEvents = true;
            lblParents.Text = Properties.Resources.lblParents;
            lblChildren.Text = Properties.Resources.lblChildren;
            ToolTip tt = new ToolTip();
            tt.SetToolTip(cmdSave, Properties.Resources.lblSave);
            SetControlsEnabledState(false);
        }
        public bool HasSelection { get { return false; } }
        public Dictionary<long, IRepositoryNode> Selection { get { return null; } set { } }
        public bool AutoRefresh { get { return true; } }
        public bool ListContext { get { return true; } }

        private void SetControlsEnabledState(bool dirty)
        {
            if (_enableEvents)
            {
                IsDirty = dirty;
                ////lvwComments: enable / disable context menu / add buttons 
                //cmdAddComment.Enabled = _editable && txtAddComment.Text.Length>0;

                cmdSave.Enabled = IsDirty;
            }
        }

        public string GetTabText()
        {
            return Properties.Resources.lblRelations;
        }
        private void ClearGui()
        {
            lvwParents.Items.Clear();
            lvwChildren.Items.Clear();
        }

        private void SetGui()
        {
            lvwParents.Items.Clear();
            lvwChildren.Items.Clear();

            Dictionary<long, C4Relation> allRels = _o.Session.GetRelations(true, null, _o.Id, _o.Id);

            _childRels = new Dictionary<long, C4Relation>();
            _parentRels = new Dictionary<long, C4Relation>();

            foreach(long id in allRels.Keys)
            {
                C4Relation rel = allRels[id];
                if (rel.LeftId == _o.Id) _childRels.Add(id, rel);
                else _parentRels.Add(id, rel);
            }

            HashSet<long> refIds=new HashSet<long>();
            foreach (C4Relation rel in _childRels.Values)
            {
                if (!refIds.Contains(rel.RightId)) refIds.Add(rel.RightId);
            }
            foreach (C4Relation rel in _parentRels.Values)
            {
                if (!refIds.Contains(rel.LeftId)) refIds.Add(rel.LeftId);
            }
            Dictionary<long, CmnObject> refObjs = _o.Session.GetObjects(refIds, false);
            AddRelationsToLv(ref _childRels, ref _parentRels, ref refObjs);
        }

        private void AddRelationToLv(ListView lvw, C4RelationType rt, CmnObject relO, object tag)
        {
            bool first = true;
            ListViewItem lvi = null;
            foreach (XmlElement colEl in _configEl.SelectNodes("custom/columns/list_view_column"))
            {
                string tp = colEl.GetAttribute("type");
                string value = null;
                    
                if (colEl.HasAttribute("class") && colEl.GetAttribute("class") == "custom")
                {
                    // process custom attribute
                    if (relO.Summary == null) value = "";
                    else 
                    {
                        XmlNode n = relO.Summary.SelectSingleNode("/summary/fields/field[@name='" + tp + "']");
                        if (n == null) value = "";
                        else
                        {
                            if (colEl.HasAttribute("length-limit"))
                            {
                                int ll = int.Parse(colEl.GetAttribute("length-limit"));
                                if (ll > 0 && n.InnerText.Length > ll) value = n.InnerText.Substring(0, ll);
                                else value = n.InnerText;
                            }
                            else value = n.InnerText;
                        }
                    }
                }
                else
                {
                    // process standard attribute
                    switch (tp)
                    {
                        case "relationtype": value = rt.ToString(); break;
                        case "name": value = relO.Name; break;
                        case "id": value = relO.Id.ToString(); break;
                        case "format": value = (relO.Format == null ? "" : relO.Format.ToString()); break;
                        case "type": value = relO.ObjectType.ToString(); break;
                        case "version": value = relO.Version; break;
                        case "size": value = relO.ContentSize.ToString(); break;
                        case "owner": value = relO.Owner.ToString(); break;
                        case "creator": value = relO.Creator.ToString(); break;
                        case "modifier": value = relO.Modifier.ToString(); break;
                        case "locked": value = (relO.Locked == null ? "" : relO.Locked.ToString()); break;
                        case "lifecycle": value = (relO.LifecycleState == null ? "" : relO.LifecycleState.ToString()); break;
                        case "path": value = relO.Parent.FolderPath; break;
                    }
                }
                if (first)
                {
                    // first value: add ListViewItem
                    first = false;
                    lvi = lvw.Items.Add(value, _iconService.GetIconKey(relO));
                    lvi.Tag = tag;
                    XmlElement htEl=(XmlElement)_configEl.SelectSingleNode("custom/highlight_types/highlight_type[@sysname='" + rt.Name + "']");
                    if (htEl != null)
                    {
                        lvi.UseItemStyleForSubItems = false;
                        if (htEl.HasAttribute("backcolor")) lvi.BackColor = Color.FromArgb(Int32.Parse(htEl.GetAttribute("backcolor"), System.Globalization.NumberStyles.HexNumber));
                        if (htEl.HasAttribute("color")) lvi.ForeColor = Color.FromArgb(Int32.Parse(htEl.GetAttribute("color"), System.Globalization.NumberStyles.HexNumber));
                    }
                }
                else
                {
                    // subsequent value: add cell
                    ListViewItem.ListViewSubItem lvsi = lvi.SubItems.Add(value);
                    //lvsi.BackColor = Color.FromArgb(255, 255, 255);
                }
            }

        }

        private void AddRelationsToLv(ref Dictionary<long, C4Relation> childRels, ref Dictionary<long, C4Relation> parentRels, ref Dictionary<long, CmnObject> refObjs)
        {
            foreach(C4Relation rel in childRels.Values) 
            {
                CmnObject rightO = refObjs[rel.RightId];
                CmnObject leftO = _o;
                RelationDescriptor rd = new RelationDescriptor(leftO, rightO, leftO.Session.SessionConfig.C4Sc.RelationTypesById[rel.TypeId], rel.Metadata, rel);
                AddRelationToLv(lvwChildren, leftO.Session.SessionConfig.C4Sc.RelationTypesById[rel.TypeId], rightO, rd);
            }
            foreach (C4Relation rel in parentRels.Values)
            {
                CmnObject leftO = refObjs[rel.LeftId];
                CmnObject rightO = _o;
                RelationDescriptor rd = new RelationDescriptor(leftO, rightO, leftO.Session.SessionConfig.C4Sc.RelationTypesById[rel.TypeId], rel.Metadata, rel);
                AddRelationToLv(lvwParents, leftO.Session.SessionConfig.C4Sc.RelationTypesById[rel.TypeId], leftO, rd);
            }
            _lvwParentsSort.ReSort();
            _lvwChildrenSort.ReSort();
        }


        private ContextMenuStrip BuildContextMenu(ListView lvw)
        {
            ContextMenuStrip result = new ContextMenuStrip();
            bool parents=(lvw==lvwParents);
            bool addEnabled = _editable && ((parents && _o.Permissions.Relation_Parent_Add) || (!parents && _o.Permissions.Relation_Child_Add));
            bool removeEnabled = _editable && ((parents && _o.Permissions.Relation_Parent_Remove) || (!parents && _o.Permissions.Relation_Child_Remove));
            /*if (!parents)*/
            AppendToolStripItem(ref result, lvw, Properties.Resources.mnuAddNewRelation, "add", lvw.SelectedItems.Count == 0 && addEnabled);
            /*if (!parents)*/ AppendToolStripItem(ref result, lvw, Properties.Resources.mnuUpdateRelation, "update", lvw.SelectedItems.Count > 0 && addEnabled && removeEnabled);
            ///*if (!parents)*/ AppendToolStripItem(ref result, lvw, Resources.EnterprisePackage.mnuChooseRelationVersion, "choose_version", lvw.SelectedItems.Count == 1);
            /*if (!parents)*/ AppendToolStripItem(ref result, lvw, Properties.Resources.mnuReplaceRelation, "replace", lvw.SelectedItems.Count == 1 && addEnabled && removeEnabled/* && _o.Locked==null*/);
            /*if (!parents)*/ AppendToolStripItem(ref result, lvw, Properties.Resources.mnuRemoveRelation, "remove", lvw.SelectedItems.Count > 0 && removeEnabled);
            /*if (!parents)*/ result.Items.Add(new ToolStripSeparator());
            AppendToolStripItem(ref result, lvw, Properties.Resources.mnuOpenInNewWindow, "open", lvw.SelectedItems.Count == 1);
            bool enableFileMenuItems = !parents && lvw.SelectedItems.Count == 1 && _o.Locked == _o.Session.User;
            result.Items.Add(new ToolStripSeparator());
            // determine whether single object is checked out
            AppendToolStripItem(ref result, lvw, Properties.Resources.mnuCopyFilename, "copy_filename", enableFileMenuItems);
            AppendToolStripItem(ref result, lvw, Properties.Resources.mnuCopyPathFilename, "copy_path_filename", enableFileMenuItems);

            return result;
        }

        private ToolStripItem AppendToolStripItem(ref ContextMenuStrip cm, ListView lvw, string text, string name, bool enabled)
        {
            ToolStripItem result = cm.Items.Add(text);
            result.Name = name;
            result.Enabled = enabled;
            result.Click += HandleContextMenuClick;
            result.Tag = lvw;
            return result;
        }

        private void HandleContextMenuClick(object sender, System.EventArgs e)
        {
            ToolStripMenuItem tsmi=(ToolStripMenuItem)sender;
            try
            {
                switch (tsmi.Name)
                {
                    case "add": AddRelation((ListView)(tsmi.Tag)); break;
                    case "update": UpdateRelations((ListView)(tsmi.Tag)); break;
                    //case "choose_version": ChooseRelationVersion((ListView)(tsmi.Tag)); break;
                    case "replace": ReplaceRelation((ListView)(tsmi.Tag)); break;
                    case "remove": DeleteRelations((ListView)(tsmi.Tag)); break;
                    case "open": OpenRelation((ListView)(tsmi.Tag)); break;
                    case "copy_filename": CopyRelationFilename((ListView)(tsmi.Tag)); break;
                    case "copy_path_filename": CopyRelationFilenamePath((ListView)(tsmi.Tag)); break;
                }
            }
            catch (Exception ex)
            {
                StandardMessage.ShowMessage("Caught Exception", StandardMessage.Severity.ErrorMessage, this, ex);
            }

        }

        private void AddRelation(ListView lvw)
        {
            bool parents = (lvw == lvwParents);
            SelectObjectsDialog sd = new SelectObjectsDialog(_o.Session,
                                                             (XmlElement)_configEl.SelectSingleNode("custom/object_selector/custom"), 
                                                             SelectObjectsDialog.SelectionModes.SelectMultiObjects, 
                                                             _o.Parent,
                                                             Properties.Resources.lblChooseTargets, 
                                                             _gad);
            if (sd.ShowSelectorDialog() == DialogResult.OK)
            {
                // TODO: select relation type
                XmlElement defaultRtEl=(XmlElement)_configEl.SelectSingleNode("custom/default_relation_type");
                C4RelationType defaultRt =null;
                if(defaultRtEl!=null) 
                {
                    if(_o.Session.SessionConfig.C4Sc.RelationTypesByName.ContainsKey(defaultRtEl.InnerText))  defaultRt=_o.Session.SessionConfig.C4Sc.RelationTypesByName[defaultRtEl.InnerText];
                }
                XmlElement restrictRtsEl = (XmlElement)_configEl.SelectSingleNode("custom/restrict_new_relation_types");

                if(restrictRtsEl!=null && restrictRtsEl.GetAttribute("superuser")!="all" && restrictRtsEl.SelectNodes("allow_type").Count == 1)
                {
                    // don't show relation type selector, assign single selected type
                    Cursor = Cursors.WaitCursor;
                    C4RelationType rt = _o.Session.SessionConfig.C4Sc.RelationTypesByName[restrictRtsEl.SelectSingleNode("allow_type").InnerText];
                    foreach (IRepositoryNode ow in sd.Selection.Values)
                    {
                        RelationDescriptor rd = null;
                        CmnObject relO = (CmnObject)ow;
                        if (relO == _o) StandardMessage.ShowMessage(Properties.Resources.msgSelfReferencesNotAllowed, StandardMessage.Severity.ErrorMessage, null, null,
                                               _o.Session.GetHelpUrl(DataModelErrorCodes.INVALID_SELF_REFERENCE));
                        else
                        {
                            if (parents) rd = new RelationDescriptor(relO, _o, rt, null);
                            else rd = new RelationDescriptor(_o, relO, rt, null);
                            if (!lvw.Items.ContainsKey(rd.Key)) AddRelationToLv(lvw, rt, relO, rd);
                        }
                    }
                    if (parents) _lvwParentsSort.ReSort();
                    else _lvwChildrenSort.ReSort();
                    Cursor = null;
                    SetControlsEnabledState(true);
                }
                else
                {
                    // show relation type selector
                    HashSet<SimpleDisplayItem> availableRelationTypes = new HashSet<SimpleDisplayItem>();
                    foreach (C4RelationType rt in _o.Session.SessionConfig.C4Sc.RelationTypesByName.Values)
                    {
                        if (restrictRtsEl is null || restrictRtsEl.GetAttribute("superuser") == "all" || restrictRtsEl.SelectSingleNode("allow_type[text()='" + rt.Name + "']") is not null)
                            availableRelationTypes.Add(new SimpleDisplayItem(rt.Name, rt.ToString(), rt));
                    }
                    string selRtLabel = defaultRt == null ? null : defaultRt.ToString();
                    EditListValue elv = new EditListValue(Resources.lblSelectRelationType,Resources.lblRelationType, selRtLabel, availableRelationTypes);
                    if (elv.ShowDialog() == DialogResult.OK)
                    {
                        Cursor = Cursors.WaitCursor;
                        foreach (IRepositoryNode ow in sd.Selection.Values)
                        {
                            RelationDescriptor rd = null;
                            CmnObject relO=(CmnObject)ow;
                            if (relO == _o) StandardMessage.ShowMessage(Properties.Resources.msgSelfReferencesNotAllowed, StandardMessage.Severity.ErrorMessage, null, null,
                                                   _o.Session.GetHelpUrl(DataModelErrorCodes.INVALID_SELF_REFERENCE));
                            else
                            {
                                C4RelationType selRt = elv.Value.Tag as C4RelationType;
                                if (parents) rd = new RelationDescriptor(relO, _o, selRt, null);
                                else rd = new RelationDescriptor(_o, relO, selRt, null);
                                if (!lvw.Items.ContainsKey(rd.Key)) AddRelationToLv(lvw, selRt, relO, rd);
                            }
                        }
                        if (parents) _lvwParentsSort.ReSort();
                        else _lvwChildrenSort.ReSort();
                        Cursor = null;
                        SetControlsEnabledState(true);
                    }
                }
            }
        }

        private void UpdateRelations(ListView lvw)
        {
            Cursor = Cursors.WaitCursor;

            bool parents = (lvw == lvwParents);
            List<ListViewItem> delItems = new List<ListViewItem>();

            // pass 1: collect root ids
            List<long> rootIds = new List<long>();
            foreach (ListViewItem lvi in lvw.SelectedItems)
            {
                delItems.Add(lvi);
                RelationDescriptor rd = (RelationDescriptor)lvi.Tag;
                if (rd.Relation != null && !_delRelIds.Contains((long)rd.Relation.Id)) _delRelIds.Add((long)rd.Relation.Id);
                CmnObject relO = (parents ? rd.LeftObject : rd.RightObject);
                if (!rootIds.Contains(relO.RootId)) rootIds.Add(relO.RootId);
            }
            string query = "<?xml version=\"1.0\" encoding=\"UTF-8\"?><BooleanQuery><Clause occurs=\"must\"><BooleanQuery minimumNumberShouldMatch=\"1\">";
            foreach (long rootId in rootIds)
                query += "<Clause occurs=\"should\"><ExactPointQuery type=\"long\" fieldName=\"root\" value=\"" + rootId.ToString() + "\"/></Clause>";
            query += "</BooleanQuery></Clause><Clause occurs=\"must\"><TermQuery fieldName=\"latest_head\">true</TermQuery></Clause></BooleanQuery>";
            Dictionary<long, CmnObject> latestObjsByRootId = new Dictionary<long, CmnObject>();
            foreach (CmnObject latestO in _o.Session.SearchObjects(query).Values)
            {
                if (!latestObjsByRootId.ContainsKey(latestO.RootId) && latestO.LatestHead) latestObjsByRootId.Add(latestO.RootId, latestO);
            }
            // build lookup table

            // pass 2: create new rels
            foreach (ListViewItem lvi in lvw.SelectedItems)
            {
                RelationDescriptor rd = (RelationDescriptor)lvi.Tag;
                CmnObject relO = (parents ? rd.LeftObject : rd.RightObject);
                CmnObject newRelO = latestObjsByRootId[relO.RootId];     // TODO: update
                RelationDescriptor newRd = new RelationDescriptor(parents ? newRelO : _o, parents ? _o : newRelO, rd.RelationType,rd.Metadata);
                if (!lvw.Items.ContainsKey(newRd.Key)) AddRelationToLv(lvw, newRd.RelationType, newRelO, newRd);
                if (relO == newRelO) _delRelIds.Remove((long)rd.Relation.Id);
            }

            foreach (ListViewItem lvi in delItems) lvi.Remove();

            if (parents) _lvwParentsSort.ReSort();
            else _lvwChildrenSort.ReSort();
            Cursor = null;
            SetControlsEnabledState(true);

        }


        //private void ChooseRelationVersion(ListView lvw)
        //{

        //}

        private void ReplaceRelation(ListView lvw)
        {
            bool parents = (lvw == lvwParents);
            RelationDescriptor rd=(RelationDescriptor)lvw.SelectedItems[0].Tag;
            SelectObjectsDialog sd = new SelectObjectsDialog(_o.Session, 
                                                             (XmlElement)_configEl.SelectSingleNode("custom/object_selector/custom"), 
                                                             SelectObjectsDialog.SelectionModes.SelectSingleObject,
                                                             (parents ? rd.LeftObject.Parent : rd.RightObject.Parent),
                                                             Properties.Resources.lblChooseTarget,
															 _gad);
            if (sd.ShowSelectorDialog() == DialogResult.OK)
            {
                // TODO: select relation type
                XmlElement defaultRtEl = (XmlElement)_configEl.SelectSingleNode("custom/default_relation_type");
                C4RelationType defaultRt = null;
                if (defaultRtEl != null)
                {
                    if (_o.Session.SessionConfig.C4Sc.RelationTypesByName.ContainsKey(defaultRtEl.InnerText)) defaultRt = _o.Session.SessionConfig.C4Sc.RelationTypesByName[defaultRtEl.InnerText];
                }
                Cursor = Cursors.WaitCursor;

                RelationDescriptor newRd = null;
                CmnObject relO = (CmnObject)sd.Selection.Values.First();
                if(relO==_o) StandardMessage.ShowMessage(Properties.Resources.msgSelfReferencesNotAllowed, StandardMessage.Severity.ErrorMessage, null, null,
                                    _o.Session.GetHelpUrl(DataModelErrorCodes.INVALID_SELF_REFERENCE));
                else
                {
                    if (parents) newRd = new RelationDescriptor(relO, _o, rd.RelationType,rd.Metadata);
                    else newRd = new RelationDescriptor(_o, relO, rd.RelationType, rd.Metadata);
                    if (rd.Relation != null) _delRelIds.Add((long)rd.Relation.Id);
                    lvw.SelectedItems[0].Remove();
                    if (!lvw.Items.ContainsKey(newRd.Key)) AddRelationToLv(lvw, rd.RelationType, relO, newRd);
                    if (parents) _lvwParentsSort.ReSort();
                    else _lvwChildrenSort.ReSort();
                    Cursor = null;
                }
                SetControlsEnabledState(true);
            }

        }

        private void DeleteRelations(ListView lvw)
        {
            StandardMessage sm = new StandardMessage(Properties.Resources.msgSureRemoveRelations, StandardMessage.Severity.WarningMessage,null, null);
            sm.AddButton("ok", Properties.Resources.lblOk, false, true);
            sm.AddButton("cancel", Properties.Resources.lblCancel, true, false);
            sm.ShowDialog(this);
            if(sm.ExitButtonKey=="ok")
            {
                Cursor = Cursors.WaitCursor;
                List<ListViewItem> delItems = new List<ListViewItem>();
                foreach (ListViewItem lvi in lvw.SelectedItems)
                {
                    delItems.Add(lvi);
                    RelationDescriptor rd = (RelationDescriptor)lvi.Tag; 
                    if (rd.Relation!=null) _delRelIds.Add((long)rd.Relation.Id);
                }
                foreach (ListViewItem lvi in delItems) lvi.Remove();
                Cursor = null;
                SetControlsEnabledState(true);
            }
        }

        private void OpenRelation(ListView lvw)
        {
            Cursor = Cursors.WaitCursor;
            bool parents = (lvw == lvwParents);
            RelationDescriptor rd=(RelationDescriptor)lvw.SelectedItems[0].Tag;
            CmnObject o = (parents ? rd.LeftObject : rd.RightObject);
            WindowSelectionData wsd = new WindowSelectionData();
            wsd.Selection = new Dictionary<long, IRepositoryNode> { { o.Id, o } };
            wsd.SelectedFolder = o.Parent;
            SessionWindowRequest?.Invoke(wsd);
            Cursor = null;
        }

        private void CopyRelationFilename(ListView lvw)
        {
            Cursor = Cursors.WaitCursor;
            try
            {
                bool parents = (lvw == lvwParents);
                RelationDescriptor rd = (RelationDescriptor)lvw.SelectedItems[0].Tag;
                CmnObject o = (parents ? rd.LeftObject : rd.RightObject);
                if (_o.Session.LocksMgr.Locks.ContainsKey(_o.Id) && _o.Session.LocksMgr.Locks[_o.Id].DependenciesById.ContainsKey(o.Id)) Clipboard.SetText(Path.GetFileName(_o.Session.LocksMgr.Locks[_o.Id].DependenciesById[o.Id]));
                else StandardMessage.ShowMessage(Properties.Resources.msgFailureCopyingToClipboardNoChildFound, StandardMessage.Severity.ErrorMessage, null, null,
                                                    _o.Session.GetHelpUrl(DataModelErrorCodes.FAILURE_COPYING_TO_CLIPBOARD));
            }
            catch(Exception ex)
            {
                StandardMessage.ShowMessage(Properties.Resources.msgFailureCopyingToClipboard, StandardMessage.Severity.ErrorMessage, null, ex,
                                                    _o.Session.GetHelpUrl(DataModelErrorCodes.FAILURE_COPYING_TO_CLIPBOARD));
            }
            finally
            {
                Cursor = null;
            }
        }

        private void CopyRelationFilenamePath(ListView lvw)
        {
            Cursor = Cursors.WaitCursor;
            try
            {
                bool parents = (lvw == lvwParents);
                RelationDescriptor rd = (RelationDescriptor)lvw.SelectedItems[0].Tag;
                CmnObject o = (parents ? rd.LeftObject : rd.RightObject);
                if (_o.Session.LocksMgr.Locks.ContainsKey(_o.Id) && _o.Session.LocksMgr.Locks[_o.Id].DependenciesById.ContainsKey(o.Id)) Clipboard.SetText(_o.Session.LocksMgr.Locks[_o.Id].DependenciesById[o.Id]);
                else StandardMessage.ShowMessage(Properties.Resources.msgFailureCopyingToClipboardNoChildFound, StandardMessage.Severity.ErrorMessage, null, null,
                                                    _o.Session.GetHelpUrl(DataModelErrorCodes.FAILURE_COPYING_TO_CLIPBOARD));
            }
            catch (Exception ex)
            {
                StandardMessage.ShowMessage(Properties.Resources.msgFailureCopyingToClipboard, StandardMessage.Severity.ErrorMessage, null, ex,
                                                    _o.Session.GetHelpUrl(DataModelErrorCodes.FAILURE_COPYING_TO_CLIPBOARD));
            }
            finally
            {
                Cursor = null;
            }
        }

        public void Init(Dictionary<long, IRepositoryNode> dict)
        {
            _dict = dict;
            _o = DictionaryHelper.GetSingleObject(dict);
            IsDirty = false;
            // TODO: read permissions and enable / disable controls --> method SetControlStatus
            _enableEvents = false;
            if (_o == null)
            {
                ClearGui();
            }
            else
            {
                _editable = (_allowRelationEditOnLockedObjects || _o.Locked == null) && (_o.Permissions.Relation_Parent_Add || _o.Permissions.Relation_Child_Add || _o.Session.Server.Build < 148);
                _readable = _o.Permissions.Node_Browse;

                if (_readable)
                {
                    SetGui();
                }
                else ClearGui();
            }
            _enableEvents = true;
            SetControlsEnabledState(false);
        }
        public bool IsDirty { get; private set; }
        public bool IsValid(Dictionary<long, IRepositoryNode> dict, IGenericControl.ContextType ct)
        {
            if (ct != IGenericControl.ContextType.Object) return false;
            return DictionaryHelper.GetSingleObject(dict)!=null;
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
            XmlAttribute lockedObjectEdit = (XmlAttribute)configEl.SelectSingleNode("custom/locked_object_edit/@value");
            if (lockedObjectEdit != null && lockedObjectEdit.Value == "false") _allowRelationEditOnLockedObjects = false;
            else _allowRelationEditOnLockedObjects = true;

            XmlAttribute frs = (XmlAttribute)configEl.SelectSingleNode("custom/appearance/full_row_select/@value");
            if(frs!=null && frs.Value=="true")
            {
                lvwParents.FullRowSelect = true;
                lvwChildren.FullRowSelect = true;
            }
            else
            {
                lvwParents.FullRowSelect = false;
                lvwChildren.FullRowSelect = false;
            }

            lvwParents.Columns.Clear();
            lvwChildren.Columns.Clear();
            bool sortAscending=false;
            int sortColumn= -1;
            int i = 0;
            foreach (XmlElement colEl in _configEl.SelectNodes("custom/columns/list_view_column"))
            {
                string tp = colEl.GetAttribute("type");
                string w = colEl.GetAttribute("width");
                if (colEl.HasAttribute("sort"))
                {
                    sortAscending = (colEl.GetAttribute("sort") == "ascending" ? true : false);
                    sortColumn = i;
                }
                ++i;
                string label = tp;
                if (colEl.HasAttribute("class") && colEl.GetAttribute("class") == "custom")
                {
                    if (colEl.HasAttribute("label")) label = colEl.GetAttribute("label");
                }
                else
                {
                    label = GetStandardColumnTitle(tp);
                }
                // TODO: softcode horizontal alignment
                lvwParents.Columns.Add(tp, label, int.Parse(w), HorizontalAlignment.Left, -1);
                lvwChildren.Columns.Add(tp, label, int.Parse(w), HorizontalAlignment.Left, -1);

                _lvwParentsSort = new ListViewSort(lvwParents, true);
                _lvwChildrenSort = new ListViewSort(lvwChildren, true);
            }

            _iconService = (IIconService)s.GetSessionExtension("icon_service");
            lvwParents.SmallImageList = _iconService.GlobalSmallImageList;
            lvwParents.LargeImageList = _iconService.GlobalLargeImageList;
            //lvwParents.StateImageList = _iconService.GlobalStateImageList;
            lvwChildren.SmallImageList = _iconService.GlobalSmallImageList;
            lvwChildren.LargeImageList = _iconService.GlobalLargeImageList;
            //lvwChildren.StateImageList = _iconService.GlobalStateImageList;

            // TODO: sort
            if (sortColumn != -1)
            {
                _lvwParentsSort.Sort(sortColumn, sortAscending ? SortOrder.Ascending : SortOrder.Descending);
                _lvwChildrenSort.Sort(sortColumn, sortAscending ? SortOrder.Ascending : SortOrder.Descending);
            }

        }
        private string GetRelKey(C4Relation rel)
        {
            return string.Join("#", rel.TypeId.ToString(), rel.LeftId.ToString(), rel.RightId.ToString());
        }
        public void Save()
        {
            try
            {
                Dictionary<string, C4Relation> existingRelations = new Dictionary<string, C4Relation>();
                foreach (C4Relation rel in _parentRels.Values) existingRelations.Add(GetRelKey(rel), rel);
                foreach (C4Relation rel in _childRels.Values) existingRelations.Add(GetRelKey(rel), rel);

                Dictionary<string, C4Relation> requiredRelations = new Dictionary<string, C4Relation>();
                foreach (ListViewItem lvi in lvwParents.Items)
                {
                    RelationDescriptor rd = (RelationDescriptor)lvi.Tag;
                    C4Relation rel = new C4Relation((long)rd.RelationType.Id, rd.LeftObject.Id, rd.RightObject.Id, null);
                    requiredRelations.Add(GetRelKey(rel),rel);
                }
                foreach (ListViewItem lvi in lvwChildren.Items)
                {
                    RelationDescriptor rd = (RelationDescriptor)lvi.Tag;
                    C4Relation rel = new C4Relation((long)rd.RelationType.Id, rd.LeftObject.Id, rd.RightObject.Id, null);
                    string relKey = GetRelKey(rel);
                    requiredRelations.Add(relKey, rel);
                }

                HashSet<long> deleteRelationIds = new HashSet<long>();
                foreach (string relKey in existingRelations.Keys)
                {
                    if (!requiredRelations.ContainsKey(relKey)) deleteRelationIds.Add((long)existingRelations[relKey].Id);
                }
                if (deleteRelationIds.Count > 0) _o.Session.CommandSession.DeleteRelations(deleteRelationIds, true);

                Dictionary<string, C4Relation> createRelations = new Dictionary<string, C4Relation>();
                foreach(string relKey in requiredRelations.Keys)
                {
                    if (!existingRelations.ContainsKey(relKey)) createRelations.Add(relKey, requiredRelations[relKey]);
                }
                _o.Session.CommandSession.CreateRelations(createRelations.Values.ToHashSet<C4Relation>());

                Init(_dict);
            }
            catch(Exception ex)
            {
                StandardMessage.ShowMessage("Caught Exception", StandardMessage.Severity.ErrorMessage, this, ex);
            }
        }


        public string GetStandardColumnTitle(string tp)
        {
            switch (tp)
            {
                case "relationtype": return Properties.Resources.lblRelationType; 
                case "name": return Properties.Resources.lblName; 
                case "id": return Properties.Resources.lblId; 
                case "format": return Properties.Resources.lblFormat; 
                case "type": return Properties.Resources.lblType; 
                case "version": return Properties.Resources.lblVersion; 
                case "size": return Properties.Resources.lblSize; 
                case "owner": return Properties.Resources.lblOwner;
                case "creator": return Properties.Resources.lblCreatedBy;
                case "modifier": return Properties.Resources.lblModifiedBy;
                case "locked": return Properties.Resources.lblLockedBy; 
                case "lifecycle": return Properties.Resources.lblLifecycle; 
                case "path": return Properties.Resources.lblPath; 
                default: return null;
            }
        }

        private void lvwChildren_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                //ListViewHitTestInfo hi = lvwChildren.HitTest(e.Location);
                ContextMenuStrip cm = BuildContextMenu(lvwChildren);
                cm.Show(lvwChildren, e.Location);
            }
        }

        private void lvwParents_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                ContextMenuStrip cm = BuildContextMenu(lvwParents);
                cm.Show(lvwParents, e.Location);
            }

        }

        private void cmdSave_Click(object sender, EventArgs e)
        {
            Save();
        }

    }
}
