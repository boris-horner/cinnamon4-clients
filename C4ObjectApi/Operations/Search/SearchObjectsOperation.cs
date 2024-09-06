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
using C4ObjectApi.Repository;
using C4ServerConnector.Assets;

namespace C4ObjectApi.Operations.Search
{
    public class SearchObjectsOperation
    {
        private string _query;
        private SortedDictionary<long, long> _idsResult;
        private CmnSession _s;
        public SearchObjectsOperation(CmnSession s, string query)
        {
            _s = s;
            _query = query;
            _idsResult= new SortedDictionary<long, long>();
            long j = 0;
            foreach (long id in _s.CommandSession.SearchObjectIds(_query))
            {
                _idsResult.Add(j++, id);
            }
        }

        public long TotalResultCount { get { return _idsResult.Count; } }
        public Dictionary<long, CmnObject> GetObjects(long page= -1, long pageSize= -1, long batchSize= -1)
        {
            if (TotalResultCount == 0) return new Dictionary<long, CmnObject>();
            // page is zero-based
            long minIndex = 0;
            long maxIndex = TotalResultCount-1;
            if (pageSize != -1)
            {
                if (page == -1)
                    maxIndex = pageSize -1;
                else
                {
                    minIndex = pageSize * page;
                    maxIndex = pageSize * (page + 1) - 1;
                }
            }

            if(batchSize!= -1 && maxIndex-minIndex+1>batchSize)
            {
                // multiple batches
                long batchCt = (long)Math.Ceiling((maxIndex - minIndex + 1) / (double)batchSize);
                Dictionary<long, CmnObject> result = new Dictionary<long, CmnObject>();
                
                // append all folders that were found to _s.CachedFolders, and update existing folders

                for (long i=0; i<batchCt; ++i)
                {
                    HashSet<long> ids = new HashSet<long>();
                    long currMinIndex = minIndex + i * batchSize;
                    long currMaxIndex = minIndex + (i+1) * batchSize - 1;
                    for(long j=currMinIndex; j<=currMaxIndex; ++j)
                    {
                        if (j<_idsResult.Count && !ids.Contains(_idsResult[j])) ids.Add(_idsResult[j]);
                    }
                    Dictionary<long, C4Object> objs = _s.CommandSession.GetObjectsById(ids, false);
                    foreach(C4Object c4o in objs.Values)
                    {
                        CmnObject o = new CmnObject(_s, c4o);
                        result.Add(c4o.Id, o);
                    }
                }
                return result;
            }
            else
            {
                // one single call
                Dictionary<long, CmnObject> result = new Dictionary<long, CmnObject>();
                HashSet<long> ids = new HashSet<long>();
                for (long j = minIndex; j <= maxIndex; ++j)
                {
                    if(j>=_idsResult.Count) continue;
                    if (!ids.Contains(_idsResult[j])) ids.Add(_idsResult[j]);
                }
                Dictionary<long, C4Object> objs = _s.CommandSession.GetObjectsById(ids, false);
                foreach (C4Object c4o in objs.Values)
                {
                    CmnObject o = new CmnObject(_s, c4o);
                    result.Add(c4o.Id, o);
                }
                return result; // _s.WrapC4Objects(_s.CommandSession.GetObjectsById(ids, false));
            }


        }
    }
}
