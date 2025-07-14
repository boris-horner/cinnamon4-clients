using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace C4Logic.Requests
{
    public class AsyncRequest
    {
        public long Id { get; private set; }
        private XmlElement _metaset;
        public XmlElement Parameters { get; set; }
        public AsyncRequest(long id, XmlElement metaset)
        {
            Id = id;
            _metaset = metaset;
            Parameters = (XmlElement)metaset.SelectSingleNode("parameters");
        }
    }
}
