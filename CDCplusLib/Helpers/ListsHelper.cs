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
using C4Logic;
using C4ObjectApi.Interfaces;
using C4ObjectApi.Repository;
using C4ServerConnector;

namespace CDCplusLib.Helpers
{
    public class ListsHelper
    {
        public static HashSet<HashSet<string>> SplitHashSet(HashSet<string> inHs)
        {
            HashSet<HashSet<string>> result = new HashSet<HashSet<string>>();

            if (inHs.Count < Constants.MAX_CLAUSE_COUNT) result.Add(inHs);
            else
            {
                long i = 0;
                HashSet<string> workHs = new HashSet<string>();
                foreach (string s in inHs)
                {
                    workHs.Add(s);
                    ++i;
                    if (i % Constants.MAX_CLAUSE_COUNT == 0)
                    {
                        result.Add(workHs);
                        workHs = new HashSet<string>();
                    }
                }
                if (workHs.Count > 0) result.Add(workHs);
            }

            return result;
        }
        public static Dictionary<long, IRepositoryNode> ConvertToOwnables(Dictionary<long, CmnObject> dictO, Dictionary<long, CmnFolder> dictF)
        {
            Dictionary<long, IRepositoryNode> result = new Dictionary<long, IRepositoryNode>();
            if (dictO != null) foreach (CmnObject o in dictO.Values) result.Add(o.Id, o);
            if (dictF != null) foreach (CmnFolder f in dictF.Values) result.Add(f.Id, f);
            return result;
        }
    }
}
