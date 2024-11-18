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
using C4ObjectApi.Repository;
using C4ServerConnector.Assets;

namespace CDCplusLib.DataModel
{
    public class RelationDescriptor
    {
        public RelationDescriptor(CmnObject leftObject, CmnObject rightObject, C4RelationType relType, XmlDocument metadata, C4Relation rel = null) 
        {
            LeftObject = leftObject;
            RightObject = rightObject;
            RelationType = relType;
            Relation = rel;
            Metadata = metadata;
        }

        public string Key 
        { 
            get 
            {
                if (Relation != null) return Relation.LeftId.ToString() + "_" + Relation.RightId.ToString() + "_" + RelationType.Name;
                return LeftObject.Id.ToString() + "_" + RightObject.Id.ToString() + "_" + RelationType.Name;
            }
        }
        public CmnObject LeftObject { get; }
        public CmnObject RightObject { get; }
        public C4RelationType RelationType { get; }
        public C4Relation Relation { get; }
        public XmlDocument Metadata { get; }

    }
}
