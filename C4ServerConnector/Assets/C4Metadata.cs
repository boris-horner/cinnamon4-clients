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
using System.Collections.Generic;
using System.Xml;

namespace C4ServerConnector.Assets
{
    public class C4Metadata
    {
        //private XmlDocument _legacyXml;
        public Dictionary<long, HashSet<C4Metaset>> MetasetsByTypeId { get; private set; }
        public C4Metadata() 
        {
            MetasetsByTypeId = new Dictionary<long, HashSet<C4Metaset>>();
        }
        public C4Metadata(XmlElement assetEl)
        {
            MetasetsByTypeId = new Dictionary<long, HashSet<C4Metaset>>();
            foreach(XmlElement msEl in assetEl.SelectNodes("metaset"))
            {
                AppendMetaset(new C4Metaset(msEl));
            }
            //XmlNode idN = assetEl.SelectSingleNode("id");
        }
        //public C4Metadata(long objId, XmlDocument legacyXml, Dictionary<string, C4MetasetType> msTypesByName)
        //{
        //    _legacyXml= legacyXml;
        //    MetasetsByTypeId = new Dictionary<long, HashSet<C4Metaset>>();
        //    foreach (XmlElement msEl in legacyXml.DocumentElement.SelectNodes("metaset"))
        //    {
        //        C4MetasetType msType = msTypesByName[msEl.GetAttribute("type")];
        //        if (!MetasetsByTypeId.ContainsKey((long)msType.Id)) MetasetsByTypeId.Add((long)msType.Id, new HashSet<C4Metaset>());
        //        C4Metaset ms = new C4Metaset((long)msType.Id, objId, msEl);
        //        if (!MetasetsByTypeId[(long)msType.Id].Contains(ms)) MetasetsByTypeId[ms.TypeId].Add(ms);
        //    }
        //}

        //public void AppendAssetEl(XmlElement parentEl)
        //{
        //    XmlElement assetEl = (XmlElement)parentEl.AppendChild(parentEl.OwnerDocument.CreateElement("metaset"));
        //    foreach (long typeId in MetasetsByTypeId.Keys) foreach (C4Metaset ms in MetasetsByTypeId[typeId]) ms.AppendAssetEl(assetEl);
        //}
        public void AppendMetaset(C4Metaset ms)
        {
            if (!MetasetsByTypeId.ContainsKey(ms.TypeId)) MetasetsByTypeId.Add(ms.TypeId, new HashSet<C4Metaset>());
            if(!MetasetsByTypeId[ms.TypeId].Contains(ms)) MetasetsByTypeId[ms.TypeId].Add(ms);
        }
        [Obsolete("The LegacyXml property is temporary until all client code using the legacy Metadata property are ported to the new logic.")]
        //public XmlDocument LegacyXml
        //{
        //    get
        //    {
        //        if(_legacyXml==null)
        //        {
        //            _legacyXml = new XmlDocument();
        //            _legacyXml.AppendChild(_legacyXml.CreateElement("meta"));
        //            foreach(long typeId in MetasetsByTypeId.Keys)
        //                foreach(C4Metaset ms in MetasetsByTypeId[typeId])
        //                    _legacyXml.DocumentElement.AppendChild(_legacyXml.ImportNode(ms.Content.CloneNode(true), true));
        //        }
        //        return _legacyXml;
        //    }
        //}
        public override string ToString()
        {
            return "";
        }
    }
}
