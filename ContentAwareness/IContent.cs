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
using System.Collections.Generic;
using System.Xml;
using C4ServerConnector;

namespace ContentAwareness
{
    public interface IContent
    {
        void Init(XmlElement contentEl, bool allowLoggingToConsole);    // can be switched off due to headless software that might fail otherwise
        HashSet<string> GetReferences(string fileName, C4Session c4s, C4SessionConfiguration c4sc, long? id = null); // returns the raw file references (potentially - on import - with relative paths)
        void RewriteReferences(string fileName, Dictionary<string, string> currentToNewRef, C4Session c4s, C4SessionConfiguration c4sc, long? id);
        string Name { get; }
    }
}
