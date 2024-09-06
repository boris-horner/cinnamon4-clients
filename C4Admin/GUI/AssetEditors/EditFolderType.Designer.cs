
namespace C4Admin.GUI.AssetEditors
{
    partial class EditFolderType
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
            this.panel7 = new System.Windows.Forms.Panel();
            this.pCancel = new System.Windows.Forms.Panel();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.pSaveAs = new System.Windows.Forms.Panel();
            this.cmdSaveAs = new System.Windows.Forms.Button();
            this.pSave = new System.Windows.Forms.Panel();
            this.cmdOk = new System.Windows.Forms.Button();
            this.pData = new System.Windows.Forms.Panel();
            this.vtxtName = new C4GeneralGui.GuiElements.ValidatingTextBox();
            this.lblName = new System.Windows.Forms.Label();
            this.txtId = new System.Windows.Forms.TextBox();
            this.lblId = new System.Windows.Forms.Label();
            this.panel7.SuspendLayout();
            this.pCancel.SuspendLayout();
            this.pSaveAs.SuspendLayout();
            this.pSave.SuspendLayout();
            this.pData.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel7
            // 
            this.panel7.Controls.Add(this.pCancel);
            this.panel7.Controls.Add(this.pSaveAs);
            this.panel7.Controls.Add(this.pSave);
            this.panel7.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel7.Location = new System.Drawing.Point(0, 112);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(457, 37);
            this.panel7.TabIndex = 6;
            // 
            // pCancel
            // 
            this.pCancel.Controls.Add(this.cmdCancel);
            this.pCancel.Dock = System.Windows.Forms.DockStyle.Left;
            this.pCancel.Location = new System.Drawing.Point(224, 0);
            this.pCancel.Name = "pCancel";
            this.pCancel.Padding = new System.Windows.Forms.Padding(6, 7, 10, 7);
            this.pCancel.Size = new System.Drawing.Size(112, 37);
            this.pCancel.TabIndex = 11;
            // 
            // cmdCancel
            // 
            this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdCancel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmdCancel.Location = new System.Drawing.Point(6, 7);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(96, 23);
            this.cmdCancel.TabIndex = 9;
            this.cmdCancel.Text = "Cancel";
            this.cmdCancel.UseVisualStyleBackColor = true;
            this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
            // 
            // pSaveAs
            // 
            this.pSaveAs.Controls.Add(this.cmdSaveAs);
            this.pSaveAs.Dock = System.Windows.Forms.DockStyle.Left;
            this.pSaveAs.Location = new System.Drawing.Point(112, 0);
            this.pSaveAs.Name = "pSaveAs";
            this.pSaveAs.Padding = new System.Windows.Forms.Padding(6, 7, 10, 7);
            this.pSaveAs.Size = new System.Drawing.Size(112, 37);
            this.pSaveAs.TabIndex = 10;
            // 
            // cmdSaveAs
            // 
            this.cmdSaveAs.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdSaveAs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmdSaveAs.Location = new System.Drawing.Point(6, 7);
            this.cmdSaveAs.Name = "cmdSaveAs";
            this.cmdSaveAs.Size = new System.Drawing.Size(96, 23);
            this.cmdSaveAs.TabIndex = 6;
            this.cmdSaveAs.Text = "Save as";
            this.cmdSaveAs.UseVisualStyleBackColor = true;
            this.cmdSaveAs.Click += new System.EventHandler(this.cmdSaveAs_Click);
            // 
            // pSave
            // 
            this.pSave.Controls.Add(this.cmdOk);
            this.pSave.Dock = System.Windows.Forms.DockStyle.Left;
            this.pSave.Location = new System.Drawing.Point(0, 0);
            this.pSave.Name = "pSave";
            this.pSave.Padding = new System.Windows.Forms.Padding(6, 7, 10, 7);
            this.pSave.Size = new System.Drawing.Size(112, 37);
            this.pSave.TabIndex = 9;
            // 
            // cmdOk
            // 
            this.cmdOk.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmdOk.Location = new System.Drawing.Point(6, 7);
            this.cmdOk.Name = "cmdOk";
            this.cmdOk.Size = new System.Drawing.Size(96, 23);
            this.cmdOk.TabIndex = 2;
            this.cmdOk.Text = "Save";
            this.cmdOk.UseVisualStyleBackColor = true;
            this.cmdOk.Click += new System.EventHandler(this.cmdOk_Click);
            // 
            // pData
            // 
            this.pData.Controls.Add(this.vtxtName);
            this.pData.Controls.Add(this.lblName);
            this.pData.Controls.Add(this.txtId);
            this.pData.Controls.Add(this.lblId);
            this.pData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pData.Location = new System.Drawing.Point(0, 0);
            this.pData.Name = "pData";
            this.pData.Padding = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.pData.Size = new System.Drawing.Size(457, 112);
            this.pData.TabIndex = 0;
            // 
            // vtxtName
            // 
            this.vtxtName.BackColor = System.Drawing.Color.Yellow;
            this.vtxtName.BothInvalidColor = System.Drawing.Color.Red;
            this.vtxtName.Dock = System.Windows.Forms.DockStyle.Top;
            this.vtxtName.ExternalInvalidColor = System.Drawing.Color.Orange;
            this.vtxtName.ExternalValidation = null;
            this.vtxtName.InternalInvalidColor = System.Drawing.Color.Yellow;
            this.vtxtName.InvalidReadOnlyColor = System.Drawing.Color.LightYellow;
            this.vtxtName.Location = new System.Drawing.Point(5, 68);
            this.vtxtName.Name = "vtxtName";
            this.vtxtName.RegularExpression = "^[a-zA-Z0-9_.-]+$";
            this.vtxtName.Size = new System.Drawing.Size(447, 23);
            this.vtxtName.TabIndex = 0;
            this.vtxtName.ValidColor = System.Drawing.Color.White;
            this.vtxtName.ValidReadOnlyColor = System.Drawing.SystemColors.Control;
            this.vtxtName.TextChanged += new System.EventHandler(this.vtxtName_TextChanged);
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblName.Location = new System.Drawing.Point(5, 44);
            this.lblName.Margin = new System.Windows.Forms.Padding(0);
            this.lblName.Name = "lblName";
            this.lblName.Padding = new System.Windows.Forms.Padding(0, 9, 0, 0);
            this.lblName.Size = new System.Drawing.Size(39, 24);
            this.lblName.TabIndex = 4;
            this.lblName.Text = "Name";
            // 
            // txtId
            // 
            this.txtId.Dock = System.Windows.Forms.DockStyle.Top;
            this.txtId.Location = new System.Drawing.Point(5, 21);
            this.txtId.Margin = new System.Windows.Forms.Padding(0);
            this.txtId.Name = "txtId";
            this.txtId.ReadOnly = true;
            this.txtId.Size = new System.Drawing.Size(447, 23);
            this.txtId.TabIndex = 1;
            // 
            // lblId
            // 
            this.lblId.AutoSize = true;
            this.lblId.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblId.Location = new System.Drawing.Point(5, 6);
            this.lblId.Margin = new System.Windows.Forms.Padding(0);
            this.lblId.Name = "lblId";
            this.lblId.Size = new System.Drawing.Size(17, 15);
            this.lblId.TabIndex = 0;
            this.lblId.Text = "Id";
            // 
            // EditFolderType
            // 
            this.AcceptButton = this.cmdOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cmdCancel;
            this.ClientSize = new System.Drawing.Size(457, 149);
            this.Controls.Add(this.pData);
            this.Controls.Add(this.panel7);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "EditFolderType";
            this.Text = "Edit folder type";
            this.Load += new System.EventHandler(this.EditFolderType_Load);
            this.panel7.ResumeLayout(false);
            this.pCancel.ResumeLayout(false);
            this.pSaveAs.ResumeLayout(false);
            this.pSave.ResumeLayout(false);
            this.pData.ResumeLayout(false);
            this.pData.PerformLayout();
            this.ResumeLayout(false);

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
    }
}