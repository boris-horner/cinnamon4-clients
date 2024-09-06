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
using CDCplusLib.Messages;
using CDCplusLib.Messages.SessionWindowRequestData;
using System.Xml;
using C4ObjectApi.Repository;
using C4ObjectApi.Operations.Search;
using C4GeneralGui.GuiElements;

namespace CDCplusLib.DataModel
{
    public class QuickSearch
    {
        private const long SEARCH_BATCH_SIZE = 500L;

        public event ISessionWindow.MessageSentEventHandler MessageSent;

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
            SessionWindowRequestMessage msg = new SessionWindowRequestMessage();
            ResultListSessionWindowRequestData msgData = new ResultListSessionWindowRequestData();
            // msg.Source = "CDCplusLegacyLib.MainWindow"
            msg.Session = _s;
            string searchText = text.ToLower().Trim();



            // Search for ID?
            long id;
            if (searchText.StartsWith("#"))
            {
                Dictionary<long, CmnObject> objs = null;
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

                    objs = ids.Count()>0?_s.GetObjects(ids, false) :new Dictionary<long, CmnObject>();
                    msgData.ResultListTitle = Properties.Resources.lblId + ": " + objs.Count.ToString() + " search result(s)";
                }
                catch (Exception ex)
                {
                    // TODO: error message saying that object search has failed
                    // should normally not happen
                    StandardMessage.ShowMessage(Properties.Resources.msgNoObjectFound, StandardMessage.Severity.ErrorMessage, null, ex);
                    return;
                }

                msgData.ResultListObjects = objs;
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
                    if (sm.ExitButtonKey == "ok")
                    {
                        msgData.ResultListObjects = soo.GetObjects(0L, -1L, SEARCH_BATCH_SIZE);
                    }
                    else
                    {
                        msgData.ResultListObjects = soo.GetObjects(0L, _maxQuickSearchResultCount, SEARCH_BATCH_SIZE);
                    }
                    {

                    }
                }
                else
                {
                    msgData.ResultListObjects = soo.GetObjects(batchSize: SEARCH_BATCH_SIZE);
                }
            }

            msg.SessionWindowRequestData = msgData;
            msg.WindowType = "CDCplusLib.Common.GUI.MainWindow";
            MessageSent?.Invoke(msg);
        }
    }
}
