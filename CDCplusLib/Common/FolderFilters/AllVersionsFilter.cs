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
using CDCplusLib.Interfaces;
using CDCplusLib.DataModel;
using System.Xml;
using C4ObjectApi.Interfaces;
using C4ObjectApi.Repository;

namespace CDCplusLib.Common.FolderFilters
{
    public class AllVersionsFilter : IFolderFilter
    {
        public override string ToString()
        {
            return Properties.Resources.lblShowAllVersions;
        }
        public string GetKey()
        {
            return "all";
        }
        public bool ShowsAllVersions { get { return true; } }
        public CmnNodeList GetNodeList(CmnFolder parent)
        {
            CmnNodeList result = new CmnNodeList(parent.Session, parent);
            foreach(IRepositoryNode ow in parent.GetSubfolders().Values)
            {
                result.List.Add(ow.Id, ow);
            }
            foreach(IRepositoryNode ow in parent.GetObjects(CmnSession.Versions.All).Values)
            {
                result.List.Add(ow.Id, ow);
            }
            return result;
        }

        public void Init(XmlElement filterEl)
        {
            // nothing to do
        }
    }
}
