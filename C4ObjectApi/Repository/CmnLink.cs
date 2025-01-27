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
using C4ObjectApi.Interfaces;
using System.Xml;
using C4ServerConnector.Assets;
using System.Collections.Generic;
using static C4ServerConnector.Assets.C4Link;

namespace C4ObjectApi.Repository
{
    public class CmnLink
    {
        private readonly CmnSession _s;
        private C4Link _c4l;

        public CmnLink(CmnSession s, C4Link c4l)
        {
            _s = s;
            _c4l = c4l;
            Id = (long)c4l.Id;
            Acl = _s.SessionConfig.C4Sc.AclsById[c4l.AclId];
            Owner = _s.SessionConfig.C4Sc.UsersById[c4l.OwnerId];
            ParentId = c4l.ParentId;
        }

        public long Id { get; }
        public C4Acl Acl { get { return _s.SessionConfig.C4Sc.AclsById[_c4l.AclId]; } set { _c4l.AclId = (long)value.Id; } }
        public C4User Owner { get { return _s.SessionConfig.C4Sc.UsersById[_c4l.OwnerId]; } set { _c4l.OwnerId = (long)value.Id; } }
        public long ParentId { get { return _c4l.ParentId; } set { _c4l.ParentId = value; } }
        public bool LatestVersion { get; }
        public void Delete()
        {
            HashSet<long> linkIds = new HashSet<long>();
            linkIds.Add(Id);
            XmlDocument respDoc = _s.CommandSession.DeleteLinks(linkIds, true);
        }
        public void Update(ResolverTypes resolverType, C4Acl newAcl = null, C4User newOwner = null, CmnFolder newParentFolder = null, IRepositoryNode newTarget = null)
        {
            HashSet<C4Link> links = new HashSet<C4Link>();
            _c4l = new C4Link((long)(newAcl == null ? Acl.Id : newAcl.Id), newParentFolder == null ? ParentId : newParentFolder.Id, (long)(newOwner == null ? Owner.Id : newOwner.Id), _c4l.LinkType, resolverType, newTarget == null ? _c4l.RepositoryNodeId : newTarget.Id, Id);
            links.Add(_c4l);
            _s.CommandSession.UpdateLinks(links);
        }
    }
}
