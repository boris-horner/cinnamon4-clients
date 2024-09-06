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
using C4ObjectApi.Repository;

namespace C4ObjectApi.Global
{
    public class SearchResults
    {
        public SearchResults(Dictionary<long, CmnObject> objects, Dictionary<long, CmnFolder> folders)
        {
            Objects = objects;
            Folders = folders;
        }
        public Dictionary<long, CmnObject> Objects { get; private set; }
        public Dictionary<long, CmnFolder> Folders { get; private set; }
    }
}
