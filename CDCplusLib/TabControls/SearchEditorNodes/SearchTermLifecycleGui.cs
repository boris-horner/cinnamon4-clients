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
using CDCplusLib.Interfaces;
using System.Xml;
using C4ObjectApi.Repository;
using C4ServerConnector.Assets;
using CDCplusLib.Common;

namespace CDCplusLib.TabControls.SearchEditorNodes
{
    public partial class SearchTermLifecycleGui : UserControl, ISearchTermGui
    {
        public event EventHandler ControlChanged;
        private CmnSession _s;
        
        public SearchTermLifecycleGui()
        {
            InitializeComponent();
            tbOperator.Text=Properties.Resources.lblEquals;
        }
        public string FieldName { get; private set; }
        public string FieldDisplayName { get; private set; }
        public string Value
        {
            get
            {
                return lcsLifecycleState.SelectedLifecycleState.Id.ToString();
            }
            private set
            {
                if (value == "") lcsLifecycleState.SelectedLifecycleState = null;
                else lcsLifecycleState.SelectedLifecycleState = _s.SessionConfig.C4Sc.LifecycleStatesById[long.Parse(value)];
            }
        }
        public string ValueDisplayText
        {
            get
            {
                return lcsLifecycleState.SelectedLifecycleState==null?null:lcsLifecycleState.SelectedLifecycleState.ToString();
            }
        }


        public void Init(CmnSession s, string fieldName, XmlElement termEl, XmlElement termDefEl, Dictionary<string, C4Metaset> nameToMasterData, GlobalApplicationData gad)
        {
            _s = s;
            lcsLifecycleState.Init(s,false, termEl==null?null:_s.SessionConfig.C4Sc.LifecycleStatesById[long.Parse(termEl.GetAttribute("value"))]);
            FieldName = fieldName; // termEl.GetAttribute("index_item");
            FieldDisplayName = _s.SessionConfig.C4Sc.GetLocalizedLabel(FieldName, "index_item");
            Negation = termEl==null?false:(termEl.GetAttribute("negative") == "true");
            AnyValue = termEl == null ? false : (termEl.GetAttribute("any") == "true");
            VariableName = termEl == null ? null : (termEl.HasAttribute("variable_name") ? termEl.GetAttribute("variable_name") : null);
            Value = termEl == null ? "" : termEl.GetAttribute("value");
        }
        public SearchEditor.Operators Operator
        {
            get
            {
                return SearchEditor.Operators.Equals;
            }
            set
            {
                if (value != SearchEditor.Operators.Equals) throw new ApplicationException("Invalid search term operator.");
            }
        }

        public List<SearchEditor.Operators> ValidOperators { get; }

        public bool Negation { get; set; }
        public string VariableName { get; set; }
        public bool AnyValue { get; set; }

        private void LcsLifecycleState_GuiChanged()
        {
            ControlChanged?.Invoke(this, new EventArgs());
        }
        public void AppendToQuery(XmlElement parentClauseEl)
        {
            XmlElement directParentClauseEl = null;
            if (Negation)
            {
                XmlElement bqEl = (XmlElement)parentClauseEl.AppendChild(parentClauseEl.OwnerDocument.CreateElement("BooleanQuery"));
                directParentClauseEl = (XmlElement)bqEl.AppendChild(parentClauseEl.OwnerDocument.CreateElement("Clause"));
                directParentClauseEl.SetAttribute("occurs", "mustnot");
                XmlElement madqClauseEl = (XmlElement)bqEl.AppendChild(directParentClauseEl.OwnerDocument.CreateElement("Clause"));
                madqClauseEl.SetAttribute("occurs", "should");
                madqClauseEl.AppendChild(directParentClauseEl.OwnerDocument.CreateElement("MatchAllDocsQuery"));
            }
            else directParentClauseEl = parentClauseEl;

            if (AnyValue)
            {
                // TODO: change this to the new field type "long"
                XmlElement avRqEl = (XmlElement)directParentClauseEl.AppendChild(directParentClauseEl.OwnerDocument.CreateElement("RangeQuery"));
                avRqEl.SetAttribute("fieldName", FieldName);
                avRqEl.SetAttribute("lowerTerm", "!");
                avRqEl.SetAttribute("upperTerm", new String('\uff80', 20));
                avRqEl.SetAttribute("includeLower", "true");
                avRqEl.SetAttribute("includeUpper", "true");
            }
            else
            {
                XmlElement epqEl = (XmlElement)directParentClauseEl.AppendChild(directParentClauseEl.OwnerDocument.CreateElement("ExactPointQuery"));
                epqEl.SetAttribute("fieldName", FieldName);
                epqEl.SetAttribute("type", "long");
                epqEl.SetAttribute("value", ((long)lcsLifecycleState.SelectedLifecycleState.Id).ToString());
            }
        }
    }
}
