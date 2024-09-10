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
using System.Xml;
using CDCplusLib.Interfaces;
using C4ObjectApi.Interfaces;
using C4ObjectApi.Repository;

namespace CDCplusLib.DataModel
{

    // TODO: move this and interface to EnterprisePackage
    public class DefaultNodeDataProvider : INodeDataProvider
    {
        private Dictionary<string, NodeColumnDefinition> _columnDefinitions;
        private string _defaultSortColumn;
        private SortOrder _defaultSortColumnOrder;
        private Dictionary<string, string> _knownCustomFields;
        private Dictionary<string, string> _knownStandardFields;
        private bool _initCompleted;

        public DefaultNodeDataProvider()
        {
            _initCompleted = false;
        }

        public string GetValue(IRepositoryNode ow, string field)
        {
            if (!_initCompleted)
                throw new ApplicationException("Init() function on DefaultNodeDataProvider must be called before using it");
            // name, id, format, type, size, owner, locked, lifecycle, path
            CmnObject o = null;
            CmnFolder f = null;
            bool folder = false;
            if (ow is CmnFolder)
            {
                f = (CmnFolder)ow;
                folder = true;
            }
            else
            {
                o = (CmnObject)ow;
            }

            switch (field ?? "")
            {
                case "name":
                    {
                        return ow.Name;
                    }

                case "id":
                    {
                        return ow.Id.ToString();
                    }

                case "format":
                    {
                        if (folder)
                            return "";
                        if (o.Format is null)
                            return "";
                        return o.Format.ToString();
                    }

                case "type":
                    {
                        if (folder)
                            return f.FolderType.ToString();
                        return o.ObjectType.ToString();
                    }

                case "version":
                    {
                        if (folder)
                            return "";
                        return o.Version;
                    }

                case "size":
                    {
                        if (folder)
                            return "";
                        return o.ContentSize.ToString();
                    }

                case "owner":
                    {
                        return ow.Owner.FullName;
                    }

                case "creator":
                    {
                        if (folder)
                            return "";
                        return o.Creator.FullName;
                    }

                case "created":
                    {
                        if (folder)
                            return "";
                        return o.Created.ToShortDateString() + " " + o.Created.ToShortTimeString();
                    }

                case "modifier":
                    {
                        if (folder)
                            return "";
                        return o.Modifier.FullName;
                    }

                case "modified":
                    {
                        if (folder)
                            return "";
                        return o.Modified.ToShortDateString() + " " + o.Modified.ToShortTimeString();
                    }

                case "locked":
                    {
                        if (folder)
                            return "";
                        if (o.Locked is null)
                            return "";
                        return o.Locked.FullName;
                    }

                case "lifecycle":
                    {
                        if (folder)
                            return "";
                        if (o.LifecycleState is null)
                            return "";
                        return o.LifecycleState.ToString() + " [" + o.Session.SessionConfig.C4Sc.LifecyclesById[o.LifecycleState.LifecycleId].ToString() + "]";
                    }

                case "language":
                    {
                        if (folder)
                            return "";
                        return o.Language.ToString();
                    }

                case "path":
                    {
                        return ow.Parent is null ? "/" : ow.Parent.FolderPath;
                    }

                default:
                    {
                        if (_knownCustomFields.ContainsKey(field))
                        {
                            if (ow.Summary is object)
                            {
                                XmlNode n = ow.Summary.SelectSingleNode("/summary/fields/field[@name='" + field + "']");
                                if (n is null)
                                {
                                    return "";
                                }
                                else
                                {
                                    int ll = _columnDefinitions[field].LengthLimit;
                                    if (ll > 0 && n.InnerText.Length > ll)
                                    {
                                        return n.InnerText.Substring(0, _columnDefinitions[field].LengthLimit);
                                    }
                                    else
                                    {
                                        return n.InnerText;
                                    }
                                }
                            }
                            else
                            {
                                return "";
                            }
                        }
                        else
                        {
                            // Throw New ApplicationException("Invalid column name " + label)
                            // silently ignore unconfigured columns
                            return "---";
                        }

                        break;
                    }
            }
        }

        public Dictionary<string, string> GetKnownStandardFields()
        {
            if (!_initCompleted)
                throw new ApplicationException("Init() function on DefaultNodeDataProvider must be called before using it");
            return _knownStandardFields;
        }

        public Dictionary<string, string> GetKnownCustomFields()
        {
            if (!_initCompleted)
                throw new ApplicationException("Init() function on DefaultNodeDataProvider must be called before using it");
            return _knownCustomFields;
        }

        public string GetFieldTitle(string field)
        {
            if (!_initCompleted)
                throw new ApplicationException("Init() function on DefaultNodeDataProvider must be called before using it");
            if (_knownStandardFields.ContainsKey(field))
            {
                return _knownStandardFields[field];
            }
            else if (_knownCustomFields.ContainsKey(field))
            {
                return _knownCustomFields[field];
            }
            else
            {
                // Throw New ApplicationException("Invalid column name " + label)
                // silently ignore unconfigured columns
                return "UNKNOWN: " + field;
            }
        }

        public void Init(CmnSession s, XmlElement configEl, XmlElement resultListEl)
        {
            _columnDefinitions = new Dictionary<string, NodeColumnDefinition>();
            _defaultSortColumn = null;
            _defaultSortColumnOrder = SortOrder.Ascending;
            _knownCustomFields = new Dictionary<string, string>();
            if (resultListEl is object)
            {
                foreach (XmlNode fieldN in resultListEl.SelectNodes("allowed_custom_fields/field"))
                    _knownCustomFields.Add(fieldN.InnerText, s.SessionConfig.C4Sc.GetLocalizedLabel("listview_custom_column." + fieldN.InnerText, "other"));
            }

            foreach (XmlElement colEl in configEl.SelectNodes("columns/list_view_column"))
            {
                string tp = colEl.GetAttribute("type");
                string w = colEl.GetAttribute("width");
                int ll = 0;
                if (colEl.HasAttribute("sort"))
                {
                    _defaultSortColumnOrder = colEl.GetAttribute("sort") == "ascending" ? SortOrder.Ascending : SortOrder.Descending;
                    _defaultSortColumn = tp;
                }
                if (colEl.HasAttribute("length-limit"))
                {
                    ll = int.Parse(colEl.GetAttribute("length-limit"));
                }

                _columnDefinitions.Add(tp, new NodeColumnDefinition(int.Parse(w), ll, tp == "size" | tp == "id" ? HorizontalAlignment.Right : HorizontalAlignment.Left));
            }

            if (_knownStandardFields is null)
            {
                _knownStandardFields = new Dictionary<string, string>();
                _knownStandardFields.Add("name", Properties.Resources.lblName);
                _knownStandardFields.Add("id", Properties.Resources.lblId);
                _knownStandardFields.Add("format", Properties.Resources.lblFormat);
                _knownStandardFields.Add("type", Properties.Resources.lblType);
                _knownStandardFields.Add("version", Properties.Resources.lblVersion);
                _knownStandardFields.Add("size", Properties.Resources.lblSize);
                _knownStandardFields.Add("owner", Properties.Resources.lblOwner);
                _knownStandardFields.Add("creator", Properties.Resources.lblCreatedBy);
                _knownStandardFields.Add("created", Properties.Resources.lblCreationDate);
                _knownStandardFields.Add("modifier", Properties.Resources.lblModifiedBy);
                _knownStandardFields.Add("modified", Properties.Resources.lblModificationDate);
                _knownStandardFields.Add("locked", Properties.Resources.lblLockedBy);
                _knownStandardFields.Add("lifecycle", Properties.Resources.lblLifecycle);
                _knownStandardFields.Add("language", Properties.Resources.lblLanguage);
                _knownStandardFields.Add("path", Properties.Resources.lblPath);
            }

            _initCompleted = true;
        }

        public string GetDefaultSortColumn()
        {
            if (!_initCompleted)
                throw new ApplicationException("Init() function on DefaultNodeDataProvider must be called before using it");
            return _defaultSortColumn;
        }

        public SortOrder GetDefaultSortColumnOrder()
        {
            if (!_initCompleted)
                throw new ApplicationException("Init() function on DefaultNodeDataProvider must be called before using it");
            return _defaultSortColumnOrder;
        }

        public Dictionary<string, NodeColumnDefinition> GetColumnDefinitions()
        {
            if (!_initCompleted)
                throw new ApplicationException("Init() function on DefaultNodeDataProvider must be called before using it");
            return _columnDefinitions;
        }
    }
}