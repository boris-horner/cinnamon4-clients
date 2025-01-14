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
using CDCplusLib.Common.KeyboardEvents;
using CDCplusLib.Interfaces;
using CDCplusLib.Messages;
using System.Collections;
using System.Xml;
using C4ObjectApi.Interfaces;
using C4ObjectApi.Repository;
using C4GeneralGui.GuiElements;

namespace CDCplusLib.Common.GUI
{
    public partial class SessionTree : UserControl
    {
        public const string NODE_SESSION = "00_session";
        public const string NODE_HOME = "01_home";
        public const string NODE_SEARCHES = "02_searches";
        public const string NODE_LOCKED = "03_locked";
        public const string NODE_TASKS = "04_tasks";
        public const string NODE_RESULTS = "05_results";
        public const string DUMMY_FOLDER = "";  // assign empty string, otherwise it shows the name defined here when building the folder view
        private CmnSession _s;
        private XmlElement _configEl;
        private bool _selectEventActive;
        private KeyEventTable _keyEvents;

        public event ISessionWindow.MessageSentEventHandler MessageSent;
        public event SelectionChangedEventHandler SelectionChanged;
        public delegate void SelectionChangedEventHandler(TreeNode selection, IClientMessage msg);
        public TreeNode SelectedNode
        {
            get { return tvwSession.SelectedNode; }
            set { tvwSession.SelectedNode = value; }
        }
        public SessionTree()
        {
            // image list is filled by code because serializing the image list in designer wouldn't work without further changes
            InitializeComponent();
            _selectEventActive = true;
        }
        public void InitTreeView(CmnSession s)
        {
            tvwSession.SuspendLayout();
            tvwSession.BeginUpdate();
            _s = s;
            _configEl = (XmlElement)_s.UserConfig.DocumentElement.SelectSingleNode("classes/session_tree");
            _keyEvents = new KeyEventTable((XmlElement)_configEl.SelectSingleNode("key_events"));
            _keyEvents.MessageSent += KeyMessageSentHandler;

            tvwSession.ImageList = new ImageList();
            tvwSession.ImageList.ColorDepth=ColorDepth.Depth24Bit;
            tvwSession.ImageList.TransparentColor = Color.Transparent;
            string imagePath = Path.Combine(Application.StartupPath, "Images", "Icons", "16x16");
            tvwSession.ImageList.Images.Add("server-database", new Bitmap(Path.Combine(imagePath, "server-database.png")));
            tvwSession.ImageList.Images.Add("go-home", new Bitmap(Path.Combine(imagePath, "go-home.png")));
            tvwSession.ImageList.Images.Add("edit-find", new Bitmap(Path.Combine(imagePath, "edit-find.png")));
            tvwSession.ImageList.Images.Add("application-x-kgetlist", new Bitmap(Path.Combine(imagePath, "application-x-kgetlist.png")));
            tvwSession.ImageList.Images.Add("format-list-unordered", new Bitmap(Path.Combine(imagePath, "format-list-unordered.png")));
            tvwSession.ImageList.Images.Add("mail-folder-inbox", new Bitmap(Path.Combine(imagePath, "mail-folder-inbox.png")));
            tvwSession.ImageList.Images.Add("folder", new Bitmap(Path.Combine(imagePath, "folder.png")));

            tvwSession.TreeViewNodeSorter = new TreeNodeSorter();

            TreeNode tn = tvwSession.Nodes.Add(NODE_SESSION, Properties.Resources.lblTreeContent, "server-database", "server-database");
            tn.Tag = _s;
            InitFolderRootNode(tn);

            if (_s.SessionConfig.HomeFolder != null)
            {
                tn = tvwSession.Nodes.Add(NODE_HOME, Properties.Resources.lblTreeHome, "go-home", "go-home");
                tn.Tag = _s.SessionConfig.HomeFolder;
                InitFolderRootNode(tn);
            }

            if (_s.SessionConfig.SearchesFolder != null)
            {
                tn = tvwSession.Nodes.Add(NODE_SEARCHES, Properties.Resources.lblTreeSearches, "edit-find", "edit-find");
                tn.Tag = _s.SessionConfig.SearchesFolder;
                InitFolderRootNode(tn);
            }

            tn = tvwSession.Nodes.Add(NODE_LOCKED, Properties.Resources.lblTreeLocked, "application-x-kgetlist", "application-x-kgetlist");
            tn.Tag = new Dictionary<long, IRepositoryNode>();

            tn = tvwSession.Nodes.Add(NODE_TASKS, Properties.Resources.lblTreeTasks, "mail-folder-inbox", "mail-folder-inbox");
            tn.Tag = new Dictionary<long, IRepositoryNode>();

            tn = tvwSession.Nodes.Add(NODE_RESULTS, Properties.Resources.lblTreeResults, "format-list-unordered", "format-list-unordered");
            tn.Tag = new Dictionary<long, IRepositoryNode>();

            tvwSession.Sort();
            tvwSession.EndUpdate();
            tvwSession.ResumeLayout(true);
        }
        private void InitFolderRootNode(TreeNode n)
        {
            if (n.Tag.GetType() == typeof(CmnSession))
            {
                UpdateFolderNode(n);
            }
            else if (n.Tag.GetType() == typeof(CmnFolder))
            {
                UpdateFolderNode(n);
            }
            else
            {
                throw new NotImplementedException(string.Concat("Type: ", n.Tag.GetType().ToString(), " is not supported in tree selection."));
            }
        }
        protected virtual void KeyMessageSentHandler(IClientMessage msg)
        {
            MessageSent?.Invoke(msg);
        }

        private void UpdateFolderNode(TreeNode n)
        {
            CmnFolder f = null;
            if (n.Tag.GetType() == typeof(CmnFolder))
            {
                f = (CmnFolder)n.Tag;
            }
            else if (n.Tag.GetType() == typeof(CmnSession))
            {
                f = ((CmnSession)n.Tag).RootFolder;
            }
            if (f != null)
            {
                if (n.IsExpanded)
                {
                    Dictionary<long, CmnFolder> subfolders = f.GetSubfolders();
                    string selectedNodeKey = null;
                    if (tvwSession.SelectedNode != null && tvwSession.SelectedNode.Parent == n) selectedNodeKey = tvwSession.SelectedNode.Name;
                    n.Nodes.Clear();
                    foreach (long id in subfolders.Keys)
                    {
                        CmnFolder addF = subfolders[id];
                        TreeNode tn = n.Nodes.Add(id.ToString(), addF.Name, "folder", "folder");
                        tn.Tag = addF;
                        //UpdateFolderNode(tn);
                        if (tn.IsExpanded) UpdateFolderNode(tn);
                        else if (addF.HasSubfolders)
                        {
                            // add dummy folder to let treeview show a [+] next to the folder
                            tn.Nodes.Add(DUMMY_FOLDER, DUMMY_FOLDER, "folder", "folder");
                        }
                    }
                    if (selectedNodeKey != null && n.Nodes.ContainsKey(selectedNodeKey)) tvwSession.SelectedNode = n.Nodes[selectedNodeKey];
                }
                else
                {
                    if (f.Parent != null)
                    {
                        f.Refresh();
                        n.Nodes.Clear();
                        if (f.HasSubfolders)
                        {
                            // add dummy folder to let treeview show a [+] next to the folder
                            TreeNode tn = n.Nodes.Add(DUMMY_FOLDER, DUMMY_FOLDER, "folder", "folder");
                        }
                    }
                    else
                    {
                        if (n.Nodes.Count == 0)
                        {
                            // add dummy folder to let treeview show a [+] next to the folder
                            TreeNode tn = n.Nodes.Add(DUMMY_FOLDER, DUMMY_FOLDER, "folder", "folder");
                        }
                    }

                }
            }
        }

        public void SetSelection(Dictionary<long, IRepositoryNode> dict)
        {
            tvwSession.SuspendLayout();
            tvwSession.BeginUpdate();
            tvwSession.Nodes[NODE_RESULTS].Tag = dict;
            tvwSession.SelectedNode = tvwSession.Nodes[NODE_RESULTS];
            tvwSession.EndUpdate();
            tvwSession.ResumeLayout(true);
        }
        public void SetSelection(CmnFolder f)
        {
            tvwSession.SuspendLayout();
            tvwSession.BeginUpdate();
            if (f == null)
            {
                // nothing to do
            }
            else
            {
                if (f.FolderPath.StartsWith(_s.SessionConfig.HomeFolder.FolderPath))
                {
                    OpenAndSelectPath(tvwSession.Nodes[NODE_HOME], f);
                }
                else if (f.FolderPath.StartsWith(_s.SessionConfig.SearchesFolder.FolderPath))
                {
                    OpenAndSelectPath(tvwSession.Nodes[NODE_SEARCHES], f);
                }
                else
                {
                    // folder not under one of the special folders - open in session structure
                    OpenAndSelectPath(tvwSession.Nodes[NODE_SESSION], f);
                }
                SelectedNode = tvwSession.SelectedNode;

            }
            tvwSession.EndUpdate();
            tvwSession.ResumeLayout(true);
        }
        private void OpenAndSelectPath(TreeNode tn, CmnFolder f)
        {
            if (tn.Tag is CmnFolder || tn.Tag is CmnSession)
            {
                // _s is already defined
                if (tn.Tag is CmnFolder && f.FolderPath == ((CmnFolder)tn.Tag).FolderPath)
                {
                    _selectEventActive = false;
                    UpdateFolderNode(tn);
                    tvwSession.SelectedNode = tn;
                    _selectEventActive = true;
                    return;
                }
                else
                {
                    if (!tn.IsExpanded) tn.Expand();
                    UpdateFolderNode(tn);
                    // find folder
                    foreach (TreeNode subN in tn.Nodes)
                    {
                        if (subN.Tag != null)
                        {
                            CmnFolder subF = (CmnFolder)subN.Tag;
                            if (f.FolderPath == subF.FolderPath)
                            {
                                _selectEventActive = false;
                                tvwSession.SelectedNode = subN;
                                _selectEventActive = true;
                                return;
                            }
                            else if (f.FolderPath.StartsWith(subF.FolderPath) && tn!=subN)
                            {
                                OpenAndSelectPath(subN, f);
                                break;
                            }
                        }
                    }

                }
            }
        }
        public class TreeNodeSorter : IComparer
        {
            int IComparer.Compare(object x, object y)
            {
                TreeNode tx = (TreeNode)x;
                TreeNode ty = (TreeNode)y;
                if (tx.Parent == null) return string.Compare(tx.Name, ty.Name);
                else return string.Compare(tx.Text, ty.Text);
            }
        }

        private void tvwSession_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (_selectEventActive)
            {
                SelectionChangedEventHandler handler = SelectionChanged;
                handler?.Invoke(SelectedNode, null);
            }
        }

        private void tvwSession_AfterExpand(object sender, TreeViewEventArgs e)
        {
            tvwSession.SuspendLayout();
            tvwSession.BeginUpdate();
            _selectEventActive = false;
            //NodeExpandedEventHandler neHandler = NodeExpanded;
            //neHandler?.Invoke(e.Node, null);
            UpdateFolderNode(e.Node);
            _selectEventActive = true;
            SelectionChangedEventHandler scHandler = SelectionChanged;
            scHandler?.Invoke(SelectedNode, null);
            tvwSession.EndUpdate();
            tvwSession.ResumeLayout(true);
        }

        private void tvwSession_KeyDown(object sender, KeyEventArgs e)
        {
            tvwSession.SuspendLayout();
            tvwSession.BeginUpdate();
            if (tvwSession.SelectedNode != null && tvwSession.SelectedNode.Tag is CmnFolder)
            {
                Dictionary<long, IRepositoryNode> dict = new Dictionary<long, IRepositoryNode>();
                CmnFolder f = (CmnFolder)tvwSession.SelectedNode.Tag;
                dict.Add(f.Id, f);
                _keyEvents.PerformKeyReaction(dict, e.KeyCode, e.Shift, e.Control, e.Alt, null);
            }
            tvwSession.EndUpdate();
            tvwSession.ResumeLayout(true);
        }

        private void tvwSession_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                //SelectionChanged()
                if (e.Button == MouseButtons.Right)
                {
                    // context menu
                    if (tvwSession.SelectedNode != null)
                    {
                        TreeNode clickedNode = tvwSession.HitTest(e.Location).Node;

                        if (clickedNode != null)
                        {
                            SelectionRightClickedMessage msg = new SelectionRightClickedMessage();
                            //switch (tvwSession.SelectedNode.Tag.GetType().Name)
                            switch (clickedNode.Tag.GetType().Name)
                            {
                                case "CmnFolder":
                                    msg.ListSelection.Add(((CmnFolder)(clickedNode.Tag)).Id, (CmnFolder)(clickedNode.Tag));
                                    msg.Pos = e.Location;
                                    msg.ReferenceControl = tvwSession;
                                    MessageSent?.Invoke(msg);
                                    break;
                                case "CmnSession":
                                    msg.Pos = e.Location;
                                    msg.ReferenceControl = tvwSession;
                                    MessageSent?.Invoke(msg);
                                    break;
                                    // TODO: support non folder tags for context menu
                            }

                        }
                    }
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
    }
}
