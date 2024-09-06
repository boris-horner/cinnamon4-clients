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
using C4ObjectApi.Interfaces;
using C4ObjectApi.Repository;

namespace CDCplusLib.Helpers
{
    public class ContentsListViewHelper
    {
        public static IList<long> GetSelectedIds(ListView lvw)
        {
            IList<long> result = new List<long>();
            foreach (ListViewItem lvi in lvw.SelectedItems)
            {
                long id = ((IRepositoryNode)lvi.Tag).Id;
                if (!result.Contains(id))
                    result.Add(id);
            }

            return result;
        }

        public static void AddObjectToListview(ref ListView lvw, ref HashSet<string> types, ref CmnObject o)
        {
            string imageKey;
            if (o.Locked is null)
            {
                imageKey = o.Link is null ? "document_unlocked" : "link_document_unlocked";
            }
            else if (o.Locked == o.Session.User)
            {
                imageKey = o.Link is null ? "document_locked_self" : "link_document_locked_self";
            }
            else
            {
                imageKey = o.Link is null ? "document_locked_other" : "link_document_locked_other";
            }

            Dictionary<int, string> colVals = new Dictionary<int, string>();
            int i = 0;
            foreach (string t in types)
            {
                switch (t ?? "")
                {
                    case "name":
                        {
                            colVals.Add(i, o.Name);
                            break;
                        }

                    case "id":
                        {
                            colVals.Add(i, o.Id.ToString());
                            break;
                        }

                    case "format":
                        {
                            if (o.Format is null)
                            {
                                colVals.Add(i, "");
                            }
                            else
                            {
                                colVals.Add(i, o.Format.Name);
                            }

                            break;
                        }

                    case "language":
                        {
                            colVals.Add(i, o.Language.ToString());
                            break;
                        }

                    case "type":
                        {
                            colVals.Add(i, o.ObjectType.Name);
                            break;
                        }

                    case "size":
                        {
                            colVals.Add(i, o.ContentSize.ToString());
                            break;
                        }

                    case "owner":
                        {
                            colVals.Add(i, o.Owner.Name);
                            break;
                        }

                    case "creator":
                        {
                            colVals.Add(i, o.Creator.Name);
                            break;
                        }

                    case "modifier":
                        {
                            colVals.Add(i, o.Modifier.Name);
                            break;
                        }

                    case "locked":
                        {
                            if (o.Locked is null)
                            {
                                colVals.Add(i, "");
                            }
                            else
                            {
                                colVals.Add(i, o.Locked.Name);
                            }

                            break;
                        }

                    case "version":
                        {
                            colVals.Add(i, o.Version);
                            break;
                        }

                    case "lifecycle":
                        {
                            if (o.LifecycleState is null)
                            {
                                colVals.Add(i, "");
                            }
                            else
                            {
                                colVals.Add(i, o.LifecycleState.Name + " [" + o.Session.SessionConfig.C4Sc.LifecyclesById[o.LifecycleState.LifecycleId].Name + "]");
                            }

                            break;
                        }

                    case "path":
                        {
                            colVals.Add(i, o.Parent.FolderPath);
                            break;
                        }
                }

                i += 1;
            }

            string txt = colVals[0];
            ListViewItem it = lvw.Items.Add(o.Id.ToString(), txt, imageKey);
            it.Tag = o;
            if (colVals.Count > 1)
            {
                int loopTo = colVals.Count - 1;
                for (i = 1; i <= loopTo; i++)
                    it.SubItems.Add(colVals[i]);
            }
        }

        public static void AddFolderToListview(ref ListView lvw, ref HashSet<string> types, ref CmnFolder f)
        {
            Dictionary<int, string> colVals = new Dictionary<int, string>();
            int i = 0;
            foreach (string t in types)
            {
                ++i;
                switch (t ?? "")
                {
                    case "name":
                        {
                            colVals.Add(i, f.Name);
                            break;
                        }

                    case "id":
                        {
                            colVals.Add(i, f.Id.ToString());
                            break;
                        }

                    case "format":
                        {
                            colVals.Add(i, "");
                            break;
                        }

                    case "language":
                        {
                            colVals.Add(i, "");
                            break;
                        }

                    case "type":
                        {
                            colVals.Add(i, Properties.Resources.lblFolder);
                            break;
                        }

                    case "size":
                        {
                            colVals.Add(i, "");
                            break;
                        }

                    case "owner":
                        {
                            colVals.Add(i, f.Owner.Name);
                            break;
                        }

                    case "creator":
                        {
                            colVals.Add(i, "");
                            break;
                        }

                    case "modifier":
                        {
                            colVals.Add(i, "");
                            break;
                        }

                    case "locked":
                        {
                            colVals.Add(i, "");
                            break;
                        }

                    case "version":
                        {
                            colVals.Add(i, "");
                            break;
                        }

                    case "lifecycle":
                        {
                            colVals.Add(i, "");
                            break;
                        }

                    case "path":
                        {
                            colVals.Add(i, f.Parent.FolderPath);
                            break;
                        }
                }
            }

            ListViewItem it = lvw.Items.Add(f.Id.ToString(), colVals[1], f.Link is null ? "folder" : "link_folder");
            it.Tag = f;
            if (colVals.Count > 1)
            {
                for (int j = 2; j <= colVals.Count; ++j)
                    it.SubItems.Add(colVals[j]);
            }
        }
        public static void AddIcon(string name, ImageList iml, string relPath = null)
        {
            relPath = relPath ?? Path.Combine("Images", "Icons");
            string iconFn = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), relPath, name + ".ico");
            iml.Images.Add(name, new Icon(iconFn));
        }
    }
}