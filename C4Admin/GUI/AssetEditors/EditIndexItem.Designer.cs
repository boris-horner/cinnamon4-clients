
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
            panel7 = new System.Windows.Forms.Panel();
            pCancel = new System.Windows.Forms.Panel();
            cmdCancel = new System.Windows.Forms.Button();
            pSaveAs = new System.Windows.Forms.Panel();
            cmdSaveAs = new System.Windows.Forms.Button();
            pSave = new System.Windows.Forms.Panel();
            cmdOk = new System.Windows.Forms.Button();
            pData = new System.Windows.Forms.Panel();
            chkStoreField = new System.Windows.Forms.CheckBox();
            chkMultipleResults = new System.Windows.Forms.CheckBox();
            cboIndexTypeName = new System.Windows.Forms.ComboBox();
            lblIndexTypeName = new System.Windows.Forms.Label();
            vtxtSearchCondition = new C4GeneralGui.GuiElements.ValidatingTextBox();
            lblSearchCondition = new System.Windows.Forms.Label();
            vtxtSearchString = new C4GeneralGui.GuiElements.ValidatingTextBox();
            lblSearchString = new System.Windows.Forms.Label();
            vtxtFieldName = new C4GeneralGui.GuiElements.ValidatingTextBox();
            lblFieldName = new System.Windows.Forms.Label();
            vtxtName = new C4GeneralGui.GuiElements.ValidatingTextBox();
            lblName = new System.Windows.Forms.Label();
            txtId = new System.Windows.Forms.TextBox();
            lblId = new System.Windows.Forms.Label();
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
            panel7.Dock = System.Windows.Forms.DockStyle.Bottom;
            panel7.Location = new System.Drawing.Point(0, 440);
            panel7.Name = "panel7";
            panel7.Size = new System.Drawing.Size(481, 37);
            panel7.TabIndex = 6;
            // 
            // pCancel
            // 
            pCancel.Controls.Add(cmdCancel);
            pCancel.Dock = System.Windows.Forms.DockStyle.Left;
            pCancel.Location = new System.Drawing.Point(224, 0);
            pCancel.Name = "pCancel";
            pCancel.Padding = new System.Windows.Forms.Padding(6, 7, 10, 7);
            pCancel.Size = new System.Drawing.Size(112, 37);
            pCancel.TabIndex = 11;
            // 
            // cmdCancel
            // 
            cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            cmdCancel.Dock = System.Windows.Forms.DockStyle.Fill;
            cmdCancel.Location = new System.Drawing.Point(6, 7);
            cmdCancel.Name = "cmdCancel";
            cmdCancel.Size = new System.Drawing.Size(96, 23);
            cmdCancel.TabIndex = 9;
            cmdCancel.Text = "Cancel";
            cmdCancel.UseVisualStyleBackColor = true;
            cmdCancel.Click += cmdCancel_Click;
            // 
            // pSaveAs
            // 
            pSaveAs.Controls.Add(cmdSaveAs);
            pSaveAs.Dock = System.Windows.Forms.DockStyle.Left;
            pSaveAs.Location = new System.Drawing.Point(112, 0);
            pSaveAs.Name = "pSaveAs";
            pSaveAs.Padding = new System.Windows.Forms.Padding(6, 7, 10, 7);
            pSaveAs.Size = new System.Drawing.Size(112, 37);
            pSaveAs.TabIndex = 10;
            // 
            // cmdSaveAs
            // 
            cmdSaveAs.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            cmdSaveAs.Dock = System.Windows.Forms.DockStyle.Fill;
            cmdSaveAs.Location = new System.Drawing.Point(6, 7);
            cmdSaveAs.Name = "cmdSaveAs";
            cmdSaveAs.Size = new System.Drawing.Size(96, 23);
            cmdSaveAs.TabIndex = 6;
            cmdSaveAs.Text = "Save as";
            cmdSaveAs.UseVisualStyleBackColor = true;
            cmdSaveAs.Click += cmdSaveAs_Click;
            // 
            // pSave
            // 
            pSave.Controls.Add(cmdOk);
            pSave.Dock = System.Windows.Forms.DockStyle.Left;
            pSave.Location = new System.Drawing.Point(0, 0);
            pSave.Name = "pSave";
            pSave.Padding = new System.Windows.Forms.Padding(6, 7, 10, 7);
            pSave.Size = new System.Drawing.Size(112, 37);
            pSave.TabIndex = 9;
            // 
            // cmdOk
            // 
            cmdOk.Dock = System.Windows.Forms.DockStyle.Fill;
            cmdOk.Location = new System.Drawing.Point(6, 7);
            cmdOk.Name = "cmdOk";
            cmdOk.Size = new System.Drawing.Size(96, 23);
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
            pData.Dock = System.Windows.Forms.DockStyle.Fill;
            pData.Location = new System.Drawing.Point(0, 0);
            pData.Name = "pData";
            pData.Padding = new System.Windows.Forms.Padding(5, 6, 5, 6);
            pData.Size = new System.Drawing.Size(481, 440);
            pData.TabIndex = 0;
            // 
            // chkStoreField
            // 
            chkStoreField.AutoSize = true;
            chkStoreField.Dock = System.Windows.Forms.DockStyle.Top;
            chkStoreField.Location = new System.Drawing.Point(5, 392);
            chkStoreField.Name = "chkStoreField";
            chkStoreField.Padding = new System.Windows.Forms.Padding(0, 9, 0, 0);
            chkStoreField.Size = new System.Drawing.Size(471, 28);
            chkStoreField.TabIndex = 27;
            chkStoreField.Text = "Store field value";
            chkStoreField.UseVisualStyleBackColor = true;
            // 
            // chkMultipleResults
            // 
            chkMultipleResults.AutoSize = true;
            chkMultipleResults.Dock = System.Windows.Forms.DockStyle.Top;
            chkMultipleResults.Location = new System.Drawing.Point(5, 364);
            chkMultipleResults.Name = "chkMultipleResults";
            chkMultipleResults.Padding = new System.Windows.Forms.Padding(0, 9, 0, 0);
            chkMultipleResults.Size = new System.Drawing.Size(471, 28);
            chkMultipleResults.TabIndex = 26;
            chkMultipleResults.Text = "Multiple results";
            chkMultipleResults.UseVisualStyleBackColor = true;
            // 
            // cboIndexTypeName
            // 
            cboIndexTypeName.Dock = System.Windows.Forms.DockStyle.Top;
            cboIndexTypeName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            cboIndexTypeName.FormattingEnabled = true;
            cboIndexTypeName.Items.AddRange(new object[] { "BOOLEAN_INDEXER", "COMPLETE_STRING_INDEXER", "DATE_INDEXER", "DATE_TIME_INDEXER", "DEFAULT_INDEXER", "DESCENDING_STRING_INDEXER", "ELEMENT_NAME_INDEXER", "INTEGER_INDEXER" });
            cboIndexTypeName.Location = new System.Drawing.Point(5, 341);
            cboIndexTypeName.Name = "cboIndexTypeName";
            cboIndexTypeName.Size = new System.Drawing.Size(471, 23);
            cboIndexTypeName.Sorted = true;
            cboIndexTypeName.TabIndex = 25;
            // 
            // lblIndexTypeName
            // 
            lblIndexTypeName.AutoSize = true;
            lblIndexTypeName.Dock = System.Windows.Forms.DockStyle.Top;
            lblIndexTypeName.Location = new System.Drawing.Point(5, 317);
            lblIndexTypeName.Margin = new System.Windows.Forms.Padding(0);
            lblIndexTypeName.Name = "lblIndexTypeName";
            lblIndexTypeName.Padding = new System.Windows.Forms.Padding(0, 9, 0, 0);
            lblIndexTypeName.Size = new System.Drawing.Size(94, 24);
            lblIndexTypeName.TabIndex = 12;
            lblIndexTypeName.Text = "Index type name";
            // 
            // vtxtSearchCondition
            // 
            vtxtSearchCondition.BackColor = System.Drawing.Color.Yellow;
            vtxtSearchCondition.BothInvalidColor = System.Drawing.Color.Red;
            vtxtSearchCondition.Dock = System.Windows.Forms.DockStyle.Top;
            vtxtSearchCondition.ExternalInvalidColor = System.Drawing.Color.Orange;
            vtxtSearchCondition.ExternalValidation = null;
            vtxtSearchCondition.InternalInvalidColor = System.Drawing.Color.Yellow;
            vtxtSearchCondition.InvalidReadOnlyColor = System.Drawing.Color.LightYellow;
            vtxtSearchCondition.Location = new System.Drawing.Point(5, 250);
            vtxtSearchCondition.Multiline = true;
            vtxtSearchCondition.Name = "vtxtSearchCondition";
            vtxtSearchCondition.RegularExpression = "^.+$";
            vtxtSearchCondition.Size = new System.Drawing.Size(471, 67);
            vtxtSearchCondition.TabIndex = 9;
            vtxtSearchCondition.ValidColor = System.Drawing.Color.White;
            vtxtSearchCondition.ValidReadOnlyColor = System.Drawing.SystemColors.Control;
            vtxtSearchCondition.TextChanged += vtxtSearchCondition_TextChanged;
            // 
            // lblSearchCondition
            // 
            lblSearchCondition.AutoSize = true;
            lblSearchCondition.Dock = System.Windows.Forms.DockStyle.Top;
            lblSearchCondition.Location = new System.Drawing.Point(5, 226);
            lblSearchCondition.Margin = new System.Windows.Forms.Padding(0);
            lblSearchCondition.Name = "lblSearchCondition";
            lblSearchCondition.Padding = new System.Windows.Forms.Padding(0, 9, 0, 0);
            lblSearchCondition.Size = new System.Drawing.Size(92, 24);
            lblSearchCondition.TabIndex = 10;
            lblSearchCondition.Text = "XPath condition";
            // 
            // vtxtSearchString
            // 
            vtxtSearchString.BackColor = System.Drawing.Color.Yellow;
            vtxtSearchString.BothInvalidColor = System.Drawing.Color.Red;
            vtxtSearchString.Dock = System.Windows.Forms.DockStyle.Top;
            vtxtSearchString.ExternalInvalidColor = System.Drawing.Color.Orange;
            vtxtSearchString.ExternalValidation = null;
            vtxtSearchString.InternalInvalidColor = System.Drawing.Color.Yellow;
            vtxtSearchString.InvalidReadOnlyColor = System.Drawing.Color.LightYellow;
            vtxtSearchString.Location = new System.Drawing.Point(5, 162);
            vtxtSearchString.Multiline = true;
            vtxtSearchString.Name = "vtxtSearchString";
            vtxtSearchString.RegularExpression = "^.+$";
            vtxtSearchString.Size = new System.Drawing.Size(471, 64);
            vtxtSearchString.TabIndex = 7;
            vtxtSearchString.ValidColor = System.Drawing.Color.White;
            vtxtSearchString.ValidReadOnlyColor = System.Drawing.SystemColors.Control;
            vtxtSearchString.TextChanged += vtxtSearchString_TextChanged;
            // 
            // lblSearchString
            // 
            lblSearchString.AutoSize = true;
            lblSearchString.Dock = System.Windows.Forms.DockStyle.Top;
            lblSearchString.Location = new System.Drawing.Point(5, 138);
            lblSearchString.Margin = new System.Windows.Forms.Padding(0);
            lblSearchString.Name = "lblSearchString";
            lblSearchString.Padding = new System.Windows.Forms.Padding(0, 9, 0, 0);
            lblSearchString.Size = new System.Drawing.Size(108, 24);
            lblSearchString.TabIndex = 8;
            lblSearchString.Text = "XPath search string";
            // 
            // vtxtFieldName
            // 
            vtxtFieldName.BackColor = System.Drawing.Color.Yellow;
            vtxtFieldName.BothInvalidColor = System.Drawing.Color.Red;
            vtxtFieldName.Dock = System.Windows.Forms.DockStyle.Top;
            vtxtFieldName.ExternalInvalidColor = System.Drawing.Color.Orange;
            vtxtFieldName.ExternalValidation = null;
            vtxtFieldName.InternalInvalidColor = System.Drawing.Color.Yellow;
            vtxtFieldName.InvalidReadOnlyColor = System.Drawing.Color.LightYellow;
            vtxtFieldName.Location = new System.Drawing.Point(5, 115);
            vtxtFieldName.Name = "vtxtFieldName";
            vtxtFieldName.RegularExpression = "^[a-zA-Z0-9_.-]+$";
            vtxtFieldName.Size = new System.Drawing.Size(471, 23);
            vtxtFieldName.TabIndex = 5;
            vtxtFieldName.ValidColor = System.Drawing.Color.White;
            vtxtFieldName.ValidReadOnlyColor = System.Drawing.SystemColors.Control;
            vtxtFieldName.TextChanged += vtxtFieldName_TextChanged;
            // 
            // lblFieldName
            // 
            lblFieldName.AutoSize = true;
            lblFieldName.Dock = System.Windows.Forms.DockStyle.Top;
            lblFieldName.Location = new System.Drawing.Point(5, 91);
            lblFieldName.Margin = new System.Windows.Forms.Padding(0);
            lblFieldName.Name = "lblFieldName";
            lblFieldName.Padding = new System.Windows.Forms.Padding(0, 9, 0, 0);
            lblFieldName.Size = new System.Drawing.Size(65, 24);
            lblFieldName.TabIndex = 6;
            lblFieldName.Text = "Field name";
            // 
            // vtxtName
            // 
            vtxtName.BackColor = System.Drawing.Color.Yellow;
            vtxtName.BothInvalidColor = System.Drawing.Color.Red;
            vtxtName.Dock = System.Windows.Forms.DockStyle.Top;
            vtxtName.ExternalInvalidColor = System.Drawing.Color.Orange;
            vtxtName.ExternalValidation = null;
            vtxtName.InternalInvalidColor = System.Drawing.Color.Yellow;
            vtxtName.InvalidReadOnlyColor = System.Drawing.Color.LightYellow;
            vtxtName.Location = new System.Drawing.Point(5, 68);
            vtxtName.Name = "vtxtName";
            vtxtName.RegularExpression = "^[a-zA-Z0-9_.-]+$";
            vtxtName.Size = new System.Drawing.Size(471, 23);
            vtxtName.TabIndex = 0;
            vtxtName.ValidColor = System.Drawing.Color.White;
            vtxtName.ValidReadOnlyColor = System.Drawing.SystemColors.Control;
            vtxtName.TextChanged += vtxtName_TextChanged;
            // 
            // lblName
            // 
            lblName.AutoSize = true;
            lblName.Dock = System.Windows.Forms.DockStyle.Top;
            lblName.Location = new System.Drawing.Point(5, 44);
            lblName.Margin = new System.Windows.Forms.Padding(0);
            lblName.Name = "lblName";
            lblName.Padding = new System.Windows.Forms.Padding(0, 9, 0, 0);
            lblName.Size = new System.Drawing.Size(39, 24);
            lblName.TabIndex = 4;
            lblName.Text = "Name";
            // 
            // txtId
            // 
            txtId.Dock = System.Windows.Forms.DockStyle.Top;
            txtId.Location = new System.Drawing.Point(5, 21);
            txtId.Margin = new System.Windows.Forms.Padding(0);
            txtId.Name = "txtId";
            txtId.ReadOnly = true;
            txtId.Size = new System.Drawing.Size(471, 23);
            txtId.TabIndex = 1;
            // 
            // lblId
            // 
            lblId.AutoSize = true;
            lblId.Dock = System.Windows.Forms.DockStyle.Top;
            lblId.Location = new System.Drawing.Point(5, 6);
            lblId.Margin = new System.Windows.Forms.Padding(0);
            lblId.Name = "lblId";
            lblId.Size = new System.Drawing.Size(17, 15);
            lblId.TabIndex = 0;
            lblId.Text = "Id";
            // 
            // EditIndexItem
            // 
            AcceptButton = cmdOk;
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            CancelButton = cmdCancel;
            ClientSize = new System.Drawing.Size(481, 477);
            Controls.Add(pData);
            Controls.Add(panel7);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
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