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
using System;
using System.Collections.Generic;
using System.Xml;

namespace C4ServerConnector.Assets
{
    public class C4Permissions
    {
        public C4AllPermissions AllPermissions { get; set; }
        public SortedDictionary<string, long> PermissionIdsByName { get; set; }
        public SortedDictionary<long, string> PermissionNamesById { get; set; }

        // Convenience functions
        public bool Folder_Create_Folder { get { return GetPermission("folder.create.folder"); } set { SetPermission("folder.create.folder", value); } }
        public bool Folder_Create_Object { get { return GetPermission("folder.create.object"); } set { SetPermission("folder.create.object", value); } }
        public bool Link_Target_Write { get { return GetPermission("link.target.write"); } set { SetPermission("link.target.write", value); } }
        public bool Node_Acl_Write { get { return GetPermission("node.acl.write"); } set { SetPermission("node.acl.write", value); } }
        public bool Node_Browse { get { return GetPermission("node.browse"); } set { SetPermission("node.browse", value); } }
        public bool Node_Delete { get { return GetPermission("node.delete"); } set { SetPermission("node.delete", value); } }
        public bool Node_Metadata_Read { get { return GetPermission("node.metadata.read"); } set { SetPermission("node.metadata.read", value); } }
        public bool Node_Metadata_Write { get { return GetPermission("node.metadata.write"); } set { SetPermission("node.metadata.write", value); } }
        public bool Node_Name_Write { get { return GetPermission("node.name.write"); } set { SetPermission("node.name.write", value); } }
        public bool Node_Owner_Write { get { return GetPermission("node.owner.write"); } set { SetPermission("node.owner.write", value); } }
        public bool Node_Parent_Folder_Write{ get { return GetPermission("node.parent_folder.write"); } set { SetPermission("node.parent_folder.write", value); } }
        public bool Node_Summary_Write { get { return GetPermission("node.summary.write"); } set { SetPermission("node.summary.write", value); } }
        //public bool Node_SysMetadata_Read { get { return GetPermission("node.sysmetadata.read"); } set { SetPermission("node.sysmetadata.read", value); } }
        public bool Node_Type_Write { get { return GetPermission("node.type.write"); } set { SetPermission("node.type.write", value); } }
        public bool Object_Content_Read { get { return GetPermission("object.content.read"); } set { SetPermission("object.content.read", value); } }
        public bool Object_Content_Write { get { return GetPermission("object.content.write"); } set { SetPermission("object.content.write", value); } }
        public bool Object_Language_Write { get { return GetPermission("object.language.write"); } set { SetPermission("object.language.write", value); } }
        public bool Object_LifecycleState_Write { get { return GetPermission("object.lifecyclestate.write"); } set { SetPermission("object.lifecyclestate.write", value); } }
        public bool Object_Lock { get { return GetPermission("object.lock"); } set { SetPermission("object.lock", value); } }
        public bool Object_Version { get { return GetPermission("object.version"); } set { SetPermission("object.version", value); } }
        public bool Relation_Child_Add { get { return GetPermission("relation.child.add"); } set { SetPermission("relation.child.add", value); } }
        public bool Relation_Child_Remove { get { return GetPermission("relation.child.remove"); } set { SetPermission("relation.child.remove", value); } }
        public bool Relation_Parent_Add { get { return GetPermission("relation.parent.add"); } set { SetPermission("relation.parent.add", value); } }
        public bool Relation_Parent_Remove { get { return GetPermission("relation.parent.remove"); } set { SetPermission("relation.parent.remove", value); } }
        public C4Permissions(XmlElement assetEl, C4AllPermissions allPermissions)
        {
            AllPermissions = allPermissions;
            PermissionIdsByName = new SortedDictionary<string, long>();
            PermissionNamesById = new SortedDictionary<long, string>();
            XmlNodeList nl=assetEl.SelectNodes("permissionId");
            if(nl.Count == 0) nl = assetEl.SelectNodes("permission/id");
            foreach (XmlElement permIdEl in nl)
            {
                long permId = long.Parse(permIdEl.InnerText);
                string permName = AllPermissions.PermissionNamesById[permId];
                if(!PermissionIdsByName.ContainsKey(permName)) PermissionIdsByName.Add(permName, permId);
                if (!PermissionNamesById.ContainsKey(permId)) PermissionNamesById.Add(permId, permName);
            }
        }
        public C4Permissions(bool defaultValue, C4AllPermissions allPermissions)
        {
            AllPermissions = allPermissions;
            PermissionIdsByName = new SortedDictionary<string, long>();
            PermissionNamesById = new SortedDictionary<long, string>();
            if (defaultValue) 
                foreach(string permName in allPermissions.PermissionIdsByName.Keys)
                {
                    if (!PermissionIdsByName.ContainsKey(permName)) PermissionIdsByName.Add(permName, AllPermissions.PermissionIdsByName[permName]);
                    if (!PermissionNamesById.ContainsKey(AllPermissions.PermissionIdsByName[permName])) PermissionNamesById.Add(AllPermissions.PermissionIdsByName[permName], permName);
                }
        }
        public void SetPermission(string permName, bool value)
        {
            if (!AllPermissions.IsValidPermission(permName)) throw new ApplicationException(string.Concat("Invalid permission ", permName));
            if (value)
            {
                if (!PermissionIdsByName.ContainsKey(permName)) 
                {
                    if (!PermissionIdsByName.ContainsKey(permName)) PermissionIdsByName.Add(permName, AllPermissions.PermissionIdsByName[permName]);
                    if (!PermissionNamesById.ContainsKey(AllPermissions.PermissionIdsByName[permName])) PermissionNamesById.Add(AllPermissions.PermissionIdsByName[permName], permName);
                }
            }
            else
            {
                if (PermissionIdsByName.ContainsKey(permName)) 
                {
                    PermissionIdsByName.Remove(permName);
                    PermissionNamesById.Remove(AllPermissions.PermissionIdsByName[permName]);
                }
            }
        }
        public bool GetPermission(string permissionName)
        {
            if (!AllPermissions.IsValidPermission(permissionName)) throw new ApplicationException(string.Concat("Invalid permission ",permissionName));
            return PermissionIdsByName.ContainsKey(permissionName);
        }
        public void AppendAssetEl(XmlElement parentEl)
        {
            XmlElement assetEl = (XmlElement)parentEl.AppendChild(parentEl.OwnerDocument.CreateElement("permissions"));
            foreach(long permId in PermissionNamesById.Keys)
            {
                XmlElement permIdEl = (XmlElement)assetEl.AppendChild(parentEl.OwnerDocument.CreateElement("permissionId"));
                permIdEl.InnerText = permId.ToString();
            }
        }
    }
}
