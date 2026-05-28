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
using CDCplusLib.Common;
using CDCplusLib.Interfaces;
using System.Xml;
using C4ObjectApi.Interfaces;
using C4ObjectApi.Repository;
using C4ObjectApi.Helpers;
using C4ServerConnector.Assets;
using CDCplusLib.EventData;
using CDCplusLib.Common.GUI;

namespace CDCplusLib.TabControls
{
    public partial class EditSummaryTabControl : UserControl, IGenericControl
    {
        private bool _isSuperuser;
        private CmnSession _s;
        private IRepositoryNode _n;
        private bool _enableEvents;
        private GlobalApplicationData _gad;
        private Dictionary<long, IRepositoryNode> _dict;

        public event SessionWindowRequestEventHandler SessionWindowRequest;
        public event ListSelectionChangedEventHandler ListSelectionChanged;
        public event TreeSelectionChangedEventHandler TreeSelectionChanged;
        public event ContextMenuRequestEventHandler ContextMenuRequest;
        public event FunctionRequestEventHandler FunctionRequest;
        public event NodesModifiedEventHandler NodesModified;
        public event KeyPressedEventHandler KeyPressedEvent;
        public event RefreshRequestEventHandler RefreshRequest;

        public EditSummaryTabControl()
        {
            InitializeComponent();
            LocalizeGUI();
            InitImageList();
        }


        public bool HasSelection
        {
            get
            {
                return false;
            }
        }
        public Dictionary<long, IRepositoryNode> Selection
        {
            get
            {
                return null;
            }
            set
            {

            }
        }
        public bool AutoRefresh
        {
            get
            {
                return true;
            }
        }
        public bool ListContext
        {
            get
            {
                return true;
            }
        }
        private void LocalizeGUI()
        {
        }

        public void Reset(CmnSession s, GlobalApplicationData globalAppData, XmlElement configEl)
        {
            _gad = globalAppData;
            _s = s;
            _isSuperuser = _s.User.GroupIds.Contains((long)_s.SessionConfig.C4Sc.GroupsByName["_superusers"].Id);
            _enableEvents = true;
        }

        public string GetTabText()
        {
            return Properties.Resources.lblSummary;
        }
        public void Init(Dictionary<long, IRepositoryNode> dict)
        {
            _enableEvents = false;  
            _dict = dict;
            _n = _dict.Values.First();
            cmdSave.Enabled = false;

            if (_n is null)
            {
                xtxtSummary.Text = "";
            }
            else
            {
                xtxtSummary.Text = _n.Summary.OuterXml;
            }
            _enableEvents = true;
        }

        public bool IsValid(Dictionary<long, IRepositoryNode> dict, IGenericControl.ContextType ct)
        {
            if (!_isSuperuser) return false;
            if (ct != IGenericControl.ContextType.Object) return false;
            if (dict.Count != 1) return false;
            return true;
        }
        private void InitImageList()
        {
            // nothing to do
        }
        public bool IsDirty
        {
            get
            {
                return cmdSave.Enabled;
            }
        }

        public void Save()
        {
            if (_n != null)
            {
                _enableEvents = false;
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(xtxtSummary.Text);
                if (_n.GetType() == typeof(CmnObject)) _s.CommandSession.SetObjectSummary(_n.Id, doc.DocumentElement);
                else _s.CommandSession.SetFolderSummary(_n.Id, doc.DocumentElement);
                WindowSelectionData wsd = new WindowSelectionData();
                wsd.Selection.Add(_n.Id, _n);
                NodesModified?.Invoke(wsd);
                _enableEvents = true;
            }
        }

        public void ReInit()
        {
            Init(_dict);
        }

        private void cmdSave_Click(object sender, EventArgs e)
        {
            Save();
            cmdSave.Enabled = false;
        }

        private void xtxtSummary_TextChanged(object sender, EventArgs e)
        {
            if(_enableEvents) cmdSave.Enabled = true;
        }
    }
}
