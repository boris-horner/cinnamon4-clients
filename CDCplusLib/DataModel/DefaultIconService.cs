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
using C4ObjectApi.Interfaces;
using C4ObjectApi.Repository;

namespace CDCplusLib.DataModel
{
    public class DefaultIconService : IIconService, ISessionExtension
    {

        public DefaultIconService()
        {
            LoadImages();
        }

        private void LoadImages()
        {
            try
            {
                string iconsPath = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "Images", "Icons");
                GlobalSmallImageList = new ImageList();
                GlobalSmallImageList.ImageSize = new Size(16, 16);
                GlobalSmallImageList.ColorDepth = ColorDepth.Depth24Bit;
                string smallImagePath = Path.Combine(iconsPath, "16x16");
                GlobalLargeImageList = new ImageList();
                GlobalLargeImageList.ImageSize = new Size(64, 64);
                GlobalLargeImageList.ColorDepth = ColorDepth.Depth24Bit;
                string largeImagePath = Path.Combine(iconsPath, "64x64");

                AddIcon("blank", "blank", smallImagePath, largeImagePath, false);
                // AddIcon("link", "emblem-symbolic-link", smallImagePath, largeImagePath)
                AddIcon("unknown", "unknown", smallImagePath, largeImagePath, false);
                AddIcon("document", "application-x-zerosize", smallImagePath, largeImagePath, false);
                AddIcon("folder", "folder", smallImagePath, largeImagePath, false);
                AddIcon("document_locked_other", "image-missing", smallImagePath, largeImagePath, false);
                AddIcon("document_locked_self", "application-x-kgetlist", smallImagePath, largeImagePath, false);
                AddIcon("descending", "go-up", smallImagePath, largeImagePath, false);
                AddIcon("ascending", "go-down", smallImagePath, largeImagePath, false);
                AddIcon("lifecycle", "code-block", smallImagePath, largeImagePath, false);
                AddIcon("lifecycle_state", "flag-yellow", smallImagePath, largeImagePath, false);
                AddIcon("info", "dialog-information", smallImagePath, largeImagePath, false);
                AddIcon("find", "edit-find", smallImagePath, largeImagePath, false);
                AddIcon("notification", "mail-message", smallImagePath, largeImagePath, false);
                AddIcon("config", "application-x-desktop", smallImagePath, largeImagePath, false);
                AddIcon("translation", "internet-web-browser", smallImagePath, largeImagePath, false);

                // only append to _smallIl - _largeIl will receive a dummy
                AddIcon("search_sysmeta", "applications-system", smallImagePath, largeImagePath, true);
                AddIcon("search_custom", "format-list-unordered", smallImagePath, largeImagePath, true);
                AddIcon("search_and", "folder-violet", smallImagePath, largeImagePath, true);
                AddIcon("search_or", "folder-green", smallImagePath, largeImagePath, true);

            }
            catch(Exception ex)
            {

            }
        }

        public string GetIconKey(IRepositoryNode ow)
        {
            if (ow is CmnFolder)
            {
                CmnFolder f = (CmnFolder)ow;
                return "folder";
            }
            else
            {
                CmnObject o = (CmnObject)ow;
                switch (o.ObjectType.Name ?? "")
                {
                    case "_search":
                        {
                            return "find";
                        }

                    case "_notification":
                        {
                            return "notification";
                        }

                    case "_task":
                        {
                            return "notification";   // TODO: define a separate icon
                        }

                    case "_config":
                        {
                            return "config";
                        }

                    case "translation_task":
                        {
                            return "translation";
                        }
                }

                if (o.Locked is null)
                {
                    return "document";
                }
                else if (o.Locked == o.Session.User)
                {
                    return "document_locked_self";
                }
                else
                {
                    return "document_locked_other";
                }
            }
        }


        private void AddIcon(string key, string name, string smallImagePath, string largeImagePath, bool loadSmallOnly)
        {
            GlobalSmallImageList.Images.Add(key, new Bitmap(Path.Combine(smallImagePath, name + ".png")));
            if (loadSmallOnly)
            {
                GlobalLargeImageList.Images.Add(key, new Bitmap(Path.Combine(largeImagePath, "blank.png")));
            }
            else
            {
                GlobalLargeImageList.Images.Add(key, new Bitmap(Path.Combine(largeImagePath, name + ".png")));
            }
        }


        public ImageList GlobalLargeImageList { get; private set; }

        public ImageList GlobalSmallImageList { get; private set; }

        public void Init(CmnSession s, System.Xml.XmlElement configEl)
        {
            // nothing to do
        }
    }
}