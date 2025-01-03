using C4ServerConnector.Assets;
using C4Admin.Global;
using C4ServerConnector;
using C4GeneralGui.Helpers;
using C4GeneralGui.GuiElements;
using System.Xml;
using CDCplusLib.Common;

namespace C4Admin.GUI.AssetEditors
{
    public partial class EditLifecycle : Form
	{
		public C4Lifecycle Lifecycle { get; private set; }
		public bool SaveAs { get; private set; }
        private ListViewSort _lvwSort;
        private string _origName;
		private C4Session _c4s;
		private Dictionary<long, C4LifecycleState> _allStates;

		public EditLifecycle(string title, C4Session c4s, C4Lifecycle lifecycle, bool showSaveAs)
		{
			InitializeComponent();
			_c4s = c4s;
			_allStates = c4s.ListLifecycleStates();
			Text = title;
			Lifecycle = lifecycle;
			InitStatesListView();
			if (lifecycle == null)
			{
				txtId.Text = Definitions.NEW;
				vtxtName.Text = "";
				cboDefaultStateId.Text = "";
				_origName = null;
			}
			else
			{
				txtId.Text = Lifecycle.Id == null ? Definitions.NEW : Lifecycle.Id.ToString();
				vtxtName.Text = Lifecycle.Name;

				if (Lifecycle.DefaultStateId != null) cboDefaultStateId.Text = _allStates[(long)Lifecycle.DefaultStateId].Name;
				_origName = Lifecycle.Name;
			}

			GuiActions();
			pSaveAs.Visible = showSaveAs;
		}

		private void InitStatesListView()
		{
			lvwStates.Items.Clear();
			if(Lifecycle!=null)
			{
				foreach (C4LifecycleState state in Lifecycle.States.Values)
				{
					cboDefaultStateId.Items.Add(state);
					ListViewItem lvi = lvwStates.Items.Add(state.Id.ToString());
					lvi.Tag = state;
					lvi.SubItems.Add(state.Name);
					lvi.SubItems.Add(state.StateClass);
					lvi.SubItems.Add(state.LifecycleStateForCopyId == null ? "" : _allStates[(long)state.LifecycleStateForCopyId].Name);
				}
			}
            if (_lvwSort == null) _lvwSort = new ListViewSort(lvwStates, false);
            _lvwSort.Sort(_lvwSort.LastColumn, _lvwSort.ColumnSortOrder);
        }

        private void cmdOk_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.OK;
			SaveAs = false;
			long? id = null;
			if (txtId.Text != Definitions.NEW) id = long.Parse(txtId.Text);
			Lifecycle = new C4Lifecycle(vtxtName.Text, cboDefaultStateId.Text!=""? ((C4LifecycleState)cboDefaultStateId.SelectedItem).Id : null, id);
			Close();
		}

		private void cmdSaveAs_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.OK;
			SaveAs = true;
			long? id = null;
			if (txtId.Text != Definitions.NEW) id = long.Parse(txtId.Text);
			Lifecycle = new C4Lifecycle(vtxtName.Text, cboDefaultStateId.Text != "" ? ((C4LifecycleState)cboDefaultStateId.SelectedItem).Id : null, id);
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

		private void EditLifecycle_Load(object sender, EventArgs e)
		{
			ActiveControl = vtxtName;
		}
		private void lvwStates_MouseUp(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right)
			{
				ContextMenuStrip cm = new ContextMenuStrip();
				ToolStripItem tsi = null;
				if (lvwStates.SelectedItems.Count == 0)
				{
					tsi = GuiHelper.AppendToolStripItem(ref cm, lvwStates, "Add lifecycle state", "add", true);
					tsi.Click += HandleContextMenuClick;
				}
				if (lvwStates.SelectedItems.Count == 1)
				{
					tsi = GuiHelper.AppendToolStripItem(ref cm, lvwStates, "Edit lifecycle state", "edit", true);
					tsi.Click += HandleContextMenuClick;
					tsi = GuiHelper.AppendToolStripItem(ref cm, lvwStates, "Replace lifecycle state", "replace", true);
					tsi.Click += HandleContextMenuClick;
				}
				if (lvwStates.SelectedItems.Count > 0)
				{
					tsi = GuiHelper.AppendToolStripItem(ref cm, lvwStates, "Delete lifecycle state(s)", "delete", true);
					tsi.Click += HandleContextMenuClick;
				}

				cm.Show(lvwStates, e.Location);
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
							EditLifecycleState elcs = new EditLifecycleState("New lifecycle state", _c4s, null, Lifecycle, false);
							elcs.StartPosition = FormStartPosition.Manual;
							elcs.Location = MousePosition;
							if (elcs.ShowDialog(this) == DialogResult.OK)
							{
								HashSet<C4LifecycleState> lifecycleStates = new HashSet<C4LifecycleState>();
								lifecycleStates.Add(elcs.LifecycleState);
								XmlDocument result = _c4s.CreateLifecycleStates(lifecycleStates);
								foreach (XmlElement lifecycleEl in result.DocumentElement.SelectNodes("lifecycleStates/lifecycleState"))
								{
									C4LifecycleState lc = new C4LifecycleState(lifecycleEl);
									Lifecycle.States.Add((long)lc.Id, lc);
								}
								InitStatesListView();
							}
							break;
						}
					case "edit":
						{
							ListViewItem lvi = lvwStates.SelectedItems[0];
							C4LifecycleState lifecycleState = (C4LifecycleState)lvi.Tag;
							EditLifecycleState elcs = new EditLifecycleState("Edit lifecycle state", _c4s, lifecycleState, Lifecycle, true);
							elcs.StartPosition = FormStartPosition.Manual;
							elcs.Location = MousePosition;
							if (elcs.ShowDialog(this) == DialogResult.OK)
							{
								HashSet<C4LifecycleState> groups = new HashSet<C4LifecycleState>();
								if (elcs.SaveAs)
								{
									HashSet<C4LifecycleState> lifecycleStates = new HashSet<C4LifecycleState>();
									lifecycleStates.Add(elcs.LifecycleState);
									XmlDocument result = _c4s.CreateLifecycleStates(lifecycleStates);
									foreach (XmlElement lifecycleEl in result.DocumentElement.SelectNodes("lifecycleStates/lifecycleState"))
									{
										C4LifecycleState lc = new C4LifecycleState(lifecycleEl);
										Lifecycle.States.Add((long)lc.Id, lc);
									}
								}
								else
								{
									HashSet<C4LifecycleState> lifecycleStates = new HashSet<C4LifecycleState>();
									lifecycleStates.Add(elcs.LifecycleState);
									XmlDocument result = _c4s.UpdateLifecycleStates(lifecycleStates);
									foreach (XmlElement lifecycleEl in result.DocumentElement.SelectNodes("lifecycleStates/lifecycleState"))
									{
										C4LifecycleState lc = new C4LifecycleState(lifecycleEl);
										Lifecycle.States[(long)lc.Id] = lc;
									}
								}
								InitStatesListView();
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
							foreach (ListViewItem lvi in lvwStates.SelectedItems)
							{
								ids.Add(long.Parse(lvi.Text));
							}


							XmlDocument result = _c4s.DeleteLifecycleStates(ids);
							if (result.DocumentElement.SelectSingleNode("success[.='false']") != null) StandardMessage.ShowMessage("Failure deleting lifecycle state(s).", StandardMessage.Severity.ErrorMessage, this, null);
							foreach (long id in ids) Lifecycle.States.Remove(id);
							InitStatesListView();
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
