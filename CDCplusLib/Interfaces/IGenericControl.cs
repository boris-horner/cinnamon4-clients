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
using System.Xml;
using CDCplusLib.Common;
using C4ObjectApi.Interfaces;
using C4ObjectApi.Repository;

namespace CDCplusLib.Interfaces
{
    public interface IGenericControl
    {
        enum ContextType { Object, Folder, Session, List }

        event SessionWindowRequestEventHandler SessionWindowRequest;
        event ListSelectionChangedEventHandler ListSelectionChanged; 
        event TreeSelectionChangedEventHandler TreeSelectionChanged;
        event ContextMenuRequestEventHandler ContextMenuRequest;
        event FunctionRequestEventHandler FunctionRequest;
        event NodesModifiedEventHandler NodesModified;
        event KeyPressedEventHandler KeyPressedEvent;
        event RefreshRequestEventHandler RefreshRequest;

        bool HasSelection { get; }
        bool AutoRefresh { get; }
        bool ListContext { get; }

        Dictionary<long, IRepositoryNode> Selection { get; set; }

        bool IsValid(Dictionary<long, IRepositoryNode> dict, ContextType ct);
        void Init(Dictionary<long, IRepositoryNode> dict);
        string GetTabText();
        void Reset(CmnSession s, GlobalApplicationData globalAppData, XmlElement configEl);
        bool IsDirty { get; }

        void Save();
        void ReInit();
    }
}