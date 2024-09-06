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
    public class KeyEvent
    {
        public enum KeyEventSelection
        {
            ObjectSelected,
            FolderSelected,
            ListSelected,
            NothingSelected
        }

        private readonly bool alt_;
        private readonly KeyEventSelection filter_;

        public KeyEvent(Keys key, bool shift, bool ctrl, bool alt, KeyEventSelection filter)
        {
            Key = key;
            Shift = shift;
            Ctrl = ctrl;
            alt_ = alt;
            filter_ = filter;
        }

        public bool Shift { get; private set; }
        public bool Ctrl { get; private set; }
        public Keys Key { get; private set; }

        public KeyEventSelection Filter
        {
            get
            {
                return filter_;
            }
        }

        public override string ToString()
        {
            string result = Key.ToString() + ":" + (Shift ? "t" : "f") + (Ctrl ? "t" : "f") + (alt_ ? "t" : "f");


            switch (filter_)
            {
                case KeyEventSelection.ObjectSelected:
                    {
                        result += "o";
                        break;
                    }

                case KeyEventSelection.FolderSelected:
                    {
                        result += "f";
                        break;
                    }

                case KeyEventSelection.ListSelected:
                    {
                        result += "l";
                        break;
                    }

                case KeyEventSelection.NothingSelected:
                    {
                        result += "n";
                        break;
                    }
            }

            return result;
        }
    }
}