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
using System.Xml;
using CDCplusLib.Interfaces;
using C4ObjectApi.Repository;

namespace CDCplusLib.DataModel
{
    public class RldColumnConfiguration
    {
        public readonly string Name;
        public readonly bool GlobalConfig;
        public readonly Dictionary<string, RldColumnDefinition> ColumnDefinitions;
        public RldColumnConfiguration(XmlElement configurationEl, CmnSession s, INodeDataProvider ndp, bool globalConfig)
        {
            Name = configurationEl.GetAttribute("name");
            GlobalConfig = globalConfig;
            ColumnDefinitions = new Dictionary<string, RldColumnDefinition>();
            foreach(XmlElement col in configurationEl.SelectNodes("columns/list_view_column"))
            {
                RldColumnDefinition cd = new RldColumnDefinition(col, s, ndp);
                ColumnDefinitions.Add(cd.ColumnType, cd);
            }
        }
        public RldColumnConfiguration(string name, Dictionary<string, RldColumnDefinition> columnDefinitions, CmnSession s, INodeDataProvider ndp, bool globalConfig)
        {
            Name = name;
            GlobalConfig = globalConfig;
            ColumnDefinitions = columnDefinitions;
        }
        public override string ToString()
        {
            return Name;
        }
    }
}
