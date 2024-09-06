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

namespace C4ServerConnector.Assets
{
    public class C4LifecycleState
    {
        public string Name { get; private set; }
        public XmlDocument Config { get; private set; }
        public string StateClass { get; private set; }
        public long LifecycleId { get; private set; }
        public long? LifecycleStateForCopyId { get; private set; }
        public long? Id { get; private set; }
        public string LocalizedName { get; set; }
        public C4LifecycleState(string name, XmlDocument config, string stateClass, long lifecycleId, long? lifecycleStateForCopyId, long? id=null) 
        {
            Name = name;
            Config = config;
            StateClass = stateClass;
            LifecycleId = lifecycleId;
            LifecycleStateForCopyId = lifecycleStateForCopyId;
            Id = id;
        }
        public C4LifecycleState(XmlElement assetEl)
        {
            Name = assetEl.SelectSingleNode("name").InnerText;
            Config = new XmlDocument();
            Config.LoadXml(assetEl.SelectSingleNode("config").InnerText);
            StateClass = assetEl.SelectSingleNode("stateClass").InnerText;
            LifecycleId = long.Parse(assetEl.SelectSingleNode("lifecycleId").InnerText);
            string lifecycleStateForCopyIdS = assetEl.SelectSingleNode("lifecycleStateForCopyId").InnerText.Trim();
            LifecycleStateForCopyId = lifecycleStateForCopyIdS.Length > 0 ? long.Parse(lifecycleStateForCopyIdS) : null;
            XmlNode idN = assetEl.SelectSingleNode("id");
            if (idN == null || idN.InnerText.Trim().Length==0) Id = null;
            else Id = long.Parse(idN.InnerText);
        }
        public void AppendAssetEl(XmlElement parentEl)
        {
            XmlElement assetEl = (XmlElement)parentEl.AppendChild(parentEl.OwnerDocument.CreateElement("lifecycleState"));
            assetEl.AppendChild(parentEl.OwnerDocument.CreateElement("name")).InnerText = Name;
            assetEl.AppendChild(parentEl.OwnerDocument.CreateElement("config")).InnerText = Config.OuterXml;
            assetEl.AppendChild(parentEl.OwnerDocument.CreateElement("stateClass")).InnerText = StateClass;
            assetEl.AppendChild(assetEl.OwnerDocument.CreateElement("lifecycleId")).InnerText = LifecycleId.ToString();
            assetEl.AppendChild(assetEl.OwnerDocument.CreateElement("lifecycleStateForCopyId")).InnerText = LifecycleStateForCopyId == null ? "" : LifecycleStateForCopyId.ToString();
            if (Id != null) assetEl.AppendChild(assetEl.OwnerDocument.CreateElement("id")).InnerText = Id.ToString();
        }
        public override string ToString()
        {
            return LocalizedName == null ? Name : LocalizedName;
        }
    }
}
