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
using CDCplusLib.Messages;
using C4ObjectApi.Interfaces;
using C4ObjectApi.Repository;
using C4ObjectApi.Exceptions;
using C4ObjectApi.Helpers;
using C4ServerConnector.Assets;
using C4GeneralGui.GuiElements;

namespace CDCplusLib.ContextFunctions
{

    public class AppendContent : IGenericFunction
    {

        private GlobalApplicationData _gad;

        public void AppendSubmenu(ToolStripMenuItem cmi)
        {

        }

        public bool HasSubmenuItems()
        {
            return false;
        }

        public Image GetIcon()
        {
            return null;
        }


        public string GetMenuText()
        {
            return Properties.Resources.mnuAppendContent;
        }
        public void Execute(Dictionary<long, IRepositoryNode> dict)
        {
            CmnObject o = DictionaryHelper.GetSingleObject(dict);
            Checkin ci = new Checkin(o, _gad.CheckoutPath, false, false);
            if (ci.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    Dictionary<string, C4Format> fmtByExtension = new Dictionary<string, C4Format>();
                    foreach (C4Format fmt in o.Session.SessionConfig.C4Sc.FormatsByName.Values)
                        fmtByExtension.Add(fmt.Extension, fmt);

                    if (ci.NewVersion)
                    {
                        if (!o.Permissions.Object_Version)
                        {
                            throw new ApplicationException(Properties.Resources.msgInsufficientPermissions);
                        }
                        string ext = Path.GetExtension(ci.CheckinFile).Substring(1);
                        if (fmtByExtension.ContainsKey(ext))
                        {
                            try
                            {
                                o.Lock();
                                C4Format fmt = fmtByExtension[ext];
                                CmnObject newVersion = o.CheckinFromFile(ci.CheckinFile, fmt, true, ci.ChildUpdateBehavior);
                                newVersion.Lock();
                                o.CopyToExisting(newVersion, false, o.Session.SessionConfig.CopyMetasetTypeIds);
                                newVersion.Unlock();
                                ObjectVersionedMessage msg = new ObjectVersionedMessage();
                                msg.OldVersion = o;
                                msg.NewVersion = newVersion;
                                // msg.Source = instanceName_
                                MessageSent?.Invoke(msg);
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
                                }
                            }
                        }
                        else
                        {
                            throw new ApplicationException(string.Format(Properties.Resources.msgUnknownFileExtension, ext));
                        }
                    }
                    else
                    {
                        if (!(o.Permissions.Object_Content_Write && o.Permissions.Node_Metadata_Write))
                        {
                            throw new ApplicationException(Properties.Resources.msgInsufficientPermissions);
                        }
                        string ext = Path.GetExtension(ci.CheckinFile).Substring(1);
                        if (fmtByExtension.ContainsKey(ext))
                        {
                            o.Lock();
                            C4Format fmt = fmtByExtension[ext];
                            o.CheckinFromFile(ci.CheckinFile, fmt, false, ci.ChildUpdateBehavior);
                            ObjectsModifiedMessage msg = new ObjectsModifiedMessage();
                            msg.ModificationType = ObjectsModifiedMessage.ModificationTypes.Unspecified;
                            msg.ModifiedObjects.Add(o.Id, o);
                            // msg.Source = instanceName_
                            MessageSent?.Invoke(msg);
                        }
                        else
                        {
                            throw new ApplicationException(string.Format(Properties.Resources.msgUnknownFileExtension, ext));
                        }
                    }
                }
                catch (Exception ex)
                {
                    StandardMessage.ShowMessage(Properties.Resources.msgFailureAppendingContent, StandardMessage.Severity.ErrorMessage, ex: ex);
                }
            }
        }

        public bool IsValid(Dictionary<long, IRepositoryNode> dict)
        { 
            CmnObject o = DictionaryHelper.GetSingleObject(dict);
            // conditions on o for enabling cancel checkout  
            if (o is null)
                return false;
            if (o.Locked != null)
                return false;
            if (o.Format != null)
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
        public event IGenericFunction.MessageSentEventHandler MessageSent;

        public void Reset(CmnSession s, GlobalApplicationData globalAppData, XmlElement configEl)
        {
            _gad = globalAppData;
        }
    }
}