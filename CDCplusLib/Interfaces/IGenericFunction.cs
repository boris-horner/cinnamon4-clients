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
using C4ObjectApi.Interfaces;
using C4ObjectApi.Repository;
using CDCplusLib.EventData;

namespace CDCplusLib.Interfaces
{
    public interface IGenericFunction
    {
        event SessionWindowRequestEventHandler SessionWindowRequest;
        delegate void SessionWindowRequestEventHandler(WindowSelectionData wsd);

        event NodesModifiedEventHandler NodesModified;
        delegate void NodesModifiedEventHandler(WindowSelectionData wsd);

        string GetMenuText();

        string InstanceName { get; set; }

        void Reset(CmnSession s, GlobalApplicationData globalAppData, XmlElement configEl);
        bool IsValid(Dictionary<long, IRepositoryNode> dict);
        bool DisplayInMenu(Dictionary<long, IRepositoryNode> dict);
        void Execute(Dictionary<long, IRepositoryNode> dict);
        Image GetIcon();
        bool HasSubmenuItems(Dictionary<long, IRepositoryNode> dict);
        void AppendSubmenu(ToolStripMenuItem cmi, Dictionary<long, IRepositoryNode> dict);
    }
}