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
    public partial class SearchTermSystemConfigItemsGui : UserControl, ISearchTermGui
    {
        public event EventHandler ControlChanged;
        private CmnSession _s;

        public SearchTermSystemConfigItemsGui()
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
                switch (FieldName)
                {
                    case "acl": return ((C4Acl)lbConfigValues.SelectedItem).Id.ToString();
                    case "owner":
                    case "locker":
                    case "creator":
                    case "modifier":
                        return ((C4User)lbConfigValues.SelectedItem).Id.ToString();
                    case "format": return ((C4Format)lbConfigValues.SelectedItem).Id.ToString();
                    case "object_type": return ((C4ObjectType)lbConfigValues.SelectedItem).Id.ToString();
                    case "folder_type": return ((C4FolderType)lbConfigValues.SelectedItem).Id.ToString();
                    case "language": return ((C4Language)lbConfigValues.SelectedItem).Id.ToString();
                    default:
                        throw new ApplicationException("Invalid field name '" + FieldName + "'.");
                }
            }
        }
        public string ValueDisplayText
        {
            get
            {
                return lbConfigValues.Text.Trim();
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

            lbConfigValues.Items.Clear();
            //int fieldValue = termEl!=null?0:int.Parse(termEl.GetAttribute("value"));
            switch (FieldName)
            {
                case "acl":
                    foreach (C4Acl acl in _s.SessionConfig.C4Sc.AclsById.Values) lbConfigValues.Items.Add(acl);
                    if (termEl != null) lbConfigValues.SelectedItem = _s.SessionConfig.C4Sc.AclsById[long.Parse(termEl.GetAttribute("value"))];
                    else lbConfigValues.SelectedIndex = 0;
                    break;
                case "owner":
                case "locker":
                case "creator":
                case "modifier":
                    foreach (C4User u in _s.SessionConfig.C4Sc.UsersById.Values) lbConfigValues.Items.Add(u);
                    if (termEl != null) lbConfigValues.SelectedItem = _s.SessionConfig.C4Sc.UsersById[long.Parse(termEl.GetAttribute("value"))];
                    else lbConfigValues.SelectedIndex = 0;
                    break;
                case "format":
                    foreach (C4Format fmt in _s.SessionConfig.C4Sc.FormatsByName.Values) lbConfigValues.Items.Add(fmt);
                    if (termEl != null && !AnyValue) lbConfigValues.SelectedItem = _s.SessionConfig.C4Sc.FormatsById[long.Parse(termEl.GetAttribute("value"))];
                    else lbConfigValues.SelectedIndex = 0;
                    break;
                case "object_type":
                    foreach (C4ObjectType ot in _s.SessionConfig.C4Sc.ObjectTypesById.Values) lbConfigValues.Items.Add(ot);
                    if (termEl != null) lbConfigValues.SelectedItem = _s.SessionConfig.C4Sc.ObjectTypesById[long.Parse(termEl.GetAttribute("value"))];
                    else lbConfigValues.SelectedIndex = 0;
                    break;
                case "folder_type":
                    foreach (C4FolderType ft in _s.SessionConfig.C4Sc.FolderTypesById.Values) lbConfigValues.Items.Add(ft);
                    if (termEl != null) lbConfigValues.SelectedItem = _s.SessionConfig.C4Sc.FolderTypesById[long.Parse(termEl.GetAttribute("value"))];
                    else lbConfigValues.SelectedIndex = 0;
                    break;
                case "language":
                    foreach (C4Language l in _s.SessionConfig.C4Sc.LanguagesByName.Values) lbConfigValues.Items.Add(l);
                    if (termEl != null) lbConfigValues.SelectedItem = _s.SessionConfig.C4Sc.LanguagesById[long.Parse(termEl.GetAttribute("value"))];
                    else lbConfigValues.SelectedIndex = 0;
                    break;
            }
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
                //<PointRangeQuery fieldName='format' lowerTerm='0' upperTerm='100000' type='long'/>
                XmlElement avRqEl = (XmlElement)directParentClauseEl.AppendChild(directParentClauseEl.OwnerDocument.CreateElement("PointRangeQuery"));
                avRqEl.SetAttribute("fieldName", FieldName);
                avRqEl.SetAttribute("lowerTerm", "0");
                avRqEl.SetAttribute("upperTerm", long.MaxValue.ToString());
                avRqEl.SetAttribute("includeLower", "true");
                avRqEl.SetAttribute("includeUpper", "true");
            }
            else
            {
                XmlElement epqEl = (XmlElement)directParentClauseEl.AppendChild(directParentClauseEl.OwnerDocument.CreateElement("ExactPointQuery"));
                epqEl.SetAttribute("fieldName", FieldName);
                epqEl.SetAttribute("type", "long");
                switch (FieldName)
                {
                    case "acl":
                        epqEl.SetAttribute("value", ((long)((C4Acl)lbConfigValues.SelectedItem).Id).ToString());
                        break;
                    case "owner":
                    case "locker":
                    case "creator":
                    case "modifier":
                        epqEl.SetAttribute("value", ((long)((C4User)lbConfigValues.SelectedItem).Id).ToString());
                        break;
                    case "format":
                        epqEl.SetAttribute("value", ((long)((C4Format)lbConfigValues.SelectedItem).Id).ToString());
                        break;
                    case "object_type":
                        epqEl.SetAttribute("value", ((long)((C4ObjectType)lbConfigValues.SelectedItem).Id).ToString());
                        break;
                    case "folder_type":
                        epqEl.SetAttribute("value", ((long)((C4FolderType)lbConfigValues.SelectedItem).Id).ToString());
                        break;
                    case "language":
                        epqEl.SetAttribute("value", ((long)((C4Language)lbConfigValues.SelectedItem).Id).ToString());
                        break;
                }

            }
        }
    }
}
