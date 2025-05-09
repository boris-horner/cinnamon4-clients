﻿// Copyright 2012,2024 texolution GmbH
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
using C4ObjectApi.Repository;
using CDCplusLib.Common;

namespace CDCplusLib.Interfaces
{
    public interface ISearchTermGui
    {
        event EventHandler ControlChanged;
        void Init(CmnSession s, string fieldName, XmlElement termEl, XmlElement termDefEl, Dictionary<string, XmlElement> nameToMasterData, GlobalApplicationData gad);
        string FieldName { get; }
        string FieldDisplayName { get; }
        string Value { get; }
        string ValueDisplayText { get; }
        //XmlElement Term { get; }
        bool Negation { get; set; }
        bool AnyValue { get; set; }
        string VariableName { get; set; }
        void AppendToQuery(XmlElement parentClauseEl);

        TabControls.SearchEditor.Operators Operator { get; set; }
        List<TabControls.SearchEditor.Operators> ValidOperators { get; }
    }
}
