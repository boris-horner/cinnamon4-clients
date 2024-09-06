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
namespace C4ServerConnector
{
    public class Constants
    {
        public const string VALID_NODE_NAME_REGEX = "^([^/\\\\]+)$";
        public const long MAX_CLAUSE_COUNT = 980;
        public const string CHILD_CONTENT_RELATION = "child_content";
        public const string REQUEST_OBJECT_TYPE = "async_processing_request";
        public const string REQUEST_METASET_TYPE = "async_processing_request";
        //public const int UPLOAD_BUFFER_SIZE = 16777216;
        public const int UPLOAD_BUFFER_SIZE = 1048576;
        //public const int DOWNLOAD_BUFFER_SIZE = 16777216;
        public const int DOWNLOAD_BUFFER_SIZE = 1048576;
        public const string BOUNDARY = "u7g89dsaanu43g279dfs";
    }
}
