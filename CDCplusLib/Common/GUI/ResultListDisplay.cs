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

namespace CDCplusLib.Common.GUI
{
    public partial class ResultListDisplay : UserControl
    {

        private CmnNodeList _nl;
        private IIconService _iconService;
        private INodeDataProvider _nodeDataProvider;
        private KeyEventTable _keyEvents;
        private ListViewSort _lvwSort;
        private bool _enableCheck;
        private bool _useTimer = true;
        private View _view;
        private System.Windows.Forms.Timer selectionChangedTimer;
        public bool EventsActive { get; set; }

        //private bool _idleHandlerSet;
        private CmnSession _s;

        //public event EventHandler SelectedIndexChanged;
        public event TreeSelectionChangedEventHandler TreeSelectionChanged;
        public event ListSelectionChangedEventHandler ListSelectionChanged;
        public event ContextMenuRequestEventHandler ContextMenuRequest;
        public event FunctionRequestEventHandler FunctionRequest;
        public event KeyPressedEventHandler KeyPressedEvent;
        public event RefreshRequestEventHandler RefreshRequest;

        public ResultListDisplay()
        {
            InitializeComponent();
            EventsActive = false;
        }
        public void Init(CmnSession s, XmlElement customConfigEl, XmlElement rldConfigEl, INodeDataProvider ndp)
        {
            _nodeDataProvider = ndp;
            //_nodeDataProvider.Init(s, customConfigEl, rldConfigEl);
            _s = s;
            //_idleHandlerSet = false;
            _enableCheck = false;
			_view = View.Details;
            XmlAttribute frs = ((XmlAttribute)(rldConfigEl.SelectSingleNode("appearance/full_row_select/@value")));
            _useTimer = s.Server.LocalSettings.ContainsKey("use_threaded_gui_controls") ? s.Server.LocalSettings["use_threaded_gui_controls"] == "true" : true;
            if(_useTimer)
            {
                selectionChangedTimer = new System.Windows.Forms.Timer();
                selectionChangedTimer.Interval = 100; // Delay in milliseconds
                selectionChangedTimer.Tick += SelectionChangedTimer_Tick;
            }

            lvwNodeList.FullRowSelect = (frs != null && frs.Value == "true");
            lvwNodeList.Columns.Clear();
            foreach (string colName in _nodeDataProvider.GetColumnDefinitions().Keys)
            {
                NodeColumnDefinition col = _nodeDataProvider.GetColumnDefinitions()[colName];
                lvwNodeList.Columns.Add(colName, _nodeDataProvider.GetFieldTitle(colName), col.Width, col.HorAlignment, -1);
            }

            XmlNode customEventsN = customConfigEl.SelectSingleNode("events");  // detect legacy config
            if(_keyEvents==null) _keyEvents = new KeyEventTable(customEventsN==null?(XmlElement)rldConfigEl.SelectSingleNode("key_events"):customConfigEl, KeyEventTable.Modes.List); // these are only loaded for the first time since they are global

            if (_lvwSort == null) _lvwSort = new ListViewSort(lvwNodeList, true);

            if (_nodeDataProvider.GetDefaultSortColumn() != null)
            {
                int i = lvwNodeList.Columns.IndexOfKey(_nodeDataProvider.GetDefaultSortColumn());
                _lvwSort.Sort(i, _nodeDataProvider.GetDefaultSortColumnOrder());
            }
            _iconService = ((IIconService)(_s.GetSessionExtension("icon_service")));
            lvwNodeList.SmallImageList = _iconService.GlobalSmallImageList;
            lvwNodeList.LargeImageList = _iconService.GlobalLargeImageList;

            string stateImagePath = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "Images", "Icons", "16x16");
            lvwNodeList.StateImageList = new ImageList();
            lvwNodeList.StateImageList.ImageSize = new Size(16, 16);
            lvwNodeList.StateImageList.ColorDepth = ColorDepth.Depth24Bit;
            lvwNodeList.StateImageList.Images.Add("blank", new Bitmap(Path.Combine(stateImagePath, "blank.png")));
            lvwNodeList.StateImageList.Images.Add("link", new Bitmap(Path.Combine(stateImagePath, "emblem-symbolic-link.png")));

            EventsActive = true;
		}
        protected virtual void TreeSelectionChangedEventHandler(WindowSelectionData wsd, ISessionWindow sw)
        {
            TreeSelectionChanged?.Invoke(wsd, null);
        }
        protected virtual void ListSelectionChangedEventHandler(WindowSelectionData wsd)
        {
            ListSelectionChanged?.Invoke(wsd);
        }
        protected virtual void ContextMenuRequestEventHandler(WindowSelectionData wsd, Point position)
        {
            ContextMenuRequest?.Invoke(wsd, position);
        }
        protected virtual void FunctionRequestEventHandler(WindowSelectionData wsd)
        {
            FunctionRequest?.Invoke(wsd, null, null);
        }
        protected virtual void KeyPressedEventHandler(WindowSelectionData wsd, Keys key, bool shift, bool ctrl, bool alt)
        {
            KeyPressedEvent?.Invoke(wsd, key, shift, ctrl, alt);
        }
        protected virtual void RefreshRequestEventHandler(WindowSelectionData wsd)
        {
            RefreshRequest?.Invoke();
        }

        public View View {
            get
            {
                return _view;
            }
            set
            {
                _view = value;
                lvwNodeList.View = _view;
            }
        }



        public CmnNodeList NodeList
        {
            get
            {
                return _nl;
            }
            set
            {
				_enableCheck = true;
                lvwNodeList.BeginUpdate();
                _nl = value;
                Dictionary<long, IRepositoryNode> selNl = Selection;

                if (_nl == null) lvwNodeList.Items.Clear();
                else
                {
                    IList<long> lvsi = ContentsListViewHelper.GetSelectedIds(lvwNodeList);

                    //  TODO: determine delta and decide whether to update or to reload
                    lvwNodeList.Items.Clear();
                    lvwNodeList.Visible = true;
                    lvwNodeList.View = View.Details;
                    lvwNodeList.CheckBoxes = true;
                    _lvwSort.Active = false;
                    foreach (IRepositoryNode ow in _nl.List.Values)
                    {
                        string iconKey = (_nl.IconOverrides != null && _nl.IconOverrides.ContainsKey(ow.Id))?_nl.IconOverrides[ow.Id] :_iconService.GetIconKey(ow);
                        ListViewItem lvi = lvwNodeList.Items.Add(ow.Id.ToString(), ow.Name, iconKey);
                        if(ow.GetType()==typeof(CmnObject))
                        {
                            CmnObject o = (CmnObject)ow;
                            if(!o.LatestHead)
                            {
                                if (o.LatestBranch) lvi.ForeColor = System.Drawing.Color.SteelBlue;
                                else if(o.Version.Contains(".")) lvi.ForeColor = System.Drawing.Color.LightSkyBlue;
                                else lvi.ForeColor = System.Drawing.Color.Silver;
                            }
                        }
                        lvi.StateImageIndex = ow.Link == null ? 0 : 1;
                        bool first = true;
                        foreach (string colName in _nodeDataProvider.GetColumnDefinitions().Keys)
                        {
                            if (first) first = false;
                            else
                            {
                                //NodeColumnDefinition colDef = _nodeDataProvider.GetColumnDefinitions()[colName];
                                string columnValue = null;
                                try
                                {
                                    columnValue = _nodeDataProvider.GetValue(ow, colName);
                                }
                                catch(Exception ex)
                                {
                                    columnValue = "";
                                }
                                lvi.SubItems.Add(columnValue);
                            }
                        }

                        lvi.Tag = ow;

                        //System.Diagnostics.Debug.Print("list value");
                        // TODO: only if already up front
                        //lvwNodeList.Focus();
                        // SelectionChanged()
                    }
                    _lvwSort.Active = true;
                    Selection = selNl;

                    foreach (long id in lvsi)
                    {
                        if (lvwNodeList.Items.ContainsKey(id.ToString())) lvwNodeList.Items[id.ToString()].Selected = true;
                    }
                    if (_lvwSort != null) _lvwSort.ReSort();

                    WindowSelectionData wsd = new WindowSelectionData();
                    foreach (ListViewItem li in lvwNodeList.SelectedItems)
                    {
                        IRepositoryNode ow = (IRepositoryNode)li.Tag;
                        wsd.Selection.Add(ow.Id, ow);
                    }
                    ListSelectionChanged?.Invoke(wsd);

                    if ((lvwNodeList.SelectedItems.Count > 0)) lvwNodeList.SelectedItems[0].EnsureVisible();

                    lvwNodeList.EndUpdate();
                }
                _enableCheck = false;
			}
		}




        public Dictionary<long, IRepositoryNode> Selection
        {
            get
            {
                Dictionary<long, IRepositoryNode> result = new Dictionary<long, IRepositoryNode>();
                foreach (ListViewItem n in lvwNodeList.SelectedItems)
                {
                    IRepositoryNode ow = ((IRepositoryNode)(n.Tag));
                    if (!result.ContainsKey(ow.Id)) result.Add(ow.Id, ow);
                }

                return result;
            }
            set
            {
                lvwNodeList.SelectedItems.Clear();
                if(value!=null)
                {
                    foreach (long owId in value.Keys)
                    {
                        IRepositoryNode ow = value[owId];
                        if (lvwNodeList.Items.ContainsKey(owId.ToString())) lvwNodeList.Items[owId.ToString()].Selected = true;
                    }
                    if(lvwNodeList.SelectedItems.Count>0) lvwNodeList.SelectedItems[0].EnsureVisible();
                }
            }
        }

        private void lvwNodeList_DoubleClick(object sender, EventArgs e)
        {
            if ((lvwNodeList.SelectedItems[0].Tag).GetType() == typeof(CmnFolder))
            {
                WindowSelectionData wsd = new WindowSelectionData();
                wsd.SelectedFolder = lvwNodeList.SelectedItems[0].Tag as CmnFolder;
                TreeSelectionChanged?.Invoke(wsd, null);
            }
            else if ((lvwNodeList.SelectedItems[0].Tag).GetType() == typeof(CmnObject))
            {
                CmnObject o = (CmnObject)(lvwNodeList.SelectedItems[0].Tag);
                WindowSelectionData wsd = new WindowSelectionData();
                wsd.Selection.Add(o.Id, o);
                FunctionRequest?.Invoke(wsd, null, null);   // default function
            }
        }

        private void lvwNodeList_KeyDown(object sender, KeyEventArgs e)
        {
            // first check for hard coded keys
            switch (e.KeyCode)
            {
                // Ignore these
                case Keys.Up: break;
                case Keys.Down: break;
                case Keys.Space: break;
                case Keys.ShiftKey: break;
                case Keys.Menu: break;// this is the ALT key
                case Keys.ControlKey: break;
                default:
                    // handle configured function
                    bool useContext = false;
                    KeyEvent ke = default(KeyEvent);
                    if (lvwNodeList.SelectedItems.Count > 0)
                    {
                        if (lvwNodeList.SelectedItems.Count == 1)
                        {
                            if (lvwNodeList.SelectedItems[0].Tag is CmnFolder) ke = new KeyEvent(e.KeyCode, e.Shift, e.Control, e.Alt, KeyEvent.KeyEventSelection.FolderSelected);
                            else ke = new KeyEvent(e.KeyCode, e.Shift, e.Control, e.Alt, KeyEvent.KeyEventSelection.ObjectSelected);
                        }
                        else ke = new KeyEvent(e.KeyCode, e.Shift, e.Control, e.Alt, KeyEvent.KeyEventSelection.ListSelected);
                    }
                    else
                    {
                        if (_nl.Context == null) ke = new KeyEvent(e.KeyCode, e.Shift, e.Control, e.Alt, KeyEvent.KeyEventSelection.NothingSelected);
                        else
                        {
                            ke = new KeyEvent(e.KeyCode, e.Shift, e.Control, e.Alt, KeyEvent.KeyEventSelection.FolderSelected);    // parent folder
                            useContext = true;
                        }
                    }

                    try
                    {
                        KeyEventReaction ker = _keyEvents.GetKeyEventReaction(ke);
                        switch (ker.EventType)
                        {
                            case KeyEventReaction.EventTypes.ExecuteMethod:
                                // send message to Session Window to execute matching function
                                //switch (ke.Filter)
                                //{
                                //    case KeyEvent.KeyEventSelection.FolderSelected:
                                //        kpMsg.FunctionType = KeyPressedMessage.FunctionTypes.FolderFunction;
                                //        break;
                                //    case KeyEvent.KeyEventSelection.ObjectSelected:
                                //        kpMsg.FunctionType = KeyPressedMessage.FunctionTypes.ObjectFunction;
                                //        break;
                                //    case KeyEvent.KeyEventSelection.ListSelected:
                                //        kpMsg.FunctionType = KeyPressedMessage.FunctionTypes.ListFunction;
                                //        break;
                                //    default:
                                //        throw new ApplicationException("Invalid function type for no selection");
                                //}
                                WindowSelectionData wsd = null;
                                if(useContext)
                                {
                                    wsd = new WindowSelectionData();
                                    wsd.SelectedFolder = _nl.Context;
                                }
                                else
                                {
                                    wsd = new WindowSelectionData();
                                    foreach (ListViewItem li in lvwNodeList.SelectedItems)
                                    {
                                        wsd.Selection.Add(((IRepositoryNode)(li.Tag)).Id, (IRepositoryNode)(li.Tag));
                                    }
                                }

                                FunctionRequest?.Invoke(wsd, ker.Assembly, ker.Type);

                                break;
                            case KeyEventReaction.EventTypes.Refresh:
                                RefreshRequest?.Invoke();
                                break;
                            case KeyEventReaction.EventTypes.SelectAll:
                                foreach (ListViewItem lvi in lvwNodeList.Items) lvi.Selected = true;
                                break;
                            //SelectionChanged()
                            default:
                                System.Diagnostics.Debug.Print("Unsupported key was pressed");
                                break;
                        }
                    }
                    catch (ApplicationException ex)
                    {
                        StandardMessage.ShowMessage("Caught ApplicationException", StandardMessage.Severity.ErrorMessage, this, ex);
                    }
                    catch (Exception ex)
                    {
                        StandardMessage.ShowMessage("Caught Exception", StandardMessage.Severity.ErrorMessage, this, ex);
                    }

                    break;
            }
        }

        private void lvwNodeList_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                //SelectionChanged()
                if (e.Button == MouseButtons.Right)
                {
                    WindowSelectionData wsd = new WindowSelectionData();
                    foreach (ListViewItem li in lvwNodeList.SelectedItems)
                    {
                        wsd.Selection.Add(((IRepositoryNode)(li.Tag)).Id, (IRepositoryNode)(li.Tag));
                    }
                    ContextMenuRequest?.Invoke(wsd, lvwNodeList.PointToScreen(e.Location));
                }
            }
            catch (ApplicationException ex)
            {
                StandardMessage.ShowMessage("Caught ApplicationException", StandardMessage.Severity.ErrorMessage, this, ex);
            }
            catch (Exception ex)
            {
                StandardMessage.ShowMessage("Caught Exception", StandardMessage.Severity.ErrorMessage, this, ex);
            }
        }


        private void SelectionChangedTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                selectionChangedTimer.Stop();
                if (InvokeRequired)
                {
                    Invoke((MethodInvoker)SelectionChanged);
                }
                else
                {
                    SelectionChanged();
                }
            }
            catch (Exception ex)
            {
                Debug.Print(string.Join("\n", ex.GetType().ToString(), ex.Message, ex.StackTrace));
            }
        }

        private void SelectionChanged()
        {
            if (EventsActive)
            {
                WindowSelectionData wsd = new WindowSelectionData();
                foreach (ListViewItem li in lvwNodeList.SelectedItems)
                {
                    IRepositoryNode ow = (IRepositoryNode)li.Tag;
                    wsd.Selection.Add(ow.Id, ow);
                }
                ListSelectionChanged?.Invoke(wsd);
            }
        }
        private void lvwNodeList_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if(_useTimer)
                {
                    selectionChangedTimer.Stop();
                    selectionChangedTimer.Start();
                }
                else
                {
                    SelectionChanged();
                }
            }
            catch (Exception ex)
            {
                Debug.Print(string.Join("\n", ex.GetType().ToString(), ex.Message, ex.StackTrace));
            }
        }

        private void lvwNodeList_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if(!_enableCheck) e.NewValue = e.CurrentValue;
        }



    }
}
