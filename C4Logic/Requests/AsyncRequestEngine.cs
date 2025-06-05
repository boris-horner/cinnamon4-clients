using System;
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
        public AsyncRequestEngine(C4Session c4s, C4SessionConfiguration c4sc)
        {
            _c4s = c4s;
            _c4sc = c4sc;
            _requestFolder = _c4s.GetFolderByPath(Constants.REQUEST_PATH);
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
                                                (long)_c4sc.ObjectTypesByName[C4Logic.Constants.REQUEST_OBJECT_TYPE].Id,
                                                (long)_c4sc.LanguagesByName[sourceLang].Id,
                                                null,
                                                null,
                                                null);

            Dictionary<long, HashSet<C4Metaset>> metasets = new Dictionary<long, HashSet<C4Metaset>>();
            metasets.Add(reqO.Id, new HashSet<C4Metaset>());
            metasets[reqO.Id].Add(new C4Metaset((long)_c4sc.MetasetTypesByName[C4Logic.Constants.REQUEST_METASET_TYPE].Id, reqO.Id, ms));
            _c4s.CreateObjectMeta(metasets);
            return new AsyncRequest(reqO.Id, ms);  
        }
    }
}
