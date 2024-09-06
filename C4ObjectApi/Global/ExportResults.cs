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
    public class ExportResults
    {
        public int IdCount { get; set; }
        public int ObjectCount { get; set; }
        public List<long> MissingObjects { get; }
        public Dictionary<long, CmnObject> MissingVariants { get; }
        public Dictionary<long, CmnObject> UntranslatedVariants { get; }
        public Dictionary<long, CmnObject> UnacceptedVariants { get; }
        public Dictionary<long, string> IdToPathLookup { get; }
        public CmnObject RootObject { get; set; }

        public ExportResults()
        {
            MissingObjects = new List<long>();
            MissingVariants = new Dictionary<long, CmnObject>();
            UntranslatedVariants = new Dictionary<long, CmnObject>();
            UnacceptedVariants = new Dictionary<long, CmnObject>();
            IdToPathLookup = new Dictionary<long, string>();
        }
    }
}
