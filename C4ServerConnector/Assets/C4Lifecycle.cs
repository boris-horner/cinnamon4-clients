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
using System.Collections.Generic;
using System.Xml;

namespace C4ServerConnector.Assets
{
    public class C4Lifecycle
    {
        public string Name { get; private set; }
        public long? DefaultStateId { get; private set; }
        public Dictionary<long, C4LifecycleState> States { get; private set; }
        public long? Id { get; private set; }
        public string LocalizedName { get; set; }
        public C4Lifecycle(string name, long? defaultStateId, long? id = null) 
        {
            Name = name;
            States = new Dictionary<long, C4LifecycleState>();
            DefaultStateId = defaultStateId;
            Id = id;
        }
        public C4Lifecycle(XmlElement assetEl)
        {
            Name = assetEl.SelectSingleNode("name").InnerText;
            string defaultStateIdS = assetEl.SelectSingleNode("defaultStateId").InnerText.Trim();
            DefaultStateId = defaultStateIdS.Length > 0 ? long.Parse(defaultStateIdS) : null;
            States = new Dictionary<long, C4LifecycleState>();
            foreach(XmlElement lifecycleStateEl in assetEl.SelectNodes("lifecycleStates/lifecycleState"))
            {
                C4LifecycleState lcs = new C4LifecycleState(lifecycleStateEl);
                States.Add((long)lcs.Id, lcs);
            }
            XmlNode idN = assetEl.SelectSingleNode("id");
            if (idN == null) Id = null;
            else Id = long.Parse(idN.InnerText);
        }
        public void AppendAssetEl(XmlElement parentEl)
        {
            XmlElement assetEl = (XmlElement)parentEl.AppendChild(parentEl.OwnerDocument.CreateElement("lifecycle"));
            assetEl.AppendChild(parentEl.OwnerDocument.CreateElement("name")).InnerText = Name;
            assetEl.AppendChild(assetEl.OwnerDocument.CreateElement("defaultStateId")).InnerText = DefaultStateId == null ? "" : DefaultStateId.ToString();
            // NOTE: lifecycle states can't be updated here and are not passed
            if (Id != null) assetEl.AppendChild(assetEl.OwnerDocument.CreateElement("id")).InnerText = Id.ToString();
        }
        public override string ToString()
        {
            return LocalizedName == null ? Name : LocalizedName;
        }
    }
}
