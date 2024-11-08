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
            optCheckinFromOtherFile = new RadioButton();
            optCheckinFromCheckoutPath = new RadioButton();
            chkNewVersion = new CheckBox();
            panel2 = new Panel();
            txtCheckinFile = new TextBox();
            panel6 = new Panel();
            cmdSelectFile = new Button();
            lblFormat = new Label();
            panel3 = new Panel();
            cmdOk = new Button();
            panel4 = new Panel();
            cmdCancel = new Button();
            chkUpdateReferences = new CheckBox();
            cboFormat = new ComboBox();
            panel2.SuspendLayout();
            panel3.SuspendLayout();
            SuspendLayout();
            // 
            // optCheckinFromOtherFile
            // 
            optCheckinFromOtherFile.AutoSize = true;
            optCheckinFromOtherFile.Dock = DockStyle.Top;
            optCheckinFromOtherFile.ImeMode = ImeMode.NoControl;
            optCheckinFromOtherFile.Location = new Point(10, 58);
            optCheckinFromOtherFile.Margin = new Padding(4);
            optCheckinFromOtherFile.Name = "optCheckinFromOtherFile";
            optCheckinFromOtherFile.Size = new Size(464, 19);
            optCheckinFromOtherFile.TabIndex = 22;
            optCheckinFromOtherFile.Text = "Check in from other file";
            optCheckinFromOtherFile.UseVisualStyleBackColor = true;
            // 
            // optCheckinFromCheckoutPath
            // 
            optCheckinFromCheckoutPath.AutoSize = true;
            optCheckinFromCheckoutPath.Checked = true;
            optCheckinFromCheckoutPath.Dock = DockStyle.Top;
            optCheckinFromCheckoutPath.ImeMode = ImeMode.NoControl;
            optCheckinFromCheckoutPath.Location = new Point(10, 39);
            optCheckinFromCheckoutPath.Margin = new Padding(4);
            optCheckinFromCheckoutPath.Name = "optCheckinFromCheckoutPath";
            optCheckinFromCheckoutPath.Size = new Size(464, 19);
            optCheckinFromCheckoutPath.TabIndex = 21;
            optCheckinFromCheckoutPath.TabStop = true;
            optCheckinFromCheckoutPath.Text = "Check in from checked out file";
            optCheckinFromCheckoutPath.UseVisualStyleBackColor = true;
            optCheckinFromCheckoutPath.CheckedChanged += optCheckinFromCheckoutPath_CheckedChanged;
            // 
            // chkNewVersion
            // 
            chkNewVersion.AutoSize = true;
            chkNewVersion.Checked = true;
            chkNewVersion.CheckState = CheckState.Checked;
            chkNewVersion.Dock = DockStyle.Top;
            chkNewVersion.ImeMode = ImeMode.NoControl;
            chkNewVersion.Location = new Point(10, 10);
            chkNewVersion.Margin = new Padding(4);
            chkNewVersion.Name = "chkNewVersion";
            chkNewVersion.Padding = new Padding(0, 0, 0, 10);
            chkNewVersion.Size = new Size(464, 29);
            chkNewVersion.TabIndex = 20;
            chkNewVersion.Text = "Create new version";
            chkNewVersion.UseVisualStyleBackColor = true;
            // 
            // panel2
            // 
            panel2.Controls.Add(txtCheckinFile);
            panel2.Controls.Add(panel6);
            panel2.Controls.Add(cmdSelectFile);
            panel2.Dock = DockStyle.Top;
            panel2.Location = new Point(10, 77);
            panel2.Name = "panel2";
            panel2.Padding = new Padding(0, 0, 0, 10);
            panel2.Size = new Size(464, 34);
            panel2.TabIndex = 52;
            // 
            // txtCheckinFile
            // 
            txtCheckinFile.Dock = DockStyle.Fill;
            txtCheckinFile.Enabled = false;
            txtCheckinFile.Location = new Point(0, 0);
            txtCheckinFile.Margin = new Padding(4);
            txtCheckinFile.Name = "txtCheckinFile";
            txtCheckinFile.Size = new Size(410, 23);
            txtCheckinFile.TabIndex = 46;
            txtCheckinFile.TextChanged += txtCheckinFile_TextChanged;
            // 
            // panel6
            // 
            panel6.Dock = DockStyle.Right;
            panel6.Location = new Point(410, 0);
            panel6.Name = "panel6";
            panel6.Size = new Size(10, 24);
            panel6.TabIndex = 45;
            // 
            // cmdSelectFile
            // 
            cmdSelectFile.Dock = DockStyle.Right;
            cmdSelectFile.ImeMode = ImeMode.NoControl;
            cmdSelectFile.Location = new Point(420, 0);
            cmdSelectFile.Margin = new Padding(4, 3, 4, 3);
            cmdSelectFile.Name = "cmdSelectFile";
            cmdSelectFile.Size = new Size(44, 24);
            cmdSelectFile.TabIndex = 40;
            cmdSelectFile.Text = "...";
            cmdSelectFile.UseVisualStyleBackColor = true;
            cmdSelectFile.Click += cmdSelectFile_Click;
            // 
            // lblFormat
            // 
            lblFormat.AutoSize = true;
            lblFormat.Dock = DockStyle.Top;
            lblFormat.ImeMode = ImeMode.NoControl;
            lblFormat.Location = new Point(10, 111);
            lblFormat.Margin = new Padding(4, 0, 4, 0);
            lblFormat.Name = "lblFormat";
            lblFormat.Size = new Size(45, 15);
            lblFormat.TabIndex = 54;
            lblFormat.Text = "Format";
            // 
            // panel3
            // 
            panel3.Controls.Add(cmdOk);
            panel3.Controls.Add(panel4);
            panel3.Controls.Add(cmdCancel);
            panel3.Dock = DockStyle.Top;
            panel3.Location = new Point(10, 178);
            panel3.Name = "panel3";
            panel3.Padding = new Padding(0, 20, 0, 0);
            panel3.Size = new Size(464, 45);
            panel3.TabIndex = 59;
            // 
            // cmdOk
            // 
            cmdOk.DialogResult = DialogResult.OK;
            cmdOk.Dock = DockStyle.Right;
            cmdOk.ImeMode = ImeMode.NoControl;
            cmdOk.Location = new Point(278, 20);
            cmdOk.Margin = new Padding(4);
            cmdOk.Name = "cmdOk";
            cmdOk.Size = new Size(88, 25);
            cmdOk.TabIndex = 37;
            cmdOk.Text = "Ok";
            cmdOk.UseVisualStyleBackColor = true;
            // 
            // panel4
            // 
            panel4.Dock = DockStyle.Right;
            panel4.Location = new Point(366, 20);
            panel4.Name = "panel4";
            panel4.Size = new Size(10, 25);
            panel4.TabIndex = 36;
            // 
            // cmdCancel
            // 
            cmdCancel.DialogResult = DialogResult.Cancel;
            cmdCancel.Dock = DockStyle.Right;
            cmdCancel.ImeMode = ImeMode.NoControl;
            cmdCancel.Location = new Point(376, 20);
            cmdCancel.Margin = new Padding(4);
            cmdCancel.Name = "cmdCancel";
            cmdCancel.Size = new Size(88, 25);
            cmdCancel.TabIndex = 12;
            cmdCancel.Text = "Cancel";
            cmdCancel.UseVisualStyleBackColor = true;
            // 
            // chkUpdateReferences
            // 
            chkUpdateReferences.AutoSize = true;
            chkUpdateReferences.Dock = DockStyle.Top;
            chkUpdateReferences.ImeMode = ImeMode.NoControl;
            chkUpdateReferences.Location = new Point(10, 149);
            chkUpdateReferences.Margin = new Padding(4);
            chkUpdateReferences.Name = "chkUpdateReferences";
            chkUpdateReferences.Padding = new Padding(0, 10, 0, 0);
            chkUpdateReferences.Size = new Size(464, 29);
            chkUpdateReferences.TabIndex = 58;
            chkUpdateReferences.Text = "Update all references to latest version";
            chkUpdateReferences.UseVisualStyleBackColor = true;
            // 
            // cboFormat
            // 
            cboFormat.Dock = DockStyle.Top;
            cboFormat.DropDownStyle = ComboBoxStyle.DropDownList;
            cboFormat.FormattingEnabled = true;
            cboFormat.Location = new Point(10, 126);
            cboFormat.Margin = new Padding(4);
            cboFormat.Name = "cboFormat";
            cboFormat.Size = new Size(464, 23);
            cboFormat.Sorted = true;
            cboFormat.TabIndex = 57;
            // 
            // Checkin
            // 
            AcceptButton = cmdOk;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            CancelButton = cmdCancel;
            ClientSize = new Size(484, 231);
            Controls.Add(panel3);
            Controls.Add(chkUpdateReferences);
            Controls.Add(cboFormat);
            Controls.Add(lblFormat);
            Controls.Add(panel2);
            Controls.Add(optCheckinFromOtherFile);
            Controls.Add(optCheckinFromCheckoutPath);
            Controls.Add(chkNewVersion);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MinimumSize = new Size(500, 0);
            Name = "Checkin";
            Padding = new Padding(10);
            Text = "Checkin";
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            panel3.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        internal System.Windows.Forms.RadioButton optCheckinFromOtherFile;
        internal System.Windows.Forms.RadioButton optCheckinFromCheckoutPath;
        internal System.Windows.Forms.CheckBox chkNewVersion;
        private Panel panel2;
        internal TextBox txtCheckinFile;
        private Panel panel6;
        internal Button cmdSelectFile;
        internal Label lblFormat;
        private Panel panel3;
        internal Button cmdOk;
        private Panel panel4;
        internal Button cmdCancel;
        internal CheckBox chkUpdateReferences;
        internal ComboBox cboFormat;
    }
}