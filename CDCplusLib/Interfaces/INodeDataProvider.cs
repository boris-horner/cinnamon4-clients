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
using CDCplusLib.DataModel;
using C4ObjectApi.Interfaces;
using C4ObjectApi.Repository;

namespace CDCplusLib.Interfaces
{
    public interface INodeDataProvider
    {
        void Init(CmnSession s, XmlElement columnConfigurationEl, XmlElement resultListConfigurationEl);
        Dictionary<string, NodeColumnDefinition> GetColumnDefinitions();
        string GetValue(IRepositoryNode ow, string label);
        string GetFieldTitle(string label);
        string GetDefaultSortColumn();
        SortOrder GetDefaultSortColumnOrder();
        Dictionary<string, string> GetKnownStandardFields();   // dictionary: name --> label
        Dictionary<string, string> GetKnownCustomFields();   // dictionary: name --> label
    }
}
