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
using C4ObjectApi.Repository;
using CDCplusLib.Interfaces;

namespace CDCplusLib.Messages
{
    public class SetTreeContextSelectionMessage : IClientMessage
    {
        private Dictionary<long, CmnFolder> selectedFolders_;

        public SetTreeContextSelectionMessage()
        {
            SelectedObjects = new Dictionary<long, CmnObject>();
            selectedFolders_ = new Dictionary<long, CmnFolder>();
            Handled = false;
        }

        public ISessionWindowRequestData SessionWindowRequestData { get; set; }
        public Dictionary<long, CmnObject> SelectedObjects { get; set; }

        public Dictionary<long, CmnFolder> SelectedFolders
        {
            get
            {
                return selectedFolders_;
            }

            set
            {
                selectedFolders_ = value;
            }
        }

        public bool Handled { get; set; }
    }
}