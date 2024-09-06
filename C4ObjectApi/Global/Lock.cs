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
using System.IO;
using System.Xml;

namespace C4ObjectApi.Global
{
    public class Lock
    {
        public long Id { get; }
        public string Filename { get; }
        public string ServerId { get; }
        public string ContentFilename { get; }
        public Dictionary<long, string> DependenciesById { get; }
        public Dictionary<string, long> DependenciesByFilename { get; }
        public Lock(long id, string contentFilename, string locksDir, string serverId)
        {
            Id = id;
            ServerId = serverId;
            Filename = Path.Combine(locksDir, string.Concat(ServerId, ".", id.ToString(), ".xml"));
            DependenciesById = new Dictionary<long, string>();
            DependenciesByFilename = new Dictionary<string, long>();
            ContentFilename = contentFilename;
        }
        public Lock(long id, string locksDir, string serverId)
        {
            Id = id;
            ServerId = serverId;
            Filename = Path.Combine(locksDir, string.Concat(ServerId, ".", id.ToString(), ".xml"));
            DependenciesById = new Dictionary<long, string>();
            DependenciesByFilename = new Dictionary<string, long>();
            XmlDocument depDoc = new XmlDocument();
            depDoc.Load(Filename);
            foreach (XmlElement depEl in depDoc.DocumentElement.SelectNodes("dependencies/dependency"))
            {
                long depId = long.Parse(depEl.GetAttribute("id"));
                DependenciesById.Add(depId, depEl.InnerText);
                DependenciesByFilename.Add(depEl.InnerText, depId);
            }
            ContentFilename = depDoc.DocumentElement.SelectSingleNode("content_file").InnerText;
        }
        public void Save()
        {
            XmlDocument depDoc = new XmlDocument();
            depDoc.AppendChild(depDoc.CreateElement("lock"));
            XmlElement depsEl = (XmlElement)depDoc.DocumentElement.AppendChild(depDoc.CreateElement("dependencies"));
            foreach(long depId in DependenciesById.Keys)
            {
                XmlElement depEl = (XmlElement)depsEl.AppendChild(depDoc.CreateElement("dependency"));
                depEl.SetAttribute("id", depId.ToString());
                depEl.InnerText = DependenciesById[depId];
            }
            depDoc.DocumentElement.AppendChild(depDoc.CreateElement("content_file")).InnerText=ContentFilename;
            depDoc.Save(Filename);
        }
        public void RemoveChild(long childId)
        {
            DependenciesByFilename.Remove(DependenciesById[childId]);
            DependenciesById.Remove(childId);
            Save();
        }
        public void AddChild(long childId, string filename)
        {
            DependenciesById.Add(childId, filename);
            DependenciesByFilename.Add(filename, childId);
            Save();
        }
    }
}
