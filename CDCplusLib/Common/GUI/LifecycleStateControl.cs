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
using CDCplusLib.Interfaces;
using C4ObjectApi.Repository;
using C4ServerConnector.Assets;

namespace CDCplusLib.Common.GUI
{
    public partial class LifecycleStateControl : UserControl
    {
        public LifecycleStateControl()
        {
            InitializeComponent();
        }

        private IIconService iconService_;
        private bool initComplete_;
        private bool allowNull_;

        public event GuiChangedEventHandler GuiChanged;

        public delegate void GuiChangedEventHandler();

        public void Init(CmnSession s, bool allowNull = true, C4LifecycleState lcs = null)
        {
            initComplete_ = false;
            LocalizeGui();
            allowNull_ = allowNull;
            if (iconService_ is null)
            {
                iconService_ = (IIconService)s.GetSessionExtension("icon_service");
                tvwLifecycle.ImageList = iconService_.GlobalSmallImageList;
            }

            tvwLifecycle.Nodes.Clear();
            foreach (C4Lifecycle lc in s.SessionConfig.C4Sc.LifecyclesById.Values)
            {
                if (s.IsSuperuser | !lc.Name.StartsWith("_"))
                {
                    bool found = false;
                    TreeNode tn = tvwLifecycle.Nodes.Add(lc.Name, lc.LocalizedName, "lifecycle", "lifecycle");
                    tn.Tag = lc;
                    SortedDictionary<string, C4LifecycleState> sl = new SortedDictionary<string, C4LifecycleState>();
                    foreach (C4LifecycleState subLcs in lc.States.Values)
                        sl.Add(subLcs.Name, subLcs);
                    foreach (C4LifecycleState subLcs in sl.Values)
                    {
                        TreeNode stn = tn.Nodes.Add(lc.Name + ":" + subLcs.Name, subLcs.LocalizedName, "lifecycle_state", "lifecycle_state");
                        stn.Tag = subLcs;
                        if (lcs is object && lcs == subLcs)
                        {
                            tvwLifecycle.SelectedNode = stn;
                            found = true;
                        }
                    }

                    if (found)
                    {
                        tn.ExpandAll();
                    }
                    else
                    {
                        tn.Collapse();
                    }
                }
            }

            tvwLifecycle.ExpandAll();
            if (!allowNull)
            {
                optLifecycle.Enabled = false;
                optLifecycle.Checked = true;
                optNoLifecycle.Enabled = false;
                tvwLifecycle.Enabled = true;
                if(tvwLifecycle.Nodes[0].Nodes.Count>0) tvwLifecycle.SelectedNode = tvwLifecycle.Nodes[0].Nodes[0];
            }

            if (lcs is null)
            {
                optNoLifecycle.Checked = true;
                optLifecycle.Checked = false;
            }
            else
            {
                optNoLifecycle.Checked = false;
                optLifecycle.Checked = true;
            }

            initComplete_ = true;
        }

        private void LocalizeGui()
        {
            optLifecycle.Text = Properties.Resources.lblLifecycleAndState;
            optNoLifecycle.Text = Properties.Resources.lblNoLifecycle;
        }

        public C4LifecycleState SelectedLifecycleState
        {
            get
            {
                if (optNoLifecycle.Checked)
                    return null;
                if (tvwLifecycle.SelectedNode is null)
                {
                    if (tvwLifecycle.Nodes[0].Nodes.Count > 0) tvwLifecycle.SelectedNode = tvwLifecycle.Nodes[0].Nodes[0]; 
                }
                else if (tvwLifecycle.SelectedNode.Tag is null)
                {
                    optNoLifecycle.Checked = true;
                    return null;
                }

                if (!(tvwLifecycle.SelectedNode is null) && tvwLifecycle.SelectedNode.Tag is C4LifecycleState)
                    return (C4LifecycleState)tvwLifecycle.SelectedNode.Tag;
                return null;
            }

            set
            {
                initComplete_ = false;
                if (value is null)
                {
                    optNoLifecycle.Checked = true;
                    optLifecycle.Checked = false;
                    if (allowNull_)
                    {
                        tvwLifecycle.SelectedNode = null;
                    }
                    else
                    {
                        optNoLifecycle.Checked = false;
                        optLifecycle.Checked = true;
                        tvwLifecycle.Enabled = true;
                        if (tvwLifecycle.Nodes[0].Nodes.Count > 0) tvwLifecycle.SelectedNode = tvwLifecycle.Nodes[0].Nodes[0];
                    }
                }
                else
                {
                    optNoLifecycle.Checked = false;
                    optLifecycle.Checked = true;
                    foreach (TreeNode tn in tvwLifecycle.Nodes)
                    {
                        foreach (TreeNode lcsTn in tn.Nodes)
                        {
                            C4LifecycleState curLcs = (C4LifecycleState)lcsTn.Tag;
                            if (curLcs == value)
                            {
                                tvwLifecycle.SelectedNode = lcsTn;
                            }
                        }
                    }
                }

                initComplete_ = true;
            }
        }

        private void optNoLifecycle_CheckedChanged(object sender, EventArgs e)
        {
            ActivateControls();
        }

        private void optLifecycle_CheckedChanged(object sender, EventArgs e)
        {
            ActivateControls();
        }

        private void ActivateControls()
        {
            optLifecycle.Enabled = true;
            optNoLifecycle.Enabled = true;
            tvwLifecycle.Enabled = optLifecycle.Checked;
            if (initComplete_)
                GuiChanged?.Invoke();
        }

        private void tvwLifecycle_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (initComplete_)
                GuiChanged?.Invoke();
        }
    }
}
