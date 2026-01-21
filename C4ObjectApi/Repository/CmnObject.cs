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
using System.Diagnostics;
using System.Linq;
using System.Collections.Generic;
using System;
using System.IO;
using System.Xml;
using C4ObjectApi.Helpers;
using C4ObjectApi.Exceptions;
using C4ObjectApi.Interfaces;
using System.Text;
using C4ObjectApi.Global;
using C4ServerConnector;
using C4ServerConnector.Assets;
using ContentAwareness.Interfaces;
using C4Logic;

namespace C4ObjectApi.Repository
{
    public class CmnObject : IRepositoryNode, IEquatable<CmnObject>
    {
        public enum ChildExportPolicy
        {
            ObjectOnly,
            ObjectAndDirectChildren,
            ObjectAndDescendants
        }

        public enum ChildLinkUpdateBehaviour
        {
            DontUpdate,
            // UpdateToLatestInBranches
            UpdateToLatestInHead
        }

        public enum ObjectMetadata
        {
            AclId,
            Name,
            ParentFolderId,
            OwnerId,
            Metadata,
            TypeId,
            Language
        }


        public C4Object C4O { get; private set; }

        public CmnObject(CmnSession session, C4Object c4o)
        {
            Session = session;
            C4O = c4o;
            if (c4o.Link != null) Link = new CmnLink(session, c4o.Link);
            // TODO: read permissions only when actually needed
            Permissions = Session.SessionConfig.GetCombinedPermissions(Session.SessionConfig.C4Sc.AclsById[C4O.AclId], Session.User.Id == C4O.OwnerId, Session.IsSuperuser);
            LocalPath = Locked != null ? (Session.LocksMgr.Locks.ContainsKey(Id)?Session.LocksMgr.Locks[Id].ContentFilename:null) : "" ;
        }
        public long Id { get { return C4O.Id; } }
        public string Name
        {
            get
            {
                return C4O.Name;
            }
        }
        public long ContentSize { get { return C4O.ContentSize; } }
        public long? PredecessorId { get { return C4O.PredecessorId; } }
        public long RootId { get { return C4O.RootId; } }
        public C4User Creator { get { return Session.SessionConfig.C4Sc.UsersById[C4O.CreatorId]; } }      // make this writable by admin
        public C4User Modifier { get { return Session.SessionConfig.C4Sc.UsersById[C4O.ModifierId]; } }      // make this writable by admin
        public C4User Owner
        { 
            get 
            { 
                return Session.SessionConfig.C4Sc.UsersById[C4O.OwnerId]; 
            }
        }
        public C4User Locked { get { return C4O.LockedId == null ? null : Session.SessionConfig.C4Sc.UsersById[(long)C4O.LockedId]; } }
        public long? LockedId { get { return C4O.LockedId; } }
        public DateTime Created { get { return C4O.Created; } }      // make this writable by admin
        public DateTime Modified { get { return C4O.Modified; } }      // make this writable by admin
        public C4Language Language 
        { 
            get 
            { 
                return Session.SessionConfig.C4Sc.LanguagesById[C4O.LanguageId]; 
            }
        }     // make this writable
        public C4Acl Acl 
        { 
            get 
            { 
                return Session.SessionConfig.C4Sc.AclsById[C4O.AclId]; 
            } 
        }
        public long ParentId 
        { 
            get 
            { 
                return C4O.ParentId; 
            }
        }    // make this writable
        public CmnFolder Parent
        {
            get
            {
                return Session.GetFolder(ParentId, false);
            }
        }
        public C4Format Format 
        { 
            get 
            { 
                return C4O.FormatId == null ? null : Session.SessionConfig.C4Sc.FormatsById[(long)C4O.FormatId]; 
            } 
        }     // make this writable on set content etc.
        public C4ObjectType ObjectType 
        { 
            get 
            { 
                return Session.SessionConfig.C4Sc.ObjectTypesById[C4O.TypeId]; 
            }
        }     // make this writable
        public bool LatestHead { get { return C4O.LatestHead; } }
        public bool LatestBranch { get { return C4O.LatestBranch; } }
        public bool ContentChanged { get { return C4O.ContentChanged; } }      // make this writable by admin
        public bool MetadataChanged { get { return C4O.MetadataChanged; } }      // make this writable by admin
        public string Version { get { return C4O.Version; } }
        public C4LifecycleState LifecycleState 
        { 
            get 
            { 
                return C4O.LifecycleStateId == null ? null : Session.SessionConfig.C4Sc.LifecycleStatesById[(long)C4O.LifecycleStateId]; 
            } 
            set
            {
                if(C4O.LifecycleStateId == null)
                {
                    if(value!=null) Session.CommandSession.AttachLifecycle(Id, value.LifecycleId, (long)value.Id);
                    // else: nothing to do
                }
                else
                {
                    if (value == null) Session.CommandSession.DetachLifecycle(Id);
                    else
                    {
                        if (value.LifecycleId != Session.SessionConfig.C4Sc.LifecycleStatesById[(long)C4O.LifecycleStateId].LifecycleId)
                        {
                            Session.CommandSession.DetachLifecycle(Id);
                            Session.CommandSession.AttachLifecycle(Id, value.LifecycleId, (long)value.Id);
                        }
                        else Session.CommandSession.ChangeState(Id, (long)value.Id);
                    }
                }
                C4O.LifecycleStateId = value==null?null:value.Id;
            }
        }
        public XmlElement Summary { get { return C4O.Summary; } }      // make this writable by admin
        public C4Permissions Permissions { get; }
        public CmnSession Session { get; }

        public CmnObject LatestHeadVersion
        {
            get
            {
                return Session.SearchSingleObject("<BooleanQuery><Clause occurs=\"must\"><ExactPointQuery type=\"long\" fieldName=\"root\" value=\"" + RootId.ToString() + "\"/></Clause><Clause occurs=\"must\"><TermQuery fieldName=\"latest_head\">true</TermQuery></Clause></BooleanQuery>", false);
            }
        }

        private Dictionary<long, XmlDocument> GetRelMdLookup(Dictionary<long, C4Relation> rels)
        {
            Dictionary<long, XmlDocument> relMdLookup = new Dictionary<long, XmlDocument>();
            foreach (C4Relation rel in rels.Values)
                relMdLookup.Add(rel.RightId, rel.Metadata);
            return relMdLookup;
        }
        private void CreateChildRelationsSameVersion(HashSet<string> refList, Dictionary<string, long> lookup, long id, Dictionary<long, C4Relation> rels, Dictionary<long, XmlDocument> relMdLookup)
        {
            long relTypeId = (long)Session.SessionConfig.C4Sc.RelationTypesByName[Constants.CHILD_CONTENT_RELATION].Id;

            // delete child_content relations
            HashSet<long> ids = new HashSet<long>();
            foreach (C4Relation rel in rels.Values) if (rel.TypeId == relTypeId) ids.Add((long)rel.Id);
            Session.CommandSession.DeleteRelations(ids, true);


            // create dependencies
            {
                HashSet<C4Relation> relations = new HashSet<C4Relation>();
                foreach (string childFilename in refList)
                {
                    Debug.Print(childFilename);
                    string fullChildFn=Path.Combine(Session.Server.CheckoutDir, childFilename);
                    if (lookup.ContainsKey(fullChildFn))
                    {
                        long rightId = lookup[fullChildFn];
                        XmlElement metadata;
                        if (relMdLookup.ContainsKey(rightId))
                        {
                            metadata = relMdLookup[rightId].DocumentElement;
                            if (metadata.SelectSingleNode("filename") == null)
                                // Dim metaEl As XmlElement = CType(metaDoc.SelectSingleNode("/meta"), XmlElement)
                                metadata.AppendChild(metadata.OwnerDocument.CreateElement("filename")).InnerText = Path.GetFileName(childFilename);
                            else
                                metadata.SelectSingleNode("filename").InnerText = Path.GetFileName(childFilename);
                        }
                        else
                        {
                            XmlDocument m = new XmlDocument();
                            metadata = (XmlElement)m.AppendChild(m.CreateElement("metadata"));
                            XmlElement filenameEl = (XmlElement)metadata.AppendChild(metadata.OwnerDocument.CreateElement("filename"));
                            filenameEl.InnerText = Path.GetFileName(childFilename);
                        }
                        relations.Add(new C4Relation((long)Session.SessionConfig.C4Sc.RelationTypesByName[Constants.CHILD_CONTENT_RELATION].Id, id, rightId, metadata.OwnerDocument));
                    }
                }
                Session.CommandSession.CreateRelations(relations);
            }
        }
        private void CreateChildRelationsNewVersion(HashSet<string> refList, Dictionary<string, long> lookup, long oldId, long newId, Dictionary<long, C4Relation> oldRels, Dictionary<long, XmlDocument> relMdLookup)
        {
            long relTypeId = (long)Session.SessionConfig.C4Sc.RelationTypesByName[Constants.CHILD_CONTENT_RELATION].Id;
            Dictionary<long, C4Relation> newRels = Session.GetRelations(false, null, newId);

            // delete child_content relations on new object
            HashSet<long> ids = new HashSet<long>();
            foreach (C4Relation rel in newRels.Values) if (rel.TypeId == relTypeId) ids.Add((long)rel.Id);
            Session.CommandSession.DeleteRelations(ids, true);

            // create dependencies
            HashSet<C4Relation> relations = new HashSet<C4Relation>();
            foreach (string childFilename in refList)
            {
                Debug.Print(childFilename);
                string fullChildFn = Path.Combine(Session.Server.CheckoutDir, childFilename);
                if (lookup.ContainsKey(fullChildFn))
                {
                    long rightId = lookup[fullChildFn];
                    XmlElement metadata;
                    if (relMdLookup.ContainsKey(rightId))
                    {
                        metadata = relMdLookup[rightId].DocumentElement;
                        if (metadata.SelectSingleNode("filename") == null)
                            // Dim metaEl As XmlElement = CType(metaDoc.SelectSingleNode("/meta"), XmlElement)
                            metadata.AppendChild(metadata.OwnerDocument.CreateElement("filename")).InnerText = Path.GetFileName(childFilename);
                        else
                            metadata.SelectSingleNode("filename").InnerText = Path.GetFileName(childFilename);
                    }
                    else
                    {
                        XmlDocument m = new XmlDocument();
                        metadata = (XmlElement)m.AppendChild(m.CreateElement("metadata"));
                        XmlElement filenameEl = (XmlElement)metadata.AppendChild(metadata.OwnerDocument.CreateElement("filename"));
                        filenameEl.InnerText = Path.GetFileName(childFilename);
                    }
                    relations.Add(new C4Relation((long)Session.SessionConfig.C4Sc.RelationTypesByName[Constants.CHILD_CONTENT_RELATION].Id, newId, rightId, metadata.OwnerDocument));
                }
            }
            Session.CommandSession.CreateRelations(relations);
        }
        private HashSet<string> CheckChildren(HashSet<string> refList, Dictionary<string, long> lookup)
        {
            HashSet<string> result = new HashSet<string>();
            foreach (string dd in refList)
            {
                if(dd.Trim().Length>0)
                {
                    string lookupKey = Path.Combine(Session.Server.CheckoutDir, dd);
                    if (!lookup.ContainsKey(lookupKey))
                    {
                        if (!result.Contains(dd))
                            result.Add(dd);
                    }
                }

            }
            return result;
        }
        public CmnObject CheckinFromFile(string filename, C4Format format, bool newVersion, ChildLinkUpdateBehaviour childUpdatePolicy)
        {
            return newVersion?CheckinFromFileNewVersion(filename, format, childUpdatePolicy) : CheckinFromFileSameVersion(filename, format, childUpdatePolicy);
        }
        private CmnObject CheckinFromFileNewVersion(string filename, C4Format format, ChildLinkUpdateBehaviour childUpdatePolicy)
        {
            CmnObject result = this;
            IContent c = Parent.FolderPath.StartsWith("/.data/translations") ? null : Session.GetContentAwarenessModule(Format);
            if (c == null)
            {
                Unlock();
                result = VersionCmd(filename, format, Name);
            }
            else
            {
                Dictionary<string, long> childFileToIdLookup = Session.LocksMgr.Locks[Id].DependenciesByFilename;

                foreach (long childId in Session.LocksMgr.Locks[Id].DependenciesById.Keys)

                    if (childUpdatePolicy != (int)ChildLinkUpdateBehaviour.DontUpdate)
                    childFileToIdLookup = UpdateChildLinkLookupTable(Session, childFileToIdLookup, childUpdatePolicy);

                Dictionary<long, C4Relation> allChildRels = Session.GetRelations(false, null, Id);
                Dictionary<long, C4Relation> allContentChildRels =new Dictionary<long, C4Relation>();
                foreach(C4Relation rel in allChildRels.Values) if(rel.TypeId== Session.SessionConfig.C4Sc.RelationTypesByName[Constants.CHILD_CONTENT_RELATION].Id) allContentChildRels.Add((long)rel.Id, rel);
                Dictionary<long, XmlDocument> childIdRelMdLookup = GetRelMdLookup(allContentChildRels);


                HashSet<string> refList = c.GetReferences(filename, Session.CommandSession, Session.SessionConfig.C4Sc);
                HashSet<string> unresolvedReferences = CheckChildren(refList, childFileToIdLookup);
                if (unresolvedReferences.Count > 0)
                {
                    CmnDataModelException ex = new CmnDataModelException(DataModelErrorCodes.UNRESOLVED_INBOUND_REFERENCES, "Unresolved inbound references");
                    ex.Data["unresolved_refs"] = unresolvedReferences;
                    throw ex;
                }
                Unlock();
                result = VersionCmd(filename, format, Name);
                CreateChildRelationsNewVersion(refList, childFileToIdLookup, Id, result.Id, allChildRels, childIdRelMdLookup);
            }

            RemoveFromLocal();
            return result;
        }

        private CmnObject CheckinFromFileSameVersion(string filename, C4Format format, ChildLinkUpdateBehaviour childUpdatePolicy)
        {
            CmnObject result = this;
            IContent c = Parent.FolderPath.StartsWith("/.data/translations") ? null : Session.GetContentAwarenessModule(Format);
            if (c == null)
            {
                SetContent(filename, format);
                Unlock();
            }
            else
            {
                Dictionary<string, long> childFileToIdLookup = Session.LocksMgr.Locks[Id].DependenciesByFilename;

                foreach (long childId in Session.LocksMgr.Locks[Id].DependenciesById.Keys)

                    if (childUpdatePolicy != (int)ChildLinkUpdateBehaviour.DontUpdate)
                        childFileToIdLookup = UpdateChildLinkLookupTable(Session, childFileToIdLookup, childUpdatePolicy);

                Dictionary<long, C4Relation> allChildRels = Session.GetRelations(false, null, Id);
                Dictionary<long, C4Relation> allContentChildRels = new Dictionary<long, C4Relation>();
                foreach (C4Relation rel in allChildRels.Values) if (rel.TypeId == Session.SessionConfig.C4Sc.RelationTypesByName[Constants.CHILD_CONTENT_RELATION].Id) allContentChildRels.Add((long)rel.Id, rel);
                Dictionary<long, XmlDocument> childIdRelMdLookup = GetRelMdLookup(allContentChildRels);


                HashSet<string> refList = c.GetReferences(filename, Session.CommandSession, Session.SessionConfig.C4Sc);
                HashSet<string> unresolvedReferences = CheckChildren(refList, childFileToIdLookup);
                if (unresolvedReferences.Count > 0)
                {
                    CmnDataModelException ex = new CmnDataModelException(DataModelErrorCodes.UNRESOLVED_INBOUND_REFERENCES, "Unresolved inbound references");
                    ex.Data["unresolved_refs"] = unresolvedReferences;
                    throw ex;
                }
                SetContent(filename, format);
                Unlock();
                CreateChildRelationsSameVersion(refList, childFileToIdLookup, result.Id, allChildRels, childIdRelMdLookup);
            }

            RemoveFromLocal();
            return result;
        }
        private Dictionary<string, long> UpdateChildLinkLookupTable(CmnSession s, Dictionary<string, long> before, ChildLinkUpdateBehaviour updateMode)
        {
            switch (updateMode)
            {
                case ChildLinkUpdateBehaviour.DontUpdate:
                    {
                        return before;
                    }

                case ChildLinkUpdateBehaviour.UpdateToLatestInHead:
                    {
                        Dictionary<string, long> result = new Dictionary<string, long>();
                        foreach (string fn in before.Keys)
                        {
                            long origId = long.Parse(before[fn].ToString());
                            CmnObject origObj = ApiHelper.GetObject(Session, origId);
                            string query = "<?xml version=\"1.0\" encoding=\"UTF-8\"?><BooleanQuery><Clause occurs=\"must\"><ExactPointQuery type=\"long\" fieldName=\"root\" value=\"" + origObj.RootId.ToString() + "\"/></Clause><Clause occurs=\"must\"><TermQuery fieldName=\"latest_head\">true</TermQuery></Clause></BooleanQuery>";
                            CmnObject obj = s.SearchSingleObject(query, false);
                            result.Add(fn, obj.Id);
                        }
                        return result;
                    }

                default:
                    {
                        return before;
                    }
            }
        }

        public CmnObject VersionCmd(string inputFilename, C4Format format, string name)
        {
            if (!Session.AllowBranching)
            {
                Refresh();
                if (!LatestHead)
                    throw new CmnDataModelException(DataModelErrorCodes.BRANCHING_NOT_ALLOWED,"Failure versioning object  " + Id.ToString() + ". Branching is disabled. ");
            }

            C4Object c4o = null;
            if (format == null)
                c4o = Session.CommandSession.VersionObject(Id, null, null);
            else
                c4o = Session.CommandSession.VersionObject(Id, (long)format.Id, inputFilename);

            HashSet<long> ids = new HashSet<long>();
            ids.Add(c4o.Id);
            CmnObject newO = new CmnObject(Session, Session.CommandSession.GetObjectsById(ids, false).Values.First());
            return newO;
        }

        public void SetContent(string inputFilename, C4Format format)
        {
            if (File.Exists(inputFilename) & !(format == null))
            {
                C4O.FormatId = format.Id;
                Session.CommandSession.SetContent(Id, (long)format.Id, inputFilename);

                Refresh();
            }
            else
            {
            }
        }
        public Dictionary<long, CmnObject> GetAllVersions()
        {
            string query = "<?xml version=\"1.0\" encoding=\"UTF-8\"?><BooleanQuery><Clause occurs=\"must\"><ExactPointQuery type=\"long\" fieldName=\"root\" value=\"" + RootId.ToString() + "\"/></Clause></BooleanQuery>";
            return Session.SearchObjects(query);
        }
        public void Delete(bool allVersions, bool withDescendants)
        {
            HashSet<long> ids = new HashSet<long>();
            ids.Add(Id);
            XmlDocument respDoc = Session.CommandSession.DeleteObjects(ids, allVersions, withDescendants);
        }
        public void CancelCheckout()
        {
            Unlock();
            RemoveFromLocal();
        }
        private void RemoveFromLocal()
        {
            try
            {
                if (LocalPath != null && LocalPath.Trim().Length > 0)
                    File.Delete(LocalPath);
                // File.Delete(localPath_ & ".ca")
                if(Session.LocksMgr.Locks.ContainsKey(Id)) foreach (long childId in Session.LocksMgr.Locks[Id].DependenciesById.Keys)
                {
                    string fileName = Session.LocksMgr.Locks[Id].DependenciesById[childId];
                    File.SetAttributes(fileName, File.GetAttributes(fileName) & ~FileAttributes.ReadOnly);
                    File.Delete(fileName);
                }
                Session.LocksMgr.RemoveLock(Id);
                LocalPath = "";
            }
            catch (Exception ex)
            {
            }
        }

        private Dictionary<string, string> GetOldToNew(long parentId, Dictionary<long, string> objs)
        {
            Dictionary<string, string> result = new Dictionary<string, string>();
            C4RelationType childRt = Session.SessionConfig.C4Sc.RelationTypesByName[Constants.CHILD_CONTENT_RELATION];
            Dictionary<long, C4Relation> rels = Session.GetRelations(false, childRt, parentId);
            // Dim ids As IList(Of long) = New List(Of long)
            string dirSep = Path.DirectorySeparatorChar.ToString();
            foreach (C4Relation rel in rels.Values)
            {
                if (objs.ContainsKey(rel.RightId))
                {
                    string localPath = objs[rel.RightId];
                    XmlNode fnN = rel.Metadata.SelectSingleNode("//filename");
                    if (fnN != null)
                    {
                        string key = fnN.InnerText; //.Replace(@"\", dirSep).Replace("/", dirSep);
                        if (!result.ContainsKey(key))
                            result.Add(key, Path.GetFileName(localPath));
                    }
                }
            }
            return result;
        }

        private HashSet<long> GetDependencies(long parentId, bool recursive, HashSet<long> l = null)
        {
            if (l == null)
                l = new HashSet<long>();

            // get deep dependency relations for parentId
            HashSet<long> col = GetRelIdsFromRep(parentId, Session.SessionConfig.C4Sc.RelationTypesByName[Constants.CHILD_CONTENT_RELATION]);
            // for each relation:
            // -add it if it's missing
            // -resolve recursive dependencies
            foreach (long childId in col)
            {
                if (!l.Contains(childId))
                {
                    l.Add(childId);
                    if (recursive)
                    {
                        HashSet<long> colSub = GetDependencies(childId, recursive, l);
                        foreach (long subChildId in colSub)
                        {
                            if (!l.Contains(subChildId))
                                l.Add(subChildId);
                        }
                    }
                }
            }

            return l;
        }
        private HashSet<long> GetRelIdsFromRep(long parentId, C4RelationType relType)
        {
            HashSet<long> result = new HashSet<long>();
            foreach (C4Relation rel in Session.GetRelations(false, relType, parentId).Values)
            {
                if (!result.Contains(rel.RightId))
                    result.Add(rel.RightId);
            }

            return result;
        }

        public ExportResults Checkout(ChildExportPolicy cep, 
                                      IExportResolver exportResolver = null,
                                      C4Language targetLanguage = null)
        {
            try
            {
                ExportResults result = Export(cep, false, null, exportResolver, targetLanguage);
                result.RootObject.Lock();
                Session.LocksMgr.AddLock(result.RootObject.Id, result.RootObject.LocalPath, result.IdToPathLookup);
                return result;
            }
            catch (Exception ex)
            {
                if(Locked!=null) Unlock();
                Exception e = new Exception("Failure checking out " + Id.ToString() + "\n" + ex.Message + "\n" + ex.StackTrace);
                e.Source = "CmnObject.Checkout";
                e.Data.Add("ErrorCode", "FailureCheckingOut");
                e.Data.Add("OriginalMessage", ex.Message + "\n" + ex.StackTrace);
                e.Data.Add("Para_001", Id.ToString());
                throw e;
            }
        }
        public ExportResults Export(ChildExportPolicy cep,
                                                bool writeProtect,
                                                string targetPath = null,
                                                IExportResolver exportResolver = null,
                                                C4Language targetLanguage = null)
        {
            if(targetPath==null)
            {
                return ExportInternal(Session.Server.CheckoutDir, false, cep, writeProtect, exportResolver, targetLanguage);
            }
            else
            {
                return ExportInternal(targetPath, false, cep, writeProtect, exportResolver, targetLanguage);
            }
            // no path - goes to checkout folder - never overwrite
        }

        private ExportResults ExportInternal(string targetPath, 
                                             bool overwrite, 
                                             ChildExportPolicy cep, 
                                             bool writeProtect, 
                                             IExportResolver exportResolver = null,
                                             C4Language targetLanguage = null)
        {
            try
            {
                //Dictionary<long, string> objs = new Dictionary<long, string>();
                ExportResults result = new ExportResults();
                // pass 1: get all root objects
                Dictionary<string, string> hrefFnToId = new Dictionary<string, string>();
                Dictionary<long, HashSet<long>> objTree = new Dictionary<long, HashSet<long>>();
                HashSet<long> allIds=new HashSet<long>();
                allIds.Add(Id);
                HashSet<long> relTypeIds = new HashSet<long>();
                relTypeIds.Add((long)Session.SessionConfig.C4Sc.RelationTypesByName[Constants.CHILD_CONTENT_RELATION].Id);
                AppendObjects(Id, relTypeIds, cep, ref objTree, ref hrefFnToId, ref allIds);

                Dictionary<long, long> addIds = new Dictionary<long, long>();
                if (Id!=null && !addIds.ContainsKey(Id)) addIds.Add(Id, Id);
                //foreach(string id in hrefFnToId.Values)
                foreach (long id in allIds) if (!addIds.ContainsKey(id)) addIds.Add(id, id);
                Dictionary<long, CmnObject> rootObjects = Session.GetObjects(new HashSet<long>(addIds.Keys), false);
                result.IdCount = addIds.Count;
                result.ObjectCount = rootObjects.Values.Count;
                if(result.IdCount != result.ObjectCount)
                {
                    foreach(long checkId in addIds.Keys)
                    {
                        if (!rootObjects.ContainsKey(checkId)) result.MissingObjects.Add(checkId);
                    }
                }

                // here, custom filters can be attached - one example is breaking off export with children of referenced DITA maps when checking out a parent map
                // in most cases, there is no export resolver configured and this step is skipped
                if(exportResolver!=null) exportResolver.Filter(Id, ref objTree, rootObjects);

                // pass 2: get language variants
                Dictionary<long, CmnObject> langObjects = new Dictionary<long, CmnObject>();
                if (targetLanguage==null)
                {
                    foreach (long sourceLangId in rootObjects.Keys) langObjects.Add(sourceLangId, rootObjects[sourceLangId]);
                    //foreach (long sourceLangId in objTree.Keys) langObjects.Add(sourceLangId, rootObjects[sourceLangId]);
                }
                else 
                {
                    // find ids of variant candidates and append objects where (by its language) no variant will be used
                    List<long> langVariantRequiredIds = new List<long>();
                    //foreach (long sourceLangId in objTree.Keys)
                    foreach (long sourceLangId in rootObjects.Keys)
                    {
                        CmnObject sourceLangO = rootObjects[sourceLangId];
                        if(sourceLangO.Language.Name != "zxx" && sourceLangO.Language.Name != "mul" && sourceLangO.Language.Name != "und" && sourceLangO.Language != targetLanguage)
                        {
                            // find correct language variant
                            langVariantRequiredIds.Add(sourceLangId);
                        }
                        else
                        {
                            // add original object
                            langObjects.Add(sourceLangId, sourceLangO);
                        }
                    }

                    // build query and find language variants
                    if(langVariantRequiredIds.Count>0)
                    {
                        StringBuilder querySb = new StringBuilder();
                        querySb.Append("<BooleanQuery><Clause occurs=\"must\"><TermQuery fieldName=\"latest_head\">true</TermQuery></Clause>" +
                            "<Clause occurs=\"must\"><BooleanQuery minimumNumberShouldMatch=\"1\">");
                        foreach(long lvrId in langVariantRequiredIds)
                        {
                            querySb.Append("<Clause occurs=\"should\"><TermQuery fieldName=\"name\">");
                            querySb.Append(lvrId.ToString());
                            querySb.Append("</TermQuery></Clause>");
                        }
                        querySb.Append("</BooleanQuery></Clause><Clause occurs=\"must\"><WildcardQuery fieldName=\"folderpath\">/root/.data/translations*</WildcardQuery></Clause><Clause occurs=\"must\"><ExactPointQuery type=\"long\" fieldName=\"language\" value=\"" + targetLanguage.Id.ToString() + "\"/></Clause></BooleanQuery>");

                        // verify which variants were found and what their status is
                        Dictionary<long, CmnObject> srBySourceId = new Dictionary<long, CmnObject>();
                        foreach(CmnObject varObj in Session.SearchObjects(querySb.ToString()).Values)
                        {
                            long sourceObjId = long.Parse(varObj.Name);
                            if(srBySourceId.ContainsKey(sourceObjId))
                            {
                                Debug.Print(sourceObjId.ToString() + " exists");
                            }
                            else srBySourceId.Add(sourceObjId, varObj);
                        }
                        foreach (long lvrId in langVariantRequiredIds)
                        {
                            if(srBySourceId.ContainsKey(lvrId))
                            {
                                CmnObject lvrO = srBySourceId[lvrId];
                                langObjects.Add(lvrId, lvrO);
                                if(lvrO.LifecycleState==null)
                                {
                                    result.UntranslatedVariants.Add(lvrId, rootObjects[lvrId]);
                                }
                                else switch(lvrO.LifecycleState.Name)
                                {
                                    case "lcs.trans.01untranslated":
                                            result.UntranslatedVariants.Add(lvrId, rootObjects[lvrId]);
                                            break;
                                    case "lcs.trans.02translated":
                                            result.UnacceptedVariants.Add(lvrId, rootObjects[lvrId]);
                                            break;
                                    case "lcs.trans.03accepted":
                                            // nothing to do
                                        break;
                                    default:
                                            result.UntranslatedVariants.Add(lvrId, rootObjects[lvrId]);
                                            break;
                                }
                            }
                            else
                            {
                                result.MissingVariants.Add(lvrId, rootObjects[lvrId]);
                            }
                        }
                    }

                }

                // only for formats that have a content awareness module
                foreach(long sourceId in langObjects.Keys)
                {
                    if(langObjects[sourceId]==rootObjects[sourceId]) langObjects[sourceId].GetContentInternal(targetPath, overwrite, writeProtect);
                    else langObjects[sourceId].GetContentInternal(targetPath, overwrite, writeProtect, rootObjects[sourceId].Name);
                    result.IdToPathLookup.Add(sourceId, langObjects[sourceId].LocalPath);
                }


                foreach (long sourceId in result.IdToPathLookup.Keys)
                {
                    IContent c = Session.GetContentAwarenessModule(Session.SessionConfig.C4Sc.FormatsByExtension[Path.GetExtension(result.IdToPathLookup[sourceId]).Substring(1).ToLower()]);
                    if (!(c == null))
                    {
                        Dictionary<string, string> oldToNew = GetOldToNew(sourceId, result.IdToPathLookup);
                        c.RewriteReferences(result.IdToPathLookup[sourceId], oldToNew, Session.CommandSession, Session.SessionConfig.C4Sc, sourceId);
                    }
                }
                result.RootObject = langObjects[Id];
                LocalPath = langObjects[Id].LocalPath;
                return result;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(string.Format("Caught unexpected exception: Type: {0}" + "\n"
                                            + "Message: {1}" + "\n"
                                            + "{2}", ex.GetType().ToString(), ex.Message, ex.StackTrace));
                                            LocalPath = "";
                throw;
            }
        }
        private void AppendObjects(long id,
                                   HashSet<long> typeIds,
                                   ChildExportPolicy cep,
                                   ref Dictionary<long, HashSet<long>> objTree,
                                   ref Dictionary<string, string> hrefFnToId,
                                   ref HashSet<long> allIds)
        {
            if (!objTree.ContainsKey(id)) objTree.Add(id, new HashSet<long>());
            HashSet<long> leftIds = new HashSet<long>();
            leftIds.Add(id);
            Dictionary<long, C4Relation> rels = Session.CommandSession.SearchRelations(true, false, typeIds, leftIds, null);
            if(cep== ChildExportPolicy.ObjectAndDescendants || cep == ChildExportPolicy.ObjectAndDirectChildren)
            {
                foreach (C4Relation rel in rels.Values)
                {
                    XmlNode fileNameN = rel.Metadata.SelectSingleNode("filename");
                    bool idIsMissing = !allIds.Contains(rel.RightId);
                    if (idIsMissing) allIds.Add(rel.RightId);
                    if (fileNameN != null && fileNameN.InnerText.Length > 0 && !hrefFnToId.ContainsKey(fileNameN.InnerText)) hrefFnToId.Add(fileNameN.InnerText, rel.RightId.ToString());
                    if (!objTree[id].Contains(rel.RightId)) objTree[id].Add(rel.RightId);
                    if (cep != ChildExportPolicy.ObjectOnly && idIsMissing)
                        AppendObjects(rel.RightId,
                                        typeIds,
                                        cep == ChildExportPolicy.ObjectAndDirectChildren ? ChildExportPolicy.ObjectOnly : ChildExportPolicy.ObjectAndDescendants,
                                        ref objTree,
                                        ref hrefFnToId,
                                        ref allIds);
                }

            }
        }



        public void SetLocalFileTime(string targetFilename = null)
        {
            File.SetLastWriteTime(targetFilename == null ? LocalPath : targetFilename, Modified);
            File.SetCreationTime(targetFilename == null ? LocalPath : targetFilename, Created);
        }
        public IContent ContentAwarenessModule
        {
            get
            {
                return Session.GetContentAwarenessModule(Format);
            }
        }
        public void Lock()
        {
            XmlDocument respDoc = Session.CommandSession.LockObject(Id);
            C4O.LockedId = Session.User.Id;
        }

        public void Unlock()
        {
            XmlDocument respDoc = Session.CommandSession.UnlockObject(Id);
            C4O.LockedId = null;
        }

        public void GetContent(bool writeProtect, string targetFilename = null)
        {
            // target filename is specified
            if (targetFilename == null)
                targetFilename = GetLocalFilename();

            Session.CommandSession.GetContent(Id, targetFilename);
            LocalPath = targetFilename;
            SetLocalFileTime();
            if (writeProtect)
                File.SetAttributes(targetFilename, File.GetAttributes(targetFilename) | FileAttributes.ReadOnly);
        }
        private string GetContentInternal(string targetFolder, bool overwrite, bool writeProtect, string nameOverride = null)
        {
            //string targetFilename = "";
            if (overwrite)
            {
                LocalPath = Path.Combine(targetFolder, ReplaceInvalidPathChars(nameOverride==null?Name:nameOverride) + "." + Format.Extension);
                GetContent(writeProtect, LocalPath);
            }
            else
            {
                LocalPath = GetLocalFilename(targetFolder, nameOverride);
                GetContent(writeProtect, LocalPath);
            }
            //LocalPath = targetFilename;
            // If Not suppressTimeCorrection Then SetLocalFileTime()
            return LocalPath;
        }
        private string ReplaceInvalidPathChars(string orig)
        {
            string result = orig;
            foreach (char c in Path.GetInvalidFileNameChars())
                result = result.Replace(c.ToString(), "_");// replace invalid characters with an underscore
            return result;
        }
        private string GetLocalFilename(string targetPath = null, string nameOverride=null)
        {
            string ext = Format.Extension;
            // take checkout dir from server if target path is empty
            if (targetPath==null)
                targetPath = Session.Server.CheckoutDir;

            string stem = ReplaceInvalidPathChars(nameOverride == null ? Name : nameOverride);
            // stem = targetPath + stem
            string testName = Path.Combine(targetPath, stem + "." + ext);

            // append random number if file already exists
            int i = 0;
            while (File.Exists(testName))
            {
                testName = Path.Combine(targetPath, stem + "." + i.ToString() + "." + ext);
                ++i;
            }
            return testName;
        }

        public CmnObject Copy(CmnFolder targetFolder, HashSet<long> copyMetasetTypeIds = null)
        {
            HashSet<long> copyIds = new HashSet<long>();
            copyIds.Add(Id);
            Dictionary<long, C4Object> copyObjects = Session.CommandSession.Copy(copyIds, targetFolder.Id, copyMetasetTypeIds == null ? Session.SessionConfig.CopyMetasetTypeIds : copyMetasetTypeIds);    
            return new CmnObject(Session, copyObjects.Values.First());
        }
        public void CopyToExisting(CmnObject targetO, bool copyContent, HashSet<long> copyMetasetTypeIds = null)
        {
            Session.CommandSession.CopyToExisting(Id, targetO.Id, copyContent, copyMetasetTypeIds == null ? Session.SessionConfig.CopyMetasetTypeIds : copyMetasetTypeIds);
        }
        public string LocalPath { get; private set; }
        public void Refresh()
        {
            HashSet<long> ids = new HashSet<long>();
            ids.Add(Id);
            C4O = Session.CommandSession.GetObjectsById(ids, false).Values.First();
        }

        public override string ToString()
        {
            return Name;
        }

        public bool Equals(CmnObject otherObject)
        {
            if (otherObject == null)
                return false;
            else
                return Id == otherObject.Id;
        }
        public static bool operator ==(CmnObject object1, CmnObject object2)
        {
            if (object1 is null)
                return object2 is null;
            else
                return object1.Equals(object2);
        }

        public static bool operator !=(CmnObject object1, CmnObject object2)
        {
            return !(object1 == object2);
        }
        public CmnLink Link { get; }
    }
}
