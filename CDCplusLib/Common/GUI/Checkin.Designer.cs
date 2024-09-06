namespace CDCplusLib.Common.GUI
{
    partial class Checkin
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
            this.chkUpdateReferences = new System.Windows.Forms.CheckBox();
            this.lblFormat = new System.Windows.Forms.Label();
            this.cboFormat = new System.Windows.Forms.ComboBox();
            this.cmdSelectFile = new System.Windows.Forms.Button();
            this.optCheckinFromOtherFile = new System.Windows.Forms.RadioButton();
            this.optCheckinFromCheckoutPath = new System.Windows.Forms.RadioButton();
            this.chkNewVersion = new System.Windows.Forms.CheckBox();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.cmdOk = new System.Windows.Forms.Button();
            this.txtCheckinFile = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // chkUpdateReferences
            // 
            this.chkUpdateReferences.AutoSize = true;
            this.chkUpdateReferences.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.chkUpdateReferences.Location = new System.Drawing.Point(13, 204);
            this.chkUpdateReferences.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.chkUpdateReferences.Name = "chkUpdateReferences";
            this.chkUpdateReferences.Size = new System.Drawing.Size(254, 20);
            this.chkUpdateReferences.TabIndex = 26;
            this.chkUpdateReferences.Text = "Update all references to latest version";
            this.chkUpdateReferences.UseVisualStyleBackColor = true;
            // 
            // lblFormat
            // 
            this.lblFormat.AutoSize = true;
            this.lblFormat.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblFormat.Location = new System.Drawing.Point(13, 139);
            this.lblFormat.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblFormat.Name = "lblFormat";
            this.lblFormat.Size = new System.Drawing.Size(49, 16);
            this.lblFormat.TabIndex = 25;
            this.lblFormat.Text = "Format";
            // 
            // cboFormat
            // 
            this.cboFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboFormat.FormattingEnabled = true;
            this.cboFormat.Location = new System.Drawing.Point(13, 158);
            this.cboFormat.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cboFormat.Name = "cboFormat";
            this.cboFormat.Size = new System.Drawing.Size(433, 24);
            this.cboFormat.Sorted = true;
            this.cboFormat.TabIndex = 24;
            // 
            // cmdSelectFile
            // 
            this.cmdSelectFile.Enabled = false;
            this.cmdSelectFile.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.cmdSelectFile.Location = new System.Drawing.Point(416, 99);
            this.cmdSelectFile.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cmdSelectFile.Name = "cmdSelectFile";
            this.cmdSelectFile.Size = new System.Drawing.Size(32, 25);
            this.cmdSelectFile.TabIndex = 23;
            this.cmdSelectFile.Text = "...";
            this.cmdSelectFile.UseVisualStyleBackColor = true;
            this.cmdSelectFile.Click += new System.EventHandler(this.cmdSelectFile_Click);
            // 
            // optCheckinFromOtherFile
            // 
            this.optCheckinFromOtherFile.AutoSize = true;
            this.optCheckinFromOtherFile.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.optCheckinFromOtherFile.Location = new System.Drawing.Point(13, 71);
            this.optCheckinFromOtherFile.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.optCheckinFromOtherFile.Name = "optCheckinFromOtherFile";
            this.optCheckinFromOtherFile.Size = new System.Drawing.Size(161, 20);
            this.optCheckinFromOtherFile.TabIndex = 22;
            this.optCheckinFromOtherFile.Text = "Check in from other file";
            this.optCheckinFromOtherFile.UseVisualStyleBackColor = true;
            // 
            // optCheckinFromCheckoutPath
            // 
            this.optCheckinFromCheckoutPath.AutoSize = true;
            this.optCheckinFromCheckoutPath.Checked = true;
            this.optCheckinFromCheckoutPath.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.optCheckinFromCheckoutPath.Location = new System.Drawing.Point(13, 43);
            this.optCheckinFromCheckoutPath.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.optCheckinFromCheckoutPath.Name = "optCheckinFromCheckoutPath";
            this.optCheckinFromCheckoutPath.Size = new System.Drawing.Size(204, 20);
            this.optCheckinFromCheckoutPath.TabIndex = 21;
            this.optCheckinFromCheckoutPath.TabStop = true;
            this.optCheckinFromCheckoutPath.Text = "Check in from checked out file";
            this.optCheckinFromCheckoutPath.UseVisualStyleBackColor = true;
            this.optCheckinFromCheckoutPath.CheckedChanged += new System.EventHandler(this.optCheckinFromCheckoutPath_CheckedChanged);
            // 
            // chkNewVersion
            // 
            this.chkNewVersion.AutoSize = true;
            this.chkNewVersion.Checked = true;
            this.chkNewVersion.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkNewVersion.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.chkNewVersion.Location = new System.Drawing.Point(13, 13);
            this.chkNewVersion.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.chkNewVersion.Name = "chkNewVersion";
            this.chkNewVersion.Size = new System.Drawing.Size(143, 20);
            this.chkNewVersion.TabIndex = 20;
            this.chkNewVersion.Text = "Create new version";
            this.chkNewVersion.UseVisualStyleBackColor = true;
            // 
            // cmdCancel
            // 
            this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdCancel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.cmdCancel.Location = new System.Drawing.Point(121, 259);
            this.cmdCancel.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(100, 28);
            this.cmdCancel.TabIndex = 19;
            this.cmdCancel.Text = "Cancel";
            this.cmdCancel.UseVisualStyleBackColor = true;
            // 
            // cmdOk
            // 
            this.cmdOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.cmdOk.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.cmdOk.Location = new System.Drawing.Point(13, 259);
            this.cmdOk.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cmdOk.Name = "cmdOk";
            this.cmdOk.Size = new System.Drawing.Size(100, 28);
            this.cmdOk.TabIndex = 18;
            this.cmdOk.Text = "Ok";
            this.cmdOk.UseVisualStyleBackColor = true;
            // 
            // txtCheckinFile
            // 
            this.txtCheckinFile.Enabled = false;
            this.txtCheckinFile.Location = new System.Drawing.Point(13, 99);
            this.txtCheckinFile.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtCheckinFile.Name = "txtCheckinFile";
            this.txtCheckinFile.Size = new System.Drawing.Size(393, 22);
            this.txtCheckinFile.TabIndex = 17;
            this.txtCheckinFile.TextChanged += new System.EventHandler(this.txtCheckinFile_TextChanged);
            // 
            // Checkin
            // 
            this.AcceptButton = this.cmdOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cmdCancel;
            this.ClientSize = new System.Drawing.Size(460, 300);
            this.Controls.Add(this.chkUpdateReferences);
            this.Controls.Add(this.lblFormat);
            this.Controls.Add(this.cboFormat);
            this.Controls.Add(this.cmdSelectFile);
            this.Controls.Add(this.optCheckinFromOtherFile);
            this.Controls.Add(this.optCheckinFromCheckoutPath);
            this.Controls.Add(this.chkNewVersion);
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.cmdOk);
            this.Controls.Add(this.txtCheckinFile);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "Checkin";
            this.Text = "Checkin";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.CheckBox chkUpdateReferences;
        internal System.Windows.Forms.Label lblFormat;
        internal System.Windows.Forms.ComboBox cboFormat;
        internal System.Windows.Forms.Button cmdSelectFile;
        internal System.Windows.Forms.RadioButton optCheckinFromOtherFile;
        internal System.Windows.Forms.RadioButton optCheckinFromCheckoutPath;
        internal System.Windows.Forms.CheckBox chkNewVersion;
        internal System.Windows.Forms.Button cmdCancel;
        internal System.Windows.Forms.Button cmdOk;
        internal System.Windows.Forms.TextBox txtCheckinFile;
    }
}