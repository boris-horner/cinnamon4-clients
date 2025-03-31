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
using CDCplusLib.DataModel;
using CDCplusLib.Interfaces;
using CDCplusLib.Common.KeyboardEvents;
using CDCplusLib.Helpers;
using C4ObjectApi.Interfaces;
using C4ObjectApi.Repository;
using C4GeneralGui.GuiElements;
using System.Diagnostics;
using CDCplusLib.EventData;
using C4ServerConnector;
using C4ServerConnector.Assets;

namespace CDCplusLib.Common.GUI
{
    public partial class ResultListDisplayLight : UserControl
    {

        private Dictionary<long, IC4Node> _nl;
        private ListViewSort _lvwSort;
        public bool EventsActive { get; set; }
        private Dictionary<string, int> _columnDefinitions;
        private Dictionary<string, string> _knownStandardFields;
        private IIconService _iconService;

        //private bool _idleHandlerSet;
        private CmnSession _s;
        private C4Session _c4s;
        private C4SessionConfiguration _c4sc;

        //public event EventHandler SelectedIndexChanged;
        public ResultListDisplayLight()
        {
            InitializeComponent();
            _knownStandardFields = new Dictionary<string, string>();
            _knownStandardFields.Add("name", Properties.Resources.lblName);
            _knownStandardFields.Add("id", Properties.Resources.lblId);
            _knownStandardFields.Add("format", Properties.Resources.lblFormat);
            _knownStandardFields.Add("type", Properties.Resources.lblType);
            _knownStandardFields.Add("version", Properties.Resources.lblVersion);
            _knownStandardFields.Add("size", Properties.Resources.lblSize);
            _knownStandardFields.Add("owner", Properties.Resources.lblOwner);
            _knownStandardFields.Add("creator", Properties.Resources.lblCreatedBy);
            _knownStandardFields.Add("created", Properties.Resources.lblCreationDate);
            _knownStandardFields.Add("modifier", Properties.Resources.lblModifiedBy);
            _knownStandardFields.Add("modified", Properties.Resources.lblModificationDate);
            _knownStandardFields.Add("locked", Properties.Resources.lblLockedBy);
            _knownStandardFields.Add("lifecycle", Properties.Resources.lblLifecycle);
            _knownStandardFields.Add("language", Properties.Resources.lblLanguage);
            _knownStandardFields.Add("path", Properties.Resources.lblPath);

            EventsActive = false;
        }
        public void Init(CmnSession s)
        {
            _s = s;
            _c4s = _s.CommandSession;
            _c4sc = _s.SessionConfig.C4Sc;

            _columnDefinitions = new Dictionary<string, int>();
            _columnDefinitions.Add("name", 200); // width
            _columnDefinitions.Add("type", 100);
            _columnDefinitions.Add("version", 60);
            _columnDefinitions.Add("id", 100);

            lvwNodeList.Columns.Clear();
            foreach (string colName in _columnDefinitions.Keys)
            {
                string colLabel = _knownStandardFields[colName];
                ColumnHeader ch = lvwNodeList.Columns.Add(colName, colLabel);
                ch.Width = _columnDefinitions[colName];
            }

            if (_lvwSort == null) _lvwSort = new ListViewSort(lvwNodeList, true);
            _iconService = ((IIconService)(_s.GetSessionExtension("icon_service")));
            lvwNodeList.SmallImageList = _iconService.GlobalSmallImageList;
            EventsActive = true;
		}
        public Dictionary<long, IC4Node> Selection
        {
            get
            {
                Dictionary<long, IC4Node> result = new Dictionary<long, IC4Node>();
                foreach (ListViewItem n in lvwNodeList.SelectedItems)
                {
                    IC4Node ow = ((IC4Node)(n.Tag));
                    if (!result.ContainsKey(ow.Id)) result.Add(ow.Id, ow);
                }

                return result;
            }
        }


        public Dictionary<long, IC4Node> NodeList
        {
            get
            {
                return _nl;
            }
            set
            {
                lvwNodeList.BeginUpdate();
                _nl = value;
                lvwNodeList.Items.Clear();
                if (_nl != null)
                {
                    foreach (IC4Node c4n in _nl.Values)
                    {
                        bool isObject = false;
                        ListViewItem lvi = lvwNodeList.Items.Add(c4n.Id.ToString(), c4n.Name, _iconService.GetIconKey(c4n));
                        if(c4n.GetType()==typeof(C4Object))
                        {
                            isObject = true;
                            C4Object o = (C4Object)c4n;
                            if(!o.LatestHead)
                            {
                                if (o.LatestBranch) lvi.ForeColor = System.Drawing.Color.SteelBlue;
                                else if(o.Version.Contains(".")) lvi.ForeColor = System.Drawing.Color.LightSkyBlue;
                                else lvi.ForeColor = System.Drawing.Color.Silver;
                            }
                        }
                        bool first = true;
                        foreach (string colName in _columnDefinitions.Keys)
                        {
                            if (first) first = false;
                            else
                            {
                                string columnValue = null;
                                try
                                {
                                    switch(colName)
                                    {
                                        case "name":
                                            columnValue = c4n.Name;
                                            break;
                                        case "type":
                                            columnValue = isObject?_c4sc.ObjectTypesById[(long)c4n.TypeId].Name:_c4sc.FolderTypesById[(long)c4n.TypeId].Name;
                                            break;
                                        case "version":
                                            if (isObject) columnValue = ((C4Object)c4n).Version;
                                            else columnValue = "";
                                            break;
                                        case "id":
                                            columnValue = c4n.Id.ToString();
                                            break;
                                    }
                                }
                                catch(Exception ex)
                                {
                                    columnValue = "";
                                }
                                lvi.SubItems.Add(columnValue);
                            }
                        }

                        lvi.Tag = c4n;
                    }
                    lvwNodeList.EndUpdate();
                }
			}
		}

        private void lvwNodeList_SelectedIndexChanged(object sender, EventArgs e)
        {
            //try
            //{
            //    if(_useTimer)
            //    {
            //        selectionChangedTimer.Stop();
            //        selectionChangedTimer.Start();
            //    }
            //    else
            //    {
            //        SelectionChanged();
            //    }
            //}
            //catch (Exception ex)
            //{
            //    Debug.Print(string.Join("\n", ex.GetType().ToString(), ex.Message, ex.StackTrace));
            //}
        }
    }
}
