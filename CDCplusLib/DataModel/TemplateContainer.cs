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
using C4ObjectApi.Repository;

namespace CDCplusLib.DataModel
{
    public class TemplateContainer
    {
        public TemplateContainer(CmnFolder f)
        {
            Containers = new Dictionary<long, TemplateContainer>();
            if (f is null)
            {
                Name = "[none]";
                Templates = new Dictionary<long, CmnObject>();
            }
            else
            {
                Name = f.Name;
                foreach (CmnFolder subF in f.GetSubfolders().Values)
                    Containers.Add(subF.Id, new TemplateContainer(subF));
                Templates = f.GetObjects(CmnSession.Versions.Head);
            }
        }

        public string Name { get; }

        public Dictionary<long, CmnObject> Templates { get; private set; }
        public Dictionary<long, TemplateContainer> Containers { get; private set; }
    }
}