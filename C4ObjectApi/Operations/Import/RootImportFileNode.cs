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
using System.Collections.Generic;
using System;
using System.Xml;
using C4ObjectApi.Repository;
using System.IO;
using C4ServerConnector.Assets;
using C4ServerConnector;
using ContentAwareness.Interfaces;
using C4Logic;

namespace C4ObjectApi.Operations.Import
{
    public class RootImportFileNode : ImportFileNode
    {
        private readonly CmnSession _s;
        public RootImportFileNode(CmnSession s, string fullFileName, string objectName, C4ObjectType objType, C4Language lang, CmnFolder targetFolder, List<string> validationResults = null) : base(s, fullFileName, objectName, objType, lang, targetFolder, validationResults)
        {
            _s = s;
            MissingReferences = false;
            ImportNodes = new Dictionary<string, ImportFileNode>();
            ImportNodes.Add(FullFileName, this);
            CollectImportNodes(fullFileName, ImportNodes);
        }

        /// <summary>
        /// Gets the child ImportFileNode dictionary.
        /// </summary>
        public Dictionary<string, ImportFileNode> ImportNodes { get; }

        public bool MissingReferences { get; private set; }


        /// <summary>
        /// Add the deep dependencies of a content adaptor to <c>children</c>.
        /// </summary>
        /// <param name="ca">content adaptor</param>
        /// <param name="children">hashtable containing all dependencies of content adaptor ca</param>
        /// <remarks></remarks>
        private void CollectImportNodes(string parentFn, Dictionary<string, ImportFileNode> children)
        {
            C4Format fmt = _s.SessionConfig.C4Sc.FormatsByExtension[Path.GetExtension(parentFn).Substring(1).ToLower()];
            IContent c = Session.GetContentAwarenessModule(fmt);
            if (!(c == null))
            {
                try
                {
                    foreach (string childFn in c.GetReferences(parentFn, _s.CommandSession, _s.SessionConfig.C4Sc))
                    {
                        // Dim childFullFn As String = If(Path.IsPathRooted(fn), fn, CombinePath(Path.GetDirectoryName(FullFilename), fn))
                        string childFullFn = Path.IsPathRooted(childFn) ? childFn : Path.Combine(Path.GetDirectoryName(parentFn), childFn);
                        if (childFullFn.Contains("."))
                        {
                            string ext = Path.GetExtension(childFullFn).Trim().Substring(1).ToLower();
                            if (ext.Contains("#"))
                            {
                                string newExt = ext.Split('#')[0];
                                if (!Session.SessionConfig.C4Sc.FormatsByExtension.ContainsKey(newExt))
                                    throw new ApplicationException("No known format for extension '" + newExt + "'.");
                                childFullFn = childFullFn.Substring(0, childFullFn.Length - ext.Length - 1 + newExt.Length);
                            }
                            else if (!Session.SessionConfig.C4Sc.FormatsByExtension.ContainsKey(ext))
                                throw new ApplicationException("No known format for extension '" + ext + "'.");

                            if (!children.ContainsKey(childFullFn))
                            {
                                C4Format childFormat = Session.SessionConfig.C4Sc.FormatsByExtension[ext];
                                ImportFileNode childIfn = new ImportFileNode(Session, childFullFn, Path.GetFileNameWithoutExtension(childFullFn), ObjType, Language, TargetFolder);
                                if (File.Exists(childFullFn))
                                {
                                    children.Add(childFullFn, childIfn);
                                    childIfn.FileExists = true; // default: false
                                    CollectImportNodes(childFullFn, children);
                                }
                                else
                                {
                                    MissingReferences = true;
                                    children.Add(childFullFn, childIfn);
                                }
                            }
                        }
                        else
                            Debug.Print("Invalid reference to " + childFullFn + " was ignored.");
                    }

                }
                catch (Exception ex)
                {
                    Debug.Print("Exception");
                }
            }
        }


        private void CreateRelations(ImportFileNode ifn)
        {
            C4Format fmt = _s.SessionConfig.C4Sc.FormatsByExtension[Path.GetExtension(ifn.FullFileName).Substring(1).ToLower()];
            IContent c = Session.GetContentAwarenessModule(fmt);
            if (!(c == null))
            {
                HashSet<C4Relation> relations = new HashSet<C4Relation>();
                long typeId = (long)Session.SessionConfig.C4Sc.RelationTypesByName[Constants.CHILD_CONTENT_RELATION].Id;
                foreach (string childFn in c.GetReferences(ifn.FullFileName, _s.CommandSession, _s.SessionConfig.C4Sc))
                {
                    string childFullFn = Path.IsPathRooted(childFn) ? childFn : Path.Combine(Path.GetDirectoryName(ifn.FullFileName), childFn);
                    if (File.Exists(childFullFn))
                    {
                        // Dim childFullFn As String = If(Path.IsPathRooted(childFn), childFn, Path.Combine(Path.GetDirectoryName(ifn.FullFileName), childFn))
                        CmnObject childO = ImportNodes[childFullFn].CinnamonObject;
                        XmlDocument metadata = new XmlDocument();
                        metadata.AppendChild(metadata.CreateElement("metadata"));
                        XmlElement filenameEl = (XmlElement)metadata.DocumentElement.AppendChild(metadata.CreateElement("filename"));
                        filenameEl.InnerText = childFn;
                        relations.Add(new C4Relation(typeId, ifn.CinnamonObject.Id, childO.Id, metadata));
                    }
                }
                Session.CommandSession.CreateRelations(relations);
            }
        }

        public List<string> CumulatedValidationResults { get; set; }
    }
}
