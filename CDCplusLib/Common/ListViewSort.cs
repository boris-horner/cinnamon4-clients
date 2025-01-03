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
using System.Collections;
using System.Runtime.CompilerServices;

namespace CDCplusLib.Common
{
    public class ListViewSort
    {
        public bool Active
        {
            get
            {
                return _active;
            }
            set
            {
                _active = value;
                if (_lvw != null)
                {
                    if (_active) ReSort();
                    else _lvw.ListViewItemSorter = null;
                }
            }
        }
        private ListView lvw_
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _lvw;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_lvw != null)
                {
                    _lvw.ColumnClick -= lvw_ColumnClick;
                }

                _lvw = value;
                if (_lvw != null)
                {
                    _lvw.ColumnClick += lvw_ColumnClick;
                }
            }
        }

        private ListView _lvw;
        private bool _active;
        private readonly Form _frm;
        public SortOrder ColumnSortOrder = SortOrder.Ascending;
        public int LastColumn;

        public ListViewSort(ListView lvw, bool sortByColumnClickAutomatic = true, bool setNumericColumsRightAlign = true)
        {
            Active = false;
            LastColumn = -1;
            lvw_ = lvw;
            _frm = lvw.FindForm();
            SortByColumnClick = sortByColumnClickAutomatic;
            if (setNumericColumsRightAlign)
            {
                for (int i = 0, loopTo = lvw.Columns.Count - 1; i <= loopTo; i++)
                    NumericRightAlign(i);
            }
            Active = true;
        }

        public void ReSort()
        {
            if (LastColumn != -1)
            {
                Sort(LastColumn, ColumnSortOrder);
            }
        }
        public bool SortByColumnClick { get; set; } = true;

        public void NumericRightAlign(int columnIndex = -1)
        {
            if (columnIndex == -1)
            {
                for (int i = 1, loopTo = lvw_.Columns.Count - 1; i <= loopTo; i++)
                {
                    if (get_IsNumericColumn(i, true))
                    {
                        lvw_.Columns[i].TextAlign = HorizontalAlignment.Right;
                    }
                }
            }
            else if (get_IsNumericColumn(columnIndex, true))
            {
                lvw_.Columns[columnIndex].TextAlign = HorizontalAlignment.Right;
            }
        }
        public void Sort(int columnIndex= -1, SortOrder sortOrder = SortOrder.None)
        {
            int ci = -1;
            if (columnIndex == -1)
            {
                if (LastColumn == -1) // no column set yet - init to first column with sort order ascending
                {
                    ci = 0;
                    ColumnSortOrder = sortOrder==SortOrder.None?SortOrder.Ascending:sortOrder;
                }
                else
                {
                    ci = LastColumn;
                    ColumnSortOrder = sortOrder == SortOrder.None?(ColumnSortOrder == SortOrder.Descending) ? SortOrder.Ascending : ColumnSortOrder = SortOrder.Descending:sortOrder;
                }
            }
            else
            {
                ci = columnIndex;
                if(ci==LastColumn) ColumnSortOrder = sortOrder == SortOrder.None ? (ColumnSortOrder == SortOrder.Descending) ? SortOrder.Ascending : ColumnSortOrder = SortOrder.Descending : sortOrder;
                else ColumnSortOrder = ColumnSortOrder = sortOrder == SortOrder.None ? SortOrder.Ascending:sortOrder;
            }

            Cursor cc = null;
            if (_frm != null)
            {
                cc = _frm.Cursor;
                _frm.Cursor = Cursors.WaitCursor;
            }

            lvw_.ListViewItemSorter = null;
            if (Active)
            {
                if (get_IsEmptyColumn(ci))
                {
                }
                // nothing to do
                else if (get_IsDateColumn(ci))
                {
                    lvw_.ListViewItemSorter = new ListViewSorterDate(ci, ColumnSortOrder);
                }
                else if (get_IsNumericColumn(ci, false))
                {
                    lvw_.ListViewItemSorter = new ListViewSorterDecimal(ci, ColumnSortOrder);
                }
                else
                {
                    lvw_.ListViewItemSorter = new ListViewSorterString(ci, ColumnSortOrder);
                }
            }

            SortOrderIconShow(ci);
            LastColumn = ci;
			if (_frm != null)  _frm.Cursor = cc;
		}

		public bool SortOrderIcon { get; set; } = true;

        public void SortOrderIconShow(int columnIndex)
        {
            if (lvw_.SmallImageList is null | !SortOrderIcon)
            {
                return;
            }

            SortOrderIconRemove();
            if (ColumnSortOrder == SortOrder.Ascending)
            {
                lvw_.Columns[columnIndex].ImageKey = "ascending";
            }
            else
            {
                lvw_.Columns[columnIndex].ImageKey = "descending";
            }
        }

        public void SortOrderIconRemove()
        {
            for (int i = 0, loopTo = lvw_.Columns.Count - 1; i <= loopTo; i++)
            {
                {
                    ColumnHeader withBlock = lvw_.Columns[i];
                    if (withBlock.ImageKey == "ascending" | withBlock.ImageKey == "descending")
                    {
                        lvw_.Columns[i].ImageIndex = -1;
                        lvw_.Columns[i].ImageKey = null;
                        lvw_.Columns[i].TextAlign = HorizontalAlignment.Left;
                    }
                }
            }
        }

        private void lvw_ColumnClick(object sender, ColumnClickEventArgs e)
        {

            if (SortByColumnClick)
            {
                Sort(e.Column);
            }
        }

        public bool get_IsNumericColumn(int columnIndex, bool checkDateBefore)
        {
            if (checkDateBefore)
            {
                if (get_IsDateColumn(columnIndex))
                {
                    return false;
                }
            }

            decimal d = default;
            DateTime d1 = default;
            for (int i = 0, loopTo = lvw_.Items.Count - 1; i <= loopTo; i++)
            {
                string s = null;
                if (columnIndex == 0)
                {
                    s = lvw_.Items[i].Text;
                }
                else if (lvw_.Items[i].SubItems.Count > columnIndex)
                {
                    s = lvw_.Items[i].SubItems[columnIndex].Text;
                }

                if (s != default)
                {
                    if (!decimal.TryParse(s, out d))
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public bool get_IsDateColumn(int columnIndex)
        {
            DateTime d;
            for (int i = 0, loopTo = lvw_.Items.Count - 1; i <= loopTo; i++)
            {
                string s = null;
                if (columnIndex == 0)
                {
                    s = lvw_.Items[i].Text;
                }
                else if (lvw_.Items[i].SubItems.Count > columnIndex)
                {
                    s = lvw_.Items[i].SubItems[columnIndex].Text;
                }

                if (s != default)
                {
                    if (!DateTime.TryParse(s, out d))
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public bool get_IsEmptyColumn(int columnIndex)
        {
            for (int i = 0, loopTo = lvw_.Items.Count - 1; i <= loopTo; i++)
            {
                string s = null;
                if (columnIndex == 0)
                {
                    s = lvw_.Items[i].Text;
                }
                else if (lvw_.Items[i].SubItems.Count > columnIndex)
                {
                    s = lvw_.Items[i].SubItems[columnIndex].Text;
                }

                if (s != default)
                {
                    return false;
                }
            }

            return true;
        }
    }

    public class ListViewSorterString : IComparer
    {
        private readonly int col_;
        private readonly SortOrder sortOrder_;

        public ListViewSorterString(int columnIndex, SortOrder sortOrder)
        {
            col_ = columnIndex;
            sortOrder_ = sortOrder;
        }

        public int Compare(object x, object y)
        {
            string xS = null, yS = null;
            var xItem = (ListViewItem)x;
            var yItem = (ListViewItem)y;

            if (col_ == 0)
            {
                xS = xItem.Text;
                yS = yItem.Text;
            }
            else
            {
                if (xItem.SubItems.Count > col_) xS = xItem.SubItems[col_].Text;
                if (yItem.SubItems.Count > col_) yS = yItem.SubItems[col_].Text;
            }

            int result = string.Compare(xS, yS, StringComparison.CurrentCulture);
            if (result == 0) // If equal, sort by Key
            {
                long xKey = long.TryParse(xItem.Name, out var xLong) ? xLong : 0;
                long yKey = long.TryParse(yItem.Name, out var yLong) ? yLong : 0;
                result = xKey.CompareTo(yKey);
            }

            return sortOrder_ == SortOrder.Ascending ? result : -result;
        }
    }

    public class ListViewSorterDate : IComparer
    {
        private readonly int col_;
        private readonly SortOrder sortOrder_;

        public ListViewSorterDate(int columnIndex, SortOrder sortOrder)
        {
            col_ = columnIndex;
            sortOrder_ = sortOrder;
        }

        public int Compare(object x, object y)
        {
            var xItem = (ListViewItem)x;
            var yItem = (ListViewItem)y;

            DateTime.TryParse(col_ == 0 ? xItem.Text : xItem.SubItems[col_].Text, out var xD);
            DateTime.TryParse(col_ == 0 ? yItem.Text : yItem.SubItems[col_].Text, out var yD);

            int result = DateTime.Compare(xD, yD);
            if (result == 0) // If equal, sort by Key
            {
                long xKey = long.TryParse(xItem.Name, out var xLong) ? xLong : 0;
                long yKey = long.TryParse(yItem.Name, out var yLong) ? yLong : 0;
                result = xKey.CompareTo(yKey);
            }

            return sortOrder_ == SortOrder.Ascending ? result : -result;
        }
    }

    public class ListViewSorterDecimal : IComparer
    {
        private readonly int col_;
        private readonly SortOrder sortOrder_;

        public ListViewSorterDecimal(int columnIndex, SortOrder sortOrder)
        {
            col_ = columnIndex;
            sortOrder_ = sortOrder;
        }

        public int Compare(object x, object y)
        {
            var xItem = (ListViewItem)x;
            var yItem = (ListViewItem)y;

            decimal.TryParse(col_ == 0 ? xItem.Text : xItem.SubItems[col_].Text, out var xD);
            decimal.TryParse(col_ == 0 ? yItem.Text : yItem.SubItems[col_].Text, out var yD);

            int result = decimal.Compare(xD, yD);
            if (result == 0) // If equal, sort by Key
            {
                long xKey = long.TryParse(xItem.Name, out var xLong) ? xLong : 0;
                long yKey = long.TryParse(yItem.Name, out var yLong) ? yLong : 0;
                result = xKey.CompareTo(yKey);
            }

            return sortOrder_ == SortOrder.Ascending ? result : -result;
        }
    }
}