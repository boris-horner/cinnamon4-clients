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
namespace C4Logic
{
    public class Constants
    {
        public const string VALID_NODE_NAME_REGEX = "^([^/\\\\]+)$";
        public const long MAX_CLAUSE_COUNT = 980;
        public const string CHILD_CONTENT_RELATION = "child_content";
        public const string REQUEST_OBJECT_TYPE = "async_processing_request";
        public const string REQUEST_METASET_TYPE = "async_processing_request";
        public const string REQUEST_PATH = "/.data/transient/async_processing_requests";
        public const string REQUEST_NEW_LCS = "req_new.lcs";
        public const string REQUEST_RUNNING_LCS = "req_running.lcs";
        public const string REQUEST_SUCCESS_LCS = "req_success.lcs";
        public const string REQUEST_FAILURE_LCS = "req_failure.lcs";

        public const string HISTORY_METASET_TYPE = "history";
    }
}
