namespace CDCplusLib.Common.GUI
{
    partial class SelectVersion
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
            this.TableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.cmdOk = new System.Windows.Forms.Button();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.lvwVersion = new System.Windows.Forms.ListView();
            this.colVersion = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.optFixedVersion = new System.Windows.Forms.RadioButton();
            this.optLatestVersion = new System.Windows.Forms.RadioButton();
            this.TableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // TableLayoutPanel1
            // 
            this.TableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.TableLayoutPanel1.ColumnCount = 2;
            this.TableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.TableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.TableLayoutPanel1.Controls.Add(this.cmdOk, 0, 0);
            this.TableLayoutPanel1.Controls.Add(this.cmdCancel, 1, 0);
            this.TableLayoutPanel1.Location = new System.Drawing.Point(13, 373);
            this.TableLayoutPanel1.Margin = new System.Windows.Forms.Padding(4);
            this.TableLayoutPanel1.Name = "TableLayoutPanel1";
            this.TableLayoutPanel1.RowCount = 1;
            this.TableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.TableLayoutPanel1.Size = new System.Drawing.Size(195, 36);
            this.TableLayoutPanel1.TabIndex = 7;
            // 
            // cmdOk
            // 
            this.cmdOk.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.cmdOk.Location = new System.Drawing.Point(4, 4);
            this.cmdOk.Margin = new System.Windows.Forms.Padding(4);
            this.cmdOk.Name = "cmdOk";
            this.cmdOk.Size = new System.Drawing.Size(89, 28);
            this.cmdOk.TabIndex = 0;
            this.cmdOk.Text = "OK";
            this.cmdOk.Click += new System.EventHandler(this.cmdOk_Click);
            // 
            // cmdCancel
            // 
            this.cmdCancel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdCancel.Location = new System.Drawing.Point(101, 4);
            this.cmdCancel.Margin = new System.Windows.Forms.Padding(4);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(89, 28);
            this.cmdCancel.TabIndex = 1;
            this.cmdCancel.Text = "Cancel";
            this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
            // 
            // lvwVersion
            // 
            this.lvwVersion.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colVersion,
            this.colName});
            this.lvwVersion.FullRowSelect = true;
            this.lvwVersion.HideSelection = false;
            this.lvwVersion.Location = new System.Drawing.Point(12, 70);
            this.lvwVersion.Margin = new System.Windows.Forms.Padding(4);
            this.lvwVersion.Name = "lvwVersion";
            this.lvwVersion.Size = new System.Drawing.Size(547, 281);
            this.lvwVersion.TabIndex = 8;
            this.lvwVersion.UseCompatibleStateImageBehavior = false;
            this.lvwVersion.View = System.Windows.Forms.View.Details;
            this.lvwVersion.SelectedIndexChanged += new System.EventHandler(this.lvwVersion_SelectedIndexChanged);
            // 
            // colVersion
            // 
            this.colVersion.Text = "Version";
            this.colVersion.Width = 94;
            // 
            // colName
            // 
            this.colName.Text = "Name";
            this.colName.Width = 293;
            // 
            // optFixedVersion
            // 
            this.optFixedVersion.AutoSize = true;
            this.optFixedVersion.Location = new System.Drawing.Point(13, 41);
            this.optFixedVersion.Margin = new System.Windows.Forms.Padding(4);
            this.optFixedVersion.Name = "optFixedVersion";
            this.optFixedVersion.Size = new System.Drawing.Size(108, 20);
            this.optFixedVersion.TabIndex = 10;
            this.optFixedVersion.TabStop = true;
            this.optFixedVersion.Text = "Fixed version";
            this.optFixedVersion.UseVisualStyleBackColor = true;
            this.optFixedVersion.CheckedChanged += new System.EventHandler(this.optFixedVersion_CheckedChanged);
            // 
            // optLatestVersion
            // 
            this.optLatestVersion.AutoSize = true;
            this.optLatestVersion.Location = new System.Drawing.Point(13, 13);
            this.optLatestVersion.Margin = new System.Windows.Forms.Padding(4);
            this.optLatestVersion.Name = "optLatestVersion";
            this.optLatestVersion.Size = new System.Drawing.Size(111, 20);
            this.optLatestVersion.TabIndex = 9;
            this.optLatestVersion.TabStop = true;
            this.optLatestVersion.Text = "Latest version";
            this.optLatestVersion.UseVisualStyleBackColor = true;
            this.optLatestVersion.CheckedChanged += new System.EventHandler(this.optLatestVersion_CheckedChanged);
            // 
            // SelectVersion
            // 
            this.AcceptButton = this.cmdOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cmdCancel;
            this.ClientSize = new System.Drawing.Size(571, 422);
            this.Controls.Add(this.TableLayoutPanel1);
            this.Controls.Add(this.lvwVersion);
            this.Controls.Add(this.optFixedVersion);
            this.Controls.Add(this.optLatestVersion);
            this.Name = "SelectVersion";
            this.Text = "Select version";
            this.TableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.TableLayoutPanel TableLayoutPanel1;
        internal System.Windows.Forms.Button cmdOk;
        internal System.Windows.Forms.Button cmdCancel;
        internal System.Windows.Forms.ListView lvwVersion;
        internal System.Windows.Forms.ColumnHeader colVersion;
        internal System.Windows.Forms.ColumnHeader colName;
        internal System.Windows.Forms.RadioButton optFixedVersion;
        internal System.Windows.Forms.RadioButton optLatestVersion;
    }
}