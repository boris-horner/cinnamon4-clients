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
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Xml;

namespace C4ServerConnector
{
    public class C4Session
    {
        public enum NodeType { Object, Folder }
        public enum VersionMode { Head, Branch, All }
        public string Ticket { get; private set; }
        public string BaseUrl { get; private set; }
        public string LocalCertFile { get; private set; }
        public string LocalTempPath { get; private set; }
        public string CdnUri { get; private set; }
        private HttpInterface _http;
        public C4AllPermissions AllPermissions { get; private set; }
        public C4Session(string baseUrl, string user, string password, string cdnUri, string localCertFile, bool writeSessionLog, string localTempPath, long dataTimeoutSeconds=30, long contentTimeoutSeconds=300)
        {
            Dictionary<long, C4Folder> result = new Dictionary<long, C4Folder>();
            try
            {
                _http = new HttpInterface(localCertFile, null, writeSessionLog, dataTimeoutSeconds, contentTimeoutSeconds, localTempPath);
                BaseUrl = baseUrl.EndsWith("/") ? baseUrl.Substring(0, BaseUrl.Length - 1) : baseUrl;
                CdnUri = cdnUri;
                LocalCertFile = localCertFile;
                XmlDocument requestBody = new XmlDocument();
                requestBody.AppendChild(requestBody.CreateElement("connectionRequest"));
                requestBody.DocumentElement.AppendChild(requestBody.CreateElement("username")).InnerText=user;
                requestBody.DocumentElement.AppendChild(requestBody.CreateElement("password")).InnerText = password;
                requestBody.DocumentElement.AppendChild(requestBody.CreateElement("format")).InnerText = "xml";
                Ticket = _http.PostConnectCommand(string.Concat(BaseUrl, "/cinnamon/connect"), requestBody);
                _http.Ticket = Ticket;
                AllPermissions = ListPermissions();
            }
            catch (WebException ex)
            {
                ThrowException(ex);
            }
        }
        public C4Session(string baseUrl, string ticket, string cdnUri, string localCertFile, bool writeSessionLog, string localTempPath, long dataTimeoutSeconds = 30, long contentTimeoutSeconds = 300)
        {
            _http = new HttpInterface(localCertFile, ticket, writeSessionLog, dataTimeoutSeconds, contentTimeoutSeconds, localTempPath);
            Ticket = ticket;
            BaseUrl = baseUrl.EndsWith("/") ? baseUrl.Substring(0, BaseUrl.Length - 1) : baseUrl;
            CdnUri = cdnUri;
            AllPermissions = ListPermissions();
        }
        public static string GetRequest(string baseUrl, string localCertFile, int timeout)
        {
            HttpInterface http = new HttpInterface(localCertFile, null, false, timeout, 300, Path.GetTempPath());
            string httpResult = http.GetCommand(baseUrl);
            return httpResult;
        }
        public XmlDocument Echo(XmlDocument requestBody=null)
        {
            if (requestBody == null)
            {
                requestBody = new XmlDocument();
                requestBody.AppendChild(requestBody.CreateElement("echo"));
            }
            return _http.PostCommand(string.Concat(BaseUrl, "/api/test/echo"), requestBody);
        }
        public void Disconnect()
        {
            try
            {
                _http.PostDisconnectCommand(string.Concat(BaseUrl, "/cinnamon/disconnect"));
            }
            catch (WebException ex)
            {
                ThrowException(ex);
            }
        }
        public XmlDocument TestInterface(XmlDocument requestBody, string commandPath)
        {
            return _http.PostCommand(string.Concat(BaseUrl, commandPath), requestBody);
        }
        // TODO: /cinnamon/info
        // TODO: /config/listAllConfigurations

        // TODO
        #region *** ACL GROUPS ***
        public Dictionary<long, C4AclGroup> ListAclGroupsByGroupOrAcl(long id, bool listByGroup)
        {
            Dictionary<long, C4AclGroup> result = new Dictionary<long, C4AclGroup>();
            try
            {
                XmlDocument requestBody = new XmlDocument();
                requestBody.AppendChild(requestBody.CreateElement("aclGroupListRequest"));
                requestBody.DocumentElement.AppendChild(requestBody.CreateElement("id")).InnerText = id.ToString();
                requestBody.DocumentElement.AppendChild(requestBody.CreateElement("idType")).InnerText = listByGroup ? "GROUP" : "ACL";  
                XmlDocument resp = _http.PostCommand(string.Concat(BaseUrl, "/api/aclGroup/listByGroupOrAcl"), requestBody);
                // CheckResponse(resp, requestBody, "/api/aclGroup/listByGroupOrAcl");
                foreach (XmlElement aclGroupEl in resp.DocumentElement.SelectNodes("aclGroups/aclGroup"))
                {
                    C4AclGroup aclGroup = new C4AclGroup(aclGroupEl, AllPermissions);
                    result.Add((long)aclGroup.Id, aclGroup);
                }
            }
            catch (WebException ex)
            {
                ThrowException(ex);
            }
            return result;
        }
        public XmlDocument CreateAclGroups(HashSet<C4AclGroup> aclGroups)
        {
            XmlDocument resp = null;
            try
            {
                XmlDocument requestBody = new XmlDocument();
                requestBody.AppendChild(requestBody.CreateElement("createAclGroupRequest"));
                XmlElement aclGroupsEl = (XmlElement)requestBody.DocumentElement.AppendChild(requestBody.CreateElement("aclGroups"));
                foreach (C4AclGroup aclGroup in aclGroups) aclGroup.AppendAssetEl(aclGroupsEl);
                resp = _http.PostCommand(string.Concat(BaseUrl, "/api/aclGroup/create"), requestBody);
                // CheckResponse(resp, requestBody, "/api/aclGroup/create");
            }
            catch (WebException ex)
            {
                ThrowException(ex);
            }
            return resp;
        }
        public XmlDocument UpdateAclGroups(HashSet<C4AclGroup> aclGroups)
        {
            XmlDocument resp = null;
            try
            {
                XmlDocument requestBody = new XmlDocument();
                requestBody.AppendChild(requestBody.CreateElement("updateAclGroupRequest"));
                XmlElement aclGroupsEl = (XmlElement)requestBody.DocumentElement.AppendChild(requestBody.CreateElement("aclGroups"));
                foreach (C4AclGroup aclGroup in aclGroups) aclGroup.AppendAssetEl(aclGroupsEl);
                resp = _http.PostCommand(string.Concat(BaseUrl, "/api/aclGroup/update"), requestBody);
                // CheckResponse(resp, requestBody, "/api/aclGroup/update");
            }
            catch (WebException ex)
            {
                ThrowException(ex);
            }
            return resp;
        }
        public XmlDocument DeleteAclGroups(HashSet<long> ids)
        {
            XmlDocument resp = null;
            try
            {
                XmlDocument requestBody = new XmlDocument();
                requestBody.AppendChild(requestBody.CreateElement("deleteAclGroupRequest"));

                requestBody.DocumentElement.AppendChild(requestBody.CreateElement("ignoreNotFound")).InnerText = "false";
                XmlElement idsEl = (XmlElement)requestBody.DocumentElement.AppendChild(requestBody.CreateElement("ids"));
                foreach (long id in ids)
                {
                    idsEl.AppendChild(requestBody.CreateElement("id")).InnerText = id.ToString();
                }
                resp = _http.PostCommand(string.Concat(BaseUrl, "/api/aclGroup/delete"), requestBody);
                // CheckResponse(resp, requestBody, "/api/aclGroup/delete");
            }
            catch (WebException ex)
            {
                ThrowException(ex);
            }
            return resp;
        }
        #endregion
        // TODO: aclInfo
        #region *** ACLS ***
        public Dictionary<long, C4Acl> ListAcls()
        { 
            Dictionary<long, C4Acl> result = new Dictionary<long, C4Acl>();
            try
            {
                XmlDocument requestBody = new XmlDocument();
                requestBody.AppendChild(requestBody.CreateElement("listAclRequest"));
                requestBody.DocumentElement.AppendChild(requestBody.CreateElement("type")).InnerText = "FULL";
                XmlDocument resp = _http.PostCommand(string.Concat(BaseUrl, "/api/acl/list"), requestBody);
                // CheckResponse(resp, requestBody, "/api/acl/list");
                foreach (XmlElement aclEl in resp.DocumentElement.SelectNodes("acls/acl"))
                {
                    C4Acl acl = new C4Acl(aclEl, this);
                    result.Add((long)acl.Id, acl);
                }
            }
            catch (WebException ex)
            {
                ThrowException(ex);
            }
            return result;
        }
        public XmlDocument CreateAcls(HashSet<C4Acl> acls)
        {
            XmlDocument resp = null;
            try
            {
                XmlDocument requestBody = new XmlDocument();
                requestBody.AppendChild(requestBody.CreateElement("createAclRequest"));
                XmlElement aclsEl = (XmlElement)requestBody.DocumentElement.AppendChild(requestBody.CreateElement("acls"));
                foreach (C4Acl acl in acls) acl.AppendAssetEl(aclsEl);
                resp = _http.PostCommand(string.Concat(BaseUrl, "/api/acl/create"), requestBody);
                // CheckResponse(resp, requestBody, "/api/acl/create");
            }
            catch (WebException ex)
            {
                ThrowException(ex);
            }
            return resp;
        }

        public XmlDocument UpdateAcls(HashSet<C4Acl> acls)
        {
            XmlDocument resp = null;
            try
            {
                XmlDocument requestBody = new XmlDocument();
                requestBody.AppendChild(requestBody.CreateElement("updateAclRequest"));
                XmlElement aclsEl = (XmlElement)requestBody.DocumentElement.AppendChild(requestBody.CreateElement("acls"));
                foreach (C4Acl acl in acls) acl.AppendAssetEl(aclsEl);
                resp = _http.PostCommand(string.Concat(BaseUrl, "/api/acl/update"), requestBody);
                // CheckResponse(resp, requestBody, "/api/acl/update");
            }
            catch (WebException ex)
            {
                ThrowException(ex);
            }
            return resp;
        }

        public XmlDocument DeleteAcls(HashSet<long> ids)
        {
            XmlDocument resp = null;
            try
            {
                XmlDocument requestBody = new XmlDocument();
                requestBody.AppendChild(requestBody.CreateElement("deleteAclRequest"));

                requestBody.DocumentElement.AppendChild(requestBody.CreateElement("ignoreNotFound")).InnerText = "false";
                XmlElement idsEl = (XmlElement)requestBody.DocumentElement.AppendChild(requestBody.CreateElement("ids"));
                foreach (long id in ids)
                {
                    idsEl.AppendChild(requestBody.CreateElement("id")).InnerText = id.ToString();
                }
                resp = _http.PostCommand(string.Concat(BaseUrl, "/api/acl/delete"), requestBody);
                // CheckResponse(resp, requestBody, "/api/acl/delete");
            }
            catch (WebException ex)
            {
                ThrowException(ex);
            }
            return resp;
        }
        #endregion
        #region *** CHANGE TRIGGERS ***
        // /api/changeTrigger/list
        // /api/changeTrigger/create
        // /api/changeTrigger/update
        // /api/changeTrigger/delete
        public XmlDocument ChangeTriggerNoOperation(XmlDocument requestBody)
        {
            XmlDocument resp = null;
            try
            {
                resp = _http.PostCommand(string.Concat(BaseUrl, "/api/changeTrigger/nop"), requestBody);
            }
            catch (WebException ex)
            {
                ThrowException(ex);
            }
            return resp;
        }
        public Dictionary<long, C4ChangeTrigger> ListChangeTriggers()
        {
            Dictionary<long, C4ChangeTrigger> result = new Dictionary<long, C4ChangeTrigger>();
            try
            {
                XmlDocument requestBody = new XmlDocument();
                requestBody.AppendChild(requestBody.CreateElement("listChangeTriggerRequest"));
                requestBody.DocumentElement.AppendChild(requestBody.CreateElement("type")).InnerText = "FULL";
                XmlDocument resp = _http.PostCommand(string.Concat(BaseUrl, "/api/changeTrigger/list"), requestBody);
                // CheckResponse(resp, requestBody, "/api/changeTrigger/list");
                foreach (XmlElement configEntryEl in resp.DocumentElement.SelectNodes("changeTriggers/changeTrigger"))
                {
                    C4ChangeTrigger ct = new C4ChangeTrigger(configEntryEl);
                    result.Add((long)ct.Id, ct);
                }
            }
            catch (WebException ex)
            {
                ThrowException(ex);
            }
            return result;
        }
        public XmlDocument CreateChangeTriggers(HashSet<C4ChangeTrigger> changeTriggers)
        {
            XmlDocument resp = null;
            try
            {
                XmlDocument requestBody = new XmlDocument();
                requestBody.AppendChild(requestBody.CreateElement("createChangeTriggerRequest"));
                XmlElement changeTriggerEl = (XmlElement)requestBody.DocumentElement.AppendChild(requestBody.CreateElement("changeTriggers"));
                foreach (C4ChangeTrigger ct in changeTriggers) ct.AppendAssetEl(changeTriggerEl);
                resp = _http.PostCommand(string.Concat(BaseUrl, "/api/changeTrigger/create"), requestBody);
                // CheckResponse(resp, requestBody, "/api/changeTrigger/create");
            }
            catch (WebException ex)
            {
                ThrowException(ex);
            }
            return resp;
        }

        public XmlDocument UpdateChangeTriggers(HashSet<C4ChangeTrigger> changeTriggers)
        {
            XmlDocument resp = null;
            try
            {
                XmlDocument requestBody = new XmlDocument();
                requestBody.AppendChild(requestBody.CreateElement("updateChangeTriggerRequest"));
                XmlElement configEntriesEl = (XmlElement)requestBody.DocumentElement.AppendChild(requestBody.CreateElement("changeTriggers"));
                foreach (C4ChangeTrigger ct in changeTriggers) ct.AppendAssetEl(configEntriesEl);
                resp = _http.PostCommand(string.Concat(BaseUrl, "/api/changeTrigger/update"), requestBody);
                // CheckResponse(resp, requestBody, "/api/changeTrigger/update");
            }
            catch (WebException ex)
            {
                ThrowException(ex);
            }
            return resp;
        }

        public XmlDocument DeleteChangeTriggers(HashSet<long> ids)
        {
            XmlDocument resp = null;
            try
            {
                XmlDocument requestBody = new XmlDocument();
                requestBody.AppendChild(requestBody.CreateElement("deleteChangeTriggerRequest"));

                requestBody.DocumentElement.AppendChild(requestBody.CreateElement("ignoreNotFound")).InnerText = "false";
                XmlElement idsEl = (XmlElement)requestBody.DocumentElement.AppendChild(requestBody.CreateElement("ids"));
                foreach (long id in ids)
                {
                    idsEl.AppendChild(requestBody.CreateElement("id")).InnerText = id.ToString();
                }
                resp = _http.PostCommand(string.Concat(BaseUrl, "/api/changeTrigger/delete"), requestBody);
                // CheckResponse(resp, requestBody, "/api/changeTrigger/delete");
            }
            catch (WebException ex)
            {
                ThrowException(ex);
            }
            return resp;
        }
        #endregion
        #region *** CONFIG ENTRIES ***
        public Dictionary<long, C4ConfigEntry> ListConfigEntries()
        {
            Dictionary<long, C4ConfigEntry> result = new Dictionary<long, C4ConfigEntry>();
            try
            {
                XmlDocument requestBody = new XmlDocument();
                requestBody.AppendChild(requestBody.CreateElement("listConfigEntryRequest"));
                requestBody.DocumentElement.AppendChild(requestBody.CreateElement("type")).InnerText = "FULL";
                XmlDocument resp = _http.PostCommand(string.Concat(BaseUrl, "/api/configEntry/list"), requestBody);
                // CheckResponse(resp, requestBody, "/api/configEntry/list");
                foreach (XmlElement configEntryEl in resp.DocumentElement.SelectNodes("configEntries/configEntry"))
                {
                    C4ConfigEntry ce = new C4ConfigEntry(configEntryEl);
                    result.Add((long)ce.Id, ce);
                }
            }
            catch (WebException ex)
            {
                ThrowException(ex);
            }
            return result;
        }
        public XmlDocument CreateConfigEntries(HashSet<C4ConfigEntry> configEntries)
        {
            XmlDocument resp = null;
            try
            {
                XmlDocument requestBody = new XmlDocument();
                requestBody.AppendChild(requestBody.CreateElement("createConfigEntryRequest"));
                XmlElement configEntriesEl = (XmlElement)requestBody.DocumentElement.AppendChild(requestBody.CreateElement("configEntries"));
                foreach (C4ConfigEntry configEntry in configEntries) configEntry.AppendAssetEl(configEntriesEl);
                resp = _http.PostCommand(string.Concat(BaseUrl, "/api/configEntry/create"), requestBody);
                // CheckResponse(resp, requestBody, "/api/configEntry/create");
            }
            catch (WebException ex)
            {
                ThrowException(ex);
            }
            return resp;
        }

        public XmlDocument UpdateConfigEntries(HashSet<C4ConfigEntry> configEntries)
        {
            XmlDocument resp = null;
            try
            {
                XmlDocument requestBody = new XmlDocument();
                requestBody.AppendChild(requestBody.CreateElement("updateConfigEntryRequest"));
                XmlElement configEntriesEl = (XmlElement)requestBody.DocumentElement.AppendChild(requestBody.CreateElement("configEntries"));
                foreach (C4ConfigEntry configEntry in configEntries) configEntry.AppendAssetEl(configEntriesEl);
                resp = _http.PostCommand(string.Concat(BaseUrl, "/api/configEntry/update"), requestBody);
                // CheckResponse(resp, requestBody, "/api/configEntry/update");
            }
            catch (WebException ex)
            {
                ThrowException(ex);
            }
            return resp;
        }

        public XmlDocument DeleteConfigEntries(HashSet<long> ids)
        {
            XmlDocument resp = null;
            try
            {
                XmlDocument requestBody = new XmlDocument();
                requestBody.AppendChild(requestBody.CreateElement("deleteConfigEntryRequest"));

                requestBody.DocumentElement.AppendChild(requestBody.CreateElement("ignoreNotFound")).InnerText = "false";
                XmlElement idsEl = (XmlElement)requestBody.DocumentElement.AppendChild(requestBody.CreateElement("ids"));
                foreach (long id in ids)
                {
                    idsEl.AppendChild(requestBody.CreateElement("id")).InnerText = id.ToString();
                }
                resp = _http.PostCommand(string.Concat(BaseUrl, "/api/configEntry/delete"), requestBody);
                // CheckResponse(resp, requestBody, "/api/configEntry/delete");
            }
            catch (WebException ex)
            {
                ThrowException(ex);
            }
            return resp;
        }
        #endregion
        #region *** FOLDER TYPES ***
        public Dictionary<long, C4FolderType> ListFolderTypes()
        {
                Dictionary<long, C4FolderType> result = new Dictionary<long, C4FolderType>();
            try
            {
                XmlDocument requestBody = new XmlDocument();
                requestBody.AppendChild(requestBody.CreateElement("listFolderTypeRequest"));
                requestBody.DocumentElement.AppendChild(requestBody.CreateElement("type")).InnerText = "FULL";
                XmlDocument resp = _http.PostCommand(string.Concat(BaseUrl, "/api/folderType/list"), requestBody);
                // CheckResponse(resp, requestBody, "/api/folderType/list");
                foreach (XmlElement folderTypeEl in resp.DocumentElement.SelectNodes("folderTypes/folderType"))
                {
                    C4FolderType ft = new C4FolderType(folderTypeEl);
                    result.Add((long)ft.Id, ft);
                }
            }
            catch (WebException ex)
            {
                ThrowException(ex);
            }
            return result;
        }
        public XmlDocument CreateFolderTypes(HashSet<C4FolderType> folderTypes)
        {
            XmlDocument resp = null;
            try
            {
                XmlDocument requestBody = new XmlDocument();
                requestBody.AppendChild(requestBody.CreateElement("createFolderTypeRequest"));
                XmlElement folderTypesEl = (XmlElement)requestBody.DocumentElement.AppendChild(requestBody.CreateElement("folderTypes"));
                foreach (C4FolderType folderType in folderTypes) folderType.AppendAssetEl(folderTypesEl);
                resp = _http.PostCommand(string.Concat(BaseUrl, "/api/folderType/create"), requestBody);
                // CheckResponse(resp, requestBody, "/api/folderType/create");
            }
            catch (WebException ex)
            {
                ThrowException(ex);
            }
            return resp;
        }

        public XmlDocument UpdateFolderTypes(HashSet<C4FolderType> folderTypes)
        {
            XmlDocument resp = null;
            try
            {
                XmlDocument requestBody = new XmlDocument();
                requestBody.AppendChild(requestBody.CreateElement("updateFolderTypeRequest"));
                XmlElement folderTypesEl = (XmlElement)requestBody.DocumentElement.AppendChild(requestBody.CreateElement("folderTypes"));
                foreach (C4FolderType folderType in folderTypes) folderType.AppendAssetEl(folderTypesEl);
                resp = _http.PostCommand(string.Concat(BaseUrl, "/api/folderType/update"), requestBody);
                // CheckResponse(resp, requestBody, "/api/folderType/update");
            }
            catch (WebException ex)
            {
                ThrowException(ex);
            }
            return resp;
        }

        public XmlDocument DeleteFolderTypes(HashSet<long> ids)
        {
            XmlDocument resp = null;
            try
            {
                XmlDocument requestBody = new XmlDocument();
                requestBody.AppendChild(requestBody.CreateElement("deleteFolderTypeRequest"));

                requestBody.DocumentElement.AppendChild(requestBody.CreateElement("ignoreNotFound")).InnerText = "false";
                XmlElement idsEl = (XmlElement)requestBody.DocumentElement.AppendChild(requestBody.CreateElement("ids"));
                foreach (long id in ids)
                {
                    idsEl.AppendChild(requestBody.CreateElement("id")).InnerText = id.ToString();
                }
                resp = _http.PostCommand(string.Concat(BaseUrl, "/api/folderType/delete"), requestBody);
                // CheckResponse(resp, requestBody, "/api/folderType/delete");
            }
            catch (WebException ex)
            {
                ThrowException(ex);
            }
            return resp;
        }
        #endregion
        // TODO
        #region *** FOLDERS ***
        public Dictionary<long, C4Folder> CreateFolders(HashSet<C4Folder> folders)
        {
            Dictionary<long, C4Folder> result = null;
            try
            {
                XmlDocument requestBody = new XmlDocument();
                requestBody.AppendChild(requestBody.CreateElement("createFolderRequest"));
                XmlElement foldersEl = (XmlElement)requestBody.DocumentElement.AppendChild(requestBody.CreateElement("folders"));
                foreach (C4Folder folder in folders) folder.AppendAssetEl(foldersEl);
                XmlDocument resp = _http.PostCommand(string.Concat(BaseUrl, "/api/folder/create"), requestBody);
                // CheckResponse(resp, requestBody, "/api/folder/create");
                result = ParseFolderResponse(resp);
            }
            catch (WebException ex)
            {
                ThrowException(ex);
            }
            return result;
        }
        public C4Folder GetFolderByPath(string path)
        {
            C4Folder result = null;
            try
            {
                XmlDocument requestBody = new XmlDocument();
                requestBody.AppendChild(requestBody.CreateElement("folderPathRequest"));
                requestBody.DocumentElement.AppendChild(requestBody.CreateElement("path")).InnerText = path;
                requestBody.DocumentElement.AppendChild(requestBody.CreateElement("includeSummary")).InnerText = "true";
                //requestBody.DocumentElement.AppendChild(requestBody.CreateElement("includeCustomMetadata")).InnerText = includeCustomMetadata ? "true" : "false";
                XmlDocument resp = _http.PostCommand(string.Concat(BaseUrl, "/api/folder/getFolderByPath"), requestBody);
                Dictionary<long, C4Folder> folders = ParseFolderResponse(resp);
                if (folders.Count > 0)
                {
                    Dictionary<long, C4Folder> foldersByParentId = new Dictionary<long, C4Folder>();
                    foreach (C4Folder f in folders.Values) if (f.ParentId != 0) foldersByParentId.Add(f.ParentId, f);

                    C4Folder currFolder = folders.Values.First();
                    //C4Folder currFolder = folders[1];
                    while (foldersByParentId.ContainsKey(currFolder.Id))
                    {
                        currFolder = foldersByParentId[currFolder.Id];
                    }
                    result = currFolder;
                }
                else result = null; 
            }
            catch (WebException ex)
            {
                ThrowException(ex);
            }
            return result;
        }
        public Dictionary<long, C4Folder> GetFoldersById(HashSet<long> ids)
        {
            Dictionary<long, C4Folder> result = new Dictionary<long, C4Folder>();
            try
            {
                XmlDocument requestBody = new XmlDocument();
                requestBody.AppendChild(requestBody.CreateElement("folderRequest"));
                XmlElement idsEl = (XmlElement)requestBody.DocumentElement.AppendChild(requestBody.CreateElement("ids"));
                foreach (long id in ids) idsEl.AppendChild(requestBody.CreateElement("id")).InnerText = id.ToString();
                requestBody.DocumentElement.AppendChild(requestBody.CreateElement("includeSummary")).InnerText = "true";
                //requestBody.DocumentElement.AppendChild(requestBody.CreateElement("includeCustomMetadata")).InnerText = includeCustomMetadata ? "true" : "false";
                XmlDocument resp = _http.PostCommand(string.Concat(BaseUrl, "/api/folder/getFolders"), requestBody);
                // CheckResponse(resp, requestBody, "/api/folder/getFolders");
                result = ParseFolderResponse(resp);
            }
            catch (WebException ex)
            {
                ThrowException(ex);
            }
            return result;
        }
        public C4Metadata GetFolderMeta(long id, HashSet<long> typeIds = null)
        {
            try
            {
                XmlDocument requestBody = new XmlDocument();
                requestBody.AppendChild(requestBody.CreateElement("metaRequest"));
                requestBody.DocumentElement.AppendChild(requestBody.CreateElement("id")).InnerText = id.ToString();
                if (typeIds != null)
                {
                    XmlElement typeIdsEl = (XmlElement)requestBody.DocumentElement.AppendChild(requestBody.CreateElement("typeIds"));
                    foreach (long typeId in typeIds) typeIdsEl.AppendChild(requestBody.CreateElement("typeId")).InnerText = typeId.ToString();

                }
                XmlDocument resp = _http.PostCommand(string.Concat(BaseUrl, "/api/folder/getMeta"), requestBody);
                // CheckResponse(resp, requestBody, "/api/folder/getMeta");
                return new C4Metadata(resp.DocumentElement.SelectSingleNode("metasets") as XmlElement);
            }
            catch (WebException ex)
            {
                ThrowException(ex);
                return null;
            }
        }
        public Dictionary<long, C4Folder> GetSubFolders(long id)
        {
            Dictionary<long, C4Folder> result = new Dictionary<long, C4Folder>();
            try
            {
                XmlDocument requestBody = new XmlDocument();
                requestBody.AppendChild(requestBody.CreateElement("singleFolderRequest"));
                requestBody.DocumentElement.AppendChild(requestBody.CreateElement("id")).InnerText = id.ToString();
                requestBody.DocumentElement.AppendChild(requestBody.CreateElement("includeSummary")).InnerText = "true";
                //requestBody.DocumentElement.AppendChild(requestBody.CreateElement("includeCustomMetadata")).InnerText = includeCustomMetadata ? "true" : "false";
                XmlDocument resp = _http.PostCommand(string.Concat(BaseUrl, "/api/folder/getSubFolders"), requestBody);
                // CheckResponse(resp, requestBody, "/api/folder/getSubFolders", false);
                result = ParseFolderResponse(resp);
            }
            catch (WebException ex)
            {
                ThrowException(ex);
            }
            return result;
        }
        private Dictionary<long, C4Folder> ParseFolderResponse(XmlDocument resp)
        {
            Dictionary<long, C4Folder> result = new Dictionary<long, C4Folder>();
            foreach (XmlElement folderEl in resp.DocumentElement.SelectNodes("folders/folder"))
            {
                C4Folder f = new C4Folder(folderEl);
                result.Add(f.Id, f);
            }
            return result;
        }
        public XmlDocument SetFolderSummary(long id, XmlElement summaryEl)       // summaryEl.Name must be summary, no further check
        {
            try
            {
                XmlDocument requestBody = new XmlDocument();
                requestBody.AppendChild(requestBody.CreateElement("setSummaryRequest"));
                requestBody.DocumentElement.AppendChild(requestBody.CreateElement("id")).InnerText = id.ToString();
                requestBody.DocumentElement.AppendChild(requestBody.CreateElement("summary")).InnerText = summaryEl.OuterXml;
                //requestBody.DocumentElement.AppendChild(requestBody.ImportNode(summaryEl.CloneNode(true), true));
                XmlDocument resp = _http.PostCommand(string.Concat(BaseUrl, "/api/folder/setSummary"), requestBody);
                // CheckResponse(resp, requestBody, "/api/folder/setSummary");
                return resp;
            }
            catch (WebException ex)
            {
                ThrowException(ex);
                return null;    // code is never reached - without this line, the compiler states that not all code parts return a value 
            }
        }
        // TODO: extend this to work with multiple folders
        public XmlDocument UpdateFolder(long id, long? parentId = null, string name = null, long? ownerId = null, long? typeId = null, long? aclId = null, bool? metadataChanged = null)       // summaryEl.Name must be summary, no further check
        {
            try
            {
                // TODO: metadata changed
                XmlDocument requestBody = new XmlDocument();
                requestBody.AppendChild(requestBody.CreateElement("updateFolderRequest"));
                requestBody.DocumentElement.AppendChild(requestBody.CreateElement("updateMetadataChanged")).InnerText = (metadataChanged==null?"false":"true");
                XmlElement foldersEl = (XmlElement)requestBody.DocumentElement.AppendChild(requestBody.CreateElement("folders"));
                XmlElement folderEl = (XmlElement)foldersEl.AppendChild(requestBody.CreateElement("folder"));
                folderEl.AppendChild(requestBody.CreateElement("id")).InnerText = id.ToString();
                if(parentId!=null) folderEl.AppendChild(requestBody.CreateElement("parentId")).InnerText = parentId.ToString();
                if (name != null) folderEl.AppendChild(requestBody.CreateElement("name")).InnerText = name;
                if (ownerId != null) folderEl.AppendChild(requestBody.CreateElement("ownerId")).InnerText = ownerId.ToString();
                if (typeId != null) folderEl.AppendChild(requestBody.CreateElement("typeId")).InnerText = typeId.ToString();
                if (aclId != null) folderEl.AppendChild(requestBody.CreateElement("aclId")).InnerText = aclId.ToString();
                if (metadataChanged != null) folderEl.AppendChild(requestBody.CreateElement("metadataChanged")).InnerText = (bool)metadataChanged?"true":"false";
                XmlDocument resp = _http.PostCommand(string.Concat(BaseUrl, "/api/folder/update"), requestBody);
                // CheckResponse(resp, requestBody, "/api/folder/update");
                return resp;
            }
            catch (WebException ex)
            {
                ThrowException(ex);
                return null;    // code is never reached - without this line, the compiler states that not all code parts return a value 
            }
        }
        public XmlDocument DeleteFolders(HashSet<long> ids, bool deleteRecursively, bool deleteContent) 
        {
            try
            {
                XmlDocument requestBody = new XmlDocument();
                requestBody.AppendChild(requestBody.CreateElement("deleteFolderRequest"));

                XmlElement idsEl = (XmlElement)requestBody.DocumentElement.AppendChild(requestBody.CreateElement("ids"));
                foreach (long id in ids)
                {
                    idsEl.AppendChild(requestBody.CreateElement("id")).InnerText = id.ToString();
                }
                requestBody.DocumentElement.AppendChild(requestBody.CreateElement("deleteRecursively")).InnerText = deleteRecursively ? "true" : "false";
                requestBody.DocumentElement.AppendChild(requestBody.CreateElement("deleteContent")).InnerText = deleteContent ? "true" : "false";
                XmlDocument resp = _http.PostCommand(string.Concat(BaseUrl, "/api/folder/delete"), requestBody);
                // CheckResponse(resp, requestBody, "/api/folder/delete");
                return resp;
            }
            catch (WebException ex)
            {
                ThrowException(ex);
                return null;    // code is never reached - without this line, the compiler states that not all code parts return a value 
            }
        }
        public XmlDocument CreateFolderMeta(Dictionary<long, HashSet<C4Metaset>> createMetasets)
        {
            try
            {
                XmlDocument requestBody = new XmlDocument();
                requestBody.AppendChild(requestBody.CreateElement("setMetaRequest"));
                XmlElement metasEl = (XmlElement)requestBody.DocumentElement.AppendChild(requestBody.CreateElement("metasets"));

                foreach (long objectId in createMetasets.Keys)
                {
                    foreach (C4Metaset metaset in createMetasets[objectId])
                    {
                        XmlElement metaEl = (XmlElement)metasEl.AppendChild(requestBody.CreateElement("metaset"));
                        metaEl.AppendChild(requestBody.CreateElement("id"));    //empty metaset id element
                        metaEl.AppendChild(requestBody.CreateElement("objectId")).InnerText = objectId.ToString();    //empty metaset id element
                        metaEl.AppendChild(requestBody.CreateElement("typeId")).InnerText = metaset.TypeId.ToString();
                        metaEl.AppendChild(requestBody.CreateElement("content")).InnerText = metaset.Content.OuterXml;
                    }
                }
                XmlDocument resp = _http.PostCommand(string.Concat(BaseUrl, "/api/folder/createMeta"), requestBody);
                // CheckResponse(resp, requestBody, "/api/folder/createMeta");
                return resp;
            }
            catch (WebException ex)
            {
                ThrowException(ex);
                return null;    // code is never reached - without this line, the compiler states that not all code parts return a value 
            }
        }
        public XmlDocument UpdateFolderMetaContent(Dictionary<long, HashSet<C4Metaset>> updateMetasets)
        {
            try
            {
                XmlDocument requestBody = new XmlDocument();
                requestBody.AppendChild(requestBody.CreateElement("updateMetaRequest"));
                XmlElement metasEl = (XmlElement)requestBody.DocumentElement.AppendChild(requestBody.CreateElement("metasets"));

                foreach (long folderId in updateMetasets.Keys)
                {
                    foreach (C4Metaset metaset in updateMetasets[folderId])
                    {
                        XmlElement metaEl = (XmlElement)metasEl.AppendChild(requestBody.CreateElement("metaset"));
                        metaEl.AppendChild(requestBody.CreateElement("id")).InnerText = ((long)metaset.Id).ToString();
                        metaEl.AppendChild(requestBody.CreateElement("objectId")).InnerText = folderId.ToString();
                        metaEl.AppendChild(requestBody.CreateElement("typeId")).InnerText = metaset.TypeId.ToString();
                        metaEl.AppendChild(requestBody.CreateElement("content")).InnerText = metaset.Content.OuterXml;
                    }
                }
                XmlDocument resp = _http.PostCommand(string.Concat(BaseUrl, "/api/folder/updateMetaContent"), requestBody);
                // CheckResponse(resp, requestBody, "/api/folder/updateMetaContent");
                return resp;
            }
            catch (WebException ex)
            {
                ThrowException(ex);
                return null;    // code is never reached - without this line, the compiler states that not all code parts return a value 
            }
        }
        public XmlDocument DeleteAllFolderMetadata(HashSet<long> ids)
        {
            try
            {
                XmlDocument requestBody = new XmlDocument();
                requestBody.AppendChild(requestBody.CreateElement("deleteAllMetasRequest"));

                requestBody.DocumentElement.AppendChild(requestBody.CreateElement("ignoreNotFound")).InnerText = "false";
                XmlElement idsEl = (XmlElement)requestBody.DocumentElement.AppendChild(requestBody.CreateElement("ids"));
                foreach (long id in ids)
                {
                    idsEl.AppendChild(requestBody.CreateElement("id")).InnerText = id.ToString();
                }
                XmlDocument resp = _http.PostCommand(string.Concat(BaseUrl, "/api/folder/deleteAllMetas"), requestBody);
                // CheckResponse(resp, requestBody, "/api/folder/deleteAllMetas");
                return resp;
            }
            catch (WebException ex)
            {
                ThrowException(ex);
                return null;    // code is never reached - without this line, the compiler states that not all code parts return a value 
            }
        }
        public XmlDocument DeleteFolderMetasets(HashSet<long> ids)
        {
            try
            {
                // TODO: make this a parameter if use cases arise
                bool ignoreNotFound = true;
                XmlDocument requestBody = new XmlDocument();
                requestBody.AppendChild(requestBody.CreateElement("deleteMetaRequest"));

                requestBody.DocumentElement.AppendChild(requestBody.CreateElement("ignoreNotFound")).InnerText = ignoreNotFound ? "true" : "false";
                XmlElement idsEl = (XmlElement)requestBody.DocumentElement.AppendChild(requestBody.CreateElement("ids"));
                foreach (long id in ids)
                {
                    idsEl.AppendChild(requestBody.CreateElement("id")).InnerText = id.ToString();
                }
                XmlDocument resp = _http.PostCommand(string.Concat(BaseUrl, "/api/folder/deleteMeta"), requestBody);

                // TODO: remove condition when server no longer ignores ignoreNotFound
                return resp;
            }
            catch (WebException ex)
            {
                ThrowException(ex);
                return null;    // code is never reached - without this line, the compiler states that not all code parts return a value 
            }
        }
        #endregion
        #region *** FORMATS ***
        public Dictionary<long, C4Format> ListFormats()
        {
            try
            {
                XmlDocument requestBody = new XmlDocument();
                requestBody.AppendChild(requestBody.CreateElement("listFormatRequest"));
                requestBody.DocumentElement.AppendChild(requestBody.CreateElement("type")).InnerText = "FULL";
                XmlDocument resp = _http.PostCommand(string.Concat(BaseUrl, "/api/format/list"), requestBody);
                // CheckResponse(resp, requestBody, "/api/format/delete");
                Dictionary<long, C4Format> result = new Dictionary<long, C4Format>();
                foreach (XmlElement formatEl in resp.DocumentElement.SelectNodes("formats/format"))
                {
                    C4Format f = new C4Format(formatEl);
                    result.Add((long)f.Id, f);
                }
                return result;
            }
            catch (WebException ex)
            {
                ThrowException(ex);
                return null;    // code is never reached - without this line, the compiler states that not all code parts return a value 
            }
        }
        public XmlDocument CreateFormats(HashSet<C4Format> formats)
        {
            try
            {
                XmlDocument requestBody = new XmlDocument();
                requestBody.AppendChild(requestBody.CreateElement("createFormatRequest"));
                XmlElement formatsEl = (XmlElement)requestBody.DocumentElement.AppendChild(requestBody.CreateElement("formats"));
                foreach (C4Format format in formats) format.AppendAssetEl(formatsEl);
                XmlDocument resp = _http.PostCommand(string.Concat(BaseUrl, "/api/format/create"), requestBody);
                // CheckResponse(resp, requestBody, "/api/format/create");
                return resp;
            }
            catch (WebException ex)
            {
                ThrowException(ex);
                return null;    // code is never reached - without this line, the compiler states that not all code parts return a value 
            }
        }
        public XmlDocument UpdateFormats(HashSet<C4Format> formats)
        {
            try
            {
                XmlDocument requestBody = new XmlDocument();
                //requestBody.AppendChild(requestBody.CreateXmlDeclaration("1.0", "UTF-8", null));
                requestBody.AppendChild(requestBody.CreateElement("updateFormatRequest"));
                XmlElement formatsEl = (XmlElement)requestBody.DocumentElement.AppendChild(requestBody.CreateElement("formats"));
                foreach (C4Format format in formats) format.AppendAssetEl(formatsEl);
                XmlDocument resp = _http.PostCommand(string.Concat(BaseUrl, "/api/format/update"), requestBody);
                // CheckResponse(resp, requestBody, "/api/format/update");
                return resp;
            }
            catch (WebException ex)
            {
                ThrowException(ex);
                return null;    // code is never reached - without this line, the compiler states that not all code parts return a value 
            }
        }
        public XmlDocument DeleteFormats(HashSet<long> ids)
        {
            try
            {
                XmlDocument requestBody = new XmlDocument();
                requestBody.AppendChild(requestBody.CreateElement("deleteFormatRequest"));

                requestBody.DocumentElement.AppendChild(requestBody.CreateElement("ignoreNotFound")).InnerText = "false";   // necessary?
                XmlElement idsEl = (XmlElement)requestBody.DocumentElement.AppendChild(requestBody.CreateElement("ids"));
                foreach (long id in ids)
                {
                    idsEl.AppendChild(requestBody.CreateElement("id")).InnerText = id.ToString();
                }
                XmlDocument resp = _http.PostCommand(string.Concat(BaseUrl, "/api/format/delete"), requestBody);
                // CheckResponse(resp, requestBody, "/api/format/delete");
                return resp;
            }
            catch (WebException ex)
            {
                ThrowException(ex);
                return null;    // code is never reached - without this line, the compiler states that not all code parts return a value 
            }
        }
        #endregion
        // TODO: addUserToGroups, removeUserFromGroups
        #region *** GROUPS ***
        public Dictionary<long, C4Group> ListGroups()
        {
            Dictionary<long, C4Group> result = new Dictionary<long, C4Group>();
            try
            {
                XmlDocument requestBody = new XmlDocument();
                requestBody.AppendChild(requestBody.CreateElement("listGroupRequest"));
                requestBody.DocumentElement.AppendChild(requestBody.CreateElement("type")).InnerText = "FULL";
                XmlDocument resp = _http.PostCommand(string.Concat(BaseUrl, "/api/group/list"), requestBody);
                // CheckResponse(resp, requestBody, "/api/group/list");
                foreach (XmlElement groupEl in resp.DocumentElement.SelectNodes("groups/group"))
                {
                    C4Group ft = new C4Group(groupEl);
                    result.Add((long)ft.Id, ft);
                }
            }
            catch (WebException ex)
            {
                ThrowException(ex);
            }
            return result;
        }
        public XmlDocument CreateGroups(HashSet<C4Group> groups)
        {
            XmlDocument resp = null;
            try
            {
                XmlDocument requestBody = new XmlDocument();
                requestBody.AppendChild(requestBody.CreateElement("createGroupRequest"));
                XmlElement groupsEl = (XmlElement)requestBody.DocumentElement.AppendChild(requestBody.CreateElement("groups"));
                foreach (C4Group group in groups) group.AppendAssetEl(groupsEl);
                resp = _http.PostCommand(string.Concat(BaseUrl, "/api/group/create"), requestBody);
                // CheckResponse(resp, requestBody, "/api/group/create");
            }
            catch (WebException ex)
            {
                ThrowException(ex);
            }
            return resp;
        }

        public XmlDocument UpdateGroups(HashSet<C4Group> groups)
        {
            XmlDocument resp = null;
            try
            {
                XmlDocument requestBody = new XmlDocument();
                requestBody.AppendChild(requestBody.CreateElement("updateGroupRequest"));
                XmlElement groupsEl = (XmlElement)requestBody.DocumentElement.AppendChild(requestBody.CreateElement("groups"));
                foreach (C4Group group in groups) group.AppendAssetEl(groupsEl);
                resp = _http.PostCommand(string.Concat(BaseUrl, "/api/group/update"), requestBody);
                // CheckResponse(resp, requestBody, "/api/group/update");
            }
            catch (WebException ex)
            {
                ThrowException(ex);
            }
            return resp;
        }

        public XmlDocument DeleteGroups(HashSet<long> ids, bool recursive)
        {
            XmlDocument resp = null;
            try
            {
                XmlDocument requestBody = new XmlDocument();
                requestBody.AppendChild(requestBody.CreateElement("deleteGroupRequest"));

                requestBody.DocumentElement.AppendChild(requestBody.CreateElement("ignoreNotFound")).InnerText = "false";
                requestBody.DocumentElement.AppendChild(requestBody.CreateElement("recursive")).InnerText = recursive?"true":"false";
                XmlElement idsEl = (XmlElement)requestBody.DocumentElement.AppendChild(requestBody.CreateElement("ids"));
                foreach (long id in ids)
                {
                    idsEl.AppendChild(requestBody.CreateElement("id")).InnerText = id.ToString();
                }
                resp = _http.PostCommand(string.Concat(BaseUrl, "/api/group/delete"), requestBody);
                // CheckResponse(resp, requestBody, "/api/group/delete");
            }
            catch (WebException ex)
            {
                ThrowException(ex);
            }
            return resp;
        }
        #endregion
        #region INDEX
        // TODO
        //public XmlDocument Reindex(HashSet<long> objectIds, HashSet<long> folderIds, bool recreateTika)
        //{
        //    try
        //    {
        //    }
        //    catch (WebException ex)
        //    {
        //        ThrowException(ex);
        //        return null;    // code is never reached - without this line, the compiler states that not all code parts return a value 
        //    }
        //}

        #endregion
        // TODO: RESOURCE_NOT_FOUND
        #region *** INDEX ITEMS ***
        public Dictionary<string, C4IndexItem> ListIndexItems(bool withSystemItems)     // NOTE: this is by name, apart from the other asset lists, to cover system index items that do not have an id
        {
            Dictionary<string, C4IndexItem> result = withSystemItems ? SystemIndexItemsFactory.GetSystemIndexItemsByName():new Dictionary<string, C4IndexItem>();
            try
            {
                XmlDocument requestBody = new XmlDocument();
                requestBody.AppendChild(requestBody.CreateElement("listIndexItemRequest"));
                requestBody.DocumentElement.AppendChild(requestBody.CreateElement("type")).InnerText = "FULL";
                XmlDocument resp = _http.PostCommand(string.Concat(BaseUrl, "/api/indexItem/list"), requestBody);
                // CheckResponse(resp, requestBody, "/api/indexItem/list");
                foreach (XmlElement indexItemEl in resp.DocumentElement.SelectNodes("indexItems/indexItem"))
                {
                    C4IndexItem ii = new C4IndexItem(indexItemEl);
                    result.Add(ii.Name, ii);
                }
            }
            catch (WebException ex)
            {
                ThrowException(ex);
            }
            return result;
        }
        public XmlDocument CreateIndexItems(HashSet<C4IndexItem> indexItems)
        {
            XmlDocument resp = null;
            try
            {
                XmlDocument requestBody = new XmlDocument();
                requestBody.AppendChild(requestBody.CreateElement("createIndexItemRequest"));
                XmlElement indexItemsEl = (XmlElement)requestBody.DocumentElement.AppendChild(requestBody.CreateElement("indexItems"));
                foreach (C4IndexItem indexItem in indexItems) indexItem.AppendAssetEl(indexItemsEl);
                resp = _http.PostCommand(string.Concat(BaseUrl, "/api/indexItem/create"), requestBody);
                // CheckResponse(resp, requestBody, "/api/indexItem/create");
            }
            catch (WebException ex)
            {
                ThrowException(ex);
            }
            return resp;
        }

        public XmlDocument UpdateIndexItems(HashSet<C4IndexItem> indexItems)
        {
            XmlDocument resp = null;
            try
            {
                XmlDocument requestBody = new XmlDocument();
                requestBody.AppendChild(requestBody.CreateElement("updateIndexItemRequest"));
                XmlElement indexItemsEl = (XmlElement)requestBody.DocumentElement.AppendChild(requestBody.CreateElement("indexItems"));
                foreach (C4IndexItem indexItem in indexItems) indexItem.AppendAssetEl(indexItemsEl);
                resp = _http.PostCommand(string.Concat(BaseUrl, "/api/indexItem/update"), requestBody);
                // CheckResponse(resp, requestBody, "/api/indexItem/update");
            }
            catch (WebException ex)
            {
                ThrowException(ex);
            }
            return resp;
        }

        public XmlDocument DeleteIndexItems(HashSet<long> ids)
        {
            XmlDocument resp = null;
            try
            {
                XmlDocument requestBody = new XmlDocument();
                requestBody.AppendChild(requestBody.CreateElement("deleteIndexItemRequest"));

                requestBody.DocumentElement.AppendChild(requestBody.CreateElement("ignoreNotFound")).InnerText = "false";
                XmlElement idsEl = (XmlElement)requestBody.DocumentElement.AppendChild(requestBody.CreateElement("ids"));
                foreach (long id in ids)
                {
                    idsEl.AppendChild(requestBody.CreateElement("id")).InnerText = id.ToString();
                }
                resp = _http.PostCommand(string.Concat(BaseUrl, "/api/indexItem/delete"), requestBody);
                // CheckResponse(resp, requestBody, "/api/indexItem/delete");
            }
            catch (WebException ex)
            {
                ThrowException(ex);
            }
            return resp;
        }
        #endregion
        #region *** LANGUAGES ***
        public Dictionary<long, C4Language> ListLanguages()
        {
            Dictionary<long, C4Language> result = new Dictionary<long, C4Language>();
            try
            {
                XmlDocument requestBody = new XmlDocument();
                requestBody.AppendChild(requestBody.CreateElement("listLanguageRequest"));
                requestBody.DocumentElement.AppendChild(requestBody.CreateElement("type")).InnerText = "FULL";
                XmlDocument resp = _http.PostCommand(string.Concat(BaseUrl, "/api/language/list"), requestBody);
                // CheckResponse(resp, requestBody, "/api/language/list");
                foreach (XmlElement languageEl in resp.DocumentElement.SelectNodes("languages/language"))
                {
                    C4Language lang = new C4Language(languageEl);
                    result.Add((long)lang.Id, lang);
                }
            }
            catch (WebException ex)
            {
                ThrowException(ex);
            }
            return result;
        }
        public XmlDocument CreateLanguages(HashSet<C4Language> languages)
        {
            XmlDocument resp = null;
            try
            {
                XmlDocument requestBody = new XmlDocument();
                requestBody.AppendChild(requestBody.CreateElement("createLanguageRequest"));
                XmlElement languagesEl = (XmlElement)requestBody.DocumentElement.AppendChild(requestBody.CreateElement("languages"));
                foreach (C4Language language in languages) language.AppendAssetEl(languagesEl);
                resp = _http.PostCommand(string.Concat(BaseUrl, "/api/language/create"), requestBody);
                // CheckResponse(resp, requestBody, "/api/language/create");
            }
            catch (WebException ex)
            {
                ThrowException(ex);
            }
            return resp;
        }

        public XmlDocument UpdateLanguages(HashSet<C4Language> languages)
        {
            XmlDocument resp = null;
            try
            {
                XmlDocument requestBody = new XmlDocument();
                requestBody.AppendChild(requestBody.CreateElement("updateLanguageRequest"));
                XmlElement languagesEl = (XmlElement)requestBody.DocumentElement.AppendChild(requestBody.CreateElement("languages"));
                foreach (C4Language language in languages) language.AppendAssetEl(languagesEl);
                resp = _http.PostCommand(string.Concat(BaseUrl, "/api/language/update"), requestBody);
                // CheckResponse(resp, requestBody, "/api/language/update");
            }
            catch (WebException ex)
            {
                ThrowException(ex);
            }
            return resp;
        }

        public XmlDocument DeleteLanguages(HashSet<long> ids)
        {
            XmlDocument resp = null;
            try
            {
                XmlDocument requestBody = new XmlDocument();
                requestBody.AppendChild(requestBody.CreateElement("deleteLanguageRequest"));

                requestBody.DocumentElement.AppendChild(requestBody.CreateElement("ignoreNotFound")).InnerText = "false";
                XmlElement idsEl = (XmlElement)requestBody.DocumentElement.AppendChild(requestBody.CreateElement("ids"));
                foreach (long id in ids)
                {
                    idsEl.AppendChild(requestBody.CreateElement("id")).InnerText = id.ToString();
                }
                resp = _http.PostCommand(string.Concat(BaseUrl, "/api/language/delete"), requestBody);
                // CheckResponse(resp, requestBody, "/api/language/delete");
            }
            catch (WebException ex)
            {
                ThrowException(ex);
            }
            return resp;
        }
		#endregion
		// TODO
		#region *** LIFECYCLE STATES ***
		public Dictionary<long, C4LifecycleState> ListLifecycleStates()
		{
			Dictionary<long, C4LifecycleState> result = new Dictionary<long, C4LifecycleState>();
			try
			{
				XmlDocument requestBody = new XmlDocument();
				requestBody.AppendChild(requestBody.CreateElement("listLifecycleStateRequest"));
				requestBody.DocumentElement.AppendChild(requestBody.CreateElement("type")).InnerText = "FULL";
				XmlDocument resp = _http.PostCommand(string.Concat(BaseUrl, "/api/lifecycleState/list"), requestBody);
				// CheckResponse(resp, requestBody, "/api/lifecycleState/list");
				foreach (XmlElement lifecycleEl in resp.DocumentElement.SelectNodes("lifecycleStates/lifecycleState"))
				{
					C4LifecycleState lc = new C4LifecycleState(lifecycleEl);
					result.Add((long)lc.Id, lc);
				}
			}
			catch (WebException ex)
			{
				ThrowException(ex);
			}
			return result;
		}
		public XmlDocument AttachLifecycle(long objectId, long lifecycleId, long lifecycleStateId)
        {
            XmlDocument resp = null;
            try
            {
                XmlDocument requestBody = new XmlDocument();
                requestBody.AppendChild(requestBody.CreateElement("attachLifecycleRequest"));
                requestBody.DocumentElement.AppendChild(requestBody.CreateElement("osdId")).InnerText = objectId.ToString();
                requestBody.DocumentElement.AppendChild(requestBody.CreateElement("lifecycleId")).InnerText = lifecycleId.ToString();
                requestBody.DocumentElement.AppendChild(requestBody.CreateElement("lifecycleStateId")).InnerText = lifecycleStateId.ToString();
                resp = _http.PostCommand(string.Concat(BaseUrl, "/api/lifecycleState/attachLifecycle"), requestBody);
                // CheckResponse(resp, requestBody, "/api/lifecycleState/attachLifecycle");
            }
            catch (WebException ex)
            {
                ThrowException(ex);
            }
            return resp;
        }
        public XmlDocument ChangeState(long objectId, long lifecycleStateId)
        {
            XmlDocument resp = null;
            try
            {
                XmlDocument requestBody = new XmlDocument();
                requestBody.AppendChild(requestBody.CreateElement("changeLifecycleStateRequest"));
                requestBody.DocumentElement.AppendChild(requestBody.CreateElement("osdId")).InnerText = objectId.ToString();
                requestBody.DocumentElement.AppendChild(requestBody.CreateElement("stateId")).InnerText = lifecycleStateId.ToString();
                resp = _http.PostCommand(string.Concat(BaseUrl, "/api/lifecycleState/changeState"), requestBody);
                // CheckResponse(resp, requestBody, "/api/lifecycleState/changeState");
            }
            catch (WebException ex)
            {
                ThrowException(ex);
            }
            return resp;
        }
        public XmlDocument DetachLifecycle(long id)
        {
            try
            {
                XmlDocument requestBody = new XmlDocument();
                requestBody.AppendChild(requestBody.CreateElement("idRequest"));
                requestBody.DocumentElement.AppendChild(requestBody.CreateElement("id")).InnerText = id.ToString();

                XmlDocument resp = _http.PostCommand(string.Concat(BaseUrl, "/api/lifecycleState/detachLifecycle"), requestBody);
                // CheckResponse(resp, requestBody, "/api/lifecycleState/detachLifecycle");
                return resp;
            }
            catch (WebException ex)
            {
                ThrowException(ex);
                return null;    // code is never reached - without this line, the compiler states that not all code parts return a value 
            }
        }
		public XmlDocument CreateLifecycleStates(HashSet<C4LifecycleState> lifecycleStates)
		{
			XmlDocument resp = null;
			try
			{
				XmlDocument requestBody = new XmlDocument();
				requestBody.AppendChild(requestBody.CreateElement("createLifecycleStateRequest"));
				XmlElement lifecycleStatesEl = (XmlElement)requestBody.DocumentElement.AppendChild(requestBody.CreateElement("lifecycleStates"));
				foreach (C4LifecycleState lifecycleState in lifecycleStates) lifecycleState.AppendAssetEl(lifecycleStatesEl);
				resp = _http.PostCommand(string.Concat(BaseUrl, "/api/lifecycleState/create"), requestBody);
				// CheckResponse(resp, requestBody, "/api/lifecycleState/create");
			}
			catch (WebException ex)
			{
				ThrowException(ex);
			}
			return resp;
		}
		public XmlDocument UpdateLifecycleStates(HashSet<C4LifecycleState> lifecycleStates)
		{
			XmlDocument resp = null;
			try
			{
				XmlDocument requestBody = new XmlDocument();
				requestBody.AppendChild(requestBody.CreateElement("createLifecycleStateRequest"));
				XmlElement lifecycleStatesEl = (XmlElement)requestBody.DocumentElement.AppendChild(requestBody.CreateElement("lifecycleStates"));
				foreach (C4LifecycleState lifecycleState in lifecycleStates) lifecycleState.AppendAssetEl(lifecycleStatesEl);
				resp = _http.PostCommand(string.Concat(BaseUrl, "/api/lifecycleState/update"), requestBody);
				// CheckResponse(resp, requestBody, "/api/lifecycleState/update");
			}
			catch (WebException ex)
			{
				ThrowException(ex);
			}
			return resp;
		}
		public XmlDocument DeleteLifecycleStates(HashSet<long> ids)
		{
			XmlDocument resp = null;
			try
			{
				XmlDocument requestBody = new XmlDocument();
				requestBody.AppendChild(requestBody.CreateElement("deleteLifecycleStateRequest"));

				requestBody.DocumentElement.AppendChild(requestBody.CreateElement("ignoreNotFound")).InnerText = "false";
				XmlElement idsEl = (XmlElement)requestBody.DocumentElement.AppendChild(requestBody.CreateElement("ids"));
				foreach (long id in ids)
				{
					idsEl.AppendChild(requestBody.CreateElement("id")).InnerText = id.ToString();
				}
				resp = _http.PostCommand(string.Concat(BaseUrl, "/api/lifecycleState/delete"), requestBody);
				// CheckResponse(resp, requestBody, "/api/lifecycleState/delete");
			}
			catch (WebException ex)
			{
				ThrowException(ex);
			}
			return resp;
		}
		#endregion
		// TODO: get
		#region *** LIFECYCLES ***
		public Dictionary<long, C4Lifecycle> ListLifecycles()
        {
            Dictionary<long, C4Lifecycle> result = new Dictionary<long, C4Lifecycle>();
            try
            {
                XmlDocument requestBody = new XmlDocument();
                requestBody.AppendChild(requestBody.CreateElement("listLifecycleRequest"));
                requestBody.DocumentElement.AppendChild(requestBody.CreateElement("type")).InnerText = "FULL";
                XmlDocument resp = _http.PostCommand(string.Concat(BaseUrl, "/api/lifecycle/list"), requestBody);
                // CheckResponse(resp, requestBody, "/api/lifecycle/list");
                foreach (XmlElement lifecycleEl in resp.DocumentElement.SelectNodes("lifecycles/lifecycle"))
                {
                    C4Lifecycle lc = new C4Lifecycle(lifecycleEl);
                    result.Add((long)lc.Id, lc);
                }
            }
            catch (WebException ex)
            {
                ThrowException(ex);
            }
            return result;
        }
		public XmlDocument CreateLifecycles(HashSet<C4Lifecycle> lifecycles)
		{
			XmlDocument resp = null;
			try
			{
				XmlDocument requestBody = new XmlDocument();
				requestBody.AppendChild(requestBody.CreateElement("createLifecycleRequest"));
				XmlElement lifecyclesEl = (XmlElement)requestBody.DocumentElement.AppendChild(requestBody.CreateElement("lifecycles"));
				foreach (C4Lifecycle lifecycle in lifecycles) lifecycle.AppendAssetEl(lifecyclesEl);
				resp = _http.PostCommand(string.Concat(BaseUrl, "/api/lifecycle/create"), requestBody);
				// CheckResponse(resp, requestBody, "/api/lifecycle/create");
			}
			catch (WebException ex)
			{
				ThrowException(ex);
			}
			return resp;
		}
		public XmlDocument UpdateLifecycles(HashSet<C4Lifecycle> lifecycles)
		{
			XmlDocument resp = null;
			try
			{
				XmlDocument requestBody = new XmlDocument();
				requestBody.AppendChild(requestBody.CreateElement("updateLifecycleRequest"));
				XmlElement lifecyclesEl = (XmlElement)requestBody.DocumentElement.AppendChild(requestBody.CreateElement("lifecycles"));
				foreach (C4Lifecycle lifecycle in lifecycles) lifecycle.AppendAssetEl(lifecyclesEl);
				resp = _http.PostCommand(string.Concat(BaseUrl, "/api/lifecycle/update"), requestBody);
				// CheckResponse(resp, requestBody, "/api/lifecycle/update");
			}
			catch (WebException ex)
			{
				ThrowException(ex);
			}
			return resp;
		}
		public XmlDocument DeleteLifecycles(HashSet<long> ids)
		{
			XmlDocument resp = null;
			try
			{
				XmlDocument requestBody = new XmlDocument();
				requestBody.AppendChild(requestBody.CreateElement("deleteLifecycleRequest"));

				requestBody.DocumentElement.AppendChild(requestBody.CreateElement("ignoreNotFound")).InnerText = "false";
				XmlElement idsEl = (XmlElement)requestBody.DocumentElement.AppendChild(requestBody.CreateElement("ids"));
				foreach (long id in ids)
				{
					idsEl.AppendChild(requestBody.CreateElement("id")).InnerText = id.ToString();
				}
				resp = _http.PostCommand(string.Concat(BaseUrl, "/api/lifecycle/delete"), requestBody);
				// CheckResponse(resp, requestBody, "/api/lifecycle/delete");
			}
			catch (WebException ex)
			{
				ThrowException(ex);
			}
			return resp;
		}
		#endregion
		// TODO
		#region *** LINKS ***
		public Dictionary<long, C4Link> CreateLinks(HashSet<C4Link> links)
        {
            Dictionary<long, C4Link> result = new Dictionary<long, C4Link>();
            try
            {
                XmlDocument requestBody = new XmlDocument();
                requestBody.AppendChild(requestBody.CreateElement("createLinkRequest"));
                XmlElement linksEl = (XmlElement)requestBody.DocumentElement.AppendChild(requestBody.CreateElement("links"));
                foreach (C4Link link in links) link.AppendAssetEl(linksEl);
                XmlDocument resp = _http.PostCommand(string.Concat(BaseUrl, "/api/link/create"), requestBody);
                // CheckResponse(resp, requestBody, "/api/link/create");
                foreach (XmlElement linkEl in resp.DocumentElement.SelectNodes("links/link"))
                {
                    C4Link link = new C4Link(linkEl);
                    result.Add((long)link.Id, link);
                }
            }
            catch (WebException ex)
            {
                ThrowException(ex);
            }
            return result;
        }
        public Dictionary<long, C4Link> UpdateLinks(HashSet<C4Link> links)
        {
            Dictionary<long, C4Link> result = new Dictionary<long, C4Link>();
            try
            {
                XmlDocument requestBody = new XmlDocument();
                requestBody.AppendChild(requestBody.CreateElement("updateLinkRequest"));
                XmlElement linksEl = (XmlElement)requestBody.DocumentElement.AppendChild(requestBody.CreateElement("links"));
                foreach (C4Link link in links) link.AppendAssetEl(linksEl);
                XmlDocument resp = _http.PostCommand(string.Concat(BaseUrl, "/api/link/update"), requestBody);
                // CheckResponse(resp, requestBody, "/api/link/update");
                foreach (XmlElement linkEl in resp.DocumentElement.SelectNodes("links/link"))
                {
                    C4Link link = new C4Link(linkEl);
                    result.Add((long)link.Id, link);
                }
            }
            catch (WebException ex)
            {
                ThrowException(ex);
            }
            return result;
        }
        public XmlDocument DeleteLinks(HashSet<long> ids, bool ignoreNotFound)
        {
            try
            {
                XmlDocument requestBody = new XmlDocument();
                requestBody.AppendChild(requestBody.CreateElement("deleteLinkRequest"));

                XmlElement idsEl = (XmlElement)requestBody.DocumentElement.AppendChild(requestBody.CreateElement("ids"));
                foreach (long id in ids)
                {
                    idsEl.AppendChild(requestBody.CreateElement("id")).InnerText = id.ToString();
                }
                requestBody.DocumentElement.AppendChild(requestBody.CreateElement("ignoreNotFound")).InnerText = ignoreNotFound ? "true" : "false";
                XmlDocument resp = _http.PostCommand(string.Concat(BaseUrl, "/api/link/delete"), requestBody);
                // CheckResponse(resp, requestBody, "/api/link/delete");
                return resp;
            }
            catch (WebException ex)
            {
                ThrowException(ex);
                return null;    // code is never reached - without this line, the compiler states that not all code parts return a value 
            }
        }
        //Dictionary<string, string> fields = new Dictionary<string, string>();
        //fields.Add("command", "createlink");
        //fields.Add("parent_id", Id.ToString());
        //fields.Add("acl_id", linkAcl.Id.ToString());
        //fields.Add("owner_id", Session.User.Id.ToString());
        //fields.Add("id", o.Id.ToString());
        //fields.Add("type", "OBJECT");
        //fields.Add("version", latestVersion ? "LATEST_HEAD" : "FIXED");
        //fields.Add("ticket", Session.Ticket);
        #endregion
        #region *** METASET TYPES ***
        public Dictionary<long, C4MetasetType> ListMetasetTypes()
        {
            Dictionary<long, C4MetasetType> result = new Dictionary<long, C4MetasetType>();
            try
            {
                XmlDocument requestBody = new XmlDocument();
                requestBody.AppendChild(requestBody.CreateElement("listMetasetTypeRequest"));
                requestBody.DocumentElement.AppendChild(requestBody.CreateElement("type")).InnerText = "FULL";
                XmlDocument resp = _http.PostCommand(string.Concat(BaseUrl, "/api/metasetType/list"), requestBody);
                // CheckResponse(resp, requestBody, "/api/metasetType/list");
                foreach (XmlElement metasetTypeEl in resp.DocumentElement.SelectNodes("metasetTypes/metasetType"))
                {
                    C4MetasetType mt = new C4MetasetType(metasetTypeEl);
                    result.Add((long)mt.Id, mt);
                }
            }
            catch (WebException ex)
            {
                ThrowException(ex);
            }
            return result;
        }
        public XmlDocument CreateMetasetTypes(HashSet<C4MetasetType> metasetTypes)
        {
            XmlDocument resp = null;
            try
            {
                XmlDocument requestBody = new XmlDocument();
                requestBody.AppendChild(requestBody.CreateElement("createMetasetTypeRequest"));
                XmlElement metasetTypesEl = (XmlElement)requestBody.DocumentElement.AppendChild(requestBody.CreateElement("metasetTypes"));
                foreach (C4MetasetType metasetType in metasetTypes) metasetType.AppendAssetEl(metasetTypesEl);
                resp = _http.PostCommand(string.Concat(BaseUrl, "/api/metasetType/create"), requestBody);
                // CheckResponse(resp, requestBody, "/api/metasetType/create");
            }
            catch (WebException ex)
            {
                ThrowException(ex);
            }
            return resp;
        }

        public XmlDocument UpdateMetasetTypes(HashSet<C4MetasetType> metasetTypes)
        {
            XmlDocument resp = null;
            try
            {
                XmlDocument requestBody = new XmlDocument();
                requestBody.AppendChild(requestBody.CreateElement("updateMetasetTypeRequest"));
                XmlElement metasetTypesEl = (XmlElement)requestBody.DocumentElement.AppendChild(requestBody.CreateElement("metasetTypes"));
                foreach (C4MetasetType metasetType in metasetTypes) metasetType.AppendAssetEl(metasetTypesEl);
                resp = _http.PostCommand(string.Concat(BaseUrl, "/api/metasetType/update"), requestBody);
                // CheckResponse(resp, requestBody, "/api/metasetType/update");
            }
            catch (WebException ex)
            {
                ThrowException(ex);
            }
            return resp;
        }

        public XmlDocument DeleteMetasetTypes(HashSet<long> ids)
        {
            XmlDocument resp = null;
            try
            {
                XmlDocument requestBody = new XmlDocument();
                requestBody.AppendChild(requestBody.CreateElement("deleteMetasetTypeRequest"));

                requestBody.DocumentElement.AppendChild(requestBody.CreateElement("ignoreNotFound")).InnerText = "false";
                XmlElement idsEl = (XmlElement)requestBody.DocumentElement.AppendChild(requestBody.CreateElement("ids"));
                foreach (long id in ids)
                {
                    idsEl.AppendChild(requestBody.CreateElement("id")).InnerText = id.ToString();
                }
                resp = _http.PostCommand(string.Concat(BaseUrl, "/api/metasetType/delete"), requestBody);
                // CheckResponse(resp, requestBody, "/api/metasetType/delete");
            }
            catch (WebException ex)
            {
                ThrowException(ex);
            }
            return resp;
        }
        #endregion
        #region *** OBJECT TYPES ***
        public Dictionary<long, C4ObjectType> ListObjectTypes()
        {
            Dictionary<long, C4ObjectType> result = new Dictionary<long, C4ObjectType>();
            try
            {
                XmlDocument requestBody = new XmlDocument();
                requestBody.AppendChild(requestBody.CreateElement("listObjectTypeRequest"));
                requestBody.DocumentElement.AppendChild(requestBody.CreateElement("type")).InnerText="FULL";
                XmlDocument resp = _http.PostCommand(string.Concat(BaseUrl, "/api/objectType/list"), requestBody);
                // CheckResponse(resp, requestBody, "/api/objectType/list");
                foreach (XmlElement objectTypeEl in resp.DocumentElement.SelectNodes("objectTypes/objectType"))
                {
                    C4ObjectType ot = new C4ObjectType(objectTypeEl);
                    result.Add((long)ot.Id, ot);
                }
            }
            catch (WebException ex)
            {
                ThrowException(ex);
            }
            return result;
        }
        public XmlDocument CreateObjectTypes(HashSet<C4ObjectType> objectTypes)
        {
            XmlDocument resp = null;
            try
            {
                XmlDocument requestBody = new XmlDocument();
                requestBody.AppendChild(requestBody.CreateElement("createObjectTypeRequest"));
                XmlElement objectTypesEl = (XmlElement)requestBody.DocumentElement.AppendChild(requestBody.CreateElement("objectTypes"));
                foreach (C4ObjectType objectType in objectTypes) objectType.AppendAssetEl(objectTypesEl);
                resp = _http.PostCommand(string.Concat(BaseUrl, "/api/objectType/create"), requestBody);
                // CheckResponse(resp, requestBody, "/api/objectType/create");
            }
            catch (WebException ex)
            {
                ThrowException(ex);
            }
            return resp;
        }

        public XmlDocument UpdateObjectTypes(HashSet<C4ObjectType> objectTypes)
        {
            XmlDocument resp = null;
            try
            {
                XmlDocument requestBody = new XmlDocument();
                requestBody.AppendChild(requestBody.CreateElement("updateObjectTypeRequest"));
                XmlElement objectTypesEl = (XmlElement)requestBody.DocumentElement.AppendChild(requestBody.CreateElement("objectTypes"));
                foreach (C4ObjectType objectType in objectTypes) objectType.AppendAssetEl(objectTypesEl);
                resp = _http.PostCommand(string.Concat(BaseUrl, "/api/objectType/update"), requestBody);
                // CheckResponse(resp, requestBody, "/api/objectType/update");
            }
            catch (WebException ex)
            {
                ThrowException(ex);
            }
            return resp;
        }

        public XmlDocument DeleteObjectTypes(HashSet<long> ids)
        {
            XmlDocument resp = null;
            try
            {
                XmlDocument requestBody = new XmlDocument();
                requestBody.AppendChild(requestBody.CreateElement("deleteObjectTypeRequest"));

                requestBody.DocumentElement.AppendChild(requestBody.CreateElement("ignoreNotFound")).InnerText = "false";
                XmlElement idsEl = (XmlElement)requestBody.DocumentElement.AppendChild(requestBody.CreateElement("ids"));
                foreach (long id in ids)
                {
                    idsEl.AppendChild(requestBody.CreateElement("id")).InnerText = id.ToString();
                }
                resp = _http.PostCommand(string.Concat(BaseUrl, "/api/objectType/delete"), requestBody);
                // CheckResponse(resp, requestBody, "/api/objectType/delete");
            }
            catch (WebException ex)
            {
                ThrowException(ex);
            }
            return resp;
        }
        #endregion
        // TODO
        #region *** OSDS ***
        public Dictionary<long, C4Object> Copy(HashSet<long> objectIds, long targetFolderId, HashSet<long> metasetTypeIds=null)
        {
            XmlDocument resp = null;
            try
            {
                XmlDocument requestBody = new XmlDocument();
                requestBody.AppendChild(requestBody.CreateElement("copyOsdRequest"));
                requestBody.DocumentElement.AppendChild(requestBody.CreateElement("targetFolderId")).InnerText = targetFolderId.ToString();
                XmlElement sourceIdsEl = (XmlElement)requestBody.DocumentElement.AppendChild(requestBody.CreateElement("sourceIds"));
                foreach(long id in objectIds) sourceIdsEl.AppendChild(requestBody.CreateElement("sourceId")).InnerText = id.ToString();
                XmlElement metasetTypeIdsEl = (XmlElement)requestBody.DocumentElement.AppendChild(requestBody.CreateElement("metasetTypeIds"));
                if(metasetTypeIds!=null) foreach (long id in metasetTypeIds) metasetTypeIdsEl.AppendChild(requestBody.CreateElement("metasetTypeId")).InnerText = id.ToString();
                resp = _http.PostCommand(string.Concat(BaseUrl, "/api/osd/copy"), requestBody);
                // CheckResponse(resp, requestBody, "/api/osd/copy");
            }
            catch (WebException ex)
            {
                ThrowException(ex);
            }
            Dictionary<long, C4Object> result = new Dictionary<long, C4Object>();
            foreach (XmlElement oEl in resp.DocumentElement.SelectNodes("osds/osd"))
            {
                C4Object o = new C4Object(oEl);
                result.Add(o.Id, o);
            }
            return result;
        }
        public void CopyToExisting(long sourceId, long targetId, bool copyContent, HashSet<long> copyMetasetTypeIds)
        {
            // TODO: the API method supports multiple tasks, here only one is implemented
            XmlDocument resp = null;
            try
            {
                XmlDocument requestBody = new XmlDocument();
                requestBody.AppendChild(requestBody.CreateElement("copyToExistingOsdRequest"));
                XmlElement copyTasksEl = requestBody.DocumentElement.AppendChild(requestBody.CreateElement("copyTasks")) as XmlElement;
                XmlElement copyTaskEl = copyTasksEl.AppendChild(requestBody.CreateElement("copyTasks")) as XmlElement;
                copyTaskEl.AppendChild(requestBody.CreateElement("sourceOsdId")).InnerText = sourceId.ToString();
                copyTaskEl.AppendChild(requestBody.CreateElement("targetOsdId")).InnerText = targetId.ToString();
                copyTaskEl.AppendChild(requestBody.CreateElement("copyContent")).InnerText = copyContent?"true":"false";
                XmlElement metasetTypeIdsEl = copyTaskEl.AppendChild(requestBody.CreateElement("metasetTypeIds")) as XmlElement;
                foreach (long msTypeId in copyMetasetTypeIds) metasetTypeIdsEl.AppendChild(requestBody.CreateElement("metasetTypeId")).InnerText = msTypeId.ToString();
                resp = _http.PostCommand(string.Concat(BaseUrl, "/api/osd/copyToExisting"), requestBody);
                // CheckResponse(resp, requestBody, "/api/osd/copyToExisting");
            }
            catch (WebException ex)
            {
                ThrowException(ex);
            }
        }
        public C4Object CreateObject(long parentFolderId, string name, long ownerId, long aclId, long typeId, long languageId, long? lifecycleStateId = null, long? formatId = null, string contentFn = null)
        {
            try
            {
                XmlDocument requestBody = new XmlDocument();
                requestBody.AppendChild(requestBody.CreateElement("createOsdRequest"));

                requestBody.DocumentElement.AppendChild(requestBody.CreateElement("parentId")).InnerText = parentFolderId.ToString();
                if (name != null) requestBody.DocumentElement.AppendChild(requestBody.CreateElement("name")).InnerText = name;
                requestBody.DocumentElement.AppendChild(requestBody.CreateElement("ownerId")).InnerText = ownerId.ToString();
                requestBody.DocumentElement.AppendChild(requestBody.CreateElement("aclId")).InnerText = aclId.ToString();
                requestBody.DocumentElement.AppendChild(requestBody.CreateElement("typeId")).InnerText = typeId.ToString();
                requestBody.DocumentElement.AppendChild(requestBody.CreateElement("languageId")).InnerText = languageId.ToString();
                if (lifecycleStateId != null) requestBody.DocumentElement.AppendChild(requestBody.CreateElement("lifecycleStateId")).InnerText = lifecycleStateId.ToString();
                // TODO: add summary & metasets
                XmlDocument resp = null;
                if (formatId != null)  requestBody.DocumentElement.AppendChild(requestBody.CreateElement("formatId")).InnerText = formatId.ToString();
                resp = _http.PostCommandFileUpload(string.Concat(BaseUrl, "/api/osd/createOsd"), requestBody, contentFn);
                // CheckResponse(resp, requestBody, "/api/osd/createOsd");
                Dictionary<long, C4Object> objs = ParseObjectResponse(resp);
                return objs.Count()==0?null:objs.Values.First();
            }
            catch (WebException ex)
            {
                ThrowException(ex);
                return null;    // code is never reached - without this line, the compiler states that not all code parts return a value 
            }
        }
        //public void GetContent(long id, string contentFn)
        //{
        //    try
        //    {
        //        XmlDocument requestBody = new XmlDocument();
        //        requestBody.AppendChild(requestBody.CreateElement("idRequest"));
        //        requestBody.DocumentElement.AppendChild(requestBody.CreateElement("id")).InnerText = id.ToString();

        //        // Using .Result to synchronously wait for the async operation to complete
        //        _http.PostCommandFileDownloadAsync(string.Concat(BaseUrl, "/api/osd/getContent"), requestBody, contentFn).Wait();

        //        if (!File.Exists(contentFn))
        //            throw new ApplicationException("Failure executing '/api/osd/getContent'");
        //    }
        //    catch (WebException ex)
        //    {
        //        ThrowException(ex);
        //    }
        //    // Consider catching other relevant exceptions here, especially if PostCommandFileDownloadAsync
        //    // might throw exceptions other than WebException.
        //}
        public void GetContent(long id, string contentFn)
        {
            try
            {
                XmlDocument requestBody = new XmlDocument();
                requestBody.AppendChild(requestBody.CreateElement("idRequest"));
                requestBody.DocumentElement.AppendChild(requestBody.CreateElement("id")).InnerText = id.ToString();
                _http.PostCommandFileDownload(string.Concat(BaseUrl, "/api/osd/getContent"), requestBody, contentFn);
                if (!File.Exists(contentFn))
                    throw new ApplicationException("Failure executing '/api/osd/getContent'");

            }
            catch (WebException ex)
            {
                ThrowException(ex);
            }
            return;
        }
        public void SetContent(long id, long formatId, string contentFn)
        {
            try
            {
                XmlDocument requestBody = new XmlDocument();
                requestBody.AppendChild(requestBody.CreateElement("setContentRequest"));

                requestBody.DocumentElement.AppendChild(requestBody.CreateElement("id")).InnerText = id.ToString();
                requestBody.DocumentElement.AppendChild(requestBody.CreateElement("formatId")).InnerText = formatId.ToString();
                XmlDocument resp = _http.PostCommandFileUpload(string.Concat(BaseUrl, "/api/osd/setContent"), requestBody, contentFn);
                // CheckResponse(resp, requestBody, "/api/osd/setContent");
            }
            catch (WebException ex)
            {
                ThrowException(ex);
            }
        }
        public Dictionary<long, C4Object> GetObjectsById(HashSet<long> ids, bool includeCustomMetadata)
        {
            if(ids.Count()>0)
            {
                Dictionary<long, C4Object> result=null;
                try
                {
                    XmlDocument requestBody = new XmlDocument();
                    requestBody.AppendChild(requestBody.CreateElement("osdRequest"));
                    XmlElement idsEl = (XmlElement)requestBody.DocumentElement.AppendChild(requestBody.CreateElement("ids"));
                    foreach(long id in ids) idsEl.AppendChild(requestBody.CreateElement("id")).InnerText=id.ToString();
                    requestBody.DocumentElement.AppendChild(requestBody.CreateElement("includeSummary")).InnerText = "true";
                    requestBody.DocumentElement.AppendChild(requestBody.CreateElement("includeCustomMetadata")).InnerText = includeCustomMetadata?"true":"false";
                    XmlDocument resp = _http.PostCommand(string.Concat(BaseUrl, "/api/osd/getObjectsById"), requestBody);
                    // CheckResponse(resp, requestBody, "/api/ods/getObjectsById");
                    result = ParseObjectResponse(resp);
                }
                catch (WebException ex)
                {
                    ThrowException(ex);
                }
                return result;
            }
            else return new Dictionary<long, C4Object>();
        }
        public C4Metadata GetObjectMeta(long id, HashSet<long> typeIds=null)
        {
            try
            {
                XmlDocument requestBody = new XmlDocument();
                requestBody.AppendChild(requestBody.CreateElement("metaRequest"));
                requestBody.DocumentElement.AppendChild(requestBody.CreateElement("id")).InnerText = id.ToString();
                if(typeIds!=null)
                {
                    XmlElement typeIdsEl = (XmlElement)requestBody.DocumentElement.AppendChild(requestBody.CreateElement("typeIds"));
                    foreach (long typeId in typeIds) typeIdsEl.AppendChild(requestBody.CreateElement("typeId")).InnerText = typeId.ToString();

                }
                XmlDocument resp = _http.PostCommand(string.Concat(BaseUrl, "/api/osd/getMeta"), requestBody);
                // CheckResponse(resp, requestBody, "/api/osd/getMeta");
                return new C4Metadata(resp.DocumentElement.SelectSingleNode("metasets") as XmlElement);
            }
            catch (WebException ex)
            {
                //ThrowException(ex);
                return null;
            }
        }
        public XmlDocument SetObjectSummary(long id, XmlElement summaryEl)       // summaryEl.Name must be summary, no further check
        {
            try
            {
                XmlDocument requestBody = new XmlDocument();
                requestBody.AppendChild(requestBody.CreateElement("setSummaryRequest"));
                requestBody.DocumentElement.AppendChild(requestBody.CreateElement("id")).InnerText = id.ToString();
                //requestBody.DocumentElement.AppendChild(requestBody.ImportNode(summaryEl.CloneNode(true), true));
                requestBody.DocumentElement.AppendChild(requestBody.CreateElement("summary")).InnerText = summaryEl.OuterXml;
                XmlDocument resp = _http.PostCommand(string.Concat(BaseUrl, "/api/osd/setSummary"), requestBody);
                // CheckResponse(resp, requestBody, "/api/osd/setSummary");
                return resp;
            }
            catch (WebException ex)
            {
                ThrowException(ex);
                return null;    // code is never reached - without this line, the compiler states that not all code parts return a value 
            }
        }
        public HashSet<long> SearchObjectIds(string query)
        {
            HashSet<long> result = new HashSet<long>();
            try
            {
                XmlDocument requestBody = new XmlDocument();
                requestBody.AppendChild(requestBody.CreateElement("searchIdsRequest"));
                requestBody.DocumentElement.AppendChild(requestBody.CreateElement("searchType")).InnerText = "OSD";
                requestBody.DocumentElement.AppendChild(requestBody.CreateElement("query")).InnerText = query;
                XmlDocument resp = _http.PostCommand(string.Concat(BaseUrl, "/api/search/objectIds"), requestBody);
                // CheckResponse(resp, requestBody, "/api/search/objectIds");
                foreach(XmlElement idEl in resp.DocumentElement.SelectNodes("osdIds/osdId")) result.Add(long.Parse(idEl.InnerText));
            }
            catch (WebException ex)
            {
                ThrowException(ex);
            }
            return result;
        }
        public HashSet<long> SearchFolderIds(string query)
        {
            HashSet<long> result = new HashSet<long>();
            try
            {
                XmlDocument requestBody = new XmlDocument();
                requestBody.AppendChild(requestBody.CreateElement("searchIdsRequest"));
                requestBody.DocumentElement.AppendChild(requestBody.CreateElement("searchType")).InnerText = "FOLDER";
                requestBody.DocumentElement.AppendChild(requestBody.CreateElement("query")).InnerText = query;
                XmlDocument resp = _http.PostCommand(string.Concat(BaseUrl, "/api/search/objectIds"), requestBody);
                // CheckResponse(resp, requestBody, "/api/search/objectIds");
                foreach (XmlElement idEl in resp.DocumentElement.SelectNodes("folderIds/folderId")) result.Add(long.Parse(idEl.InnerText));
            }
            catch (WebException ex)
            {
                ThrowException(ex);
            }
            return result;
        }
        public Dictionary<long, C4Object> GetObjectsByFolderId(long folderId, bool includeCustomMetadata, VersionMode v)
        {
            Dictionary<long, C4Object> result = null;
            try
            {
                XmlDocument requestBody = new XmlDocument();
                requestBody.AppendChild(requestBody.CreateElement("osdByFolderRequest"));
                requestBody.DocumentElement.AppendChild(requestBody.CreateElement("folderId")).InnerText = folderId.ToString();
                requestBody.DocumentElement.AppendChild(requestBody.CreateElement("includeSummary")).InnerText = "true";
                requestBody.DocumentElement.AppendChild(requestBody.CreateElement("includeCustomMetadata")).InnerText = includeCustomMetadata ? "true" : "false";
                XmlNode vpN = requestBody.DocumentElement.AppendChild(requestBody.CreateElement("versionPredicate"));
                switch(v)
                {
                    case VersionMode.Head: vpN.InnerText = "HEAD"; break;
                    case VersionMode.Branch: vpN.InnerText = "BRANCH"; break;
                    case VersionMode.All: vpN.InnerText = "ALL"; break;
                }
                XmlDocument resp = _http.PostCommand(string.Concat(BaseUrl, "/api/osd/getObjectsByFolderId"), requestBody);
                // CheckResponse(resp, requestBody, "/api/ods/getObjectsByFolderId");
                result = ParseObjectResponse(resp);
            }
            catch (WebException ex)
            {
                ThrowException(ex);
            }
            return result;
        }
        private Dictionary<long, C4Object> ParseObjectResponse(XmlDocument resp)
        {
            Dictionary<long, C4Object> result = new Dictionary<long, C4Object>();
            foreach (XmlElement osdEl in resp.DocumentElement.SelectNodes("osds/osd"))
            {
                C4Object o = new C4Object(osdEl);
                result.Add(o.Id, o);
            }
            // TODO: read links
            // TODO: read references
            return result;
        }

        public XmlDocument DeleteObjects(HashSet<long> ids, bool allVersions, bool withDescendants)
        {
            try
            {
                if(ids.Count>0)
                {
                    XmlDocument requestBody = new XmlDocument();
                    requestBody.AppendChild(requestBody.CreateElement("deleteOsdRequest"));

                    XmlElement idsEl = (XmlElement)requestBody.DocumentElement.AppendChild(requestBody.CreateElement("ids"));
                    foreach (long id in ids)
                    {
                        idsEl.AppendChild(requestBody.CreateElement("id")).InnerText = id.ToString();
                    }
                    requestBody.DocumentElement.AppendChild(requestBody.CreateElement("deleteDescendants")).InnerText = withDescendants?"true":"false";
                    requestBody.DocumentElement.AppendChild(requestBody.CreateElement("deleteAllVersions")).InnerText = allVersions ? "true" : "false";
                    XmlDocument resp = _http.PostCommand(string.Concat(BaseUrl, "/api/osd/delete"), requestBody);
                    // CheckResponse(resp, requestBody, "/api/osd/delete");
                    return resp;
                }
                else return null;
            }
            catch (WebException ex)
            {
                ThrowException(ex);
                return null;    // code is never reached - without this line, the compiler states that not all code parts return a value 
            }
        }
        public XmlDocument LockObject(long id)
        {
            HashSet<long> ids=new HashSet<long>();
            ids.Add(id);
            return LockObject(ids);
        }
		public XmlDocument LockObject(HashSet<long> ids)
		{
			try
			{
				XmlDocument requestBody = new XmlDocument();
				requestBody.AppendChild(requestBody.CreateElement("idRequest"));
				XmlElement idListEl = (XmlElement)requestBody.DocumentElement.AppendChild(requestBody.CreateElement("ids")); ;
                foreach(long id in ids) idListEl.AppendChild(requestBody.CreateElement("id")).InnerText = id.ToString();

				XmlDocument resp = _http.PostCommand(string.Concat(BaseUrl, "/api/osd/lock"), requestBody);
				// CheckResponse(resp, requestBody, "/api/osd/lock");
				return resp;
			}
			catch (WebException ex)
			{
				ThrowException(ex);
				return null;    // code is never reached - without this line, the compiler states that not all code parts return a value 
			}
		}
		public XmlDocument UnlockObject(long id)
		{
			HashSet<long> ids = new HashSet<long>();
			ids.Add(id);
			return UnlockObject(ids);
		}
		public XmlDocument UnlockObject(HashSet<long> ids)
        {
            try
            {
				XmlDocument requestBody = new XmlDocument();
				requestBody.AppendChild(requestBody.CreateElement("idRequest"));
				XmlElement idListEl = (XmlElement)requestBody.DocumentElement.AppendChild(requestBody.CreateElement("ids")); ;
				foreach (long id in ids) idListEl.AppendChild(requestBody.CreateElement("id")).InnerText = id.ToString();

				XmlDocument resp = _http.PostCommand(string.Concat(BaseUrl, "/api/osd/unlock"), requestBody);
                // CheckResponse(resp, requestBody, "/api/osd/unlock");
                return resp;
            }
            catch (WebException ex)
            {
                ThrowException(ex);
                return null;    // code is never reached - without this line, the compiler states that not all code parts return a value 
            }
        }
        public XmlDocument UpdateObject(long id, long? parentFolderId = null, string name = null, long? ownerId = null, long? aclId = null, long? typeId = null, long? languageId = null, bool? metadataChanged = null, bool? contentChanged = null)
        {
            try
            {
                if (parentFolderId != null || name != null || ownerId != null || aclId != null || typeId != null || languageId != null || metadataChanged != null || contentChanged != null)
                {
                    XmlDocument requestBody = new XmlDocument();
                    requestBody.AppendChild(requestBody.CreateElement("updateOsdRequest"));
                    requestBody.DocumentElement.AppendChild(requestBody.CreateElement("updateMetadataChanged")).InnerText = (metadataChanged == null ? "false" : "true");
                    requestBody.DocumentElement.AppendChild(requestBody.CreateElement("updateContentChanged")).InnerText = (contentChanged == null ? "false" : "true");
                    XmlElement objectsEl = (XmlElement)requestBody.DocumentElement.AppendChild(requestBody.CreateElement("osds"));
                    XmlElement objectEl = (XmlElement)objectsEl.AppendChild(requestBody.CreateElement("osd"));
                    objectEl.AppendChild(requestBody.CreateElement("id")).InnerText = id.ToString();
                    if (parentFolderId != null) objectEl.AppendChild(requestBody.CreateElement("parentId")).InnerText = parentFolderId.ToString();
                    if (name != null) objectEl.AppendChild(requestBody.CreateElement("name")).InnerText = name;
                    if (ownerId != null) objectEl.AppendChild(requestBody.CreateElement("ownerId")).InnerText = ownerId.ToString();
                    if (aclId != null) objectEl.AppendChild(requestBody.CreateElement("aclId")).InnerText = aclId.ToString();
                    if (typeId != null) objectEl.AppendChild(requestBody.CreateElement("typeId")).InnerText = typeId.ToString();
                    if (languageId != null) objectEl.AppendChild(requestBody.CreateElement("languageId")).InnerText = languageId.ToString();
                    if (metadataChanged != null) objectEl.AppendChild(requestBody.CreateElement("metadataChanged")).InnerText = (bool)metadataChanged ? "true" : "false";
                    if (contentChanged != null) objectEl.AppendChild(requestBody.CreateElement("contentChanged")).InnerText = (bool)contentChanged ? "true" : "false";

                    XmlDocument resp = _http.PostCommand(string.Concat(BaseUrl, "/api/osd/update"), requestBody);
                    // CheckResponse(resp, requestBody, "/api/osd/update");
                    return resp;
                }
                else return null;
            }
            catch (WebException ex)
            {
                ThrowException(ex);
                return null;    // code is never reached - without this line, the compiler states that not all code parts return a value 
            }
        }
        public C4Object VersionObject(long id, long? formatId = null, string contentFn = null)
        {
            try
            {
                XmlDocument requestBody = new XmlDocument();
                requestBody.AppendChild(requestBody.CreateElement("createNewVersionRequest"));
                requestBody.DocumentElement.AppendChild(requestBody.CreateElement("id")).InnerText = id.ToString();
                requestBody.DocumentElement.AppendChild(requestBody.CreateElement("metaRequests"));

                XmlDocument resp = null;
                if (contentFn == null) resp = _http.PostCommandFileUpload(string.Concat(BaseUrl, "/api/osd/version"), requestBody, null);
                else
                {
                    requestBody.DocumentElement.AppendChild(requestBody.CreateElement("formatId")).InnerText = formatId.ToString();
                    resp = _http.PostCommandFileUpload(string.Concat(BaseUrl, "/api/osd/version"), requestBody, contentFn);
                }
                // CheckResponse(resp, requestBody, "/api/osd/version");
                return ParseObjectResponse(resp).Values.First();
            }
            catch (WebException ex)
            {
                ThrowException(ex);
                return null;    // code is never reached - without this line, the compiler states that not all code parts return a value 
            }
        }
        public XmlDocument CreateObjectMeta(Dictionary<long, HashSet<C4Metaset>> createMetasets)
        {
            try
            {
                XmlDocument requestBody = new XmlDocument();
                requestBody.AppendChild(requestBody.CreateElement("createMetaRequest"));
                XmlElement metasEl = (XmlElement)requestBody.DocumentElement.AppendChild(requestBody.CreateElement("metasets"));

                foreach(long objectId in createMetasets.Keys)
                {
                    foreach(C4Metaset metaset in createMetasets[objectId])
                    {
                        XmlElement metaEl = (XmlElement)metasEl.AppendChild(requestBody.CreateElement("metaset"));
                        metaEl.AppendChild(requestBody.CreateElement("id"));    //empty metaset id element
                        metaEl.AppendChild(requestBody.CreateElement("objectId")).InnerText=objectId.ToString();    
                        metaEl.AppendChild(requestBody.CreateElement("typeId")).InnerText = metaset.TypeId.ToString();
                        metaEl.AppendChild(requestBody.CreateElement("content")).InnerText = metaset.Content.OuterXml;
                    }
                }
                XmlDocument resp = _http.PostCommand(string.Concat(BaseUrl, "/api/osd/createMeta"), requestBody);
                // CheckResponse(resp, requestBody, "/api/osd/createMeta");
                return resp;
            }
            catch (WebException ex)
            {
                ThrowException(ex);
                return null;    // code is never reached - without this line, the compiler states that not all code parts return a value 
            }
        }
        public XmlDocument UpdateObjectMetaContent(Dictionary<long, HashSet<C4Metaset>> updateMetasets)
        {
            try
            {
                XmlDocument requestBody = new XmlDocument();
                requestBody.AppendChild(requestBody.CreateElement("updateMetaRequest"));
                XmlElement metasEl = (XmlElement)requestBody.DocumentElement.AppendChild(requestBody.CreateElement("metasets"));

                foreach (long objectId in updateMetasets.Keys)
                {
                    foreach (C4Metaset metaset in updateMetasets[objectId])
                    {
                        XmlElement metaEl = (XmlElement)metasEl.AppendChild(requestBody.CreateElement("metaset"));
                        metaEl.AppendChild(requestBody.CreateElement("id")).InnerText = ((long)metaset.Id).ToString();
                        metaEl.AppendChild(requestBody.CreateElement("objectId")).InnerText = objectId.ToString();    
                        metaEl.AppendChild(requestBody.CreateElement("typeId")).InnerText = metaset.TypeId.ToString();
                        metaEl.AppendChild(requestBody.CreateElement("content")).InnerText = metaset.Content.OuterXml;
                    }
                }
                XmlDocument resp = _http.PostCommand(string.Concat(BaseUrl, "/api/osd/updateMetaContent"), requestBody);
                // CheckResponse(resp, requestBody, "/api/osd/updateMetaContent");
                return resp;
            }
            catch (WebException ex)
            {
                ThrowException(ex);
                return null;    // code is never reached - without this line, the compiler states that not all code parts return a value 
            }
        }
        public XmlDocument DeleteAllObjectMetadata(HashSet<long> ids)
        {
            try
            {
                // TODO: make this a parameter if use cases arise
                bool ignoreNotFound = true;
                XmlDocument requestBody = new XmlDocument();
                requestBody.AppendChild(requestBody.CreateElement("deleteAllMetasRequest"));

                requestBody.DocumentElement.AppendChild(requestBody.CreateElement("ignoreNotFound")).InnerText = ignoreNotFound?"true":"false";
                XmlElement idsEl = (XmlElement)requestBody.DocumentElement.AppendChild(requestBody.CreateElement("ids"));
                foreach (long id in ids)
                {
                    idsEl.AppendChild(requestBody.CreateElement("id")).InnerText = id.ToString();
                }
                XmlDocument resp = _http.PostCommand(string.Concat(BaseUrl, "/api/osd/deleteAllMetas"), requestBody);

                // TODO: remove condition when server no longer ignores ignoreNotFound
                return resp;
            }
            catch (WebException ex)
            {
                ThrowException(ex);
                return null;    // code is never reached - without this line, the compiler states that not all code parts return a value 
            }
        }
        public XmlDocument DeleteObjectMetasets(HashSet<long> ids)
        {
            try
            {
                // TODO: make this a parameter if use cases arise
                bool ignoreNotFound = true;
                XmlDocument requestBody = new XmlDocument();
                requestBody.AppendChild(requestBody.CreateElement("deleteMetaRequest"));

                requestBody.DocumentElement.AppendChild(requestBody.CreateElement("ignoreNotFound")).InnerText = ignoreNotFound ? "true" : "false";
                XmlElement idsEl = (XmlElement)requestBody.DocumentElement.AppendChild(requestBody.CreateElement("ids"));
                foreach (long id in ids)
                {
                    idsEl.AppendChild(requestBody.CreateElement("id")).InnerText = id.ToString();
                }
                XmlDocument resp = _http.PostCommand(string.Concat(BaseUrl, "/api/osd/deleteMeta"), requestBody);

                // TODO: remove condition when server no longer ignores ignoreNotFound
                return resp;
            }
            catch (WebException ex)
            {
                ThrowException(ex);
                return null;    // code is never reached - without this line, the compiler states that not all code parts return a value 
            }
        }
        #endregion



        // TODO
        #region *** PERMISSIONS ***
        public C4AllPermissions ListPermissions()
        {
            try
            {
                XmlDocument requestBody = new XmlDocument();
                requestBody.AppendChild(requestBody.CreateElement("listPermissionRequest"));
                requestBody.DocumentElement.AppendChild(requestBody.CreateElement("type")).InnerText = "FULL";
                XmlDocument resp = _http.PostCommand(string.Concat(BaseUrl, "/api/permission/list"), requestBody);
                // CheckResponse(resp, requestBody, "/api/permission/list");
                return new C4AllPermissions((XmlElement)resp.DocumentElement.SelectSingleNode("permissions"));
            }
            catch (WebException ex)
            {
                ThrowException(ex);
            }
            return null;    // code is never reached - without this line, the compiler states that not all code parts return a value 
        }
        public C4Permissions GetUserPermissions(long userId, long aclId)
        {
            try
            {
                XmlDocument requestBody = new XmlDocument();
                requestBody.AppendChild(requestBody.CreateElement("userPermissionRequest"));
                requestBody.DocumentElement.AppendChild(requestBody.CreateElement("userId")).InnerText = userId.ToString();
                requestBody.DocumentElement.AppendChild(requestBody.CreateElement("aclId")).InnerText = aclId.ToString();
                XmlDocument resp = _http.PostCommand(string.Concat(BaseUrl, "/api/permission/getUserPermissions"), requestBody);
                // CheckResponse(resp, requestBody, "/api/permission/getUserPermissions");
                return new C4Permissions((XmlElement)resp.DocumentElement.SelectSingleNode("permissions"), AllPermissions);
            }
            catch (WebException ex)
            {
                ThrowException(ex);
            }
            return null;    // code is never reached - without this line, the compiler states that not all code parts return a value 
        }
        #endregion
        #region *** RELATION TYPES ***
        public Dictionary<long, C4RelationType> ListRelationTypes()
        {
            Dictionary<long, C4RelationType> result = new Dictionary<long, C4RelationType>();
            try
            {
                XmlDocument requestBody = new XmlDocument();
                requestBody.AppendChild(requestBody.CreateElement("listRelationTypeRequest"));
                requestBody.DocumentElement.AppendChild(requestBody.CreateElement("type")).InnerText = "FULL";
                XmlDocument resp = _http.PostCommand(string.Concat(BaseUrl, "/api/relationType/list"), requestBody);
                // CheckResponse(resp, requestBody, "/api/relationType/list");
                foreach (XmlElement relationTypeEl in resp.DocumentElement.SelectNodes("relationTypes/relationType"))
                {
                    C4RelationType rt = new C4RelationType(relationTypeEl);
                    result.Add((long)rt.Id, rt);
                }
            }
            catch (WebException ex)
            {
                ThrowException(ex);
            }
            return result;
        }
        public XmlDocument CreateRelationTypes(HashSet<C4RelationType> relationTypes)
        {
            XmlDocument resp = null;
            try
            {
                XmlDocument requestBody = new XmlDocument();
                requestBody.AppendChild(requestBody.CreateElement("createRelationTypeRequest"));
                XmlElement relationTypesEl = (XmlElement)requestBody.DocumentElement.AppendChild(requestBody.CreateElement("relationTypes"));
                foreach (C4RelationType relationType in relationTypes) relationType.AppendAssetEl(relationTypesEl);
                resp = _http.PostCommand(string.Concat(BaseUrl, "/api/relationType/create"), requestBody);
                // CheckResponse(resp, requestBody, "/api/relationType/create");
            }
            catch (WebException ex)
            {
                ThrowException(ex);
            }
            return resp;
        }

        public XmlDocument UpdateRelationTypes(HashSet<C4RelationType> relationTypes)
        {
            XmlDocument resp = null;
            try
            {
                XmlDocument requestBody = new XmlDocument();
                requestBody.AppendChild(requestBody.CreateElement("updateRelationTypeRequest"));
                XmlElement relationTypesEl = (XmlElement)requestBody.DocumentElement.AppendChild(requestBody.CreateElement("relationTypes"));
                foreach (C4RelationType relationType in relationTypes) relationType.AppendAssetEl(relationTypesEl);
                resp = _http.PostCommand(string.Concat(BaseUrl, "/api/relationType/update"), requestBody);
                // CheckResponse(resp, requestBody, "/api/relationType/update");
            }
            catch (WebException ex)
            {
                ThrowException(ex);
            }
            return resp;
        }

        public XmlDocument DeleteRelationTypes(HashSet<long> ids)
        {
            try
            {
                XmlDocument requestBody = new XmlDocument();
                requestBody.AppendChild(requestBody.CreateElement("deleteRelationTypeRequest"));

                requestBody.DocumentElement.AppendChild(requestBody.CreateElement("ignoreNotFound")).InnerText = "false";
                XmlElement idsEl = (XmlElement)requestBody.DocumentElement.AppendChild(requestBody.CreateElement("ids"));
                foreach (long id in ids)
                {
                    idsEl.AppendChild(requestBody.CreateElement("id")).InnerText = id.ToString();
                }
                XmlDocument resp = _http.PostCommand(string.Concat(BaseUrl, "/api/relationType/delete"), requestBody);
                // CheckResponse(resp, requestBody, "/api/relationType/delete");
                return resp;
            }
            catch (WebException ex)
            {
                ThrowException(ex);
                return null;    // code is never reached - without this line, the compiler states that not all code parts return a value 
            }
        }
        #endregion
        #region *** RELATIONS ***
        public Dictionary<long, C4Relation> CreateRelations(HashSet<C4Relation> relations)
        {
            Dictionary<long, C4Relation> result = new Dictionary<long, C4Relation>();
            try
            {
                if(relations.Count>0)
                {
                    XmlDocument requestBody = new XmlDocument();
                    requestBody.AppendChild(requestBody.CreateElement("createRelationRequest"));
                    XmlElement relationsEl = (XmlElement)requestBody.DocumentElement.AppendChild(requestBody.CreateElement("relations"));
                    foreach (C4Relation relation in relations) relation.AppendAssetEl(relationsEl);
                    XmlDocument resp = _http.PostCommand(string.Concat(BaseUrl, "/api/relation/create"), requestBody);
                    // CheckResponse(resp, requestBody, "/api/relation/create");
                    foreach (XmlElement relEl in resp.DocumentElement.SelectNodes("relations/relation"))
                    {
                        C4Relation rel = new C4Relation(relEl);
                        result.Add((long)rel.Id, rel);
                    }
                }
            }
            catch (WebException ex)
            {
                ThrowException(ex);
            }
            return result;
        }

        // the function works, but is not needed and misleading for developers - you normally want SearchRelations
        //public Dictionary<long, C4Relation> GetRelations(bool includeMetadata, HashSet<long> relationTypeIds = null)
        //{
        //    Dictionary<long, C4Relation> result = new Dictionary<long, C4Relation>();
        //    try
        //    {
        //        XmlDocument requestBody = new XmlDocument();
        //        requestBody.AppendChild(requestBody.CreateElement("getRelationRequest"));
        //        XmlElement idsEl = (XmlElement)requestBody.DocumentElement.AppendChild(requestBody.CreateElement("ids"));
        //        if (relationTypeIds != null) foreach (long id in relationTypeIds) idsEl.AppendChild(requestBody.CreateElement("id")).InnerText = id.ToString();
        //        requestBody.DocumentElement.AppendChild(requestBody.CreateElement("includeMetadata")).InnerText = includeMetadata ? "true" : "false";
        //        XmlDocument resp = _http.PostCommand(string.Concat(BaseUrl, "/api/osd/getRelations"), requestBody);
        //        // CheckResponse(resp, requestBody, "/api/osd/getRelations");
        //        foreach (XmlElement relEl in resp.DocumentElement.SelectNodes("relations/relation"))
        //        {
        //            C4Relation rel = new C4Relation(relEl);
        //            result.Add((long)rel.Id, rel);
        //        }
        //    }
        //    catch (WebException ex)
        //    {
        //        ThrowException(ex);
        //    }
        //    return result;
        //}
        public Dictionary<long, C4Relation> SearchRelations(bool includeMetadata, bool orMode, HashSet<long> relationTypeIds = null, HashSet<long> leftIds = null, HashSet<long> rightIds = null)
        {
            Dictionary<long, C4Relation> result = new Dictionary<long, C4Relation>();
            try
            {
                XmlDocument requestBody = new XmlDocument();
                requestBody.AppendChild(requestBody.CreateElement("searchRelationRequest"));
                XmlElement idsEl = (XmlElement)requestBody.DocumentElement.AppendChild(requestBody.CreateElement("relationTypeIds"));
                if (relationTypeIds != null) foreach (long id in relationTypeIds) idsEl.AppendChild(requestBody.CreateElement("relationTypeId")).InnerText = id.ToString();
                XmlElement leftIdsEl = (XmlElement)requestBody.DocumentElement.AppendChild(requestBody.CreateElement("leftIds"));
                if (leftIds != null) foreach (long leftId in leftIds) leftIdsEl.AppendChild(requestBody.CreateElement("leftId")).InnerText = leftId.ToString();
                XmlElement rightIdsEl = (XmlElement)requestBody.DocumentElement.AppendChild(requestBody.CreateElement("rightIds"));
                if (rightIds != null) foreach (long rightId in rightIds) rightIdsEl.AppendChild(requestBody.CreateElement("rightId")).InnerText = rightId.ToString();
                requestBody.DocumentElement.AppendChild(requestBody.CreateElement("includeMetadata")).InnerText = includeMetadata ? "true" : "false";
                requestBody.DocumentElement.AppendChild(requestBody.CreateElement("orMode")).InnerText = orMode ? "true" : "false";
                XmlDocument resp = _http.PostCommand(string.Concat(BaseUrl, "/api/relation/search"), requestBody);
                // CheckResponse(resp, requestBody, "/api/relation/search");
                foreach (XmlElement relEl in resp.DocumentElement.SelectNodes("relations/relation"))
                {
                    C4Relation rel = new C4Relation(relEl);
                    result.Add((long)rel.Id, rel);
                }
            }
            catch (WebException ex)
            {
                ThrowException(ex);
            }
            return result;
        }
        public XmlDocument DeleteRelations(HashSet<long> ids, bool ignoreNotFound)
        {
            if (ids.Count() > 0)
            {
                try
                {
                    XmlDocument requestBody = new XmlDocument();
                    requestBody.AppendChild(requestBody.CreateElement("deleteRelationRequest"));
                    XmlElement idsEl = (XmlElement)requestBody.DocumentElement.AppendChild(requestBody.CreateElement("ids"));
                    foreach (long id in ids) idsEl.AppendChild(requestBody.CreateElement("id")).InnerText = id.ToString();
                    requestBody.DocumentElement.AppendChild(requestBody.CreateElement("ignoreNotFound")).InnerText = ignoreNotFound ? "true" : "false";
                    XmlDocument resp = _http.PostCommand(string.Concat(BaseUrl, "/api/relation/delete"), requestBody);
                    // CheckResponse(resp, requestBody, "/api/relation/delete");
                    return resp;
                }
                catch (WebException ex)
                {
                    ThrowException(ex);
                    return null;    // code is never reached - without this line, the compiler states that not all code parts return a value 
                }
            }
            else return null;
        }
        #endregion
        #region *** UI LANGUAGES ***
        public Dictionary<long, C4Language> ListUiLanguages()
        {
            Dictionary<long, C4Language> result = new Dictionary<long, C4Language>();
            try
            {
                XmlDocument requestBody = new XmlDocument();
                requestBody.AppendChild(requestBody.CreateElement("listUiLanguageRequest"));
                requestBody.DocumentElement.AppendChild(requestBody.CreateElement("type")).InnerText = "FULL";
                XmlDocument resp = _http.PostCommand(string.Concat(BaseUrl, "/api/uiLanguage/list"), requestBody);
                // CheckResponse(resp, requestBody, "/api/uiLanguage/list");
                foreach (XmlElement languageEl in resp.DocumentElement.SelectNodes("uiLanguages/uiLanguage"))
                {
                    C4Language lang = new C4Language(languageEl);
                    result.Add((long)lang.Id, lang);
                }
            }
            catch (WebException ex)
            {
                ThrowException(ex);
            }
            return result;
        }
        public XmlDocument CreateUiLanguages(HashSet<C4Language> languages)
        {
            XmlDocument resp = null;
            try
            {
                XmlDocument requestBody = new XmlDocument();
                requestBody.AppendChild(requestBody.CreateElement("createUiLanguageRequest"));
                XmlElement languagesEl = (XmlElement)requestBody.DocumentElement.AppendChild(requestBody.CreateElement("uiLanguages"));
                foreach (C4Language language in languages) language.AppendAssetEl(languagesEl);
                resp = _http.PostCommand(string.Concat(BaseUrl, "/api/uiLanguage/create"), requestBody);
                // CheckResponse(resp, requestBody, "/api/uiLanguage/create");
            }
            catch (WebException ex)
            {
                ThrowException(ex);
            }
            return resp;
        }

        public XmlDocument UpdateUiLanguages(HashSet<C4Language> languages)
        {
            XmlDocument resp = null;
            try
            {
                XmlDocument requestBody = new XmlDocument();
                requestBody.AppendChild(requestBody.CreateElement("updateUiLanguageRequest"));
                XmlElement languagesEl = (XmlElement)requestBody.DocumentElement.AppendChild(requestBody.CreateElement("uiLanguages"));
                foreach (C4Language language in languages) language.AppendAssetEl(languagesEl);
                resp = _http.PostCommand(string.Concat(BaseUrl, "/api/uiLanguage/update"), requestBody);
                // CheckResponse(resp, requestBody, "/api/uiLanguage/update");
            }
            catch (WebException ex)
            {
                ThrowException(ex);
            }
            return resp;
        }

        public XmlDocument DeleteUiLanguages(HashSet<long> ids)
        {
            XmlDocument resp = null;
            try
            {
                XmlDocument requestBody = new XmlDocument();
                requestBody.AppendChild(requestBody.CreateElement("deleteUiLanguageRequest"));

                requestBody.DocumentElement.AppendChild(requestBody.CreateElement("ignoreNotFound")).InnerText = "false";
                XmlElement idsEl = (XmlElement)requestBody.DocumentElement.AppendChild(requestBody.CreateElement("ids"));
                foreach (long id in ids)
                {
                    idsEl.AppendChild(requestBody.CreateElement("id")).InnerText = id.ToString();
                }
                resp = _http.PostCommand(string.Concat(BaseUrl, "/api/uiLanguage/delete"), requestBody);
                // CheckResponse(resp, requestBody, "/api/uiLanguage/delete");
            }
            catch (WebException ex)
            {
                ThrowException(ex);
            }
            return resp;
        }
        #endregion
        // TODO: get
        #region *** USERS ***
        public XmlDocument CreateUsers(HashSet<C4User> users)
        {
            XmlDocument resp = null;
            try
            {
                XmlDocument requestBody = new XmlDocument();
                requestBody.AppendChild(requestBody.CreateElement("createUserAccountRequest"));
                XmlElement userAccountsEl = (XmlElement)requestBody.DocumentElement.AppendChild(requestBody.CreateElement("userAccounts"));
                foreach (C4User user in users) user.AppendAssetEl(userAccountsEl);
                resp = _http.PostCommand(string.Concat(BaseUrl, "/api/user/create"), requestBody);
                // CheckResponse(resp, requestBody, "/api/user/create");
                // TODO: create the user folders of the users successfully created
            }
            catch (WebException ex)
            {
                ThrowException(ex);
            }
            return resp;
        }
        public Dictionary<long, C4User> ListUsers()
        {
            Dictionary<long, C4User> result = new Dictionary<long, C4User>();
            try
            {
                XmlDocument requestBody = new XmlDocument();
                requestBody.AppendChild(requestBody.CreateXmlDeclaration("1.0", "UTF-8", null));
                requestBody.AppendChild(requestBody.CreateElement("listUserAccountRequest"));
                requestBody.DocumentElement.AppendChild(requestBody.CreateElement("type")).InnerText = "FULL";
                XmlDocument resp = _http.PostCommand(string.Concat(BaseUrl, "/api/user/list"), requestBody);
                // CheckResponse(resp, requestBody, "/api/user/list");
                foreach (XmlElement userEl in resp.DocumentElement.SelectNodes("users/user"))
                {
                    C4User ft = new C4User(userEl, this);
                    result.Add((long)ft.Id, ft);
                }
            }
            catch (WebException ex)
            {
                ThrowException(ex);
            }
            return result;
        }
        public C4User GetUser(long userId)
        {
            return GetUser(null, userId);
        }
        public C4User GetUser(string userName)
        {
            return GetUser(userName, null);
        }
        private C4User GetUser(string userName, long? userId)
        {
            Dictionary<long, C4User> result = new Dictionary<long, C4User>();
            try
            {
                XmlDocument requestBody = new XmlDocument();
                requestBody.AppendChild(requestBody.CreateXmlDeclaration("1.0", "UTF-8", null));
                requestBody.AppendChild(requestBody.CreateElement("getUserAccountRequest"));
                requestBody.DocumentElement.AppendChild(requestBody.CreateElement("userId")).InnerText = userId==null?"":userId.ToString();
                requestBody.DocumentElement.AppendChild(requestBody.CreateElement("username")).InnerText = userName == null ? "" : userName;
                XmlDocument resp = _http.PostCommand(string.Concat(BaseUrl, "/api/user/get"), requestBody);
                // CheckResponse(resp, requestBody, "/api/user/get");
                XmlElement userEl = resp.DocumentElement.SelectSingleNode("users/user") as XmlElement;
                if(userEl != null) return new C4User(userEl, this); 
            }
            catch (WebException ex)
            {
                ThrowException(ex);
            }
            return null;
        }
        public XmlDocument UpdateUsers(HashSet<C4User> users)
        {
            XmlDocument resp = null;
            try
            {
                XmlDocument requestBody = new XmlDocument();
                requestBody.AppendChild(requestBody.CreateElement("updateUserAccountRequest"));
                XmlElement userAccountsEl = (XmlElement)requestBody.DocumentElement.AppendChild(requestBody.CreateElement("userAccounts"));
                foreach (C4User user in users) user.AppendAssetEl(userAccountsEl);
                resp = _http.PostCommand(string.Concat(BaseUrl, "/api/user/update"), requestBody);
                // CheckResponse(resp, requestBody, "/api/user/update");
                // TODO: create the user folders of the users successfully created
            }
            catch (WebException ex)
            {
                ThrowException(ex);
            }
            return resp;
        }
        public XmlDocument DeleteUser(long userId, long assetReceiverId)
        {
            try
            {
                XmlDocument requestBody = new XmlDocument();
                requestBody.AppendChild(requestBody.CreateElement("deleteUserAccountRequest"));
                requestBody.DocumentElement.AppendChild(requestBody.CreateElement("userId")).InnerText = userId.ToString();
                requestBody.DocumentElement.AppendChild(requestBody.CreateElement("assetReceiverId")).InnerText = assetReceiverId.ToString();
                XmlDocument resp = _http.PostCommand(string.Concat(BaseUrl, "/api/user/delete"), requestBody);
                // CheckResponse(resp, requestBody, "/api/user/delete");
                // TODO: delete the user folders of the users successfully deleted
                return resp;
            }
            catch (WebException ex)
            {
                ThrowException(ex);
                return null;    // code is never reached - without this line, the compiler states that not all code parts return a value 
            }
        }
        public XmlDocument SetUserConfig(long userId, XmlDocument userConfig)
        {
            try
            {
                XmlDocument requestBody = new XmlDocument();
                requestBody.AppendChild(requestBody.CreateElement("setUserConfigRequest"));
                requestBody.DocumentElement.AppendChild(requestBody.CreateElement("userId")).InnerText = userId.ToString();
                requestBody.DocumentElement.AppendChild(requestBody.CreateElement("config")).InnerText = userConfig.OuterXml;
                XmlDocument resp = _http.PostCommand(string.Concat(BaseUrl, "/api/user/setConfig"), requestBody);
                // CheckResponse(resp, requestBody, "/api/user/setConfig");
                return resp;
            }
            catch (WebException ex)
            {
                ThrowException(ex);
                return null;    // code is never reached - without this line, the compiler states that not all code parts return a value 
            }
        }
        public XmlDocument SetPassword(long userId, string password)
        {
            try
            {
                XmlDocument requestBody = new XmlDocument();
                requestBody.AppendChild(requestBody.CreateElement("setPasswordRequest"));
                requestBody.DocumentElement.AppendChild(requestBody.CreateElement("userId")).InnerText = userId.ToString();
                requestBody.DocumentElement.AppendChild(requestBody.CreateElement("password")).InnerText = password;
                XmlDocument resp = _http.PostCommand(string.Concat(BaseUrl, "/api/user/setPassword"), requestBody);
                // CheckResponse(resp, requestBody, "/api/user/setPassword");
                return resp;
            }
            catch (WebException ex)
            {
                ThrowException(ex);
                return null;    // code is never reached - without this line, the compiler states that not all code parts return a value 
            }
        }
        #endregion
        private void ThrowException(WebException ex)
        {
            if (ex.Message.Contains("(401)")) throw new ApplicationException("Unauthorized", ex); // TODO: specific exceptions; 
            if (ex.Message.Contains("(403)")) throw new ApplicationException("Forbidden", ex); // TODO: specific exceptions; 
            throw (ex);
        }
   //     private bool // // CheckResponse(XmlDocument resp, XmlDocument req, string apiCommand, bool throwException=true)   // result = true means: no error
   //     {
   //         XmlElement errorsEl = (XmlElement)resp.SelectSingleNode("//errors");
			//StringBuilder message = new StringBuilder();
   //         message.Append("Request: "+req.OuterXml+"\n");
   //         if (errorsEl == null)
   //         {
   //             // TODO: fallback, if other single error elements are returned (if so, needs to be fixed in server)
   //             XmlElement errorEl = (XmlElement)resp.SelectSingleNode("//error");
   //             if (errorEl == null) return true;
			//	message.Append("SINGLE ERROR MESSAGE\n");
			//	message.Append(GetErrorString(apiCommand, errorEl));
   //         }
   //         else
   //         {
   //             foreach (XmlElement errorEl in errorsEl.SelectNodes("error"))
   //             {
   //                 message.Append(GetErrorString(apiCommand, errorEl));
   //             }
   //         }
   //         if(throwException) throw new ApplicationException(string.Concat("Failure executing '", apiCommand, "'", "\nMessages: ", message.ToString()));
   //         return false;
   //     }

   //     private string GetErrorString(string apiCommand, XmlElement errorEl)
   //     {
   //         string result = string.Concat("Code: ", errorEl.SelectSingleNode("code").InnerText, "\nMessage: ", errorEl.SelectSingleNode("message").InnerText, "\nId: ", errorEl.SelectSingleNode("id").InnerText, "\n");
   //         Debug.Print(result);
   //         return result;
   //     }
    }
}
