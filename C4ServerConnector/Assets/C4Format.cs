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
    public class C4Format
    {
        public string ContentType { get; private set; }
        public string Extension { get; private set; }
        public long? DefaultObjectTypeId { get; private set; }
		public string IndexMode { get; private set; }
		public string Name { get; private set; }
        public long? Id { get; private set; }
        public string LocalizedName { get; set; }
        public C4Format(string name, string extension, string contentType, long? defaultObjectTypeId, string indexType, long? id=null) 
        {
            Name = name;
            Id = id;
            ContentType = contentType;
            Extension = extension;
            DefaultObjectTypeId = defaultObjectTypeId;
            IndexMode= indexType;
        }
        public C4Format(XmlElement assetEl)
        {
            Name = assetEl.SelectSingleNode("name").InnerText;
            Extension = assetEl.SelectSingleNode("extension").InnerText;
            ContentType = assetEl.SelectSingleNode("contentType").InnerText;
			IndexMode = assetEl.SelectSingleNode("indexMode").InnerText;
			XmlNode dotN = assetEl.SelectSingleNode("defaultObjectTypeId");
            if (dotN == null || dotN.InnerText.Length==0) DefaultObjectTypeId = null;
            else DefaultObjectTypeId = long.Parse(dotN.InnerText);
            XmlNode idN = assetEl.SelectSingleNode("id");
            if (idN == null) Id = null;
            else Id = long.Parse(idN.InnerText);
        }
        public void AppendAssetEl(XmlElement parentEl)
        {
            XmlElement assetEl = (XmlElement)parentEl.AppendChild(parentEl.OwnerDocument.CreateElement("format"));
            assetEl.AppendChild(parentEl.OwnerDocument.CreateElement("name")).InnerText = Name;
            assetEl.AppendChild(parentEl.OwnerDocument.CreateElement("extension")).InnerText = Extension;
            assetEl.AppendChild(parentEl.OwnerDocument.CreateElement("contentType")).InnerText = ContentType;
			assetEl.AppendChild(parentEl.OwnerDocument.CreateElement("indexMode")).InnerText = IndexMode;
			assetEl.AppendChild(parentEl.OwnerDocument.CreateElement("defaultObjectTypeId")).InnerText = (DefaultObjectTypeId == null)?"":DefaultObjectTypeId.ToString();
            if (Id != null) assetEl.AppendChild(parentEl.OwnerDocument.CreateElement("id")).InnerText = Id.ToString();
        }
        public override string ToString()
        {
            return LocalizedName == null ? Name : LocalizedName;
        }
    }
}
