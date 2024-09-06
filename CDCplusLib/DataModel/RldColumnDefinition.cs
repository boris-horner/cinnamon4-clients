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
using System.Xml;
using CDCplusLib.Interfaces;
using C4ObjectApi.Repository;

namespace CDCplusLib.DataModel
{
    public class RldColumnDefinition
    {
        public enum SortOptions { None, SortAscending, SortDescending}
        public readonly bool CustomField;
        public readonly string ColumnType;
        public SortOptions SortOption;
        public int Width;
        private readonly string _label;
        public RldColumnDefinition(XmlElement columnEl, CmnSession s, INodeDataProvider ndp)
        {
            ColumnType = columnEl.GetAttribute("type");
            CustomField = (columnEl.GetAttribute("class") == "custom");
            Width = int.Parse(columnEl.GetAttribute("width"));
            switch(columnEl.GetAttribute("sort"))
            {
                case "": SortOption = SortOptions.None; break;
                case "ascending": SortOption = SortOptions.SortAscending; break;
                case "descending": SortOption = SortOptions.SortDescending; break;
            }
            _label = CustomField ? s.SessionConfig.C4Sc.GetLocalizedLabel("listview_custom_column." + ColumnType, "other") : ndp.GetFieldTitle(ColumnType);
        }
        public RldColumnDefinition(string columnType, int width, SortOptions sortOption, bool customField, CmnSession s, INodeDataProvider ndp)
        {
            ColumnType = columnType;
            CustomField = customField;
            Width = width;
            SortOption = sortOption;
            _label = CustomField ? s.SessionConfig.C4Sc.GetLocalizedLabel("listview_custom_column." + ColumnType, "other") : ndp.GetFieldTitle(ColumnType);
        }
        public override string ToString()
        {
            return _label;
        }

        public void AppendXmlSerialization(XmlElement parentEl)
        {
            XmlElement lvcEl = (XmlElement)parentEl.AppendChild(parentEl.OwnerDocument.CreateElement("list_view_column"));
            lvcEl.SetAttribute("type", ColumnType);
            lvcEl.SetAttribute("width", Width.ToString());
            if (SortOption != SortOptions.None) lvcEl.SetAttribute("sort", SortOption == SortOptions.SortAscending ? "ascending" : "descending");
            if(CustomField) lvcEl.SetAttribute("class", "custom");
        }
    }
}
