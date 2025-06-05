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
using CDCplusLib.EventData;
using CDCplusLib.Common.GUI;

namespace CDCplusLib.ContextFunctions
{

    public class OpenInNewWindow : IGenericFunction
    {

        private GlobalApplicationData _gad;
        private CmnSession _s;

        public string GetMenuText()
        {
            return Properties.Resources.mnuOpenInNewWindow;
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
            WindowSelectionData wsd = new WindowSelectionData();
            if(dict.Values.First().GetType() == typeof(CmnObject))
            {
                CmnObject o = dict.Values.First() as CmnObject;
                wsd.RootNodeType = SessionTree.RootNodeTypes.Session;
                wsd.Selection.Add(o.Id, o);
                wsd.SelectedFolder = o.Parent;
            }
            else
            {
                CmnFolder f = dict.Values.First() as CmnFolder;
                wsd.RootNodeType = SessionTree.RootNodeTypes.Session;
                wsd.Selection.Add(f.Id, f);
                wsd.SelectedFolder = f;
            }

            SessionWindowRequest?.Invoke(wsd);
        }

        public bool IsValid(Dictionary<long, IRepositoryNode> dict)
        {
            return dict != null && dict.Count == 1;
        }
        public bool DisplayInMenu(Dictionary<long, IRepositoryNode> dict)
        {
            return dict != null && dict.Count == 1;
        }
        public string InstanceName { get; set; }


        public event IGenericFunction.SessionWindowRequestEventHandler SessionWindowRequest;
        public event IGenericFunction.NodesModifiedEventHandler NodesModified;

        public void Reset(CmnSession s, GlobalApplicationData globalAppData, XmlElement configEl)
        {
            _gad = globalAppData;
            _s = s;
        }

        public Image GetIcon()
        {
            return null;
        }
    }
}