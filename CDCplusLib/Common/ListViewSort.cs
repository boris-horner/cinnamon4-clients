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
        private ListViewItem xItem_;
        private ListViewItem yItem_;

        public ListViewSorterString(int columnIndex, SortOrder sortOrder)
        {
            col_ = columnIndex;
            sortOrder_ = sortOrder;
        }

        public int Compare(object x, object y)
        {
            string xS = null;
            string yS = null;
            xItem_ = (ListViewItem)x;
            yItem_ = (ListViewItem)y;
            if (col_ == 0)
            {
                xS = xItem_.Text;
                yS = yItem_.Text;
            }
            else
            {
                if (xItem_.SubItems.Count > col_)
                {
                    xS = xItem_.SubItems[col_].Text;
                }

                if (yItem_.SubItems.Count > col_)
                {
                    yS = yItem_.SubItems[col_].Text;
                }
            }

            if (sortOrder_ == SortOrder.Ascending)
            {
                return string.Compare(xS, yS);
            }
            else
            {
                return string.Compare(yS, xS);
            }
        }
    }

    public class ListViewSorterDate : IComparer
    {
        private readonly int col_;
        private readonly SortOrder sortOrder_;
        private ListViewItem xItem_;
        private ListViewItem yItem_;

        public ListViewSorterDate(int columnIndex, SortOrder sortOrder)
        {
            col_ = columnIndex;
            sortOrder_ = sortOrder;
        }

        public int Compare(object x, object y)
        {
            DateTime xD = default;
            DateTime yD = default;
            xItem_ = (ListViewItem)x;
            yItem_ = (ListViewItem)y;
            if (col_ == 0)
            {
                DateTime.TryParse(xItem_.Text, out xD);
                DateTime.TryParse(yItem_.Text, out yD);
            }
            else
            {
                if (xItem_.SubItems.Count > col_)
                {
                    DateTime.TryParse(xItem_.SubItems[col_].Text, out xD);
                }

                if (yItem_.SubItems.Count > col_)
                {
                    DateTime.TryParse(yItem_.SubItems[col_].Text, out yD);
                }
            }

            if (sortOrder_ == SortOrder.Ascending)
            {
                return DateTime.Compare(xD, yD);
            }
            else
            {
                return DateTime.Compare(yD, xD);
            }
        }
    }

    public class ListViewSorterDecimal : IComparer
    {
        private int col_;
        private readonly SortOrder sortOrder_;
        private ListViewItem xItem_;
        private ListViewItem yItem_;

        public ListViewSorterDecimal(int columnIndex, SortOrder sortOrder)
        {
            col_ = columnIndex;
            sortOrder_ = sortOrder;
        }

        public int Compare(object x, object y)
        {
            decimal xD = default;
            decimal yD = default;
            xItem_ = (ListViewItem)x;
            yItem_ = (ListViewItem)y;
            if (yItem_.SubItems.Count <= col_)
            {
                col_ = 0;
            }

            if (col_ == 0)
            {
                decimal.TryParse(xItem_.Text, out xD);
                decimal.TryParse(yItem_.Text, out yD);
            }
            else
            {
                if (xItem_.SubItems.Count >= col_)
                {
                    try
                    {
                        decimal.TryParse(xItem_.SubItems[col_].Text, out xD);
                    }
                    catch (Exception ex)
                    {
                        xD = 0m;
                    }
                }

                if (yItem_.SubItems.Count >= col_)
                {
                    try
                    {
                        decimal.TryParse(yItem_.SubItems[col_].Text, out yD);
                    }
                    catch (Exception ex)
                    {
                        yD = 0m;
                    }
                }
            }

            if (sortOrder_ == SortOrder.Ascending)
            {
                return decimal.Compare(xD, yD);
            }
            else
            {
                return decimal.Compare(yD, xD);
            }
        }
    }
}