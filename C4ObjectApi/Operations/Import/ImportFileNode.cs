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
using System.IO;
using System.Collections.Generic;
using C4ServerConnector.Assets;

namespace C4ObjectApi.Operations.Import
{
    public class ImportFileNode
    {
        public ImportFileNode(CmnSession s, string fullFileName, string objectName, C4ObjectType objType, C4Language lang, CmnFolder targetFolder, List<string> validationResults = null)
        {
            Session = s;
            FullFileName = fullFileName;
            ObjectName = objectName;
            ObjType = objType;
            string ext = Path.GetExtension(FullFileName).Trim().Substring(1).ToLower();
            Format = Session.SessionConfig.C4Sc.FormatsByExtension[ext];
            Language = lang;
            TargetFolder = targetFolder;
            FileExists = false;
            ValidationResults = validationResults;
            CinnamonObject = null;
        }

        public CmnObject CinnamonObject { get; set; }
        public bool FileExists { get; set; }
        // Public ReadOnly Property ContentAdaptor() As IContentAdaptor
        // Get
        // Return contentAdaptor_
        // End Get
        // End Property
        public CmnSession Session { get; }
        public string FullFileName { get; }
        public string ObjectName { get; set; }
        public C4Format Format { get; set; }
        public C4ObjectType ObjType { get; set; }
        public C4Language Language { get; set; }
        public CmnFolder TargetFolder { get; set; }
        public List<string> ValidationResults { get; set; }
    }
}
