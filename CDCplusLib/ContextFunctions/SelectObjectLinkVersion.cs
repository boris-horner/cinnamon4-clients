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

namespace CDCplusLib.ContextFunctions
{

    public class SelectObjectLinkVersion : IGenericFunction
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
            return Properties.Resources.mnuSelectLinkVersion;
        }
        public void Execute(Dictionary<long, IRepositoryNode> dict)
        {
            CmnObject o = DictionaryHelper.GetSingleObject(dict);
            SelectVersion vs = new SelectVersion(o);
            if (vs.ShowDialog() == DialogResult.OK)
            {
                //CmnObject newO = null;
                if (vs.FixedVersion)
                {
                    o.Link.Update(true, newTarget: vs.SelectedVersion);
                }
                else
                {
                    o.Link.Update(false);
                }


                //ObjectVersionedMessage msg;
                //msg = new ObjectVersionedMessage();
                //msg.OldVersion = o;
                //msg.NewVersion = newO;
                //// msg.Source = instanceName_
                //MessageSent?.Invoke(msg);
            }
        }
        public bool IsValid(Dictionary<long, IRepositoryNode> dict)
        {
            CmnObject o = DictionaryHelper.GetSingleObject(dict);
            if (o is null)
                return false;
            if (o.Link is null)
                return false;
            // If Not (o.Permissions.WriteObjectSysmeta) Then Return False
            return true;
        }
        public bool DisplayInMenu(Dictionary<long, IRepositoryNode> dict)
        {
            CmnObject o = DictionaryHelper.GetSingleObject(dict);
            if (o is null)
                return false;
            if (o.Link is null)
                return false;
            return true;
        }
        public string InstanceName { get; set; }
        public event IGenericFunction.MessageSentEventHandler MessageSent;

        public void Reset(CmnSession s, GlobalApplicationData globalAppData, XmlElement configEl)
        {
            _gad = globalAppData;
        }
    }
}