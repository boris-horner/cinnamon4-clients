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
using CDCplusLib.Interfaces;
using System.Xml;
using C4ObjectApi.Repository;
using C4ObjectApi.Operations.Search;
using C4GeneralGui.GuiElements;
using CDCplusLib.EventData;
using C4ObjectApi.Interfaces;

namespace CDCplusLib.DataModel
{
    public class QuickSearch
    {
        private const long SEARCH_BATCH_SIZE = 500L;

        public event SessionWindowRequestEventHandler SessionWindowRequest;

        private HashSet<string> _quickSearchFields;
        private int _maxQuickSearchResultCount;
        private CmnSession _s;
        public void Init(CmnSession s)
        {
            _s = s;
            _quickSearchFields = new HashSet<string>();
            XmlElement qsEl = (XmlElement)s.UserConfig.DocumentElement.SelectSingleNode("classes/quick_search");
            _maxQuickSearchResultCount = qsEl.HasAttribute("maxpagesize") ? int.Parse(qsEl.GetAttribute("maxpagesize")) : -1;
            foreach (XmlElement qsfEl in qsEl.SelectNodes("fieldname"))
            {
                string qsf = qsfEl.InnerText;
                if (!_quickSearchFields.Contains(qsf))
                    _quickSearchFields.Add(qsf);
            }
        }

        public void Search(string text)
        {
            string searchText = text.ToLower().Trim();


            // Search for ID?
            long id;
            Dictionary<long, IRepositoryNode> results = null;
            if (searchText.StartsWith("#"))
            {
                try
                {
                    HashSet<long> ids = new HashSet<long>();
                    foreach (string stSeg in searchText.Split(' '))
                    {
                        if (stSeg.StartsWith("#"))
                        {
                            string idString = stSeg.Substring(1).Trim();
                            if (long.TryParse(idString, out id))
                            {
                                if (!ids.Contains(id))
                                    ids.Add(id);
                            }
                            else
                            {
                                // TODO: error handling: id can't be parsed to long
                            }
                        }
                        else
                        {
                            // TODO: error handling: entry not starting with #
                        }

                    }

                    Dictionary<long, CmnObject> objs = ids.Count()>0?_s.GetObjects(ids, false) :new Dictionary<long, CmnObject>();
                    results = objs.ToDictionary(
                        kvp => kvp.Key,
                        kvp => (IRepositoryNode)kvp.Value
                    );
                }
                catch (Exception ex)
                {
                    // TODO: error message saying that object search has failed
                    // should normally not happen
                    StandardMessage.ShowMessage(Properties.Resources.msgNoObjectFound, StandardMessage.Severity.ErrorMessage, null, ex);
                    return;
                }

            }
            // msgData.ResultListFolders = o_.Session.SearchFolders(lsb.GetQuery(searchMs))
            else
            {
                string query = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>" + "<BooleanQuery>" + "<Clause occurs=\"must\">" + "<BooleanQuery minimumNumberShouldMatch=\"1\">";
                foreach (string stSeg in searchText.Split(' '))
                {
                    if (stSeg.Trim().Length > 0)
                    {
                        foreach (string qsf in _quickSearchFields)
                            query += "<Clause occurs=\"should\"><WildcardQuery fieldName=\"" + qsf + "\">" + stSeg.Trim() + "*</WildcardQuery></Clause>";
                    }
                }

                query += "</BooleanQuery>" + "</Clause>" + "<Clause occurs=\"must\"><TermQuery fieldName=\"latest_head\">true</TermQuery></Clause>" + "</BooleanQuery>";
                SearchObjectsOperation soo = new SearchObjectsOperation(_s, query);
                if (soo.TotalResultCount > _maxQuickSearchResultCount)
                {
                    StandardMessage sm = new StandardMessage(string.Format(Properties.Resources.msgShowAllSearchResults, soo.TotalResultCount.ToString()), StandardMessage.Severity.WarningMessage, null, null);
                    sm.AddButton("ok", Properties.Resources.lblOk, false, true);
                    sm.AddButton("cancel", Properties.Resources.lblCancel, true, false);
                    sm.ShowDialog();
                    Dictionary<long, CmnObject> objs = null;
                    if (sm.ExitButtonKey == "ok")
                    {
                        objs = soo.GetObjects(0L, -1L, SEARCH_BATCH_SIZE);
                    }
                    else
                    {
                        objs = soo.GetObjects(0L, _maxQuickSearchResultCount, SEARCH_BATCH_SIZE);
                    }
                    results = objs.ToDictionary(
                        kvp => kvp.Key,
                        kvp => (IRepositoryNode)kvp.Value
                    );
                }
                else
                {
                    Dictionary<long, CmnObject> objs = soo.GetObjects(batchSize: SEARCH_BATCH_SIZE);
                    results = objs.ToDictionary(
                        kvp => kvp.Key,
                        kvp => (IRepositoryNode)kvp.Value
                    );
                }
            }

            WindowSelectionData wsd = new WindowSelectionData();
            wsd.Selection = results;
            SessionWindowRequest?.Invoke(wsd);
        }
    }
}
