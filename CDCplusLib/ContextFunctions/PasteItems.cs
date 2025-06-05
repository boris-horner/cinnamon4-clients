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
using C4ObjectApi.Interfaces;
using C4ObjectApi.Repository;
using C4ObjectApi.Helpers;
using C4ServerConnector.Assets;
using C4GeneralGui.GuiElements;
using CDCplusLib.EventData;

namespace CDCplusLib.ContextFunctions
{
    public class PasteItems : IGenericFunction
    {
        private GlobalApplicationData _gad;
        private XmlElement _configEl;
        private CmnSession _s;
        private CmnFolder _f;
        private HashSet<C4MetasetType> _copyMetasets;

        public string InstanceName { get; set; }

        public event IGenericFunction.SessionWindowRequestEventHandler SessionWindowRequest;
        public event IGenericFunction.NodesModifiedEventHandler NodesModified;

        public void AppendSubmenu(ToolStripMenuItem cmi, Dictionary<long, IRepositoryNode> dict)
        {
            ToolStripMenuItem tsmi = new ToolStripMenuItem(Properties.Resources.mnuPasteAllVersions);
            tsmi.Name = "all";
            cmi.DropDownItems.Add(tsmi);
            tsmi.Click += new EventHandler(menu_Click);
            tsmi = new ToolStripMenuItem(Properties.Resources.mnuPasteSelectedVersions);
            tsmi.Name = "sel";
            cmi.DropDownItems.Add(tsmi);
            tsmi.Click += new EventHandler(menu_Click);
        }
        void menu_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem tsmi = (ToolStripMenuItem)sender;
            if (tsmi.Name != "")
            {
                System.Diagnostics.Debug.Print(tsmi.Name);
            }
            else System.Diagnostics.Debug.Print("empty");

            switch (_gad.ClipboardSourceOperation)
            {
                case GlobalApplicationData.ClipboardOperations.ClipboardCopy:
                    if (tsmi.Name == "all") CopyAllVersions();
                    else if (tsmi.Name == "sel") CopySelectedVersions();
                    break;
                case GlobalApplicationData.ClipboardOperations.ClipboardCut:
                    if (tsmi.Name == "all") MoveAllVersions();
                    else if (tsmi.Name == "sel") MoveSelectedVersions();
                    break;
                case GlobalApplicationData.ClipboardOperations.ClipboardEmpty:
                    // nothing to do
                    break;
            }
            if(tsmi.Name!="") _gad.ClipboardSourceOperation = GlobalApplicationData.ClipboardOperations.ClipboardEmpty;
        }
        public bool DisplayInMenu(Dictionary<long, IRepositoryNode> dict)
        {
            _f = DictionaryHelper.GetSingleFolder(dict);
            return (_f != null);
        }
        private void MoveAllVersions()
        {
            try
            {
                string failedItemsList = null;

                Dictionary<long, IRepositoryNode> selectedObjs = new Dictionary<long, IRepositoryNode>();
                foreach (IRepositoryNode ow in _gad.Clipboard.Values)
                {
                    if (!ow.Permissions.Node_Parent_Folder_Write)
                    {
                        if (failedItemsList == null) failedItemsList = ow.Name;
                        else failedItemsList += "\n" + ow.Name;
                    
                    }
                    else if(ow.GetType()==typeof(CmnObject))
                    {
                        CmnObject selO = (CmnObject)ow;
                        if (!selectedObjs.ContainsKey(selO.RootId)) selectedObjs.Add(selO.RootId, selO);
                    }
                }
                if (failedItemsList != null) StandardMessage.ShowMessage(Properties.Resources.msgNoMovePermission + "\n\n" + failedItemsList,
                    StandardMessage.Severity.ErrorMessage);
                else
                {
                    CopyOperation co = new CopyOperation();
                    Dictionary<long, IRepositoryNode> selectedObjsAllVersions = co.ResolveAllNodes(selectedObjs);
                    // NB: only objects are passed here, unlike in CopyAllVersions, where folders must also be resolved!

                    // check permissions on all versions
                    foreach (IRepositoryNode ow in selectedObjsAllVersions.Values)
                    {
                        if (!ow.Permissions.Node_Parent_Folder_Write)
                        {
                            if (failedItemsList == null) failedItemsList = ow.Name;
                            else failedItemsList += "\n" + ow.Name;
                            if (ow.GetType() == typeof(CmnObject))
                            {
                                CmnObject selO = (CmnObject)ow;
                                if (!selectedObjs.ContainsKey(selO.RootId)) selectedObjs.Add(selO.RootId, selO);
                            }
                        }
                    }
                    if (failedItemsList != null) StandardMessage.ShowMessage(Properties.Resources.msgNoMovePermission + "\n\n" + failedItemsList,
                        StandardMessage.Severity.ErrorMessage);
                    else
                    {
                        // find objects with collected root ids
                        // loop through objects and folders and set parent to _f

                        WindowSelectionData wsd = new WindowSelectionData();
                        foreach (IRepositoryNode ow in _gad.Clipboard.Values)
                        {
                            if (ow.GetType() == typeof(CmnFolder))
                            {
                                // folder
                                CmnFolder pasteF = (CmnFolder)ow;
                                if (pasteF.ParentId != 0 && !wsd.Selection.ContainsKey(pasteF.ParentId)) wsd.Selection.Add(pasteF.ParentId, pasteF.Parent);
								_s.CommandSession.UpdateFolder(pasteF.Id, _f.Id);
                                wsd.Selection.Add(pasteF.Id, pasteF);
                                wsd.Modification.Add(pasteF.Id, pasteF);
                            }
                        }
                        foreach (CmnObject pasteO in selectedObjsAllVersions.Values)
                        {
                            if (pasteO.ParentId != 0 && !wsd.Selection.ContainsKey(pasteO.ParentId)) wsd.Selection.Add(pasteO.ParentId, pasteO.Parent);
                            pasteO.Lock();
							_s.CommandSession.UpdateObject(pasteO.Id, _f.Id);
                            pasteO.Unlock();
                            wsd.Selection.Add(pasteO.Id, pasteO);
                            wsd.Modification.Add(pasteO.Id, pasteO);
                        }
                        NodesModified?.Invoke(wsd);

                    }

                }


            }
            catch (Exception ex)
            {
                StandardMessage.ShowMessage(Properties.Resources.msgFailureMoving, StandardMessage.Severity.ErrorMessage, null, ex);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        private void MoveSelectedVersions()
        {
            try
            {
                string failedItemsList = null;
                foreach (IRepositoryNode ow in _gad.Clipboard.Values)
                {
                    if (!ow.Permissions.Node_Parent_Folder_Write)
                    {
                        if (failedItemsList == null) failedItemsList = ow.Name;
                        else failedItemsList += "\n" + ow.Name;
                    }
                }
                if (failedItemsList != null) StandardMessage.ShowMessage(Properties.Resources.msgNoMovePermission + "\n\n" + failedItemsList,
                    StandardMessage.Severity.ErrorMessage);
                else
                {
                    WindowSelectionData wsd = new WindowSelectionData();
                    foreach (IRepositoryNode ow in _gad.Clipboard.Values)
                    {
                        if (ow.GetType() == typeof(CmnFolder))
                        {
                            // folder
                            CmnFolder pasteF = (CmnFolder)ow;
                            if (pasteF.ParentId != 0 && pasteF.ParentId != _f.Id && !wsd.Selection.ContainsKey(pasteF.ParentId)) wsd.Selection.Add(pasteF.ParentId, pasteF.Parent);

							_s.CommandSession.UpdateFolder(pasteF.Id, _f.Id);
							wsd.Selection.Add(pasteF.Id, pasteF);
                            wsd.Modification.Add(pasteF.Id, pasteF);
                        }
                        else
                        {
                            // object
                            CmnObject pasteO = (CmnObject)ow;
                            if (pasteO.ParentId != 0 && pasteO.ParentId != _f.Id && !wsd.Selection.ContainsKey(pasteO.ParentId)) wsd.Selection.Add(pasteO.ParentId, pasteO.Parent);
                            pasteO.Lock();
							_s.CommandSession.UpdateObject(pasteO.Id, _f.Id);
							pasteO.Unlock();
                            wsd.Selection.Add(pasteO.Id, pasteO);
                            wsd.Modification.Add(pasteO.Id, pasteO);
                        }
                    }
                    NodesModified?.Invoke(wsd);
                }
            }
            catch (Exception ex)
            {
                StandardMessage.ShowMessage(Properties.Resources.msgFailureMoving, StandardMessage.Severity.ErrorMessage, null, ex);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }


        private void CopyAllVersions()
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                // pass 1: check whether objects and / or folders are in root
                Dictionary<string, CmnFolder> rootFoldersByPath = new Dictionary<string, CmnFolder>();

                bool rootObjects = false;
                bool rootFolders = false;
                foreach (IRepositoryNode ow in _gad.Clipboard.Values)
                {
                    if (ow.GetType() == typeof(CmnFolder))
                    {
                        rootFolders = true;
                        rootFoldersByPath.Add(((CmnFolder)ow).FolderPath, (CmnFolder)ow);
                    }
                    else
                    {
                        rootObjects = true;
                    }
                }
                string targetPermMsg = null;
                if (rootFolders && !_f.Permissions.Folder_Create_Folder) targetPermMsg = Properties.Resources.msgNoCreateFolderPermission;
                if (rootObjects && !_f.Permissions.Folder_Create_Object) targetPermMsg = Properties.Resources.msgNoCreateFolderPermission;

                if (targetPermMsg != null)
                    StandardMessage.ShowMessage(targetPermMsg, StandardMessage.Severity.ErrorMessage);
                else
                {
                    // pass 2: retrieve all objects and folders, and check source permissions
                    string failedItemsList = null;
                    CopyOperation co = new CopyOperation();
                    Dictionary<long, IRepositoryNode> allNodes = co.ResolveAllNodes(_gad.Clipboard);  // TODO: only if all versions should be copied (only possible if rootFolders==false)
                                                                                                // TODO: configurable total size warning
                    Dictionary<long, Dictionary<long, CmnObject>> rootTree = new Dictionary<long, Dictionary<long, CmnObject>>();
                    Dictionary<long, SortedDictionary<Int64, CmnFolder>> foldersByRoot = new Dictionary<long, SortedDictionary<Int64, CmnFolder>>();
                    foreach (IRepositoryNode ow in allNodes.Values)
                    {
                        if (ow.GetType() == typeof(CmnFolder))
                        {
                            if (!ow.Permissions.Node_Browse)
                            {
                                if (failedItemsList == null) failedItemsList = ow.Name;
                                else failedItemsList += "\n" + ow.Name;
                            }
                            else
                            {
                                CmnFolder sourceSubF = (CmnFolder)ow;
                                CmnFolder sourceRootF = null;
                                foreach (string rf in rootFoldersByPath.Keys)
                                {
                                    if (sourceSubF.FolderPath.StartsWith(rf))
                                    {
                                        sourceRootF = rootFoldersByPath[rf];
                                        break;
                                    }
                                }
                                if (!foldersByRoot.ContainsKey(sourceRootF.Id))
                                {
                                    foldersByRoot.Add(sourceRootF.Id, new SortedDictionary<long, CmnFolder>());
                                    //foldersByRoot[sourceRootF.Id].Add(0, sourceRootF);
                                }
                                Int64 l = sourceSubF.FolderPath.Length * 10000L;
                                while (foldersByRoot[sourceRootF.Id].ContainsKey(l)) ++l;
                                foldersByRoot[sourceRootF.Id].Add(l, sourceSubF);
                            }

                        }
                        else
                        {
                            if (!ow.Permissions.Object_Content_Read || !ow.Permissions.Node_Browse || !ow.Permissions.Node_Metadata_Read)
                            {
                                if (failedItemsList == null) failedItemsList = ow.Name;
                                else failedItemsList += "\n" + ow.Name;
                            }
                            else
                            {
                                CmnObject o = (CmnObject)ow;
                                if (!rootTree.ContainsKey(o.RootId)) rootTree.Add(o.RootId, new Dictionary<long, CmnObject>());
                                if (!rootTree[o.RootId].ContainsKey(o.Id)) rootTree[o.RootId].Add(o.Id, o);
                            }
                        }
                    }
                    if (failedItemsList != null) StandardMessage.ShowMessage(Properties.Resources.msgNoReadPermission + "\n\n" + failedItemsList,
                        StandardMessage.Severity.ErrorMessage);
                    else
                    {
                        bool containsRootFolders = false;
                        foreach (long rootFId in foldersByRoot.Keys)
                        {
                            CmnFolder rootF = foldersByRoot[rootFId].Values.First();
                            if (rootF.ParentId == 0)
                            {
                                containsRootFolders = true;
                                break;
                            }
                        }
                        if (containsRootFolders) StandardMessage.ShowMessage(Properties.Resources.msgCopyingRootFoldersNotSupported, StandardMessage.Severity.ErrorMessage);
                        else
                        {
                            // pass 3: create folders
                            WindowSelectionData wsd = new WindowSelectionData();
                            Dictionary<long, CmnFolder> sourceTargetFLookup = new Dictionary<long, CmnFolder>();
                            foreach (long rootFId in foldersByRoot.Keys)
                            {
                                foreach (CmnFolder srcF in foldersByRoot[rootFId].Values)
                                {
                                    if (srcF.ParentId == 0)
                                    {
                                        // TODO: root folders - check this before start
                                        System.Diagnostics.Debug.Print("copying from root still missing");
                                    }
                                    else
                                    {
                                        if (srcF.Id == rootFId)
                                        {
                                            CmnFolder targetSubF = _f.CreateSubfolder(srcF.Name);
                                            sourceTargetFLookup.Add(srcF.Id, targetSubF);
                                            wsd.Selection.Add(targetSubF.Id, targetSubF);
                                        }
                                        else
                                        {
                                            if (sourceTargetFLookup.ContainsKey(srcF.ParentId))
                                            {
                                                CmnFolder targetSubF = sourceTargetFLookup[srcF.ParentId].CreateSubfolder(srcF.Name);
                                                sourceTargetFLookup.Add(srcF.Id, targetSubF);
                                                wsd.Selection.Add(targetSubF.Id, targetSubF);
                                            }
                                            else
                                            {
                                                // is this an error? does this ever happen?
                                                System.Diagnostics.Debug.Print("no known parent copy");
                                            }
                                        }
                                    }
                                }

                            }

                            // pass 4: create objects
                            // traverse rootTree.Keys
                            Dictionary<long, CmnObject> sourceTargetOLookup = new Dictionary<long, CmnObject>();
                            foreach (long rootId in rootTree.Keys)
                            {
                                CmnFolder copyTargetFolder;
                                if (sourceTargetFLookup.ContainsKey(rootTree[rootId][rootId].ParentId)) copyTargetFolder = sourceTargetFLookup[rootTree[rootId][rootId].ParentId];
                                else copyTargetFolder = _f;

                                // sort all versions of an object in the order it must be copied
                                SortedDictionary<DateTime, CmnObject> versionsByCreationDate = new SortedDictionary<DateTime, CmnObject>();
                                foreach (CmnObject versionO in rootTree[rootId].Values) versionsByCreationDate.Add(versionO.Created, versionO);
                                CmnObject curO = null;
                                foreach (CmnObject versionO in versionsByCreationDate.Values)
                                {
                                    
                                    if (curO == null) curO = _s.Create(copyTargetFolder.Id, versionO.Name, null, null, versionO.Language.Id, versionO.ObjectType.Id, null, _s.SessionConfig.C4Sc.AclsByName["_default_acl"].Id);
                                    else
                                    {
                                        CmnObject preO = sourceTargetOLookup[(long)versionO.PredecessorId];
                                        curO = preO.VersionCmd(null, null, versionO.Name);
                                    }
                                    sourceTargetOLookup.Add(versionO.Id, curO);
                                    wsd.Selection.Add(curO.Id, curO);
                                    wsd.Modification.Add(curO.Id, curO);

                                    // more performant server based copy
                                    curO.Lock();
                                    versionO.CopyToExisting(curO, versionO.Format != null, curO.Session.SessionConfig.CopyMetasetTypeIds); // copy all metasets (only tika is caught and not copied)
                                    if (versionO.LifecycleState == null)
                                    {
										_s.CommandSession.UpdateObject(curO.Id,
																			null,
																			null,
                                                                            null,
                                                                            versionO.Acl.Id);
                                    }
                                    else
                                    {
                                        // Default State ID must be set for all lifecycles
                                        curO.LifecycleState = curO.Session.SessionConfig.C4Sc.LifecycleStatesById[(long)versionO.Session.SessionConfig.C4Sc.LifecyclesById[versionO.LifecycleState.LifecycleId].DefaultStateId];
                                    }
                                    curO.Unlock();

                                }

                            }

                            // rebuild child relations, to copy, if they point to an object also in the copy scope (only if all versions are copied), else to the same object as source
                            C4RelationType childContentReltype = _s.SessionConfig.C4Sc.RelationTypesByName["child_content"];
                            //foreach (CmnObject versionO in versionsByCreationDate.Values)
                            foreach (long sourceId in sourceTargetOLookup.Keys)
                            {
                                Dictionary<long, C4Relation> rels = _s.GetRelations(false, childContentReltype, sourceId);
                                HashSet<C4Relation> relations = new HashSet<C4Relation>();
                                foreach (C4Relation rel in rels.Values)
                                {
                                    if (sourceTargetOLookup.ContainsKey(rel.RightId))
                                    {
                                        // link within scope - point to copy
                                        relations.Add(new C4Relation((long)childContentReltype.Id, sourceTargetOLookup[sourceId].Id, sourceTargetOLookup[rel.RightId].Id, rel.Metadata));
                                    }
                                    else
                                    {
                                        // link outside of scope - point to original
                                        relations.Add(new C4Relation((long)childContentReltype.Id, sourceTargetOLookup[sourceId].Id, rel.RightId, rel.Metadata));
                                    }
                                }
                                _s.CommandSession.CreateRelations(relations);
                            }

                            NodesModified?.Invoke(wsd);
                        }

                    }
                }
            }
            catch(Exception ex)
            {
                StandardMessage.ShowMessage(Properties.Resources.msgFailureCopying, StandardMessage.Severity.ErrorMessage, null, ex);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }
        private void CopySelectedVersions()
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                // pass 1: check whether objects and / or folders are in root
                Dictionary<string, CmnFolder> rootFoldersByPath = new Dictionary<string, CmnFolder>();

                foreach (IRepositoryNode ow in _gad.Clipboard.Values)
                {
                    if (ow.GetType() == typeof(CmnFolder))
                    {
                        StandardMessage.ShowMessage(Properties.Resources.msgCopySelectedNoFoldersPermitted, StandardMessage.Severity.ErrorMessage);
                        return;
                    }
                }

                string targetPermMsg = null;
                if (!_f.Permissions.Folder_Create_Object) targetPermMsg = Properties.Resources.msgNoCreateFolderPermission;

                if (targetPermMsg != null)
                    StandardMessage.ShowMessage(targetPermMsg, StandardMessage.Severity.ErrorMessage);
                else
                {
                    // pass 2: retrieve all objects and folders, and check source permissions
                    string failedItemsList = null;
                    CopyOperation co = new CopyOperation();
                    foreach (IRepositoryNode ow in _gad.Clipboard.Values)
                    {
                        if (!ow.Permissions.Object_Content_Read || !ow.Permissions.Node_Browse || !ow.Permissions.Node_Metadata_Read)
                        {
                            if (failedItemsList == null) failedItemsList = ow.Name;
                            else failedItemsList += "\n" + ow.Name;
                        }
                    }
                    if (failedItemsList != null) StandardMessage.ShowMessage(Properties.Resources.msgNoReadPermission + "\n\n" + failedItemsList,
                        StandardMessage.Severity.ErrorMessage);
                    else
                    {

                        // pass 4: create objects
                        WindowSelectionData wsd = new WindowSelectionData();
                        foreach (IRepositoryNode ow in _gad.Clipboard.Values)
                        {
                            CmnObject curTgtO = ((CmnObject)ow).Copy(_f);   // TODO: move this to create / copytoexisting
                            wsd.Selection.Add(curTgtO.Id, curTgtO);
                            wsd.Modification.Add(curTgtO.Id, curTgtO);
                        }
                        NodesModified?.Invoke(wsd);
                    }

                }
            }
            catch (Exception ex)
            {
                StandardMessage.ShowMessage(Properties.Resources.msgFailureCopying, StandardMessage.Severity.ErrorMessage, null, ex);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }
        public void Execute(Dictionary<long, IRepositoryNode> dict)
        {
        }

        public Image GetIcon()
        {
            return null; // TODO
        }

        public string GetMenuText()
        {
            return Properties.Resources.mnuPaste;
        }

        public bool HasSubmenuItems(Dictionary<long, IRepositoryNode> dict)
        {
            return true;
        }
        public bool IsValid(Dictionary<long, IRepositoryNode> dict)
        {
            if (dict.Count() == 1 && 
                dict.Values.First().GetType() == typeof(CmnFolder) && 
                _gad.ClipboardSourceOperation != GlobalApplicationData.ClipboardOperations.ClipboardEmpty) return true;
            return false;
        }

        public void Reset(CmnSession s, GlobalApplicationData globalAppData, XmlElement configEl)
        {
            _gad = globalAppData;
            _configEl = configEl;
            _s = s;
            _copyMetasets = new HashSet<C4MetasetType>();
            foreach (string msTypeName in _s.SessionConfig.C4Sc.MetasetTypesByName.Keys) if (msTypeName != "tika" && msTypeName != "html_preview") _copyMetasets.Add(_s.SessionConfig.C4Sc.MetasetTypesByName[msTypeName]);
        }
    }
}
