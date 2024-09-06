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

namespace CDCplusLib.Interfaces
{
    public interface ILogRecord
    {
        public enum LogSeverity
        {
            LogInfo,     // message just for informational purposes
            LogWarning,  // message containing a warning about an issue that is uncritical processing the current data set (e. g. document)
            LogError,    // message containing a warning about an issue that renders the current data set (e. g. document) incorrect
            LogFatal    // message containing a warning about an issue that interrupts or corrupts the entire processing
        }
        LogSeverity Severity { get; set; }
        string Title { get; set; }
        string Description { get; set; }
        long? RelatesToId { get; set; }
        string RelatesToName { get; set; }
        XmlElement CustomInfo { get; set; }
        DateTime Timestamp { get; }
        string EntryTypeLabel { get; }
        string EntryTypeName { get; }
        string RecordKey { get; }
    }
}