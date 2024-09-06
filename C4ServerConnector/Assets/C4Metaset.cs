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
    public class C4Metaset
    {
        public long? Id { get; private set; }
        public XmlElement Content { get; set; }
        public long TypeId { get; set; }
        public long ObjectId { get; set; }

        public C4Metaset(long typeId, long objectId, XmlElement content, long? id=null) 
        {
            TypeId=typeId;
            ObjectId=objectId;
            Content = content;
            Id = id;
        }
        public C4Metaset(XmlElement assetEl)
        {
            XmlNode idN = assetEl.SelectSingleNode("id");
            if (idN == null) Id = null;
            else Id = long.Parse(idN.InnerText);
            TypeId = long.Parse(assetEl.SelectSingleNode("typeId").InnerText);
            ObjectId = long.Parse(assetEl.SelectSingleNode("objectId").InnerText);
            XmlDocument content=new XmlDocument();
            content.LoadXml(assetEl.SelectSingleNode("content").InnerText);
            Content = (XmlElement)content.DocumentElement;
        }
        public void AppendAssetEl(XmlElement parentEl)
        {
            XmlElement assetEl = (XmlElement)parentEl.AppendChild(parentEl.OwnerDocument.CreateElement("metaset"));
            if (Id != null) assetEl.AppendChild(assetEl.OwnerDocument.CreateElement("id")).InnerText = Id.ToString();
            assetEl.AppendChild(assetEl.OwnerDocument.CreateElement("typeId")).InnerText = TypeId.ToString();
            assetEl.AppendChild(assetEl.OwnerDocument.CreateElement("objectId")).InnerText = ObjectId.ToString();
            assetEl.AppendChild(assetEl.OwnerDocument.ImportNode(Content.CloneNode(true),true));
        }
        public override string ToString()
        {
            return Id==null?"(null)":Id.ToString();
        }
    }
}
