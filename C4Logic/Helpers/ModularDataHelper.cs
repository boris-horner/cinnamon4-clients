using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using C4ServerConnector;
using C4ServerConnector.Assets;

namespace C4Logic.Helpers
{
    public class ModularDataHelper
    {
        public static long CloneToFolder(C4Session c4s, C4SessionConfiguration c4sc, long rootId, long parentFolderId, string subFolderName=null)
        {
            long targetFolderId = parentFolderId;
            HashSet<long> relTypeIds = new HashSet<long> { (long)c4sc.RelationTypesByName[Constants.CHILD_CONTENT_RELATION].Id };

            if (subFolderName!=null)
            {
                // create target folder
                C4Folder targetFolder = new C4Folder(0,
                             subFolderName,
                             (long)c4sc.UsersByName[c4sc.Username].Id,
                             parentFolderId,
                             (long)c4sc.FolderTypesByName["_default_folder_type"].Id,
                             (long)c4sc.AclsByName["_default_acl"].Id);
                Dictionary<long, C4Folder> createdFolders = c4s.CreateFolders(new HashSet<C4Folder> { targetFolder });
                if(createdFolders.Count == 0) throw new ApplicationException("Failed to create folder: " + subFolderName);
                targetFolderId = targetFolder.Id;
            }

            HashSet<long> ids = new HashSet<long>();
            HashSet<C4Relation> rels = new HashSet<C4Relation>();
            AppendObjects(c4s, c4sc, rootId, relTypeIds, ids, rels);
            Dictionary<long, C4Object> objects = c4s.GetObjectsById(ids,false);

            // TODO: queue a feature request that /api/osd/copy returns the source id of which the copy was taken
            HashSet<long> leftIds = new HashSet<long>();
            Dictionary<long, long> targetsBySourceId = new Dictionary<long, long>();
            long targetRootId = 0;
            foreach (long id in ids)
            {
                Dictionary<long, C4Object> copies = c4s.Copy(new HashSet<long> { id }, targetFolderId, c4sc.MetasetTypesById.Keys.ToHashSet<long>());
                if(copies.Count==0) throw new ApplicationException("Failed to copy object with id: " + id.ToString());
                C4Object copy = copies.Values.First();
                if (id == rootId) targetRootId = copy.Id;
                targetsBySourceId[id] = copy.Id;
                if (copy.Name.StartsWith("Copy_"))
                {
                    copy.Name = copy.Name.Substring(5);
                }
                copy.OwnerId = objects[id].OwnerId;
                c4s.UpdateObject(copy.Id, null, copy.Name, copy.OwnerId);
                leftIds.Add(copy.Id);
            }
            Dictionary<long, C4Relation> delRels = c4s.SearchRelations(false, false, relTypeIds, leftIds);

            HashSet<C4Relation> createRels = new HashSet<C4Relation>();
            foreach (C4Relation rel in rels)
            {
                C4Relation newRel = new C4Relation(rel.TypeId, targetsBySourceId[rel.LeftId], targetsBySourceId[rel.RightId], rel.Metadata);
                createRels.Add(newRel);
            }
            c4s.CreateRelations(createRels);
            c4s.DeleteRelations(delRels.Keys.ToHashSet(), true);

            return targetRootId;
        }
        private static void AppendObjects(C4Session c4s,
                                   C4SessionConfiguration c4sc,
                                   long id,
                                   HashSet<long> typeIds,
                                   HashSet<long> ids,
                                   HashSet<C4Relation> rels)
        {
            Debug.Print(" Appending children of " + id.ToString());
            if(!ids.Contains(id))
            {
                ids.Add(id);
                Dictionary<long, C4Relation> childRels = c4s.SearchRelations(true, false, typeIds, new HashSet<long> { id }, null);
                foreach (C4Relation rel in childRels.Values)
                {
                    rels.Add(rel);
                    AppendObjects(c4s, c4sc, rel.RightId, typeIds, ids, rels);
                }
            }
        }

        public static string GetXPath(XmlElement contentEl, XmlDocument topicDoc)
        {
            if (contentEl == null)
                throw new ArgumentNullException(nameof(contentEl));
            if (topicDoc == null)
                throw new ArgumentNullException(nameof(topicDoc));

            // Build the XPath from the element up to the root
            string path = string.Empty;
            XmlNode? current = contentEl;

            while (current != null && current.NodeType == XmlNodeType.Element)
            {
                XmlElement el = (XmlElement)current;

                // Determine the element’s index among siblings with the same name
                int index = 1;
                XmlNode? sibling = el.PreviousSibling;
                while (sibling != null)
                {
                    if (sibling.NodeType == XmlNodeType.Element && sibling.Name == el.Name)
                        index++;
                    sibling = sibling.PreviousSibling;
                }

                string segment = (index == 1 && (el.NextSibling == null ||
                                el.ParentNode == null ||
                                el.ParentNode.SelectNodes(el.Name).Count == 1))
                                ? $"/{el.Name}"
                                : $"/{el.Name}[{index}]";

                path = segment + path;

                // Stop if we’ve reached the document root
                if (el == topicDoc.DocumentElement)
                    break;

                current = el.ParentNode;
            }

            return path;
        }

    }
}
