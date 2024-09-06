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
using C4ServerConnector.Assets;
using ChangeTriggerLib.Interfaces;
using ChangeTriggerLib.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ChangeTriggerLib.TriggerActionFactories
{
    public class UserCreateFactory : ITriggerActionFactory
    {
        private TriggerActionService _triggerActionService;
        private XmlElement _config;
        private C4Folder _usersFolder;
        public ITriggerAction CreateAction(ILogger logger)
        {
            UserCreateAction action = new UserCreateAction();
            action.Init(_config, _triggerActionService, logger);
            action.AclId= long.Parse(_config.SelectSingleNode("user_folder_acl_id").InnerText);
            action.FolderTypeId = long.Parse(_config.SelectSingleNode("user_folder_type_id").InnerText);
            action.UsersFolder = _usersFolder;
            return action;
        }

        public void Init(XmlElement config, TriggerActionService triggerActionService)
        {
            _config = config;
            _triggerActionService = triggerActionService;
            _usersFolder = _triggerActionService.ServiceSession.GetFolderByPath("/.data/users");
        }
    }
    public class UserCreateAction : ITriggerAction
    {
        private TriggerActionService _triggerActionService;
        private ILogger _logger;
        private XmlElement _config;
        public long AclId { get; set; }
        public long FolderTypeId { get; set; }
        public C4Folder UsersFolder { get; set; }
        public async Task<XmlDocument> ExecuteAsync(string ticket, XmlDocument requestData, XmlDocument requestToCinnamon, IHeaderDictionary headers)
        {
            try
            {
                HashSet<C4Folder> folders = new HashSet<C4Folder>();

                _logger.LogInformation("UserCreateAction: "+requestData.OuterXml);

                foreach (XmlElement userEl in requestData.DocumentElement.SelectNodes("users/user"))
                {
                    string username = userEl.SelectSingleNode("name").InnerText;
                    //long userId = long.Parse(userEl.SelectSingleNode("id").InnerText);
                    long userId = (long)_triggerActionService.SessionConfiguration.UsersByName[_triggerActionService.SessionConfiguration.Username].Id;
                    folders.Add(new C4Folder(0, username, userId, UsersFolder.Id, FolderTypeId, AclId));
                }
                _triggerActionService.ServiceSession.CreateFolders(folders);

                folders = new HashSet<C4Folder>();
                foreach (XmlElement userEl in requestData.DocumentElement.SelectNodes("users/user"))
                {
                    string username = userEl.SelectSingleNode("name").InnerText;
                    C4Folder userFolder = _triggerActionService.ServiceSession.GetFolderByPath("/.data/users/" + username);
                    long userId = long.Parse(userEl.SelectSingleNode("id").InnerText);
                    //long userId = (long)_triggerActionService.SessionConfiguration.UsersByName[_triggerActionService.SessionConfiguration.Username].Id;
                    if (userFolder != null)
                    {
                        folders.Add(new C4Folder(0, "home", userId, userFolder.Id, FolderTypeId, AclId));
                        folders.Add(new C4Folder(0, "config", userId, userFolder.Id, FolderTypeId, AclId));
                        folders.Add(new C4Folder(0, "searches", userId, userFolder.Id, FolderTypeId, AclId));
                    }
                    else
                    {
                        // TODO: log error
                    }
                }
                _triggerActionService.ServiceSession.CreateFolders(folders);
                XmlDocument result = new XmlDocument();
                result.AppendChild(result.CreateElement("success"));
                return await Task.FromResult(result);
            }
            catch(Exception ex)
            {
                XmlDocument result = new XmlDocument();
                result.AppendChild(result.CreateElement("error")).InnerText = string.Join("\n",ex.GetType().ToString(), ex.Message, ex.StackTrace);
                return await Task.FromResult(result);
            }
        }
        

        public void Init(XmlElement config, TriggerActionService triggerActionService, ILogger logger)
        {
            _config = config;
            _triggerActionService = triggerActionService;
            _logger = logger;
        }
    }
}
