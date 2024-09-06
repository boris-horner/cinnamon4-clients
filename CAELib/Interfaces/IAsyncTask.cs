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
using C4ServerConnector;
using CAELib.Log;
using System.Xml;

namespace CAELib.Interfaces
{
    public interface IAsyncTask
    {
        DateTime NextRun { get; set; }
        int Interval { get; set; }
        void Init(C4Session c4s, C4SessionConfiguration sc, XmlElement tEl, Logger l);

        // usage: if the object was processed, return true
        //        if the object was skipped, return false
        //        if the object was processed, but processing failed, throw an exception

        // use this one for operations on an object or folder (tasks)
        bool Execute(IC4Node n, ref bool? contentChanged, ref bool? metadataChanged);

        // use this one for session operations (jobs)
        bool Execute();
    }
}
