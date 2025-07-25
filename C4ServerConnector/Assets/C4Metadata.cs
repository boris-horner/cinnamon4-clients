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
            if(assetEl!=null) foreach(XmlElement msEl in assetEl.SelectNodes("metaset"))
            {
                AppendMetaset(new C4Metaset(msEl));
            }
            //XmlNode idN = assetEl.SelectSingleNode("id");
        }
        public void AppendMetaset(C4Metaset ms)
        {
            if (!MetasetsByTypeId.ContainsKey(ms.TypeId)) MetasetsByTypeId.Add(ms.TypeId, new HashSet<C4Metaset>());
            if(!MetasetsByTypeId[ms.TypeId].Contains(ms)) MetasetsByTypeId[ms.TypeId].Add(ms);
        }
        public override string ToString()
        {
            return "";
        }
    }
}
