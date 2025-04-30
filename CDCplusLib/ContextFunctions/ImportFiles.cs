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
using CDCplusLib.Common;
using CDCplusLib.Common.Import;
using System.Diagnostics;
using C4ObjectApi.Interfaces;
using C4ObjectApi.Repository;
using C4ObjectApi.Helpers;
using C4ServerConnector.Assets;
using CDCplusLib.EventData;

namespace CDCplusLib.ContextFunctions
{
    public class ImportFiles : IGenericFunction
    {
        private GlobalApplicationData _gad;
        private XmlElement _configEl;
        private CmnSession _s;
        private CmnFolder _f;
        private CmnFolder _lastFolder;

        public string InstanceName { get; set; }

        public event IGenericFunction.SessionWindowRequestEventHandler SessionWindowRequest;
        public event IGenericFunction.NodesModifiedEventHandler NodesModified;

        public void AppendSubmenu(ToolStripMenuItem cmi, Dictionary<long, IRepositoryNode> dict)
        {
            ToolStripMenuItem tsmi = new ToolStripMenuItem(Properties.Resources.lblFiles);
            tsmi.Tag = "files";
            cmi.DropDownItems.Add(tsmi);
            tsmi.Click += new EventHandler(tsmi_Click);

            tsmi = new ToolStripMenuItem(Properties.Resources.lblEntireFolder);
            tsmi.Tag = "folder";
            cmi.DropDownItems.Add(tsmi);
            tsmi.Click += new EventHandler(tsmi_Click);
        }

        void tsmi_Click(object sender, EventArgs e)
        {
            try
            {
                if (_lastFolder != null)
                {
                    ToolStripMenuItem tsmi = sender as ToolStripMenuItem;
                    string key = (string)tsmi.Tag;
                    WindowSelectionData wsd = new WindowSelectionData();
                    switch (key)
                    {
                        // TODO: find a solution to select multiple files and / or folders, as required
                        case "files":
                            {
                                OpenFileDialog ofd = new OpenFileDialog();
                                ofd.InitialDirectory = Environment.CurrentDirectory;
                                ofd.Multiselect = true;
                                if (ofd.ShowDialog() == DialogResult.OK)
                                {
                                    HashSet<string> parsedFolders = new HashSet<string>();
                                    Dictionary<string, FileImportRequest> firsByAbsFn = new Dictionary<string, FileImportRequest>();
                                    ImportParser ip = new ImportParser();
                                    Cursor.Current = Cursors.WaitCursor;
                                    foreach (string fn in ofd.FileNames) ip.ParseFile(_lastFolder.Session, fn, firsByAbsFn, parsedFolders);
                                    Cursor.Current = Cursors.Default;

                                    string dirName = Path.GetDirectoryName(ofd.FileNames[0]);   // assumes that all initial files are in the same folder (which makes sense, because the selector dialog makes sure it is so - hopefully)
                                    int l = dirName.Length + 1;
                                    foreach (FileImportRequest fir in firsByAbsFn.Values)
                                    {
                                        fir.TargetPath = _lastFolder.FolderPath;
                                        string relPath = null;
                                        if (fir.AbsoluteFilename.ToLower().StartsWith(dirName.ToLower())) relPath = fir.AbsoluteFilename.Substring(l);
                                        else relPath = fir.AbsoluteFilename.Replace(":", "");
                                        string[] pathSegs = relPath.Split('\\');
                                        for (int i = 0; i < pathSegs.Count() - 1; ++i) fir.TargetPath += "/" + pathSegs[i];
                                    }

                                    EditFileImportRequests editFirs = new EditFileImportRequests(_f, firsByAbsFn, _gad, _configEl);
                                    bool cont = true;
                                    while (cont)
                                    {
                                        DialogResult dr = editFirs.ShowDialog();
                                        if (dr == DialogResult.OK)
                                        {
                                            Cursor.Current = Cursors.WaitCursor;
                                            ImportRequests(_f.Session, firsByAbsFn, wsd);
                                            cont = false;
                                            Cursor.Current = Cursors.Default;

                                            Debug.Print("Done");
                                        }
                                        else if (dr == DialogResult.Yes) // Yes is used to trigger reload
                                        {
                                            firsByAbsFn = new Dictionary<string, FileImportRequest>();
                                            parsedFolders = new HashSet<string>();
                                            Cursor.Current = Cursors.WaitCursor;
                                            foreach (string fn in ofd.FileNames) ip.ParseFile(_lastFolder.Session, fn, firsByAbsFn, parsedFolders);
                                            Cursor.Current = Cursors.Default;
                                        }
                                        else cont = false; // cancel
                                    }
                                    if(ofd.FileNames.Count()>0) Environment.CurrentDirectory = Path.GetDirectoryName(ofd.FileNames[0]);
                                }
                            }
                            break;
                        case "folder":
                            {
                                FolderBrowserDialog fbd = new FolderBrowserDialog();
                                fbd.SelectedPath = Environment.CurrentDirectory;
                                if (fbd.ShowDialog() == DialogResult.OK)
                                {
                                    HashSet<string> parsedFolders = new HashSet<string>();
                                    Dictionary<string, FileImportRequest> firsByAbsFn = new Dictionary<string, FileImportRequest>();
                                    ImportParser ip = new ImportParser();
                                    Cursor.Current = Cursors.WaitCursor;
                                    ip.ParseFolder(_lastFolder.Session, fbd.SelectedPath, firsByAbsFn, parsedFolders);
                                    SetDefaultPaths(fbd.SelectedPath, firsByAbsFn);
                                    Cursor.Current = Cursors.Default;

                                    EditFileImportRequests editFirs = new EditFileImportRequests(_f, firsByAbsFn, _gad, _configEl);
                                    bool cont = true;
                                    while (cont)
                                    {
                                        DialogResult dr = editFirs.ShowDialog();
                                        if (dr == DialogResult.OK)
                                        {
                                            Cursor.Current = Cursors.WaitCursor;
                                            ImportRequests(_f.Session, firsByAbsFn, wsd);
                                            cont = false;
                                            Cursor.Current = Cursors.Default;

                                            Debug.Print("Done");
                                        }
                                        else if (dr == DialogResult.Yes) // Yes is used to trigger reload
                                        {
                                            firsByAbsFn = new Dictionary<string, FileImportRequest>();
                                            parsedFolders = new HashSet<string>();
                                            Cursor.Current = Cursors.WaitCursor;
                                            ip.ParseFolder(_lastFolder.Session, fbd.SelectedPath, firsByAbsFn, parsedFolders);
                                            SetDefaultPaths(fbd.SelectedPath, firsByAbsFn);
                                            Cursor.Current = Cursors.Default;
                                            editFirs = new EditFileImportRequests(_f, firsByAbsFn, _gad, _configEl);
                                        }
                                        else cont = false; // cancel
                                    }
                                    Environment.CurrentDirectory = fbd.SelectedPath;
                                }
                            }
                            break;

                    }
                    NodesModified?.Invoke(wsd);
                }

            }
            catch (Exception ex)
            {
                // TODO
                throw (ex);
            }
        }

        private void SetDefaultPaths(string selPath, Dictionary<string, FileImportRequest> firsByAbsFn)
        {
            int l = selPath.Length + 1;
            foreach (FileImportRequest fir in firsByAbsFn.Values)
            {
                fir.TargetPath = _lastFolder.FolderPath;
                string relPath = null;
                if (fir.AbsoluteFilename.ToLower().StartsWith(selPath.ToLower())) relPath = fir.AbsoluteFilename.Substring(l);
                else relPath = fir.AbsoluteFilename.Replace(":", "");
                string[] pathSegs = relPath.Split('\\');
                for (int i = 0; i < pathSegs.Count() - 1; ++i) fir.TargetPath += "/" + pathSegs[i];
            }
        }
        public bool DisplayInMenu(Dictionary<long, IRepositoryNode> dict)
        {
            _f = DictionaryHelper.GetSingleFolder(dict);
            _lastFolder = _f;
            return (_f != null);
        }

        private void ImportRequests(CmnSession s, Dictionary<string, FileImportRequest> firsByAbsFn, WindowSelectionData wsd)
        {
            // pass 1: import files
            foreach(FileImportRequest fir in firsByAbsFn.Values)
            {
                try
                {
                    CmnFolder targetF = new CmnFolder(s, s.GetOrCreateFolder(fir.TargetPath));
                    if (!wsd.Selection.ContainsKey(targetF.Id) && targetF!=_lastFolder) wsd.Selection.Add(targetF.Id, targetF);
                    //fir.Object = s.Create(targetF.Id, fir.ObjectName, fir.AbsoluteFilename, fir.Format.Id, fir.Language.Id, fir.ObjectType.Id, null, null);
                    fir.Object = s.Create(targetF.Id, fir.ObjectName, fir.AbsoluteFilename, fir.Format.Id, fir.Language.Id, fir.ObjectType.Id, null, s.SessionConfig.C4Sc.AclsByName["_default_acl"].Id);   // TODO: let user assign ACL

                    // import metadata, if a file named <filename>.metadata.xml is present in the folder of the content file
                    string mdFn = string.Join(".", fir.AbsoluteFilename, "metadata.xml");
                    if(File.Exists(mdFn))
                    {
                        // several things can go wrong with md files not matching the configured metasets etc. Avoid breaking the whole import
                        try
                        {
                            XmlDocument mdDoc = new XmlDocument();
                            mdDoc.Load(mdFn);
                            HashSet<C4Metaset> metasets = new HashSet<C4Metaset>();
                            foreach(XmlElement msEl in mdDoc.DocumentElement.SelectNodes("metaset"))
                            {
                                string msTypeName = msEl.GetAttribute("type");
                                if(_s.SessionConfig.C4Sc.MetasetTypesByName.ContainsKey(msTypeName))
                                {
                                    C4Metaset ms = new C4Metaset((long)_s.SessionConfig.C4Sc.MetasetTypesByName[msTypeName].Id, fir.Object.Id, msEl);
                                    metasets.Add(ms);
                                }
                            }
                            if(metasets.Count() > 0)
                            {
                                Dictionary<long, HashSet<C4Metaset>> metasetsByObjId = new Dictionary<long, HashSet<C4Metaset>>();
                                metasetsByObjId.Add(fir.Object.Id, metasets);
                                _s.CommandSession.CreateObjectMeta(metasetsByObjId);
                            }
                        }
                        catch(Exception mdEx)
                        {
                            // TODO: collect errors and display them
                        }
                    }


                    if (!wsd.Selection.ContainsKey(fir.Object.Id)) wsd.Selection.Add(fir.Object.Id, fir.Object);
                    Debug.Print("Imported " + fir.ObjectName);

                }
                catch(Exception ex)
                {
                    // TODO: can happen when a file can't be opened - prevent this

                    Debug.Print("Exception");
                }
            }

            // pass 2: link files
            C4RelationType contentRelType = s.SessionConfig.C4Sc.RelationTypesByName["child_content"];
            foreach (FileImportRequest fir in firsByAbsFn.Values)
            {
                try
                {
                    HashSet<C4Relation> relations = new HashSet<C4Relation>();
                    foreach (string href in fir.ChildrenByHref.Keys)
                    {
                        FileImportRequest subFir = fir.ChildrenByHref[href];
                        if(fir.Object.Id != subFir.Object.Id)
                        {
                            XmlDocument metadata = new XmlDocument();
                            XmlElement metaEl = (XmlElement)metadata.AppendChild(metadata.CreateElement("metadata"));
                            XmlElement filenameEl = (XmlElement)metaEl.AppendChild(metadata.CreateElement("filename"));
                            filenameEl.InnerText = href;
                            relations.Add(new C4Relation((long)contentRelType.Id, fir.Object.Id, subFir.Object.Id, metadata));
                        }
                    }
                    _s.CommandSession.CreateRelations(relations);
                    Debug.Print("Linked " + fir.ObjectName);

                }
                catch (Exception ex)
                {
                    Debug.Print("Exception");
                }
            }

            // pass 3: assign lifecycles (do this last to avoid the LC taking away required permissions)
            foreach (FileImportRequest fir in firsByAbsFn.Values)
            {
                try
                {
                    if (fir.LifecycleState!=null)
                    {
                        fir.Object.Lock();
                        fir.Object.LifecycleState = fir.LifecycleState;
                        fir.Object.Unlock();
                        Debug.Print("Attached lifecycle to " + fir.ObjectName);
                    }

                }
                catch (Exception ex)
                {
                    Debug.Print("Exception");
                }
            }
        }
        public void Execute(Dictionary<long, IRepositoryNode> dict)
        {
        }

        public Image GetIcon()
        {
            return null; // TODO
        }

        public string GetMenuText()
        {
            return Properties.Resources.lblImportDots;
        }

        public bool HasSubmenuItems(Dictionary<long, IRepositoryNode> dict)
        {
            return true;
        }
        public bool IsValid(Dictionary<long, IRepositoryNode> dict)
        {
            return (DictionaryHelper.GetSingleFolder(dict) != null);
        }

        public void Reset(CmnSession s, GlobalApplicationData globalAppData, XmlElement configEl)
        {
            _gad = globalAppData;
            _configEl = configEl;
            _s = s;
        }
    }
}
