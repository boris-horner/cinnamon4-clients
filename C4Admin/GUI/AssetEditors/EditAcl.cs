using C4Admin.Global;
using C4GeneralGui.GuiElements;
using C4GeneralGui.Helpers;
using C4ServerConnector;
using C4ServerConnector.Assets;
using CDCplusLib.Common;
using System.Xml;

namespace C4Admin.GUI.AssetEditors
{
    public partial class EditAcl : Form
	{
		private C4Session _s;
        private ListViewSort _lvwSort;
		private string _origName;
		private Dictionary<long, C4Group> _groups;
        public C4Acl Acl { get; private set; }
		public bool SaveAs { get; private set; }
		public EditAcl(string title, C4Session s, C4Acl acl, bool showSaveAs)
		{
			InitializeComponent();
			Text = title;
			Acl = acl;
			_s = s;
			if (acl == null)
			{
				txtId.Text = Definitions.NEW;
				vtxtName.Text = "";
				_origName = null;
			}
			else
			{
				txtId.Text = Acl.Id == null ? Definitions.NEW : Acl.Id.ToString();
				vtxtName.Text = Acl.Name;
				_origName = Acl.Name;
			}
			_groups = s.ListGroups();
			InitGroupsListView();

			GuiActions();
			pSaveAs.Visible = showSaveAs;
		}

		private void InitGroupsListView()
		{
			lvwAclGroups.Items.Clear();
			if (Acl != null)
			{
				Dictionary<long, C4AclGroup> aclGroups = _s.ListAclGroupsByGroupOrAcl((long)Acl.Id, false);
				foreach (C4AclGroup aclGroup in aclGroups.Values)
				{
					ListViewItem lvi = lvwAclGroups.Items.Add(aclGroup.Id.ToString());
					lvi.Tag = aclGroup;
					lvi.SubItems.Add(_groups[(long)(aclGroup.GroupId)].Name);
				}
			}
            if (_lvwSort == null) _lvwSort = new ListViewSort(lvwAclGroups, false);
            _lvwSort.Sort(_lvwSort.LastColumn, _lvwSort.ColumnSortOrder);
        }

        private void cmdOk_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.OK;
			SaveAs = false;
			long? id = null;
			if (txtId.Text != Definitions.NEW) id = long.Parse(txtId.Text);
			Acl = new C4Acl(vtxtName.Text, id);
			Close();
		}

		private void cmdSaveAs_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.OK;
			SaveAs = true;
			long? id = null;
			if (txtId.Text != Definitions.NEW) id = long.Parse(txtId.Text);
			Acl = new C4Acl(vtxtName.Text, id);
			Close();
		}
		private void cmdCancel_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.Cancel;
			Close();
		}

		private void GuiActions()
		{
			bool valid = vtxtName.IsValid;
			cmdOk.Enabled = valid;
			cmdSaveAs.Enabled = (vtxtName.Text != _origName && valid);
		}
		private void vtxtName_TextChanged(object sender, EventArgs e)
		{
			GuiActions();
		}

		private void EditFolderType_Load(object sender, EventArgs e)
		{
			ActiveControl = vtxtName;
		}
		private void lvwAclGroups_MouseUp(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right)
			{
				ContextMenuStrip cm = new ContextMenuStrip();
				ToolStripItem tsi = null;
				if (lvwAclGroups.SelectedItems.Count == 0)
				{
					tsi = GuiHelper.AppendToolStripItem(ref cm, lvwAclGroups, "Add ACL group", "add", true);
					tsi.Click += HandleContextMenuClick;
				}
				if (lvwAclGroups.SelectedItems.Count == 1)
				{
					tsi = GuiHelper.AppendToolStripItem(ref cm, lvwAclGroups, "Edit ACL group", "edit", true);
					tsi.Click += HandleContextMenuClick;
					tsi = GuiHelper.AppendToolStripItem(ref cm, lvwAclGroups, "Replace ACL group", "replace", true);
					tsi.Click += HandleContextMenuClick;
				}
				if (lvwAclGroups.SelectedItems.Count > 0)
				{
					tsi = GuiHelper.AppendToolStripItem(ref cm, lvwAclGroups, "Delete ACL group(s)", "delete", true);
					tsi.Click += HandleContextMenuClick;
				}

				cm.Show(lvwAclGroups, e.Location);
			}
		}
		private void HandleContextMenuClick(object sender, System.EventArgs e)
		{
			ToolStripMenuItem tsmi = (ToolStripMenuItem)sender;
			try
			{
				switch (tsmi.Name)
				{
					case "add":
						{
							EditAclGroup eag = new EditAclGroup("New ACL group", _s, null, false, Acl.Id, null);
							eag.StartPosition = FormStartPosition.Manual;
							eag.Location = MousePosition;
							if (eag.ShowDialog(this) == DialogResult.OK)
							{
								HashSet<C4AclGroup> aclGroups = new HashSet<C4AclGroup>();
								aclGroups.Add(eag.AclGroup);
								XmlDocument result = _s.CreateAclGroups(aclGroups);
								InitGroupsListView();
							}
							break;
						}
					case "edit":
						{
							ListViewItem lvi = lvwAclGroups.SelectedItems[0];
							C4AclGroup aclGroup = (C4AclGroup)lvi.Tag;
							EditAclGroup eag = new EditAclGroup("Edit ACL group", _s, aclGroup, true, aclGroup.AclId, aclGroup.GroupId);
							eag.StartPosition = FormStartPosition.Manual;
							eag.Location = MousePosition;
							if (eag.ShowDialog(this) == DialogResult.OK)
							{
								HashSet<C4Group> groups = new HashSet<C4Group>();
								if (eag.SaveAs)
								{
									HashSet<C4AclGroup> aclGroups = new HashSet<C4AclGroup>();
									aclGroups.Add(eag.AclGroup);
									XmlDocument result = _s.CreateAclGroups(aclGroups);
								}
								else
								{
									HashSet<C4AclGroup> aclGroups = new HashSet<C4AclGroup>();
									aclGroups.Add(eag.AclGroup);
									XmlDocument result = _s.UpdateAclGroups(aclGroups);
								}
								InitGroupsListView();
							}
							break;
						}
					case "replace":
						{
							StandardMessage.ShowMessage("This function is not yet implemented.", StandardMessage.Severity.ErrorMessage, this, null);
							break;
						}
					case "delete":
						{
							// build HashSet of group ids
							HashSet<long> ids = new HashSet<long>();
							foreach (ListViewItem lvi in lvwAclGroups.SelectedItems)
							{
								ids.Add(long.Parse(lvi.Text));
							}


							XmlDocument result = _s.DeleteAclGroups(ids);
							if (result.DocumentElement.SelectSingleNode("success[.='false']") != null) StandardMessage.ShowMessage("Failure deleting ACL groups(s).", StandardMessage.Severity.ErrorMessage, this, null);
							InitGroupsListView();
							break;
						}
				}
			}
			catch (Exception ex)
			{
				StandardMessage.ShowMessage("Caught Exception", StandardMessage.Severity.ErrorMessage, this, ex);
			}

		}
	}
}
