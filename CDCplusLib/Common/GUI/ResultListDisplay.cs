using System.Xml;
using Microsoft.Win32;
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
        private CmnSession _s;

        private ImageList _stateImageList;
        private string _stateImagePath;
        private bool _displayEventsHooked;

        public bool EventsActive { get; set; }

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

            HandleCreated += ResultListDisplay_HandleCreated;
            Disposed += ResultListDisplay_Disposed;
        }

        public void Init(CmnSession s, XmlElement customConfigEl, XmlElement rldConfigEl, INodeDataProvider ndp)
        {
            _nodeDataProvider = ndp;
            _s = s;
            _enableCheck = false;
            _view = View.Details;

            XmlAttribute frs = (XmlAttribute)rldConfigEl.SelectSingleNode("appearance/full_row_select/@value");

            _useTimer = s.Server.LocalSettings.ContainsKey("use_threaded_gui_controls")
                ? s.Server.LocalSettings["use_threaded_gui_controls"] == "true"
                : true;

            if (_useTimer && selectionChangedTimer == null)
            {
                selectionChangedTimer = new System.Windows.Forms.Timer();
                selectionChangedTimer.Interval = 100;
                selectionChangedTimer.Tick += SelectionChangedTimer_Tick;
            }

            lvwNodeList.FullRowSelect = frs != null && frs.Value == "true";
            lvwNodeList.View = _view;

            /*
             * Important:
             * Set CheckBoxes before assigning StateImageList.
             * Repeatedly toggling CheckBoxes while a StateImageList is attached
             * can cause corrupted/wrong state images after handle recreation.
             */
            lvwNodeList.CheckBoxes = true;

            lvwNodeList.Columns.Clear();
            foreach (string colName in _nodeDataProvider.GetColumnDefinitions().Keys)
            {
                NodeColumnDefinition col = _nodeDataProvider.GetColumnDefinitions()[colName];
                lvwNodeList.Columns.Add(
                    colName,
                    _nodeDataProvider.GetFieldTitle(colName),
                    col.Width,
                    col.HorAlignment,
                    -1);
            }

            XmlNode customEventsN = customConfigEl.SelectSingleNode("events");
            if (_keyEvents == null)
            {
                _keyEvents = new KeyEventTable(
                    customEventsN == null
                        ? (XmlElement)rldConfigEl.SelectSingleNode("key_events")
                        : customConfigEl,
                    KeyEventTable.Modes.List);
            }

            if (_lvwSort == null)
                _lvwSort = new ListViewSort(lvwNodeList, true);

            if (_nodeDataProvider.GetDefaultSortColumn() != null)
            {
                int i = lvwNodeList.Columns.IndexOfKey(_nodeDataProvider.GetDefaultSortColumn());
                _lvwSort.Sort(i, _nodeDataProvider.GetDefaultSortColumnOrder());
            }

            _iconService = (IIconService)_s.GetSessionExtension("icon_service");

            _stateImagePath = Path.Combine(
                Path.GetDirectoryName(Application.ExecutablePath),
                "Images",
                "Icons",
                "16x16");

            ReassignImageLists();

            HookDisplayEvents();

            EventsActive = true;
        }

        private void HookDisplayEvents()
        {
            if (_displayEventsHooked)
                return;

            SystemEvents.DisplaySettingsChanged += SystemEvents_DisplaySettingsChanged;
            _displayEventsHooked = true;
        }

        private void ResultListDisplay_Disposed(object sender, EventArgs e)
        {
            if (_displayEventsHooked)
            {
                SystemEvents.DisplaySettingsChanged -= SystemEvents_DisplaySettingsChanged;
                _displayEventsHooked = false;
            }

            if (selectionChangedTimer != null)
            {
                selectionChangedTimer.Tick -= SelectionChangedTimer_Tick;
                selectionChangedTimer.Dispose();
                selectionChangedTimer = null;
            }

            if (_stateImageList != null)
            {
                _stateImageList.Dispose();
                _stateImageList = null;
            }
        }

        private void ResultListDisplay_HandleCreated(object sender, EventArgs e)
        {
            if (_iconService != null)
                ReassignImageLists();
        }

        private void SystemEvents_DisplaySettingsChanged(object sender, EventArgs e)
        {
            if (IsDisposed)
                return;

            if (InvokeRequired)
            {
                BeginInvoke((MethodInvoker)ReassignImageLists);
            }
            else
            {
                ReassignImageLists();
            }
        }

        private void ReassignImageLists()
        {
            if (_iconService == null || lvwNodeList == null || lvwNodeList.IsDisposed)
                return;

            lvwNodeList.BeginUpdate();

            try
            {
                lvwNodeList.SmallImageList = null;
                lvwNodeList.LargeImageList = null;
                lvwNodeList.StateImageList = null;

                lvwNodeList.SmallImageList = _iconService.GlobalSmallImageList;
                lvwNodeList.LargeImageList = _iconService.GlobalLargeImageList;

                RebuildStateImageList();
                lvwNodeList.StateImageList = _stateImageList;

                lvwNodeList.Invalidate();
            }
            finally
            {
                lvwNodeList.EndUpdate();
            }
        }

        private void RebuildStateImageList()
        {
            ImageList old = _stateImageList;

            ImageList fresh = new ImageList
            {
                ImageSize = new Size(16, 16),
                ColorDepth = ColorDepth.Depth32Bit
            };

            fresh.Images.Add("blank", new Bitmap(Path.Combine(_stateImagePath, "blank.png")));
            fresh.Images.Add("link", new Bitmap(Path.Combine(_stateImagePath, "emblem-symbolic-link.png")));

            _stateImageList = fresh;

            old?.Dispose();
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

        public View View
        {
            get => _view;
            set
            {
                _view = value;
                lvwNodeList.View = _view;
            }
        }

        public CmnNodeList NodeList
        {
            get => _nl;
            set
            {
                _enableCheck = true;
                lvwNodeList.BeginUpdate();

                try
                {
                    _nl = value;

                    Dictionary<long, IRepositoryNode> selNl = Selection;

                    if (_nl == null)
                    {
                        lvwNodeList.Items.Clear();
                        return;
                    }

                    IList<long> lvsi = ContentsListViewHelper.GetSelectedIds(lvwNodeList);

                    lvwNodeList.Items.Clear();
                    lvwNodeList.Visible = true;
                    lvwNodeList.View = View.Details;

                    /*
                     * Do not set lvwNodeList.CheckBoxes here.
                     * It is already set once in Init before StateImageList is assigned.
                     */

                    _lvwSort.Active = false;

                    foreach (IRepositoryNode ow in _nl.List.Values)
                    {
                        string iconKey =
                            _nl.IconOverrides != null && _nl.IconOverrides.ContainsKey(ow.Id)
                                ? _nl.IconOverrides[ow.Id]
                                : _iconService.GetIconKey(ow);

                        ListViewItem lvi = lvwNodeList.Items.Add(ow.Id.ToString(), ow.Name, iconKey);

                        if (ow.GetType() == typeof(CmnObject))
                        {
                            CmnObject o = (CmnObject)ow;

                            if (!o.LatestHead)
                            {
                                if (o.LatestBranch)
                                    lvi.ForeColor = Color.SteelBlue;
                                else if (o.Version.Contains("."))
                                    lvi.ForeColor = Color.LightSkyBlue;
                                else
                                    lvi.ForeColor = Color.Silver;
                            }
                        }

                        lvi.StateImageIndex = ow.Link == null ? 0 : 1;

                        bool first = true;
                        foreach (string colName in _nodeDataProvider.GetColumnDefinitions().Keys)
                        {
                            if (first)
                            {
                                first = false;
                            }
                            else
                            {
                                string columnValue;

                                try
                                {
                                    columnValue = _nodeDataProvider.GetValue(ow, colName);
                                }
                                catch
                                {
                                    columnValue = "";
                                }

                                lvi.SubItems.Add(columnValue);
                            }
                        }

                        lvi.Tag = ow;
                    }

                    _lvwSort.Active = true;

                    Selection = selNl;

                    foreach (long id in lvsi)
                    {
                        if (lvwNodeList.Items.ContainsKey(id.ToString()))
                            lvwNodeList.Items[id.ToString()].Selected = true;
                    }

                    _lvwSort?.ReSort();

                    WindowSelectionData wsd = new WindowSelectionData();
                    foreach (ListViewItem li in lvwNodeList.SelectedItems)
                    {
                        IRepositoryNode ow = (IRepositoryNode)li.Tag;
                        wsd.Selection.Add(ow.Id, ow);
                    }

                    ListSelectionChanged?.Invoke(wsd);

                    if (lvwNodeList.SelectedItems.Count > 0)
                        lvwNodeList.SelectedItems[0].EnsureVisible();
                }
                finally
                {
                    lvwNodeList.EndUpdate();
                    _enableCheck = false;
                }
            }
        }

        public Dictionary<long, IRepositoryNode> Selection
        {
            get
            {
                Dictionary<long, IRepositoryNode> result = new Dictionary<long, IRepositoryNode>();

                foreach (ListViewItem n in lvwNodeList.SelectedItems)
                {
                    IRepositoryNode ow = (IRepositoryNode)n.Tag;

                    if (!result.ContainsKey(ow.Id))
                        result.Add(ow.Id, ow);
                }

                return result;
            }
            set
            {
                lvwNodeList.SelectedItems.Clear();

                if (value == null)
                    return;

                foreach (long owId in value.Keys)
                {
                    if (lvwNodeList.Items.ContainsKey(owId.ToString()))
                        lvwNodeList.Items[owId.ToString()].Selected = true;
                }

                if (lvwNodeList.SelectedItems.Count > 0)
                    lvwNodeList.SelectedItems[0].EnsureVisible();
            }
        }

        private void lvwNodeList_DoubleClick(object sender, EventArgs e)
        {
            if (lvwNodeList.SelectedItems.Count == 0)
                return;

            if (lvwNodeList.SelectedItems[0].Tag.GetType() == typeof(CmnFolder))
            {
                WindowSelectionData wsd = new WindowSelectionData();
                wsd.SelectedFolder = lvwNodeList.SelectedItems[0].Tag as CmnFolder;
                TreeSelectionChanged?.Invoke(wsd, null);
            }
            else if (lvwNodeList.SelectedItems[0].Tag.GetType() == typeof(CmnObject))
            {
                CmnObject o = (CmnObject)lvwNodeList.SelectedItems[0].Tag;
                WindowSelectionData wsd = new WindowSelectionData();
                wsd.Selection.Add(o.Id, o);
                FunctionRequest?.Invoke(wsd, null, null);
            }
        }

        private void lvwNodeList_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Up:
                case Keys.Down:
                case Keys.Space:
                case Keys.ShiftKey:
                case Keys.Menu:
                case Keys.ControlKey:
                    break;

                default:
                    HandleConfiguredKey(e);
                    break;
            }
        }

        private void HandleConfiguredKey(KeyEventArgs e)
        {
            bool useContext = false;
            KeyEvent ke;

            if (lvwNodeList.SelectedItems.Count > 0)
            {
                if (lvwNodeList.SelectedItems.Count == 1)
                {
                    ke = lvwNodeList.SelectedItems[0].Tag is CmnFolder
                        ? new KeyEvent(e.KeyCode, e.Shift, e.Control, e.Alt, KeyEvent.KeyEventSelection.FolderSelected)
                        : new KeyEvent(e.KeyCode, e.Shift, e.Control, e.Alt, KeyEvent.KeyEventSelection.ObjectSelected);
                }
                else
                {
                    ke = new KeyEvent(e.KeyCode, e.Shift, e.Control, e.Alt, KeyEvent.KeyEventSelection.ListSelected);
                }
            }
            else
            {
                if (_nl.Context == null)
                {
                    ke = new KeyEvent(e.KeyCode, e.Shift, e.Control, e.Alt, KeyEvent.KeyEventSelection.NothingSelected);
                }
                else
                {
                    ke = new KeyEvent(e.KeyCode, e.Shift, e.Control, e.Alt, KeyEvent.KeyEventSelection.FolderSelected);
                    useContext = true;
                }
            }

            try
            {
                KeyEventReaction ker = _keyEvents.GetKeyEventReaction(ke);

                switch (ker.EventType)
                {
                    case KeyEventReaction.EventTypes.ExecuteMethod:
                        WindowSelectionData wsd = new WindowSelectionData();

                        if (useContext)
                        {
                            wsd.SelectedFolder = _nl.Context;
                        }
                        else
                        {
                            foreach (ListViewItem li in lvwNodeList.SelectedItems)
                            {
                                IRepositoryNode node = (IRepositoryNode)li.Tag;
                                wsd.Selection.Add(node.Id, node);
                            }
                        }

                        FunctionRequest?.Invoke(wsd, ker.Assembly, ker.Type);
                        break;

                    case KeyEventReaction.EventTypes.Refresh:
                        RefreshRequest?.Invoke();
                        break;

                    case KeyEventReaction.EventTypes.SelectAll:
                        foreach (ListViewItem lvi in lvwNodeList.Items)
                            lvi.Selected = true;
                        break;

                    default:
                        Debug.Print("Unsupported key was pressed");
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
        }

        private void lvwNodeList_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button == MouseButtons.Right)
                {
                    WindowSelectionData wsd = new WindowSelectionData();

                    foreach (ListViewItem li in lvwNodeList.SelectedItems)
                    {
                        IRepositoryNode node = (IRepositoryNode)li.Tag;
                        wsd.Selection.Add(node.Id, node);
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
                    Invoke((MethodInvoker)SelectionChanged);
                else
                    SelectionChanged();
            }
            catch (Exception ex)
            {
                Debug.Print(string.Join("\n", ex.GetType().ToString(), ex.Message, ex.StackTrace));
            }
        }

        private void SelectionChanged()
        {
            if (!EventsActive)
                return;

            WindowSelectionData wsd = new WindowSelectionData();

            foreach (ListViewItem li in lvwNodeList.SelectedItems)
            {
                IRepositoryNode ow = (IRepositoryNode)li.Tag;
                wsd.Selection.Add(ow.Id, ow);
            }

            ListSelectionChanged?.Invoke(wsd);
        }

        private void lvwNodeList_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (_useTimer)
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
            if (!_enableCheck)
                e.NewValue = e.CurrentValue;
        }
    }
}