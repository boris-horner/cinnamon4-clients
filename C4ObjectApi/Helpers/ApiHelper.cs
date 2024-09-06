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
using C4ObjectApi.Repository;
using C4ServerConnector;
using C4ServerConnector.Assets;
using System.Collections.Generic;
using System.Linq;
using System.Xml;

namespace C4ObjectApi.Helpers
{
    public class ApiHelper
    {
		public static void SetUniqueObjectMetaset(C4Session c4s, C4SessionConfiguration c4sc, long objectId, XmlElement metasetContent, string msTypeName, bool deleteExisting, long? metasetId)
        {
            // expects that object is locked
            C4MetasetType msType = c4sc.MetasetTypesByName[msTypeName];
            if (metasetId == null)
            {
                // new metaset
                if(deleteExisting)
                {
                    C4Metadata m = c4s.GetObjectMeta(objectId, new HashSet<long> { (long)msType.Id });
                    if(m.MetasetsByTypeId.Count>0) c4s.DeleteObjectMetasets(m.MetasetsByTypeId[(long)msType.Id].Select(x => (long)x.Id).ToList().ToHashSet<long>());
                }
                C4Metaset ms=new C4Metaset((long)msType.Id, objectId, metasetContent);
                c4s.CreateObjectMeta(new Dictionary<long, HashSet<C4Metaset>>() { { objectId, new HashSet<C4Metaset>() { ms } } });
            }
            else
            {
                c4s.UpdateObjectMetaContent(new Dictionary<long, HashSet<C4Metaset>>() { { objectId, new HashSet<C4Metaset>() { new C4Metaset((long)msType.Id, objectId, metasetContent, metasetId) } } } );
            }
        }

        //public static void SetUniqueFolderMetaset(C4Session c4s, long id, XmlElement metaset, C4MetasetType type, Dictionary<string, C4MetasetType> MetasetTypesByName, long? metasetId)
        //{
        //    // Convenience function for some legacy code
        //    if (metasetId == null)
        //    {
        //        C4Metadata m = c4s.GetFolderMeta(id, DictionaryHelper.IdToHashSet((long)type.Id));
        //        if (m.MetasetsByTypeId.ContainsKey((long)type.Id)) metasetId = m.MetasetsByTypeId[(long)type.Id].First().Id;
        //    }

        //    if (metasetId == null)
        //    {
        //        // new metaset - but to be sure, check whether one exists
        //        HashSet<long> ids = DictionaryHelper.IdToHashSet(id);
        //        Dictionary<long, HashSet<C4Metaset>> createMetasets = new Dictionary<long, HashSet<C4Metaset>>();
        //        createMetasets.Add(id, new HashSet<C4Metaset>());
        //        createMetasets[id].Add(new C4Metaset((long)type.Id, id, metaset));
        //        c4s.CreateFolderMeta(createMetasets);
        //    }
        //    else
        //    {
        //        // existing metaset
        //        Dictionary<long, HashSet<C4Metaset>> updateMetasets = new Dictionary<long, HashSet<C4Metaset>>();
        //        updateMetasets.Add(id, new HashSet<C4Metaset>());
        //        updateMetasets[id].Add(new C4Metaset((long)type.Id, id, metaset, metasetId));
        //        c4s.UpdateFolderMetaContent(updateMetasets);
        //    }
        //}
        public static CmnObject GetObject(CmnSession s, long id)
        {
            return new CmnObject(s, s.CommandSession.GetObjectsById(DictionaryHelper.IdToHashSet(id), false).Values.First());
        }

    }
}
