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

namespace C4ServerConnector.Assets
{
    public class C4ChangeTrigger
    {
        public string Name { get; private set; }
        public long? Id { get; private set; }
        public string TriggerType { get; private set; }
        public bool Active { get; private set; }
        public bool PreTrigger { get; private set; }
        public bool PostTrigger { get; private set; }
        public bool PostCommitTrigger { get; private set; }
        public bool CopyFileContent { get; private set; }
        public long Ranking { get; private set; }
        public XmlDocument Config { get; private set; }
        public string Controller { get; private set; }
        public string Action { get; private set; }
        public C4ChangeTrigger(string name, string triggerType, bool active, bool pre, bool post, bool postCommit, bool copyFileContent, long ranking, string controller, string action, XmlDocument config, long? id=null) 
        {
            Name = name;
            TriggerType = triggerType;
            Active = active;
            PreTrigger = pre;
            PostTrigger = post;
            PostCommitTrigger = postCommit;
            CopyFileContent = copyFileContent;
            Ranking = ranking;
            Controller = controller;
            Action = action;
            Config = config;
            Id = id;
        }
        public C4ChangeTrigger(XmlElement assetEl)
        {
            Name = assetEl.SelectSingleNode("name").InnerText;
            TriggerType = assetEl.SelectSingleNode("triggerType").InnerText;
            Active = assetEl.SelectSingleNode("active").InnerText=="true";
            PreTrigger = assetEl.SelectSingleNode("preTrigger").InnerText == "true";
            PostTrigger = assetEl.SelectSingleNode("postTrigger").InnerText == "true";
            PostCommitTrigger = assetEl.SelectSingleNode("postCommitTrigger").InnerText == "true";
            CopyFileContent = assetEl.SelectSingleNode("copyFileContent").InnerText == "true";
            Ranking = long.Parse(assetEl.SelectSingleNode("ranking").InnerText);
            Controller = assetEl.SelectSingleNode("controller").InnerText;
            Action = assetEl.SelectSingleNode("action").InnerText;
            Config = new XmlDocument();
            Config.LoadXml(assetEl.SelectSingleNode("config").InnerText);
            XmlNode idN = assetEl.SelectSingleNode("id");
            if (idN == null) Id = null;
            else Id = long.Parse(idN.InnerText);
        }
        public void AppendAssetEl(XmlElement parentEl)
        {
            XmlElement assetEl = (XmlElement)parentEl.AppendChild(parentEl.OwnerDocument.CreateElement("changeTrigger"));
            assetEl.AppendChild(parentEl.OwnerDocument.CreateElement("name")).InnerText = Name;
            assetEl.AppendChild(parentEl.OwnerDocument.CreateElement("triggerType")).InnerText = TriggerType;
            assetEl.AppendChild(parentEl.OwnerDocument.CreateElement("active")).InnerText = Active ? "true" : "false";
            assetEl.AppendChild(parentEl.OwnerDocument.CreateElement("preTrigger")).InnerText = PreTrigger ? "true" : "false";
            assetEl.AppendChild(parentEl.OwnerDocument.CreateElement("postTrigger")).InnerText = PostTrigger ? "true" : "false";
            assetEl.AppendChild(parentEl.OwnerDocument.CreateElement("postCommitTrigger")).InnerText = PostCommitTrigger ? "true" : "false";
            assetEl.AppendChild(parentEl.OwnerDocument.CreateElement("copyFileContent")).InnerText = CopyFileContent ? "true" : "false";
            assetEl.AppendChild(parentEl.OwnerDocument.CreateElement("ranking")).InnerText = Ranking.ToString();
            assetEl.AppendChild(parentEl.OwnerDocument.CreateElement("controller")).InnerText = Controller;
            assetEl.AppendChild(parentEl.OwnerDocument.CreateElement("action")).InnerText = Action;
            assetEl.AppendChild(parentEl.OwnerDocument.CreateElement("config")).InnerText = Config.OuterXml;
            if (Id != null) assetEl.AppendChild(assetEl.OwnerDocument.CreateElement("id")).InnerText = Id.ToString();
        }
        public override string ToString()
        {
            return Name;
        }
    }
}
