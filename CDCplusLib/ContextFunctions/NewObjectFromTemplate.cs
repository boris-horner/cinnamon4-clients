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
using CDCplusLib.DataModel;
using CDCplusLib.Interfaces;
using C4ObjectApi.Interfaces;
using C4ObjectApi.Repository;
using C4ObjectApi.Helpers;
using C4GeneralGui.GuiElements;
using CDCplusLib.EventData;

namespace CDCplusLib.ContextFunctions
{
    public class NewObjectFromTemplate : IGenericFunction
    {
        private GlobalApplicationData _gad;
        private TemplateContainer _rootTemplateContainer;
        private CmnSession _s;
        private CmnFolder _lastFolder; // set by IsValid
        private XmlElement _configEl;

        public string GetMenuText()
        {
            return Properties.Resources.mnuNewObjectFromTemplate;
        }

        private void InitRootTemplateContainer()
        {
            XmlNode tpN = _configEl.SelectSingleNode("custom/template_path");
            if (_rootTemplateContainer is null)
            {
                string tp = tpN is null ? "/templates" : tpN.InnerText;
                CmnFolder templateFolder = _s.GetFolder(tp);
                _rootTemplateContainer = new TemplateContainer(templateFolder);
            }
        }

        public void AppendSubmenu(ToolStripMenuItem cmi, Dictionary<long, IRepositoryNode> dict)
        {
            InitRootTemplateContainer();
            AppendSubmenuR(cmi, _rootTemplateContainer);
        }

        private void AppendSubmenuR(ToolStripMenuItem parentTsmi, TemplateContainer parentTc)
        {
            foreach (TemplateContainer tc in parentTc.Containers.Values)
            {
                ToolStripMenuItem tsmi = new ToolStripMenuItem(tc.Name); 
                parentTsmi.DropDownItems.Add(tsmi);
                AppendSubmenuR(tsmi, tc);
            }

            foreach (CmnObject o in parentTc.Templates.Values)
            {
                ToolStripMenuItem tsmi = new ToolStripMenuItem(o.Name);
                tsmi.Tag = o;
                parentTsmi.DropDownItems.Add(tsmi);
                tsmi.Click += HandleContextMenuClick;
            }
        }

        protected void HandleContextMenuClick(object sender, EventArgs e)
        {
            try
            {
                ToolStripMenuItem tsmi = (ToolStripMenuItem)sender;
                CmnObject o = (CmnObject)tsmi.Tag;
                CmnObject newO = null;
                if (_lastFolder is object)
                {
                    newO = o.Copy(_lastFolder);
                    WindowSelectionData wsd = new WindowSelectionData();
                    wsd.Selection.Add(newO.Id, newO);
                    wsd.Modification.Add(newO.Id, newO);
                    NodesModified?.Invoke(wsd);
                }
            }
            catch (Exception ex)
            {
                StandardMessage.ShowMessage(Properties.Resources.msgFailureCreatingObject, StandardMessage.Severity.ErrorMessage, ex: ex);
            }
        }

        public bool HasSubmenuItems(Dictionary<long, IRepositoryNode> dict)
        {
            return true;
        }

        public void Execute(Dictionary<long, IRepositoryNode> dict)
        {
            // nothing to do
        }

        public bool IsValid(Dictionary<long, IRepositoryNode> dict)
        {
            CmnFolder f = DictionaryHelper.GetSingleFolder(dict);
            _lastFolder = f;
            if (f is null)
                return false;
            if (!f.Permissions.Folder_Create_Folder)
                return false;
            return true;
        }

        public bool DisplayInMenu(Dictionary<long, IRepositoryNode> dict)
        {
            CmnFolder f = DictionaryHelper.GetSingleFolder(dict);
            return f is object;
        }

        public string InstanceName { get; set; }

        public event IGenericFunction.SessionWindowRequestEventHandler SessionWindowRequest;
        public event IGenericFunction.NodesModifiedEventHandler NodesModified;

        public void Reset(CmnSession s, GlobalApplicationData globalAppData, XmlElement configEl)
        {
            _s = s;
            _gad = globalAppData;
            _configEl = configEl;
        }

        public System.Drawing.Image GetIcon()
        {
            return null;
        }
    }
}