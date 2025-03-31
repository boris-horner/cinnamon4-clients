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
using CDCplusLib.DataModel;
using C4ObjectApi.Repository;
using CDCplusLib.Common;

namespace CDCplusLib.TabControls.SearchEditorNodes
{
    public partial class SearchTermCustomListValueGui : UserControl, ISearchTermGui
    {
        public event EventHandler ControlChanged;
        private CmnSession _s;

        public SearchTermCustomListValueGui()
        {
            InitializeComponent();
            tbOperator.Text = Properties.Resources.lblEquals;
            ValidOperators = new List<SearchEditor.Operators>();
            ValidOperators.Add(SearchEditor.Operators.Equals);
        }
        public string FieldName { get; private set; }
        public string FieldDisplayName { get; private set; }
        public string Value
        {
            get
            {
                return ((SimpleDisplayItem)lbListValues.SelectedItem).Key;
            }
        }
        public string ValueDisplayText
        {
            get
            {
                return ((SimpleDisplayItem)lbListValues.SelectedItem).Label;
            }
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

        public void Init(CmnSession s, string fieldName, XmlElement termEl, XmlElement termDefEl, Dictionary<string, XmlElement> nameToMasterData, GlobalApplicationData gad)
        {
            _s = s;
            FieldName = fieldName; // termEl.GetAttribute("index_item");
            FieldDisplayName = _s.SessionConfig.C4Sc.GetLocalizedLabel(FieldName, "index_item");
            Negation = termEl == null ? false : (termEl.GetAttribute("negative") == "true");
            AnyValue = termEl == null ? false : (termEl.GetAttribute("any") == "true");
            VariableName = termEl == null ? null : (termEl.HasAttribute("variable_name") ? termEl.GetAttribute("variable_name") : null);


            lbListValues.Items.Clear();
            string mdsName = termDefEl.SelectSingleNode("custom/master_data_source/@name").InnerText;
            XmlElement masterData = (XmlElement)nameToMasterData[mdsName].SelectSingleNode(termDefEl.SelectSingleNode("custom/master_data_root_xpath").InnerText);
            lbListValues.Sorted = termDefEl.SelectSingleNode("custom/sorted").InnerText == "true";

            foreach (XmlElement value in masterData.SelectNodes("data/value"))
            {
                XmlNode labelN = value.SelectSingleNode("label[@lang='" + s.SessionConfig.C4Sc.UILanguagesById[s.User.UiLanguageId].Name + "']");
                if(labelN==null) labelN = value.SelectSingleNode("label[@lang='" + masterData.SelectSingleNode("data/@default_lang").InnerText + "']");
                SimpleDisplayItem sdi = new SimpleDisplayItem(value.GetAttribute("id"), labelN.InnerText);
                lbListValues.Items.Add(sdi);
            }
            if (termEl != null)
            {
                XmlNode labelN = masterData.SelectSingleNode("data/value[@id='"+ termEl.GetAttribute("value") + "']/label[@lang='" + s.SessionConfig.C4Sc.UILanguagesById[s.User.UiLanguageId].Name + "']");
                if (labelN == null) labelN = masterData.SelectSingleNode("data/value[@id='" + termEl.GetAttribute("value") + "']/label[@lang='" + masterData.SelectSingleNode("data/@default_lang").InnerText + "']");
                lbListValues.Text = labelN.InnerText;
            }
            else lbListValues.SelectedIndex = 0;
        }

        public bool Negation { get; set; }
        public string VariableName { get; set; }
        public bool AnyValue { get; set; }

        private void LbConfigValues_SelectedIndexChanged(object sender, EventArgs e)
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
                XmlElement tqEl = (XmlElement)directParentClauseEl.AppendChild(directParentClauseEl.OwnerDocument.CreateElement("TermQuery"));
                tqEl.SetAttribute("fieldName", FieldName);
                tqEl.InnerText = ((SimpleDisplayItem)lbListValues.SelectedItem).Key.ToLower();

            }
        }
    }
}
