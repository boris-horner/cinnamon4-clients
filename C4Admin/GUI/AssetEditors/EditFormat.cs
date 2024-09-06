using C4ServerConnector.Assets;
using C4Admin.Global;

namespace C4Admin.GUI.AssetEditors
{
    public partial class EditFormat : Form
	{
		public C4Format Format { get; private set; }
		public bool SaveAs { get; private set; }
		private string _origName;
		public EditFormat(string title, C4Format format, Dictionary<long, C4ObjectType> objectTypes, bool showSaveAs)
		{
			InitializeComponent();
			Text = title;
			Format = format;

			cboDefaultObjectTypeId.Items.Clear();
			foreach (C4ObjectType ot in objectTypes.Values) cboDefaultObjectTypeId.Items.Add(ot);
			if (format == null)
			{
				txtId.Text = Definitions.NEW;
				vtxtName.Text = "";
				vtxtExtension.Text = "";
				vtxtContentType.Text = "";
				cboIndexMode.Text = "NONE";
				cboDefaultObjectTypeId.SelectedIndex = 0;
				_origName = null;
			}
			else
			{
				txtId.Text = Format.Id == null ? Definitions.NEW : Format.Id.ToString();
				vtxtName.Text = Format.Name;
				vtxtExtension.Text = Format.Extension;
				vtxtContentType.Text = Format.ContentType;
				cboIndexMode.Text = Format.IndexMode;
				if (Format.DefaultObjectTypeId == null) cboDefaultObjectTypeId.SelectedIndex = 0;
				else cboDefaultObjectTypeId.SelectedItem = objectTypes[(long)Format.DefaultObjectTypeId];
				_origName = Format.Name;
			}
			GuiActions();
			pSaveAs.Visible = showSaveAs;
		}

		private void cmdOk_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.OK;
			SaveAs = false;
			long? id = null;
			if (txtId.Text != Definitions.NEW) id = long.Parse(txtId.Text);
			Format = new C4Format(vtxtName.Text, vtxtExtension.Text, vtxtContentType.Text, cboDefaultObjectTypeId.SelectedItem.ToString() == "" ? null : ((C4ObjectType)cboDefaultObjectTypeId.SelectedItem).Id, cboIndexMode.Text, id);
			Close();
		}

		private void cmdSaveAs_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.OK;
			SaveAs = true;
			long? id = null;
			if (txtId.Text != Definitions.NEW) id = long.Parse(txtId.Text);
			Format = new C4Format(vtxtName.Text, vtxtExtension.Text, vtxtContentType.Text, cboDefaultObjectTypeId.SelectedItem.ToString() == "" ? null : ((C4ObjectType)cboDefaultObjectTypeId.SelectedItem).Id, cboIndexMode.Text, id);
			Close();
		}
		private void cmdCancel_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.Cancel;
			Close();
		}

		private void GuiActions()
		{
			bool valid = vtxtName.IsValid && vtxtExtension.IsValid && vtxtContentType.IsValid;
			cmdOk.Enabled = valid;
			cmdSaveAs.Enabled = (vtxtName.Text != _origName && valid);
		}
		private void vtxtName_TextChanged(object sender, EventArgs e)
		{
			GuiActions();
		}
		private void vtxtExtension_TextChanged(object sender, EventArgs e)
		{
			GuiActions();
		}
		private void vtxtContentType_TextChanged(object sender, EventArgs e)
		{
			GuiActions();
		}

		private void EditFormat_Load(object sender, EventArgs e)
		{
			ActiveControl = vtxtName;
		}

	}
}
