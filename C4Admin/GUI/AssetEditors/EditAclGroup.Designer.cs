
namespace C4Admin.GUI.AssetEditors
{
    partial class EditAclGroup
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
            clbPermissions = new CheckedListBox();
            lblPermissions = new Label();
            cboGroup = new ComboBox();
            lblGroup = new Label();
            cboAcl = new ComboBox();
            lblAcl = new Label();
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
            panel7.Location = new Point(0, 845);
            panel7.Name = "panel7";
            panel7.Size = new Size(570, 37);
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
            pData.Controls.Add(clbPermissions);
            pData.Controls.Add(lblPermissions);
            pData.Controls.Add(cboGroup);
            pData.Controls.Add(lblGroup);
            pData.Controls.Add(cboAcl);
            pData.Controls.Add(lblAcl);
            pData.Controls.Add(txtId);
            pData.Controls.Add(lblId);
            pData.Dock = DockStyle.Fill;
            pData.Location = new Point(0, 0);
            pData.Name = "pData";
            pData.Padding = new Padding(5, 6, 5, 6);
            pData.Size = new Size(570, 845);
            pData.TabIndex = 0;
            // 
            // clbPermissions
            // 
            clbPermissions.CheckOnClick = true;
            clbPermissions.Dock = DockStyle.Fill;
            clbPermissions.FormattingEnabled = true;
            clbPermissions.Location = new Point(5, 162);
            clbPermissions.Name = "clbPermissions";
            clbPermissions.Size = new Size(560, 677);
            clbPermissions.TabIndex = 14;
            clbPermissions.ItemCheck += clbPermissions_ItemCheck;
            // 
            // lblPermissions
            // 
            lblPermissions.AutoSize = true;
            lblPermissions.Dock = DockStyle.Top;
            lblPermissions.Location = new Point(5, 138);
            lblPermissions.Margin = new Padding(0);
            lblPermissions.Name = "lblPermissions";
            lblPermissions.Padding = new Padding(0, 9, 0, 0);
            lblPermissions.Size = new Size(70, 24);
            lblPermissions.TabIndex = 13;
            lblPermissions.Text = "Permissions";
            // 
            // cboGroup
            // 
            cboGroup.Dock = DockStyle.Top;
            cboGroup.DropDownStyle = ComboBoxStyle.DropDownList;
            cboGroup.FormattingEnabled = true;
            cboGroup.Location = new Point(5, 115);
            cboGroup.Name = "cboGroup";
            cboGroup.Size = new Size(560, 23);
            cboGroup.TabIndex = 11;
            cboGroup.SelectedIndexChanged += cboGroup_SelectedIndexChanged;
            // 
            // lblGroup
            // 
            lblGroup.AutoSize = true;
            lblGroup.Dock = DockStyle.Top;
            lblGroup.Location = new Point(5, 91);
            lblGroup.Margin = new Padding(0);
            lblGroup.Name = "lblGroup";
            lblGroup.Padding = new Padding(0, 9, 0, 0);
            lblGroup.Size = new Size(40, 24);
            lblGroup.TabIndex = 10;
            lblGroup.Text = "Group";
            // 
            // cboAcl
            // 
            cboAcl.Dock = DockStyle.Top;
            cboAcl.DropDownStyle = ComboBoxStyle.DropDownList;
            cboAcl.FormattingEnabled = true;
            cboAcl.Location = new Point(5, 68);
            cboAcl.Name = "cboAcl";
            cboAcl.Size = new Size(560, 23);
            cboAcl.TabIndex = 5;
            cboAcl.SelectedIndexChanged += cboAcl_SelectedIndexChanged;
            // 
            // lblAcl
            // 
            lblAcl.AutoSize = true;
            lblAcl.Dock = DockStyle.Top;
            lblAcl.Location = new Point(5, 44);
            lblAcl.Margin = new Padding(0);
            lblAcl.Name = "lblAcl";
            lblAcl.Padding = new Padding(0, 9, 0, 0);
            lblAcl.Size = new Size(29, 24);
            lblAcl.TabIndex = 4;
            lblAcl.Text = "ACL";
            // 
            // txtId
            // 
            txtId.Dock = DockStyle.Top;
            txtId.Location = new Point(5, 21);
            txtId.Margin = new Padding(0);
            txtId.Name = "txtId";
            txtId.ReadOnly = true;
            txtId.Size = new Size(560, 23);
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
            // EditAclGroup
            // 
            AcceptButton = cmdOk;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = cmdCancel;
            ClientSize = new Size(570, 882);
            Controls.Add(pData);
            Controls.Add(panel7);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Name = "EditAclGroup";
            Text = "Edit ACL group";
            Load += EditAclGroup_Load;
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
        private System.Windows.Forms.Label lblAcl;
        private System.Windows.Forms.Panel pCancel;
        private System.Windows.Forms.Button cmdCancel;
        private System.Windows.Forms.Panel pSaveAs;
        private System.Windows.Forms.Button cmdSaveAs;
        private System.Windows.Forms.Panel pSave;
        private System.Windows.Forms.Button cmdOk;
        private System.Windows.Forms.CheckedListBox clbPermissions;
        private System.Windows.Forms.Label lblPermissions;
        private System.Windows.Forms.ComboBox cboGroup;
        private System.Windows.Forms.Label lblGroup;
        private System.Windows.Forms.ComboBox cboAcl;
    }
}