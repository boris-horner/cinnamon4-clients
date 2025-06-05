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
using CDCplusLib.TabControls.SearchEditorNodes;
using CDCplusLib.Common;
using CDCplusLib.Interfaces;
using CDCplusLib.CommonGui;
using CDCplusLib.Helpers;
using C4ObjectApi.Interfaces;
using C4ObjectApi.Repository;
using C4ObjectApi.Operations.Search;
using C4ObjectApi.Global;
using C4ObjectApi.Helpers;
using C4ServerConnector.Assets;
using C4GeneralGui.GuiElements;
using CDCplusLib.EventData;
using CDCplusLib.Common.GUI;

namespace CDCplusLib.TabControls
{
    public partial class SearchEditor : UserControl, IGenericControl
    {
        public enum Operators
        {
            Equals,
            Less,
            LessOrEqual,
            Greater,
            GreaterOrEqual,
            StartsWith,
            Contains
        }

        private const long SEARCH_BATCH_SIZE = 500L;
        private GlobalApplicationData _gad;
        private XmlElement _configEl;
        private CmnObject _o;
        private C4Metaset _ms;
        private CmnSession _s;
        private int _maxResultCount;
        private bool _enableEvents;
        private Dictionary<string, XmlElement> _fieldToTermDefinition;
        private Dictionary<string, XmlElement> _nameToMasterData;
        private readonly Random _rnd;
        private Dictionary<long, IRepositoryNode> _dict;
        private C4Metadata _meta;

        public event SessionWindowRequestEventHandler SessionWindowRequest;
        public event ListSelectionChangedEventHandler ListSelectionChanged;
        public event TreeSelectionChangedEventHandler TreeSelectionChanged;
        public event ContextMenuRequestEventHandler ContextMenuRequest;
        public event FunctionRequestEventHandler FunctionRequest;
        public event NodesModifiedEventHandler NodesModified;
        public event KeyPressedEventHandler KeyPressedEvent;
        public event RefreshRequestEventHandler RefreshRequest;

        public SearchEditor()
        {
            InitializeComponent();
            _o = null;
            IsDirty = false;
            ttSearch.SetToolTip(cmdSave, Properties.Resources.ttSave);
            ttSearch.SetToolTip(optVersionLatest, Properties.Resources.lblShowLatestInHead);
            ttSearch.SetToolTip(optVersionLatestBranches, Properties.Resources.lblShowLatestInBranches);
            ttSearch.SetToolTip(optVersionAll, Properties.Resources.lblShowAllVersions);
            ttSearch.SetToolTip(optSearchObjects, Properties.Resources.lblSearchObjects);
            ttSearch.SetToolTip(optSearchFolders, Properties.Resources.lblSearchFolders);
            ttSearch.SetToolTip(cmdRunQueryWithPreview, Properties.Resources.lblSearch);
            ttSearch.SetToolTip(cmdRunQuery, Properties.Resources.lblSearch);
            ttSearch.SetToolTip(cmdShowQuery, Properties.Resources.lblShowQuery);
            lblFieldName.Text = Properties.Resources.lblFieldName;
            chkNotTerm.Text = Properties.Resources.lblNot;
            chkAnyTerm.Text = Properties.Resources.lblAny;
            chkVariable.Text = Properties.Resources.lblVariableSpecifyName;
            tlpTerm.Visible = false;
            //SessionWindowRequest += SessionWindowRequestEventHandler;
            _rnd = new Random();
        }

        public bool HasSelection { get { return false; } }
        public Dictionary<long, IRepositoryNode> Selection { get { return null; } set { } }
        public bool AutoRefresh { get { return true; } }
        public bool ListContext { get { return true; } }

        protected virtual void SessionWindowRequestEventHandler(WindowSelectionData wsd)
        {
            if(_enableEvents)
            {
                _enableEvents = false;
                SessionWindowRequest?.Invoke(wsd);
                _enableEvents = true;
            }
        }

        private void ActivateControls()
        {
            tlpTerm.Visible = (tvwSearchDef.SelectedNode != null && tvwSearchDef.SelectedNode.Tag.GetType() != typeof(SearchOperatorGui));
            cmdSave.Enabled = IsDirty;
            cmdShowQuery.Enabled = tvwSearchDef.Nodes.Count > 0;
            cmdRunQuery.Enabled = tvwSearchDef.Nodes.Count > 0;
        }

        public bool IsDirty { get; private set; }


        public string GetTabText()
        {
            return Properties.Resources.lblSearchEditor;
        }
        public void Init(Dictionary<long, IRepositoryNode> dict)
        {
            _o = DictionaryHelper.GetSingleObject(dict);
            _enableEvents = false;
            tvwSearchDef.Nodes.Clear();
            pTermEditorContainer.Controls.Clear();
            if (_o == null)
            {
                optSearchObjects.Checked = true;
                optVersionAll.Checked = true;
            }
            else
            {
                //_ms = _o.Session.CommandSession.getobjectmetaset(_o.Id, (long)_o.Session.SessionConfig.C4Sc.MetasetTypesByName["search"].Id);
                HashSet<long> typeIds = new HashSet<long>();
                typeIds.Add((long)_o.Session.SessionConfig.C4Sc.MetasetTypesByName["search"].Id);
                _meta = _o.Session.CommandSession.GetObjectMeta(_o.Id, typeIds);
                InitFromMetadata();
            }
            IsDirty = false;
            ActivateControls();
            _enableEvents = true;
        }
        private void InitFromMetadata()
        {
            if (_meta.MetasetsByTypeId.ContainsKey((long)_o.Session.SessionConfig.C4Sc.MetasetTypesByName["search"].Id))
            {
                HashSet<C4Metaset> metasets = _meta.MetasetsByTypeId[(long)_o.Session.SessionConfig.C4Sc.MetasetTypesByName["search"].Id];
                if (metasets.Count == 1)
                {
                    _ms = metasets.First();
                    XmlNode soN = _ms.Content.SelectSingleNode("search_type/@value");
                    optSearchObjects.Checked = soN == null || soN.InnerText == "objects";
                    XmlNode svN = _ms.Content.SelectSingleNode("search_version/@value");
                    if (svN == null) optVersionLatest.Checked = true;
                    else switch (svN.InnerText)
                        {
                            case "all": optVersionAll.Checked = true; break;
                            case "branches": optVersionLatestBranches.Checked = true; break;
                            case "head": optVersionLatest.Checked = true; break;
                        }
                    AppendChildren(tvwSearchDef.Nodes, _ms.Content);
                    tvwSearchDef.ExpandAll();
                    if (tvwSearchDef.Nodes.Count > 0)
                    {
                        tvwSearchDef.SelectedNode = tvwSearchDef.Nodes[0];
                        if (tvwSearchDef.SelectedNode.Tag.GetType() != typeof(SearchOperatorGui))
                            ((UserControl)tvwSearchDef.SelectedNode.Tag).Visible = !chkAnyTerm.Checked;

                    }
                }
                else _ms = null;
            }
            else _ms = null;

        }

        private void AppendChildren(TreeNodeCollection nodes, XmlElement parentEl)
        {
            foreach (XmlElement childEl in parentEl.SelectNodes("*"))
            {
                TreeNode childNode = null;
                string iconName = null;
                string label = null;

                switch (childEl.Name)
                {
                    case "operator_group":
                        SearchOperatorGui sog = new SearchOperatorGui(childEl);
                        sog.ControlChanged += new EventHandler(OperatorControlChanged);

                        GetOperatorInfo(sog, out iconName, out label);
                        childNode = nodes.Add(Guid.NewGuid().ToString(), label, iconName, iconName);
                        childNode.Tag = sog;
                        AppendChildren(childNode.Nodes, childEl);
                        break;
                    case "term":

                        string fieldName = childEl.GetAttribute("index_item");  // actually, the field, not the index item is searched.
                        ISearchTermGui stg = GetSearchTermGui(fieldName, childEl);
                        if (stg != null)
                        {
                            XmlElement termDefEl = _fieldToTermDefinition[fieldName];
                            stg.Init(_s, fieldName, childEl, termDefEl, _nameToMasterData, _gad);
                            stg.ControlChanged += new EventHandler(TermParametersChangedHandler);
                            chkAnyTerm.Checked = stg.AnyValue;
                            chkNotTerm.Checked = stg.Negation;
                            chkVariable.Checked = stg.VariableName != null;
                            vtxtVariableName.ReadOnly = stg.VariableName == null;
                            vtxtVariableName.Text = (stg.VariableName ?? "");
                            vtxtVariableName.RegularExpression = chkVariable.Checked ? "^(?!\\s*$).+" : "";
                            //vtxtVariableName.InternalInvalidColor = chkVariable.Checked ? Color.Yellow : SystemColors.Control;

                            GetTermInfo(stg, out iconName, out label);
                            childNode = nodes.Add(Guid.NewGuid().ToString(), label, iconName, iconName);
                            childNode.Tag = stg;
                        }
                        else
                        {
                            throw new ApplicationException(string.Concat("Search field ", fieldName, " not found"));
                        }
                        break;
                }
            }
        }

        private void GetTermInfo(ISearchTermGui stg, out string iconName, out string label)
        {
            if (stg.AnyValue)
            {
                iconName = stg.Negation ? "flag-black.png" : "flag-yellow.png";
                label = stg.Negation ? "No value of " + stg.FieldDisplayName : "Any value of " + stg.FieldDisplayName;
            }
            else            // or
            {
                iconName = stg.Negation ? "flag-red.png" : "flag-green.png";
                label = stg.Negation ?
                    "NOT " + stg.FieldDisplayName + " " + GetOperatorDisplayName(stg.Operator) + " " + stg.ValueDisplayText :
                    stg.FieldDisplayName + " " + GetOperatorDisplayName(stg.Operator) + " " + stg.ValueDisplayText;
            }

        }
        private void GetOperatorInfo(SearchOperatorGui sog, out string iconName, out string label)
        {
            if (sog.OperatorType == SearchOperatorGui.OperatorTypes.And)
            {
                iconName = "folder-green.png";
                label = "AND";
            }
            else            // or
            {
                iconName = "folder-yellow.png";
                label = "OR";
            }
        }

        void OperatorControlChanged(object sender, EventArgs e)
        {
            SearchOperatorGui sog = (SearchOperatorGui)tvwSearchDef.SelectedNode.Tag;
            string iconName = null;
            string label = null;
            GetOperatorInfo(sog, out iconName, out label);
            tvwSearchDef.SelectedNode.ImageKey = iconName;
            tvwSearchDef.SelectedNode.SelectedImageKey = iconName;
            tvwSearchDef.SelectedNode.Text = label;
            IsDirty = true;
            ActivateControls();
        }

        void TermParametersChangedHandler(object sender, EventArgs e)
        {
            ISearchTermGui stg = (ISearchTermGui)tvwSearchDef.SelectedNode.Tag;
            string iconName = null;
            string label = null;
            GetTermInfo(stg, out iconName, out label);
            tvwSearchDef.SelectedNode.ImageKey = iconName;
            tvwSearchDef.SelectedNode.SelectedImageKey = iconName;
            tvwSearchDef.SelectedNode.Text = label;
            IsDirty = true;
            ActivateControls();
        }
        void TermChanged(ISearchTermGui stg)
        {
            tvwSearchDef.SelectedNode.Tag = stg;
            stg.ControlChanged += new EventHandler(TermParametersChangedHandler);
            string iconName = null;
            string label = null;
            GetTermInfo(stg, out iconName, out label);
            tvwSearchDef.SelectedNode.ImageKey = iconName;
            tvwSearchDef.SelectedNode.SelectedImageKey = iconName;
            tvwSearchDef.SelectedNode.Text = label;
            IsDirty = true;
            ActivateControls();
        }

        private string GetOperatorDisplayName(Operators op)
        {
            switch (op)
            {
                case Operators.Equals: return Properties.Resources.lblEquals;
                case Operators.Less: return Properties.Resources.lblLessThan;
                case Operators.LessOrEqual: return Properties.Resources.lblLessThanOrEqual;
                case Operators.Greater: return Properties.Resources.lblGreaterThan;
                case Operators.GreaterOrEqual: return Properties.Resources.lblGreaterThanOrEqual;
                case Operators.StartsWith: return Properties.Resources.lblStartsWith;
                case Operators.Contains: return Properties.Resources.lblContains;
            }
            return null;
        }

        private string GetOperatorString(Operators op)
        {
            switch (op)
            {
                case Operators.Equals: return "is_equal";
                case Operators.Less: return "is_less";
                case Operators.LessOrEqual: return "is_equal_or_less";
                case Operators.Greater: return "is_greater";
                case Operators.GreaterOrEqual: return "is_equal_or_greater";
                case Operators.StartsWith: return "string_starts_with";
                case Operators.Contains: return "string_contains";
            }
            return null;
        }

        private ISearchTermGui GetSearchTermGui(string fieldName, XmlElement termEl)
        {
            if (!_fieldToTermDefinition.ContainsKey(fieldName)) return null;
            XmlElement termDefEl = _fieldToTermDefinition[fieldName];
            ISearchTermGui result = (ISearchTermGui)_s.SessionConfig.GetAssembly(termDefEl.GetAttribute("assembly")).CreateInstance(termDefEl.GetAttribute("assembly") + "." + termDefEl.GetAttribute("type"));
            result.Init(_s, fieldName, termEl, termDefEl, _nameToMasterData, _gad);
            return result;
        }
        public bool IsValid(Dictionary<long, IRepositoryNode> dict, IGenericControl.ContextType ct)
        {
            if (ct != IGenericControl.ContextType.Object) return false;
            CmnObject o = DictionaryHelper.GetSingleObject(dict);
            if (o == null) return false;
            return (o.ObjectType.Name == "_search");
        }
        public void ReInit()
        {
            Init(_dict);
        }

        public void Reset(CmnSession s, GlobalApplicationData globalAppData, XmlElement configEl)
        {

            _enableEvents = false;
            _gad = globalAppData;
            _s = s;
            _configEl = configEl;
            _fieldToTermDefinition = new Dictionary<string, XmlElement>();
            cmdShowQuery.Visible = _s.IsSuperuser;
            foreach (XmlElement termEl in _configEl.SelectNodes("custom/search_term_controls[@selector='field_name']/search_term_control"))
            {
                string fieldName = termEl.GetAttribute("field_name");
                if (!_fieldToTermDefinition.ContainsKey(fieldName)) _fieldToTermDefinition.Add(fieldName, termEl);
            }
            foreach (C4IndexItem ii in _s.SessionConfig.C4Sc.IndexItemsByName.Values)
            {
                System.Diagnostics.Debug.Print(ii.Name);
                XmlElement termEl = (XmlElement)_configEl.SelectSingleNode(
                    "custom/search_term_controls[@selector='index_type_name']/search_term_control[@index_type_name='" + ii.IndexTypeName + "']");
                if (termEl != null && !_fieldToTermDefinition.ContainsKey(ii.FieldName)) _fieldToTermDefinition.Add(ii.FieldName, termEl);
            }
            cboIndexItem.Items.Clear();
            Dictionary<string, C4IndexItem> fields = new Dictionary<string, C4IndexItem>();
            foreach (C4IndexItem ii in _s.SessionConfig.C4Sc.IndexItemsByName.Values)
            {
                if (!fields.ContainsKey(ii.FieldName))
                {
                    fields.Add(ii.FieldName, ii);
                    cboIndexItem.Items.Add(ii);
                }
            }

            _nameToMasterData = new Dictionary<string, XmlElement>();
            foreach (XmlElement mdoEl in _configEl.SelectNodes("custom/master_data/master_data_object"))
            {
                string name = mdoEl.GetAttribute("name");
                switch (mdoEl.GetAttribute("type"))
                {
                    case "metaset":
                        {
                            if (!_nameToMasterData.ContainsKey(name))
                            {
                                // find object
                                // TODO: add folder path reference as search criteria
                                XmlElement mdSourceEl = (XmlElement)mdoEl.SelectSingleNode("custom/control_data/master_data_source");
                                CmnObject mdO = _s.SearchSingleObject("<BooleanQuery>" +
                                    "<Clause occurs=\"must\" ><ExactPointQuery type=\"long\" fieldName=\"object_type\" value=\"" + ((long)_s.SessionConfig.C4Sc.ObjectTypesByName["_config"].Id).ToString() + "\"/></Clause>" +
                                    "<Clause occurs=\"must\" ><TermQuery fieldName=\"name\" >" + mdoEl.SelectSingleNode("master_data_source").InnerText.ToLower() + "</TermQuery></Clause>" +
                                    "<Clause occurs=\"must\" ><TermQuery fieldName=\"latest_head\" >true</TermQuery></Clause></BooleanQuery>", false);

                                // get metaset
                                C4Metaset ms = null;
                                try
                                {
                                    HashSet<long> typeIds = new HashSet<long>();
                                    long mdTypeNameId = (long)_s.SessionConfig.C4Sc.MetasetTypesByName[mdoEl.SelectSingleNode("master_data_metaset_name").InnerText].Id;
                                    typeIds.Add(mdTypeNameId);
                                    C4Metadata m = _s.CommandSession.GetObjectMeta(mdO.Id, typeIds);
                                    ms = m.MetasetsByTypeId[mdTypeNameId].First();
                                    _nameToMasterData.Add(name, ms.Content);
                                }
                                catch (NullReferenceException ex)
                                {
                                    throw new ApplicationException("SearchEditor: master_data_metaset_name missing from the configuration.");
                                }
                            }
                            break;
                        }
                    case "config_entry":
                        {
                            XmlDocument ce = _s.GetConfigEntry(mdoEl.GetAttribute("name"));
                            _nameToMasterData.Add(name, ce.DocumentElement);
                            break;
                        }
                }
            }
            XmlNode mpsN = _configEl.SelectSingleNode("custom/maxpagesize");
            _maxResultCount = mpsN == null ? -1 : int.Parse(mpsN.InnerText);
            _enableEvents = true;
            IsDirty = false;
            ActivateControls();
        }

        public void Save()
        {
            _enableEvents = false;
            //C4Metadata md = new C4Metadata();
            C4MetasetType msType = _o.Session.SessionConfig.C4Sc.MetasetTypesByName["search"];
            //md.MetasetsByTypeId.Add(msTypeId, new HashSet<C4Metaset>());

            XmlDocument ms = new XmlDocument();
            ms.AppendChild(ms.CreateElement("metaset"));
            XmlElement typeEl = (XmlElement)ms.DocumentElement.AppendChild(ms.CreateElement("search_type"));
            typeEl.SetAttribute("value", optSearchObjects.Checked ? "objects" : "folders");
            XmlElement versionEl = (XmlElement)ms.DocumentElement.AppendChild(ms.CreateElement("search_version"));
            string versionValue = null;
            if (optVersionAll.Checked) versionValue = "all";
            else if (optVersionLatestBranches.Checked) versionValue = "branches";
            else versionValue = "head";
            versionEl.SetAttribute("value", versionValue);

            AppendChildrenFromTree(tvwSearchDef.Nodes[0], ms.DocumentElement);


            //md.MetasetsByTypeId[msTypeId].Add(new C4Metaset(msTypeId, _o.Id, ms.DocumentElement));


            _o.Lock();
            ApiHelper.SetUniqueObjectMetaset(_o.Session.CommandSession, _o.Session.SessionConfig.C4Sc, _o.Id, ms.DocumentElement, msType.Name, true, _ms == null ? null : _ms.Id);
            _o.Unlock();

            IsDirty = false;
            ActivateControls();
            WindowSelectionData wsd = new WindowSelectionData();
            wsd.Selection.Add(_o.Id, _o);
            NodesModified?.Invoke(wsd);
            _enableEvents = true;
        }

        private void AddOperatorToParent(TreeNode parent)
        {
            SearchOperatorGui sog = new SearchOperatorGui();
            sog.ControlChanged += new EventHandler(OperatorControlChanged);

            string iconName = null;
            string label = null;
            GetOperatorInfo(sog, out iconName, out label);
            TreeNodeCollection ns = parent == null ? tvwSearchDef.Nodes : parent.Nodes;
            TreeNode childNode = ns.Add(Guid.NewGuid().ToString(), label, iconName, iconName);
            tvwSearchDef.ExpandAll();
            childNode.Tag = sog;
        }

        private void AddTermToParent(TreeNode parent)
        {
            string defaultFieldName = "name"; // name is default (does this need to be configurable?)
            XmlElement termDefEl = _fieldToTermDefinition[defaultFieldName];
            ISearchTermGui stg = (ISearchTermGui)_s.SessionConfig.GetAssembly(termDefEl.GetAttribute("assembly")).CreateInstance(termDefEl.GetAttribute("assembly") + "." + termDefEl.GetAttribute("type"));
            stg.Init(_s, defaultFieldName, null, termDefEl, _nameToMasterData, _gad);
            stg.ControlChanged += new EventHandler(TermParametersChangedHandler);
            chkAnyTerm.Checked = false;
            chkNotTerm.Checked = false;
            chkVariable.Checked = false;
            vtxtVariableName.ReadOnly = true;
            vtxtVariableName.Text = "";
            vtxtVariableName.RegularExpression = "";
            //vtxtVariableName.InternalInvalidColor = chkVariable.Checked ? Color.Yellow : SystemColors.Control;

            string iconName = null;
            string label = null;
            GetTermInfo(stg, out iconName, out label);
            TreeNodeCollection ns = parent == null ? tvwSearchDef.Nodes : parent.Nodes;
            TreeNode childNode = ns.Add(Guid.NewGuid().ToString(), label, iconName, iconName);
            tvwSearchDef.ExpandAll();
            childNode.Tag = stg;
        }

        private void ContextMenuClickHandler(object sender, EventArgs e)
        {

            ToolStripItem item = (ToolStripItem)sender;
            switch (item.Name)
            {
                case "delete":
                    {
                        TreeNode selectedNode = (TreeNode)item.Tag;
                        selectedNode.Remove();
                        pTermEditorContainer.Controls.Clear();
                        IsDirty = true;
                        ActivateControls();
                        break;
                    }
                case "add_operator":
                    {
                        TreeNode selectedNode = (TreeNode)item.Tag;
                        AddOperatorToParent(selectedNode);
                        IsDirty = true;
                        ActivateControls();
                        break;
                    }
                case "add_term":
                    {
                        TreeNode selectedNode = (TreeNode)item.Tag;
                        AddTermToParent(selectedNode);
                        IsDirty = true;
                        ActivateControls();
                        break;
                    }
            }
        }

        private void TvwSearchDef_AfterSelect(object sender, TreeViewEventArgs e)
        {
            _enableEvents = false;
            pTermEditorContainer.Controls.Clear();
            if (tvwSearchDef.SelectedNode == null)
            {
                tlpTerm.Visible = false;
            }
            else
            {
                if (tvwSearchDef.SelectedNode.Tag == null)
                {
                    // TODO: error message
                    System.Diagnostics.Debug.Print("error");
                    tlpTerm.Visible = false;
                }
                else
                {
                    if (tvwSearchDef.SelectedNode.Tag.GetType() == typeof(SearchOperatorGui))
                    {
                        tlpTerm.Visible = false;
                        SearchOperatorGui sog = (SearchOperatorGui)tvwSearchDef.SelectedNode.Tag;
                        pTermEditorContainer.Controls.Add(sog);
                        sog.Dock = DockStyle.Fill;
                    }
                    else
                    {
                        tlpTerm.Visible = true;
                        ISearchTermGui stg = (ISearchTermGui)tvwSearchDef.SelectedNode.Tag;
                        chkNotTerm.Checked = stg.Negation;
                        chkAnyTerm.Checked = stg.AnyValue;
                        pTermEditorContainer.Controls.Add((UserControl)stg);
                        ((UserControl)stg).Dock = DockStyle.Fill;
                        cboIndexItem.Text = stg.FieldDisplayName;
                    }
                }
            }
            _enableEvents = true;
        }

        private void ChkVariable_CheckedChanged(object sender, EventArgs e)
        {
            if (tvwSearchDef.SelectedNode != null && _ms != null)
            {
                ISearchTermGui stg = (ISearchTermGui)tvwSearchDef.SelectedNode.Tag;
                vtxtVariableName.ReadOnly = !chkVariable.Checked;
                if (chkVariable.Checked)
                {
                    HashSet<string> varNames = new HashSet<string>();
                    foreach (XmlNode n in _ms.Content.SelectNodes("//@variable_name")) varNames.Add(n.InnerText);
                    string testName = null;
                    do
                    {
                        testName = "variable_" + _rnd.Next(0, 1000).ToString("D3");
                    } while (varNames.Contains(testName));
                    vtxtVariableName.Text = testName;
                }
                else vtxtVariableName.Text = "";
                vtxtVariableName.RegularExpression = chkVariable.Checked ? "^(?!\\s*$).+" : "";
                //vtxtVariableName.InternalInvalidColor = chkVariable.Checked ? Color.Yellow : SystemColors.Control;
                stg.VariableName = (chkVariable.Checked ? vtxtVariableName.Text : null);
            }
            IsDirty = true;
            ActivateControls();
        }

        private void ChkNotTerm_CheckedChanged(object sender, EventArgs e)
        {
            if (tvwSearchDef.SelectedNode != null)
            {
                if (tvwSearchDef.SelectedNode.Tag.GetType() != typeof(SearchOperatorGui))
                {
                    ISearchTermGui stg = (ISearchTermGui)tvwSearchDef.SelectedNode.Tag;
                    stg.Negation = chkNotTerm.Checked;
                    TermParametersChangedHandler(this, new EventArgs());
                }
            }
            IsDirty = true;
            ActivateControls();
        }

        private void VtxtVariableName_TextChanged(object sender, EventArgs e)
        {
            ISearchTermGui stg = (ISearchTermGui)tvwSearchDef.SelectedNode.Tag;
            if (vtxtVariableName.Text.Trim().Length > 0) stg.VariableName = vtxtVariableName.Text;
            IsDirty = true;
            ActivateControls();
        }

        private void ChkAnyTerm_CheckedChanged(object sender, EventArgs e)
        {
            if (tvwSearchDef.SelectedNode != null)
            {
                if (tvwSearchDef.SelectedNode.Tag.GetType() != typeof(SearchOperatorGui))
                {
                    ISearchTermGui stg = (ISearchTermGui)tvwSearchDef.SelectedNode.Tag;
                    stg.AnyValue = chkAnyTerm.Checked;
                    ((UserControl)tvwSearchDef.SelectedNode.Tag).Visible = !chkAnyTerm.Checked;
                    TermParametersChangedHandler(this, new EventArgs());
                }
            }
            IsDirty = true;
            ActivateControls();
        }

        private void ChkNotOperator_CheckedChanged(object sender, EventArgs e)
        {
            SearchOperatorGui stg = (SearchOperatorGui)tvwSearchDef.SelectedNode.Tag;
            OperatorControlChanged(this, new EventArgs());
            IsDirty = true;
            ActivateControls();
        }

        private void CboIndexItem_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_enableEvents)
            {
                C4IndexItem ii = (C4IndexItem)cboIndexItem.SelectedItem;
                XmlElement termDefEl = _fieldToTermDefinition[ii.FieldName];
                ISearchTermGui stg = (ISearchTermGui)_s.SessionConfig.GetAssembly(termDefEl.GetAttribute("assembly")).CreateInstance(termDefEl.GetAttribute("assembly") + "." + termDefEl.GetAttribute("type"));
                stg.Init(_s, ii.FieldName, null, termDefEl, _nameToMasterData, _gad);
                pTermEditorContainer.Controls.Clear();
                chkNotTerm.Checked = false;
                chkAnyTerm.Checked = false;
                pTermEditorContainer.Controls.Add((UserControl)stg);
                ((UserControl)stg).Dock = DockStyle.Fill;
                TermChanged(stg);
                IsDirty = true;
                ActivateControls();
            }
        }

        private void TvwSearchDef_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                TreeNode selectedNode = tvwSearchDef.GetNodeAt(e.X, e.Y);
                ContextMenuStrip menu = new ContextMenuStrip();
                if (selectedNode == null)
                {
                    if (tvwSearchDef.Nodes.Count == 0)
                    {
                        // add operator
                        ToolStripItem mi = menu.Items.Add(Properties.Resources.lblAddOperator);
                        mi.Name = "add_operator";
                        mi.Tag = selectedNode;
                        mi.Click += new EventHandler(ContextMenuClickHandler);

                        // add term 
                        mi = menu.Items.Add(Properties.Resources.lblAddTerm);
                        mi.Name = "add_term";
                        mi.Tag = selectedNode;
                        mi.Click += new EventHandler(ContextMenuClickHandler);
                    }
                }
                else
                {
                    if (selectedNode.Tag.GetType() == typeof(SearchOperatorGui))
                    {
                        // delete
                        ToolStripItem mi = menu.Items.Add(Properties.Resources.lblDelete);
                        mi.Name = "delete";
                        mi.Tag = selectedNode;
                        mi.Click += new EventHandler(ContextMenuClickHandler);

                        // add operator
                        mi = menu.Items.Add(Properties.Resources.lblAddOperator);
                        mi.Name = "add_operator";
                        mi.Tag = selectedNode;
                        mi.Click += new EventHandler(ContextMenuClickHandler);

                        // add term 
                        mi = menu.Items.Add(Properties.Resources.lblAddTerm);
                        mi.Name = "add_term";
                        mi.Tag = selectedNode;
                        mi.Click += new EventHandler(ContextMenuClickHandler);
                    }
                    else
                    {
                        // delete
                        ToolStripItem mi = menu.Items.Add(Properties.Resources.lblDelete);
                        mi.Name = "delete";
                        mi.Tag = selectedNode;
                        mi.Click += new EventHandler(ContextMenuClickHandler);
                    }
                }
                menu.Show(tvwSearchDef, e.X, e.Y);
            }
        }

        private void CmdShowQuery_Click(object sender, EventArgs e)
        {
            if (tvwSearchDef.Nodes.Count == 0)
            {
                StandardMessage.ShowMessage(Properties.Resources.msgEmptySearchDefinition, StandardMessage.Severity.ErrorMessage, this);
                return;
            }

            LuceneHelper.VersionSearchBehaviour vsb = optSearchObjects.Checked ? (optVersionAll.Checked ? LuceneHelper.VersionSearchBehaviour.AllVersions : (optVersionLatest.Checked ? LuceneHelper.VersionSearchBehaviour.LatestHead : LuceneHelper.VersionSearchBehaviour.LatestBranches)) : LuceneHelper.VersionSearchBehaviour.Folders;
            XmlDocument query = LuceneHelper.BuildQuery(tvwSearchDef.Nodes[0], vsb);
            if (query != null)
            {
                TextWindow tw = new TextWindow(Properties.Resources.lblQuery, LuceneHelper.IndentXml(query));
                tw.ShowDialog(this);
            }
        }

        private void CmdRunQuery_Click(object sender, EventArgs e)
        {
            if (tvwSearchDef.Nodes.Count == 0)
            {
                StandardMessage.ShowMessage(Properties.Resources.msgEmptySearchDefinition, StandardMessage.Severity.ErrorMessage, this);
                return;
            }

            LuceneHelper.VersionSearchBehaviour vsb = optSearchObjects.Checked ? (optVersionAll.Checked ? LuceneHelper.VersionSearchBehaviour.AllVersions : (optVersionLatest.Checked ? LuceneHelper.VersionSearchBehaviour.LatestHead : LuceneHelper.VersionSearchBehaviour.LatestBranches)) : LuceneHelper.VersionSearchBehaviour.Folders;
            XmlDocument query = LuceneHelper.BuildQuery(tvwSearchDef.Nodes[0], vsb);
            if (query != null)
            {
                int maxResults = 1000;// TODO: softcode max search results
                SearchResults res = null;
                Dictionary<long, IRepositoryNode> results;
                if (optSearchObjects.Checked)    // search objects
                {
                    SearchObjectsOperation soo = new SearchObjectsOperation(_s, query.OuterXml);
                    Dictionary<long, CmnObject> objs;
                    if (soo.TotalResultCount > maxResults)
                    {
                        if (MessageBox.Show(String.Format(Properties.Resources.msgMoreThanXResults, soo.TotalResultCount.ToString()), "Search results", MessageBoxButtons.YesNo) == DialogResult.Yes)
                            objs = soo.GetObjects(-1, -1, SEARCH_BATCH_SIZE);
                        else objs = soo.GetObjects(0, maxResults, SEARCH_BATCH_SIZE);
                    }
                    else objs = soo.GetObjects(-1, -1, SEARCH_BATCH_SIZE);
                    results = objs.ToDictionary(
                        kvp => kvp.Key,
                        kvp => (IRepositoryNode)kvp.Value
                    );
                }
                else    // search folders
                {
                    res = _o.Session.SearchFolders(query.InnerXml);
                    results = res.Folders.ToDictionary(
                        kvp => kvp.Key,
                        kvp => (IRepositoryNode)kvp.Value
                    );

                }
                WindowSelectionData wsd = new WindowSelectionData();
                wsd.RootNodeType = SessionTree.RootNodeTypes.Results;
                wsd.ResultList = results;
                SessionWindowRequest?.Invoke(wsd);
            }
        }

        private void CmdSave_Click(object sender, EventArgs e)
        {
            Save();
        }

        private void AppendChildrenFromTree(TreeNode n, XmlElement parentEl)
        {
            if (n.Tag.GetType() == typeof(SearchOperatorGui))  // operator
            {
                XmlElement ogEl = (XmlElement)parentEl.AppendChild(parentEl.OwnerDocument.CreateElement("operator_group"));
                ogEl.SetAttribute("type", ((SearchOperatorGui)n.Tag).OperatorType == SearchOperatorGui.OperatorTypes.And ? "and" : "or");
                foreach (TreeNode subN in n.Nodes)
                {
                    AppendChildrenFromTree(subN, ogEl);
                }
            }
            else  // term
            {
                ISearchTermGui stc = (ISearchTermGui)n.Tag;
                XmlElement termEl = (XmlElement)parentEl.AppendChild(parentEl.OwnerDocument.CreateElement("term"));
                termEl.SetAttribute("index_item", stc.FieldName);
                termEl.SetAttribute("operator", GetOperatorString(stc.Operator));
                termEl.SetAttribute("any", stc.AnyValue ? "true" : "false");
                termEl.SetAttribute("negative", stc.Negation ? "true" : "false");
                termEl.SetAttribute("value", stc.AnyValue ? "" : stc.Value);
            }
        }


        private void OptSearchObjects_CheckedChanged(object sender, EventArgs e)
        {
            IsDirty = true;
            ActivateControls();
        }

        private void OptSearchFolders_CheckedChanged(object sender, EventArgs e)
        {
            IsDirty = true;
            ActivateControls();
        }

        private void OptVersionLatest_CheckedChanged(object sender, EventArgs e)
        {
            IsDirty = true;
            ActivateControls();
        }

        private void OptVersionLatestBranches_CheckedChanged(object sender, EventArgs e)
        {
            IsDirty = true;
            ActivateControls();
        }

        private void OptVersionAll_CheckedChanged(object sender, EventArgs e)
        {
            IsDirty = true;
            ActivateControls();
        }

        private void cmdRunQueryWithPreview_Click(object sender, EventArgs e)
        {
            if (tvwSearchDef.Nodes.Count == 0)
            {
                StandardMessage.ShowMessage(Properties.Resources.msgEmptySearchDefinition, StandardMessage.Severity.ErrorMessage, this);
                return;
            }

            LuceneHelper.VersionSearchBehaviour vsb = optSearchObjects.Checked ? (optVersionAll.Checked ? LuceneHelper.VersionSearchBehaviour.AllVersions : (optVersionLatest.Checked ? LuceneHelper.VersionSearchBehaviour.LatestHead : LuceneHelper.VersionSearchBehaviour.LatestBranches)) : LuceneHelper.VersionSearchBehaviour.Folders;
            XmlDocument query = LuceneHelper.BuildQuery(tvwSearchDef.Nodes[0], vsb);
            if (query != null)
            {
                if (optSearchObjects.Checked)    // search objects
                {
                    HashSet<long> ids = _s.CommandSession.SearchObjectIds(query.OuterXml);
                    Dictionary<long, C4Object> results = _s.CommandSession.GetObjectsById(ids, false);
                    SearchResultsPreview srp = new SearchResultsPreview(_s);
                    srp.SetList(results);
                    srp.SessionWindowRequest += SessionWindowRequestEventHandler;
                    srp.ShowDialog(this);
                    srp.SessionWindowRequest -= SessionWindowRequestEventHandler;
                }
                else    // search folders
                {
                    HashSet<long> ids = _s.CommandSession.SearchFolderIds(query.OuterXml);
                    Dictionary<long, C4Folder> results = _s.CommandSession.GetFoldersById(ids);
                    SearchResultsPreview srp = new SearchResultsPreview(_s);
                    srp.SetList(results);
                    srp.SessionWindowRequest += SessionWindowRequestEventHandler;
                    srp.ShowDialog(this);
                    srp.SessionWindowRequest -= SessionWindowRequestEventHandler;
                }
            }
        }
    }
}
