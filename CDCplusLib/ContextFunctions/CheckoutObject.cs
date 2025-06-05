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
using C4GeneralGui.GuiElements;
using CDCplusLib.EventData;

namespace CDCplusLib.ContextFunctions
{

    public class CheckoutObject : IGenericFunction
    {

        private GlobalApplicationData _gad;
        private IExportResolver _exportResolver;
        private CmnObject.ChildExportPolicy _cep;

        public string GetMenuText()
        {
            return Properties.Resources.mnuCheckout;
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
            CmnObject o = DictionaryHelper.GetSingleObject(dict);
            o.Checkout(_cep, _exportResolver);

            StandardMessage sm = new StandardMessage(Properties.Resources.msgCheckoutOpenInAssociatedApp, StandardMessage.Severity.InfoMessage, null, null);
            sm.AddButton("no", Properties.Resources.lblNo, true, false);
            sm.AddButton("yes", Properties.Resources.lblYes, false, true);
            sm.ShowDialog();
            if (sm.ExitButtonKey == "yes")
            {
				ProcessStartInfo psi = new ProcessStartInfo
				{
					FileName = o.LocalPath,
					UseShellExecute = true
				};

				Process.Start(psi);
			}
            WindowSelectionData wsd = new WindowSelectionData();
            wsd.Selection.Add(o.Id, o);
            wsd.Modification.Add(o.Id, o);
            NodesModified?.Invoke(wsd);
        }

        public bool IsValid(Dictionary<long, IRepositoryNode> dict)
        {
            CmnObject o = DictionaryHelper.GetSingleObject(dict);
            // conditions on cmno for enabling checkout  
            if (o is null)
                return false;
            if (o.Locked != null)
                return false;
            if (o.Format == null)
                return false;
            if (!o.Permissions.Object_Lock)
                return false;
            return true;
        }

        public bool DisplayInMenu(Dictionary<long, IRepositoryNode> dict)
        {
            CmnObject o = DictionaryHelper.GetSingleObject(dict);
            return o is not null;
        }
        public string InstanceName { get; set; }
        public event IGenericFunction.SessionWindowRequestEventHandler SessionWindowRequest;
        public event IGenericFunction.NodesModifiedEventHandler NodesModified;

        public void Reset(CmnSession s, GlobalApplicationData globalAppData, XmlElement configEl)
        {
            _gad = globalAppData;
            XmlElement expResEl = (XmlElement)configEl.SelectSingleNode("custom/export_resolver");
            if (expResEl is not null)
            {
                string asm = expResEl.GetAttribute("assembly");
                string tp = expResEl.GetAttribute("type");
                _exportResolver = (IExportResolver)s.SessionConfig.GetAssembly(asm).CreateInstance(asm + "." + tp);
            }
            XmlNode cebN = configEl.SelectSingleNode("custom/child_export_behavior");
            if (cebN is null)
            {
                _cep = CmnObject.ChildExportPolicy.ObjectAndDirectChildren;
            }
            else
            {
                switch (cebN.InnerText ?? "")
                {
                    case "descendents":
                        {
                            _cep = CmnObject.ChildExportPolicy.ObjectAndDescendants;
                            break;
                        }
                    case "children":
                        {
                            _cep = CmnObject.ChildExportPolicy.ObjectAndDirectChildren;
                            break;
                        }
                    case "object":
                        {
                            _cep = CmnObject.ChildExportPolicy.ObjectOnly;
                            break;
                        }
                }
            }
        }
    }
}