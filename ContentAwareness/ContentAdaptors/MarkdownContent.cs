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
using ContentAwareness.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml;

namespace ContentAwareness.ContentAdaptors
{
    public class MarkdownContent : IContent
    {
        private XmlElement _contentEl;
        private bool _allowLoggingToConsole;
        private HashSet<string> _regexs;

        public void Init(XmlElement contentEl, bool allowLoggingToConsole)
        {
            _contentEl = contentEl;
            Name = _contentEl.GetAttribute("id");
            _allowLoggingToConsole = allowLoggingToConsole;
            _regexs = new HashSet<string>();
            foreach (XmlElement regexEl in _contentEl.SelectNodes("custom/children/regex"))
            {
                if(!_regexs.Contains(regexEl.InnerText)) _regexs.Add(regexEl.InnerText);
            }

        }
        public string Name { get; private set; }
        public HashSet<string> GetReferences(string fileName, C4Session c4s, C4SessionConfiguration c4sc, long? id = null)
        {
            HashSet<string> result = new HashSet<string>();
            string[] lines = File.ReadAllLines(fileName);

            foreach (string line in lines)
            {
                foreach (string pattern in _regexs)
                {
                    Regex regex = new Regex(pattern);
                    MatchCollection matches = regex.Matches(line);

                    foreach (Match match in matches)
                    {
                        if (match.Groups.Count > 1)
                        {
                            string childPath = match.Groups[1].Value;
                            result.Add(childPath);
                        }
                    }
                }
            }
            return result;
        }
        public void RewriteReferences(string fileName, Dictionary<string, string> lookup, C4Session c4s, C4SessionConfiguration c4sc, long? id = null)
        {
            string[] lines = File.ReadAllLines(fileName);
            List<string> updatedLines = new List<string>();

            bool changed = false;
            foreach (string line in lines)
            {
                string updatedLine = line;

                foreach (string pattern in _regexs)
                {
                    Regex regex = new Regex(pattern);
                    MatchCollection matches = regex.Matches(line);

                    foreach (Match match in matches)
                    {
                        if (match.Groups.Count > 1)
                        {
                            string childPath = match.Groups[1].Value;
                            if (lookup.ContainsKey(childPath))
                            {
                                // Replace the matched path with the value from the lookup dictionary
                                string newPath = lookup[childPath];
                                updatedLine = updatedLine.Replace(childPath, newPath);
                                changed = true;
                            }
                        }
                    }
                }

                // Add the (potentially updated) line to the list
                updatedLines.Add(updatedLine);
            }

            if (changed)
            {
                // Check if the file is write-protected
                FileAttributes originalAttributes = File.GetAttributes(fileName);
                bool wasReadOnly = originalAttributes.HasFlag(FileAttributes.ReadOnly);

                try
                {
                    // Remove write protection if necessary
                    if (wasReadOnly)
                    {
                        File.SetAttributes(fileName, originalAttributes & ~FileAttributes.ReadOnly);
                    }

                    // Write the updated lines back to the file
                    File.WriteAllLines(fileName, updatedLines);
                }
                finally
                {
                    // Restore the original file attributes
                    if (wasReadOnly)
                    {
                        File.SetAttributes(fileName, originalAttributes);
                    }
                }
            }
        }
        private void Log(string text)
        {
            if (_allowLoggingToConsole) Console.WriteLine(text);
        }
    }
}
