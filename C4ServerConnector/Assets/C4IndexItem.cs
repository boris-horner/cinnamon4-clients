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

namespace C4ServerConnector.Assets
{
    public class C4IndexItem
    {
        public enum IndexDataTypes
        {
            IndexBoolean,
            IndexDateTime,
            IndexInteger,
            IndexString,
            IndexDecimal,
            IndexText
        }
        public string Name { get; private set; }
        public string FieldName { get; private set; }
        public string SearchString { get; private set; }
        public string SearchCondition { get; private set; }
        public string IndexTypeName { get; private set; }
        public bool MultipleResults { get; private set; }
        public bool StoreField { get; private set; }
        public IndexDataTypes DataType { get; private set; }
        //public XmlDocument ValueAssistance { get; private set; }
        public string LocalizedName { get; set; }
        public long? Id { get; private set; }
        public C4IndexItem(string name,
                           string fieldName,
                           string searchString,
                           string searchCondition,
                           string indexTypeName,
                           bool multipleResults,
                           bool storeField,
                           long? id = null)
        {
            Name = name;
            FieldName = fieldName;
            SearchString = searchString;
            SearchCondition = searchCondition;
            IndexTypeName = indexTypeName;
            MultipleResults = multipleResults;
            StoreField = storeField;
            SetDataType(IndexTypeName);
            Id = id;
        }
        public C4IndexItem(XmlElement assetEl)
        {
            Name = assetEl.SelectSingleNode("name").InnerText;
            FieldName = assetEl.SelectSingleNode("fieldName").InnerText;
            SearchString = assetEl.SelectSingleNode("searchString").InnerText;
            SearchCondition = assetEl.SelectSingleNode("searchCondition").InnerText;
            IndexTypeName = assetEl.SelectSingleNode("indexType").InnerText;
            MultipleResults = assetEl.SelectSingleNode("multipleResults").InnerText == "true";
            StoreField = assetEl.SelectSingleNode("storeField").InnerText == "true";
            //ValueAssistance = new XmlDocument();
            //ValueAssistance.LoadXml(assetEl.SelectSingleNode("vaParams").InnerText);
            XmlNode idN = assetEl.SelectSingleNode("id");
            if (idN == null) Id = null;
            else Id = long.Parse(idN.InnerText);
            SetDataType(IndexTypeName);
        }
        public void AppendAssetEl(XmlElement parentEl)
        {
            XmlElement assetEl = (XmlElement)parentEl.AppendChild(parentEl.OwnerDocument.CreateElement("indexItem"));
            if (Id != null) assetEl.AppendChild(assetEl.OwnerDocument.CreateElement("id")).InnerText = Id.ToString();
            assetEl.AppendChild(parentEl.OwnerDocument.CreateElement("name")).InnerText = Name;
            assetEl.AppendChild(parentEl.OwnerDocument.CreateElement("fieldName")).InnerText = FieldName;
            assetEl.AppendChild(parentEl.OwnerDocument.CreateElement("searchString")).InnerText = SearchString;
            assetEl.AppendChild(parentEl.OwnerDocument.CreateElement("searchCondition")).InnerText = SearchCondition;
            assetEl.AppendChild(parentEl.OwnerDocument.CreateElement("indexType")).InnerText = IndexTypeName;
            assetEl.AppendChild(parentEl.OwnerDocument.CreateElement("multipleResults")).InnerText = MultipleResults ? "true" : "false";
            assetEl.AppendChild(parentEl.OwnerDocument.CreateElement("storeField")).InnerText = StoreField ? "true" : "false";
        }
        private void SetDataType(string indexTypeName) 
        {
            switch(indexTypeName)
            {
                case "DEFAULT_STRING_INDEXER": DataType = IndexDataTypes.IndexString; break; 
            }
        }
        public override string ToString()
        {
            return LocalizedName == null ? Name : LocalizedName;
        }
    }
}
