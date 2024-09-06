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

    public class StandardLogRecord : ILogRecord
    {

        private readonly string key_;

        public StandardLogRecord(string title, string description, ILogRecord.LogSeverity severity, IRepositoryNode relatesTo)


        {
            Timestamp = DateTime.Now;
            Title = title;
            Description = description;
            Severity = severity;
            if (relatesTo is null)
            {
                RelatesToId = null;
                RelatesToName = "";
            }
            else
            {
                RelatesToId = relatesTo.Id;
                RelatesToName = relatesTo.Name;
            }
            key_ = Guid.NewGuid().ToString();
        }
        public StandardLogRecord(string title, string description, ILogRecord.LogSeverity severity, long relatesToId, string relatesToName)



        {
            Timestamp = DateTime.Now;
            Title = title;
            Description = description;
            Severity = severity;
            RelatesToId = relatesToId;
            RelatesToName = relatesToName;
            key_ = Guid.NewGuid().ToString();
        }
        public string Description { get; set; }

        public long? RelatesToId { get; set; }

        public ILogRecord.LogSeverity Severity { get; set; }

        public string Title { get; set; }

        public DateTime Timestamp { get; private set; }
        public override string ToString()
        {

            string result = "";
            switch (Severity)
            {
                case ILogRecord.LogSeverity.LogError:
                    {
                        result += "[E]";
                        break;
                    }
                case ILogRecord.LogSeverity.LogInfo:
                    {
                        result += "[I]";
                        break;
                    }
                case ILogRecord.LogSeverity.LogFatal:
                    {
                        result += "[F]";
                        break;
                    }
                case ILogRecord.LogSeverity.LogWarning:
                    {
                        result += "[W]";
                        break;
                    }
            }
            result += "[" + Timestamp.ToShortDateString() + " " + Timestamp.ToLongTimeString() + "] ";
            if (RelatesToId is not null)
            {
                result += RelatesToName + " (id=" + RelatesToId.ToString() + ") ";
            }
            result += Title;
            if (Description.Length > 0)
                result += "; " + Description;
            return result;
        }

        public virtual string EntryTypeName
        {
            get
            {
                return GetType().Name;
            }
        }
        public virtual string EntryTypeLabel
        {
            get
            {
                return GetType().Name;
            }
        }

        public string RelatesToName { get; set; }

        public string RecordKey
        {
            get
            {
                return key_;
            }
        }

        public XmlElement CustomInfo { get; set; }
    }
}