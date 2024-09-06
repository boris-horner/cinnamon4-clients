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
    public class C4Link
    {
        public enum LinkTypes { Object, Folder }
        public long? Id { get; private set; }
        public long AclId { get; set; }
        public long ParentId { get; set; }
        public long OwnerId { get; set; }
        public long RepositoryNodeId { get;  set; }
        public LinkTypes LinkType { get; private set; }
        public C4Link(long aclId, long parentId, long ownerId, LinkTypes linkType, long repositoryNodeId, long? id=null) 
        {
            Id = id;
            AclId = aclId;
            ParentId = parentId;
            OwnerId = ownerId;
            LinkType = linkType;   
            RepositoryNodeId = repositoryNodeId;
        }
        public C4Link(XmlElement assetEl)
        {
            XmlNode idN = assetEl.SelectSingleNode("id");
            if (idN == null) Id = null;
            else Id = long.Parse(idN.InnerText);
            ParentId = long.Parse(assetEl.SelectSingleNode("aclId").InnerText);
            ParentId = long.Parse(assetEl.SelectSingleNode("parentId").InnerText);
            OwnerId = long.Parse(assetEl.SelectSingleNode("ownerId").InnerText);
            LinkType = assetEl.SelectSingleNode("type").InnerText=="OBJECT"?LinkTypes.Object:LinkTypes.Folder;
            if(LinkType==LinkTypes.Object) RepositoryNodeId = long.Parse(assetEl.SelectSingleNode("objectId").InnerText);
            else RepositoryNodeId = long.Parse(assetEl.SelectSingleNode("folderId").InnerText);
        }
        public void AppendAssetEl(XmlElement parentEl)
        {
            XmlElement assetEl = (XmlElement)parentEl.AppendChild(parentEl.OwnerDocument.CreateElement("link"));
            if (Id != null) assetEl.AppendChild(assetEl.OwnerDocument.CreateElement("id")).InnerText = Id.ToString();
            assetEl.AppendChild(assetEl.OwnerDocument.CreateElement("aclId")).InnerText = ParentId.ToString();
            assetEl.AppendChild(assetEl.OwnerDocument.CreateElement("parentId")).InnerText = ParentId.ToString();
            assetEl.AppendChild(assetEl.OwnerDocument.CreateElement("ownerId")).InnerText = OwnerId.ToString();
            assetEl.AppendChild(assetEl.OwnerDocument.CreateElement("type")).InnerText = LinkType==LinkTypes.Object?"OBJECT":"FOLDER";
            assetEl.AppendChild(assetEl.OwnerDocument.CreateElement("folderId")).InnerText = LinkType == LinkTypes.Object ? "" : RepositoryNodeId.ToString();
            assetEl.AppendChild(assetEl.OwnerDocument.CreateElement("objectId")).InnerText = LinkType == LinkTypes.Object ? RepositoryNodeId.ToString() : "";
        }
        public override string ToString()
        {
            return Id==null?"(null)":Id.ToString();
        }
    }
}
