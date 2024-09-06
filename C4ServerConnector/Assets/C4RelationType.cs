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
    public class C4RelationType
    {
        public string Name { get; private set; }
        public long? Id { get; private set; }
        public string LocalizedName { get; set; }
        public bool LeftObjectProtected { get; private set; }
        public bool RightObjectProtected { get; private set; }
        public bool CloneOnLeftCopy { get; private set; }
        public bool CloneOnRightCopy { get; private set; }
        public bool CloneOnLeftVersion { get; private set; }
        public bool CloneOnRightVersion { get; private set; }
        public C4RelationType(string name, bool leftObjectProtected, bool rightObjectProtected, bool cloneOnLeftCopy, bool cloneOnRightCopy, bool cloneOnLeftVersion, bool cloneOnRightVersion, long? id=null) 
        {
            Name = name;
            Id = id;
            LeftObjectProtected = leftObjectProtected;
            RightObjectProtected = rightObjectProtected;
            CloneOnLeftCopy = cloneOnLeftCopy;
            CloneOnRightCopy = cloneOnRightCopy;
            CloneOnLeftVersion = cloneOnLeftVersion;
            CloneOnRightVersion = cloneOnRightVersion;
        }
        public C4RelationType(XmlElement assetEl)
        {
            Name = assetEl.SelectSingleNode("name").InnerText;
            LeftObjectProtected = assetEl.SelectSingleNode("leftObjectProtected").InnerText == "true";
            RightObjectProtected = assetEl.SelectSingleNode("rightObjectProtected").InnerText == "true";
            CloneOnLeftCopy = assetEl.SelectSingleNode("cloneOnLeftCopy").InnerText == "true";
            CloneOnRightCopy = assetEl.SelectSingleNode("cloneOnRightCopy").InnerText == "true";
            CloneOnLeftVersion = assetEl.SelectSingleNode("cloneOnLeftVersion").InnerText == "true";
            CloneOnRightVersion = assetEl.SelectSingleNode("cloneOnRightVersion").InnerText == "true";
            XmlNode idN = assetEl.SelectSingleNode("id");
            if (idN == null) Id = null;
            else Id = long.Parse(idN.InnerText);
        }
        public void AppendAssetEl(XmlElement parentEl)
        {
            XmlElement relationTypeEl = (XmlElement)parentEl.AppendChild(parentEl.OwnerDocument.CreateElement("relationType"));
            relationTypeEl.AppendChild(parentEl.OwnerDocument.CreateElement("name")).InnerText = Name;
            if (Id != null) relationTypeEl.AppendChild(parentEl.OwnerDocument.CreateElement("id")).InnerText = Id.ToString();
            relationTypeEl.AppendChild(parentEl.OwnerDocument.CreateElement("leftObjectProtected")).InnerText = LeftObjectProtected ? "true" : "false";
            relationTypeEl.AppendChild(parentEl.OwnerDocument.CreateElement("rightObjectProtected")).InnerText = RightObjectProtected ? "true" : "false";
            relationTypeEl.AppendChild(parentEl.OwnerDocument.CreateElement("cloneOnLeftCopy")).InnerText = CloneOnLeftCopy ? "true" : "false";
            relationTypeEl.AppendChild(parentEl.OwnerDocument.CreateElement("cloneOnRightCopy")).InnerText = CloneOnRightCopy ? "true" : "false";
            relationTypeEl.AppendChild(parentEl.OwnerDocument.CreateElement("cloneOnLeftVersion")).InnerText = CloneOnLeftVersion ? "true" : "false";
            relationTypeEl.AppendChild(parentEl.OwnerDocument.CreateElement("cloneOnRightVersion")).InnerText = CloneOnRightVersion ? "true" : "false";
        }
        public override string ToString()
        {
            return LocalizedName == null ? Name : LocalizedName;
        }
    }
}
