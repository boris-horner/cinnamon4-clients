using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using C4ServerConnector;
using C4ServerConnector.Assets;

namespace C4Logic.Helpers
{
    public class StandardMetadataHelper
    {
        public static long AppendHistoryMetaset(C4Session c4s, C4SessionConfiguration c4sc, long objId, string comment)
        {
            XmlDocument historyDoc = new XmlDocument();
            historyDoc.AppendChild(historyDoc.CreateElement("history"));
            historyDoc.DocumentElement.SetAttribute("timestamp", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"));
            historyDoc.DocumentElement.AppendChild(historyDoc.CreateElement("comment")).InnerText = comment;
            C4Metaset history = new C4Metaset((long)c4sc.MetasetTypesByName[Constants.HISTORY_METASET_TYPE].Id, objId, historyDoc.DocumentElement);
            XmlDocument result = c4s.CreateObjectMeta(new Dictionary<long, HashSet<C4Metaset>> { { objId, new HashSet<C4Metaset> { history } } });
            XmlNodeList nl = result.DocumentElement.SelectNodes("metasets/metaset/id");
            if(nl.Count==1) return long.Parse(nl[0].InnerText);
            else throw new ApplicationException("Failed to create history metaset for object " + objId.ToString());
        }
    }
}
