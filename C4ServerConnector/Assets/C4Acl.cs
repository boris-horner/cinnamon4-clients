using System.Collections.Generic;
using System.Xml;

namespace C4ServerConnector.Assets
{
    public class C4Acl
    {
        public string Name { get; private set; }
        public long? Id { get; private set; }
        public string LocalizedName { get; set; }
        public Dictionary<long, C4AclGroup> AclGroups { get; set; }
        public C4Acl(string name, long? id=null) 
        {
            Name = name;
            Id = id;
        }
        public C4Acl(XmlElement assetEl, C4Session c4s)
        {
            Name = assetEl.SelectSingleNode("name").InnerText;
            XmlNode idN = assetEl.SelectSingleNode("id");
            if (idN == null) Id = null;
            else Id = long.Parse(idN.InnerText);
            if(Id!=null) AclGroups = c4s.ListAclGroupsByGroupOrAcl((long)Id,false);

        }
        public void AppendAssetEl(XmlElement parentEl)
        {
            XmlElement assetEl = (XmlElement)parentEl.AppendChild(parentEl.OwnerDocument.CreateElement("acl"));
            assetEl.AppendChild(parentEl.OwnerDocument.CreateElement("name")).InnerText = Name;
            if (Id != null) assetEl.AppendChild(assetEl.OwnerDocument.CreateElement("id")).InnerText = Id.ToString();
        }
        public override string ToString()
        {
            return LocalizedName == null ? Name : LocalizedName;
        }
    }
}
