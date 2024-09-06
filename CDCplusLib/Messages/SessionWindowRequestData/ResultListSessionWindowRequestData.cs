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
// the License.using CDCplusLib.Interfaces;
using CDCplusLib.Helpers;
using C4ObjectApi.Interfaces;
using C4ObjectApi.Repository;
using CDCplusLib.Interfaces;

namespace CDCplusLib.Messages.SessionWindowRequestData
{
    public class ResultListSessionWindowRequestData : ISessionWindowRequestData
    {
        private string resultListTitle_;

        public ResultListSessionWindowRequestData()
        {
            SelectedObjects = new Dictionary<long, CmnObject>();
            SelectedFolders = new Dictionary<long, CmnFolder>();
            ResultListObjects = new Dictionary<long, CmnObject>();
            ResultListFolders = new Dictionary<long, CmnFolder>();
        }

        public Dictionary<long, CmnObject> SelectedObjects { get; set; }
        public Dictionary<long, CmnFolder> SelectedFolders { get; set; }
        public Dictionary<long, CmnObject> ResultListObjects { get; set; }
        public Dictionary<long, CmnFolder> ResultListFolders { get; set; }
        public Dictionary<long, IRepositoryNode> ResultList 
        { 
            get 
            {
                return ListsHelper.ConvertToOwnables(ResultListObjects, ResultListFolders);
            } 
        }


        public string ResultListTitle
        {
            get
            {
                return resultListTitle_;
            }

            set
            {
                resultListTitle_ = value;
            }
        }
    }
}