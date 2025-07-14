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
using C4ServerConnector;
using C4ServerConnector.Assets;
using System.Xml;

namespace CAELib.Helpers
{
    public class ApiHelper
    {
        public static Dictionary<long, HashSet<C4Metaset>> GetCreateMetasetsData(long objId, C4Metaset ms)
        {
            Dictionary<long, HashSet<C4Metaset>> result = new Dictionary<long, HashSet<C4Metaset>>();
            result[objId] = new HashSet<C4Metaset>();
            result[objId].Add(ms);
            return result;
        }
        //public static HashSet<long> IdToHashSet(long id)
        //{
        //    HashSet<long> result = new HashSet<long>();
        //    result.Add(id);
        //    return result;
        //}
        public static C4Relation CreateSingleRelation(long leftId, long rightId, long typeId, XmlDocument metadata, C4Session c4s)
        {
            HashSet<C4Relation> createRelations = new HashSet<C4Relation>();
            createRelations.Add(new C4Relation(typeId, leftId, rightId, metadata));
            return c4s.CreateRelations(createRelations).Values.First();
        }
        public static HashSet<long> GetAllVersionIds(long rootId, C4Session c4s)
        {
            string query = "<?xml version=\"1.0\" encoding=\"UTF-8\"?><BooleanQuery><Clause occurs=\"must\"><ExactPointQuery type=\"long\" fieldName=\"root\" value=\"" + rootId.ToString() + "\"/></Clause></BooleanQuery>";
            return c4s.SearchObjectIds(query);
        }
        public static long? GetLatestHeadId(long rootId, C4Session c4s)
        {
            string query = "<?xml version=\"1.0\" encoding=\"UTF-8\"?><BooleanQuery><Clause occurs=\"must\"><ExactPointQuery type=\"long\" fieldName=\"root\" value=\"" + rootId.ToString() + "\"/></Clause><Clause occurs=\"must\"><TermQuery fieldName=\"latest_head\">true</TermQuery></Clause></BooleanQuery>";
            HashSet<long> searchResult = c4s.SearchObjectIds(query);
            if (searchResult.Count == 0) return null;
            else return searchResult.First();
        }
    }
}