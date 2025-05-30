﻿// Copyright 2012,2024 texolution GmbH
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
using CDCplusLib.Interfaces;
using System.Xml;
using C4ObjectApi.Interfaces;
using C4ObjectApi.Repository;
using C4GeneralGui.GuiElements;
using CDCplusLib.EventData;

namespace CDCplusLib.Common.GUI
{
    public partial class ContextTabControlContainer : UserControl
    {
        private CmnSession _s;
        private string _currentTabFingerprint; // just a sequence of all the keys for comparison reasons
        private GlobalApplicationData _gad;
        private Dictionary<string, TabPage> _tabPages;
        protected string _lastListTabSelectedKey;
        private Dictionary<long, IRepositoryNode> _context;
        private bool _listContext;
        private string _lastSelectedTabName;
        private readonly object _tabContextLock = new object();

        public SessionWindowRequestEventHandler SessionWindowRequest;
        public ListSelectionChangedEventHandler ListSelectionChanged;
        public TreeSelectionChangedEventHandler TreeSelectionChanged;
        public ContextMenuRequestEventHandler ContextMenuRequest;
        public FunctionRequestEventHandler FunctionRequest;
        public NodesModifiedEventHandler NodesModified;
        public KeyPressedEventHandler KeyPressedEvent;
        public RefreshRequestEventHandler RefreshRequest;
        public bool EventsActive { get; set; }
        public Dictionary<long, IRepositoryNode> ControlSelection
        {
            get
            {
                IGenericControl gc = (IGenericControl)tabContext.SelectedTab.Controls[0];
                return gc.Selection;
            }
            set
            {
                IGenericControl gc = (IGenericControl)tabContext.SelectedTab.Controls[0];
                gc.Selection=value;
            }
        }
        public bool ControlHasSelection
        {
            get
            {
                IGenericControl gc = (IGenericControl)tabContext.SelectedTab.Controls[0];
                return gc.HasSelection;
            }
        }

        public ContextTabControlContainer()
        {
            InitializeComponent();
            EventsActive = false;
        }
        public void Init(CmnSession s, bool listContext, GlobalApplicationData globalAppData)
        {
            _s = s;
            _gad = globalAppData;
            _listContext = listContext;
            InitTabControl();
        }

        protected virtual void tabContext_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                CheckForUnsavedChanges();
                if (tabContext.SelectedTab!=null && tabContext.SelectedTab.Controls.Count>0)
                {
                    IGenericControl listGc = (IGenericControl)tabContext.SelectedTab.Controls[0];
                    listGc.Init(_context);
                }
            }
            catch (NotImplementedException ex)
            {
                StandardMessage.ShowMessage(Properties.Resources.exFailureUpdatingTabs, StandardMessage.Severity.ErrorMessage, this, ex);
            }
            // for the time being, ignore
            // TODO: think of a better solution (clear control, remove tab, ...)
            catch (NullReferenceException ex)
            {
                // ignore
            }
            catch (Exception ex)
            {
                StandardMessage.ShowMessage(Properties.Resources.exFailureUpdatingTabs, StandardMessage.Severity.ErrorMessage, this,ex);
            }
        }

        public void CheckForUnsavedChanges()
        {
            Enabled = false;
            foreach (TabPage tp in tabContext.TabPages)
            {
                if (tp!=null && tp.Controls.Count > 0)
                {
                    IGenericControl gc = (IGenericControl)tp.Controls[0];
                    if (gc.IsDirty)
                    {
                        if (MessageBox.Show(string.Format(Properties.Resources.msgSaveUnsavedChanges, tp.Text), Properties.Resources.lblUnsavedChanges, MessageBoxButtons.YesNo)==DialogResult.Yes) gc.Save();
                        else gc.ReInit();
                    }
                }
            }

            Enabled = true;
        }
        //public SessionWindowRequestEventHandler SessionWindowRequest;
        //public ListSelectionChangedEventHandler ListSelectionChanged;
        //public TreeSelectionChangedEventHandler TreeSelectionChanged;
        //public ContextMenuRequestEventHandler ContextMenuRequest;
        //public FunctionRequestEventHandler FunctionRequest;
        //public NodesModifiedEventHandler NodesModified;
        //public KeyPressedEventHandler KeyPressedEvent;
        //public RefreshRequestEventHandler RefreshRequest;

        protected virtual void InitTabControl()
        {
            _tabPages = new Dictionary<string, TabPage>();
            tabContext.TabPages.Clear();
            int i = 0;

            // add object controls
            foreach (XmlElement ocEl in _s.UserConfig.DocumentElement.SelectNodes("classes/tab_controls/tab_control"))
            {
                try
                {
                    string asm = ocEl.GetAttribute("assembly");
                    string type = ocEl.GetAttribute("type");
                    IGenericControl gc = (IGenericControl)_s.SessionConfig.GetAssembly(asm).CreateInstance(asm + "." + type);
                    if (gc is null)
                    {
                        throw new ApplicationException(string.Format("Object control {0}.{1} has not been found.", ocEl.GetAttribute("assembly"), ocEl.GetAttribute("type")));
                    }
                    if(gc.ListContext==_listContext)
                    {
                        gc.SessionWindowRequest += SessionWindowRequestHandler;
                        gc.ListSelectionChanged += ListSelectionChangedHandler;
                        gc.TreeSelectionChanged += TreeSelectionChangedHandler;
                        gc.ContextMenuRequest += ContextMenuRequestEventHandler;
                        gc.NodesModified += NodesModifiedEventHandler;
                        //gc.KeyPressed += FunctionRequestEventHandler;
                        gc.FunctionRequest += FunctionRequestEventHandler;
                        gc.Reset(_s, _gad, ocEl);
                        TabPage tp = new TabPage(gc.GetTabText());
                        tp.Name = i.ToString();
                        tp.Controls.Add((Control)gc);
                        tp.Controls[0].Dock = DockStyle.Fill;
                        _tabPages.Add(i.ToString(), tp);
                        i += 1;

                    }
                }
                catch (Exception ex)
                {
                    StandardMessage.ShowMessage("Failure initializing tab control " + ocEl.GetAttribute("assembly") + "." + ocEl.GetAttribute("type"), StandardMessage.Severity.WarningMessage, this, ex);
                }
            }

            _currentTabFingerprint = "";
        }

        protected virtual void SessionWindowRequestHandler(WindowSelectionData wsd)
        {
            if (EventsActive) SessionWindowRequest?.Invoke(wsd);
        }
        protected virtual void ListSelectionChangedHandler(WindowSelectionData wsd)
        {
            if (EventsActive) ListSelectionChanged?.Invoke(wsd);
        }
        protected virtual void TreeSelectionChangedHandler(WindowSelectionData wsd, ISessionWindow sw)
        {
            if (EventsActive) TreeSelectionChanged?.Invoke(wsd, null);
        }
        protected virtual void ContextMenuRequestEventHandler(WindowSelectionData wsd, Point position)
        {
            if (EventsActive) ContextMenuRequest?.Invoke(wsd, position);
        }
        protected virtual void FunctionRequestEventHandler(WindowSelectionData wsd, string assembly, string type)
        {
            if (EventsActive) FunctionRequest?.Invoke(wsd, assembly, type);
        }
        protected virtual void NodesModifiedEventHandler(WindowSelectionData wsd)
        {
            if (EventsActive) NodesModified?.Invoke(wsd);
        }
        protected virtual void KeyPressedEventHandler(WindowSelectionData wsd, Keys key, bool shift, bool ctrl, bool alt)
        {
            if (EventsActive) KeyPressedEvent?.Invoke(wsd, key, shift, ctrl, alt);
        }
        protected virtual void RefreshRequestEventHandler(WindowSelectionData wsd)
        {
            if (EventsActive) RefreshRequest?.Invoke();
        }

        public void UpdateTabControl(Dictionary<long, IRepositoryNode> context, IGenericControl.ContextType ct, WindowSelectionData wsd)
        {
            if (InvokeRequired)
            {
                Invoke((MethodInvoker)(() => UpdateTabControl(context, ct, wsd)));
                return;
            }

            lock (_tabContextLock)
            {
                try
                {
                    tabContext.SuspendLayout();

                    // Temporarily disable the SelectedIndexChanged event to avoid redundant calls
                    tabContext.SelectedIndexChanged -= tabContext_SelectedIndexChanged;

                    _context = context;

                    string newTabFingerprint = null;
                    foreach (string k in _tabPages.Keys)
                    {
                        TabPage tp = _tabPages[k];
                        if (tp.Controls.Count > 0)
                        {
                            IGenericControl ctl = tp.Controls[0] as IGenericControl;
                            if (ctl != null && ctl.IsValid(_context, ct))
                            {
                                if (newTabFingerprint == null)
                                {
                                    newTabFingerprint = k;
                                }
                                else
                                {
                                    newTabFingerprint += "#" + k;
                                }
                            }
                        }
                    }

                    TabPage sel = tabContext.SelectedTab;
                    if (sel != null) _lastSelectedTabName = sel.Name;

                    if (_currentTabFingerprint != newTabFingerprint)
                    {
                        tabContext.TabPages.Clear();
                        foreach (string k in _tabPages.Keys)
                        {
                            TabPage tp = _tabPages[k];
                            if (tp.Controls.Count > 0)
                            {
                                IGenericControl ctl = tp.Controls[0] as IGenericControl;
                                if (ctl != null && ctl.IsValid(_context, ct))
                                {
                                    tabContext.TabPages.Add(tp);
                                }
                            }
                        }
                    }

                    if (tabContext.TabPages.ContainsKey(_lastSelectedTabName))
                    {
                        tabContext.SelectTab(_lastSelectedTabName);
                    }
                    else if (tabContext.TabPages.Count > 0)
                    {
                        tabContext.SelectTab(0);
                    }

                    if (tabContext.SelectedTab != null && tabContext.SelectedTab.Controls.Count > 0)
                    {
                        IGenericControl ctl = tabContext.SelectedTab.Controls[0] as IGenericControl;
                        ctl?.Init(_context);
                    }

                    _currentTabFingerprint = newTabFingerprint;
                }
                catch (Exception ex)
                {
                    // this was commented out, because the msg doesn't help anything here
                    //StandardMessage.ShowMessage(Properties.Resources.exFailureUpdatingTabs, StandardMessage.Severity.ErrorMessage, this, ex);
                }
                finally
                {
                    tabContext.SelectedIndexChanged += tabContext_SelectedIndexChanged;
                    tabContext.ResumeLayout();
                }
            }
        }
    }
}
