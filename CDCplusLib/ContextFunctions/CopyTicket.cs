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
    public class CopyTicket : IGenericFunction
    {
        private GlobalApplicationData _gad;
        private readonly Image _image;
        private CmnSession _s;

        public void AppendSubmenu(ToolStripMenuItem cmi)
        {
        }

        public bool HasSubmenuItems()
        {
            return false;
        }

        public CopyTicket()
        {
            string appPath = Application.ExecutablePath;
            _image = Image.FromFile(Path.Combine(Path.GetDirectoryName(appPath), "Images", "Icons", "16x16", "view-barcode.png"));

        }

        public string GetMenuText()
        {
            return Properties.Resources.mnuCopyTicket;
        }

        public string InstanceName { get; set; }

        public event IGenericFunction.MessageSentEventHandler MessageSent;

        public void Reset(CmnSession s, GlobalApplicationData globalAppData, XmlElement configEl)
        {
            _s = s;
            _gad = globalAppData;
        }

        public Image GetIcon()
        {
            return _image;
        }

        public bool DisplayInMenu(Dictionary<long, IRepositoryNode> dict)
        {
            return dict==null;
        }

        public void Execute(Dictionary<long, IRepositoryNode> dict)
        {
            Clipboard.SetText(_s.Ticket);
        }

        public bool IsValid(Dictionary<long, IRepositoryNode> dict)
        {
            return dict == null;
        }
    }
}