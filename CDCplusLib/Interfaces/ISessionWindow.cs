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
using CDCplusLib.Common;
using C4ObjectApi.Repository;
using CDCplusLib.EventData;
using CDCplusLib.DataModel;

namespace CDCplusLib.Interfaces
{
    public delegate void WindowClosedEventHandler(ISessionWindow sw);
    public delegate void SessionWindowRequestEventHandler(WindowSelectionData wsd);
    public delegate void ContextMenuRequestEventHandler(WindowSelectionData wsd, Point position);
    public delegate void TreeSelectionChangedEventHandler(WindowSelectionData wsd, ISessionWindow sw);
    public delegate void ListSelectionChangedEventHandler(WindowSelectionData wsd);
    public delegate void FunctionRequestEventHandler(WindowSelectionData wsd, string assembly, string type);
    public delegate void NodesModifiedEventHandler(WindowSelectionData wsd);
    public delegate void RefreshRequestEventHandler();
    public delegate void KeyPressedEventHandler(WindowSelectionData wsd, Keys key, bool shift, bool ctrl, bool alt);
    public interface ISessionWindow
    {
        event WindowClosedEventHandler WindowClosed;
        event SessionWindowRequestEventHandler SessionWindowRequest;
        event ContextMenuRequestEventHandler ContextMenuRequest;
        event TreeSelectionChangedEventHandler TreeSelectionChanged;
        event ListSelectionChangedEventHandler ListSelectionChanged;
        event FunctionRequestEventHandler FunctionRequest;
        event NodesModifiedEventHandler NodesModified;
        event RefreshRequestEventHandler RefreshRequest;
        event KeyPressedEventHandler KeyPressedEvent;

        void ShowSessionWindow(CmnSession s, GlobalApplicationData globalAppData, WindowSelectionData wrd = null);
        void CloseWindow();
        void WindowTop();

        string Guid { get; }
        string WindowTitle { get; }
        ContextFunctionsContainer ContextFunctions { get; set; }
    }
}
