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
using CDCplusLib.DataModel;
using System.Xml;
using C4ObjectApi.Repository;
using C4ServerConnector.Assets;
using CDCplusLib.Common;

namespace CDCplusLib.TabControls.SearchEditorNodes
{
    public partial class SearchTermStringGui : UserControl, ISearchTermGui
    {
        public event EventHandler ControlChanged;
        private CmnSession _s;
        private bool _caseSensitive;
        public SearchTermStringGui()
        {
            InitializeComponent();
            ValidOperators = new List<SearchEditor.Operators>();
            ValidOperators.Add(SearchEditor.Operators.Equals);
            ValidOperators.Add(SearchEditor.Operators.StartsWith);
            ValidOperators.Add(SearchEditor.Operators.Contains);
            cboOperator.Items.Clear();
            cboOperator.Items.Add(new SimpleDisplayItem("is_equal", Properties.Resources.lblEquals));
            cboOperator.Items.Add(new SimpleDisplayItem("string_starts_with", Properties.Resources.lblStartsWith));
            cboOperator.Items.Add(new SimpleDisplayItem("string_contains", Properties.Resources.lblContains));
            cboOperator.SelectedIndex = 0;
        }
        public string FieldName { get; private set; }
        public string FieldDisplayName { get; private set; }
        public string Value
        {
            get
            {
                return vtxtString.Text.Trim();
            }
            private set
            {
                vtxtString.Text = value;
            }
        }
        public string ValueDisplayText
        {
            get
            {
                return vtxtString.Text.Trim();
            }
        }


        public void Init(CmnSession s, string fieldName, XmlElement termEl, XmlElement termDefEl, Dictionary<string, XmlElement> nameToMasterData, GlobalApplicationData gad)
        {
            _s = s;
            FieldName = fieldName; // termEl.GetAttribute("index_item");
            FieldDisplayName = _s.SessionConfig.C4Sc.GetLocalizedLabel(FieldName, "index_item");
            Negation = termEl == null ? false : (termEl.GetAttribute("negative") == "true");
            AnyValue = termEl == null ? false : (termEl.GetAttribute("any") == "true");
            VariableName = termEl == null ? null : (termEl.HasAttribute("variable_name") ? termEl.GetAttribute("variable_name") : null);
            Value = termEl == null ? "" : termEl.GetAttribute("value");
            if (termEl == null) Operator = SearchEditor.Operators.Equals;
            else switch (termEl.GetAttribute("operator"))
                {
                    case "is_equal": Operator = SearchEditor.Operators.Equals; break;
                    case "string_starts_with": Operator = SearchEditor.Operators.StartsWith; break;
                    case "string_contains": Operator = SearchEditor.Operators.Contains; break;
                }
            _caseSensitive=(termDefEl.SelectSingleNode("custom[case_sensitive='true']")!=null);
        }
        public SearchEditor.Operators Operator
        {
            get
            {
                switch (cboOperator.SelectedIndex)
                {
                    case 0: return SearchEditor.Operators.Equals;
                    case 1: return SearchEditor.Operators.StartsWith;
                    case 2: return SearchEditor.Operators.Contains;
                }
                throw new ApplicationException("No search term operator selected.");
            }
            set
            {
                if (!ValidOperators.Contains(value)) throw new ApplicationException("Invalid search term operator.");

                switch (value)
                {
                    case SearchEditor.Operators.Equals: cboOperator.SelectedIndex = 0; break;
                    case SearchEditor.Operators.StartsWith: cboOperator.SelectedIndex = 1; break;
                    case SearchEditor.Operators.Contains: cboOperator.SelectedIndex = 2; break;
                }
            }
        }

        public List<SearchEditor.Operators> ValidOperators { get; }

        public bool Negation { get; set; }
        public string VariableName { get; set; }
        public bool AnyValue { get; set; }

        private void VtxtString_TextChanged(object sender, EventArgs e)
        {
            ControlChanged?.Invoke(this, new EventArgs());
        }

        private void CboOperator_SelectedIndexChanged(object sender, EventArgs e)
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
                XmlElement avRqEl = (XmlElement)directParentClauseEl.AppendChild(directParentClauseEl.OwnerDocument.CreateElement("RangeQuery"));
                avRqEl.SetAttribute("fieldName", FieldName);
                avRqEl.SetAttribute("lowerTerm", "!");
                avRqEl.SetAttribute("upperTerm", new String('\uff80', 20));
                avRqEl.SetAttribute("includeLower", "true");
                avRqEl.SetAttribute("includeUpper", "true");
            }
            else
            {
                string text = _caseSensitive ? vtxtString.Text.Trim() : vtxtString.Text.Trim().ToLower();
                switch (Operator)
                {
                    case SearchEditor.Operators.Equals:
                        {
                            XmlElement tqEl = (XmlElement)directParentClauseEl.AppendChild(directParentClauseEl.OwnerDocument.CreateElement("TermQuery"));
                            tqEl.SetAttribute("fieldName", FieldName);
                            tqEl.InnerText = text;
                            break;
                        }
                    case SearchEditor.Operators.StartsWith:
                        {
                            XmlElement wqEl = (XmlElement)directParentClauseEl.AppendChild(directParentClauseEl.OwnerDocument.CreateElement("WildcardQuery"));
                            wqEl.SetAttribute("fieldName", FieldName);
                            wqEl.InnerText = text + "*";
                            break;
                        }
                    case SearchEditor.Operators.Contains:
                        {
                            XmlElement wqEl = (XmlElement)directParentClauseEl.AppendChild(directParentClauseEl.OwnerDocument.CreateElement("WildcardQuery"));
                            wqEl.SetAttribute("fieldName", FieldName);
                            wqEl.InnerText = "*" + text + "*";
                            break;
                        }
                }

            }
        }
    }
}
