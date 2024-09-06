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
using System.Diagnostics;
using System.Xml;
using CDCplusLib.Common;
using CDCplusLib.Common.GUI;
using CDCplusLib.Interfaces;
using CDCplusLib.Messages;
using C4ObjectApi.Interfaces;
using C4ObjectApi.Repository;
using C4ServerConnector.Assets;
using C4GeneralGui.GuiElements;

namespace CDCplusLib.ContextFunctions
{
    public class DeleteSelection : IGenericFunction
    {
        private GlobalApplicationData _gad;
        private CmnSession _s;
        private bool _deleteAllVersionsEnabled;
        private bool _useProgressBar;

        public void AppendSubmenu(ToolStripMenuItem cmi)
        {
        }

        public bool HasSubmenuItems()
        {
            return false;
        }

        public string GetMenuText()
        {
            return Properties.Resources.lblDelete;
        }

        public void Execute(Dictionary<long, IRepositoryNode> sel)
        {
            if (sel is object)
            {

                // Pass 1: gather objects
                bool cnt = true;
                Dictionary<long, IRepositoryNode> l = new Dictionary<long, IRepositoryNode>();
                bool delAllRels = false;
                if (sel.Count == 1 && sel.Values.First() is CmnObject)
                {
                    if (((CmnObject)sel.Values.First()).LatestBranch && _deleteAllVersionsEnabled)
                    {
                        SelectDeleteVersions delDlg = new SelectDeleteVersions(_deleteAllVersionsEnabled, true);
                        if (delDlg.ShowDialog() == DialogResult.OK)
                        {
                            delAllRels = delDlg.DeleteProtectedRelations;
                            AppendOwnable(sel.Values.First(), l, delDlg.AllVersions);
                        }
                        else
                        {
                            cnt = false;
                        }
                    }
                    else
                    {
                        StandardMessage.ShowMessage(string.Format(Properties.Resources.msgFailureDeletingVersionWithSuccessor, "1"), StandardMessage.Severity.ErrorMessage);
                        return;
                    }
                }
                else
                {
                    bool selectionContainsFolders = false;
                    foreach (IRepositoryNode ow in sel.Values)
                    {
                        if (ReferenceEquals(ow.GetType(), typeof(CmnFolder)))
                        {
                            selectionContainsFolders = true;
                            break;
                        }
                    }

                    SelectDeleteVersions delDlg = new SelectDeleteVersions(_deleteAllVersionsEnabled, !selectionContainsFolders);
                    //bool allVersions = false;
                    if (delDlg.ShowDialog() == DialogResult.OK)
                    {
                        delAllRels = delDlg.DeleteProtectedRelations;
                        ProgressBarPopup dlgProgress=null;
                        if (_useProgressBar)
                        {
                            dlgProgress = new ProgressBarPopup(Properties.Resources.lblDelete, 0, sel.Count, 0, "");
                            dlgProgress.UpdateProgressBar(0, "Getting items");
                            dlgProgress.Show();
                        }
                        int i = 0;
                        foreach (IRepositoryNode ow in sel.Values)
                        {
                            AppendOwnable(ow, l, delDlg.AllVersions);
                            i += 1;
                            if (_useProgressBar) dlgProgress.UpdateProgressBar(i);
                        }

                        if (_useProgressBar) dlgProgress.Close();
                    }

                    // End If
                }

                // Pass 2: check whether delete is possible
                Dictionary<long, IRepositoryNode> noPermissions = new Dictionary<long, IRepositoryNode>();
                foreach (IRepositoryNode ow in l.Values)
                {
                    try
                    {
                        if (!ow.Permissions.Node_Delete && !noPermissions.ContainsKey(ow.Id))
                            noPermissions.Add(ow.Id, ow);
                    }
                    catch (Exception ex)
                    {
                        Debug.Print("Exception");
                    }
                }

                if (noPermissions.Count > 0)
                {
                    StandardMessage.ShowMessage(Properties.Resources.msgFailureDeletingInsufPerm, StandardMessage.Severity.ErrorMessage);
                    cnt = false;
                }

                if (cnt)
                {
                    ObjectsDeletedMessage msg = new ObjectsDeletedMessage();
                    // msg.Source = instanceName_
                    if (l.Count > 1)
                    {
                        ProgressBarPopup dlgProgress=null;
                        if (_useProgressBar)
                        {
                            dlgProgress = new ProgressBarPopup(Properties.Resources.lblDelete, 0, l.Count, 0, "");
                            dlgProgress.Show();
                        }
                        int remaining = DeleteList(l, msg.DeletedObjects, delAllRels, dlgProgress).Count;
                        if (_useProgressBar) dlgProgress.Close();
                        if (remaining > 0)
                            StandardMessage.ShowMessage(string.Format(Properties.Resources.msgFailureDeleting, remaining.ToString()), StandardMessage.Severity.ErrorMessage);
                    }
                    else
                    {
                        try
                        {
                            IRepositoryNode ow = l.Values.First();
                            if (ow is CmnObject)
                            {
                                ((CmnObject)ow).Delete(false, false);
                            }
                            else
                            {
                                ((CmnFolder)ow).Delete(false);
                            }

                            msg.DeletedObjects.Add(ow.Id, ow);
                        }
                        catch (Exception ex)
                        {
                            StandardMessage.ShowMessage(string.Format(Properties.Resources.msgFailureDeleting, "1"), StandardMessage.Severity.ErrorMessage);
                        }
                    }

                    if (msg.DeletedObjects.Count > 0)
                        MessageSent?.Invoke(msg);
                }
            }
        }

        private void AppendOwnable(IRepositoryNode ow, Dictionary<long, IRepositoryNode> l, bool allVersions)
        {
            if (ow is CmnFolder)
            {
                if (!l.ContainsKey(ow.Id))
                {
                    l.Add(ow.Id, ow);
                    CmnFolder f = (CmnFolder)ow;
                    foreach (CmnObject o in f.GetObjects(CmnSession.Versions.All).Values)
                        AppendOwnable(o, l, false);
                    foreach (CmnFolder subF in f.GetSubfolders().Values)
                        AppendOwnable(subF, l, false);
                }
            }
            else if (allVersions)
            {
                Dictionary<long, CmnObject> av = ((CmnObject)ow).GetAllVersions();
                foreach (CmnObject o in av.Values)
                    AppendOwnable(o, l, false);
            }
            else if (!l.ContainsKey(ow.Id))
            {
                l.Add(ow.Id, ow);
                // AppendRelatedObjects(CType(ow, CmnObject), l, relL)
            }
        }

        private SortedDictionary<string, IRepositoryNode> DeleteList(Dictionary<long, IRepositoryNode> l, Dictionary<long, IRepositoryNode> deletedObjects, bool delAllRels, ProgressBarPopup dlgProgress)
        {
            SortedDictionary<string, IRepositoryNode> sl = new SortedDictionary<string, IRepositoryNode>();
            if (_useProgressBar) dlgProgress.UpdateProgressBar(0, "Configuring delete operation");
            foreach (IRepositoryNode ow in l.Values)
            {
                string key = "";
                if (ow is CmnFolder)
                {
                    key = "1" + ((CmnFolder)ow).FolderPath;
                }
                else
                {
                    CmnObject o = (CmnObject)ow;
                    key = "2" + o.RootId.ToString() + o.Version;
                }

                if (!sl.ContainsKey(key))
                    sl.Add(key, ow);
            }

            if (_useProgressBar) dlgProgress.UpdateProgressBar(0, "Deleting");
            int ct = sl.Count;
            bool cont = true;
            SortedDictionary<string, IRepositoryNode> result = DeletePass(sl, deletedObjects, delAllRels, dlgProgress, 0);
            while (cont)
            {
                if (result.Count < ct)
                {
                    ct = result.Count;
                    result = DeletePass(result, deletedObjects, delAllRels, dlgProgress, sl.Count - ct);
                }
                else
                {
                    cont = false;
                    // TODO: report back the failed objects
                }
            }

            return result;
        }

        private SortedDictionary<string, IRepositoryNode> DeletePass(SortedDictionary<string, IRepositoryNode> sl, Dictionary<long, IRepositoryNode> deletedObjects, bool delAllRels, ProgressBarPopup dlgProgress, int startIndex)
        {
            int i = startIndex;
            SortedDictionary<string, IRepositoryNode> result = new SortedDictionary<string, IRepositoryNode>();
            if (sl.Count > 0)
            {
                foreach (string key in sl.Keys.Reverse())
                {
                    IRepositoryNode ow = sl[key];
                    try
                    {
                        if (ow is CmnObject)
                        {
                            CmnObject delO = (CmnObject)ow;
                            if (delAllRels)
                            {
                                HashSet<long> ids = new HashSet<long>();
                                foreach (C4Relation rel in delO.Session.GetRelations(false, leftId: delO.Id).Values) ids.Add((long)rel.Id);
                                _s.CommandSession.DeleteRelations(ids, true);
                            }
                            delO.Delete(false, false);
                        }
                        else
                        {
                            ((CmnFolder)ow).Delete(false);
                        }

                        if (!deletedObjects.ContainsKey(ow.Id))
                            deletedObjects.Add(ow.Id, ow);
                        i += 1;
                        if (_useProgressBar) dlgProgress.UpdateProgressBar(i);
                    }
                    catch (Exception ex)
                    {
                        result.Add(key, ow);
                    }
                }
            }

            return result;
        }

        public bool IsValid(Dictionary<long, IRepositoryNode> dict)
        {
            // menu item should be enabled if object can be checked out
            if (dict is null)
                return false;
            foreach (IRepositoryNode ow in dict.Values)
            {
                if (ow is CmnFolder)
                {
                    CmnFolder cmnf = (CmnFolder)ow;
                    if (!cmnf.Permissions.Node_Delete)
                        return false;
                    if (cmnf.Link is object)
                        return false;
                }
                else if (ow is CmnObject)
                {
                    CmnObject cmno = (CmnObject)ow;
                    if (cmno.Locked is object)
                        return false;
                    if (!cmno.Permissions.Node_Delete)
                        return false;
                    if (cmno.Link is object)
                        return false;
                }
            }

            return true;
        }

        public bool DisplayInMenu(Dictionary<long, IRepositoryNode> dict)
        {
            // menu item should be displayed for single objects  
            if (dict is null)
                return false;
            if (dict.Count == 0)
                return false;
            return true;
        }

        public string InstanceName { get; set; }

        public event IGenericFunction.MessageSentEventHandler MessageSent;

        public void Reset(CmnSession s, GlobalApplicationData globalAppData, XmlElement configEl)
        {
            _s = s;
            _gad = globalAppData;
            XmlNode davdN = configEl.SelectSingleNode("custom/delete_all_versions[@enabled='false']");
            _deleteAllVersionsEnabled = davdN is null;
            _useProgressBar = s.Server.LocalSettings.ContainsKey("use_threaded_gui_controls") ? s.Server.LocalSettings["use_threaded_gui_controls"] == "true" : true;
        }

        public Image GetIcon()
        {
            return null;
        }
    }
}