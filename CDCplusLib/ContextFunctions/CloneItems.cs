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
using C4ObjectApi.Interfaces;
using C4ObjectApi.Repository;
using C4GeneralGui.GuiElements;
using CDCplusLib.Common.GUI;
using CDCplusLib.EventData;

namespace CDCplusLib.ContextFunctions
{
    public class CloneItems : IGenericFunction
    {
        private GlobalApplicationData _gad;
        private XmlElement _configEl;
        private CmnSession _s;

        public string InstanceName { get; set; }

        public event IGenericFunction.SessionWindowRequestEventHandler SessionWindowRequest;
        public event IGenericFunction.NodesModifiedEventHandler NodesModified;

        public void AppendSubmenu(ToolStripMenuItem cmi, Dictionary<long, IRepositoryNode> dict)
        {
            // nothing to do
        }

        public bool DisplayInMenu(Dictionary<long, IRepositoryNode> dict)
        {
            return (dict != null && dict.Count > 0);
        }
        public void Execute(Dictionary<long, IRepositoryNode> dict)
        {
            WindowSelectionData wsd = new WindowSelectionData();
            string failedObjs = null;
            foreach(IRepositoryNode ow in dict.Values)
            {
                CmnObject o = (CmnObject)ow;
                try
                {
                    CmnObject copyO = o.Copy(o.Parent);
                    wsd.Selection.Add(copyO.Id, copyO);
                    wsd.Modification.Add(copyO.Id, copyO);
                }
                catch(Exception ex)
                {
                    // TODO: report error
                    if (failedObjs == null) failedObjs = string.Concat("The following objects were not cloned:\n", o.Id.ToString());
                    else failedObjs = string.Concat(failedObjs, "\n", o.Id.ToString());
                }
            }
            NodesModified?.Invoke(wsd); 
            if (failedObjs != null) StandardMessage.ShowMessage(failedObjs, StandardMessage.Severity.WarningMessage, null, null, null);
        }

        public Image GetIcon()
        {
            return null; // TODO
        }

        public string GetMenuText()
        {
            return Properties.Resources.mnuClone;
        }

        public bool HasSubmenuItems(Dictionary<long, IRepositoryNode> dict)
        {
            return false;
        }
        public bool IsValid(Dictionary<long, IRepositoryNode> dict)
        {
            if (dict != null && dict.Count > 0)
            {
                foreach(IRepositoryNode ow in dict.Values)
                {
                    if (ow.GetType() == typeof(CmnFolder)) return false;
                }
                return true;
            }
            else return false;
        }

        public void Reset(CmnSession s, GlobalApplicationData globalAppData, XmlElement configEl)
        {
            _gad = globalAppData;
            _configEl = configEl;
            _s = s;
        }
    }
}
