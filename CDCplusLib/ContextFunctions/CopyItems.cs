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

namespace CDCplusLib.ContextFunctions
{
    public class CopyItems : IGenericFunction
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
            _gad.Clipboard.Clear();
            _gad.Clipboard = dict;
            _gad.ClipboardSourceOperation = GlobalApplicationData.ClipboardOperations.ClipboardCopy;
        }

        public Image GetIcon()
        {
            return null; // TODO
        }

        public string GetMenuText()
        {
            return Properties.Resources.mnuCopy;
        }

        public bool HasSubmenuItems(Dictionary<long, IRepositoryNode> dict)
        {
            return false;
        }
        public bool IsValid(Dictionary<long, IRepositoryNode> dict)
        {
            return (dict != null && dict.Count > 0);
        }

        public void Reset(CmnSession s, GlobalApplicationData globalAppData, XmlElement configEl)
        {
            _gad = globalAppData;
            _configEl = configEl;
            _s = s;
        }
    }
}
