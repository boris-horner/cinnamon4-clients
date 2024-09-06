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
using CDCplusLib.Messages;
using CDCplusLib.Messages.SessionWindowRequestData;
using System.Xml;
using C4ObjectApi.Interfaces;
using C4ObjectApi.Repository;
using C4GeneralGui.GuiElements;

namespace CDCplusLib.Common.GUI
{
    public partial class MainWindow : Form, ISessionWindow
    {
        private CmnSession _s;
        private string _guid;
        private GlobalApplicationData _gad;
        private bool _expandEventOff;
        private ContextFunctionsContainer _contextFunctions;
        private string _lockedQuery;
        private string _tasksQuery;
        private QuickSearch _quickSearch;
        private bool _treeSelectEventsActive;
        //private SessionTree stSession;


        public string Guid
        {
            get
            {
                if (_guid == null) _guid = System.Guid.NewGuid().ToString();
                return _guid;
            }
        }

        public event ISessionWindow.WindowClosedEventHandler WindowClosed;
        public event ISessionWindow.MessageSentEventHandler MessageSent;
        public event ISessionWindow.PathChangedEventHandler PathChanged;

        public MainWindow()
        {
            // This call is required by the designer.
            InitializeComponent();
            string im16Path = Path.Combine(Application.StartupPath, "Images", "Icons", "16x16");
            //tsbQuickSearch.Image = new Bitmap(Path.Combine(im16Path, "edit-find.png"));
            //tsslUser.Image = new Bitmap(Path.Combine(im16Path, "im-user.png"));
            //tsslServer.Image = new Bitmap(Path.Combine(im16Path, "server-database.png"));
            //tsslConfiguration.Image = new Bitmap(Path.Combine(im16Path, "applications-system.png"));
            //tsslStatus.Image = new Bitmap(Path.Combine(im16Path, "dialog-information.png"));
        }
        public void CloseWindow()
        {
            Close();
        }

        public void ShowSessionWindow(CmnSession s, GlobalApplicationData gad, SessionWindowRequestMessage msg = null)
        {
            _treeSelectEventsActive = false;
            _s = s;
            _s.ReloadSettings();
            _gad = gad;
            _expandEventOff = false;
            InitQueries();
            //LocalizeGUI();
            //InitTreeView();
            _contextFunctions = new ContextFunctionsContainer(_s, _gad, this);
            _contextFunctions.MessageSent += MessageSentHandler;
            //InitFunctions();
            _quickSearch = new QuickSearch();
            _quickSearch.Init(_s);
            _quickSearch.MessageSent += MessageSentHandler;
            InitToolbar();
            stSession.InitTreeView(_s);
            stSession.MessageSent += MessageSentHandler;

            ctccTreeContext.Init(_s, false, _gad);
            ctccListContext.Init(_s, true, _gad);
            ctccTreeContext.MessageSent += MessageSentHandler;
            ctccListContext.MessageSent += MessageSentHandler;

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
            if (msg != null)
            {
                System.Diagnostics.Debug.Print(msg.SessionWindowRequestData.GetType().Name);
                switch (msg.SessionWindowRequestData.GetType().Name)
                {
                    case "BrowserSessionWindowRequestData":
                        {
                            BrowserSessionWindowRequestData bswrData = (BrowserSessionWindowRequestData)msg.SessionWindowRequestData;
                            stSession.SetSelection(bswrData.Folder); // TODO: pass selection

                            break;
                        }

                    case "ResultListSessionWindowRequestData":
                        {
                            ResultListSessionWindowRequestData rlswrData = (ResultListSessionWindowRequestData)msg.SessionWindowRequestData;
                            stSession.SetSelection(rlswrData.ResultList); // TODO: pass selection
                            break;
                        }
                }
            }

            RefreshTree(stSession.SelectedNode, msg);
            // msg handling perhaps better here?

            _treeSelectEventsActive = true;
            PathChanged?.Invoke(Text, this);
            ActivateControls();
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
                else if (stSession.SelectedNode.Tag is Dictionary<long, IRepositoryNode>)
                {
                    Text = Properties.Resources.lblCDCplusMainWindow + " - " + stSession.SelectedNode.Name;
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





        protected virtual void MessageSentHandler(IClientMessage msg)
        {
            System.Diagnostics.Debug.Print(msg.GetType().Name);
            switch (msg.GetType().Name)
            {
                case "SelectionRightClickedMessage":
                    {
                        SelectionRightClickedMessage srcMsg = (SelectionRightClickedMessage)msg;
                        _contextFunctions.ShowContextMenu(srcMsg.ListSelection, srcMsg.ReferenceControl, srcMsg.Pos);
                        break;
                    }

                case "SessionWindowRequestMessage":
                    {
                        //((SessionWindowRequestMessage)msg).WindowType = "CDCplusLegacyLib.MainWindow";
                        MessageSent?.Invoke(msg); // forward to dispatcher
                        break;
                    }
                case "ListSelectionChangeMessage":
                    {
                        ctccListContext.CheckForUnsavedChanges();
                        ListSelectionChangeMessage lscMsg = (ListSelectionChangeMessage)msg;
                        IGenericControl.ContextType ct;
                        if (lscMsg.ListSelection.Count == 1)
                        {
                            if (lscMsg.ListSelection.Values.First() is CmnObject) ct = IGenericControl.ContextType.Object;
                            else ct = IGenericControl.ContextType.Folder;
                        }
                        else
                        {
                            ct = IGenericControl.ContextType.List;
                        }
                        ctccListContext.UpdateTabControl(lscMsg.ListSelection, ct, msg);

                        break;
                    }
                case "ObjectsModifiedMessage":
                case "ObjectVersionedMessage":
                case "ObjectsDeletedMessage":
                case "ObjectsMovedMessage":
                case "ObjectsCreatedMessage":
                case "SetTreeContextSelectionMessage":
                case "TreeNodeRefreshRequestMessage":
                    {
                        RefreshTree(stSession.SelectedNode, msg);
                        SetTitle();
                        PathChanged?.Invoke(Text, this);
                        break;
                    }
                case "ObjectDoubleClickedMessage":
                    {
                        ObjectDoubleClickedMessage odcm = (ObjectDoubleClickedMessage)msg;
                        Dictionary<long, IRepositoryNode> dict = new Dictionary<long, IRepositoryNode>();
                        dict.Add(odcm.SelectedObject.Id, odcm.SelectedObject);
                        if (_contextFunctions.DefaultObjectFunction.IsValid(dict))
                        {
                            _contextFunctions.DefaultObjectFunction.Execute(dict);
                        }
                        break;
                    }
                case "WindowNotifyUserMessage":
                    {
                        WindowNotifyUserMessage wnum = (WindowNotifyUserMessage)msg;
                        tsslStatus.Text = wnum.Message;
                        tsslStatus.BackColor = Color.Gold;
                        SetTitle();
                        break;
                    }
                case "FolderChangeMessage":
                    {
                        FolderChangeMessage fcm = (FolderChangeMessage)msg;
                        stSession.SetSelection(fcm.ChangeToFolder);
                        RefreshTree(stSession.SelectedNode, msg);
                        SetTitle();
                        PathChanged?.Invoke(Text, this);
                        break;
                    }
                case "KeyPressedMessage":
                    {
                        KeyPressedMessage kpMsg = (KeyPressedMessage)msg;
                        string k = kpMsg.Assembly + "." + kpMsg.Type;
                        if (kpMsg.FunctionType == KeyPressedMessage.FunctionTypes.SessionFunction) _contextFunctions.Functions[k].Execute(null);
                        else _contextFunctions.Functions[k].Execute(kpMsg.ListSelection);
                        break;
                    }
            }
        }
        private void RefreshTree(TreeNode selection, IClientMessage msg)
        {
            if (selection == null) ctccTreeContext.UpdateTabControl(null, IGenericControl.ContextType.Session, msg);
            else switch (selection.Name)
                {
                    case SessionTree.NODE_SESSION:
                        {
                            ctccTreeContext.UpdateTabControl(null, IGenericControl.ContextType.Session, msg);
                        }
                        break;
                    case SessionTree.NODE_HOME:
                        {
                            Dictionary<long, IRepositoryNode> dict = new Dictionary<long, IRepositoryNode>();
                            CmnFolder f = (CmnFolder)selection.Tag;
                            //stSession.SetSelection(f);
                            dict.Add(f.Id, f);
                            ctccTreeContext.UpdateTabControl(dict, IGenericControl.ContextType.Folder, msg);
                        }
                        break;
                    case SessionTree.NODE_SEARCHES:
                        {
                            Dictionary<long, IRepositoryNode> dict = new Dictionary<long, IRepositoryNode>();
                            CmnFolder f = (CmnFolder)selection.Tag;
                            stSession.SetSelection(f);
                            dict.Add(f.Id, f);
                            ctccTreeContext.UpdateTabControl(dict, IGenericControl.ContextType.Folder, msg);
                        }
                        break;
                    case SessionTree.NODE_LOCKED:
                        {
                            selection.Tag = _s.SearchObjects(_lockedQuery);
                            ctccTreeContext.UpdateTabControl(ListsHelper.ConvertToOwnables((Dictionary<long, CmnObject>)selection.Tag, null), IGenericControl.ContextType.List, msg);
                        }
                        break;
                    case SessionTree.NODE_TASKS:
                        {
                            selection.Tag = _s.SearchObjects(_tasksQuery);
                            ctccTreeContext.UpdateTabControl(ListsHelper.ConvertToOwnables((Dictionary<long, CmnObject>)selection.Tag, null), IGenericControl.ContextType.List, msg);
                        }
                        break;
                    case SessionTree.NODE_RESULTS:
                        {
                            ctccTreeContext.UpdateTabControl((Dictionary<long, IRepositoryNode>)selection.Tag, IGenericControl.ContextType.List, msg);
                        }
                        break;
                    default:
                        {
                            if (selection.Tag.GetType() == typeof(CmnFolder))
                            {
                                // folder inside one of the folder controls
                                Dictionary<long, IRepositoryNode> dict = new Dictionary<long, IRepositoryNode>();
                                CmnFolder f = (CmnFolder)selection.Tag;
                                dict.Add(f.Id, f);
                                ctccTreeContext.UpdateTabControl(dict, IGenericControl.ContextType.Folder, msg);
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

        private void stSession_SelectionChanged(TreeNode selection, IClientMessage msg)
        {
            if (_treeSelectEventsActive) RefreshTree(selection, msg);
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
