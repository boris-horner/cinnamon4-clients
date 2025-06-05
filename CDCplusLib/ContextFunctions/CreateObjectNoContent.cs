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
using C4GeneralGui.GuiElements;
using CDCplusLib.EventData;

namespace CDCplusLib.ContextFunctions
{

    public class CreateObjectNoContent : IGenericFunction
    {

        private GlobalApplicationData _gad;

        public void AppendSubmenu(ToolStripMenuItem cmi, Dictionary<long, IRepositoryNode> dict)
        {

        }

        public bool HasSubmenuItems(Dictionary<long, IRepositoryNode> dict)
        {
            return false;
        }
        public string GetMenuText()
        {
            return Properties.Resources.mnuCreateObjectWithoutContent;
        }

        public Image GetIcon()
        {
            return null;
        }

        public void Execute(Dictionary<long, IRepositoryNode> dict)
        {
            CmnFolder f = DictionaryHelper.GetSingleFolder(dict);
            ObjectWithoutContent newObjDlg = new ObjectWithoutContent(_gad.CheckoutPath, f.Session);
            if (newObjDlg.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    CmnObject newO = f.Session.Create(f.Id, newObjDlg.ObjectName, null, null, newObjDlg.Language.Id, newObjDlg.ObjectType.Id, null, f.Session.SessionConfig.C4Sc.AclsByName["_default_acl"].Id);
                    WindowSelectionData wsd = new WindowSelectionData();
                    wsd.Selection.Add(newO.Id, newO);
                    wsd.Modification.Add(newO.Id, newO);
                    NodesModified?.Invoke(wsd);
                }
                catch (Exception ex)
                {
                    // TODO: I18N
                    StandardMessage.ShowMessage("Failure creating object without content. (I18N!)", StandardMessage.Severity.ErrorMessage, null, ex);
                }
            }
        }

        public bool IsValid(Dictionary<long, IRepositoryNode> dict)
        {
            CmnFolder f = DictionaryHelper.GetSingleFolder(dict);
            if (f is null)
                return false;
            if (!f.Permissions.Folder_Create_Object)
                return false;
            return true;
        }
        public bool DisplayInMenu(Dictionary<long, IRepositoryNode> dict)
        {

            CmnFolder f = DictionaryHelper.GetSingleFolder(dict);
            return f is not null;
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