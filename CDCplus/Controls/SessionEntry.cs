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
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Xml;
using C4Admin.GUI;
using C4ObjectApi.Repository;
using CDCplusLib.Common;
using CDCplusLib.DataModel;
using CDCplusLib.EventData;
using CDCplusLib.Interfaces;

namespace CDCplus.Controls
{
    public partial class SessionEntry : UserControl
	{
		public delegate void ResizedHandler(object sender);
		public delegate void SessionClosedHandler(object sender);

		public event ResizedHandler Resized;
		public event SessionClosedHandler SessionClosed;

		private Timer _t;
		private CmnSession _s;
		private Form _serverHubForm;
		private XmlElement _svrEl;
		private GlobalApplicationData _gad;
		private Dictionary<string, WindowEntry> _windowLookup;
		private string _notificationQuery;
		private HashSet<long> _prevIds;
		private ContextFunctionsContainer _contextFunctionsContainer;

        public SessionEntry()
		{
			InitializeComponent();
		}

		public void InitSessionEntry(CmnSession s, CmnServer svr, XmlElement svrEl, GlobalApplicationData gad, Form serverHubForm)
		{
			_s = s;
			_serverHubForm = serverHubForm;
			_svrEl = svrEl;
			_gad = gad;
			_windowLookup = new Dictionary<string, WindowEntry>();
			_prevIds = null;
			UserEntry ue = new UserEntry(_s, svr, _gad);
			ue.InitUserEntry(s.User);
			ue.AddWindowClicked += AddWindowClickedHandler;
			ue.AddAdminWindowClicked += AddAdminWindowClickedHandler;
			ue.CloseSessionClicked += CloseSessionClickedHandler;
			ue.Dock = DockStyle.Bottom;
			pSessionContent.Controls.Add(ue);
			//ue.SendToBack();
			if (_svrEl.SelectSingleNode("auto_show_window[@enabled='false']") == null) AddWindow();

			XmlNode refreshTimerInterval = _s.UserConfig.DocumentElement.SelectSingleNode("global_settings/auto_refresh_interval");
			if (refreshTimerInterval != null && _s.SessionConfig.C4Sc.ObjectTypesByName.ContainsKey("_task") && _s.SessionConfig.C4Sc.ObjectTypesByName.ContainsKey("notification"))
			{
				_notificationQuery = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>" +
				 "<BooleanQuery>" +
					"<Clause occurs=\"must\">" +
						"<ExactPointQuery type=\"long\" fieldName=\"locker\" value=\"" + _s.User.Id.ToString() + "\"/>" +
					"</Clause>" +
					"<Clause occurs=\"must\">" +
						"<BooleanQuery minimumNumberShouldMatch=\"1\">" +
							"<Clause occurs=\"should\">" +
				 "<BooleanQuery>" +
								"<Clause occurs=\"must\">" +
									"<TermQuery fieldName=\"index.wftask_status\">active</TermQuery>" +
								"</Clause>" +
								"<Clause occurs=\"must\">" +
								"<ExactPointQuery type=\"long\" fieldName=\"object_type\" value=\"" + ((long)_s.SessionConfig.C4Sc.ObjectTypesByName["_task"].Id).ToString() + "</Clause>" +
						"</BooleanQuery>" +
							"</Clause>" +
							 "<Clause occurs=\"should\">" +
								"<ExactPointQuery type=\"long\" fieldName=\"object_type\" value=\"" + ((long)_s.SessionConfig.C4Sc.ObjectTypesByName["notification"].Id).ToString() + "</Clause>" +
						"</BooleanQuery>" +
					"</Clause>" +
				 "</BooleanQuery>";
				_prevIds = _s.CommandSession.SearchObjectIds(_notificationQuery);
				_t = new Timer();
				_t.Interval = int.Parse(refreshTimerInterval.InnerText);
				_t.Start();
				_t.Tick += new EventHandler(t__Tick);

			}
		}
		private void t__Tick(Object myObject, EventArgs myEventArgs)
		{
			_t.Stop();
			try
			{
				HashSet<long> ids = _s.CommandSession.SearchObjectIds(_notificationQuery);
				HashSet<long> newMsgs = new HashSet<long>();
				foreach (long id in ids) if (!_prevIds.Contains(id)) newMsgs.Add(id);
				Dictionary<long, CmnObject> newMsgObjs = _s.GetObjects(newMsgs, false);
				foreach (CmnObject o in newMsgObjs.Values)
				{
					ShowBalloon(o.Name, "You received a notification."); // TODO: put some smarter text here 
																		 // could be retrieved from summary - a standard field should be added
				}
				_prevIds = ids;
			}
			catch (Exception ex)
			{
				System.Diagnostics.Debug.Print("Exception: " + ex.GetType().ToString() + "\n" + ex.Message + "\n" + ex.StackTrace);
			}
			_t.Start();
		}

		private void ShowBalloon(string title, string msg)
		{
			NotifyIcon notification = new NotifyIcon()
			{
				Visible = true,
				Icon = System.Drawing.SystemIcons.Information,
				BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info,
				BalloonTipTitle = title,
				BalloonTipText = msg,
			};
			notification.ShowBalloonTip(5000);

			// The notification should be disposed when you don't need it anymore,
			// but doing so will immediately close the balloon if it's visible.
			//notification.Dispose();
		}

		private void ResizeControl()
		{
			int h = 10; // 50;
			foreach (Control c in pSessionContent.Controls)
			{
				h += c.Size.Height;
			}
			Size = new Size(Size.Width, h);
			Resized?.Invoke(this);
		}

		private void AddWindow(WindowSelectionData wsd = null)
		{
			XmlElement defaultWindowEl = (XmlElement)_s.UserConfig.DocumentElement.SelectSingleNode("classes/default_window");
			string assemblyName = defaultWindowEl.GetAttribute("assembly");
			string typeName = defaultWindowEl.GetAttribute("type");
			ISessionWindow sw = (ISessionWindow)_s.SessionConfig.GetAssembly(assemblyName).CreateInstance(assemblyName + "." + typeName);
			WindowEntry we = new WindowEntry();
			_windowLookup.Add(sw.Guid, we);
			we.InitWindowEntry(_s, sw, "[new window]", false);
			we.Dock = DockStyle.Bottom;
			pSessionContent.Controls.Add(we);
			ResizeControl();

			sw.SessionWindowRequest += SessionWindowRequestHandler;
			sw.WindowClosed += WindowClosedHandler;
			sw.TreeSelectionChanged += TreeSelectionChangedHandler;

			if (wsd == null)
			{
				wsd=new WindowSelectionData();
				wsd.RootNodeType = CDCplusLib.Common.GUI.SessionTree.RootNodeTypes.Session;
				wsd.SelectedFolder= _s.RootFolder;
			}

            if (_contextFunctionsContainer != null) sw.ContextFunctions = _contextFunctionsContainer;
            sw.ShowSessionWindow(_s, _gad, wsd);
			if (_contextFunctionsContainer == null) _contextFunctionsContainer = sw.ContextFunctions;
		}
		private void AddAdminWindow()
		{
			ISessionWindow sw = new C4AMainWindow();
			WindowEntry we = new WindowEntry();
			_windowLookup.Add(sw.Guid, we);

			sw.SessionWindowRequest += SessionWindowRequestHandler;
			sw.WindowClosed += WindowClosedHandler;
			we.InitWindowEntry(_s, sw, "Cinnamon Administration", true);

			//we.AddWindowClicked += AddWindowClickedHandler;
			we.Dock = DockStyle.Bottom;
			pSessionContent.Controls.Add(we);
			sw.ShowSessionWindow(_s, _gad, null);
			//we.SendToBack();

			ResizeControl();
		}
		private void AddWindowClickedHandler(object sender)
		{
			AddWindow();
		}
		private void AddAdminWindowClickedHandler(object sender)
		{
			AddAdminWindow();
		}
		private void CloseSessionClickedHandler(object sender)
		{
			if (MessageBox.Show("Are you sure to close all windows and log off user " + _s.User.ToString() + "?", "Log off", MessageBoxButtons.YesNo) == DialogResult.Yes)
			{
				UserEntry ue = null;
				List<ISessionWindow> sessionWindows = new List<ISessionWindow>();
				foreach (Control c in pSessionContent.Controls)
				{
					if (c.GetType() == typeof(WindowEntry))
					{
						WindowEntry we = (WindowEntry)c;
						sessionWindows.Add(we.SessionWindow);
					}
					else if (c.GetType() == typeof(UserEntry))
					{
						ue = (UserEntry)c;
					}
				}
				foreach (ISessionWindow sw in sessionWindows)
				{
					sw.CloseWindow();
				}
				_s.Close();

				SessionClosed?.Invoke(this);
			}
		}
		private void WindowClosedHandler(ISessionWindow sw)
		{
			WindowEntry we = _windowLookup[sw.Guid];
			pSessionContent.Controls.Remove(we);
			_windowLookup.Remove(sw.Guid);
			ResizeControl();
			Resized?.Invoke(this);
		}
		private void SessionWindowRequestHandler(WindowSelectionData wsd)
		{
			AddWindow(wsd);
		}
		private void TreeSelectionChangedHandler(WindowSelectionData wsd, ISessionWindow sw)
		{
			if (_windowLookup.ContainsKey(sw.Guid))
			{
				WindowEntry we = _windowLookup[sw.Guid];
				we.Path = sw.WindowTitle;
			}
		}
	}
}
