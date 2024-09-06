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
// the License.using C4ObjectApi.Interfaces;
using C4ObjectApi.Interfaces;
using CDCplusLib.Interfaces;

namespace CDCplusLib.Messages
{

    // TODO: rename me to RequestFunctionMessage or similar
    public class KeyPressedMessage : IClientMessage
    {
        public enum FunctionTypes
        {
            ObjectFunction,
            FolderFunction,
            SessionFunction,
            ListFunction
        }

        private bool handled_;

        public KeyPressedMessage()
        {
            ListSelection = new Dictionary<long, IRepositoryNode>();
            handled_ = false;
        }

        public Dictionary<long, IRepositoryNode> ListSelection { get; set; }
        public string Assembly { get; set; }
        public string Type { get; set; }
        public FunctionTypes FunctionType { get; set; }

        public bool Handled
        {
            get
            {
                return handled_;
            }

            set
            {
                handled_ = value;
            }
        }
    }
}