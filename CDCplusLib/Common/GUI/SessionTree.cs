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
using System.Collections;
using System.Xml;
using C4ObjectApi.Interfaces;
using C4ObjectApi.Repository;
using C4GeneralGui.GuiElements;
using CDCplusLib.EventData;
using Windows.Media.Audio;

namespace CDCplusLib.Common.GUI
{
    public partial class SessionTree : UserControl
    {
        public enum RootNodeTypes { Session, Home, Searches, Locked, Tasks, Results, Undefined }
        public const string NODE_SESSION = "00_session";
        public const string NODE_HOME = "01_home";
        public const string NODE_SEARCHES = "02_searches";
        public const string NODE_LOCKED = "03_locked";
        public const string NODE_TASKS = "04_tasks";
        public const string NODE_RESULTS = "05_results";
        public const string DUMMY_FOLDER = "";  // assign empty string, otherwise it shows the name defined here when building the folder view
        private CmnSession _s;
        private XmlElement _configEl;
        //private bool _selectEventActive;
        private KeyEventTable _keyEvents;

        public SessionWindowRequestEventHandler SessionWindowRequest;
        public TreeSelectionChangedEventHandler TreeSelectionChanged;
        public ContextMenuRequestEventHandler ContextMenuRequest;
        public FunctionRequestEventHandler FunctionRequest;
        public NodesModifiedEventHandler NodesModified;
        public event KeyEventTable.KeyPressedEventHandler KeyPressedEvent;
        public SessionTree()
        {
            // image list is filled by code because serializing the image list in designer wouldn't work without further changes
            InitializeComponent();
            EventsActive = false;
            //_selectEventActive = true;
        }
        public bool EventsActive { get; set; }
        public TreeNode SelectedNode
        {
            get { return tvwSession.SelectedNode; }
            set { tvwSession.SelectedNode = value; }
        }
        public void InitTreeView(CmnSession s)
        {
            tvwSession.SuspendLayout();
            tvwSession.BeginUpdate();
            _s = s;
            _configEl = (XmlElement)_s.UserConfig.DocumentElement.SelectSingleNode("classes/session_tree");
            _keyEvents = new KeyEventTable((XmlElement)_configEl.SelectSingleNode("key_events"), KeyEventTable.Modes.Tree);
            _keyEvents.KeyPressedEvent += KeyPressedEventHandler;

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
        protected virtual void SessionWindowRequestEventHandler(WindowSelectionData wsd)
        {
            SessionWindowRequest?.Invoke(wsd);
        }
        protected virtual void TreeSelectionChangedEventHandler(WindowSelectionData wsd, ISessionWindow sw)
        {
            TreeSelectionChanged?.Invoke(wsd, sw);
        }

        protected virtual void ContextMenuRequestEventHandler(WindowSelectionData wsd, Point position)
        {
            ContextMenuRequest?.Invoke(wsd, position);
        }

        protected virtual void FunctionRequestEventHandler(WindowSelectionData wsd, string assembly, string type)
        {
            FunctionRequest?.Invoke(wsd, assembly, type);
        }

        protected virtual void NodesModifiedEventHandler(WindowSelectionData wsd)
        {
            NodesModified?.Invoke(wsd);
        }
        protected virtual void KeyPressedEventHandler(WindowSelectionData wsd, Keys key, bool shift, bool ctrl, bool alt)
        {
            KeyPressedEvent?.Invoke(wsd, key, shift, ctrl, alt);
        }



        public void UpdateFolderNode(TreeNode n)
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
                //_selectEventActive = false;
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
                //_selectEventActive = true;
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
        public void SetSelection(WindowSelectionData wsd)
        {
            tvwSession.SuspendLayout();
            tvwSession.BeginUpdate();
            switch (wsd.RootNodeType)
            {
                //case SessionTree.RootNodeTypes.Session:
                //    break;
                //case SessionTree.RootNodeTypes.Home:
                //    break;
                //case SessionTree.RootNodeTypes.Searches:
                //    break;
                case RootNodeTypes.Locked:
                case RootNodeTypes.Tasks:
                    throw new NotImplementedException();
                    break;
                case RootNodeTypes.Results:
                    tvwSession.SuspendLayout();
                    tvwSession.BeginUpdate();
                    tvwSession.Nodes[NODE_RESULTS].Tag = wsd.ResultList;
                    // TODO: if wsd.Selection has content, select the objects / folders
                    tvwSession.SelectedNode = tvwSession.Nodes[NODE_RESULTS];
                    tvwSession.EndUpdate();
                    tvwSession.ResumeLayout(true);
                    break;
                default:
                    TreeNode rootNode = null;
                    if(wsd.RootNodeType==RootNodeTypes.Results) rootNode = tvwSession.Nodes[NODE_RESULTS];
                    else if(wsd.SelectedFolder!=null)
                    {
                        if (wsd.SelectedFolder.FolderPath.StartsWith(_s.SessionConfig.HomeFolder.FolderPath)) rootNode = tvwSession.Nodes[NODE_HOME];
                        else if (wsd.SelectedFolder.FolderPath.StartsWith(_s.SessionConfig.SearchesFolder.FolderPath)) rootNode = tvwSession.Nodes[NODE_SEARCHES];
                        else rootNode = tvwSession.Nodes[NODE_SESSION];
                    }
                    else rootNode = tvwSession.Nodes[NODE_SESSION];

                    if(wsd.SelectedFolder!=null) OpenAndSelectPath(rootNode, wsd.SelectedFolder);
                    else tvwSession.SelectedNode = rootNode;
                    break;

            }
            SelectedNode = tvwSession.SelectedNode;
            tvwSession.EndUpdate();
            tvwSession.ResumeLayout(true);
        }
        public void OpenAndSelectPath(TreeNode tn, CmnFolder f)
        {
            if (tn.Tag is CmnFolder || tn.Tag is CmnSession)
            {
                // _s is already defined
                if (tn.Tag is CmnFolder && f.FolderPath == ((CmnFolder)tn.Tag).FolderPath)
                {
                    //_selectEventActive = false;
                    UpdateFolderNode(tn);
                    tvwSession.SelectedNode = tn;
                    //_selectEventActive = true;
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
                                //_selectEventActive = false;
                                tvwSession.SelectedNode = subN;
                                //_selectEventActive = true;
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
            if (EventsActive)
            {
                EventsActive = false;
                WindowSelectionData wsd = new WindowSelectionData();
                wsd.RootNodeType = GetRoodNodeType(tvwSession.SelectedNode);
                if (wsd.RootNodeType==RootNodeTypes.Results) wsd.SelectedFolder = null;
                else if (wsd.RootNodeType == RootNodeTypes.Tasks) wsd.SelectedFolder = null;
                else if (wsd.RootNodeType == RootNodeTypes.Locked) wsd.SelectedFolder = null;
                else if (tvwSession.SelectedNode.Tag is CmnFolder) wsd.SelectedFolder = tvwSession.SelectedNode.Tag as CmnFolder;
                else wsd.SelectedFolder = (tvwSession.SelectedNode.Tag as CmnSession).RootFolder;

                TreeSelectionChanged?.Invoke(wsd, null);
                EventsActive = true;
            }
        }

        private RootNodeTypes GetRoodNodeType(TreeNode tn)
        {
            if(tn==null) return RootNodeTypes.Undefined;

            // Walk up to the top-level parent
            while (tn.Parent != null)
            {
                tn = tn.Parent;
            }

            // Now tn is a direct child of tvwSession.Nodes — i.e., a root node
            switch (tn.Name)
            {
                case "00_session":
                    return RootNodeTypes.Session;
                case "01_home":
                    return RootNodeTypes.Home;
                case "02_searches":
                    return RootNodeTypes.Searches;
                case "03_locked":
                    return RootNodeTypes.Locked;
                case "04_tasks":
                    return RootNodeTypes.Tasks;
                case "05_results":
                    return RootNodeTypes.Results;
                default:
                    return RootNodeTypes.Undefined;
            }
        }
        private void tvwSession_AfterExpand(object sender, TreeViewEventArgs e)
        {
            tvwSession.SuspendLayout();
            tvwSession.BeginUpdate();
            //bool selectEventActiveBuf = _selectEventActive;
            //_selectEventActive = false;
            //NodeExpandedEventHandler neHandler = NodeExpanded;
            //neHandler?.Invoke(e.Node, null);
            UpdateFolderNode(e.Node);
            //_selectEventActive = selectEventActiveBuf;
            //TreeSelectionChangedEventHandler scHandler = TreeSelectionChanged;

            //scHandler?.Invoke(SelectedNode, null);
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
                    TreeNode clickedNode = tvwSession.HitTest(e.Location).Node;

                    if (clickedNode != null)
                    {
                        if (clickedNode.Name== "03_locked" || clickedNode.Name == "04_tasks" || clickedNode.Name == "05_results")
                        {
                            // ignore for the time being
                        }
                        else
                        {
                            //switch (tvwSession.SelectedNode.Tag.GetType().Name)
                            WindowSelectionData wsd = new WindowSelectionData();

                            //if (tvwSession.SelectedNode.Tag is CmnFolder)
                            if (clickedNode.Tag is CmnFolder)
                            {
                                wsd.SelectedFolder = clickedNode.Tag as CmnFolder;
                                wsd.Selection.Add(wsd.SelectedFolder.Id, wsd.SelectedFolder);
                                ContextMenuRequest?.Invoke(wsd, tvwSession.PointToScreen(e.Location));
                            }
                            else
                            {
                                if (tvwSession.SelectedNode.Tag is Dictionary<long, IRepositoryNode>)
                                {
                                    Dictionary<long, IRepositoryNode> dict = tvwSession.SelectedNode.Tag as Dictionary<long, IRepositoryNode>;
                                    if (dict.Count>0) wsd.Selection = dict;
                                    else wsd.SelectedFolder = (clickedNode.Tag as CmnSession).RootFolder;
                                }
                                else if(tvwSession.SelectedNode.Tag is Dictionary<long, CmnObject>)
                                {
                                    Dictionary<long, IRepositoryNode> dict = (clickedNode.Tag as Dictionary<long, CmnObject>).ToDictionary(
                                        kvp => kvp.Key,
                                        kvp => (IRepositoryNode)kvp.Value
                                    );
                                    wsd.Selection = dict;
                                }
                                ContextMenuRequest?.Invoke(wsd, tvwSession.PointToScreen(e.Location));
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
