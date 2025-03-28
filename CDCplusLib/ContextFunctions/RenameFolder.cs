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
using C4ObjectApi.Helpers;
using C4ServerConnector;
using CDCplusLib.EventData;

namespace CDCplusLib.ContextFunctions
{
    public class RenameFolder : IGenericFunction
    {
        private GlobalApplicationData _gad;

        public void AppendSubmenu(ToolStripMenuItem cmi, Dictionary<long, IRepositoryNode> dict)
        {
        }

        public bool HasSubmenuItems(Dictionary<long, IRepositoryNode> dict)
        {
            return false;
        }

        public Image GetIcon()
        {
            return null;
        }

        public string GetMenuText()
        {
            return Properties.Resources.mnuRenameFolder;
        }

        public void Execute(Dictionary<long, IRepositoryNode> dict)
        {
            CmnFolder f = DictionaryHelper.GetSingleFolder(dict);
            string v = f.Name;
			EditStringValue esv = new EditStringValue(Properties.Resources.mnuRenameFolder, Properties.Resources.lblNewName, v, Constants.VALID_NODE_NAME_REGEX);
            if (esv.ShowDialog() == DialogResult.OK)
            {
				f.Session.CommandSession.UpdateFolder(f.Id, null, esv.Value);
                WindowSelectionData wsd = new WindowSelectionData();
                wsd.Selection.Add(f.Id, f);
                wsd.Modification.Add(f.Id, f);
                NodesModified?.Invoke(wsd);
            }
        }

        public bool IsValid(Dictionary<long, IRepositoryNode> dict)
        {
            CmnFolder f = DictionaryHelper.GetSingleFolder(dict);
            if (f is null)
                return false;
            if (!f.Permissions.Node_Name_Write)
                return false;
            return true;
        }

        public bool DisplayInMenu(Dictionary<long, IRepositoryNode> dict)
        {
            CmnFolder f = DictionaryHelper.GetSingleFolder(dict);
            if (f is null)
                return false;
            return true;
        }

        public string InstanceName { get; set; }

        public event IGenericFunction.SessionWindowRequestEventHandler SessionWindowRequest;
        public event IGenericFunction.NodesModifiedEventHandler NodesModified;

        public void Reset(CmnSession s, GlobalApplicationData globalAppData, XmlElement configEl)
        {
            _gad = globalAppData;
        }
    }
}