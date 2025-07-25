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
    public class C4Relation
    {
        public long TypeId { get; set; }
        public long? Id { get; private set; }
        public long LeftId { get; set; }
        public long RightId { get; set; }
        public XmlDocument Metadata { get; set; }
        public C4Relation(long typeId, long leftId, long rightId, XmlDocument metadata, long? id=null) 
        {
            Id = id;
            TypeId = typeId;
            LeftId = leftId;
            RightId = rightId;
            if (metadata == null) Metadata = null;
            else
            {
                Metadata=new XmlDocument();
                Metadata.LoadXml(metadata.DocumentElement.OuterXml);
            }
        }
        public C4Relation(XmlElement assetEl)
        {
            TypeId = long.Parse(assetEl.SelectSingleNode("typeId").InnerText);
            LeftId = long.Parse(assetEl.SelectSingleNode("leftId").InnerText);
            RightId = long.Parse(assetEl.SelectSingleNode("rightId").InnerText);
            Metadata = new XmlDocument();
            string xmlContent = assetEl.SelectSingleNode("metadata").InnerText;
            if(xmlContent.Trim().Length==0) Metadata = null;
            else Metadata.LoadXml(xmlContent);
            XmlNode idN = assetEl.SelectSingleNode("id");
            if (idN == null) Id = null;
            else Id = long.Parse(idN.InnerText);
        }
        public void AppendAssetEl(XmlElement parentEl)
        {
            XmlElement assetEl = (XmlElement)parentEl.AppendChild(parentEl.OwnerDocument.CreateElement("relation"));
            assetEl.AppendChild(parentEl.OwnerDocument.CreateElement("typeId")).InnerText = TypeId.ToString();
            assetEl.AppendChild(parentEl.OwnerDocument.CreateElement("leftId")).InnerText = LeftId.ToString();
            assetEl.AppendChild(parentEl.OwnerDocument.CreateElement("rightId")).InnerText = RightId.ToString();
            XmlNode mdNode = assetEl.AppendChild(parentEl.OwnerDocument.CreateElement("metadata"));
            if (Metadata == null) mdNode.InnerText = "<metadata/>";
            else mdNode.InnerText = Metadata.DocumentElement.OuterXml;
            assetEl.AppendChild(assetEl.OwnerDocument.CreateElement("id")).InnerText = Id==null?"":Id.ToString();
            //if(Id!=null) assetEl.AppendChild(assetEl.OwnerDocument.CreateElement("id")).InnerText = Id.ToString();
        }
        public override string ToString()
        {
            return string.Concat("Type:",TypeId," Left:",LeftId," Right:",RightId);
        }
    }
}
