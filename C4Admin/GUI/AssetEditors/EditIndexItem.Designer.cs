
namespace C4Admin.GUI.AssetEditors
{
    partial class EditIndexItem
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            panel7 = new Panel();
            pCancel = new Panel();
            cmdCancel = new Button();
            pSaveAs = new Panel();
            cmdSaveAs = new Button();
            pSave = new Panel();
            cmdOk = new Button();
            pData = new Panel();
            chkStoreField = new CheckBox();
            chkMultipleResults = new CheckBox();
            cboIndexTypeName = new ComboBox();
            lblIndexTypeName = new Label();
            vtxtSearchCondition = new C4GeneralGui.GuiElements.ValidatingTextBox();
            lblSearchCondition = new Label();
            vtxtSearchString = new C4GeneralGui.GuiElements.ValidatingTextBox();
            lblSearchString = new Label();
            vtxtFieldName = new C4GeneralGui.GuiElements.ValidatingTextBox();
            lblFieldName = new Label();
            vtxtName = new C4GeneralGui.GuiElements.ValidatingTextBox();
            lblName = new Label();
            txtId = new TextBox();
            lblId = new Label();
            panel7.SuspendLayout();
            pCancel.SuspendLayout();
            pSaveAs.SuspendLayout();
            pSave.SuspendLayout();
            pData.SuspendLayout();
            SuspendLayout();
            // 
            // panel7
            // 
            panel7.Controls.Add(pCancel);
            panel7.Controls.Add(pSaveAs);
            panel7.Controls.Add(pSave);
            panel7.Dock = DockStyle.Bottom;
            panel7.Location = new Point(0, 440);
            panel7.Name = "panel7";
            panel7.Size = new Size(481, 37);
            panel7.TabIndex = 6;
            // 
            // pCancel
            // 
            pCancel.Controls.Add(cmdCancel);
            pCancel.Dock = DockStyle.Left;
            pCancel.Location = new Point(224, 0);
            pCancel.Name = "pCancel";
            pCancel.Padding = new Padding(6, 7, 10, 7);
            pCancel.Size = new Size(112, 37);
            pCancel.TabIndex = 11;
            // 
            // cmdCancel
            // 
            cmdCancel.DialogResult = DialogResult.Cancel;
            cmdCancel.Dock = DockStyle.Fill;
            cmdCancel.Location = new Point(6, 7);
            cmdCancel.Name = "cmdCancel";
            cmdCancel.Size = new Size(96, 23);
            cmdCancel.TabIndex = 9;
            cmdCancel.Text = "Cancel";
            cmdCancel.UseVisualStyleBackColor = true;
            cmdCancel.Click += cmdCancel_Click;
            // 
            // pSaveAs
            // 
            pSaveAs.Controls.Add(cmdSaveAs);
            pSaveAs.Dock = DockStyle.Left;
            pSaveAs.Location = new Point(112, 0);
            pSaveAs.Name = "pSaveAs";
            pSaveAs.Padding = new Padding(6, 7, 10, 7);
            pSaveAs.Size = new Size(112, 37);
            pSaveAs.TabIndex = 10;
            // 
            // cmdSaveAs
            // 
            cmdSaveAs.DialogResult = DialogResult.Cancel;
            cmdSaveAs.Dock = DockStyle.Fill;
            cmdSaveAs.Location = new Point(6, 7);
            cmdSaveAs.Name = "cmdSaveAs";
            cmdSaveAs.Size = new Size(96, 23);
            cmdSaveAs.TabIndex = 6;
            cmdSaveAs.Text = "Save as";
            cmdSaveAs.UseVisualStyleBackColor = true;
            cmdSaveAs.Click += cmdSaveAs_Click;
            // 
            // pSave
            // 
            pSave.Controls.Add(cmdOk);
            pSave.Dock = DockStyle.Left;
            pSave.Location = new Point(0, 0);
            pSave.Name = "pSave";
            pSave.Padding = new Padding(6, 7, 10, 7);
            pSave.Size = new Size(112, 37);
            pSave.TabIndex = 9;
            // 
            // cmdOk
            // 
            cmdOk.Dock = DockStyle.Fill;
            cmdOk.Location = new Point(6, 7);
            cmdOk.Name = "cmdOk";
            cmdOk.Size = new Size(96, 23);
            cmdOk.TabIndex = 2;
            cmdOk.Text = "Save";
            cmdOk.UseVisualStyleBackColor = true;
            cmdOk.Click += cmdOk_Click;
            // 
            // pData
            // 
            pData.Controls.Add(chkStoreField);
            pData.Controls.Add(chkMultipleResults);
            pData.Controls.Add(cboIndexTypeName);
            pData.Controls.Add(lblIndexTypeName);
            pData.Controls.Add(vtxtSearchCondition);
            pData.Controls.Add(lblSearchCondition);
            pData.Controls.Add(vtxtSearchString);
            pData.Controls.Add(lblSearchString);
            pData.Controls.Add(vtxtFieldName);
            pData.Controls.Add(lblFieldName);
            pData.Controls.Add(vtxtName);
            pData.Controls.Add(lblName);
            pData.Controls.Add(txtId);
            pData.Controls.Add(lblId);
            pData.Dock = DockStyle.Fill;
            pData.Location = new Point(0, 0);
            pData.Name = "pData";
            pData.Padding = new Padding(5, 6, 5, 6);
            pData.Size = new Size(481, 440);
            pData.TabIndex = 0;
            // 
            // chkStoreField
            // 
            chkStoreField.AutoSize = true;
            chkStoreField.Dock = DockStyle.Top;
            chkStoreField.Location = new Point(5, 392);
            chkStoreField.Name = "chkStoreField";
            chkStoreField.Padding = new Padding(0, 9, 0, 0);
            chkStoreField.Size = new Size(471, 28);
            chkStoreField.TabIndex = 27;
            chkStoreField.Text = "Store field value";
            chkStoreField.UseVisualStyleBackColor = true;
            // 
            // chkMultipleResults
            // 
            chkMultipleResults.AutoSize = true;
            chkMultipleResults.Dock = DockStyle.Top;
            chkMultipleResults.Location = new Point(5, 364);
            chkMultipleResults.Name = "chkMultipleResults";
            chkMultipleResults.Padding = new Padding(0, 9, 0, 0);
            chkMultipleResults.Size = new Size(471, 28);
            chkMultipleResults.TabIndex = 26;
            chkMultipleResults.Text = "Multiple results";
            chkMultipleResults.UseVisualStyleBackColor = true;
            // 
            // cboIndexTypeName
            // 
            cboIndexTypeName.Dock = DockStyle.Top;
            cboIndexTypeName.DropDownStyle = ComboBoxStyle.DropDownList;
            cboIndexTypeName.FormattingEnabled = true;
            cboIndexTypeName.Items.AddRange(new object[] { "BOOLEAN_INDEXER", "COMPLETE_STRING_INDEXER", "DATE_INDEXER", "DATE_TIME_INDEXER", "DEFAULT_INDEXER", "DESCENDING_STRING_INDEXER", "ELEMENT_NAME_INDEXER", "INTEGER_INDEXER" });
            cboIndexTypeName.Location = new Point(5, 341);
            cboIndexTypeName.Name = "cboIndexTypeName";
            cboIndexTypeName.Size = new Size(471, 23);
            cboIndexTypeName.Sorted = true;
            cboIndexTypeName.TabIndex = 25;
            // 
            // lblIndexTypeName
            // 
            lblIndexTypeName.AutoSize = true;
            lblIndexTypeName.Dock = DockStyle.Top;
            lblIndexTypeName.Location = new Point(5, 317);
            lblIndexTypeName.Margin = new Padding(0);
            lblIndexTypeName.Name = "lblIndexTypeName";
            lblIndexTypeName.Padding = new Padding(0, 9, 0, 0);
            lblIndexTypeName.Size = new Size(94, 24);
            lblIndexTypeName.TabIndex = 12;
            lblIndexTypeName.Text = "Index type name";
            // 
            // vtxtSearchCondition
            // 
            vtxtSearchCondition.BackColor = Color.Yellow;
            vtxtSearchCondition.BothInvalidColor = Color.Red;
            vtxtSearchCondition.Dock = DockStyle.Top;
            vtxtSearchCondition.ExternalInvalidColor = Color.Orange;
            vtxtSearchCondition.ExternalValidation = null;
            vtxtSearchCondition.InternalInvalidColor = Color.Yellow;
            vtxtSearchCondition.InvalidReadOnlyColor = Color.LightYellow;
            vtxtSearchCondition.Location = new Point(5, 250);
            vtxtSearchCondition.Multiline = true;
            vtxtSearchCondition.Name = "vtxtSearchCondition";
            vtxtSearchCondition.RegularExpression = "^.+$";
            vtxtSearchCondition.Size = new Size(471, 67);
            vtxtSearchCondition.TabIndex = 9;
            vtxtSearchCondition.ValidColor = Color.White;
            vtxtSearchCondition.ValidReadOnlyColor = SystemColors.Control;
            vtxtSearchCondition.TextChanged += vtxtSearchCondition_TextChanged;
            // 
            // lblSearchCondition
            // 
            lblSearchCondition.AutoSize = true;
            lblSearchCondition.Dock = DockStyle.Top;
            lblSearchCondition.Location = new Point(5, 226);
            lblSearchCondition.Margin = new Padding(0);
            lblSearchCondition.Name = "lblSearchCondition";
            lblSearchCondition.Padding = new Padding(0, 9, 0, 0);
            lblSearchCondition.Size = new Size(92, 24);
            lblSearchCondition.TabIndex = 10;
            lblSearchCondition.Text = "XPath condition";
            // 
            // vtxtSearchString
            // 
            vtxtSearchString.BackColor = Color.Yellow;
            vtxtSearchString.BothInvalidColor = Color.Red;
            vtxtSearchString.Dock = DockStyle.Top;
            vtxtSearchString.ExternalInvalidColor = Color.Orange;
            vtxtSearchString.ExternalValidation = null;
            vtxtSearchString.InternalInvalidColor = Color.Yellow;
            vtxtSearchString.InvalidReadOnlyColor = Color.LightYellow;
            vtxtSearchString.Location = new Point(5, 162);
            vtxtSearchString.Multiline = true;
            vtxtSearchString.Name = "vtxtSearchString";
            vtxtSearchString.RegularExpression = "^.+$";
            vtxtSearchString.Size = new Size(471, 64);
            vtxtSearchString.TabIndex = 7;
            vtxtSearchString.ValidColor = Color.White;
            vtxtSearchString.ValidReadOnlyColor = SystemColors.Control;
            vtxtSearchString.TextChanged += vtxtSearchString_TextChanged;
            // 
            // lblSearchString
            // 
            lblSearchString.AutoSize = true;
            lblSearchString.Dock = DockStyle.Top;
            lblSearchString.Location = new Point(5, 138);
            lblSearchString.Margin = new Padding(0);
            lblSearchString.Name = "lblSearchString";
            lblSearchString.Padding = new Padding(0, 9, 0, 0);
            lblSearchString.Size = new Size(108, 24);
            lblSearchString.TabIndex = 8;
            lblSearchString.Text = "XPath search string";
            // 
            // vtxtFieldName
            // 
            vtxtFieldName.BackColor = Color.Yellow;
            vtxtFieldName.BothInvalidColor = Color.Red;
            vtxtFieldName.Dock = DockStyle.Top;
            vtxtFieldName.ExternalInvalidColor = Color.Orange;
            vtxtFieldName.ExternalValidation = null;
            vtxtFieldName.InternalInvalidColor = Color.Yellow;
            vtxtFieldName.InvalidReadOnlyColor = Color.LightYellow;
            vtxtFieldName.Location = new Point(5, 115);
            vtxtFieldName.Name = "vtxtFieldName";
            vtxtFieldName.RegularExpression = "^[a-zA-Z0-9_.-]+$";
            vtxtFieldName.Size = new Size(471, 23);
            vtxtFieldName.TabIndex = 5;
            vtxtFieldName.ValidColor = Color.White;
            vtxtFieldName.ValidReadOnlyColor = SystemColors.Control;
            vtxtFieldName.TextChanged += vtxtFieldName_TextChanged;
            // 
            // lblFieldName
            // 
            lblFieldName.AutoSize = true;
            lblFieldName.Dock = DockStyle.Top;
            lblFieldName.Location = new Point(5, 91);
            lblFieldName.Margin = new Padding(0);
            lblFieldName.Name = "lblFieldName";
            lblFieldName.Padding = new Padding(0, 9, 0, 0);
            lblFieldName.Size = new Size(65, 24);
            lblFieldName.TabIndex = 6;
            lblFieldName.Text = "Field name";
            // 
            // vtxtName
            // 
            vtxtName.BackColor = Color.Yellow;
            vtxtName.BothInvalidColor = Color.Red;
            vtxtName.Dock = DockStyle.Top;
            vtxtName.ExternalInvalidColor = Color.Orange;
            vtxtName.ExternalValidation = null;
            vtxtName.InternalInvalidColor = Color.Yellow;
            vtxtName.InvalidReadOnlyColor = Color.LightYellow;
            vtxtName.Location = new Point(5, 68);
            vtxtName.Name = "vtxtName";
            vtxtName.RegularExpression = "^[a-zA-Z0-9_.-]+$";
            vtxtName.Size = new Size(471, 23);
            vtxtName.TabIndex = 0;
            vtxtName.ValidColor = Color.White;
            vtxtName.ValidReadOnlyColor = SystemColors.Control;
            vtxtName.TextChanged += vtxtName_TextChanged;
            // 
            // lblName
            // 
            lblName.AutoSize = true;
            lblName.Dock = DockStyle.Top;
            lblName.Location = new Point(5, 44);
            lblName.Margin = new Padding(0);
            lblName.Name = "lblName";
            lblName.Padding = new Padding(0, 9, 0, 0);
            lblName.Size = new Size(39, 24);
            lblName.TabIndex = 4;
            lblName.Text = "Name";
            // 
            // txtId
            // 
            txtId.Dock = DockStyle.Top;
            txtId.Location = new Point(5, 21);
            txtId.Margin = new Padding(0);
            txtId.Name = "txtId";
            txtId.ReadOnly = true;
            txtId.Size = new Size(471, 23);
            txtId.TabIndex = 1;
            // 
            // lblId
            // 
            lblId.AutoSize = true;
            lblId.Dock = DockStyle.Top;
            lblId.Location = new Point(5, 6);
            lblId.Margin = new Padding(0);
            lblId.Name = "lblId";
            lblId.Size = new Size(17, 15);
            lblId.TabIndex = 0;
            lblId.Text = "Id";
            // 
            // EditIndexItem
            // 
            AcceptButton = cmdOk;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = cmdCancel;
            ClientSize = new Size(481, 477);
            Controls.Add(pData);
            Controls.Add(panel7);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Name = "EditIndexItem";
            Text = "Edit folder type";
            Load += EditFolderType_Load;
            panel7.ResumeLayout(false);
            pCancel.ResumeLayout(false);
            pSaveAs.ResumeLayout(false);
            pSave.ResumeLayout(false);
            pData.ResumeLayout(false);
            pData.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Panel pData;
        private System.Windows.Forms.TextBox txtId;
        private System.Windows.Forms.Label lblId;
        private System.Windows.Forms.Label lblName;
        private C4GeneralGui.GuiElements.ValidatingTextBox vtxtName;
        private System.Windows.Forms.Panel pCancel;
        private System.Windows.Forms.Button cmdCancel;
        private System.Windows.Forms.Panel pSaveAs;
        private System.Windows.Forms.Button cmdSaveAs;
        private System.Windows.Forms.Panel pSave;
        private System.Windows.Forms.Button cmdOk;
        private System.Windows.Forms.Label lblIndexTypeName;
        private C4GeneralGui.GuiElements.ValidatingTextBox vtxtSearchCondition;
        private System.Windows.Forms.Label lblSearchCondition;
        private C4GeneralGui.GuiElements.ValidatingTextBox vtxtSearchString;
        private System.Windows.Forms.Label lblSearchString;
        private C4GeneralGui.GuiElements.ValidatingTextBox vtxtFieldName;
        private System.Windows.Forms.Label lblFieldName;
        private System.Windows.Forms.CheckBox chkStoreField;
        private System.Windows.Forms.CheckBox chkMultipleResults;
        private System.Windows.Forms.ComboBox cboIndexTypeName;
    }
}