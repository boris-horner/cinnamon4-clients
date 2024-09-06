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
using C4ObjectApi.Repository;
using C4ServerConnector.Assets;

namespace CDCplusLib.Common.Import
{
    public class FileImportRequest
    {
        public enum StatusValues
        {
            Running,
            OK,
            ReferencesNotFound,
            UnknownFormat,
            Failure
        }
        public StatusValues Status { get; set; }
        public string AbsoluteFilename { get; }
        public string ObjectName { get; set; }
        public C4Format Format { get; set; }
        public C4ObjectType ObjectType { get; set; }
        public CmnObject Object { get; set; }
        public C4Language Language { get; set; }
        public string TargetPath { get; set; }
        private CmnSession _s;

        public Dictionary<string, FileImportRequest> ChildrenByHref { get; }
        public FileImportRequest(CmnSession s, string fn)
        {
            _s = s;
            AbsoluteFilename = fn;
            ObjectName = Path.GetFileNameWithoutExtension(fn);
            Status = StatusValues.Running;
            StatusMessage = "";
            ChildrenByHref = new Dictionary<string, FileImportRequest>();
            Language = _s.SessionConfig.C4Sc.LanguagesByName["zxx"];
            ObjectType = _s.SessionConfig.C4Sc.ObjectTypesByName["_default_objtype"];
            LifecycleState = null;
        }
        public string StatusMessage { get; set; }
        public C4LifecycleState LifecycleState { get; internal set; }
    }
}
