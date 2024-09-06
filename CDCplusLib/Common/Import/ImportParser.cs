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
using System.Diagnostics;
using ContentAwareness;
using C4ObjectApi.Repository;

namespace CDCplusLib.Common.Import
{
    public class ImportParser
    {
        public void ParseFile(CmnSession s, string fn, Dictionary<string, FileImportRequest> firsByAbsFn, HashSet<string> parsedFolders)
        {
            if (firsByAbsFn.ContainsKey(fn)) return;
            Debug.Print("Parsing file: " + fn);
            FileImportRequest result = new FileImportRequest(s, fn);
            try
            {
                firsByAbsFn.Add(fn, result);
                string ext = Path.GetExtension(fn).ToLower();
                if (ext.Length > 0) ext = ext.Substring(1);
                if(s.SessionConfig.C4Sc.FormatsByExtension.ContainsKey(ext))
                {
                    result.Format = s.SessionConfig.C4Sc.FormatsByExtension[ext];
                    if(result.Format.DefaultObjectTypeId!=null) result.ObjectType = s.SessionConfig.C4Sc.ObjectTypesById[(long)result.Format.DefaultObjectTypeId];
                    IContent c = s.GetContentAwarenessModule(result.Format);

                    if(c!=null)
                    {
                        string parentDir = Path.GetDirectoryName(fn);
                        foreach (string refFn in c.GetReferences(fn,s.CommandSession, s.SessionConfig.C4Sc))
                        {
                            string absRefFn = Path.GetFullPath(Path.Combine(parentDir, refFn));
                            if(File.Exists(absRefFn))
                            {
                                ParseFile(s, absRefFn, firsByAbsFn, parsedFolders);
                                result.ChildrenByHref.Add(refFn, firsByAbsFn[absRefFn]);
                            }
                            else
                            {
                                result.Status = FileImportRequest.StatusValues.ReferencesNotFound;
                                result.StatusMessage += (result.StatusMessage == "" ? "" : "\n") + "Child reference not found (rel/abs): " + refFn + " / " + absRefFn;
                            }
                        }
                    }
                    if (result.Status == FileImportRequest.StatusValues.Running) result.Status = FileImportRequest.StatusValues.OK;
                }
                else
                {
                    result.Status = FileImportRequest.StatusValues.UnknownFormat;
                    result.StatusMessage = "Unkown format extension: " + ext;
                }
            }
            catch(Exception ex)
            {
                result.Status = FileImportRequest.StatusValues.Failure;
                result.StatusMessage = new ExceptionParser(ex).ToString();
            }
        }
        public void ParseFolder(CmnSession s, string absPath, Dictionary<string, FileImportRequest> firsByAbsFn, HashSet<string> parsedFolders)
        {
            if (parsedFolders.Contains(absPath)) return;
            parsedFolders.Add(absPath);
            Debug.Print("Parsing folder " + absPath);

            foreach (string folderName in Directory.GetDirectories(absPath))
                if(!parsedFolders.Contains(folderName))
                    ParseFolder(s, folderName, firsByAbsFn, parsedFolders);

            foreach (string fn in Directory.GetFiles(absPath))
                if(!firsByAbsFn.ContainsKey(fn))
                    ParseFile(s, fn, firsByAbsFn, parsedFolders);
        }
    }
}
