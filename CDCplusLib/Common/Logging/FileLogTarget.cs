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
using CDCplusLib.Interfaces;
using C4ObjectApi.Interfaces;

namespace CDCplusLib.Common.Logging
{
    public class FileLogTarget : ILogTarget
    {


        private readonly string fn_;
        private Dictionary<string, ILogRecord> logEntries_;
        private readonly ILogRecord.LogSeverity severityLevel_;
        private readonly bool saveLogToFile_;
        private readonly bool consoleLog_;

        public FileLogTarget(string fn, bool consoleLog, ILogRecord.LogSeverity severityLevel = ILogRecord.LogSeverity.LogInfo)
        {
            saveLogToFile_ = fn is not null;
            fn_ = fn;
            severityLevel_ = severityLevel;
            consoleLog_ = consoleLog;
            logEntries_ = new Dictionary<string, ILogRecord>();
        }

        public void Add(ILogRecord r)
        {
            if (severityLevel_ <= r.Severity)
            {
                logEntries_.Add(r.RecordKey, r);
            }
            if (saveLogToFile_)
                System.IO.File.AppendAllText(fn_, string.Concat(r.ToString(),"\n"));
            if (consoleLog_)
                Console.WriteLine(string.Concat(r.Title, ":\n", r.Description));
        }

        public Dictionary<string, ILogRecord> GetLogEntries()
        {
            return logEntries_;
        }

        public void Add(string title, string description, ILogRecord.LogSeverity severity, IRepositoryNode relatesTo, XmlElement customInfo = null)
        {

            StandardLogRecord slr = new StandardLogRecord(title, description, severity, relatesTo);
            slr.CustomInfo = customInfo;
            Add(slr);
        }

        public void Remove(ILogRecord delR)
        {
            RemoveIgnoreException(ref logEntries_, delR);
        }
        private void RemoveIgnoreException(ref Dictionary<string, ILogRecord> ls, ILogRecord lr)
        {
            try
            {
                ls.Remove(lr.RecordKey);
            }
            catch (Exception ex)
            {
                // ignore
            }
        }

        public Dictionary<string, ILogRecord> GetLogEntries(ILogRecord.LogSeverity minSeverity, ILogRecord.LogSeverity maxSeverity)
        {
            Dictionary<string, ILogRecord> result = new Dictionary<string, ILogRecord>();
            foreach (ILogRecord r in logEntries_.Values)
            {
                if (r.Severity >= minSeverity & r.Severity <= maxSeverity)
                    result.Add(r.RecordKey, r);
            }
            return result;
        }
    }
}