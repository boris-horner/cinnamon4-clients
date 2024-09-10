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
using System.Xml;

namespace C4ServerConnector.Assets
{
    public class C4MetasetType
    {
        public string Name { get; private set; }
        public bool Unique { get; private set; }
        public long? Id { get; private set; }
        public C4MetasetType(string name, bool unique, long? id=null) 
        {
            Name = name;
            Unique = unique;
            Id = id;
        }
        public C4MetasetType(XmlElement assetEl)
        {
            Name = assetEl.SelectSingleNode("name").InnerText;
            XmlNode idN = assetEl.SelectSingleNode("id");
            Unique = assetEl.SelectSingleNode("unique").InnerText == "true";
            if (idN == null) Id = null;
            else Id = long.Parse(idN.InnerText);
        }
        public void AppendAssetEl(XmlElement parentEl)
        {
            XmlElement assetEl = (XmlElement)parentEl.AppendChild(parentEl.OwnerDocument.CreateElement("metasetType"));
            assetEl.AppendChild(parentEl.OwnerDocument.CreateElement("name")).InnerText = Name;
            assetEl.AppendChild(parentEl.OwnerDocument.CreateElement("unique")).InnerText = Unique?"true":"false";
            if (Id != null) assetEl.AppendChild(assetEl.OwnerDocument.CreateElement("id")).InnerText = Id.ToString();
        }
        public override string ToString()
        {
            return Name;
        }
    }
}