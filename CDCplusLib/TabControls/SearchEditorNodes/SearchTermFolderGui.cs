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
using CDCplusLib.DataModel;
using CDCplusLib.Common.GUI;
using C4ObjectApi.Repository;
using CDCplusLib.Common;

namespace CDCplusLib.TabControls.SearchEditorNodes
{
    public partial class SearchTermFolderGui : UserControl, ISearchTermGui
    {
        public event EventHandler ControlChanged;
        private CmnSession _s;
        private GlobalApplicationData _gad;

		public SearchTermFolderGui()
        {
            InitializeComponent();
            ValidOperators = new List<SearchEditor.Operators>();
            ValidOperators.Add(SearchEditor.Operators.Equals);
            ValidOperators.Add(SearchEditor.Operators.StartsWith);
            cboOperator.Items.Clear();
            cboOperator.Items.Add(new SimpleDisplayItem("is_equal", Properties.Resources.lblEquals));
            cboOperator.Items.Add(new SimpleDisplayItem("string_starts_with", Properties.Resources.lblStartsWith));
            cboOperator.SelectedIndex = 0;
        }
        public string FieldName { get; set; }
        public string FieldDisplayName { get; set; }
        public string Value
        {
            get
            {
                return vtxtPath.Text.Trim();
            }
            private set
            {
                vtxtPath.Text = value;
            }
        }
        public string ValueDisplayText
        {
            get
            {
                return vtxtPath.Text.Trim();
            }
        }


        public void Init(CmnSession s, string fieldName, XmlElement termEl, XmlElement termDefEl, Dictionary<string, XmlElement> nameToMasterData, GlobalApplicationData gad)
        {
            _s = s;
            _gad = gad;
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
                }
        }
        public SearchEditor.Operators Operator
        {
            get
            {
                switch (cboOperator.SelectedIndex)
                {
                    case 0: return SearchEditor.Operators.Equals;
                    case 1: return SearchEditor.Operators.StartsWith;
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
                }
            }
        }

        public List<SearchEditor.Operators> ValidOperators { get; }

        public bool Negation { get; set; }
        public string VariableName { get; set; }
        public bool AnyValue { get; set; }

        private void VtxtPath_TextChanged(object sender, EventArgs e)
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
                switch (Operator)
                {
                    case SearchEditor.Operators.Equals:
                        {
                            XmlElement tqEl = (XmlElement)directParentClauseEl.AppendChild(directParentClauseEl.OwnerDocument.CreateElement("TermQuery"));
                            tqEl.SetAttribute("fieldName", FieldName);
                            tqEl.InnerText = "/root" + vtxtPath.Text.Trim().ToLower();
                            break;
                        }
                    case SearchEditor.Operators.StartsWith:
                        {
                            XmlElement wqEl = (XmlElement)directParentClauseEl.AppendChild(directParentClauseEl.OwnerDocument.CreateElement("WildcardQuery"));
                            wqEl.SetAttribute("fieldName", FieldName);
                            wqEl.InnerText = "/root" + vtxtPath.Text.Trim().ToLower() + "*";
                            break;
                        }
                }

            }
        }

        private void CmdSelectPath_Click(object sender, EventArgs e)
        {
            XmlElement configEl = (XmlElement)_s.UserConfig.DocumentElement.SelectSingleNode("classes/object_selector/custom");
            CmnFolder f = vtxtPath.Text.Trim().Length == 0 ? null : _s.GetFolder(vtxtPath.Text.Trim());
            SelectObjectsDialog sd = new SelectObjectsDialog(_s, configEl, SelectObjectsDialog.SelectionModes.SelectFolder,f,Properties.Resources.lblChooseTargets, _gad);
            if (sd.ShowSelectorDialog() == DialogResult.OK && sd.Selection.Count()>0)
            {
                vtxtPath.Text = ((CmnFolder)sd.Selection.Values.First()).FolderPath;
                ControlChanged?.Invoke(this, new EventArgs());
            }
        }
    }
}
