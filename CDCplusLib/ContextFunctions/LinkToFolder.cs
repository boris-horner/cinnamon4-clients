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
using CDCplusLib.Common.GUI;
using CDCplusLib.Interfaces;
using C4ObjectApi.Interfaces;
using C4ObjectApi.Repository;
using C4ServerConnector.Assets;
using static C4ServerConnector.Assets.C4Link;

namespace CDCplusLib.ContextFunctions
{

    public class LinkToFolder : IGenericFunction
    {

        private GlobalApplicationData _gad;
        private CmnSession _s;
        private XmlElement _configEl;

        public Image GetIcon()
        {
            return null;
        }
        public void AppendSubmenu(ToolStripMenuItem cmi, Dictionary<long, IRepositoryNode> dict)
        {

        }

        public bool HasSubmenuItems(Dictionary<long, IRepositoryNode> dict)
        {
            return false;
        }

        public string GetMenuText()
        {
            return Properties.Resources.mnuLinkToFolder;
        }
        public void Execute(Dictionary<long, IRepositoryNode> dict)
        {
            SelectObjectsDialog sd = new SelectObjectsDialog(_s, (XmlElement)_s.UserConfig.DocumentElement.SelectSingleNode("classes/object_selector/custom"), SelectObjectsDialog.SelectionModes.SelectFolder, _s.SessionConfig.HomeFolder, "Select link target", _gad);

            if (sd.ShowSelectorDialog() == DialogResult.OK)
            {
                CmnFolder targetF = (CmnFolder)sd.Selection.Values.First();
                ResolverTypes rt = ResolverTypes.Folder;
                if(ContainsObjects(dict))
                {
                    // If there is at least one object in dict, ask whether objects should be fixed or latest version
                    SelectObjectLinkResolver slr = new SelectObjectLinkResolver(_s, true);
                    if (slr.ShowDialog() == DialogResult.OK)
                    {
                        rt = slr.FixedVersion ? ResolverTypes.Fixed : ResolverTypes.LatestHead;
                    }
                    else return;
                }


                HashSet<C4Link> links= new HashSet<C4Link>();
                C4Acl linkAcl = _s.SessionConfig.C4Sc.AclsByName["_default_acl"];
                foreach (IRepositoryNode target in dict.Values)
                {
                    if (target.GetType() == typeof(CmnObject))
                    {
                        links.Add(new C4Link((long)linkAcl.Id, targetF.Id, (long)_s.User.Id, LinkTypes.Object, rt, target.Id));
                    }
                    else
                    {
                        links.Add(new C4Link((long)linkAcl.Id, targetF.Id, (long)_s.User.Id, LinkTypes.Folder, ResolverTypes.Folder, target.Id));
                    }
                }

                _s.CommandSession.CreateLinks(links);
            }
        }
        private bool ContainsObjects(Dictionary<long, IRepositoryNode> dict)
        {
            foreach (IRepositoryNode node in dict.Values)
            {
                if (node.GetType() == typeof(CmnObject))
                    return true;
            }
            return false;
        }
        public bool IsValid(Dictionary<long, IRepositoryNode> dict)
        {
            return dict!=null && dict.Count>0;
        }
        public bool DisplayInMenu(Dictionary<long, IRepositoryNode> dict)
        {
            return dict != null && dict.Count > 0;
        }
        public string InstanceName { get; set; }

        public event IGenericFunction.SessionWindowRequestEventHandler SessionWindowRequest;
        public event IGenericFunction.NodesModifiedEventHandler NodesModified;

        public void Reset(CmnSession s, GlobalApplicationData globalAppData, XmlElement configEl)
        {
            _gad = globalAppData;
            _s = s;
            _configEl = configEl;
        }
    }
}