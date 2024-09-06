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
using CDCplus.GUI;
using CDCplus.DataModel;
using System.IO;
using CDCplusLib.Common;
using C4ObjectApi.Repository;
using C4ServerConnector.Assets;

namespace CDCplus.Controls
{
	public partial class UserEntry : UserControl
	{
		public delegate void AddWindowClickedHandler(object sender);
		public delegate void AddAdminWindowClickedHandler(object sender);
		public delegate void CloseSessionClickedHandler(object sender);

		public event AddWindowClickedHandler AddWindowClicked;
		public event AddAdminWindowClickedHandler AddAdminWindowClicked;
		public event CloseSessionClickedHandler CloseSessionClicked;

		private readonly CmnServer _svr;
		private readonly GlobalApplicationData _gad;
		public UserEntry(CmnSession s, CmnServer svr, GlobalApplicationData gad)
		{
			InitializeComponent();
			Session = s;
			_svr = svr;
			_gad = gad;
		}

		public void InitUserEntry(C4User u)
		{
			cmdAddAdminWindow.Visible = Session.IsSuperuser;
			lblUserName.Text = u.ToString();
		}

		public CmnSession Session { get; }

		private void cmdAddWindow_Click(object sender, EventArgs e)
		{
			AddWindowClicked?.Invoke(this);
		}

		private void cmdCloseSession_Click(object sender, EventArgs e)
		{
			CloseSessionClicked?.Invoke(this);
		}

		private void cmdSetPassword_Click(object sender, EventArgs e)
		{
			SetPassword sp = new SetPassword();
			if (sp.ShowDialog() == DialogResult.OK)
			{
				Session.CommandSession.SetPassword((long)Session.User.Id, sp.Password);
				if (sp.SaveCredentials)
				{
					LoginCredentials cr = new LoginCredentials(Session.User.Name, sp.Password);
					string autologinPath = Path.Combine(_gad.LocalRootPath, "autologin");
					string autologinFn = Path.Combine(autologinPath, _svr.Id);
					cr.Save(autologinFn);
				}

			}
		}

		private void cmdAddAdminWindow_Click(object sender, EventArgs e)
		{
			AddAdminWindowClicked?.Invoke(this);
		}
	}
}
