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
    public class C4Group
    {
        public string Name { get; private set; }
        public long? Id { get; private set; }
        public string LocalizedName { get; set; }
        public long? ParentId { get; private set; }
        //public Dictionary<long, C4User> Users { get; private set; }
        public C4Group(string name, long? parentId, long? id=null) 
        {
            Name = name;
            Id = id;
            ParentId = parentId;
            //Users=new Dictionary<long, C4User>();
        }
        public C4Group(XmlElement assetEl)
        {
            Name = assetEl.SelectSingleNode("name").InnerText;
            XmlNode pidN = assetEl.SelectSingleNode("parentId");
            if (pidN.InnerText.Length==0) ParentId = null;
            else ParentId = long.Parse(pidN.InnerText);
            XmlNode idN = assetEl.SelectSingleNode("id");
            if (idN == null) Id = null;
            else Id = long.Parse(idN.InnerText);
            //Users = new Dictionary<long, C4User>();
            // TODO: append users
        }
        public void AppendAssetEl(XmlElement parentEl)
        {
            XmlElement assetEl = (XmlElement)parentEl.AppendChild(parentEl.OwnerDocument.CreateElement("group"));
            assetEl.AppendChild(parentEl.OwnerDocument.CreateElement("name")).InnerText = Name;
            //if (ParentId != null) groupEl.AppendChild(parentEl.OwnerDocument.CreateElement("parentId")).InnerText = ParentId.ToString();
            assetEl.AppendChild(parentEl.OwnerDocument.CreateElement("parentId")).InnerText = (ParentId != null) ? ParentId.ToString():"";
            if (Id != null) assetEl.AppendChild(parentEl.OwnerDocument.CreateElement("id")).InnerText = Id.ToString();
        }
        public override string ToString()
        {
            return LocalizedName == null ? Name : LocalizedName;
        }
    }
}
