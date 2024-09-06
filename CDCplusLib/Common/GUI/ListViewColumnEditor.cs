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
using CDCplusLib.DataModel;
using C4ObjectApi.Repository;

namespace CDCplusLib.Common
{
    public partial class ListViewColumnEditor : UserControl
    {
        private const int MIN_COL_WIDTH = 10;
        private const int MAX_COL_WIDTH = 1000;
        private const int DEFAULT_COLUMN_WIDTH = 120;
        private RldColumnConfiguration _columnConfig;
        private INodeDataProvider _ndp;
        private CmnSession _s;
        private bool _eventsActive;

        public bool Dirty;
        public event EventHandler Changed;

        public ListViewColumnEditor()
        {
            InitializeComponent();
            _eventsActive = false;
            udWidth.Minimum = MIN_COL_WIDTH;
            udWidth.Maximum = MAX_COL_WIDTH;
            LocalizeGui();
        }

        public void Init(CmnSession s, INodeDataProvider ndp)
        {
            _s = s;
            _ndp = ndp;
            _eventsActive = false;
        }
        private void LocalizeGui()
        {
            ToolTip ttContainer = new ToolTip();
            ttContainer.SetToolTip(cmdAddColumn,Properties.Resources.lblAddColumn);
            ttContainer.SetToolTip(cmdRemoveColumn, Properties.Resources.lblRemoveColumn);
            ttContainer.SetToolTip(cmdColumnDown, Properties.Resources.lblColumnDown);
            ttContainer.SetToolTip(cmdColumnUp, Properties.Resources.lblColumnUp);
            lblAvailableColumns.Text = Properties.Resources.lblAvailableColumns;
            lblShownColumns.Text = Properties.Resources.lblShownColumns;
            lblWidth.Text = Properties.Resources.lblWidth;
            chkSortThisColumn.Text = Properties.Resources.lblSortThisColumn;
            chkReverse.Text = Properties.Resources.lblReverse;
            lblColumnWidthPreview.Text = Properties.Resources.lblColumnWidthPreview;
        }

        public RldColumnConfiguration ColumnConfiguration
        {
            get
            {
                return _columnConfig;
            }
            set
            {
                if(value!=null)
                {
                    _eventsActive = false;
                    _columnConfig = value;
                    lbShown.Items.Clear();
                    HashSet<string> shownFieldNames = new HashSet<string>();
                    foreach(RldColumnDefinition cd in _columnConfig.ColumnDefinitions.Values)
                    {
                        lbShown.Items.Add(cd);
                        shownFieldNames.Add(cd.ColumnType);
                    }

                    lbAvailable.Items.Clear();
                    foreach (string fieldName in _ndp.GetKnownStandardFields().Keys)
                    {
                        if(!shownFieldNames.Contains(fieldName))
                        {
                            SimpleDisplayItem sdi = new SimpleDisplayItem(fieldName, _ndp.GetKnownStandardFields()[fieldName]);
                            sdi.Parameters.Add("custom", "false");
                            lbAvailable.Items.Add(sdi);
                        }
                    }
                    foreach (string fieldName in _ndp.GetKnownCustomFields().Keys)
                    {
                        if (!shownFieldNames.Contains(fieldName))
                        {
                            SimpleDisplayItem sdi = new SimpleDisplayItem(fieldName, _ndp.GetKnownCustomFields()[fieldName]);
                            sdi.Parameters.Add("custom", "true");
                            lbAvailable.Items.Add(sdi);
                        }
                    }
                    lbShown.SelectedIndex = 0;
                    SetColumn(true);
                    _eventsActive = Visible;
                }
            }
        }

        private void SetColumn(bool overrideEventsActive)
        {
            if(_eventsActive || overrideEventsActive)
            {
                _eventsActive = false;
                if (lbShown.SelectedItem != null)
                {
                    RldColumnDefinition cd = (RldColumnDefinition)lbShown.SelectedItem;
                    udWidth.Value = cd.Width;
                    colDemo.Width = cd.Width;
                    colDemo.Text = cd.ToString();
                    chkSortThisColumn.Checked = cd.SortOption != RldColumnDefinition.SortOptions.None;
                    chkReverse.Checked = cd.SortOption == RldColumnDefinition.SortOptions.SortDescending;
                }
                else
                {
                    chkSortThisColumn.Checked = false;
                    chkReverse.Checked = false;
                    udWidth.Value = DEFAULT_COLUMN_WIDTH;
                    colDemo.Width = DEFAULT_COLUMN_WIDTH;
                    colDemo.Text = "";
                }
                ActivateControls(false);
                _eventsActive = true;
            }

        }

        public Dictionary<string, RldColumnDefinition> GetCurrentColumnSettings()
        {
            Dictionary<string, RldColumnDefinition> result = new Dictionary<string, RldColumnDefinition>();
            foreach(RldColumnDefinition cd in lbShown.Items)
            {
                result.Add(cd.ColumnType, cd);
            }

            return result;
        }


        private void lbShown_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetColumn(false);
        }

        private void ActivateControls(bool dirty)
        {
            Dirty = Dirty || dirty;
            cmdRemoveColumn.Enabled = (lbShown.SelectedItem != null && lbShown.SelectedIndex != 0); // name must not be removed
            cmdAddColumn.Enabled = (lbAvailable.SelectedItem != null);
            cmdColumnUp.Enabled = (lbShown.SelectedItem != null && lbShown.SelectedIndex >1);   // name must always be the first column
            cmdColumnDown.Enabled = (lbShown.SelectedItem!=null && lbShown.SelectedIndex > 0 && lbShown.SelectedIndex != lbShown.Items.Count-1);
            udWidth.Enabled = (lbShown.SelectedItem != null);
            lvwHeader.Enabled = (lbShown.SelectedItem != null);
            chkReverse.Enabled = chkSortThisColumn.Checked;
            if (dirty)
            {
                Changed?.Invoke(this, new EventArgs());
            }
        }

        private void lbAvailable_SelectedIndexChanged(object sender, EventArgs e)
        {
            ActivateControls(false);
        }

        private void cmdAddColumn_Click(object sender, EventArgs e)
        {
            if (_eventsActive)
            {
                if (lbAvailable.SelectedItem != null)
                {
                    SimpleDisplayItem sdi = (SimpleDisplayItem)lbAvailable.SelectedItem;
                    RldColumnDefinition cd = new RldColumnDefinition(sdi.Key, DEFAULT_COLUMN_WIDTH, RldColumnDefinition.SortOptions.None, sdi.Parameters["custom"] == "true", _s, _ndp);
                    lbShown.Items.Add(cd);
                    lbShown.SelectedItem = cd;
                    lbAvailable.Items.Remove(lbAvailable.SelectedItem);
                    _columnConfig.ColumnDefinitions.Add(cd.ColumnType, cd);
                    SetColumn(true);
                    ActivateControls(true);
                }
            }
        }

        private void cmdRemoveColumn_Click(object sender, EventArgs e)
        {
            if (_eventsActive)
            {
                if (lbShown.SelectedItem!=null)
                {
                    RldColumnDefinition cd = (RldColumnDefinition)lbShown.SelectedItem;
                    SimpleDisplayItem sdi = new SimpleDisplayItem(cd.ColumnType, cd.ToString());
                    sdi.Parameters.Add("custom", cd.CustomField ? "true" : "false");
                    lbAvailable.Items.Add(sdi);
                    lbAvailable.SelectedIndex=0;
                    lbShown.Items.Remove(lbShown.SelectedItem);
                    lbShown.SelectedIndex = 0;
                    _columnConfig.ColumnDefinitions.Remove(cd.ColumnType);
                    ActivateControls(true);
                }
            }
        }

        private void cmdColumnUp_Click(object sender, EventArgs e)
        {
            if (_eventsActive)
            {
                lbShown.Items.Insert(lbShown.SelectedIndex - 1, lbShown.SelectedItem);
                lbShown.SelectedIndex = lbShown.SelectedIndex - 2;
                lbShown.Items.RemoveAt(lbShown.SelectedIndex + 2);
                _columnConfig.ColumnDefinitions.Clear();
                foreach (RldColumnDefinition cd in lbShown.Items)
                {
                    _columnConfig.ColumnDefinitions.Add(cd.ColumnType, cd);
                }
                ActivateControls(true);
            }
        }

        private void cmdColumnDown_Click(object sender, EventArgs e)
        {
            if (_eventsActive)
            {
                lbShown.Items.Insert(lbShown.SelectedIndex + 2, lbShown.SelectedItem);
                lbShown.SelectedIndex = lbShown.SelectedIndex +2;
                lbShown.Items.RemoveAt(lbShown.SelectedIndex -2);
                _columnConfig.ColumnDefinitions.Clear();
                foreach(RldColumnDefinition cd in lbShown.Items)
                {
                    _columnConfig.ColumnDefinitions.Add(cd.ColumnType, cd);
                }
                ActivateControls(true);
            }
        }

        private void udWidth_ValueChanged(object sender, EventArgs e)
        {
            if (_eventsActive && _columnConfig.ColumnDefinitions.ContainsKey(((RldColumnDefinition)lbShown.SelectedItem).ColumnType))
            {
                lvwHeader.Columns[0].Width = (int)udWidth.Value;
                RldColumnDefinition cd = _columnConfig.ColumnDefinitions[((RldColumnDefinition)lbShown.SelectedItem).ColumnType];
                cd.Width = (int)udWidth.Value;
                ActivateControls(true);
            }
        }

        private void lvwHeader_ColumnWidthChanged(object sender, ColumnWidthChangedEventArgs e)
        {
            if (_eventsActive && _columnConfig.ColumnDefinitions.ContainsKey(((RldColumnDefinition)lbShown.SelectedItem).ColumnType))
            {
                if (lvwHeader.Columns[0].Width < MIN_COL_WIDTH) lvwHeader.Columns[0].Width = MIN_COL_WIDTH;
                if (lvwHeader.Columns[0].Width > MAX_COL_WIDTH) lvwHeader.Columns[0].Width = MAX_COL_WIDTH;
                udWidth.Value = lvwHeader.Columns[0].Width;
                RldColumnDefinition cd = _columnConfig.ColumnDefinitions[((RldColumnDefinition)lbShown.SelectedItem).ColumnType];
                cd.Width = (int)udWidth.Value;
                ActivateControls(true);
            }
        }

        private void ListViewColumnEditor_VisibleChanged(object sender, EventArgs e)
        {
            _eventsActive = Visible;

        }

        private void chkSortThisColumn_CheckedChanged(object sender, EventArgs e)
        {
            if (_eventsActive && _columnConfig.ColumnDefinitions.ContainsKey(((RldColumnDefinition)lbShown.SelectedItem).ColumnType))
            {
                UpdateSortSettings();
                ActivateControls(true);
            }
        }

        private void chkReverse_CheckedChanged(object sender, EventArgs e)
        {
            if (_eventsActive && _columnConfig.ColumnDefinitions.ContainsKey(((RldColumnDefinition)lbShown.SelectedItem).ColumnType))
            {
                UpdateSortSettings();
                ActivateControls(true);
            }
        }

        private void UpdateSortSettings()
        {
            if (lbShown.SelectedItem != null)
            {
                foreach(RldColumnDefinition cd in lbShown.Items)
                {
                    if(cd.ColumnType == ((RldColumnDefinition)lbShown.SelectedItem).ColumnType)
                    {
                        cd.SortOption = chkSortThisColumn.Checked ?
                                        (chkReverse.Checked ? RldColumnDefinition.SortOptions.SortDescending : RldColumnDefinition.SortOptions.SortAscending) :
                                        RldColumnDefinition.SortOptions.None;
                    }
                    else
                    {
                        //cd.SortOption = RldColumnDefinition.SortOptions.None;
                    }
                }
            }
        }

    }
}
