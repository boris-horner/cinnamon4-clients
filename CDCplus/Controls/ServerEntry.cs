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
using System.Windows.Forms;
using System.Xml;
using System.IO;
using CDCplus.DataModel;
using CDCplusLib.Common;
using C4ObjectApi.Repository;
using CDCplus.Properties;
using C4GeneralGui.GuiElements;
using System.Collections.Generic;

namespace CDCplus.Controls
{
	public partial class ServerEntry : UserControl
	{
		private Form _serverHubForm;
		private XmlElement _svrEl;
		private GlobalApplicationData _gad;
		private int _timeout;
		//private string _localCertFile;
		private int _baseHeight;
		private string _logFn;

		public delegate void ResizedHandler(object sender);

		public event ResizedHandler Resized;

		public CmnServer Server { get; private set; }
		public ServerEntry()
		{
			InitializeComponent();
		}

		public void ReInitServerEntry()
		{
			XmlNode ucN = _svrEl.SelectSingleNode("user_config/@name");
			Server = new CmnServer(Server.Url,
								 _gad.LocalRootPath,
								 Server.Name,
								 Server.Id,
								 Server.CdnUrl,
								 ucN != null ? ucN.InnerText : null,
								 Server.LocalSettings,
								 _timeout,
								 Server.LocalCertFile);
			gbServer.Text = Server.Name;
			lblVersionBuild.Text = string.Format(Resources.lblVersionBuild, Server.Version, Server.Build);

            if (Server.ServerResponse != null)
			{
				pbOnline.Visible = true;
				pbOffline.Visible = false;
				cmdConnect.Enabled = true;
				DoAutologin();
				ResizeControl();
			}

        }
        public void InitServerEntry(string serverName, string serverUrl, CmnServer svr, XmlElement svrEl, GlobalApplicationData gad, Form serverHubForm, string logFn)
		{
			_serverHubForm = serverHubForm;
			Server = svr;
			_svrEl = svrEl;
			_gad = gad;
			_logFn = logFn;
			_baseHeight = Height;
			File.AppendAllText(_logFn, " Base server entry height: " + _baseHeight.ToString() + "\n");

			_timeout = _svrEl.HasAttribute("timeout") ? int.Parse(_svrEl.GetAttribute("timeout")) : 10000;
			//_localCertFile = _svrEl.HasAttribute("local_cert_file") ? Path.Combine(Application.StartupPath, _svrEl.GetAttribute("local_cert_file")) : null;

			gbServer.Text = serverName;
			lblVersionBuild.Text = svr == null ? "" : string.Format(Resources.lblVersionBuild, svr.Version, svr.Build);

			if (svr == null || svr.ServerResponse == null)
			{
				pbOnline.Visible = false;
				pbOffline.Visible = true;
                cmdConnect.Enabled = false;
			}
			else
			{
				pbOnline.Visible = true;
				pbOffline.Visible = false;
				cmdConnect.Enabled = true;
				string checkoutPath = Path.Combine(_gad.LocalRootPath, "checkout");
				if (!Directory.Exists(checkoutPath)) Directory.CreateDirectory(checkoutPath);
				DoAutologin();
			}
			//lblServerUrlValue.Text = serverUrl;
			ResizeControl();
		}

		private void cmdConnect_Click(object sender, EventArgs e)
		{
			// TODO: allow server to forbid saving credentials - i.e., after login, the server config may forbid saving the credentials
			CDCplus.Windows.Login l = new CDCplus.Windows.Login(Server.Name, _svrEl.GetAttribute("show_save_credentials") == "true");
			if (l.ShowDialog() == DialogResult.OK)
			{
				try
				{
					CmnSession s = Server.GetSession(l.Username, l.Password);
					s.AddSessionExtensions();

					if (l.SaveCredentials)
					{
						LoginCredentials cr = new LoginCredentials(l.Username, l.Password);
						string autologinPath = Path.Combine(_gad.LocalRootPath, "autologin");
						string autologinFn = Path.Combine(autologinPath, Server.Id);
						cr.Save(autologinFn);
					}
					SessionEntry sne = new SessionEntry();
					sne.InitSessionEntry(s, Server, _svrEl, _gad, _serverHubForm);
					pSessions.Controls.Add(sne);
					sne.Resized += control_Resized;
					sne.SessionClosed += SessionClosedHandler;
					sne.Dock = DockStyle.Top;
					ResizeControl();
				}
				catch (Exception ex)
				{
					ResizeControl();
					if (ex.Message.Contains("(401)")) StandardMessage.ShowMessage("Login failed.", StandardMessage.Severity.WarningMessage, this);
					else if (ex.Message.Contains("(500)")) StandardMessage.ShowMessage("Login failed.", StandardMessage.Severity.WarningMessage, this);
					else StandardMessage.ShowMessage("Login failed.", StandardMessage.Severity.ErrorMessage, this, ex);
				}
			}
		}
		protected void control_Resized(object sender)
		{
			ResizeControl();
		}

		protected void SessionClosedHandler(object sender)
		{
			if (sender.GetType() == typeof(SessionEntry))
			{
				SessionEntry se = (SessionEntry)sender;
				pSessions.Controls.Remove(se);
				ResizeControl();
			}
		}


		private void ResizeControl()
		{
			// int h = 62;
			int h = _baseHeight;
			File.AppendAllText(_logFn, " Base server entry height: " + _baseHeight.ToString() + "\n");
			foreach (UserControl uc in pSessions.Controls)
			{
				h += uc.Height;
				File.AppendAllText(_logFn, " Session entry height: " + uc.Height.ToString() + "\n");
			}
			Height = h;
			File.AppendAllText(_logFn, " Computed server entry height: " + h.ToString() + "\n");
			Resized?.Invoke(this);

		}


		private void DoAutologin()
		{
			try
			{
				XmlElement alEl = (XmlElement)_svrEl.SelectSingleNode("auto_login");
				if (alEl != null && alEl.GetAttribute("enabled") == "true")
				{
					string autologinPath = Path.Combine(_gad.LocalRootPath, "autologin");
					if (!Directory.Exists(autologinPath)) Directory.CreateDirectory(autologinPath);
					string autologinFn = Path.Combine(autologinPath, Server.Id);
					if (Server.ServerResponse == null) StandardMessage.ShowMessage("Login failed.\nFailure connecting to server " + Server.Name, StandardMessage.Severity.ErrorMessage, this);
					else if (!File.Exists(autologinFn))
					{
						// Do nothing, silently let the user log on
						//StandardMessage.ShowMessage("Autologin failed.\nNo saved credentials for server " + _svr.Name, StandardMessage.Severity.ErrorMessage, this);
					}
					else
					{
						LoginCredentials cr = new LoginCredentials(autologinFn);
						CmnSession s = Server.GetSession(cr.Username, cr.Password);
						//s.AddSessionExtensions();
						SessionEntry sne = new SessionEntry();
						sne.Resized += control_Resized;
						sne.SessionClosed += SessionClosedHandler;
						sne.InitSessionEntry(s, Server, _svrEl, _gad, _serverHubForm);
						pSessions.Controls.Add(sne);
						sne.Dock = DockStyle.Top;
					}
				}
			}
			catch (Exception ex)
			{
				if (ex.Message.Contains("(401)")) StandardMessage.ShowMessage("Autologin failed.\nInvalid password.", StandardMessage.Severity.WarningMessage, this);
				else if (ex.Message.Contains("(500)")) StandardMessage.ShowMessage("Autologin failed.\nUnknown user.", StandardMessage.Severity.WarningMessage, this);
				else StandardMessage.ShowMessage("Autologin failed.", StandardMessage.Severity.ErrorMessage, this, ex);
			}
		}
	}
}
