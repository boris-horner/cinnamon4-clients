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
using System.Diagnostics;
using System.Xml;
using CDCplusLib.Common;
using CDCplusLib.Interfaces;
using C4ObjectApi.Interfaces;
using C4ObjectApi.Repository;
using C4ObjectApi.Helpers;
using C4ObjectApi.Global;
using C4GeneralGui.GuiElements;

namespace CDCplusLib.ContextFunctions
{

    public class View : IGenericFunction
    {

        private GlobalApplicationData _gad;


        public void AppendSubmenu(ToolStripMenuItem cmi)
        {

        }

        public bool HasSubmenuItems()
        {
            return false;
        }
        public Image GetIcon()
        {
            return null;
        }
        public string GetMenuText()
        {
            return Properties.Resources.mnuView;
        }
        public void Execute(Dictionary<long, IRepositoryNode> dict)
        {
            CmnObject o = DictionaryHelper.GetSingleObject(dict);
            try
            {
                ExportResults r = o.Export(CmnObject.ChildExportPolicy.ObjectAndDescendants, true);
                ProcessStartInfo psi = new ProcessStartInfo
                {
                    FileName = r.RootObject.LocalPath,
                    UseShellExecute = true
                };
                Process.Start(psi);
            }
            catch (Exception ex)
            {
                StandardMessage.ShowMessage(Properties.Resources.msgFailureViewingObject, StandardMessage.Severity.ErrorMessage, null, ex);
            }
        }

        public bool IsValid(Dictionary<long, IRepositoryNode> dict)
        {
            CmnObject o = DictionaryHelper.GetSingleObject(dict);
            // conditions on o for enabling cancel checkout  
            if (o is null)
                return false;
            if (o.Format == null)
                return false;
            if (!o.Permissions.Object_Content_Read)
                return false;
            return true;
        }
        public bool DisplayInMenu(Dictionary<long, IRepositoryNode> dict)
        {
            CmnObject o = DictionaryHelper.GetSingleObject(dict);
            return o is not null;
        }
        public string InstanceName { get; set; }

        public event IGenericFunction.MessageSentEventHandler MessageSent;

        public void Reset(CmnSession s, GlobalApplicationData globalAppData, XmlElement configEl)
        {
            _gad = globalAppData;
        }
    }
}