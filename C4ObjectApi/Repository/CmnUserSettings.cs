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
using System;
using System.Xml;

namespace C4ObjectApi.Repository
{
    public class CmnUserSettings
    {
        private readonly CmnSession _s;

        public CmnUserSettings(CmnSession s)
        {
            _s = s;
        }
        public XmlElement GetUserSetting(string name)
        {
            XmlNodeList nl = _s.User.UserConfig.DocumentElement.SelectNodes("user_setting[@name='" + name + "']");
            if (nl.Count == 0)
            {
                XmlElement result = (XmlElement)_s.User.UserConfig.DocumentElement.AppendChild(_s.User.UserConfig.CreateElement("user_setting"));
                result.SetAttribute("name", name);
                return result;
            }
            else if (nl.Count == 1)
                return (XmlElement)nl.Item(0);
            else
                throw new ApplicationException("Multiple user_setting elements found for name '" + name + "'");
        }
        public void SetUserSetting(string name, XmlElement usEl, bool save)
        {
            if ((usEl.Name ?? "") != "user_setting")
                throw new ApplicationException("User setting element name must be 'user_setting'.");
            usEl.SetAttribute("name", name); // just in case it's different
            XmlElement currentEl = GetUserSetting(name);
            if (currentEl != null)
                _s.User.UserConfig.DocumentElement.RemoveChild(currentEl);
            _s.User.UserConfig.DocumentElement.AppendChild(_s.User.UserConfig.ImportNode(usEl.CloneNode(true), true));

            if (save)
            {
                // TODO: save user setting
                //_s.CommandSession.setobjectmetaset(_configO.Id, _ms.DocumentElement, (long)_s.SessionConfig.C4Sc.MetasetTypesByName["user_settings"].Id);
            }
        }
    }
}

