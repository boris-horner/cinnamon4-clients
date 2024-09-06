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
using CDCplusLib.Interfaces;
using CDCplusLib.Messages;
using System.Xml;
using C4ObjectApi.Interfaces;
using C4ObjectApi.Repository;
using C4GeneralGui.GuiElements;
using C4ServerConnector.Assets;
using C4ObjectApi.Helpers;
using CDCplusLib.Properties;
using CDCplusLib.Common.GUI;
using CDCplusLib.DataModel;

namespace CDCplusLib.TabControls
{
	public partial class EditRawMetadataTabControl : UserControl, IGenericControl
	{
		private CmnSession _s;
		private C4Metadata _md;
		private GlobalApplicationData _gad;
		private bool _initCompleted = false;
		private bool _txtChangeEventActive = false;
		private bool _dirty;
		private Dictionary<long, bool> _origMetasetIdsDirty;
		private Dictionary<long, IRepositoryNode> _dict;
		private TreeNode _clickedNode;

		public event IGenericControl.MessageSentEventHandler MessageSent;

		public EditRawMetadataTabControl()
		{
			InitializeComponent();
		}
		public bool HasSelection
		{
			get
			{
				return false;
			}
		}
		public Dictionary<long, IRepositoryNode> Selection
		{
			get
			{
				return null;
			}
			set
			{

			}
		}
		public bool AutoRefresh
		{
			get
			{
				return true;
			}
		}
		public bool ListContext
		{
			get
			{
				return true;
			}
		}

		public void Reset(CmnSession s, GlobalApplicationData globalAppData, XmlElement configEl)
		{
			_gad = globalAppData;
			_s = s;
		}

		public string GetTabText()
		{
			return Properties.Resources.lblRawMetadata;
		}

		private void ActivateControls(bool dirty)
		{
			if (_initCompleted)
			{
				IRepositoryNode ow = _dict.First().Value;
				_dirty = dirty;
				if (ow.GetType() == typeof(CmnObject))
				{
					// object
					CmnObject o = (CmnObject)ow;
					cmdSave.Enabled = o.Permissions.Object_Lock && o.Permissions.Node_Metadata_Write && _dirty;
				}
				else
				{
					// folder
					CmnFolder f = (CmnFolder)ow;
					cmdSave.Enabled = f.Permissions.Node_Metadata_Write && _dirty;
				}
				if(tvwMetasets.SelectedNode!=null && tvwMetasets.SelectedNode.Tag!=null && tvwMetasets.SelectedNode.Tag.GetType()==typeof(C4Metaset))
				{
                    C4Metaset ms = (C4Metaset)tvwMetasets.SelectedNode.Tag;
					if(ms.Id!=null && _origMetasetIdsDirty.ContainsKey((long)ms.Id)) _origMetasetIdsDirty[(long)ms.Id] = true;
                }	
			}
		}
		public void Init(Dictionary<long, IRepositoryNode> dict, IClientMessage msg)
		{
			_dict = dict;
			IRepositoryNode ow = _dict.First().Value;
			if (ow.GetType() == typeof(CmnObject))
			{
				// object
				_md = _s.CommandSession.GetObjectMeta(ow.Id);
			}
			else
			{
				// folder
				_md = _s.CommandSession.GetFolderMeta(ow.Id);
			}
			xtbMetadata.Text = "";
            xtbMetadata.Enabled = false;
            tvwMetasets.Nodes.Clear();
			_origMetasetIdsDirty = new Dictionary<long, bool>();
			foreach (long msTypeId in _md.MetasetsByTypeId.Keys)
			{
				C4MetasetType msType = _s.SessionConfig.C4Sc.MetasetTypesById[msTypeId];
				TreeNode msTypeNode = tvwMetasets.Nodes.Add(msType.Name);
				msTypeNode.Tag = msType;
				foreach (C4Metaset ms in _md.MetasetsByTypeId[msTypeId])
				{
					TreeNode msNode = msTypeNode.Nodes.Add(ms.Id.ToString());
					msNode.Tag = ms;
					_origMetasetIdsDirty.Add((long)ms.Id, false);
				}
			}

			//xtbMetadata.SetText(ow.Metadata.LegacyXml.OuterXml);
			if (msg != null) MessageReceived(msg);
			tvwMetasets.ExpandAll();
			_initCompleted = true;
			_txtChangeEventActive = true;
			ActivateControls(false);
		}

		public bool IsValid(Dictionary<long, IRepositoryNode> dict, IGenericControl.ContextType ct)
		{
			return dict.Count == 1;
		}
		private void cmdSave_Click(object sender, EventArgs e)
		{
			Save();
		}
		public bool IsDirty
		{
			get
			{
				return _dirty;
			}
		}
        public void Save()
		{
			try
			{
				if(xtbMetadata.Enabled && xtbMetadata.Text.Length>0 && tvwMetasets.SelectedNode!=null && tvwMetasets.SelectedNode.Tag.GetType()==typeof(C4Metaset))
				{
					XmlDocument xmlDoc = new XmlDocument();
					xmlDoc.LoadXml(xtbMetadata.Text);
					((C4Metaset)tvwMetasets.SelectedNode.Tag).Content = xmlDoc.DocumentElement;
				}
				IRepositoryNode ow = _dict.First().Value;

				// Build list of metasets to change
				Dictionary<long, HashSet<C4Metaset>> updateMetasets = new Dictionary<long, HashSet<C4Metaset>>();
				foreach(long msTypeId in _md.MetasetsByTypeId.Keys)
				{
                    foreach(C4Metaset ms in _md.MetasetsByTypeId[msTypeId])
					{
						if(ms.Id != null && _origMetasetIdsDirty[(long)ms.Id])
						{
							if(!updateMetasets.ContainsKey(ow.Id)) updateMetasets.Add(ow.Id, new HashSet<C4Metaset>());
							updateMetasets[ow.Id].Add(ms);
                        }
                    }
                }


                // Build list of new metasets
                Dictionary<long, HashSet<C4Metaset>> createMetasets = new Dictionary<long, HashSet<C4Metaset>>();
                HashSet<long> remainingMetasetIds = new HashSet<long>();
                foreach (TreeNode msTypeNode in tvwMetasets.Nodes)
				{
                    C4MetasetType msType = (C4MetasetType)msTypeNode.Tag;
                    foreach(TreeNode msNode in msTypeNode.Nodes)
					{
						C4Metaset nodeMs = (C4Metaset)msNode.Tag;
                        if (nodeMs.Id == null)
						{
                            if (!createMetasets.ContainsKey(ow.Id)) createMetasets.Add(ow.Id, new HashSet<C4Metaset>());
                            createMetasets[ow.Id].Add(nodeMs);
                        }
                        else remainingMetasetIds.Add((long)nodeMs.Id);
                    }
                }


				// Build list of metasets to delete
				HashSet<long> deleteMetasetIds = new HashSet<long>();
				foreach(long origIds in _origMetasetIdsDirty.Keys)
				{
					if (!remainingMetasetIds.Contains(origIds)) deleteMetasetIds.Add(origIds);
				}

				if (ow.GetType() == typeof(CmnObject))
				{
					// object
					CmnObject o = (CmnObject)ow;
					o.Lock();
                    if(deleteMetasetIds.Count()>0) _s.CommandSession.DeleteObjectMetasets(deleteMetasetIds);
					if(createMetasets.Keys.Count()>0) _s.CommandSession.CreateObjectMeta(createMetasets);
                    if(updateMetasets.Keys.Count() > 0) _s.CommandSession.UpdateObjectMetaContent(updateMetasets);
                    o.Unlock();
				}
				else
				{
					// folder
					CmnFolder f = (CmnFolder)ow;
                    if (deleteMetasetIds.Count() > 0) _s.CommandSession.DeleteFolderMetasets(deleteMetasetIds);
                    if (createMetasets.Keys.Count() > 0) _s.CommandSession.CreateFolderMeta(createMetasets);
                    if (updateMetasets.Keys.Count() > 0) _s.CommandSession.UpdateFolderMetaContent(updateMetasets);
                }
                ActivateControls(false);
				ObjectsModifiedMessage msg = new ObjectsModifiedMessage();
				msg.ModificationType = ObjectsModifiedMessage.ModificationTypes.CustomMetadataChanged;
				msg.ModifiedObjects.Add(ow.Id, ow);
				MessageSent?.Invoke(msg);
			}
			catch (Exception ex)
			{
				StandardMessage.ShowMessage(Properties.Resources.msgFailureWritingMetadata, StandardMessage.Severity.ErrorMessage, this, ex);   // TODO: I18N
			}
		}

		public void ReInit()
		{
			Init(_dict, null);
		}
		public void MessageReceived(IClientMessage msg)
		{
			// Nothing to do
		}

		private void xtbMetadata_TextChanged(object sender, EventArgs e)
		{
			if(_txtChangeEventActive) ActivateControls(true);
		}

		private void tvwMetasets_AfterSelect(object sender, TreeViewEventArgs e)
		{
			if (tvwMetasets.SelectedNode != null)
			{
				if (tvwMetasets.SelectedNode.Tag != null && tvwMetasets.SelectedNode.Tag.GetType() == typeof(C4Metaset))
				{
					C4Metaset ms = (C4Metaset)tvwMetasets.SelectedNode.Tag;
					xtbMetadata.SetText(ms.Content.OuterXml);
					xtbMetadata.Enabled = true;
				}
				else
				{
					xtbMetadata.SetText("");
					xtbMetadata.Enabled = false;
				}
			}
			else
			{
				xtbMetadata.SetText("");
				xtbMetadata.Enabled = false;
			}
			_txtChangeEventActive=true; 
		}

		private void tvwMetasets_BeforeSelect(object sender, TreeViewCancelEventArgs e)
		{
			_txtChangeEventActive = false;
			if (tvwMetasets.SelectedNode != null)
			{
				if (tvwMetasets.SelectedNode.Tag != null && tvwMetasets.SelectedNode.Tag.GetType() == typeof(C4Metaset))
				{
					C4Metaset ms = (C4Metaset)tvwMetasets.SelectedNode.Tag;
					XmlDocument m = new XmlDocument();
					m.LoadXml(xtbMetadata.Text);
					ms.Content = m.DocumentElement;
				}
			}
		}

		private void tvwMetasets_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
		{
			if (e.Button == MouseButtons.Right)
			{
				_clickedNode = e.Node;
				ContextMenuStrip contextMenuStrip = new ContextMenuStrip();
				if (_clickedNode.Tag.GetType() == typeof(C4Metaset))
				{
					contextMenuStrip.Items.Add(Resources.mnuDeleteMetaset, null, HandleContextMenu).Name = "delms";
					contextMenuStrip.Show(tvwMetasets, e.Location);
				}
				else
				{
					C4MetasetType metasetType = (C4MetasetType)_clickedNode.Tag;
					if (_clickedNode.Nodes.Count == 0 || !metasetType.Unique) contextMenuStrip.Items.Add(Resources.mnuAddMetaset, null, HandleContextMenu).Name = "addms";
					contextMenuStrip.Show(tvwMetasets, e.Location);
				}
			}
		}
		private void HandleContextMenu(object sender, EventArgs e)
		{
			// Handle context menu item click event here
			ToolStripItem menuItem = (ToolStripItem)sender;
			string itemName = menuItem.Name;
			switch (itemName)
			{
				case "addmst":
					{
						HashSet<SimpleDisplayItem> missingMsTypes = new HashSet<SimpleDisplayItem>();
						foreach (C4MetasetType msType in _s.SessionConfig.C4Sc.MetasetTypesByName.Values) if (!_md.MetasetsByTypeId.ContainsKey((long)msType.Id)) missingMsTypes.Add(new SimpleDisplayItem(msType.Name, msType.ToString(), msType));
						if (missingMsTypes.Count > 0)
						{
							EditListValue elv = new EditListValue(Resources.mnuAddMetasetType, Resources.mnuAddMetasetType, null, missingMsTypes);
							if (elv.ShowDialog(this) == DialogResult.OK)
							{
								C4MetasetType msType = elv.Value.Tag as C4MetasetType;
								_md.MetasetsByTypeId.Add((long)msType.Id, new HashSet<C4Metaset>());
								XmlDocument msDoc = new XmlDocument();
								msDoc.LoadXml("<content/>");
								C4Metaset newMs = new C4Metaset((long)msType.Id, _dict.Values.First().Id, msDoc.DocumentElement);
								_md.MetasetsByTypeId[(long)msType.Id].Add(newMs);

								TreeNode msTypeNode = tvwMetasets.Nodes.Add(msType.Name);
								msTypeNode.Tag = msType;

								TreeNode msNode = msTypeNode.Nodes.Add("NEW");
								msNode.Tag = newMs;
								tvwMetasets.ExpandAll();
								ActivateControls(true);
							}
						}
						break;
					}
				case "addms":
					{
						C4MetasetType msType = (C4MetasetType)_clickedNode.Tag;
						XmlDocument msDoc = new XmlDocument();
						msDoc.LoadXml("<content/>");
						C4Metaset newMs = new C4Metaset((long)msType.Id, _dict.Values.First().Id, msDoc.DocumentElement);
						_md.MetasetsByTypeId[(long)msType.Id].Add(newMs);

						TreeNode msNode = _clickedNode.Nodes.Add("NEW");
						msNode.Tag = newMs;
						tvwMetasets.ExpandAll();
						ActivateControls(true);
						break;
					}
				case "delms":
					{
						C4Metaset ms = (C4Metaset)_clickedNode.Tag;
						C4MetasetType metasetType = (C4MetasetType)_clickedNode.Parent.Tag;
						_md.MetasetsByTypeId[(long)metasetType.Id].Remove(ms);
						TreeNode parentNode = _clickedNode.Parent;
						_clickedNode.Parent.Nodes.Remove(_clickedNode);
						if (parentNode.Nodes.Count == 0) tvwMetasets.Nodes.Remove(parentNode);
						if (_md.MetasetsByTypeId[(long)metasetType.Id].Count == 0) _md.MetasetsByTypeId.Remove((long)metasetType.Id);
						ActivateControls(true);
						break;
					}
			}
		}

		private void tvwMetasets_MouseDown(object sender, MouseEventArgs e)
		{
			// Check if the right mouse button was clicked and the mouse is not over any node
			if (e.Button == MouseButtons.Right && tvwMetasets.GetNodeAt(e.Location) == null)
			{
				ContextMenuStrip contextMenuStrip = new ContextMenuStrip();
				contextMenuStrip.Items.Add(Resources.mnuAddMetasetType, null, HandleContextMenu).Name = "addmst";
				contextMenuStrip.Show(tvwMetasets, e.Location);
			}
		}
	}
}

