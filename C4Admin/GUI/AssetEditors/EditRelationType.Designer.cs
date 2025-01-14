
namespace C4Admin.GUI.AssetEditors
{
    partial class EditRelationType
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
            chkCloneRightVersion = new CheckBox();
            chkCloneLeftVersion = new CheckBox();
            chkCloneRightCopy = new CheckBox();
            chkCloneLeftCopy = new CheckBox();
            chkRightObjectProtected = new CheckBox();
            chkLeftObjectProtected = new CheckBox();
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
            panel7.Location = new Point(0, 294);
            panel7.Name = "panel7";
            panel7.Size = new Size(457, 37);
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
            pData.Controls.Add(chkCloneRightVersion);
            pData.Controls.Add(chkCloneLeftVersion);
            pData.Controls.Add(chkCloneRightCopy);
            pData.Controls.Add(chkCloneLeftCopy);
            pData.Controls.Add(chkRightObjectProtected);
            pData.Controls.Add(chkLeftObjectProtected);
            pData.Controls.Add(vtxtName);
            pData.Controls.Add(lblName);
            pData.Controls.Add(txtId);
            pData.Controls.Add(lblId);
            pData.Dock = DockStyle.Fill;
            pData.Location = new Point(0, 0);
            pData.Name = "pData";
            pData.Padding = new Padding(5, 6, 5, 6);
            pData.Size = new Size(457, 294);
            pData.TabIndex = 0;
            // 
            // chkCloneRightVersion
            // 
            chkCloneRightVersion.AutoSize = true;
            chkCloneRightVersion.Dock = DockStyle.Top;
            chkCloneRightVersion.Location = new Point(5, 231);
            chkCloneRightVersion.Name = "chkCloneRightVersion";
            chkCloneRightVersion.Padding = new Padding(0, 9, 0, 0);
            chkCloneRightVersion.Size = new Size(447, 28);
            chkCloneRightVersion.TabIndex = 11;
            chkCloneRightVersion.Text = "Clone relation if right object is versioned";
            chkCloneRightVersion.UseVisualStyleBackColor = true;
            // 
            // chkCloneLeftVersion
            // 
            chkCloneLeftVersion.AutoSize = true;
            chkCloneLeftVersion.Dock = DockStyle.Top;
            chkCloneLeftVersion.Location = new Point(5, 203);
            chkCloneLeftVersion.Name = "chkCloneLeftVersion";
            chkCloneLeftVersion.Padding = new Padding(0, 9, 0, 0);
            chkCloneLeftVersion.Size = new Size(447, 28);
            chkCloneLeftVersion.TabIndex = 10;
            chkCloneLeftVersion.Text = "Clone relation if left object is versioned";
            chkCloneLeftVersion.UseVisualStyleBackColor = true;
            // 
            // chkCloneRightCopy
            // 
            chkCloneRightCopy.AutoSize = true;
            chkCloneRightCopy.Dock = DockStyle.Top;
            chkCloneRightCopy.Location = new Point(5, 175);
            chkCloneRightCopy.Name = "chkCloneRightCopy";
            chkCloneRightCopy.Padding = new Padding(0, 9, 0, 0);
            chkCloneRightCopy.Size = new Size(447, 28);
            chkCloneRightCopy.TabIndex = 9;
            chkCloneRightCopy.Text = "Clone relation if right object is copied";
            chkCloneRightCopy.UseVisualStyleBackColor = true;
            // 
            // chkCloneLeftCopy
            // 
            chkCloneLeftCopy.AutoSize = true;
            chkCloneLeftCopy.Dock = DockStyle.Top;
            chkCloneLeftCopy.Location = new Point(5, 147);
            chkCloneLeftCopy.Name = "chkCloneLeftCopy";
            chkCloneLeftCopy.Padding = new Padding(0, 9, 0, 0);
            chkCloneLeftCopy.Size = new Size(447, 28);
            chkCloneLeftCopy.TabIndex = 8;
            chkCloneLeftCopy.Text = "Clone relation if left object is copied";
            chkCloneLeftCopy.UseVisualStyleBackColor = true;
            // 
            // chkRightObjectProtected
            // 
            chkRightObjectProtected.AutoSize = true;
            chkRightObjectProtected.Dock = DockStyle.Top;
            chkRightObjectProtected.Location = new Point(5, 119);
            chkRightObjectProtected.Name = "chkRightObjectProtected";
            chkRightObjectProtected.Padding = new Padding(0, 9, 0, 0);
            chkRightObjectProtected.Size = new Size(447, 28);
            chkRightObjectProtected.TabIndex = 7;
            chkRightObjectProtected.Text = "Right object protected";
            chkRightObjectProtected.UseVisualStyleBackColor = true;
            // 
            // chkLeftObjectProtected
            // 
            chkLeftObjectProtected.AutoSize = true;
            chkLeftObjectProtected.Dock = DockStyle.Top;
            chkLeftObjectProtected.Location = new Point(5, 91);
            chkLeftObjectProtected.Name = "chkLeftObjectProtected";
            chkLeftObjectProtected.Padding = new Padding(0, 9, 0, 0);
            chkLeftObjectProtected.Size = new Size(447, 28);
            chkLeftObjectProtected.TabIndex = 6;
            chkLeftObjectProtected.Text = "Left object protected";
            chkLeftObjectProtected.UseVisualStyleBackColor = true;
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
            vtxtName.Size = new Size(447, 23);
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
            txtId.Size = new Size(447, 23);
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
            // EditRelationType
            // 
            AcceptButton = cmdOk;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = cmdCancel;
            ClientSize = new Size(457, 331);
            Controls.Add(pData);
            Controls.Add(panel7);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Name = "EditRelationType";
            Text = "Edit relation type";
            Load += EditRelationType_Load;
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
        private System.Windows.Forms.CheckBox chkCloneRightVersion;
        private System.Windows.Forms.CheckBox chkCloneLeftVersion;
        private System.Windows.Forms.CheckBox chkCloneRightCopy;
        private System.Windows.Forms.CheckBox chkCloneLeftCopy;
        private System.Windows.Forms.CheckBox chkRightObjectProtected;
        private System.Windows.Forms.CheckBox chkLeftObjectProtected;
    }
}