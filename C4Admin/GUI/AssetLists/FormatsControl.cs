using C4ServerConnector.Assets;
using C4Admin.GUI.AssetEditors;
using C4Admin.Interfaces;
using C4GeneralGui.Helpers;
using C4ServerConnector;
using C4GeneralGui.GuiElements;
using System.Xml;
using CDCplusLib.Common;

namespace C4Admin.GUI.AssetLists
{
    public partial class FormatsControl : UserControl, ITabView
	{
        private ListViewSort _lvwSort;
        private C4Session _s;
		private Dictionary<long, C4ObjectType> _objectTypes;
		public FormatsControl()
		{
			InitializeComponent();
		}
		public void Init(C4Session s)
		{
			_s = s;
			Dictionary<long, C4Format> assets = _s.ListFormats();
			_objectTypes = _s.ListObjectTypes();
			lvw.Items.Clear();
			foreach (C4Format a in assets.Values)
			{
				ListViewItem lvi = lvw.Items.Add(a.Id.ToString());
				lvi.SubItems.Add(a.Name);
				lvi.SubItems.Add(a.ContentType);
				lvi.SubItems.Add(a.Extension);
				lvi.SubItems.Add(a.DefaultObjectTypeId == null ? "" : _objectTypes[(long)a.DefaultObjectTypeId].Name);
				lvi.SubItems.Add(a.IndexMode);
				lvi.Tag = a;
			}
            if (_lvwSort == null) _lvwSort = new ListViewSort(lvw);
            _lvwSort.Sort(_lvwSort.LastColumn, _lvwSort.ColumnSortOrder);
        }

        private void lvw_MouseUp(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right)
			{
				ContextMenuStrip cm = new ContextMenuStrip();
				ToolStripItem tsi = null;
				if (lvw.SelectedItems.Count == 0)
				{
					tsi = GuiHelper.AppendToolStripItem(ref cm, lvw, "Add format", "add", true);
					tsi.Click += HandleContextMenuClick;
				}
				if (lvw.SelectedItems.Count == 1)
				{
					tsi = GuiHelper.AppendToolStripItem(ref cm, lvw, "Edit format", "edit", true);
					tsi.Click += HandleContextMenuClick;
					tsi = GuiHelper.AppendToolStripItem(ref cm, lvw, "Replace format", "replace", true);
					tsi.Click += HandleContextMenuClick;
				}
				if (lvw.SelectedItems.Count > 0)
				{
					tsi = GuiHelper.AppendToolStripItem(ref cm, lvw, "Delete format(s)", "delete", true);
					tsi.Click += HandleContextMenuClick;
				}

				cm.Show(lvw, e.Location);
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
							EditFormat ea = new EditFormat("New format", null, _objectTypes, false);
							ea.StartPosition = FormStartPosition.Manual;
							ea.Location = MousePosition;
							if (ea.ShowDialog(this) == DialogResult.OK)
							{
								HashSet<C4Format> formats = new HashSet<C4Format>();
								formats.Add(ea.Format);
								XmlDocument result = _s.CreateFormats(formats);
								Init(_s);
							}
							break;
						}
					case "edit":
						{
							ListViewItem lvi = lvw.SelectedItems[0];
							EditFormat ea = new EditFormat("Edit format", (C4Format)lvi.Tag, _objectTypes, true);
							ea.StartPosition = FormStartPosition.Manual;
							ea.Location = MousePosition;
							if (ea.ShowDialog(this) == DialogResult.OK)
							{
								HashSet<C4Format> formats = new HashSet<C4Format>();
								if (ea.SaveAs)
								{
									formats.Add(ea.Format);
									XmlDocument result = _s.CreateFormats(formats);
								}
								else
								{
									formats.Add(ea.Format);
									XmlDocument result = _s.UpdateFormats(formats);
								}
								Init(_s);
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
							HashSet<long> ids = new HashSet<long>();
							foreach (ListViewItem lvi in lvw.SelectedItems)
							{
								ids.Add(long.Parse(lvi.Text));
							}
							XmlDocument result = _s.DeleteFormats(ids);
							if (result.DocumentElement.SelectSingleNode("success[.='false']") != null) StandardMessage.ShowMessage("Failure deleting format.", StandardMessage.Severity.ErrorMessage, this, null);
							Init(_s);
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
