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
namespace C4ObjectApi.Exceptions
{
    public class DataModelErrorCodes
    {
        // operations that failed due to business logic reasons
        public const int BRANCHING_NOT_ALLOWED = 1001;
        public const int UNRESOLVED_INBOUND_REFERENCES = 1002;
        public const int CHECKIN_MISSING_DTD = 1003;
        public const int INSUFFICIENT_PERMISSIONS = 1004;
        public const int INVALID_SELF_REFERENCE = 1005;
        public const int FAILED_INBOUND_REFERENCES = 1006;

        // operations that failed due to technical or OS reasons
        public const int UNTRUSTED_LIBRARIES = 2001;
        public const int FAILURE_COPYING_TO_CLIPBOARD = 2002;
    }
}
