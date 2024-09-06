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

namespace CDCplusLib.Common
{
    public class OwnableDictComparer
    {
        public bool Compare(Dictionary<long, IRepositoryNode> dict1, Dictionary<long, IRepositoryNode> dict2)
        {
            if (!CompareKeys(dict1.Keys, dict2.Keys)) return false;
            else
            {
                // TODO: comparison of modify date or basic properties values
                return true;
            }

            //return false;
        }

        private bool CompareKeys(Dictionary<long, IRepositoryNode>.KeyCollection ids1, Dictionary<long, IRepositoryNode>.KeyCollection ids2)
        {
            if (ids1.Count != ids2.Count) return false;
            foreach(long id in ids1)
            {
                if (!ids2.Contains(id)) return false;
            }
            return true;
        }
    }
}
