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

namespace CDCplusLib.ContextFunctions
{

    public class DeleteLinks : IGenericFunction
    {

        private GlobalApplicationData globalAppData_;


        public void AppendSubmenu(ToolStripMenuItem cmi)
        {

        }

        public bool HasSubmenuItems()
        {
            return false;
        }

        public string GetMenuText()
        {
            return Properties.Resources.mnuDeleteLinks;
        }
        public void Execute(Dictionary<long, IRepositoryNode> l)
        {
            if (l is not null)
            {
                foreach (IRepositoryNode ow in l.Values)
                    ow.Link.Delete();
            }
        }
        public bool IsValid(Dictionary<long, IRepositoryNode> dict)
        {
            if (dict is null)
                return false;
            foreach (IRepositoryNode ow in dict.Values)
            {
                if (ow.Link is null)
                    return false;
            }
            return true;
        }
        public bool DisplayInMenu(Dictionary<long, IRepositoryNode> dict)
        {
            if (dict is null)
                return false;
            foreach (IRepositoryNode ow in dict.Values)
            {
                if (ow.Link is null)
                    return false;
            }
            return true;
        }

        public string InstanceName { get; set; }

        public event IGenericFunction.MessageSentEventHandler MessageSent;

        public void Reset(CmnSession s, GlobalApplicationData globalAppData, XmlElement configEl)
        {
            globalAppData_ = globalAppData;
        }

        public Image GetIcon()
        {
            return null;
        }
    }
}