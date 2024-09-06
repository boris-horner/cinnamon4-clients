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
using C4GeneralGui.GuiElements;
using C4ObjectApi.Interfaces;
using C4ObjectApi.Repository;
using CDCplusLib.Interfaces;
using CDCplusLib.Messages;
using System.Xml;

namespace CDCplusLib.Common.KeyboardEvents
{
    public class KeyEventTable
    {
        public event IGenericControl.MessageSentEventHandler MessageSent;
        private readonly Dictionary<KeyEvent, KeyEventReaction> _keyEventDefinitions;

        public KeyEventTable(XmlElement configParentNode)
        {
            _keyEventDefinitions = new Dictionary<KeyEvent, KeyEventReaction>(new KeyEventComparer());
            foreach (XmlElement keyColEl in configParentNode.SelectNodes("key_event"))
            {
                KeyEvent.KeyEventSelection filter;
                switch (keyColEl.GetAttribute("selection_filter").ToLower() ?? "")
                {
                    case "folder":
                        {
                            filter = KeyEvent.KeyEventSelection.FolderSelected;
                            break;
                        }

                    case "list":
                        {
                            filter = KeyEvent.KeyEventSelection.ListSelected;
                            break;
                        }

                    case "object":
                        {
                            filter = KeyEvent.KeyEventSelection.ObjectSelected;
                            break;
                        }

                    case "nothing":
                        {
                            filter = KeyEvent.KeyEventSelection.NothingSelected;
                            break;
                        }

                    default:
                        {
                            throw new ApplicationException("Invalid key_event/@selection_filter value: " + keyColEl.GetAttribute("selection_filter"));
                        }
                }

                KeyEvent ke = new KeyEvent(GetKeyCode(keyColEl.GetAttribute("key")), keyColEl.GetAttribute("shift").ToLower() == "true" ? true : false, keyColEl.GetAttribute("ctrl").ToLower() == "true" ? true : false, keyColEl.GetAttribute("alt").ToLower() == "true" ? true : false, filter);



                KeyEventReaction ker;
                switch (keyColEl.GetAttribute("event_type").ToLower() ?? "")
                {
                    case "refresh":
                        {
                            ker = new KeyEventReaction(ke, KeyEventReaction.EventTypes.Refresh);
                            break;
                        }

                    case "select_all":
                        {
                            ker = new KeyEventReaction(ke, KeyEventReaction.EventTypes.SelectAll);
                            break;
                        }

                    case "execute_method":
                        {
                            if (keyColEl.GetAttribute("type").Length == 0 | keyColEl.GetAttribute("assembly").Length == 0)
                            {
                                throw new ApplicationException("key_event/@type and key_event/@assembly must be defined if @event_type is empty or execute_method");
                            }

                            ker = new KeyEventReaction(ke, KeyEventReaction.EventTypes.ExecuteMethod, keyColEl.GetAttribute("assembly"), keyColEl.GetAttribute("type"));


                            break;
                        }

                    default:
                        {
                            throw new ApplicationException("Invalid key_event/@event_type value: " + keyColEl.GetAttribute("selection_filter"));
                        }
                }

                _keyEventDefinitions.Add(ke, ker);
            }
        }

        public void PerformKeyReaction(Dictionary<long, IRepositoryNode> selection, Keys key, bool shift, bool ctrl, bool alt, CmnFolder context)
        {
            switch (key)
            {
                // Ignore these
                case Keys.Up: break;
                case Keys.Down: break;
                case Keys.Space: break;
                case Keys.ShiftKey: break;
                case Keys.Menu: break;// this is the ALT key
                case Keys.ControlKey: break;
                default:
                    // handle configured function
                    bool useContext = false;
                    KeyEvent ke = default(KeyEvent);
                    if (selection.Count > 0)
                    {
                        if (selection.Count == 1)
                        {
                            if (selection.Values.First().GetType() == typeof(CmnFolder)) ke = new KeyEvent(key, shift, ctrl, alt, KeyEvent.KeyEventSelection.FolderSelected);
                            else ke = new KeyEvent(key, shift, ctrl, alt, KeyEvent.KeyEventSelection.ObjectSelected);
                        }
                        else ke = new KeyEvent(key, shift, ctrl, alt, KeyEvent.KeyEventSelection.ListSelected);
                    }
                    else
                    {
                        if (context == null) ke = new KeyEvent(key, shift, ctrl, alt, KeyEvent.KeyEventSelection.NothingSelected);
                        else
                        {
                            ke = new KeyEvent(key, shift, ctrl, alt, KeyEvent.KeyEventSelection.FolderSelected);    // parent folder
                            useContext = true;
                        }
                    }

                    try
                    {
                        KeyEventReaction ker = GetKeyEventReaction(ke);
                        switch (ker.EventType)
                        {
                            case KeyEventReaction.EventTypes.ExecuteMethod:
                                // send message to Session Window to execute matching function
                                KeyPressedMessage kpMsg = new KeyPressedMessage();
                                switch (ke.Filter)
                                {
                                    case KeyEvent.KeyEventSelection.FolderSelected:
                                        kpMsg.FunctionType = KeyPressedMessage.FunctionTypes.FolderFunction;
                                        break;
                                    case KeyEvent.KeyEventSelection.ObjectSelected:
                                        kpMsg.FunctionType = KeyPressedMessage.FunctionTypes.ObjectFunction;
                                        break;
                                    case KeyEvent.KeyEventSelection.ListSelected:
                                        kpMsg.FunctionType = KeyPressedMessage.FunctionTypes.ListFunction;
                                        break;
                                    default:
                                        throw new ApplicationException("Invalid function type for no selection");
                                }
                                if (useContext)
                                {
                                    kpMsg.ListSelection.Add(context.Id, context);
                                }
                                else
                                {
                                    kpMsg.ListSelection = selection;
                                }

                                kpMsg.Assembly = ker.Assembly;
                                kpMsg.Type = ker.Type;
                                MessageSent?.Invoke(kpMsg);

                                break;
                            case KeyEventReaction.EventTypes.Refresh:
                                TreeNodeRefreshRequestMessage tnrrMsg = new TreeNodeRefreshRequestMessage();
                                //msg.Source = EVENT_SOURCE
                                MessageSent?.Invoke(tnrrMsg);

                                break;
                            // TODO: add this when using the function for ResultListDisplay as well
                            //case KeyEventReaction.EventTypes.SelectAll:
                            //    foreach (ListViewItem lvi in lvwNodeList.Items) lvi.Selected = true;
                            //    break;
                            default:
                                System.Diagnostics.Debug.Print("Unsupported key was pressed");
                                break;
                        }
                    }
                    catch (ApplicationException ex)
                    {
                        StandardMessage.ShowMessage("Caught ApplicationException", StandardMessage.Severity.ErrorMessage, null, ex);
                    }
                    catch (Exception ex)
                    {
                        StandardMessage.ShowMessage("Caught Exception", StandardMessage.Severity.ErrorMessage, null, ex);
                    }

                    break;
            }

        }

        private Keys GetKeyCode(string keyString)
        {
            Keys result = default(Keys);

            // TODO: extend select case to enable arbitrary keys;
            // This is only to get started with the keys currently used
            switch (keyString.ToLower() ?? "")
            {
                // alphabetic keys
                case "a":
                    {
                        result = Keys.A;
                        break;
                    }

                case "c":
                    {
                        result = Keys.C;
                        break;
                    }

                case "v":
                    {
                        result = Keys.V;
                        break;
                    }

                case "x":
                    {
                        result = Keys.X;
                        break;
                    }

                // function keys
                case "f5":
                    {
                        result = Keys.F5;
                        break;
                    }

                // other keys
                case "delete":
                    {
                        result = Keys.Delete;
                        break;
                    }

                case "enter":
                    {
                        result = Keys.Enter;
                        break;
                    }
            }

            return result;
        }

        public KeyEventReaction GetKeyEventReaction(KeyEvent ke)
        {
            if (_keyEventDefinitions.ContainsKey(ke))
                return _keyEventDefinitions[ke];
            return new KeyEventReaction(null, KeyEventReaction.EventTypes.NullEvent);
        }
    }
}