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
using System.Linq;
using System;
using System.Xml;
using System.IO;
using C4ServerConnector;
using System.Collections.Generic;

namespace C4ObjectApi.Repository
{
    public class CmnServer : IEquatable<CmnServer>
    {

        private string _host;
        public string CheckoutDir { get;  }
        public string LocksDir { get; }
        public string LocalCertFile { get; }
        public Dictionary<string, string> LocalSettings { get; }

        public CmnServer(string url, string localRoot, string name, string id, string cdnUri, string localCertFile, Dictionary<string, string> localSettings, int getTimeout, string namedUserConfig = null)
        {
            Url = url;
            CdnUrl = cdnUri;
            Name = name;
            Id = id;
            NamedUserConfig = namedUserConfig;
            CheckoutDir = Path.Combine(localRoot, "checkout");
            LocksDir = Path.Combine(localRoot, "locks");
            LocalCertFile = localCertFile;
            LocalSettings = localSettings;
            InitHost();
            GetServerResponse(getTimeout);
        }
        private void GetServerResponse(int getTimeout)
        {
            try
            {
                ServerResponse = new XmlDocument();
                string resp = C4Session.GetRequest(string.Concat(Url, "/cinnamon/info"), LocalCertFile, getTimeout);
                ServerResponse.LoadXml(resp);


                //InitCommands();
                //InitRepositoryName();
                Version = ServerResponse.SelectSingleNode("/*/version").InnerText;
                string[] verSegs_ = Version.Split('.');
                if (verSegs_.Count() > 1)
                {
                    try
                    {
                        PrimaryVersion = int.Parse(verSegs_[0]);
                        SecondaryVersion = int.Parse(verSegs_[1]);
                    }
                    catch (Exception)
                    {
                        PrimaryVersion = 0;
                        SecondaryVersion = 0;
                    }
                }
                else
                {
                    PrimaryVersion = 0;
                    SecondaryVersion = 0;
                }
                XmlNode buildN = ServerResponse.SelectSingleNode("/*/build");
                Build = buildN == null ? 0 : int.Parse(buildN.InnerText);
            }
            catch (Exception ex)
            {
                // Failure initiating server
                throw new ApplicationException(string.Concat("Exception during server connector initialization: ",ex.GetType().ToString(), "\nMessage: ",ex.Message, "\nStack trace: ",ex.StackTrace));
            }
        }
        private void InitHost()
        {
            _host = Url.Replace("https://", "");
            _host = _host.Replace("http://", "");
            string[] segs = _host.Split('/');
            _host = segs[0];
        }

        //private void InitRepositoryName()
        //{
        //    XmlNode n = ServerResponse.SelectSingleNode("/repositories/repository[1]/name");
        //    RepositoryName = n == null ? null : n.InnerText;
        //}

        public CmnSession GetSessionByTicket(string user, string ticket)
        {
            return new CmnSession(user, null, ticket, this, NamedUserConfig);
        }

        public CmnSession GetSession(string user, string pwd)
        {
            return new CmnSession(user, pwd, null, this, NamedUserConfig);
        }
        //public string RepositoryName { get; private set; }
        public string Version { get; private set; }
        public int Build { get; private set; }
        public int PrimaryVersion { get; private set; }
        public int SecondaryVersion { get; private set; }
        public string Id { get; private set; }
        public string Url { get; private set; }
        public string CdnUrl { get; private set; }
        public string Name { get; private set; }
        public string NamedUserConfig { get; private set; }
        public XmlDocument ServerResponse { get; private set; }
        public bool Equals(CmnServer otherServer)
        {
            if (otherServer == null)
                return false;
            else
                return Url == otherServer.Url;
        }

        public static bool operator ==(CmnServer server1, CmnServer server2)
        {
            if (server1 is null)
                return server2 is null;
            else
                return server1.Equals(server2);
        }

        public static bool operator !=(CmnServer server1, CmnServer server2)
        {
            return !(server1 == server2);
        }
    }
}
