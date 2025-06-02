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
using C4ObjectApi.Repository;
using System.Xml;
using C4ObjectApi.Interfaces;
using CDCplusLib.Interfaces;
using CDCplusLib.DataModel;
using CDCplusLib.EventData;
using C4Logic;

namespace CDCplusLib.Common.GUI
{
    public partial class SelectObjectsDialog : Form
    {
        public enum SelectionModes { SelectFolder, SelectSingleObject, SelectMultiObjects, SaveSingleObject }
        private SelectionModes _sm;
        private readonly CmnSession _s;
        private CmnFolder _f;
        private XmlElement _configEl;
        private XmlElement _rldConfigEl;
        private string _title;
        private ResultListDisplay _rldNodes;
        private INodeDataProvider _nodeDataProvider;
        private GlobalApplicationData _gad;

        public event ListSelectionChangedEventHandler ListSelectionChanged;
        public event TreeSelectionChangedEventHandler TreeSelectionChanged;
        public SelectObjectsDialog(CmnSession s, XmlElement configEl, SelectionModes sm, CmnFolder initialFolder, string title, GlobalApplicationData gad)
        {
            InitializeComponent();
            _s = s;
            _sm = sm;
            _gad = gad;
            _f = initialFolder;
            _configEl = configEl;
            _rldConfigEl = (XmlElement)s.UserConfig.DocumentElement.SelectSingleNode("classes/result_list_display");
            string ndpAsm = _rldConfigEl.SelectSingleNode("node_data_provider/@assembly").InnerText;
            string ndpTp = _rldConfigEl.SelectSingleNode("node_data_provider/@type").InnerText;
            _nodeDataProvider = (INodeDataProvider)(s.SessionConfig.GetAssembly(ndpAsm).CreateInstance(ndpAsm + "." + ndpTp));
            _nodeDataProvider.Init(s, (XmlElement)_rldConfigEl.SelectSingleNode("configurations/configuration[@name='default']"), _rldConfigEl);
            _title = title;
        }
        private void AssignNodeList()
        {
            Text = _title + (_f == null ? "" : " - " + _f.FolderPath);
            CmnNodeList nl = new CmnNodeList(_s, null);
            if (_f != null) foreach (CmnFolder f in _f.GetSubfolders().Values) nl.List.Add(f.Id, f);
            else foreach (CmnFolder f in _s.GetSubfolders(null).Values) nl.List.Add(f.Id, f);

            CmnSession.Versions ver = CmnSession.Versions.All;
            if (optVersionLatest.Checked) ver = CmnSession.Versions.Head;
            else if (optVersionLatestBranches.Checked) ver = CmnSession.Versions.Branch;

            if (_f != null) foreach (CmnObject o in _f.GetObjects(ver).Values) nl.List.Add(o.Id, o);

            _rldNodes.NodeList = nl;
        }

        public CmnNodeList NodeList
        {
            get
            {
                return _rldNodes.NodeList;

            }
        }
        public DialogResult ShowSelectorDialog()
        {
            return ShowDialog();
        }
        private void SelectObjectsDialog_Shown(object sender, EventArgs e)
        {
            _rldNodes = new ResultListDisplay();
            _rldNodes.TreeSelectionChanged += new TreeSelectionChangedEventHandler(rldNodes_TreeSelectionChanged);
            splHor.Panel1.Controls.Add(_rldNodes);
            _rldNodes.Dock = DockStyle.Fill;
            //_rldNodes.TreeSelectionChanged += new ISessionWindow.TreeSelectionChangedEventHandler();
            _rldNodes.Init(_s, _configEl, _rldConfigEl, _nodeDataProvider);
            ctccListContext.Init(_s, true, _gad);
            AssignNodeList();
        }
        private void rldNodes_TreeSelectionChanged(WindowSelectionData wsd, ISessionWindow sw)
        {
            _f = wsd.SelectedFolder;
            AssignNodeList();
        }
        public Dictionary<long, IRepositoryNode> Selection
        {
            get
            {
                return _rldNodes.Selection;
            }
        }
        public CmnFolder Folder
        {
            get
            {
                return _f;
            }

            set
            {
                Cursor = Cursors.WaitCursor;
                _f = value;
                AssignNodeList();
                Cursor = null;
            }
        }
        private void optVersionLatest_CheckedChanged(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            AssignNodeList();
            Cursor = null;
        }

        private void optVersionLatestBranches_CheckedChanged(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            AssignNodeList();
            Cursor = null;
        }

        private void optVersionAll_CheckedChanged(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            AssignNodeList();
            Cursor = null;
        }

        private void cmdHome_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            _f = _s.SessionConfig.HomeFolder;
            AssignNodeList();
            Cursor = null;
        }

        private void cmdRoot_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            _f = null; // _s.RootFolder;
            AssignNodeList();
            Cursor = null;
        }

        private void cmdFolderUp_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            if (_f != null) _f = _f.Parent;
            AssignNodeList();
            Cursor = null;
        }

        private void cmdNewFolder_Click(object sender, EventArgs e)
        {
            EditStringValue esv = new EditStringValue(Properties.Resources.mnuNewFolder, Properties.Resources.lblNewName, "", Constants.VALID_NODE_NAME_REGEX);
            if (esv.ShowDialog() == DialogResult.OK)
            {
                Cursor = Cursors.WaitCursor;
                CmnFolder newF = _f.CreateSubfolder(esv.Value);
                AssignNodeList();
                Cursor = null;
            }
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void cmdOk_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

    }
}
