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
using C4GeneralGui.GuiElements;
using CDCplusLib.EventData;

namespace CDCplusLib.ContextFunctions
{

    public class CancelCheckout : IGenericFunction
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
            return Properties.Resources.mnuCancelCheckout;
        }

        public void Execute(Dictionary<long, IRepositoryNode> dict)
        {
            StandardMessage sm = new StandardMessage(Properties.Resources.msgCancelCheckoutChangesLost, StandardMessage.Severity.WarningMessage, null, null);
            sm.AddButton("no", Properties.Resources.lblNo, true, false);
            sm.AddButton("yes", Properties.Resources.lblYes, false, true);
            sm.ShowDialog();
            if (sm.ExitButtonKey == "yes")
            {
                foreach (IRepositoryNode ow in dict.Values)
                {
                    CmnObject o = (CmnObject)ow;
                    o.CancelCheckout();
                    WindowSelectionData wsd = new WindowSelectionData();
                    wsd.Selection.Add(o.Id, o);
                    wsd.Modification.Add(o.Id, o);
                    NodesModified?.Invoke(wsd);
                }
            }
        }

        public bool IsValid(Dictionary<long, IRepositoryNode> dict)
        {
            // menu item should be enabled if object can be checked out
            if (dict is null)
                return false;
            foreach (IRepositoryNode ow in dict.Values)
            {
                if (ow is CmnObject)
                {
                    if (((CmnObject)ow).Locked is null)
                        return false;
                }
                else
                {
                    return false;
                }
            }
            return true;
        }
        public bool DisplayInMenu(Dictionary<long, IRepositoryNode> dict)
        {
            // menu item should be displayed for single objects  
            if (dict is null)
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


        public Image GetIcon()
        {
            return null;
        }
    }
}