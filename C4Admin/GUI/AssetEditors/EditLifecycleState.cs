using C4ServerConnector.Assets;
using C4Admin.Global;
using C4ServerConnector;
using System.Xml;

namespace C4Admin.GUI.AssetEditors
{
    public partial class EditLifecycleState : Form
	{
		public C4LifecycleState LifecycleState { get; private set; }
		public bool SaveAs { get; private set; }
		private string _origName;
		private Dictionary<long, C4LifecycleState> _allStates;
		private Dictionary<long, C4Lifecycle> _allLifecycles;

		public EditLifecycleState(string title, C4Session c4s, C4LifecycleState lifecycleState, C4Lifecycle lifecycle, bool showSaveAs)
		{
			InitializeComponent();
			_allStates = c4s.ListLifecycleStates();
			_allLifecycles = c4s.ListLifecycles();
			Text = title;
			LifecycleState = lifecycleState;

			foreach (C4Lifecycle lc in _allLifecycles.Values) cboLifecycle.Items.Add(lc);
			if(lifecycle!=null) cboLifecycle.Text = lifecycle.Name;
			UpdateStatesForCopy();

			if (LifecycleState == null)
			{
				txtId.Text = Definitions.NEW;
				vtxtName.Text = "";
				vtxtStateClass.Text = "com.dewarim.cinnamon.lifecycle.NopState";
				xtbConfig.Text = "<config />";
				cboCopyState.Text = "";
				_origName = null;
			}
			else
			{
				txtId.Text = LifecycleState.Id == null ? Definitions.NEW : LifecycleState.Id.ToString();
				vtxtName.Text = LifecycleState.Name;
				vtxtStateClass.Text = lifecycleState.StateClass;
				xtbConfig.Text = LifecycleState.Config.OuterXml;
				cboCopyState.Text = lifecycleState.LifecycleStateForCopyId == null ? "" : _allStates[(long)lifecycleState.LifecycleStateForCopyId].Name;

				_origName = LifecycleState.Name;
			}


			GuiActions();
			pSaveAs.Visible = showSaveAs;
		}

		private void UpdateStatesForCopy()
		{
			if (cboLifecycle.SelectedItem != null)
			{
				C4Lifecycle selLc = cboLifecycle.SelectedItem as C4Lifecycle;
				cboCopyState.Items.Clear();
				cboCopyState.Items.Add("");
				foreach (C4LifecycleState lcState in _allStates.Values)
				{
					if (lcState.LifecycleId == selLc.Id)
					{
						cboCopyState.Items.Add(lcState);
					}
				}
			}

		}

		private void cmdOk_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.OK;
			SaveAs = false;
			long? id = null;
			if (txtId.Text != Definitions.NEW) id = long.Parse(txtId.Text);
			XmlDocument config = new XmlDocument();
			config.LoadXml(xtbConfig.Text);
			C4Lifecycle selLc = cboLifecycle.SelectedItem as C4Lifecycle;
			long? selStateForCopiesId = cboCopyState.SelectedItem.ToString() == "" ? null : (cboCopyState.SelectedItem as C4LifecycleState).Id;
			LifecycleState = new C4LifecycleState(vtxtName.Text, config, vtxtStateClass.Text, (long)selLc.Id, selStateForCopiesId, id);
			Close();
		}

		private void cmdSaveAs_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.OK;
			SaveAs = true;
			long? id = null;
			if (txtId.Text != Definitions.NEW) id = long.Parse(txtId.Text);
			XmlDocument config = new XmlDocument();
			config.LoadXml(xtbConfig.Text);
			C4Lifecycle selLc = cboLifecycle.SelectedItem as C4Lifecycle;
			long? selStateForCopiesId = cboCopyState.SelectedItem == null ? null : (cboCopyState.SelectedItem as C4LifecycleState).Id;
			LifecycleState = new C4LifecycleState(vtxtName.Text, config, vtxtStateClass.Text, (long)selLc.Id, selStateForCopiesId, id);
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

		private void cboLifecycle_SelectedIndexChanged(object sender, EventArgs e)
		{
			UpdateStatesForCopy();
		}
	}
}
