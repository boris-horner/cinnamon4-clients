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
using C4ObjectApi.Interfaces;
using CDCplusLib.Interfaces;

namespace CDCplusLib.Messages
{
    public class ObjectsModifiedMessage : IClientMessage
    {
        public enum ModificationTypes
        {
            CheckedOut,
            CheckedIn,
            CheckoutCancelled,
            CustomMetadataChanged,
            SystemMetadataChanged,
            LifecycleChanged,
            Unspecified
        }

        public ObjectsModifiedMessage()
        {
            ModifiedObjects = new Dictionary<long, IRepositoryNode>();
            ModificationType = ModificationTypes.Unspecified;
            Handled = false;
        }

        public Dictionary<long, IRepositoryNode> ModifiedObjects { get; set; } // can be CmnObject or CmnFolder
        public ModificationTypes ModificationType { get; set; }
        public bool Handled { get; set; }
    }
}