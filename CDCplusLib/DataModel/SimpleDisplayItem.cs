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
namespace CDCplusLib.DataModel
{
    // this class is for all simple applications in ListBox or ComboBox controls where ToString() is the display label source and only simple data is to be stored
    public class SimpleDisplayItem
    {
        public readonly string Key;
        public readonly string Label;
        public readonly Dictionary<string, string> Parameters;
        public object Tag { get; set; }
        public SimpleDisplayItem(string key, string label, object tag=null)
        {
            Key = key;
            Label = label;
            Tag = tag;
            Parameters = new Dictionary<string, string>();
        }
        public override string ToString()
        {
            return Label;
        }
    }
}
