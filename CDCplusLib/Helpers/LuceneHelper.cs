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
using CDCplusLib.Interfaces;
using CDCplusLib.TabControls.SearchEditorNodes;
using System.Text;
using System.Xml;

namespace CDCplusLib.Helpers
{
    public class LuceneHelper
    {
        public enum VersionSearchBehaviour { AllVersions, LatestHead, LatestBranches, Folders }
        public static XmlDocument BuildQuery(TreeNode rootNode, VersionSearchBehaviour versionSearchBehaviour)
        {
            XmlDocument result = new XmlDocument();
            result.AppendChild(result.CreateElement("BooleanQuery"));
            if (versionSearchBehaviour != VersionSearchBehaviour.Folders && versionSearchBehaviour !=VersionSearchBehaviour.AllVersions)
            {
                XmlElement versionClauseEl = (XmlElement)result.DocumentElement.AppendChild(result.CreateElement("Clause"));
                versionClauseEl.SetAttribute("occurs", "must");
                XmlElement versionTermEl = (XmlElement)versionClauseEl.AppendChild(result.CreateElement("TermQuery"));
                versionTermEl.SetAttribute("fieldName", versionSearchBehaviour == VersionSearchBehaviour.LatestHead ? "latest_head" : "latest_branch");
                versionTermEl.InnerText = "true";
            }

            XmlElement rootClauseEl = (XmlElement)result.DocumentElement.AppendChild(result.CreateElement("Clause"));
            rootClauseEl.SetAttribute("occurs", "must");

            // TODO: recursively insert into rootClauseEl
            object tag = rootNode.Tag;
            if (tag.GetType() == typeof(SearchOperatorGui))
            {
                ((SearchOperatorGui)tag).AppendToQuery(rootClauseEl, rootNode);
            }
            else // term
            {
                ((ISearchTermGui)tag).AppendToQuery(rootClauseEl);
            }

            return result;
        }

        public static string IndentXml(XmlDocument doc)
        {
            StringBuilder sb = new StringBuilder();
            XmlWriterSettings settings = new XmlWriterSettings
            {
                Indent = true,
                IndentChars = "  ",
                NewLineChars = "\r\n",
                NewLineHandling = NewLineHandling.Replace
            };
            using (XmlWriter writer = XmlWriter.Create(sb, settings))
            {
                doc.Save(writer);
            }
            return sb.ToString();
        }
    }
}
