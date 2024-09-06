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
using C4ObjectApi.Interfaces;
using C4ObjectApi.Repository;

namespace CDCplusLib.Common
{
    public class CopyOperation
    {
        public Dictionary<long, IRepositoryNode> ResolveAllNodes(Dictionary<long, IRepositoryNode> rootNodes)
        {
            if (rootNodes.Count() > 0)
            {
                string failedItemsList = null;
                Dictionary<long, long> objRootIds = new Dictionary<long, long>();
                Dictionary<long, CmnFolder> folders = new Dictionary<long, CmnFolder>();
                CmnSession s = null;

                // pass 1: get folders and ids of root objects
                foreach (IRepositoryNode ow in rootNodes.Values)
                {
                    if (s == null) s = ow.Session;
                    if (ow.GetType() == typeof(CmnFolder))
                    {
                        if (!ow.Permissions.Node_Browse)
                        {
                            if (failedItemsList == null) failedItemsList = ow.Name;
                            else failedItemsList += "\n" + ow.Name;
                        }
                        else
                        {
                            if (!folders.ContainsKey(ow.Id)) AddFolder(folders, objRootIds, (CmnFolder)ow);
                        }
                    }
                    else
                    {
                        if (!ow.Permissions.Node_Browse || !ow.Permissions.Object_Content_Read || !ow.Permissions.Node_Metadata_Read)
                        {
                            if (failedItemsList == null) failedItemsList = ow.Name;
                            else failedItemsList += "\n" + ow.Name;
                        }
                        else
                        {
                            CmnObject subO = (CmnObject)ow;
                            if (!objRootIds.ContainsKey(subO.RootId)) objRootIds.Add(subO.RootId, subO.RootId);
                        }
                    }
                }

                // pass 2: retrieve all source objects
                string query = "<?xml version=\"1.0\" encoding=\"UTF-8\"?><BooleanQuery minimumNumberShouldMatch=\"1\">";
                foreach (long rootId in objRootIds.Keys)
                    query += "<Clause occurs=\"should\"><ExactPointQuery type=\"long\" fieldName=\"root\" value=\"" + rootId.ToString() + "\"/></Clause>";
                query += "</BooleanQuery>";

                Dictionary<long, IRepositoryNode> result = new Dictionary<long, IRepositoryNode>();
                foreach (CmnFolder f in folders.Values) result.Add(f.Id, f);
                foreach (CmnObject o in s.SearchObjects(query).Values) result.Add(o.Id, o);
                return result;

            }
            else return new Dictionary<long, IRepositoryNode>();
        }

        private void AddFolder(Dictionary<long, CmnFolder> folders, Dictionary<long, long> objRootIds, CmnFolder f)
        {
            if(!folders.ContainsKey(f.Id))
            {
                folders.Add(f.Id, f);
                foreach(CmnFolder subF in f.GetSubfolders().Values) AddFolder(folders, objRootIds, subF);
                foreach (CmnObject subO in f.GetObjects(CmnSession.Versions.All).Values)
                {
                    if (!objRootIds.ContainsKey(subO.RootId)) objRootIds.Add(subO.RootId, subO.RootId);
                }
            }
        }
    }
}
