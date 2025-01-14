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
    public class C4Folder : IC4Node
    {
        public string Name { get; set; }
        public long Id { get; set; }
        public long OwnerId { get; set; }
        public long AclId { get; set; }
        public long ParentId { get; set; }
        public long TypeId { get; set; }
        public bool HasSubfolders { get; }
        public bool MetadataChanged { get; set; }
        public C4Link Link { get; set; }
        public C4Metadata Metadata { get; set; }
        public XmlElement Summary { get; set; }
        public C4Session.NodeType NodeType { get; private set; }

        public C4Folder(XmlElement assetEl)
        {
            NodeType = C4Session.NodeType.Folder;
            Id = long.Parse(assetEl.SelectSingleNode("id").InnerText);
            Name = assetEl.SelectSingleNode("name").InnerText;
            OwnerId = long.Parse(assetEl.SelectSingleNode("ownerId").InnerText);
            AclId = long.Parse(assetEl.SelectSingleNode("aclId").InnerText);
            string parentId = assetEl.SelectSingleNode("parentId").InnerText;
            ParentId = parentId.Length == 0 ? 0 : long.Parse(assetEl.SelectSingleNode("parentId").InnerText);
            TypeId = long.Parse(assetEl.SelectSingleNode("typeId").InnerText);
            MetadataChanged = assetEl.SelectSingleNode("metadataChanged").InnerText == "true";
            HasSubfolders = assetEl.SelectSingleNode("hasSubfolders").InnerText == "true";   // TODO: real value
        }
        public C4Folder(long id, string name, long ownerId, long parentId, long typeId, long aclId)
        {
            Id = id;
            Name = name;
            OwnerId = ownerId;
            AclId = aclId;
            ParentId = parentId;
            TypeId = typeId;
            MetadataChanged = false;
            HasSubfolders = false;
        }
        public void AppendAssetEl(XmlElement parentEl)
        {
            XmlElement assetEl = (XmlElement)parentEl.AppendChild(parentEl.OwnerDocument.CreateElement("folder"));
            if (Id != 0) assetEl.AppendChild(assetEl.OwnerDocument.CreateElement("id")).InnerText = Id.ToString();
            assetEl.AppendChild(parentEl.OwnerDocument.CreateElement("name")).InnerText = Name;
            assetEl.AppendChild(parentEl.OwnerDocument.CreateElement("aclId")).InnerText = AclId.ToString();
            assetEl.AppendChild(parentEl.OwnerDocument.CreateElement("ownerId")).InnerText = OwnerId.ToString();
            assetEl.AppendChild(parentEl.OwnerDocument.CreateElement("parentId")).InnerText = ParentId.ToString();
            assetEl.AppendChild(parentEl.OwnerDocument.CreateElement("typeId")).InnerText = TypeId.ToString();
            assetEl.AppendChild(parentEl.OwnerDocument.CreateElement("metadataChanged")).InnerText = MetadataChanged ? "true" : "false";
        }
        public override string ToString()
        {
            return Name;
        }
    }
}
