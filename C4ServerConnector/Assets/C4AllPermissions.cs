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
    public class C4AllPermissions
    {
        public SortedDictionary<string, long> PermissionIdsByName {  get; set; }
        public SortedDictionary<long, string> PermissionNamesById { get; set; }
        public C4AllPermissions(XmlElement assetEl)
        {
            PermissionIdsByName = new SortedDictionary<string, long>();
            PermissionNamesById = new SortedDictionary<long, string>();
            foreach (XmlElement permissionEl in assetEl.SelectNodes("permission"))
            {
                PermissionIdsByName.Add(permissionEl.SelectSingleNode("name").InnerText, long.Parse(permissionEl.SelectSingleNode("id").InnerText));
                PermissionNamesById.Add(long.Parse(permissionEl.SelectSingleNode("id").InnerText), permissionEl.SelectSingleNode("name").InnerText);
            }
        }
        public C4Permissions AndPermissions(HashSet<C4Permissions> permissionsSets)
        {
            //C4Permissions result = new C4Permissions(true, this);
            C4Permissions result = new C4Permissions(false, this);
            bool first = true;
            foreach(C4Permissions permissions in permissionsSets)
            {
                if(first)
                {
                    first = false;
                    // first: add all permissions
                    foreach(string permissionName in permissions.PermissionIdsByName.Keys)
                    {
                        result.PermissionIdsByName.Add(permissionName, permissions.PermissionIdsByName[permissionName]);
                        result.PermissionNamesById.Add(permissions.PermissionIdsByName[permissionName], permissionName);
                    }

                }
                else
                {
                    // following: remove all that are not contained
                    HashSet<string> del = new HashSet<string>();
                    foreach (string permName in result.PermissionIdsByName.Keys) if(!permissions.PermissionIdsByName.ContainsKey(permName)) del.Add(permName);
                    foreach (string permName in del)
                    {
                        result.PermissionIdsByName.Remove(permName);
                        result.PermissionNamesById.Remove(PermissionIdsByName[permName]);
                    }
                }
            }
            return result;
        }
        public bool IsValidPermission(string permissionName)
        {
            return PermissionIdsByName.ContainsKey(permissionName);
        }
    }
}
