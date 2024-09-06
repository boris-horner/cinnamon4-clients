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
using System.Linq;
using System.Collections.Generic;
using C4ObjectApi.Repository;
using C4ObjectApi.Interfaces;

namespace C4ObjectApi.Helpers
{
    public class DictionaryHelper
    {
        public static CmnFolder GetSingleFolder(Dictionary<long, IRepositoryNode> dict)
        {
            if (dict == null) return null;
            if (dict.Values.Count != 1)
                return null;

            IRepositoryNode ow = dict.First().Value;

            if (ow is CmnFolder)
                return (CmnFolder)ow;
            else
                return null;
        }
        public static CmnObject GetSingleObject(Dictionary<long, IRepositoryNode> dict)
        {
            if (dict == null) return null;
            if (dict.Values.Count != 1)
                return null;

            IRepositoryNode ow = dict.First().Value;

            if (ow is CmnObject)
                return (CmnObject)ow;
            else
                return null;
        }
        public static HashSet<long> IdToHashSet(long id)
        {
            HashSet<long> result = new HashSet<long>();
            result.Add(id);
            return result;
        }
    }
}


