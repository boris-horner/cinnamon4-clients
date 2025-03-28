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

namespace CDCplusLib.ContextFunctions
{

    public class ExportSelection : IGenericFunction
    {

        private GlobalApplicationData _gad;
        private IExportResolver _exportResolver;
        private CmnObject.ChildExportPolicy _cep;
        public void AppendSubmenu(ToolStripMenuItem cmi, Dictionary<long, IRepositoryNode> dict)
        {

        }

        public bool HasSubmenuItems(Dictionary<long, IRepositoryNode> dict)
        {
            return false;
        }

        public string GetMenuText()
        {
            return Properties.Resources.mnuExport;
        }
        public void Execute(Dictionary<long, IRepositoryNode> dict)
        {
            FolderBrowserDialog fb = new FolderBrowserDialog();
            fb.SelectedPath = Environment.CurrentDirectory;
            if (fb.ShowDialog() == DialogResult.OK)
            {
                Environment.CurrentDirectory = fb.SelectedPath;
                foreach (IRepositoryNode ow in dict.Values)
                {
                    CmnObject o = (CmnObject)ow;
                    o.Export(_cep, true, fb.SelectedPath, _exportResolver);
                }
                StandardMessage.ShowMessage(string.Format(Properties.Resources.msgSuccessfullyExported, fb.SelectedPath), StandardMessage.Severity.InfoMessage);
            }
        }

        public bool IsValid(Dictionary<long, IRepositoryNode> dict)
        {
            // menu item should be enabled if object can be checked out
            if (dict is null)
                return false;
            foreach (IRepositoryNode ow in dict.Values)
            {
                if (!(ow is CmnObject))
                    return false;
                if (((CmnObject)ow).Format is null)
                    return false;
            }
            return true;
        }
        public bool DisplayInMenu(Dictionary<long, IRepositoryNode> dict)
        {
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
                _cep = CmnObject.ChildExportPolicy.ObjectAndDescendants;
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

        public Image GetIcon()
        {
            return null;
        }
    }
}