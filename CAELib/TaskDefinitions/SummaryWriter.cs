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
using CAELib.Log;
using CAELib.Interfaces;
using C4ServerConnector;
using C4ServerConnector.Assets;
using System;

namespace CAELib.TaskDefinitions
{
    public class SummaryWriter : IAsyncTask
    {
        private XmlElement _taskDefEl;
        private Logger _l;
        private C4Session _c4s;
        private C4SessionConfiguration _sc;
        public DateTime NextRun { get; set; }
        public int Interval { get; set; }

        public IC4Node Result { get; set; }
        public bool Status { get; set; }
        public void Init(C4Session c4s, C4SessionConfiguration sc, System.Xml.XmlElement tEl, Logger l)
        {
            _c4s = c4s;
            _sc = sc;
            _taskDefEl = tEl;
            _l = l;
        }

        public void Execute(IC4Node n, ref bool? contentChanged, ref bool? metadataChanged)
        {
            try
            {
                C4Metadata meta = n.NodeType==C4Session.NodeType.Object?_c4s.GetObjectMeta(n.Id): _c4s.GetFolderMeta(n.Id);
                XmlDocument summary = new XmlDocument();
                XmlElement fieldsEl = (XmlElement)_taskDefEl.SelectSingleNode("custom_data/fields");
                C4Session.NodeType at = fieldsEl.GetAttribute("type") == "object" ? C4Session.NodeType.Object : C4Session.NodeType.Folder;
                if(at==n.NodeType)
                {
                    summary.AppendChild(summary.CreateElement("summary"));
                    XmlElement fieldsTargetEl = (XmlElement)summary.DocumentElement.AppendChild(summary.CreateElement("fields"));

                    foreach (XmlElement fieldEl in fieldsEl.SelectNodes("field"))
                    {
                        C4MetasetType msType = _sc.MetasetTypesByName[fieldEl.GetAttribute("metaset_type")];

                        if (fieldEl.HasAttribute("mode") && fieldEl.Attributes["mode"].Value == "multi")
                        {
                            XmlElement field = summary.CreateElement("field");
                            foreach(C4Metaset ms in meta.MetasetsByTypeId[(long)msType.Id])
                            {
                                field.SetAttribute("name", fieldEl.GetAttribute("name"));
                                foreach (XmlNode fieldValueN in ms.Content.SelectSingleNode(fieldEl.InnerText))
                                {
                                    XmlElement value = (XmlElement)field.AppendChild(summary.CreateElement("value"));
                                    value.InnerText = fieldValueN.InnerText;
                                }
                            }
                            if (field.ChildNodes.Count != 0)
                            {
                                fieldsTargetEl.AppendChild(field);
                            }
                        }
                        else
                        {
                            if (meta.MetasetsByTypeId.ContainsKey((long)msType.Id))
                            {
                                if (meta.MetasetsByTypeId[(long)msType.Id].Count > 1)
                                {
                                    // TODO: log if multiple metasets of same type in single mode and refuse to process 
                                    break;
                                }
                                XmlNode fieldValueN = (XmlNode)meta.MetasetsByTypeId[(long)msType.Id].First().Content.SelectSingleNode(fieldEl.InnerText);
                                if (fieldValueN != null)
                                {

                                    XmlElement fieldTargetEl = (XmlElement)fieldsTargetEl.AppendChild(summary.CreateElement("field"));
                                    fieldTargetEl.SetAttribute("name", fieldEl.GetAttribute("name"));
                                    fieldTargetEl.InnerText = fieldValueN.InnerText;
                                }
                            }
                        }
                    }
                    _l.Log(string.Concat("SummaryWriter: summary: creating summary for object: ", n.Id.ToString()));
                    metadataChanged = false;
                    contentChanged = false;
                    //_l.Log("SummaryWriter: summary:" + summary.OuterXml.ToString());
                    if(n.NodeType == C4Session.NodeType.Object) _c4s.SetObjectSummary(n.Id,summary.DocumentElement);
                    else _c4s.SetFolderSummary(n.Id, summary.DocumentElement);
                }

                Status = true;
            }
            catch (Exception ex)
            {
                _l.Log(string.Concat("  SummaryWriter: ", ex.GetType().ToString(), "\n", ex.Message, "\n", ex.StackTrace));
                Status = false;
            }
        }

        public void Execute()
        {
            throw new NotImplementedException();
        }
    }
}