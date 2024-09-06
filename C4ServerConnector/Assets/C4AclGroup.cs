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
    public class C4AclGroup
    {
        public C4AllPermissions AllPermissions { get; set; }
        public long? Id { get; private set; }
        public long AclId { get; private set; }
        public long GroupId { get; private set; }
        public C4Permissions Permissions { get; private set; }

        public C4AclGroup(long aclId, long groupId, C4Permissions permissions, C4AllPermissions allPermissions, long? id=null) 
        {
            AllPermissions = allPermissions;
            AclId = aclId;
            GroupId = groupId;
            Permissions = permissions;
            Id = id;
        }
        public C4AclGroup(XmlElement assetEl, C4AllPermissions allPermissions)
        {
            AllPermissions = allPermissions;
            Permissions = new C4Permissions(assetEl.SelectSingleNode("permissions") as XmlElement, AllPermissions);
            XmlNode idN = assetEl.SelectSingleNode("id");
            if (idN == null) Id = null;
            else Id = long.Parse(idN.InnerText);
            AclId = long.Parse(assetEl.SelectSingleNode("aclId").InnerText);
            GroupId = long.Parse(assetEl.SelectSingleNode("groupId").InnerText);
        }
        public void AppendAssetEl(XmlElement parentEl)
        {
            XmlElement assetEl = (XmlElement)parentEl.AppendChild(parentEl.OwnerDocument.CreateElement("aclGroup"));
            assetEl.AppendChild(parentEl.OwnerDocument.CreateElement("aclId")).InnerText = AclId.ToString();
            assetEl.AppendChild(parentEl.OwnerDocument.CreateElement("groupId")).InnerText = GroupId.ToString();
            if (Id != null) assetEl.AppendChild(assetEl.OwnerDocument.CreateElement("id")).InnerText = Id.ToString();
            Permissions.AppendAssetEl(assetEl);
        }
    }
}
