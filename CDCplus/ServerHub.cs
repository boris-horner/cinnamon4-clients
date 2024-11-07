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
using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Xml;
using CDCplus.Controls;
using System.Reflection;
using CDCplusLib.Common;
using C4ObjectApi.Repository;
using C4GeneralGui.GuiElements;
using System.Collections.Generic;

namespace CDCplus
{
    public partial class ServerHub : Form
    {
        private readonly GlobalApplicationData _gad;
        private bool _activeEvents;
        private int _baseHeight;
        private string _logFn;
        public ServerHub()
        {
            InitializeComponent();
            _activeEvents = false;
            string configFn = Path.Combine(Application.StartupPath, "Config", "C4Servers.config.xml");
            _logFn = Path.Combine(Path.GetTempPath(), "CDCplus_startup.log");
            File.WriteAllText(_logFn, "Starting CDCplus\n");

            if (File.Exists(configFn))
            {
                _gad = new GlobalApplicationData(configFn);

                foreach (XmlElement svrEl in _gad.ServerReferences.SelectNodes("/config/servers/server"))
                {
                    XmlElement cdnEl = (XmlElement)svrEl.SelectSingleNode("cdn");
                    string cdnUrl = (cdnEl == null ? null : cdnEl.GetAttribute("url"));
                    XmlNode ucN = svrEl.SelectSingleNode("user_config/@name");
                    CmnServer svr = null;
                    try
                    {
                        int getTimeout = svrEl.HasAttribute("get_timeout") ? int.Parse(svrEl.GetAttribute("get_timeout")) : 10000;
                        XmlNode localCertFileN = svrEl.SelectSingleNode("local_cert_file");
                        string localCertFile = (localCertFileN == null ? null : Path.Combine(Path.GetDirectoryName(new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath), localCertFileN.InnerText));
                        Dictionary<string, string> localSettings = new Dictionary<string, string>();
                        foreach (XmlElement lsEl in svrEl.SelectNodes("local_settings/parameter"))
                        {
                            localSettings.Add(lsEl.GetAttribute("name"), lsEl.InnerText);
                        }
                        svr = new CmnServer(svrEl.GetAttribute("url"),
                                                      _gad.LocalRootPath,
                                                      svrEl.GetAttribute("label"),
                                                      svrEl.GetAttribute("id"),
                                                      cdnUrl,
                                                      localCertFile,
                                                      localSettings,
                                                      getTimeout,
                                                      ucN != null ? ucN.InnerText : null);
                    }
                    catch (Exception ex)
                    {
                        // ignore, svr remains null
                    }
                    ServerEntry se = new ServerEntry();
                    //se.Resized += se_Resized;
                    se.InitServerEntry(svrEl.GetAttribute("label"), svrEl.GetAttribute("url"), svr, svrEl, _gad, this, _logFn);
                    se.Dock = DockStyle.Top;
                    pEntries.Controls.Add(se);
                    pEntries.Controls.SetChildIndex(se, 0);
                    ShowInTaskbar = true;
                    this.ResizeRedraw = true;
                    //se.SessionClosed += se_Resized;
                }
            }
            else
                StandardMessage.ShowMessage("Failure starting Cinnamon Desktop Client plus\nFile 'C4Servers.config.xml' not found.", StandardMessage.Severity.FatalMessage);
        }

        private void cmdAbout_Click(object sender, EventArgs e)
        {
            Form ab = new CDCplus.Windows.About();
            ab.ShowDialog();
        }

        protected void se_Resized(object sender)
        {
            //if (_activeEvents) ResizeControl();

        }

        private void ResizeControl()
        {
            int h = _baseHeight;
            File.AppendAllText(_logFn, "Base window height: " + _baseHeight.ToString() + "\n");
            foreach (Control c in pEntries.Controls)
            {
                h += c.Height;
                File.AppendAllText(_logFn, " Server entry height: " + c.Height.ToString() + "\n");
            }
            Size = new Size(Width, h);
        }
        private void cmdReloadDiscServers_Click(object sender, EventArgs e)
        {
            foreach (ServerEntry se in pEntries.Controls)
            {
                if (se.Server != null && se.Server.ServerResponse == null) se.ReInitServerEntry();
            }


        }

        private void ServerHub_Shown(object sender, EventArgs e)
        {
            _baseHeight = Height;
            _activeEvents = true;
            //ResizeControl();
        }

        private void ServerHub_Resize(object sender, EventArgs e)
        {
            pbLogo.Width = pbLogo.Height * 198 / 59;
        }
    }
}
