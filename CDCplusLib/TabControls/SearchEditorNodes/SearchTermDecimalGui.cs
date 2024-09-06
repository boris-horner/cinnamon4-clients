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
    public partial class SearchTermDecimalGui : UserControl, ISearchTermGui
    {
        public event EventHandler ControlChanged;
        private CmnSession _s;
        public SearchTermDecimalGui()
        {
            InitializeComponent();
            ValidOperators = new List<SearchEditor.Operators>();
            ValidOperators.Add(SearchEditor.Operators.Less);
            ValidOperators.Add(SearchEditor.Operators.LessOrEqual);
            ValidOperators.Add(SearchEditor.Operators.Equals);
            ValidOperators.Add(SearchEditor.Operators.GreaterOrEqual);
            ValidOperators.Add(SearchEditor.Operators.Greater);
            cboOperator.Items.Clear();
            cboOperator.Items.Add(new SimpleDisplayItem("is_less", Properties.Resources.lblEquals));
            cboOperator.Items.Add(new SimpleDisplayItem("is_equal_or_less", Properties.Resources.lblEquals));
            cboOperator.Items.Add(new SimpleDisplayItem("is_equal", Properties.Resources.lblEquals));
            cboOperator.Items.Add(new SimpleDisplayItem("is_equal_or_greater", Properties.Resources.lblEquals));
            cboOperator.Items.Add(new SimpleDisplayItem("is_greater", Properties.Resources.lblEquals));
            cboOperator.SelectedIndex = 2;
        }
        public string FieldName { get; private set; }
        public string FieldDisplayName { get; private set; }
        public string Value
        {
            get
            {
                return GetDecimalValue(decimal.Parse(vtxtDecimal.Text.Trim()));
            }
            private set
            {
                string[] segs = vtxtDecimal.Text.Split('.');
                if(segs.Count()==1)
                {
                    vtxtDecimal.Text = value;
                }
                else if(segs.Count()==2)
                {
                    vtxtDecimal.Text = segs[0] + "." + segs[1].Substring(0, 8);
                }
                else throw new ApplicationException("Invalid decimal format.");
            }
        }
        public string ValueDisplayText
        {
            get
            {
                return vtxtDecimal.Text.Trim();
            }
        }

        public void Init(CmnSession s, string fieldName, XmlElement termEl, XmlElement termDefEl, Dictionary<string, C4Metaset> nameToMasterData, GlobalApplicationData gad)
        {
            _s = s;
            FieldName = fieldName; // termEl.GetAttribute("index_item");
            FieldDisplayName = _s.SessionConfig.C4Sc.GetLocalizedLabel(FieldName, "index_item");
            Negation = termEl == null ? false : (termEl.GetAttribute("negative") == "true");
            AnyValue = termEl == null ? false : (termEl.GetAttribute("any") == "true");
            VariableName = termEl == null ? null : (termEl.HasAttribute("variable_name") ? termEl.GetAttribute("variable_name") : null);
        }
        public SearchEditor.Operators Operator
        {
            get
            {
                switch (cboOperator.SelectedIndex)
                {
                    case 0: return SearchEditor.Operators.Less;
                    case 1: return SearchEditor.Operators.LessOrEqual;
                    case 2: return SearchEditor.Operators.Equals;
                    case 3: return SearchEditor.Operators.GreaterOrEqual;
                    case 4: return SearchEditor.Operators.Greater;
                }
                throw new ApplicationException("No search term operator selected.");
            }
            set
            {
                if (!ValidOperators.Contains(value)) throw new ApplicationException("Invalid search term operator.");

                switch (value)
                {
                    case SearchEditor.Operators.Less: cboOperator.SelectedIndex = 0; break;
                    case SearchEditor.Operators.LessOrEqual: cboOperator.SelectedIndex = 1; break;
                    case SearchEditor.Operators.Equals: cboOperator.SelectedIndex = 2; break;
                    case SearchEditor.Operators.GreaterOrEqual: cboOperator.SelectedIndex = 3; break;
                    case SearchEditor.Operators.Greater: cboOperator.SelectedIndex = 4; break;
                }
            }
        }

        public List<SearchEditor.Operators> ValidOperators { get; }

        public bool Negation { get; set; }
        public string VariableName { get; set; }
        public bool AnyValue { get; set; }

        private void VtxtDecimal_TextChanged(object sender, EventArgs e)
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

            if(AnyValue)
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
                switch (Operator)
                {
                    case SearchEditor.Operators.Equals:
                        {
                            XmlElement tqEl = (XmlElement)directParentClauseEl.AppendChild(directParentClauseEl.OwnerDocument.CreateElement("TermQuery"));
                            tqEl.SetAttribute("fieldName", FieldName);
                            tqEl.InnerText = GetDecimalValue(decimal.Parse(vtxtDecimal.Text));
                            break;
                        }
                    case SearchEditor.Operators.Less:
                        {
                            XmlElement rqEl = (XmlElement)directParentClauseEl.AppendChild(directParentClauseEl.OwnerDocument.CreateElement("RangeQuery"));
                            rqEl.SetAttribute("fieldName", FieldName);
                            rqEl.SetAttribute("lowerTerm", "00000000000.00000000");
                            rqEl.SetAttribute("upperTerm", GetDecimalValue(decimal.Parse(vtxtDecimal.Text)));
                            rqEl.SetAttribute("includeLower", "true");
                            rqEl.SetAttribute("includeUpper", "false");
                            break;
                        }
                    case SearchEditor.Operators.LessOrEqual:
                        {
                            XmlElement rqEl = (XmlElement)directParentClauseEl.AppendChild(directParentClauseEl.OwnerDocument.CreateElement("RangeQuery"));
                            rqEl.SetAttribute("fieldName", FieldName);
                            rqEl.SetAttribute("lowerTerm", "00000000000.00000000");
                            rqEl.SetAttribute("upperTerm", GetDecimalValue(decimal.Parse(vtxtDecimal.Text)));
                            rqEl.SetAttribute("includeLower", "true");
                            rqEl.SetAttribute("includeUpper", "true");
                            break;
                        }
                    case SearchEditor.Operators.Greater:
                        {
                            XmlElement rqEl = (XmlElement)directParentClauseEl.AppendChild(directParentClauseEl.OwnerDocument.CreateElement("RangeQuery"));
                            rqEl.SetAttribute("fieldName", FieldName);
                            rqEl.SetAttribute("lowerTerm", GetDecimalValue(decimal.Parse(vtxtDecimal.Text)));
                            rqEl.SetAttribute("upperTerm", "99999999999.99999999");
                            rqEl.SetAttribute("includeLower", "false");
                            rqEl.SetAttribute("includeUpper", "true");
                            break;
                        }
                    case SearchEditor.Operators.GreaterOrEqual:
                        {
                            XmlElement rqEl = (XmlElement)directParentClauseEl.AppendChild(directParentClauseEl.OwnerDocument.CreateElement("RangeQuery"));
                            rqEl.SetAttribute("fieldName", FieldName);
                            rqEl.SetAttribute("lowerTerm", GetDecimalValue(decimal.Parse(vtxtDecimal.Text)));
                            rqEl.SetAttribute("upperTerm", "99999999999.99999999");
                            rqEl.SetAttribute("includeLower", "true");
                            rqEl.SetAttribute("includeUpper", "true");
                            break;
                        }
                }

            }
        }

        private string GetDecimalValue(decimal dec)
        {
            long pre = (long)Math.Floor(dec);
            long post = (long)Math.Floor((dec - pre) * 100000000);
            return pre.ToString("D11") + "." + post.ToString("D8");
        }
    }
}
