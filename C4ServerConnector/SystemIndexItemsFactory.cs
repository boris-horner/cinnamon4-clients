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
using C4ServerConnector.Assets;
using System.Collections.Generic;

namespace C4ServerConnector
{
    internal class SystemIndexItemsFactory
    {
        static internal Dictionary<string, C4IndexItem> GetSystemIndexItemsByName()
        {
            Dictionary<string, C4IndexItem> result = new Dictionary<string, C4IndexItem>();

            result.Add("folderpath", new C4IndexItem("folderpath", "folderpath", "", "", "COMPLETE_STRING_INDEXER", false, false));
            result.Add("acl", new C4IndexItem("acl", "acl", "", "", "INTEGER_INDEXER", false, false));
            result.Add("cmn_version", new C4IndexItem("cmn_version", "cmn_version", "", "", "COMPLETE_STRING_INDEXER", false, false));
            result.Add("content_changed", new C4IndexItem("content_changed", "content_changed", "", "", "BOOLEAN_INDEXER", false, false));
            result.Add("content_size", new C4IndexItem("content_size", "content_size", "", "", "INTEGER_INDEXER", false, false));
            result.Add("created", new C4IndexItem("created", "created", "", "", "DATE_TIME_INDEXER", false, false));   
            result.Add("modified", new C4IndexItem("modified", "modified", "", "", "DATE_TIME_INDEXER", false, false));
            result.Add("creator", new C4IndexItem("creator", "creator", "", "", "INTEGER_INDEXER", false, false));
            result.Add("modifier", new C4IndexItem("modifier", "modifier", "", "", "INTEGER_INDEXER", false, false));
            result.Add("format", new C4IndexItem("format", "format", "", "", "INTEGER_INDEXER", false, false));
            result.Add("language", new C4IndexItem("language", "language", "", "", "INTEGER_INDEXER", false, false));
            result.Add("latest_branch", new C4IndexItem("latest_branch", "latest_branch", "", "", "BOOLEAN_INDEXER", false, false));
            result.Add("latest_head", new C4IndexItem("latest_head", "latest_head", "", "", "BOOLEAN_INDEXER", false, false));
            result.Add("locker", new C4IndexItem("locker", "locker", "", "", "INTEGER_INDEXER", false, false));
            result.Add("metadata_changed", new C4IndexItem("metadata_changed", "metadata_changed", "", "", "BOOLEAN_INDEXER", false, false));
            result.Add("name", new C4IndexItem("name", "name", "", "", "COMPLETE_STRING_INDEXER", false, false));
            result.Add("owner", new C4IndexItem("owner", "owner", "", "", "INTEGER_INDEXER", false, false));
            result.Add("parent", new C4IndexItem("parent", "parent", "", "", "INTEGER_INDEXER", false, false));
            result.Add("predecessor", new C4IndexItem("predecessor", "predecessor", "", "", "INTEGER_INDEXER", false, false));
            result.Add("root", new C4IndexItem("root", "root", "", "", "INTEGER_INDEXER", false, false));
            result.Add("lifecycle_state", new C4IndexItem("lifecycle_state", "lifecycle_state", "", "", "INTEGER_INDEXER", false, false));
            result.Add("summary", new C4IndexItem("summary", "summary", "", "", "DEFAULT_TEXT_INDEXER", false, false));
            result.Add("object_type", new C4IndexItem("object_type", "object_type", "", "", "INTEGER_INDEXER", false, false));
            result.Add("folder_type", new C4IndexItem("folder_type", "folder_type", "", "", "INTEGER_INDEXER", false, false));
            //result.Add("child.relations", new C4IndexItem("child.relations", "child.relations", "", "", "INTEGER_INDEXER", true, false));
            //result.Add("parent.relations", new C4IndexItem("parent.relations", "parent.relations", "", "", "INTEGER_INDEXER", true, false));
            return result;
        }
    }
}
