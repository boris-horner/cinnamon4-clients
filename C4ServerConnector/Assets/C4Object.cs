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
using System;
using System.Xml;

namespace C4ServerConnector.Assets
{
    public class C4Object : IC4Node
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public long ContentSize { get; set; }
        public string ContentHash { get; set; }
        public long? PredecessorId { get; set; }
        public long RootId { get; set; }
        public long CreatorId { get; set; }
        public long ModifierId { get; set; }
        public long OwnerId { get; set; }
        public long? LockedId { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
        public long LanguageId { get; set; }
        public long AclId { get; set; }
        public long ParentId { get; set; }
        public long? FormatId { get; set; }
        public long TypeId { get; set; }
        public bool LatestHead { get; set; }
        public bool LatestBranch { get; set; }
        public bool ContentChanged { get; set; }
        public bool MetadataChanged { get; set; }
        public string Version { get; set; }
        public long? LifecycleStateId { get; set; }
        public C4Link Link { get; set; }
        public C4Metadata Metadata { get; set; }
        //public Dictionary<string, XmlElement> Metasets { get; set; }
        public XmlElement Summary { get; set; }
        public C4Session.NodeType NodeType { get; private set; }

        public C4Object(XmlElement assetEl)
        {
            NodeType = C4Session.NodeType.Object;
            Id = long.Parse(assetEl.SelectSingleNode("id").InnerText);
            XmlNode chNode = assetEl.SelectSingleNode("contentHash");
            ContentHash = chNode==null?"":chNode.InnerText;
            string contentSize = assetEl.SelectSingleNode("contentSize").InnerText;
            ContentSize = contentSize.Length == 0 ? 0 : long.Parse(contentSize);
            Name = assetEl.SelectSingleNode("name").InnerText;
            string predecessorId = assetEl.SelectSingleNode("predecessorId").InnerText;
            PredecessorId = predecessorId.Length == 0 ? null : long.Parse(predecessorId);
            RootId = long.Parse(assetEl.SelectSingleNode("rootId").InnerText);
            CreatorId = long.Parse(assetEl.SelectSingleNode("creatorId").InnerText);
            ModifierId = long.Parse(assetEl.SelectSingleNode("modifierId").InnerText);
            OwnerId = long.Parse(assetEl.SelectSingleNode("ownerId").InnerText);
            string lockedId = assetEl.SelectSingleNode("lockerId").InnerText;
            LockedId = lockedId.Length == 0 ? null : long.Parse(lockedId);
            Created = DateTime.Parse(assetEl.SelectSingleNode("created").InnerText);
            Modified = DateTime.Parse(assetEl.SelectSingleNode("modified").InnerText);
            LanguageId = long.Parse(assetEl.SelectSingleNode("languageId").InnerText);
            AclId = long.Parse(assetEl.SelectSingleNode("aclId").InnerText);
            ParentId = long.Parse(assetEl.SelectSingleNode("parentId").InnerText);
            string formatId = assetEl.SelectSingleNode("formatId").InnerText;
            FormatId = formatId.Length == 0 ? null : long.Parse(formatId);
            TypeId = long.Parse(assetEl.SelectSingleNode("typeId").InnerText);
            LatestHead = assetEl.SelectSingleNode("latestHead").InnerText=="true";
            LatestBranch = assetEl.SelectSingleNode("latestBranch").InnerText == "true";
            ContentChanged = assetEl.SelectSingleNode("contentChanged").InnerText == "true";
            MetadataChanged = assetEl.SelectSingleNode("metadataChanged").InnerText == "true";
            Version = assetEl.SelectSingleNode("cmnVersion").InnerText;
            string lifecycleStateId = assetEl.SelectSingleNode("lifecycleStateId").InnerText;
            LifecycleStateId = lifecycleStateId.Length == 0 ? null : long.Parse(lifecycleStateId);
            XmlDocument summary=new XmlDocument();
            string summaryText = assetEl.SelectSingleNode("summary").InnerText.Trim();
            if (summaryText.Length > 0)
            {
                summary.LoadXml(summaryText);
                Summary = summary.DocumentElement;
            }
            else Summary = null;

            Metadata = new C4Metadata();
            foreach(XmlElement msEl in assetEl.SelectNodes("metasets/metaset"))
            {
                C4Metaset ms = new C4Metaset(msEl);
                if (!Metadata.MetasetsByTypeId.ContainsKey(ms.TypeId)) 
                    Metadata.MetasetsByTypeId.Add(ms.TypeId, new System.Collections.Generic.HashSet<C4Metaset>());
                Metadata.MetasetsByTypeId[ms.TypeId].Add(ms);
            }
        }
        public override string ToString()
        {
            return Name;
        }
    }
}
