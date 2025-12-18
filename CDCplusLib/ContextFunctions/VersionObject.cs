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
using C4ObjectApi.Exceptions;
using C4ObjectApi.Helpers;
using C4ServerConnector.Assets;
using C4GeneralGui.GuiElements;
using CDCplusLib.EventData;

namespace CDCplusLib.ContextFunctions
{
    public class VersionObject : IGenericFunction
    {
        private GlobalApplicationData _gad;
        private HashSet<long> _keepRelationIds;
        private XmlElement _configEl;

        public Image GetIcon()
        {
            return null;
        }

        public string GetMenuText()
        {
            return Properties.Resources.mnuCreateNewVersion;
        }

        public void AppendSubmenu(ToolStripMenuItem cmi, Dictionary<long, IRepositoryNode> dict)
        {
        }

        public bool HasSubmenuItems(Dictionary<long, IRepositoryNode> dict)
        {
            return false;
        }

        public void Execute(Dictionary<long, IRepositoryNode> dict)
        {
            CmnObject o = DictionaryHelper.GetSingleObject(dict);
            // load relation types to reattach 
            if (_keepRelationIds == null)
            {
                _keepRelationIds = new HashSet<long>();
                _keepRelationIds.Add((long)o.Session.SessionConfig.C4Sc.RelationTypesByName["child_content"].Id);
                try
                {
                    XmlElement krtsEl = (XmlElement)_configEl.SelectSingleNode("custom/keep_relationtypes"); // TODO: support further sets
                    if (krtsEl != null)
                    {
                        foreach (XmlElement krtEl in krtsEl.SelectNodes("keep_relationtype"))
                        {
                            string sn = krtEl.GetAttribute("sysname");
                            long rId = (long)o.Session.SessionConfig.C4Sc.RelationTypesByName[sn].Id;
                            if (!_keepRelationIds.Contains(rId))
                                _keepRelationIds.Add(rId);
                        }
                    }
                }
                catch (Exception ex)
                {
                    // nothing to do - list stays empty
                }
            }

            // remember relations to rebuild
            //Dictionary<long, C4Relation> relsToKeep = new Dictionary<long, C4Relation>();
            //foreach (C4Relation rel in o.Session.GetRelations(false, leftId: o.Id).Values)
            //{
            //    if (_keepRelationIds.Contains(rel.TypeId))
            //    {
            //        relsToKeep.Add((long)rel.Id, rel);
            //    }
            //}

            try
            {
                CmnObject newVersion = o.VersionCmd(null, null, o.Name);
                //if(newVersion.LifecycleState!=null)
                //{
                //    newVersion.LifecycleState = o.LifecycleState;
                //}


                // Assign previous metadata
                newVersion.Lock();
                //newVersion.Metadata = o.Metadata;
                if(o.Format!=null)
                {
                    o.CopyToExisting(newVersion, true, o.Session.SessionConfig.CopyMetasetTypeIds);
                }
                newVersion.Unlock();

                // rebuild relations
                //if (relsToKeep.Count > 0)
                //{
                //    HashSet<C4Relation> relations = new HashSet<C4Relation>();
                //    foreach (C4Relation rel in relsToKeep.Values) relations.Add(new C4Relation(rel.TypeId, newVersion.Id, rel.RightId, rel.Metadata));
                //    o.Session.CommandSession.CreateRelations(relations);
                //}

                WindowSelectionData wsd = new WindowSelectionData();
                wsd.Modification.Add(o.Id, o);
                wsd.Selection.Add(newVersion.Id, newVersion);
                wsd.Modification.Add(newVersion.Id, newVersion);
                NodesModified?.Invoke(wsd);
            }
            catch (CmnDataModelException ex)
            {
                switch (ex.ErrorCode)
                {
                    case DataModelErrorCodes.BRANCHING_NOT_ALLOWED:
                        {
                            StandardMessage.ShowMessage(Properties.Resources.msgBranchingForbidden, StandardMessage.Severity.ErrorMessage, null, null, o.Session.GetHelpUrl(ex.ErrorCode));
                            break;
                        }
                }
            }
        }

        public bool IsValid(Dictionary<long, IRepositoryNode> dict)
        {
            CmnObject o = DictionaryHelper.GetSingleObject(dict);
            // conditions on o for enabling cancel checkout  
            if (o is null)
                return false;
            if (o.Locked is object)
                return false;

            // check permissions on object
            return o.Permissions.Object_Version;
        }

        public bool DisplayInMenu(Dictionary<long, IRepositoryNode> dict)
        {
            CmnObject o = DictionaryHelper.GetSingleObject(dict);
            return o is object;
        }

        public string InstanceName { get; set; }

        public event IGenericFunction.SessionWindowRequestEventHandler SessionWindowRequest;
        public event IGenericFunction.NodesModifiedEventHandler NodesModified;

        public void Reset(CmnSession s, GlobalApplicationData globalAppData, XmlElement configEl)
        {
            _gad = globalAppData;
            _configEl = configEl;
        }
    }
}