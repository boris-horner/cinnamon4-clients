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

namespace C4ServerConnector.Assets
{
    public class C4User
    {
        private XmlDocument _userConfig;
        private C4Session _s;
        public string Name { get; private set; }
        public string Password { get; private set; }
        public string FullName { get; private set; }
        public long? Id { get; private set; }
        public long UiLanguageId { get; private set; }
        public string LoginType { get; private set; }
        public string Email { get; private set; }
        public bool Active { get; private set; }
        public bool ActivateTriggers { get; private set; }
        public bool Locked { get; private set; }
        public bool ChangeTracking { get; private set; }
        public bool PasswordExpired { get; private set; }
        //public bool IsSuperuser { get; private set; }
        public HashSet<long> GroupIds { get; private set; }
        public XmlDocument UserConfig 
        { 
            get 
            { 
                return _userConfig;  
            } 
            set 
            { 
                _userConfig = value;
                _s.SetUserConfig((long)Id, _userConfig);
            } 
        }
        public C4User(string name, 
                      string password,    
                      long? id, 
                      string fullName, 
                      long uiLanguageId, 
                      string loginType, 
                      string email, 
                      bool active, 
                      bool activateTriggers, 
                      bool locked, 
                      bool changeTracking, 
                      bool passwordExpired, 
                      HashSet<long> groupIds, 
                      XmlDocument userConfig,
                      C4Session s) 
        {
            Name = name;
            Password = password;
            Id = id;
            FullName = fullName;
            UiLanguageId = uiLanguageId;
            LoginType = loginType;
            Email = email;
            Active = active;
            ActivateTriggers = activateTriggers;
            Locked = locked;
            ChangeTracking = changeTracking;
            PasswordExpired = passwordExpired;
            _userConfig = userConfig;
            GroupIds = groupIds;
            //IsSuperuser = GroupIds.Contains(superuserGroupId);
            _s = s;
        }
        public C4User(XmlElement assetEl, C4Session s)
        {
            Name = assetEl.SelectSingleNode("name").InnerText;
            Password = null;
            FullName = assetEl.SelectSingleNode("fullname").InnerText;
            XmlNode idN = assetEl.SelectSingleNode("id");
            if (idN == null) Id = null;
            else Id = long.Parse(idN.InnerText);
            UiLanguageId = long.Parse(assetEl.SelectSingleNode("uiLanguageId").InnerText);
            LoginType = assetEl.SelectSingleNode("loginType").InnerText;
            Email = assetEl.SelectSingleNode("email").InnerText;
            Active = assetEl.SelectSingleNode("activated").InnerText == "true" ? true : false;
            ActivateTriggers = assetEl.SelectSingleNode("activateTriggers").InnerText == "true" ? true : false;
            Locked = assetEl.SelectSingleNode("locked").InnerText == "true" ? true : false;
            ChangeTracking = assetEl.SelectSingleNode("changeTracking").InnerText == "true" ? true : false;
            PasswordExpired = assetEl.SelectSingleNode("passwordExpired").InnerText == "true" ? true : false;
            GroupIds = new HashSet<long>();
            foreach(XmlElement idEl in assetEl.SelectNodes("groupIds/groupId"))
            {
                GroupIds.Add(long.Parse(idEl.InnerText));
            }
            //IsSuperuser = true; // TODO: return real value when groups can be resolved
            _userConfig = new XmlDocument();
            _userConfig.LoadXml(assetEl.SelectSingleNode("config").InnerText);
            _s = s;
        }
        public void AppendAssetEl(XmlElement parentEl)
        {
            XmlElement userEl = (XmlElement)parentEl.AppendChild(parentEl.OwnerDocument.CreateElement("userAccount"));
            userEl.AppendChild(parentEl.OwnerDocument.CreateElement("name")).InnerText = Name;
            if (Id != null) userEl.AppendChild(parentEl.OwnerDocument.CreateElement("id")).InnerText = Id.ToString();
            XmlElement passwordEl = (XmlElement)userEl.AppendChild(parentEl.OwnerDocument.CreateElement("password"));
            if(Password!=null) passwordEl.InnerText = Password;
            userEl.AppendChild(parentEl.OwnerDocument.CreateElement("fullname")).InnerText = FullName;
            userEl.AppendChild(parentEl.OwnerDocument.CreateElement("uiLanguageId")).InnerText = UiLanguageId.ToString();
            userEl.AppendChild(parentEl.OwnerDocument.CreateElement("loginType")).InnerText = LoginType;
            userEl.AppendChild(parentEl.OwnerDocument.CreateElement("email")).InnerText = Email;
            userEl.AppendChild(parentEl.OwnerDocument.CreateElement("activated")).InnerText = Active?"true":"false";
            userEl.AppendChild(parentEl.OwnerDocument.CreateElement("activateTriggers")).InnerText = ActivateTriggers ? "true" : "false";
            userEl.AppendChild(parentEl.OwnerDocument.CreateElement("locked")).InnerText = Locked ? "true" : "false";
            userEl.AppendChild(parentEl.OwnerDocument.CreateElement("changeTracking")).InnerText = ChangeTracking ? "true" : "false";
            userEl.AppendChild(parentEl.OwnerDocument.CreateElement("passwordExpired")).InnerText = PasswordExpired ? "true" : "false";
            userEl.AppendChild(parentEl.OwnerDocument.CreateElement("config")).InnerText = UserConfig.OuterXml;
            XmlElement groupIdsEl = (XmlElement)userEl.AppendChild(parentEl.OwnerDocument.CreateElement("groupIds"));
            foreach (long groupId in GroupIds) groupIdsEl.AppendChild(parentEl.OwnerDocument.CreateElement("groupId")).InnerText = groupId.ToString();
        }
        public override string ToString()
        {
            return string.Concat(FullName, " [",Name, "]");
        }
        public bool Equals(C4User otherUser)
        {
            if (otherUser == null)
                return false;
            else
                return Id == otherUser.Id;
        }
        public static bool operator ==(C4User user1, C4User user2)
        {
            if (user1 is null)
                return user2 is null;
            else
                return user1.Equals(user2);
        }

        public static bool operator !=(C4User user1, C4User user2)
        {
            return !(user1 == user2);
        }
    }
}
