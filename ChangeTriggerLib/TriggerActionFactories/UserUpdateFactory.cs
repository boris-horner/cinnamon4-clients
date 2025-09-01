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
using C4ServerConnector.Assets;
using ChangeTriggerLib.Interfaces;
using ChangeTriggerLib.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration.UserSecrets;
using Serilog;
using System.Xml;

namespace ChangeTriggerLib.TriggerActionFactories
{
    public class UserUpdateFactory : ITriggerActionFactory
    {
        private TriggerActionService _triggerActionService;
        private XmlElement _config;
        private C4Folder _usersFolder;
        public ITriggerAction CreateAction(ILogger logger)
        {
            UserUpdateAction action = new UserUpdateAction();
            action.Init(_config, _triggerActionService, logger);
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
    public class UserUpdateAction : ITriggerAction
    {
        private const string _usersFolderRootPath = "/.data/users/";
        private TriggerActionService _triggerActionService;
        private ILogger _logger;
        private XmlElement _config;
        public C4Folder UsersFolder { get; set; }
        public async Task<XmlDocument> ExecuteAsync(string ticket, XmlDocument requestData, XmlDocument requestToCinnamon, IHeaderDictionary headers)
        {
            try
            {
                foreach (XmlElement userAccountEl in requestToCinnamon.DocumentElement.SelectNodes("userAccounts/userAccount"))
                {
                    long userId = long.Parse(userAccountEl.SelectSingleNode("id").InnerText);
                    string username = userAccountEl.SelectSingleNode("name").InnerText;
                    //long userId = long.Parse(userEl.SelectSingleNode("id").InnerText);
                    string query = $"<BooleanQuery><Clause occurs=\"must\"><BooleanQuery><Clause occurs=\"must\"><WildcardQuery fieldName=\"folderpath\">/root/.data/users/*</WildcardQuery></Clause><Clause occurs=\"must\"><ExactPointQuery fieldName=\"owner\" type=\"long\" value=\""+ userId.ToString()+ "\" /></Clause><Clause occurs=\"must\"><TermQuery fieldName=\"name\">home</TermQuery></Clause></BooleanQuery></Clause></BooleanQuery>";
                    HashSet<long> homeFolderIds= _triggerActionService.ServiceSession.SearchFolderIds(query);
                    if(homeFolderIds.Count()==1)
                    {
                        // rename user folder
                        Dictionary<long, C4Folder> homeFolders = _triggerActionService.ServiceSession.GetFoldersById(homeFolderIds);
                        long renameFolderId =homeFolders.Values.First().ParentId;
                        _logger.Information(renameFolderId.ToString());
                        _triggerActionService.ServiceSession.UpdateFolder(renameFolderId, null, username);
                    }
                    else
                    {
                        foreach (long hfId in homeFolderIds) _logger.Information(hfId.ToString());
                        // log error
                        XmlDocument result = new XmlDocument();
                        result.AppendChild(result.CreateElement("error")).InnerText = "No unique home folder found: " + homeFolderIds.Count().ToString();
                        return await Task.FromResult(result);
                    }
                }
            }
            catch(Exception ex)
            {
                XmlDocument result = new XmlDocument();
                result.AppendChild(result.CreateElement("error")).InnerText = string.Join("\n",ex.GetType().ToString(), ex.Message, ex.StackTrace);
                return await Task.FromResult(result);
            }
            {
                XmlDocument result = new XmlDocument();
                result.AppendChild(result.CreateElement("success"));
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
