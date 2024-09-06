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
using C4ServerConnector.Assets;
using System.Xml;

namespace C4ServerConnector
{
    public interface IC4Node
    {
        C4Session.NodeType NodeType { get; }
        string Name { get; set; }
        long Id { get; }
        long OwnerId { get; set; }
        long AclId { get; set; }
        long ParentId { get; set; }
        long TypeId { get; set; }
        bool MetadataChanged { get; set; }
        C4Metadata Metadata { get; set; }
        //Dictionary<string, XmlElement> Metasets { get; set; }
        XmlElement Summary { get; set; }
        //void AppendAssetEl(XmlElement assetEl);
    }
}
