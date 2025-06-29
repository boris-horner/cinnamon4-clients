﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml;
using C4ServerConnector;
using C4ServerConnector.Assets;

namespace C4Logic.Requests
{
    public class AsyncRequestEngine
    {
        private C4Session _c4s;
        private C4SessionConfiguration _c4sc;
        private C4Folder _requestFolder;
        private long _newRequestLcsId;
        private long _requestObjTypeId;
        public AsyncRequestEngine(C4Session c4s, C4SessionConfiguration c4sc)
        {
            _c4s = c4s;
            _c4sc = c4sc;
            if(!_c4sc.LifecycleStatesByName.ContainsKey(Constants.REQUEST_NEW_LCS)) throw new ApplicationException("Missing lifecycle state '" + Constants.REQUEST_NEW_LCS + "'");
            _newRequestLcsId = (long)_c4sc.LifecycleStatesByName[Constants.REQUEST_NEW_LCS].Id;
            _requestFolder = _c4s.GetFolderByPath(Constants.REQUEST_PATH);
            _requestObjTypeId = (long)_c4sc.ObjectTypesByName[Constants.REQUEST_OBJECT_TYPE].Id;
        }
        public AsyncRequest CreateRequest(long sourceId, string sourcePath, string sourceLang, string channel, string command, long ownerId, XmlElement parametersEl)
        {
            XmlDocument m = new XmlDocument();
            m.AppendChild(m.CreateElement("meta"));
            XmlElement ms = (XmlElement)m.DocumentElement.AppendChild(m.CreateElement("metaset"));

            ms.AppendChild(m.CreateElement("id")).InnerText = sourceId.ToString();
            ms.AppendChild(m.CreateElement("channel")).InnerText = channel;
            ms.AppendChild(m.CreateElement("command")).InnerText = command;
            ms.AppendChild(m.CreateElement("source_path")).InnerText = sourcePath;
            ms.AppendChild(m.CreateElement("language")).InnerText = "und";
            if(parametersEl!=null) ms.AppendChild(m.ImportNode(parametersEl.CloneNode(true),true));

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
        public Dictionary<long, C4Object> FindRequests(XmlElement getRequestsEl)
        {
            return FindRequests(getRequestsEl.SelectSingleNode("channel").InnerText, long.Parse(getRequestsEl.SelectSingleNode("batch_size").InnerText));
        }

        public Dictionary<long, C4Object> FindRequests(string channel, long batchSize)
        {
            string query = "<BooleanQuery>" +
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
            $"        <TermQuery fieldName=\"async_processing_request.channel\">{channel}</TermQuery>" +
            "      </Clause>" +
            "    </BooleanQuery>" +
            "  </Clause>" +
            "</BooleanQuery>";

            HashSet<long> ids = _c4s.SearchObjectIds(query);
            Dictionary<long, C4Object> objs =_c4s.GetObjectsById(ids, false);
            if (objs.Count > batchSize)
            {
                objs = new Dictionary<long, C4Object>(objs.OrderBy(kvp => kvp.Key).Take((int)batchSize));
            }
            return objs;
        }
    }
}
