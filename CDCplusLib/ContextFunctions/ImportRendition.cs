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
using C4ObjectApi.Helpers;
using C4ServerConnector.Assets;
using CDCplusLib.Common.GUI;
using CDCplusLib.EventData;

namespace CDCplusLib.ContextFunctions
{

    public class ImportRendition : IGenericFunction
    {

        private GlobalApplicationData _gad;
        private CmnSession _s;
        public void AppendSubmenu(ToolStripMenuItem cmi, Dictionary<long, IRepositoryNode> dict)
        {

        }

        public bool HasSubmenuItems(Dictionary<long, IRepositoryNode> dict)
        {
            return false;
        }

        public string GetMenuText()
        {
            return Properties.Resources.mnuImportRendition;
        }

        public Image GetIcon()
        {
            return null;
        }
        public void Execute(Dictionary<long, IRepositoryNode> dict)
        {
            CmnObject o = DictionaryHelper.GetSingleObject(dict);
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.InitialDirectory = Environment.CurrentDirectory;
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                C4Format fmt = o.Session.SessionConfig.C4Sc.FormatsByExtension[Path.GetExtension(ofd.FileName).Substring(1)];
                CmnObject newO = o.Parent.ImportObject(ofd.FileName, o.Name, o.Session.SessionConfig.C4Sc.ObjectTypesByName["_rendition"], o.Language, fmt);
                HashSet<C4Relation> relations = new HashSet<C4Relation>();
                relations.Add(new C4Relation((long)o.Session.SessionConfig.C4Sc.RelationTypesByName["rendition"].Id, o.Id, newO.Id, null));
                _s.CommandSession.CreateRelations(relations);
                WindowSelectionData wsd = new WindowSelectionData();
                wsd.Selection.Add(newO.Id, newO);
                wsd.Modification.Add(newO.Id, newO);
                NodesModified?.Invoke(wsd); 
                Environment.CurrentDirectory = Path.GetDirectoryName(ofd.FileName);
            }
        }

        public bool IsValid(Dictionary<long, IRepositoryNode> dict)
        {
            CmnObject o = DictionaryHelper.GetSingleObject(dict);
            // conditions on o for enabling cancel checkout  
            if (o is null)
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
            _s = s;
            _gad = globalAppData;
        }
    }
}