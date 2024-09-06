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
using System;
using C4ObjectApi.Repository;
using System.Xml;
using C4ServerConnector.Assets;

namespace C4ObjectApi.Operations.Import
{
    public class ImportRules
    {
        private readonly CmnSession _s;
        private readonly Dictionary<string, ImportRule> _rules;
        public ImportRules(CmnSession s)
        {
            _s = s;
            _rules = new Dictionary<string, ImportRule>();
            try
            {
                XmlDocument ce = _s.GetConfigEntry("import.format.filters");
                if (ce != null)
                {
                    foreach (XmlElement ruleEl in ce.SelectNodes("/config/rules/rule"))
                    {
                        ImportRule rule = new ImportRule(_s, ruleEl);
                        _rules.Add(rule.Name, rule);
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        public List<string> GetRulesResult(C4Format fmt, C4ObjectType ot, C4Language lang, CmnFolder targetF)
        {
            List<string> result = new List<string>();
            foreach (ImportRule r in _rules.Values)
            {
                string message = r.GetRuleResult(fmt, ot, lang, targetF);
                if (!(message == null))
                    result.Add(message);
            }
            return result;
        }
    }
}
