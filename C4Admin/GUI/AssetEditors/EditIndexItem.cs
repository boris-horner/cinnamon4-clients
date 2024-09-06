using C4ServerConnector.Assets;
using C4Admin.Global;

namespace C4Admin.GUI.AssetEditors
{
    public partial class EditIndexItem : Form
    {
        public C4IndexItem IndexItem { get; private set; }
        public bool SaveAs { get; private set; }
        private string _origName;
        public EditIndexItem(string title, C4IndexItem indexItem, bool showSaveAs)
        {
            InitializeComponent();
            Text = title;
            IndexItem = indexItem;
            if (indexItem == null)
            {
                txtId.Text = Definitions.NEW;
                vtxtName.Text = "";
                vtxtFieldName.Text = "";
                vtxtSearchString.Text = "";
                vtxtSearchCondition.Text = "true()";
                cboIndexTypeName.Text = "DEFAULT_INDEXER";
                chkMultipleResults.Checked = false;
                chkStoreField.Checked = false;
                _origName = null;
            }
            else
            {
                txtId.Text = IndexItem.Id == null ? Definitions.NEW : IndexItem.Id.ToString();
                vtxtName.Text = IndexItem.Name;
                vtxtFieldName.Text = IndexItem.FieldName;
                vtxtSearchString.Text = IndexItem.SearchString;
                vtxtSearchCondition.Text = IndexItem.SearchCondition;
                cboIndexTypeName.Text = IndexItem.IndexTypeName;
                chkMultipleResults.Checked = IndexItem.MultipleResults;
                chkStoreField.Checked = IndexItem.StoreField;
                _origName = IndexItem.Name;
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
            IndexItem = new C4IndexItem(vtxtName.Text, vtxtFieldName.Text, vtxtSearchString.Text, vtxtSearchCondition.Text, cboIndexTypeName.Text, chkMultipleResults.Checked, chkStoreField.Checked, id);
            Close();
        }

        private void cmdSaveAs_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            SaveAs = true;
            long? id = null;
            if (txtId.Text != Definitions.NEW) id = long.Parse(txtId.Text);
            IndexItem = new C4IndexItem(vtxtName.Text, vtxtFieldName.Text, vtxtSearchString.Text, vtxtSearchCondition.Text, cboIndexTypeName.Text, chkMultipleResults.Checked, chkStoreField.Checked, id);
            Close();
        }
        private void cmdCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void GuiActions()
        {
            bool valid = vtxtName.IsValid && vtxtFieldName.IsValid && cboIndexTypeName.SelectedItem != null && vtxtSearchCondition.IsValid && vtxtSearchString.IsValid;
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

        private void vtxtFieldName_TextChanged(object sender, EventArgs e)
        {
            GuiActions();
        }

        private void vtxtSearchString_TextChanged(object sender, EventArgs e)
        {
            GuiActions();
        }

        private void vtxtSearchCondition_TextChanged(object sender, EventArgs e)
        {
            GuiActions();
        }

        private void vtxtIndexTypeName_TextChanged(object sender, EventArgs e)
        {
            GuiActions();
        }
    }
}
