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
using C4ServerConnector.Assets;
using CDCplusLib.EventData;

namespace CDCplusLib.ContextFunctions
{
    public class NewFolder : IGenericFunction
    {
        private GlobalApplicationData _globalAppData;
        private CmnSession _s;
        private XmlElement _configEl;
        private C4Acl _newFolderAcl;

        public string GetMenuText()
        {
            return Properties.Resources.mnuNewFolder;
        }

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

        public void Execute(Dictionary<long, IRepositoryNode> dict)
        {
            EditStringValue esv = new EditStringValue(Properties.Resources.mnuNewFolder, Properties.Resources.lblNewName, "", Constants.VALID_NODE_NAME_REGEX);
            CmnFolder newF = null;
            if (dict==null) 
            {
                if (esv.ShowDialog() == DialogResult.OK) newF = _s.CreateSubfolder(null, esv.Value);
            }
            else 
            {
                CmnFolder f = DictionaryHelper.GetSingleFolder(dict);
                if (esv.ShowDialog() == DialogResult.OK) newF = f.CreateSubfolder(esv.Value, null, _newFolderAcl==null?f.Acl:_newFolderAcl);
            }
            if(newF!=null)
            {
                WindowSelectionData wsd = new WindowSelectionData();
                wsd.SelectedFolder = newF;
                wsd.Selection.Add(newF.Id, newF);
                wsd.Modification.Add(newF.Id, newF);
                NodesModified?.Invoke(wsd);
            }
        }

        public bool IsValid(Dictionary<long, IRepositoryNode> dict)
        {
            if (dict == null) return true;
            CmnFolder f = DictionaryHelper.GetSingleFolder(dict);
            if (f == null) return false;
            if (!f.Permissions.Folder_Create_Folder) return false;
            return true;
        }

        public bool DisplayInMenu(Dictionary<long, IRepositoryNode> dict)
        {
            if (dict == null) return true;
            CmnFolder f = DictionaryHelper.GetSingleFolder(dict);
            if (f == null) return false;
            return true;
        }

        public string InstanceName { get; set; }

        public event IGenericFunction.SessionWindowRequestEventHandler SessionWindowRequest;
        public event IGenericFunction.NodesModifiedEventHandler NodesModified;

        public void Reset(CmnSession s, GlobalApplicationData globalAppData, XmlElement configEl)
        {
            _globalAppData = globalAppData;
            _s = s;
            _configEl = configEl;
            XmlElement aclEl = _configEl.SelectSingleNode("custom/acl") as XmlElement;
            if (aclEl == null) _newFolderAcl = s.SessionConfig.C4Sc.AclsByName["_default_acl"];
            else if (aclEl.GetAttribute("mode") == "inherit_from_parent") _newFolderAcl = null;
            else _newFolderAcl = s.SessionConfig.C4Sc.AclsByName[aclEl.InnerText];
        }
    }
}