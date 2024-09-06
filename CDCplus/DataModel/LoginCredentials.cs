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
using System.IO;
using System.Xml;

namespace CDCplus.DataModel
{
    class LoginCredentials
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public LoginCredentials(string username, string password)
        {
            Username = username;
            Password = password;
        }

        public LoginCredentials(string autologinFn)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(autologinFn);
            Username = doc.SelectSingleNode("/  autologin/username").InnerText;
            Password = doc.SelectSingleNode("/autologin/password").InnerText;
        }

        public void Save(string autologinFn)
        {
            XmlDocument doc = new XmlDocument();
            doc.AppendChild(doc.CreateElement("autologin"));
            doc.DocumentElement.AppendChild(doc.CreateElement("username")).InnerText = Username;
            doc.DocumentElement.AppendChild(doc.CreateElement("password")).InnerText = Password;
            doc.Save(autologinFn);
            File.Encrypt(autologinFn);
        }
    }
}
