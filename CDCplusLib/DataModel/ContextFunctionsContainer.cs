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
using CDCplusLib.Common;
using CDCplusLib.Common.GUI;
using CDCplusLib.Interfaces;
using System.Diagnostics;
using System.Xml;
using C4ObjectApi.Interfaces;
using C4ObjectApi.Repository;
using C4GeneralGui.GuiElements;
using System.Reflection;

namespace CDCplusLib.DataModel
{
    public class ContextFunctionsContainer
    {
        public event ISessionWindow.MessageSentEventHandler MessageSent;
        private CmnSession _s;
        private GlobalApplicationData _gad;
        private MainWindow _parentWindow;
        public enum MenuType { Object, Folder, List, Session }
        public Dictionary<string, IGenericFunction> Functions { get; private set; }
        public IGenericFunction DefaultObjectFunction { get; }
        public ContextFunctionsContainer(CmnSession s, GlobalApplicationData gad, MainWindow parentWindow)
        {
            _s = s;
            _gad = gad;
            _parentWindow = parentWindow;
            Functions = new Dictionary<string, IGenericFunction>();
            foreach (XmlElement gfEl in _s.UserConfig.DocumentElement.SelectNodes("classes/functions/function"))
            {
                try
                {
                    string assemblyName = gfEl.GetAttribute("assembly");
                    Assembly a = _s.SessionConfig.GetAssembly(assemblyName);
                    if (a != null)
                    {
                        IGenericFunction gf = _s.SessionConfig.GetAssembly(assemblyName).CreateInstance(assemblyName + "." + gfEl.GetAttribute("type")) as IGenericFunction;
                        if (gf is null)
                        {
                            throw new ApplicationException(string.Format("Function {0}.{1} has not been found.", gfEl.GetAttribute("assembly"), gfEl.GetAttribute("type")));
                        }

                        gf.MessageSent += MessageSentHandler;
                        gf.Reset(_s, _gad, gfEl);
                        gf.InstanceName = gf.GetType().ToString();
                        Functions.Add(gf.InstanceName, gf);
                        if (gfEl.GetAttribute("default_object") == "true")
                            DefaultObjectFunction = gf;
                    }
                    else
                    {
                        StandardMessage.ShowMessage("Failure loading function " + gfEl.GetAttribute("assembly") + "." + gfEl.GetAttribute("type"), StandardMessage.Severity.WarningMessage, null, null);
                    }
                }
                catch (Exception ex)
                {
                    StandardMessage.ShowMessage("Failure loading function " + gfEl.GetAttribute("assembly") + "." + gfEl.GetAttribute("type"), StandardMessage.Severity.WarningMessage, null, ex);
                }
            }
        }
        protected virtual void MessageSentHandler(IClientMessage msg)
        {
            MessageSent?.Invoke(msg); // forward to dispatcher
        }
        public void ShowContextMenu(Dictionary<long, IRepositoryNode> sel, Control referenceControl, Point pos)
        {
            if (sel == null)
            {
                ContextMenuStrip cmn = BuildContextMenu(sel, MenuType.Session);
                cmn.Show(referenceControl, pos);
            }
            else
            {
                ContextMenuStrip cmn = null;
                if (sel.Count > 0)
                {
                    if (sel.Count == 1)
                    {
                        // one object or folder
                        // Dim ow As IRepositoryNode = DictionaryHelper.GetSingleOwnable(orcMsg.ListSelection)
                        IRepositoryNode ow = sel.Values.First();
                        if (ow is CmnObject)
                        {
                            cmn = BuildContextMenu(sel, MenuType.Object);
                        }
                        else if (ow is CmnFolder)
                        {
                            cmn = BuildContextMenu(sel, MenuType.Folder);
                        }
                    }
                    else
                    {
                        cmn = BuildContextMenu(sel, MenuType.List);
                    }
                    cmn.Show(referenceControl, pos);
                }
                else
                {
                    cmn = BuildContextMenu(sel, MenuType.Session);
                    cmn.Show(referenceControl, pos);
                }
            }
        }
        protected virtual ContextMenuStrip BuildContextMenu(Dictionary<long, IRepositoryNode> sel, MenuType mt)
        {
            string xPath = null;
            switch (mt)
            {
                case MenuType.Object:
                    xPath = "menu/object_context_menu/*";
                    break;
                case MenuType.Folder:
                    xPath = "menu/folder_context_menu/*";
                    break;
                case MenuType.List:
                    xPath = "menu/list_context_menu/*";
                    break;
                case MenuType.Session:
                    xPath = "menu/session_context_menu/*";
                    break;
            }
            // TODO: evaluate menu structure definition in the config
            ContextMenuStrip result = new ContextMenuStrip();
            foreach (XmlElement el in _s.UserConfig.DocumentElement.SelectNodes(xPath))
            {
                switch (el.Name ?? "")
                {
                    case "menu_item":
                        {
                            string k = el.GetAttribute("assembly") + "." + el.GetAttribute("type");
                            Debug.Print(k);
                            if (Functions.ContainsKey(k))
                            {
                                IGenericFunction fn = Functions[k];
                                bool dsp = (mt == MenuType.Session) ? fn.DisplayInMenu(null) : fn.DisplayInMenu(sel);
                                if (dsp)
                                {
                                    // add to context menu
                                    ToolStripMenuItem cmi = new ToolStripMenuItem(fn.GetMenuText()); // result.Items.Add(CType(functions_(k), IGenericFunction).GetMenuText)
                                    result.Items.Add(cmi);
                                    switch (mt)
                                    {
                                        case MenuType.Object:
                                        case MenuType.Folder:
                                        case MenuType.List:
                                            cmi.Enabled = fn.IsValid(sel);
                                            if (cmi.Enabled)
                                            {
                                                cmi.Tag = new ContextFunctionPayload(fn, sel);
                                                if (fn.HasSubmenuItems()) fn.AppendSubmenu(cmi);
                                                cmi.Click += HandleContextMenuClick;
                                            }
                                            break;
                                        case MenuType.Session:
                                            cmi.Enabled = fn.IsValid(null);
                                            if (cmi.Enabled)
                                            {
                                                cmi.Tag = new ContextFunctionPayload(fn, _s);
                                                if (fn.HasSubmenuItems()) fn.AppendSubmenu(cmi);
                                                cmi.Click += HandleContextMenuClick;
                                            }
                                            break;
                                    }
                                }
                            }
                            else
                            {
                                Debug.Print("Context menu function " + k + " not found.");
                            }

                            break;
                        }

                    case "separator":
                        {
                            if (!(result.Items[result.Items.Count - 1].GetType() == typeof(ToolStripSeparator))) result.Items.Add(new ToolStripSeparator());
                            break;
                        }

                    default:
                        {
                            Debug.Print("");
                            break;
                        }
                }
            }

            return result;
        }
        protected void HandleContextMenuClick(object sender, EventArgs e)
        {
            try
            {
                if(_parentWindow!=null) _parentWindow.Cursor = Cursors.WaitCursor;
                ToolStripItem cmi = (ToolStripItem)sender;
                ContextFunctionPayload pl = (ContextFunctionPayload)cmi.Tag;
                if(pl!=null)
                {
                    if (pl.Payload.GetType() == typeof(CmnSession)) pl.Function.Execute(null);
                    else pl.Function.Execute((Dictionary<long, IRepositoryNode>)pl.Payload);
                }
                else
                {
                    //what's wrong here?
                }
            }
            catch (ApplicationException ex)
            {
                StandardMessage.ShowMessage("Caught ApplicationException", StandardMessage.Severity.ErrorMessage, _parentWindow, ex);
            }
            catch (Exception ex)
            {
                StandardMessage.ShowMessage("Caught Exception", StandardMessage.Severity.ErrorMessage, _parentWindow, ex);
            }
            finally
            {
                if (_parentWindow != null) _parentWindow.Cursor = Cursors.Default;
            }
        }
    }
}
