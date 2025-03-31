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
using CDCplusLib.DataModel;
using CDCplusLib.Helpers;
using CDCplusLib.Interfaces;
using System.Xml;
using C4ObjectApi.Interfaces;
using C4ObjectApi.Repository;
using C4GeneralGui.GuiElements;
using CDCplusLib.EventData;
using System.ServiceModel.Channels;
using static System.ComponentModel.Design.ObjectSelectorEditor;
using static CDCplusLib.Common.GUI.SessionTree;

namespace CDCplusLib.Common.GUI
{
    public partial class MainWindow : Form, ISessionWindow
    {
        private CmnSession _s;
        private string _guid;
        private GlobalApplicationData _gad;
        private ContextFunctionsContainer _contextFunctions;
        private string _lockedQuery;
        private string _tasksQuery;
        private QuickSearch _quickSearch;
        //private bool _treeSelectEventsActive;
        //private SessionTree stSession;
        public bool EventsActive { get; set; }

        public string Guid
        {
            get
            {
                if (_guid == null) _guid = System.Guid.NewGuid().ToString();
                return _guid;
            }
        }

        public string WindowTitle
        {
            get
            {
                return Text;
            }
        }

        public event WindowClosedEventHandler WindowClosed;
        public event SessionWindowRequestEventHandler SessionWindowRequest;
        public event TreeSelectionChangedEventHandler TreeSelectionChanged;
        public event ContextMenuRequestEventHandler ContextMenuRequest;
        public event ListSelectionChangedEventHandler ListSelectionChanged;
        public event FunctionRequestEventHandler FunctionRequest;
        public event NodesModifiedEventHandler NodesModified;
        public event KeyPressedEventHandler KeyPressedEvent;
        public event RefreshRequestEventHandler RefreshRequest;

        public MainWindow()
        {
            InitializeComponent();
            //string im16Path = Path.Combine(Application.StartupPath, "Images", "Icons", "16x16");
        }
        public void CloseWindow()
        {
            Close();
        }

        public void ShowSessionWindow(CmnSession s, GlobalApplicationData gad, WindowSelectionData wsd = null)
        {
            EventsActive = false;
            if (wsd == null) throw new ApplicationException("Main window can only be opened with WindowSelectionData.");

            //_treeSelectEventsActive = false;
            _s = s;
            _s.ReloadSettings();
            _gad = gad;
            EventsActive = true;
            InitQueries();
            //LocalizeGUI();
            //InitTreeView();
            _contextFunctions = new ContextFunctionsContainer(_s, _gad, this);
            _contextFunctions.SessionWindowRequest += SessionWindowRequestEventHandler;
            _contextFunctions.NodesModified += NodesModifiedEventHandler;
            //InitFunctions();
            _quickSearch = new QuickSearch();
            _quickSearch.Init(_s);
            _quickSearch.SessionWindowRequest += SessionWindowRequestEventHandler;
            InitToolbar();
            stSession.InitTreeView(_s);
            stSession.SessionWindowRequest += SessionWindowRequestEventHandler;
            stSession.TreeSelectionChanged += TreeSelectionChangedEventHandler;
            stSession.ContextMenuRequest += ContextMenuRequestEventHandler;
            stSession.FunctionRequest += FunctionRequestEventHandler;
            stSession.NodesModified += NodesModifiedEventHandler;
            stSession.KeyPressedEvent += KeyPressedEventHandler;

            ctccTreeContext.Init(_s, false, _gad);
            ctccListContext.Init(_s, true, _gad);
            ctccTreeContext.SessionWindowRequest += SessionWindowRequestEventHandler;
            ctccListContext.SessionWindowRequest += SessionWindowRequestEventHandler;

            ctccTreeContext.ListSelectionChanged += ListSelectionChangedEventHandler;
            ctccListContext.ListSelectionChanged += ListSelectionChangedEventHandler;

            ctccTreeContext.TreeSelectionChanged += TreeSelectionChangedEventHandler;
            ctccListContext.TreeSelectionChanged += TreeSelectionChangedEventHandler;

            ctccTreeContext.ContextMenuRequest += ContextMenuRequestEventHandler;
            ctccListContext.ContextMenuRequest += ContextMenuRequestEventHandler;

            ctccTreeContext.FunctionRequest += FunctionRequestEventHandler;
            ctccListContext.FunctionRequest += FunctionRequestEventHandler;

            ctccTreeContext.NodesModified += NodesModifiedEventHandler;
            ctccListContext.NodesModified += NodesModifiedEventHandler;

            Width = (int)Math.Round(Screen.PrimaryScreen.Bounds.Width * 0.8d);
            Height = (int)Math.Round(Screen.PrimaryScreen.Bounds.Height * 0.8d);
            
            if (_s.User.GroupIds.Contains((long)_s.SessionConfig.C4Sc.GroupsByName["_superusers"].Id))
            {
                tsslUser.Text = _s.User.ToString() + " [Superuser]";
                tsslUser.BackColor = Color.LightSkyBlue;
            }
            else
            {
                tsslUser.Text = _s.User.ToString();
                tsslUser.BackColor = SystemColors.Control;
            }

            switch (_s.UserConfigType)
            {
                case CmnSession.UserConfigTypes.Standard:
                    {
                        tsslConfiguration.Text = Properties.Resources.lblStandardConfig;
                        tsslConfiguration.BackColor = SystemColors.Control;
                        break;
                    }

                case CmnSession.UserConfigTypes.Named:
                    {
                        tsslConfiguration.Text = string.Format(Properties.Resources.lblNamedConfig, _s.Server.NamedUserConfig);
                        tsslConfiguration.BackColor = Color.YellowGreen;
                        break;
                    }

                case CmnSession.UserConfigTypes.Basic:
                    {
                        tsslConfiguration.Text = Properties.Resources.lblEmergencyConfig;
                        tsslConfiguration.BackColor = Color.LightSalmon;
                        break;
                    }
            }

            tsslServer.Text = _s.Server.Name;
            tsslStatus.Text = "";
            tsslStatus.BackColor = SystemColors.Control;
            Show();

            stSession.EventsActive = true;
            stSession.SetSelection(wsd);

            ctccListContext.EventsActive = true;
            if (wsd.RootNodeType == SessionTree.RootNodeTypes.Session) UpdateTcTabControl(stSession.tvwSession.Nodes.Find(SessionTree.NODE_SESSION, true)[0], wsd);
            else if (wsd.RootNodeType == RootNodeTypes.Results) UpdateTcTabControl(stSession.tvwSession.Nodes.Find(SessionTree.NODE_RESULTS, true)[0], wsd);
            else if(wsd.SelectedFolder!=null) UpdateTcTabControl(stSession.tvwSession.Nodes.Find(wsd.SelectedFolder.Id.ToString(), true)[0], wsd);
            ctccTreeContext.EventsActive = true;

            ActivateControls();
        }
        protected virtual void SessionWindowRequestEventHandler(WindowSelectionData wsd)
        {
            SessionWindowRequest?.Invoke(wsd);
        }
        protected virtual void TreeSelectionChangedEventHandler(WindowSelectionData wsd, ISessionWindow sw)
        {
            //UpdateTcTabControl(stSession.SelectedNode, wsd);
            TreeNode rootNode = stSession.SelectedNode;
            while (rootNode.Parent != null)
            {
                rootNode = rootNode.Parent;
            }
            stSession.OpenAndSelectPath(rootNode, wsd.SelectedFolder);
            //stSession.SetSelection(wsd);
            //TreeNode tn = stSession.tvwSession.Nodes.Find(wsd.SelectedFolder.Id.ToString(), true)[0];
            //stSession.SelectedNode = tn;
            //UpdateTcTabControl(tn, wsd);
            SetTitle();
            TreeSelectionChanged?.Invoke(wsd, this);
        }
        protected virtual void ListSelectionChangedEventHandler(WindowSelectionData wsd)
        {
            ctccListContext.CheckForUnsavedChanges();
            IGenericControl.ContextType ct;
            if (wsd.Selection.Count == 1)
            {
                if (wsd.Selection.Values.First() is CmnObject) ct = IGenericControl.ContextType.Object;
                else ct = IGenericControl.ContextType.Folder;
            }
            else
            {
                ct = IGenericControl.ContextType.List;
            }
            ctccListContext.UpdateTabControl(wsd.Selection, ct, wsd);
        }
        protected virtual void ContextMenuRequestEventHandler(WindowSelectionData wsd, Point position)
        {
            _contextFunctions.ShowContextMenu(wsd.Selection, this, PointToClient(position));
        }
        protected virtual void FunctionRequestEventHandler(WindowSelectionData wsd, string assembly, string type)
        {
            string k = assembly + "." + type;
            IGenericFunction contextFunction = k == "." ? _contextFunctions.DefaultObjectFunction: _contextFunctions.Functions[k];
            if (wsd.Selection.Count==0) contextFunction.Execute(null);
            else contextFunction.Execute(wsd.Selection);
        }
        protected virtual void NodesModifiedEventHandler(WindowSelectionData wsd)
        {
            // TODO: update parents folders of all objects in modification
            SortedDictionary<string, CmnFolder> parents = new SortedDictionary<string, CmnFolder>();
            foreach(IRepositoryNode rn in wsd.Modification.Values)
            {
                if(rn.Parent!=null)
                {
                    if (!parents.ContainsKey(rn.Parent.FolderPath))
                    {
                        parents.Add(rn.Parent.FolderPath, rn.Parent);
                        foreach(TreeNode tn in stSession.tvwSession.Nodes.Find(rn.Parent.Id.ToString(), true))
                        {
                            if(tn.Parent!=null && tn.Parent.IsExpanded) stSession.UpdateFolderNode(tn);
                        }
                    }
                }
            }
            UpdateTcTabControl(stSession.SelectedNode, wsd);
            SetTitle();
            //TreeSelectionChanged?.Invoke(wsd, this);
        }
        protected virtual void KeyPressedEventHandler(WindowSelectionData wsd, Keys key, bool shift, bool ctrl, bool alt)
        {
            throw new NotImplementedException();
        }
        private void ActivateControls()
        {
            tsbQuickSearch.Enabled = txtQuickSearch.Text is object && txtQuickSearch.Text.Length > 0;
        }
        public void WindowTop()
        {
            if (WindowState == FormWindowState.Minimized) WindowState = FormWindowState.Normal;
            BringToFront();
        }
        protected void HandleToolstripButtonClick(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                ToolStripItem b = (ToolStripButton)sender;
                IGenericFunction sf = (IGenericFunction)b.Tag;
                sf.Execute(null);
            }
            catch (ApplicationException ex)
            {
                StandardMessage.ShowMessage("Caught ApplicationException", StandardMessage.Severity.ErrorMessage, this, ex);
            }
            catch (Exception ex)
            {
                StandardMessage.ShowMessage("Caught Exception", StandardMessage.Severity.ErrorMessage, this, ex);
            }
            finally
            {
                Cursor = Cursors.Arrow;
            }
        }
        private void InitQueries()
        {
            _lockedQuery = "<ExactPointQuery type=\"long\" fieldName=\"locker\" value=\"" + _s.User.Id.ToString() + "\"/>";

            _tasksQuery = string.Concat("<BooleanQuery><Clause occurs=\"must\"><ExactPointQuery type=\"long\" fieldName=\"owner\" value=\"", 
                                              _s.User.Id.ToString(), 
                                              "\"/></Clause><Clause occurs=\"must\"><BooleanQuery minimumNumberShouldMatch=\"1\"><Clause occurs=\"should\"><BooleanQuery><Clause occurs=\"must\"><TermQuery fieldName=\"index.wftask_status\">active</TermQuery></Clause>",
                                              "<Clause occurs=\"must\"><ExactPointQuery type=\"long\" fieldName=\"object_type\" value=\"", 
                                              ((long)_s.SessionConfig.C4Sc.ObjectTypesByName["_task"].Id).ToString(), 
                                              "\"/></Clause></BooleanQuery></Clause><Clause occurs=\"should\"><ExactPointQuery type=\"long\" fieldName=\"object_type\" value=\"", 
                                              ((long)_s.SessionConfig.C4Sc.ObjectTypesByName["_notification"].Id).ToString(), 
                                              "\"/></Clause></BooleanQuery></Clause></BooleanQuery>");
        }


        private void SetTitle()
        {
            if (stSession.SelectedNode is object)
            {
                if (stSession.SelectedNode.Tag is CmnFolder)
                {
                    Text = Properties.Resources.lblCDCplusMainWindow + " - " + ((CmnFolder)stSession.SelectedNode.Tag).FolderPath;
                }
                else if (stSession.SelectedNode.Tag is CmnObject)
                {
                    Text = Properties.Resources.lblCDCplusMainWindow; // TODO: find something better if this happens
                }
                else if (stSession.SelectedNode.Tag is CmnSession)
                {
                    Text = Properties.Resources.lblCDCplusMainWindow + " - /";
                }
                else if (stSession.SelectedNode.Tag is Dictionary<long, CmnObject>)
                {
                    Text = Properties.Resources.lblCDCplusMainWindow + " - " + stSession.SelectedNode.Text;
                }
                else if (stSession.SelectedNode.Tag is Dictionary<long, IRepositoryNode>)
                {
                    Text = Properties.Resources.lblCDCplusMainWindow + " - " + stSession.SelectedNode.Text;
                }
            }
            else
            {
                Text = Properties.Resources.lblCDCplusMainWindow;
            }
        }


        protected virtual void InitToolbar()
        {
            foreach (XmlElement el in _s.UserConfig.DocumentElement.SelectNodes("menu/toolbar/*"))
            {
                switch (el.Name ?? "")
                {
                    case "button":
                        {
                            string k = el.GetAttribute("assembly") + "." + el.GetAttribute("type");
                            if(_contextFunctions.Functions.ContainsKey(k))
                            {
                                IGenericFunction sf = _contextFunctions.Functions[k];
                                ToolStripButton b = null;
                                Image icon = sf.GetIcon();
                                if (icon is null)
                                {
                                    b = new ToolStripButton(sf.GetMenuText());
                                }
                                else
                                {
                                    b = new ToolStripButton(icon);
                                }

                                b.ToolTipText = sf.GetMenuText();
                                b.Tag = sf;
                                b.Click += HandleToolstripButtonClick;
                                tstrRepositoryNav.Items.Add(b);
                            }
                            break;
                        }

                    case "separator":
                        {
                            if (!(tstrRepositoryNav.Items[tstrRepositoryNav.Items.Count - 1].GetType() == typeof(ToolStripSeparator))) tstrRepositoryNav.Items.Add(new ToolStripSeparator());
                            break;
                        }
                }
            }
        }





        private void UpdateTcTabControl(TreeNode selectedTreeNode, WindowSelectionData wsd)
        {
            if (selectedTreeNode == null) ctccTreeContext.UpdateTabControl(null, IGenericControl.ContextType.Session, wsd);
            else
            {

                switch (selectedTreeNode.Name)
                {
                    case SessionTree.NODE_SESSION:
                        {
                            ctccTreeContext.UpdateTabControl(null, IGenericControl.ContextType.Session, wsd);
                        }
                        break;
                    case SessionTree.NODE_HOME:
                        {
                            Dictionary<long, IRepositoryNode> dict = new Dictionary<long, IRepositoryNode>();
                            CmnFolder f = (CmnFolder)selectedTreeNode.Tag;
                            //if (selection.Parent == null) stSession.UpdateFolderNode(selection);
                            //else stSession.UpdateFolderNode(selection.Parent);
                            dict.Add(f.Id, f);
                            ctccTreeContext.UpdateTabControl(dict, IGenericControl.ContextType.Folder, wsd);
                        }
                        break;
                    case SessionTree.NODE_SEARCHES:
                        {
                            Dictionary<long, IRepositoryNode> dict = new Dictionary<long, IRepositoryNode>();
                            CmnFolder f = (CmnFolder)selectedTreeNode.Tag;
                            //if (selection.Parent == null) stSession.UpdateFolderNode(selection);
                            //else stSession.UpdateFolderNode(selection.Parent);
                            dict.Add(f.Id, f);
                            ctccTreeContext.UpdateTabControl(dict, IGenericControl.ContextType.Folder, wsd);
                        }
                        break;
                    case SessionTree.NODE_LOCKED:
                        {
                            selectedTreeNode.Tag = _s.SearchObjects(_lockedQuery);
                            ctccTreeContext.UpdateTabControl(ListsHelper.ConvertToOwnables((Dictionary<long, CmnObject>)selectedTreeNode.Tag, null), IGenericControl.ContextType.List, wsd);
                        }
                        break;
                    case SessionTree.NODE_TASKS:
                        {
                            selectedTreeNode.Tag = _s.SearchObjects(_tasksQuery);
                            ctccTreeContext.UpdateTabControl(ListsHelper.ConvertToOwnables((Dictionary<long, CmnObject>)selectedTreeNode.Tag, null), IGenericControl.ContextType.List, wsd);
                        }
                        break;
                    case SessionTree.NODE_RESULTS:
                        {
                            ctccTreeContext.UpdateTabControl(wsd.Selection, IGenericControl.ContextType.List, wsd);
                        }
                        break;
                    default:
                        {
                            if (selectedTreeNode.Tag.GetType() == typeof(CmnFolder))
                            {
                                // folder inside one of the folder controls
                                Dictionary<long, IRepositoryNode> dict = new Dictionary<long, IRepositoryNode>();
                                CmnFolder f = (CmnFolder)selectedTreeNode.Tag;
                                //if (selection.Parent == null) stSession.UpdateFolderNode(selection);
                                //else stSession.UpdateFolderNode(selection.Parent);
                                dict.Add(f.Id, f);
                                ctccTreeContext.UpdateTabControl(dict, IGenericControl.ContextType.Folder, wsd);
                            }
                            else
                            {
                                // shouldn't happen
                                // Debug.Print("?");
                            }
                        }
                        break;
                }
            }

        }

        private void stSession_SelectionChanged(WindowSelectionData wsd, ISessionWindow sw)
        {
            UpdateTcTabControl(stSession.SelectedNode, wsd);
            SetTitle();
        }

        private void MainWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            ctccListContext.CheckForUnsavedChanges();
            ctccTreeContext.CheckForUnsavedChanges();
            WindowClosed?.Invoke(this);
        }

        private void txtQuickSearch_TextChanged(object sender, EventArgs e)
        {
            ActivateControls();
        }

        private void tsbQuickSearch_Click(object sender, EventArgs e)
        {
            tsbQuickSearch.Enabled = false;
            txtQuickSearch.Enabled = false;
            Cursor = Cursors.WaitCursor;
            _quickSearch.Search(txtQuickSearch.Text);
            tsbQuickSearch.Enabled = true;
            txtQuickSearch.Enabled = true;
            Cursor = Cursors.Default;
        }

        private void txtQuickSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (tsbQuickSearch.Enabled)
            {
                if (e.KeyChar == 13)
                {
                    tsbQuickSearch.Enabled = false;
                    txtQuickSearch.Enabled = false;
                    Cursor = Cursors.WaitCursor;
                    _quickSearch.Search(txtQuickSearch.Text);
                    tsbQuickSearch.Enabled = true;
                    txtQuickSearch.Enabled = true;
                    Cursor = Cursors.Default;
                    e.Handled = true;
                }
            }
        }

        private void tsbShowBoth_Click(object sender, EventArgs e)
        {
            splHor.Panel1Collapsed = false;
            splHor.Panel2Collapsed = false;
            ActivateControls();
        }

        private void tsbShowTop_Click(object sender, EventArgs e)
        {
            splHor.Panel1Collapsed = false;
            splHor.Panel2Collapsed = true;
            ActivateControls();
        }

        private void tsbShowBottom_Click(object sender, EventArgs e)
        {
            splHor.Panel1Collapsed = true;
            splHor.Panel2Collapsed = false;
            ActivateControls();
        }

        private void tsbShowRightOnly_Click(object sender, EventArgs e)
        {
            splVert.Panel1Collapsed = true;
            splVert.Panel2Collapsed = false;
            ActivateControls();
        }

        private void tsbShowTree_Click(object sender, EventArgs e)
        {
            splVert.Panel1Collapsed = false;
            splVert.Panel2Collapsed = false;
            ActivateControls();
        }

    }
}
