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
using C4ServerConnector;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;

namespace ContentAwareness
{
    public class XmlContent : IContent
    {
        private XmlElement _contentEl;
        private bool _allowLoggingToConsole;

        public void Init(XmlElement contentEl, bool allowLoggingToConsole)
        {
            _contentEl = contentEl;
            Name = _contentEl.GetAttribute("id");
            _allowLoggingToConsole = allowLoggingToConsole;
        }
        public string Name { get; private set; }
        public HashSet<string> GetReferences(string fileName, C4Session c4s, C4SessionConfiguration c4sc, long? id = null)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(fileName);
            return GetReferences(doc);
        }
        private HashSet<string> GetReferences(XmlDocument doc)
        {
            HashSet<string> result = new HashSet<string>();
            // TODO: do inbound processing here
            foreach (XmlElement xPathEl in _contentEl.SelectNodes("custom/children/xPath"))
            {
                foreach (XmlNode n in doc.SelectNodes(xPathEl.InnerText))
                {
                    string r = n.InnerText;
                    // TODO: add ftp:// etc. as required
                    if (!r.ToLower().StartsWith("http://") && !r.ToLower().StartsWith("https://"))
                    {
                        if (r.Contains("#"))
                        {
                            string[] rSegs = r.Split('#');
                            r = r.Substring(0, r.Length - rSegs[rSegs.Count() - 1].Length - 1).Replace('\\', Path.DirectorySeparatorChar).Replace('/', Path.DirectorySeparatorChar);
                        }
                        else
                        {
                            r = r.Replace('\\', Path.DirectorySeparatorChar).Replace('/', Path.DirectorySeparatorChar);
                        }
                        if (!result.Contains(r)) result.Add(r);
                    }

                }
            }
            return result;
        }

        public void RewriteReferences(string fileName, Dictionary<string, string> lookup, C4Session c4s, C4SessionConfiguration c4sc, long? id = null)
        {
            // TODO: support conref
            XmlDocument doc = new XmlDocument();
            doc.XmlResolver = null;
            Log("Opening file: " + fileName);
            doc.Load(fileName);
            RewriteReferences(doc, lookup, c4s, id);
            bool wasReadOnly = (File.GetAttributes(fileName) & FileAttributes.ReadOnly) == FileAttributes.ReadOnly;
            if (wasReadOnly) File.SetAttributes(fileName, File.GetAttributes(fileName) & ~FileAttributes.ReadOnly);
            //doc.Save(fileName);
            using(StreamWriter outStream = System.IO.File.CreateText(fileName))
            {
                doc.Save(outStream);
            }
            Log("Saved file: " + fileName);
            if (wasReadOnly) File.SetAttributes(fileName, File.GetAttributes(fileName) | FileAttributes.ReadOnly);
        }

        public void RewriteReferences(XmlDocument doc, Dictionary<string, string> lookup, C4Session c4s, long? id = null)
        {
            foreach (XmlElement xPathEl in _contentEl.SelectNodes("custom/children/xPath"))
            {
                foreach (XmlNode n in doc.SelectNodes(xPathEl.InnerText))
                {
                    string r = n.InnerText;
                    // TODO: add ftp:// etc. as required
                    if(!r.ToLower().StartsWith("http://") && !r.ToLower().StartsWith("https://"))
                    {
                        if (r.Contains("#"))
                        {
                            Log("Rewriting references - r='" + r + "'");
                            string[] rSegs = r.Split('#');
                            // just replace it in path, not in conref
                            string refFn = r.Substring(0, r.Length - rSegs[rSegs.Count() - 1].Length - 1).Replace('\\', Path.DirectorySeparatorChar).Replace('/', Path.DirectorySeparatorChar);
                            Log(" refFn='" + refFn + "'");
                            if (lookup.ContainsKey(refFn))
                            {
                                Log(" Rewrote " + n.InnerText);
                                n.InnerText = lookup[refFn].Replace('\\', Path.DirectorySeparatorChar).Replace('/', Path.DirectorySeparatorChar) + 
                                    "#" + rSegs[rSegs.Count() - 1];
                                Log(" with " + n.InnerText);

                            }
                            else
                            {
                                // reference not found in lookup
                                Log(" Key " + refFn + " not found - trying sloppy mode");
                                string sloppyRef = RemoveNumberFromRef(refFn);
                                if(sloppyRef!=null)
                                {
                                    if (lookup.ContainsKey(sloppyRef))
                                    {
                                        n.InnerText = lookup[sloppyRef];
                                    }
                                    else
                                    {
                                        bool found = false;
                                        foreach (string key in lookup.Keys)
                                        {
                                            string sloppyKey = RemoveNumberFromRef(key);
                                            if(sloppyKey!=null)
                                            {
                                                if(sloppyRef==sloppyKey)
                                                {
                                                    Log(" Rewrote " + n.InnerText);
                                                    if (lookup.ContainsKey(sloppyKey)) n.InnerText = lookup[sloppyKey].Replace('\\', Path.DirectorySeparatorChar).Replace('/', Path.DirectorySeparatorChar) +
                                                        "#" + rSegs[rSegs.Count() - 1];
                                                    else n.InnerText = sloppyKey.Replace('\\', Path.DirectorySeparatorChar).Replace('/', Path.DirectorySeparatorChar) +
                                                        "#" + rSegs[rSegs.Count() - 1];
                                                    Log(" with " + n.InnerText);
                                                    found = true;
                                                    break;
                                                }
                                            }
                                        }
                                        if (!found)
                                        {
                                            Log(" Sloppy reference " + sloppyRef + " not found - replacing with sloppy reference");
                                            Log(" Rewrote " + n.InnerText);
                                            n.InnerText = sloppyRef.Replace('\\', Path.DirectorySeparatorChar).Replace('/', Path.DirectorySeparatorChar) +
                                                "#" + rSegs[rSegs.Count() - 1];
                                            Log(" with " + n.InnerText);
                                        }

                                    }
                                }
                                else
                                {
                                    Log(" Rewrote " + n.InnerText);
                                    n.InnerText = refFn.Replace('\\', Path.DirectorySeparatorChar).Replace('/', Path.DirectorySeparatorChar) +
                                                    "#" + rSegs[rSegs.Count() - 1];
                                    Log(" with " + n.InnerText);
                                }
                            }
                        }
                        else
                        {
                            //r = r.Replace('\\', Path.DirectorySeparatorChar).Replace('/', Path.DirectorySeparatorChar);
                            if(lookup.ContainsKey(r))
                            {
                                n.InnerText = lookup[r];
                            }
                            else
                            {
                                // reference not found in lookup
                                Log(" Key " + r + " not found - trying sloppy mode");
                                string sloppyRef = RemoveNumberFromRef(r);
                                if (sloppyRef != null)
                                {
                                    if (lookup.ContainsKey(sloppyRef))
                                    {
                                        n.InnerText = lookup[sloppyRef];
                                    }
                                    else
                                    {
                                        //Dictionary<string, string> sloppyLookup = new Dictionary<string, string>();
                                        bool found =false;
                                        foreach (string key in lookup.Keys)
                                        {
                                            string sloppyKey = RemoveNumberFromRef(key);
                                            if (sloppyKey != null)
                                            {
                                                if (sloppyRef == sloppyKey)
                                                {
                                                    Log(" Rewrote " + n.InnerText);
                                                    if (lookup.ContainsKey(sloppyKey)) n.InnerText = lookup[sloppyKey].Replace('\\', Path.DirectorySeparatorChar).Replace('/', Path.DirectorySeparatorChar);
                                                    else n.InnerText = sloppyKey.Replace('\\', Path.DirectorySeparatorChar).Replace('/', Path.DirectorySeparatorChar);
                                                    Log(" with " + n.InnerText);
                                                    found = true;
                                                    break;
                                                }
                                            }
                                        }
                                        if (!found)
                                        {
                                            Log(" Sloppy reference " + sloppyRef + " not found - replacing with sloppy reference");
                                            Log(" Rewrote " + n.InnerText);
                                            n.InnerText = sloppyRef.Replace('\\', Path.DirectorySeparatorChar).Replace('/', Path.DirectorySeparatorChar);
                                            Log(" with " + n.InnerText);
                                        }

                                    }
                                }
                                else
                                {
                                    Log(" Rewrote " + n.InnerText);
                                    n.InnerText = r.Replace('\\', Path.DirectorySeparatorChar).Replace('/', Path.DirectorySeparatorChar);
                                    Log(" with " + n.InnerText);
                                }
                            }
                        }
                    }
                }
            }
            // TODO: do outbound processing here
        }
        private void Log(string text)
        {
            if (_allowLoggingToConsole) Console.WriteLine(text);
        }
        private string RemoveNumberFromRef(string r)
        {
            string[] segs = r.Split('.');
            long rL;
            if(segs.Length>2 && long.TryParse(segs[segs.Length-2],out rL))
            {
                string result = r.Replace("." + segs[segs.Length - 2] + ".", ".");
                return result;
            }
            else
            {
                return null;
            }
        }
    }
}
