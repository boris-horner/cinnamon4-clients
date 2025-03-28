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
using CDCplusLib.Common.GUI;
using CDCplusLib.EventData;

namespace CDCplusLib.TabControls
{
    public partial class ViewNotificationTabControl : UserControl, IGenericControl
    {
        private CmnObject _o;
        private CmnObject _relatesToO;
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

        public ViewNotificationTabControl()
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
            // TODO: I18N
            gbNotification.Text = Properties.Resources.lblNotification;
            lblTitle.Text = Properties.Resources.lblTitle;
            lblMessage.Text = Properties.Resources.lblMessage;
            lblRelatesTo.Text = Properties.Resources.lblRelatesTo;
        }

        private void ActivateControls()
        {
            // nothing to do
        }

        public void Reset(CmnSession s, GlobalApplicationData globalAppData, XmlElement configEl)
        {
            _gad = globalAppData;
        }

        public string GetTabText()
        {
            return Properties.Resources.lblNotification;
        }
        public void Init(Dictionary<long, IRepositoryNode> dict)
        {
            _dict = dict;
            _o = DictionaryHelper.GetSingleObject(dict);

            if (_o is null)
            {
                txtTitle.Text = "";
                txtMessage.Text = "";
            }
            else
            {
                HashSet<long> typeIds = new HashSet<long>();
                long notTypeId = (long)_o.Session.SessionConfig.C4Sc.MetasetTypesByName["notification"].Id;
                typeIds.Add(notTypeId);
                C4Metadata m = _o.Session.CommandSession.GetObjectMeta(_o.Id, typeIds);
                C4Metaset c4Ms = m.MetasetsByTypeId[notTypeId].First();   // assuming it's unique
                if (c4Ms is not null)
                {
                    XmlElement ms = c4Ms.Content;
                    txtTitle.Text = ms.SelectSingleNode("title").InnerText; // was: o_.Name
                    txtMessage.Text = ms.SelectSingleNode("message").InnerText.Replace("\n", "\r\n");
                    XmlNode relatesToN = ms.SelectSingleNode("relates_to");
                    if (relatesToN is null)
                    {
                        _relatesToO = null;
                        lnkRelatesTo.Text = "(none)";
                        lnkRelatesTo.Enabled = false;
                    }
                    else
                    {
                        long id;
                        if (long.TryParse(relatesToN.InnerText, out id))
                        {
                            _relatesToO = _o.Session.GetObject(id, false);
                            lnkRelatesTo.Text = _relatesToO.Parent.FolderPath + "/" + _relatesToO.Name;
                            lnkRelatesTo.Tag = _relatesToO;
                            lnkRelatesTo.Enabled = true;
                        }
                        else
                        {
                            lnkRelatesTo.Text = "(none)";
                            lnkRelatesTo.Enabled = false;
                        }
                    }
                }
            }

            ActivateControls();
        }

        public bool IsValid(Dictionary<long, IRepositoryNode> dict, IGenericControl.ContextType ct)
        {
            if (ct != IGenericControl.ContextType.Object)
                return false;
            CmnObject o = DictionaryHelper.GetSingleObject(dict);
            if (o is null)
                return false;
            return o.ObjectType.Name == "_notification";
        }
        private void InitImageList()
        {
            // nothing to do
        }
        public bool IsDirty
        {
            get
            {
                return false;
            }
        }

        public void Save()
        {
            // nothing to do
        }

        public void ReInit()
        {
            Init(_dict);
        }
        private void lnkRelatesTo_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
        {
            WindowSelectionData wsd = new WindowSelectionData();
            wsd.Selection = new Dictionary<long, IRepositoryNode> { { _relatesToO.Id, _relatesToO } };
            SessionWindowRequest?.Invoke(wsd);

        }
    }
}
