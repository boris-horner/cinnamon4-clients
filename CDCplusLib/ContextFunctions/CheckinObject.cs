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
using CDCplusLib.Common;
using CDCplusLib.Common.GUI;
using CDCplusLib.Interfaces;
using C4ObjectApi.Interfaces;
using C4ObjectApi.Repository;
using C4ObjectApi.Exceptions;
using C4ObjectApi.Helpers;
using C4ServerConnector.Assets;
using C4GeneralGui.GuiElements;
using CDCplusLib.EventData;

namespace CDCplusLib.ContextFunctions
{

    public class CheckinObject : IGenericFunction
    {

        private GlobalApplicationData _gad;
        private bool _checkinFromCheckoutDefaultOverwrite;
        private bool _checkinFromOtherDefaultOverwrite;
        private XmlElement _configEl;

        public void AppendSubmenu(ToolStripMenuItem cmi, Dictionary<long, IRepositoryNode> dict)
        {

        }

        public bool HasSubmenuItems(Dictionary<long, IRepositoryNode> dict)
        {
            return false;
        }

        public string GetMenuText()
        {
            return Properties.Resources.mnuCheckin;
        }
        public Image GetIcon()
        {
            return null;
        }
        public void Execute(Dictionary<long, IRepositoryNode> dict)
        {
            CmnObject o = DictionaryHelper.GetSingleObject(dict);
            Checkin ci = new Checkin(o, _gad.CheckoutPath, _checkinFromCheckoutDefaultOverwrite, _checkinFromOtherDefaultOverwrite);
            if (ci.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string checkinPath;
                    C4Format fmt = null;
                    if (ci.CheckinFromCheckedOutFile)
                    {
                        checkinPath = o.Session.LocksMgr.Locks[o.Id].ContentFilename;
                        //checkinPath = o.LocalPath;
                        fmt = o.Format;
                    }
                    else
                    {
                        checkinPath = ci.CheckinFile;
                        fmt = ci.Format;
                    }
                    if (ci.NewVersion)
                    {
                        if (!o.Permissions.Object_Version)
                        {
                            StandardMessage.ShowMessage(Properties.Resources.msgInsufficientPermissions, StandardMessage.Severity.ErrorMessage, null, null, o.Session.GetHelpUrl(DataModelErrorCodes.INSUFFICIENT_PERMISSIONS));
                        }
                        try
                        {
                            CmnObject newVersion = o.CheckinFromFile(checkinPath, fmt, true, ci.ChildUpdateBehavior);
                            newVersion.Lock();
                            o.CopyToExisting(newVersion, false, o.Session.SessionConfig.CopyMetasetTypeIds);
                            newVersion.Unlock();
                            WindowSelectionData wsd = new WindowSelectionData();
                            wsd.Modification.Add(o.Id, o);
                            wsd.Selection.Add(newVersion.Id, newVersion);
                            wsd.Modification.Add(newVersion.Id, newVersion);
                            NodesModified?.Invoke(wsd);
                        }
                        catch (CmnDataModelException ex)
                        {
                            switch (ex.ErrorCode)
                            {
                                case DataModelErrorCodes.BRANCHING_NOT_ALLOWED:
                                    {
                                        StandardMessage.ShowMessage(Properties.Resources.msgBranchingForbidden, StandardMessage.Severity.ErrorMessage, null, null, o.Session.GetHelpUrl(ex.ErrorCode));
                                        break;
                                    }
                                case DataModelErrorCodes.UNRESOLVED_INBOUND_REFERENCES:
                                    {
                                        string msg = Properties.Resources.msgReferencesNotFound + "\n";
                                        HashSet<string> files = (HashSet<string>)ex.Data["unresolved_refs"];
                                        foreach (string fn in files)
                                            msg += "\n" + fn;
                                        StandardMessage.ShowMessage(msg, StandardMessage.Severity.ErrorMessage, null, null, o.Session.GetHelpUrl(ex.ErrorCode));
                                        break;
                                    }
                                case DataModelErrorCodes.FAILED_INBOUND_REFERENCES:
                                    {
                                        string msg = Properties.Resources.msgFailureCreatingReferences + "\n";
                                        HashSet<string> files = (HashSet<string>)ex.Data["failed_refs"];
                                        foreach (string fn in files)
                                            msg += "\n" + fn;
                                        StandardMessage.ShowMessage(msg, StandardMessage.Severity.ErrorMessage, null, null, o.Session.GetHelpUrl(ex.ErrorCode));
                                        break;
                                    }
                            }
                        }
                    }
                    else
                    {
                        if (!(o.Permissions.Object_Content_Write && o.Permissions.Node_Metadata_Write))
                        {
                            StandardMessage.ShowMessage(Properties.Resources.msgInsufficientPermissions, StandardMessage.Severity.ErrorMessage, null, null, o.Session.GetHelpUrl(DataModelErrorCodes.INSUFFICIENT_PERMISSIONS));
                        }
                        o.CheckinFromFile(checkinPath, fmt, false, ci.ChildUpdateBehavior);
                        WindowSelectionData wsd = new WindowSelectionData();
                        wsd.Modification.Add(o.Id, o);
                        wsd.Selection.Add(o.Id, o);
                        NodesModified?.Invoke(wsd);
                    }
                }
                catch (CmnDataModelException ex)
                {
                    switch (ex.ErrorCode)
                    {
                        case DataModelErrorCodes.UNRESOLVED_INBOUND_REFERENCES:
                            {
                                string msg = Properties.Resources.msgReferencesNotFound + "\n";
                                HashSet<string> files = (HashSet<string>)ex.Data["unresolved_refs"];
                                foreach (string fn in files)
                                    msg += "\n" + fn;
                                StandardMessage.ShowMessage(msg, StandardMessage.Severity.ErrorMessage, null, null, o.Session.GetHelpUrl(ex.ErrorCode));
                                break;
                            }
                        case DataModelErrorCodes.FAILED_INBOUND_REFERENCES:
                            {
                                string msg = Properties.Resources.msgFailureCreatingReferences + "\n";
                                HashSet<string> files = (HashSet<string>)ex.Data["failed_refs"];
                                foreach (string fn in files)
                                    msg += "\n" + fn;
                                StandardMessage.ShowMessage(msg, StandardMessage.Severity.ErrorMessage, null, null, o.Session.GetHelpUrl(ex.ErrorCode));
                                WindowSelectionData wsd = new WindowSelectionData();
                                wsd.Selection.Add(o.Id, o);
                                wsd.Modification.Add(o.Id, o);
                                NodesModified?.Invoke(wsd);
                                break;
                            }
                    }
                }
                catch (Exception ex)
                {
                    StandardMessage.ShowMessage(Properties.Resources.msgFailureCheckingIn, StandardMessage.Severity.ErrorMessage, null, ex, null);
                }
            }
        }

        public bool IsValid(Dictionary<long, IRepositoryNode> dict)
        {
            CmnObject o = DictionaryHelper.GetSingleObject(dict);
            // conditions on o for enabling checkin 
            if (o is null)
                return false;
            if (o.Locked is null)
                return false;
            if (o.Locked != o.Session.User)
                return false;
            if (o.Format is null)
                return false;

            // check permissions on object
            if (!(o.Permissions.Object_Version || (o.Permissions.Object_Content_Write && o.Permissions.Node_Metadata_Write)))
                return false;
            return true;
        }
        public bool DisplayInMenu(Dictionary<long, IRepositoryNode> dict)
        {
            CmnObject o = DictionaryHelper.GetSingleObject(dict);
            return o is not null;
        }
        public string InstanceName { get; set; }
        public event IGenericFunction.SessionWindowRequestEventHandler SessionWindowRequest;
        public event IGenericFunction.NodesModifiedEventHandler NodesModified;

        public void Reset(CmnSession s, GlobalApplicationData globalAppData, XmlElement configEl)
        {
            _gad = globalAppData;
            _configEl = configEl;

            XmlElement dvbEl = (XmlElement)_configEl.SelectSingleNode("custom/default_versioning_behaviour");
            if (dvbEl is null)
                throw new ApplicationException(GetType().ToString() + "\nRequired configuration custom/default_versioning_behaviour\nContact your system administrator.");
            if (dvbEl.HasAttribute("checked_out_file") && dvbEl.HasAttribute("other_file"))
            {
                // new versioning behaviour
                _checkinFromCheckoutDefaultOverwrite = dvbEl.GetAttribute("checked_out_file") == "write";
                _checkinFromOtherDefaultOverwrite = dvbEl.GetAttribute("other_file") == "write";
            }
            //else
            //{
            //    if (!dvbEl.HasAttribute("value"))
            //        throw new ApplicationException(GetType().ToString() + "\nNeither new or legacy checkin configuration was found.\nContact your system administrator.");
            //    _checkinFromCheckoutDefaultOverwrite = dvbEl.GetAttribute("value") == "write";
            //    _checkinFromOtherDefaultOverwrite = dvbEl.GetAttribute("value") == "write";
            //}
        }
    }
}