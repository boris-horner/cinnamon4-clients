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
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Xml;
using C4ObjectApi.Repository;
using C4ServerConnector.Assets;

namespace C4ObjectApi.Operations.Import
{
    public class ImportRule
    {
        private readonly string _objecttypeMustMatch;
        private readonly string _objecttypeMustNotMatch;
        private readonly string _languageMustMatch;
        private readonly string _languageMustNotMatch;
        private readonly string _folderpathMustMatch;
        private readonly string _folderpathMustNotMatch;
        private readonly string _applicationMustMatch;
        private readonly string _applicationMustNotMatch;
        private readonly string _errorMessage;

        public ImportRule(CmnSession s, XmlElement ruleEl)
        {
            Name = ruleEl.GetAttribute("name");
            _errorMessage = ruleEl.SelectSingleNode("errormessage").InnerText;
            _objecttypeMustMatch = GetRegex(ruleEl, "objecttype/must_match");
            _objecttypeMustNotMatch = GetRegex(ruleEl, "objecttype/must_not_match");
            _languageMustMatch = GetRegex(ruleEl, "language/must_match");
            _languageMustNotMatch = GetRegex(ruleEl, "language/must_not_match");
            _folderpathMustMatch = GetRegex(ruleEl, "folderpath/must_match");
            _folderpathMustNotMatch = GetRegex(ruleEl, "folderpath/must_not_match");
            _applicationMustMatch = GetRegex(ruleEl, "application/must_match");
            _applicationMustNotMatch = GetRegex(ruleEl, "application/must_not_match");

            ApplyToFormats = new Dictionary<long, C4Format>();
            ApplyToNullFormat = false;
            foreach (XmlElement formatEl in ruleEl.SelectNodes("formats/format"))
            {
                if (string.IsNullOrEmpty(formatEl.InnerText))
                    ApplyToNullFormat = true;
                else
                {
                    C4Format fmt = s.SessionConfig.C4Sc.FormatsByName[formatEl.InnerText];
                    if (!ApplyToFormats.ContainsKey((long)fmt.Id))
                        ApplyToFormats.Add((long)fmt.Id, fmt);
                }
            }
        }
        private string GetRegex(XmlElement ruleEl, string xPath)
        {
            XmlNode regexEl = ruleEl.SelectSingleNode(xPath);
            return regexEl == null ? null : regexEl.InnerText;
        }
        public string Name { get; }
        public Dictionary<long, C4Format> ApplyToFormats { get; }
        public bool ApplyToNullFormat { get; }
        public string GetRuleResult(C4Format fmt, C4ObjectType ot, C4Language lang, CmnFolder targetF)
        {
            // pass Nothing to not perform a check

            if (fmt == null & ApplyToNullFormat || !(fmt == null) && ApplyToFormats.ContainsKey((long)fmt.Id))
            {
                if (!(ot == null))
                {
                    if (!(_objecttypeMustMatch == null))
                    {
                        Match m = Regex.Match(ot.Name, _objecttypeMustMatch);
                        if (!m.Success)
                            return _errorMessage;
                    }
                    if (!(_objecttypeMustNotMatch == null))
                    {
                        Match m = Regex.Match(ot.Name, _objecttypeMustNotMatch);
                        if (m.Success)
                            return _errorMessage;
                    }
                }

                if (!(lang == null))
                {
                    if (!(_languageMustMatch == null))
                    {
                        Match m = Regex.Match(lang.Name, _languageMustMatch);
                        if (!m.Success)
                            return _errorMessage;
                    }
                    if (!(_languageMustNotMatch == null))
                    {
                        Match m = Regex.Match(lang.Name, _languageMustNotMatch);
                        if (m.Success)
                            return _errorMessage;
                    }
                }

                if (!(targetF == null))
                {
                    if (!(_folderpathMustMatch == null))
                    {
                        Match m = Regex.Match(targetF.FolderPath, _folderpathMustMatch);
                        if (!m.Success)
                            return _errorMessage;
                    }
                    if (!(_folderpathMustNotMatch == null))
                    {
                        Match m = Regex.Match(targetF.FolderPath, _folderpathMustNotMatch);
                        if (m.Success)
                            return _errorMessage;
                    }
                }
            }
            return null;
        }
    }
}
