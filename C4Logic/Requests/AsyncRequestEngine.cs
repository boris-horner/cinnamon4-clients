using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml;
using C4ServerConnector;
using C4ServerConnector.Assets;
using System.Security.AccessControl;

namespace C4Logic.Requests
{
    public class AsyncRequestEngine
    {
        private C4Session _c4s;
        private C4SessionConfiguration _c4sc;
        private C4Folder _requestFolder;
        private long _newRequestLcsId;
        private long _requestObjTypeId;
        private string _baseRequestQuery;
        public AsyncRequestEngine(C4Session c4s, C4SessionConfiguration c4sc)
        {
            _c4s = c4s;
            _c4sc = c4sc;
            if(!_c4sc.LifecycleStatesByName.ContainsKey(Constants.REQUEST_NEW_LCS)) throw new ApplicationException("Missing lifecycle state '" + Constants.REQUEST_NEW_LCS + "'");
            _newRequestLcsId = (long)_c4sc.LifecycleStatesByName[Constants.REQUEST_NEW_LCS].Id;
            _requestFolder = _c4s.GetFolderByPath(Constants.REQUEST_PATH);
            _requestObjTypeId = (long)_c4sc.ObjectTypesByName[Constants.REQUEST_OBJECT_TYPE].Id;
            _baseRequestQuery = "<BooleanQuery>" +
                            "  <Clause occurs=\"must\">" +
                            "    <TermQuery fieldName=\"latest_head\">true</TermQuery>" +
                            "  </Clause>" +
                            "  <Clause occurs=\"must\">" +
                            "    <BooleanQuery>" +
                            "      <Clause occurs=\"must\">" +
                            $"        <ExactPointQuery fieldName=\"lifecycle_state\" type=\"long\" value=\"{_newRequestLcsId}\" />" +
                            "      </Clause>" +
                            "      <Clause occurs=\"must\">" +
                            $"        <ExactPointQuery fieldName=\"object_type\" type=\"long\" value=\"{_requestObjTypeId}\" />" +
                            "      </Clause>" +
                            "      <Clause occurs=\"must\">" +
                            $"        <TermQuery fieldName=\"async_processing_request.channel\">{{CHANNEL}}</TermQuery>" +
                            "      </Clause>" +
                            "    </BooleanQuery>" +
                            "  </Clause>" +
                            "</BooleanQuery>";
        }
        public AsyncRequest CreateRequest(long sourceId, string sourcePath, string sourceLang, string channel, string command, long ownerId, XmlElement parametersEl)
        {
            XmlElement ms=GetRequestMetasetXml(sourceId, sourcePath, sourceLang, channel, command, ownerId, parametersEl);
            C4Object reqO = _c4s.CreateObject(_requestFolder.Id,
                                                string.Join("_",channel,command,sourceLang,sourceId.ToString()),
                                                ownerId,
                                                (long)_c4sc.AclsByName["_default_acl"].Id,
                                                _requestObjTypeId,
                                                (long)_c4sc.LanguagesByName[sourceLang].Id,
                                                _newRequestLcsId,
                                                null,
                                                null);

            Dictionary<long, HashSet<C4Metaset>> metasets = new Dictionary<long, HashSet<C4Metaset>>();
            metasets.Add(reqO.Id, new HashSet<C4Metaset>());
            metasets[reqO.Id].Add(new C4Metaset((long)_c4sc.MetasetTypesByName[C4Logic.Constants.REQUEST_METASET_TYPE].Id, reqO.Id, ms));
            _c4s.CreateObjectMeta(metasets);
            return new AsyncRequest(reqO.Id, ms);  
        }
        public XmlElement GetRequestMetasetXml(long sourceId, string sourcePath, string sourceLang, string channel, string command, long ownerId, XmlElement parametersEl)
        {
            XmlDocument m = new XmlDocument();
            m.AppendChild(m.CreateElement("meta"));
            XmlElement result = (XmlElement)m.DocumentElement.AppendChild(m.CreateElement("metaset"));

            result.AppendChild(m.CreateElement("id")).InnerText = sourceId.ToString();
            result.AppendChild(m.CreateElement("channel")).InnerText = channel;
            result.AppendChild(m.CreateElement("command")).InnerText = command;
            result.AppendChild(m.CreateElement("source_path")).InnerText = sourcePath;
            result.AppendChild(m.CreateElement("language")).InnerText = "und";
            if (parametersEl != null) result.AppendChild(m.ImportNode(parametersEl.CloneNode(true), true));
            return result;
        }
        public Dictionary<long, C4Object> FindRequests(XmlElement getRequestsEl)
        {
            return FindRequests(getRequestsEl.SelectSingleNode("channel").InnerText, long.Parse(getRequestsEl.SelectSingleNode("batch_size").InnerText));
        }

        public Dictionary<long, C4Object> FindRequests(string channel, long batchSize)
        {
            string query = _baseRequestQuery.Replace("{CHANNEL}", channel);
            try
            {
                HashSet<long> ids = _c4s.SearchObjectIds(query);
                if(ids.Count>0)
                {
                    Dictionary<long, C4Object> objs =_c4s.GetObjectsById(ids, false);
                    if (objs.Count > batchSize)
                    {
                        objs = new Dictionary<long, C4Object>(objs.OrderBy(kvp => kvp.Key).Take((int)batchSize));
                    }
                    return objs;
                }
                else
                {
                    return new Dictionary<long, C4Object>();
                }
            }
            catch(AggregateException ex) 
            {
                // this can happen at times - just ignore and the engine will try again in the next loop
                return new Dictionary<long, C4Object>();
            }
        }
    }
}
