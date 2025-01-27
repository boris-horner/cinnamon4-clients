namespace CDCplusLib.Common.GUI
{
    partial class SelectObjectLinkResolver
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
            TableLayoutPanel1 = new TableLayoutPanel();
            cmdOk = new Button();
            cmdCancel = new Button();
            optFixedVersion = new RadioButton();
            optLatestVersion = new RadioButton();
            TableLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // TableLayoutPanel1
            // 
            TableLayoutPanel1.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            TableLayoutPanel1.ColumnCount = 2;
            TableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            TableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            TableLayoutPanel1.Controls.Add(cmdOk, 0, 0);
            TableLayoutPanel1.Controls.Add(cmdCancel, 1, 0);
            TableLayoutPanel1.Location = new Point(11, 80);
            TableLayoutPanel1.Margin = new Padding(4);
            TableLayoutPanel1.Name = "TableLayoutPanel1";
            TableLayoutPanel1.RowCount = 1;
            TableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            TableLayoutPanel1.Size = new Size(171, 34);
            TableLayoutPanel1.TabIndex = 7;
            // 
            // cmdOk
            // 
            cmdOk.Anchor = AnchorStyles.None;
            cmdOk.Location = new Point(4, 4);
            cmdOk.Margin = new Padding(4);
            cmdOk.Name = "cmdOk";
            cmdOk.Size = new Size(77, 26);
            cmdOk.TabIndex = 0;
            cmdOk.Text = "OK";
            cmdOk.Click += cmdOk_Click;
            // 
            // cmdCancel
            // 
            cmdCancel.Anchor = AnchorStyles.None;
            cmdCancel.DialogResult = DialogResult.Cancel;
            cmdCancel.Location = new Point(89, 4);
            cmdCancel.Margin = new Padding(4);
            cmdCancel.Name = "cmdCancel";
            cmdCancel.Size = new Size(78, 26);
            cmdCancel.TabIndex = 1;
            cmdCancel.Text = "Cancel";
            cmdCancel.Click += cmdCancel_Click;
            // 
            // optFixedVersion
            // 
            optFixedVersion.AutoSize = true;
            optFixedVersion.Location = new Point(11, 38);
            optFixedVersion.Margin = new Padding(4);
            optFixedVersion.Name = "optFixedVersion";
            optFixedVersion.Size = new Size(93, 19);
            optFixedVersion.TabIndex = 10;
            optFixedVersion.TabStop = true;
            optFixedVersion.Text = "Fixed version";
            optFixedVersion.UseVisualStyleBackColor = true;
            // 
            // optLatestVersion
            // 
            optLatestVersion.AutoSize = true;
            optLatestVersion.Location = new Point(11, 12);
            optLatestVersion.Margin = new Padding(4);
            optLatestVersion.Name = "optLatestVersion";
            optLatestVersion.Size = new Size(97, 19);
            optLatestVersion.TabIndex = 9;
            optLatestVersion.TabStop = true;
            optLatestVersion.Text = "Latest version";
            optLatestVersion.UseVisualStyleBackColor = true;
            // 
            // SelectObjectLinkResolver
            // 
            AcceptButton = cmdOk;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = cmdCancel;
            ClientSize = new Size(500, 126);
            Controls.Add(TableLayoutPanel1);
            Controls.Add(optFixedVersion);
            Controls.Add(optLatestVersion);
            Name = "SelectObjectLinkResolver";
            Text = "Select object link resolver";
            TableLayoutPanel1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        internal System.Windows.Forms.TableLayoutPanel TableLayoutPanel1;
        internal System.Windows.Forms.Button cmdOk;
        internal System.Windows.Forms.Button cmdCancel;
        internal System.Windows.Forms.RadioButton optFixedVersion;
        internal System.Windows.Forms.RadioButton optLatestVersion;
    }
}