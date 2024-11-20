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
using System.Reflection;
using C4ServerConnector;
using CAELib.Interfaces;
using CAELib.Log;
using System.Net;
using C4ServerConnector.Assets;

namespace CAE
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                // load config
                string logFilename = null;
                XmlDocument config = new XmlDocument();
                string assemblyPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                config.Load(Path.Combine(assemblyPath, "Config", "cae.config.xml"));
                XmlElement logEl = (XmlElement)config.SelectSingleNode("/config/log");
                if (logEl.GetAttribute("file") == "true")
                    logFilename = Path.Combine(logEl.InnerText, "cae_" + DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss") + ".log");
                bool console = (logEl.GetAttribute("console") == "true");
                bool framework = (logEl.GetAttribute("framework") != "false");
                bool session = (logEl.GetAttribute("session") == "true");
                Logger l = new Logger(console, logFilename);
                l.Log("Starting CAE 4.0");
                l.Log(string.Concat("Framework logging: ", framework ? "enabled" : "disabled"));
                XmlNode tempPathN = (XmlElement)config.SelectSingleNode("/config/temp_path");
                string tempPath = null;
                if(tempPathN != null) 
                { 
                    tempPath = tempPathN.InnerText;
                    l.Log(string.Concat("Temp path: ", tempPath));
                }
                else
                {
                    tempPath = Path.GetTempPath();
                    l.Log(string.Concat("Temp path: ", tempPath, " (default)"));
                }
                XmlElement serverEl = (XmlElement)config.SelectSingleNode("/config/server");
                string url = serverEl.SelectSingleNode("url").InnerText;
                string username = serverEl.SelectSingleNode("username").InnerText;
                string password = serverEl.SelectSingleNode("password").InnerText;
                XmlNode localCertFileN = serverEl.SelectSingleNode("local_cert_file");
                string localCertFile = (localCertFileN == null ? null : Path.Combine(Path.GetDirectoryName(new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath), localCertFileN.InnerText));
                l.Log(string.Concat("Connecting to server ", url));
                C4Session c4s = new C4Session(url, username, password, null, localCertFile, session, tempPath);
                C4SessionConfiguration sc = new C4SessionConfiguration(c4s, username, tempPath, null, null);
                //sc.CachedFolders.Add("async_processing_requests", c4s.GetFolderByPath("/.data/transient/async_processing_requests"));

                l.Log("Connected");

                // load task definitions
                Dictionary<string, IAsyncTask> tasks = new Dictionary<string, IAsyncTask>();
                foreach (XmlElement tEl in config.SelectNodes("/config/tasks/task"))
                {
                    string taskName = tEl.GetAttribute("name");
                    string taskAsm = tEl.GetAttribute("assembly");
                    string taskType = tEl.GetAttribute("type");
                    bool taskLog = (tEl.GetAttribute("log") == "true");
                    try
                    {
                        Assembly asm = Assembly.LoadFrom(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), taskAsm + ".dll"));
                        IAsyncTask t = (IAsyncTask)asm.CreateInstance(taskAsm + "." + taskType);
                        if (t == null) l.Log("Failure creating task instance: " + taskName);
                        else
                        {
                            l.Log("Created task instance: " + taskName);
                            t.Init(c4s, sc, tEl, l);
                            l.Log("Initialized task: " + taskName);
                            if (tEl.HasAttribute("interval"))
                            {
                                t.Interval = int.Parse(tEl.GetAttribute("interval"));
                                string nextrun = tEl.GetAttribute("nextrun").Trim();
                                if (nextrun.Length == 0) t.NextRun = DateTime.Now;
                                else t.NextRun = DateTime.Parse(nextrun);
                            }
                            else
                            {
                                t.NextRun = DateTime.MaxValue;
                                t.Interval = 0;
                            }
                            tasks.Add(taskName, t);
                            l.Log("Loaded task: " + taskName);
                        }
                    }
                    catch (Exception ex2)
                    {
                        l.Log("Failure loading task: " + taskName + Environment.NewLine + ex2.GetType().ToString() + Environment.NewLine + ex2.Message + Environment.NewLine + ex2.StackTrace);
                    }
                }

                bool cont = true;
                int waitTime = int.Parse(serverEl.SelectSingleNode("thread_wait/@milliseconds").InnerText);
                while (cont)
                {
                    try
                    {
                        // test session
                        //XmlDocument echoResp = c4s.Echo();
                        //if (echoResp != null)
                        //{
                            // jobs
                            XmlElement jobsEl = (XmlElement)serverEl.SelectSingleNode("jobs[@active='true']");
                            if (jobsEl != null)
                            {
                                if (framework) l.Log("-------");
                                foreach (XmlElement otEl in jobsEl.SelectNodes("tasks/task"))
                                {
                                    string tn = otEl.GetAttribute("name");
                                    IAsyncTask t = null;
                                    try
                                    {
                                        t = tasks[tn];
                                        if (t.Interval == 0 || (t.Interval > 0 && t.NextRun < DateTime.Now))
                                        {
                                            bool execResult = t.Execute();
                                            if (framework) l.Log((execResult ? "[x] " : "[ ] ") + tn);
                                            if (t.Interval > 0) t.NextRun = DateTime.Now.AddSeconds(t.Interval);
                                        }
                                    }
                                    catch (Exception ex5)
                                    {
                                        l.Log("ERR " + tn + Environment.NewLine + ex5.GetType().ToString() + Environment.NewLine + ex5.Message + Environment.NewLine + ex5.StackTrace);
                                    }
                                    finally
                                    {
                                    }

                                }
                                //c4s.setchangedstatus(id, false, false, C4Session.NodeType.Object);
                                if (framework) l.Log("Processed jobs");
                            }




                            // objects
                            //XmlElement objectsEl = (XmlElement)repoEl.SelectSingleNode("objects[@active='true']");
                            foreach (XmlElement objectsEl in serverEl.SelectNodes("objects[@active='true']"))
                            {
                                XmlElement queryEl = (XmlElement)objectsEl.SelectSingleNode("query");
                                XmlDocument oq = new XmlDocument();
                                oq.AppendChild(oq.CreateXmlDeclaration("1.0", "UTF-8", ""));
                                oq.AppendChild(oq.ImportNode((XmlElement)queryEl.SelectSingleNode("*").CloneNode(true), true));
                                //Dictionary<long, C4Object> objects = c4s.GetObjectsById(c4s.SearchObjectIds(oq.OuterXml, long.Parse(queryEl.GetAttribute("batch_size"))),false);
                                Dictionary<long, C4Object> objects = c4s.GetObjectsById(c4s.SearchObjectIds(oq.OuterXml),false);

                                foreach (C4Object o in objects.Values)
                                {
                                    try
                                    {
                                        // these are passed to the task as null. If the task changes metadata or content, it should set this to true.
                                        bool? contentChanged = null;
                                        bool? metadataChanged = null;
                                        if (framework) l.Log("-------");
                                        foreach (XmlElement otEl in objectsEl.SelectNodes("tasks/task"))
                                        {
                                            string tn = otEl.GetAttribute("name");
                                            try
                                            {
                                                IAsyncTask t = tasks[tn];
                                                bool execResult = t.Execute(o, ref contentChanged, ref metadataChanged);
                                                if (framework) l.Log((execResult ? "[x] " : "[ ] ") + tn);
                                            }
                                            catch (Exception ex5)
                                            {
                                                l.Log(string.Concat("ERR ", tn, "\n", ex5.GetType().ToString(), "\n", ex5.Message, "\n", ex5.StackTrace));
                                            }

                                        }
                                        c4s.LockObject(o.Id);
                                        l.Log(string.Concat("Setting metadata changed on object: id=", o.Id.ToString(), " to ", (bool)metadataChanged?"true":"false"));
                                        l.Log(string.Concat("Setting content changed on object: id=", o.Id.ToString(), " to ", (bool)contentChanged ? "true" : "false"));

                                        c4s.UpdateObject(o.Id, null, null, null, null, null, null, objectsEl.GetAttribute("clearmetadatachanged") == "true" ? false : (bool)metadataChanged, objectsEl.GetAttribute("clearcontentchanged") == "true" ? false : (bool)contentChanged);
                                        c4s.UnlockObject(o.Id);
                                        if (framework) l.Log(string.Concat("Processed object: id=", o.Id.ToString()));
                                    }
                                    catch (Exception ex4)
                                    {
                                        l.Log(string.Concat("Failure processing object: id=", o.Id.ToString(), "\n", ex4.GetType().ToString(), "\n", ex4.Message, "\n", ex4.StackTrace));
                                    }
                                }
                            }


                            // folders
                            //XmlElement foldersEl = (XmlElement)repoEl.SelectSingleNode("folders[@active='true']");
                            foreach (XmlElement foldersEl in serverEl.SelectNodes("folders[@active='true']"))
                            {
                                XmlElement queryEl = (XmlElement)foldersEl.SelectSingleNode("query");
                                Dictionary<long, C4Folder> folders = c4s.GetFoldersById(c4s.SearchFolderIds(queryEl.InnerXml));

                                foreach (C4Folder f in folders.Values)
                                {
                                    try
                                    {
                                        bool? contentChanged = null;
                                        bool? metadataChanged = null;
                                        foreach (XmlElement ftEl in foldersEl.SelectNodes("tasks/task"))
                                        {
                                            string tn = ftEl.GetAttribute("name");
                                            try
                                            {
                                                IAsyncTask t = tasks[tn];
                                                bool execResult = t.Execute(f, ref contentChanged, ref metadataChanged);
                                                if (framework) l.Log((execResult ? "[x] " : "[ ] ") + tn);
                                            }
                                            catch (Exception ex5)
                                            {
                                                l.Log(string.Concat("ERR ", tn, "\n", ex5.GetType().ToString(), "\n", ex5.Message, "\n", ex5.StackTrace));
                                            }
                                            //tasks[ftEl.GetAttribute("name")].Execute(c4s, id, folderEl, ref contentChanged, ref metadataChanged);
                                        }
                                        c4s.UpdateFolder(f.Id, null, null, null, null, null, metadataChanged);
                                        if (framework) l.Log(string.Concat("Processed folder: id=", f.Id.ToString()));
                                    }
                                    catch (Exception ex4)
                                    {
                                        l.Log(string.Concat("Failure processing folder: id=", f.Id.ToString(), "\n", ex4.GetType().ToString(), "\n", ex4.Message, "\n", ex4.StackTrace));
                                    }
                                }
                            }
                        //}
                        //else
                        //{
                        //    l.Log("Session is expired - trying to reconnect.");
                        //    c4s = new C4Session(url, username, password, null, localCertFile, session, tempPath);
                        //}


                    }
                    catch (Exception ex3)
                    {
                        try
                        {
                            l.Log("Failure processing cycle, trying to reconnect" + Environment.NewLine + ex3.GetType().ToString() + Environment.NewLine + ex3.Message + Environment.NewLine + ex3.StackTrace);
                            c4s = new C4Session(url, username, password, null, localCertFile, session, tempPath);

                        }
                        catch (Exception ex4)
                        {
                            l.Log("Failure reconnecting - retrying on next iteration" + Environment.NewLine + ex4.GetType().ToString() + Environment.NewLine + ex4.Message + Environment.NewLine + ex4.StackTrace);
                            // ignore otherwise
                        }
                    }

                    if (framework) l.Log("Finished cycle, waiting " + waitTime.ToString() + " milliseconds");
                    // wait some time
                    System.Threading.Thread.Sleep(waitTime);
                }

            }
            catch (WebException ex)
            {
                Logger l = new Logger(false, Path.Combine(Path.GetTempPath(), "CAE.UnhandledException.log"));
                l.Log(DateTime.Now.ToUniversalTime() + " Failure running CAE" + Environment.NewLine + ex.GetType().ToString() + Environment.NewLine + ex.Message + Environment.NewLine + ex.StackTrace);
            }
            catch (Exception ex)
            {
                Logger l = new Logger(false, Path.Combine(Path.GetTempPath(), "CAE.UnhandledException.log"));
                l.Log(DateTime.Now.ToUniversalTime() + " Failure running CAE" + Environment.NewLine + ex.GetType().ToString() + Environment.NewLine + ex.Message + Environment.NewLine + ex.StackTrace);
            }

        }

    }
}
