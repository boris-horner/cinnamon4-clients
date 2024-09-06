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

namespace CDCplusLib.Common.KeyboardEvents
{
    public class KeyEventReaction
    {
        public enum EventTypes
        {
            Refresh,
            SelectAll,
            ExecuteMethod,
            NullEvent // this is used if an event was initially caught, but later the event got abandoned (conditions not fulfilled)
        }

        private readonly KeyEvent associatedKeyEvent_;

        public KeyEventReaction(KeyEvent associatedKeyEvent, EventTypes eventType, string assembly = null, string type = null)


        {
            associatedKeyEvent_ = associatedKeyEvent;
            EventType = eventType;
            if (eventType == EventTypes.ExecuteMethod)
            {
                Assembly = assembly;
                Type = type;
            }
        }

        public string Assembly { get; private set; }
        public string Type { get; private set; }
        public EventTypes EventType { get; private set; }

        public KeyEvent AssociatedKeyEvent
        {
            get
            {
                return associatedKeyEvent_;
            }
        }
    }
}