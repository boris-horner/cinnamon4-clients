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
using C4ObjectApi.Interfaces;

namespace CDCplusLib.Interfaces
{

    public interface ILogTarget
    {
        // Sub AddInfoEntry(ByVal line As String)
        void Add(ILogRecord r);
        void Add(string title, string description, ILogRecord.LogSeverity severity, IRepositoryNode relatesTo, XmlElement customInfo = null);
        Dictionary<string, ILogRecord> GetLogEntries();
        Dictionary<string, ILogRecord> GetLogEntries(ILogRecord.LogSeverity minSeverity, ILogRecord.LogSeverity maxSeverity);
        void Remove(ILogRecord delR);
    }
}