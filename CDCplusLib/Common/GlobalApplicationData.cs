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
using C4ObjectApi.Interfaces;

namespace CDCplusLib.Common
{
    public class GlobalApplicationData
    {
        public GlobalApplicationData(string configFilename)
        {
            try
            {
                ClipboardSourceOperation = ClipboardOperations.ClipboardEmpty;
                ServerReferences = new XmlDocument();
                ServerReferences.Load(configFilename);
                Clipboard = new Dictionary<long, IRepositoryNode>();
                LocalRootPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "cinnamon");
                CheckoutPath = Path.Combine(LocalRootPath, "checkout");
            }
            // InitLogfile()
            catch (Exception ex)
            {
                throw new Exception(Properties.Resources.exFailureLoadingConfiguration, ex);
            }
        }

        public enum ClipboardOperations
        {
            ClipboardCopy,
            ClipboardCut,
            ClipboardEmpty
        }

        public Dictionary<long, IRepositoryNode> Clipboard { get; set; }
        public string LocalRootPath { get; private set; }
        public string CheckoutPath { get; private set; }
        public ClipboardOperations ClipboardSourceOperation { get; set; }
        public XmlDocument ServerReferences { get; private set; }
    }
}