namespace CDCplusLib.TabControls
{
	partial class FolderPropertiesTabControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FolderPropertiesTabControl));
            panel20 = new Panel();
            cmdSave = new Button();
            tableLayoutPanel1 = new TableLayoutPanel();
            tableLayoutPanel3 = new TableLayoutPanel();
            PictureBox6 = new PictureBox();
            Panel11 = new Panel();
            Panel47 = new Panel();
            lnkParentPath = new LinkLabel();
            lblFolderPath = new Label();
            Panel65 = new Panel();
            Panel66 = new Panel();
            txtParentId = new TextBox();
            lblFolderId = new Label();
            Panel67 = new Panel();
            Panel68 = new Panel();
            txtId = new TextBox();
            lblId = new Label();
            tableLayoutPanel2 = new TableLayoutPanel();
            Panel42 = new Panel();
            cboOwner = new ComboBox();
            lblOwner = new Label();
            Panel57 = new Panel();
            cboFolderType = new ComboBox();
            lblFolderType = new Label();
            PictureBox1 = new PictureBox();
            PictureBox9 = new PictureBox();
            PictureBox3 = new PictureBox();
            Panel36 = new Panel();
            txtName = new TextBox();
            lblName = new Label();
            panel20.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            tableLayoutPanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)PictureBox6).BeginInit();
            Panel11.SuspendLayout();
            Panel47.SuspendLayout();
            Panel66.SuspendLayout();
            Panel68.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            Panel42.SuspendLayout();
            Panel57.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)PictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox9).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox3).BeginInit();
            Panel36.SuspendLayout();
            SuspendLayout();
            // 
            // panel20
            // 
            panel20.Controls.Add(cmdSave);
            panel20.Dock = DockStyle.Bottom;
            panel20.Location = new Point(0, 930);
            panel20.Margin = new Padding(2);
            panel20.Name = "panel20";
            panel20.Size = new Size(1480, 40);
            panel20.TabIndex = 0;
            // 
            // cmdSave
            // 
            cmdSave.Dock = DockStyle.Left;
            cmdSave.Image = (Image)resources.GetObject("cmdSave.Image");
            cmdSave.Location = new Point(0, 0);
            cmdSave.Margin = new Padding(2);
            cmdSave.Name = "cmdSave";
            cmdSave.Size = new Size(40, 40);
            cmdSave.TabIndex = 4;
            cmdSave.UseVisualStyleBackColor = true;
            cmdSave.Click += cmdSave_Click;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.AutoSize = true;
            tableLayoutPanel1.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.Controls.Add(tableLayoutPanel3, 1, 0);
            tableLayoutPanel1.Controls.Add(tableLayoutPanel2, 0, 0);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Margin = new Padding(2);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 1;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.Size = new Size(1480, 930);
            tableLayoutPanel1.TabIndex = 1;
            // 
            // tableLayoutPanel3
            // 
            tableLayoutPanel3.AutoSize = true;
            tableLayoutPanel3.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            tableLayoutPanel3.ColumnCount = 2;
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 50F));
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel3.Controls.Add(PictureBox6, 0, 0);
            tableLayoutPanel3.Controls.Add(Panel11, 1, 0);
            tableLayoutPanel3.Dock = DockStyle.Top;
            tableLayoutPanel3.Location = new Point(742, 2);
            tableLayoutPanel3.Margin = new Padding(2);
            tableLayoutPanel3.Name = "tableLayoutPanel3";
            tableLayoutPanel3.RowCount = 1;
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
            tableLayoutPanel3.Size = new Size(736, 50);
            tableLayoutPanel3.TabIndex = 4;
            // 
            // PictureBox6
            // 
            PictureBox6.Dock = DockStyle.Fill;
            PictureBox6.Image = (Image)resources.GetObject("PictureBox6.Image");
            PictureBox6.Location = new Point(0, 0);
            PictureBox6.Margin = new Padding(0);
            PictureBox6.Name = "PictureBox6";
            PictureBox6.Padding = new Padding(9);
            PictureBox6.Size = new Size(50, 50);
            PictureBox6.SizeMode = PictureBoxSizeMode.AutoSize;
            PictureBox6.TabIndex = 7;
            PictureBox6.TabStop = false;
            // 
            // Panel11
            // 
            Panel11.Controls.Add(Panel47);
            Panel11.Dock = DockStyle.Fill;
            Panel11.Location = new Point(52, 2);
            Panel11.Margin = new Padding(2);
            Panel11.Name = "Panel11";
            Panel11.Size = new Size(682, 46);
            Panel11.TabIndex = 1;
            // 
            // Panel47
            // 
            Panel47.Controls.Add(lnkParentPath);
            Panel47.Controls.Add(lblFolderPath);
            Panel47.Controls.Add(Panel65);
            Panel47.Controls.Add(Panel66);
            Panel47.Controls.Add(Panel67);
            Panel47.Controls.Add(Panel68);
            Panel47.Dock = DockStyle.Fill;
            Panel47.Location = new Point(0, 0);
            Panel47.Margin = new Padding(0);
            Panel47.Name = "Panel47";
            Panel47.Size = new Size(682, 46);
            Panel47.TabIndex = 51;
            // 
            // lnkParentPath
            // 
            lnkParentPath.AutoSize = true;
            lnkParentPath.Dock = DockStyle.Top;
            lnkParentPath.Location = new Point(98, 15);
            lnkParentPath.Margin = new Padding(2, 0, 2, 0);
            lnkParentPath.Name = "lnkParentPath";
            lnkParentPath.Padding = new Padding(0, 1, 0, 0);
            lnkParentPath.Size = new Size(59, 16);
            lnkParentPath.TabIndex = 59;
            lnkParentPath.TabStop = true;
            lnkParentPath.Text = "/link/here";
            // 
            // lblFolderPath
            // 
            lblFolderPath.AutoSize = true;
            lblFolderPath.Dock = DockStyle.Top;
            lblFolderPath.Location = new Point(98, 0);
            lblFolderPath.Margin = new Padding(2, 0, 2, 0);
            lblFolderPath.Name = "lblFolderPath";
            lblFolderPath.Size = new Size(31, 15);
            lblFolderPath.TabIndex = 58;
            lblFolderPath.Text = "Path";
            // 
            // Panel65
            // 
            Panel65.Dock = DockStyle.Left;
            Panel65.Location = new Point(89, 0);
            Panel65.Margin = new Padding(0);
            Panel65.Name = "Panel65";
            Panel65.Size = new Size(9, 46);
            Panel65.TabIndex = 55;
            // 
            // Panel66
            // 
            Panel66.Controls.Add(txtParentId);
            Panel66.Controls.Add(lblFolderId);
            Panel66.Dock = DockStyle.Left;
            Panel66.Location = new Point(49, 0);
            Panel66.Margin = new Padding(0);
            Panel66.Name = "Panel66";
            Panel66.Size = new Size(40, 46);
            Panel66.TabIndex = 54;
            // 
            // txtParentId
            // 
            txtParentId.Dock = DockStyle.Fill;
            txtParentId.Location = new Point(0, 15);
            txtParentId.Margin = new Padding(2);
            txtParentId.Name = "txtParentId";
            txtParentId.ReadOnly = true;
            txtParentId.Size = new Size(40, 23);
            txtParentId.TabIndex = 45;
            txtParentId.Text = "Hallo";
            // 
            // lblFolderId
            // 
            lblFolderId.AutoSize = true;
            lblFolderId.Dock = DockStyle.Top;
            lblFolderId.Location = new Point(0, 0);
            lblFolderId.Margin = new Padding(2, 0, 2, 0);
            lblFolderId.Name = "lblFolderId";
            lblFolderId.Size = new Size(55, 15);
            lblFolderId.TabIndex = 44;
            lblFolderId.Text = "Parent ID";
            // 
            // Panel67
            // 
            Panel67.Dock = DockStyle.Left;
            Panel67.Location = new Point(40, 0);
            Panel67.Margin = new Padding(0);
            Panel67.Name = "Panel67";
            Panel67.Size = new Size(9, 46);
            Panel67.TabIndex = 53;
            // 
            // Panel68
            // 
            Panel68.Controls.Add(txtId);
            Panel68.Controls.Add(lblId);
            Panel68.Dock = DockStyle.Left;
            Panel68.Location = new Point(0, 0);
            Panel68.Margin = new Padding(0);
            Panel68.Name = "Panel68";
            Panel68.Size = new Size(40, 46);
            Panel68.TabIndex = 52;
            // 
            // txtId
            // 
            txtId.Dock = DockStyle.Fill;
            txtId.Location = new Point(0, 15);
            txtId.Margin = new Padding(2);
            txtId.Name = "txtId";
            txtId.ReadOnly = true;
            txtId.Size = new Size(40, 23);
            txtId.TabIndex = 45;
            // 
            // lblId
            // 
            lblId.AutoSize = true;
            lblId.Dock = DockStyle.Top;
            lblId.Location = new Point(0, 0);
            lblId.Margin = new Padding(2, 0, 2, 0);
            lblId.Name = "lblId";
            lblId.Size = new Size(18, 15);
            lblId.TabIndex = 44;
            lblId.Text = "ID";
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.AutoSize = true;
            tableLayoutPanel2.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            tableLayoutPanel2.ColumnCount = 2;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 50F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel2.Controls.Add(Panel42, 1, 2);
            tableLayoutPanel2.Controls.Add(Panel57, 1, 1);
            tableLayoutPanel2.Controls.Add(PictureBox1, 0, 0);
            tableLayoutPanel2.Controls.Add(PictureBox9, 0, 1);
            tableLayoutPanel2.Controls.Add(PictureBox3, 0, 2);
            tableLayoutPanel2.Controls.Add(Panel36, 1, 0);
            tableLayoutPanel2.Dock = DockStyle.Top;
            tableLayoutPanel2.Location = new Point(2, 2);
            tableLayoutPanel2.Margin = new Padding(2);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 3;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel2.Size = new Size(736, 150);
            tableLayoutPanel2.TabIndex = 3;
            // 
            // Panel42
            // 
            Panel42.Controls.Add(cboOwner);
            Panel42.Controls.Add(lblOwner);
            Panel42.Dock = DockStyle.Fill;
            Panel42.Location = new Point(50, 100);
            Panel42.Margin = new Padding(0);
            Panel42.Name = "Panel42";
            Panel42.Size = new Size(686, 50);
            Panel42.TabIndex = 52;
            // 
            // cboOwner
            // 
            cboOwner.Dock = DockStyle.Fill;
            cboOwner.DropDownStyle = ComboBoxStyle.DropDownList;
            cboOwner.FormattingEnabled = true;
            cboOwner.Location = new Point(0, 15);
            cboOwner.Margin = new Padding(2);
            cboOwner.Name = "cboOwner";
            cboOwner.Size = new Size(686, 23);
            cboOwner.Sorted = true;
            cboOwner.TabIndex = 22;
            cboOwner.SelectedIndexChanged += cboOwner_SelectedIndexChanged;
            // 
            // lblOwner
            // 
            lblOwner.AutoSize = true;
            lblOwner.Dock = DockStyle.Top;
            lblOwner.Location = new Point(0, 0);
            lblOwner.Margin = new Padding(2, 0, 2, 0);
            lblOwner.Name = "lblOwner";
            lblOwner.Size = new Size(42, 15);
            lblOwner.TabIndex = 21;
            lblOwner.Text = "Owner";
            // 
            // Panel57
            // 
            Panel57.Controls.Add(cboFolderType);
            Panel57.Controls.Add(lblFolderType);
            Panel57.Dock = DockStyle.Fill;
            Panel57.Location = new Point(50, 50);
            Panel57.Margin = new Padding(0);
            Panel57.Name = "Panel57";
            Panel57.Size = new Size(686, 50);
            Panel57.TabIndex = 48;
            // 
            // cboFolderType
            // 
            cboFolderType.Dock = DockStyle.Fill;
            cboFolderType.DropDownStyle = ComboBoxStyle.DropDownList;
            cboFolderType.FormattingEnabled = true;
            cboFolderType.Location = new Point(0, 15);
            cboFolderType.Margin = new Padding(2);
            cboFolderType.Name = "cboFolderType";
            cboFolderType.Size = new Size(686, 23);
            cboFolderType.Sorted = true;
            cboFolderType.TabIndex = 19;
            cboFolderType.SelectedIndexChanged += cboFolderType_SelectedIndexChanged;
            // 
            // lblFolderType
            // 
            lblFolderType.AutoSize = true;
            lblFolderType.Dock = DockStyle.Top;
            lblFolderType.Location = new Point(0, 0);
            lblFolderType.Margin = new Padding(2, 0, 2, 0);
            lblFolderType.Name = "lblFolderType";
            lblFolderType.Size = new Size(66, 15);
            lblFolderType.TabIndex = 18;
            lblFolderType.Text = "Folder type";
            // 
            // PictureBox1
            // 
            PictureBox1.Dock = DockStyle.Fill;
            PictureBox1.Image = (Image)resources.GetObject("PictureBox1.Image");
            PictureBox1.Location = new Point(0, 0);
            PictureBox1.Margin = new Padding(0);
            PictureBox1.Name = "PictureBox1";
            PictureBox1.Padding = new Padding(9);
            PictureBox1.Size = new Size(50, 50);
            PictureBox1.SizeMode = PictureBoxSizeMode.AutoSize;
            PictureBox1.TabIndex = 24;
            PictureBox1.TabStop = false;
            // 
            // PictureBox9
            // 
            PictureBox9.Dock = DockStyle.Fill;
            PictureBox9.Image = (Image)resources.GetObject("PictureBox9.Image");
            PictureBox9.Location = new Point(0, 50);
            PictureBox9.Margin = new Padding(0);
            PictureBox9.Name = "PictureBox9";
            PictureBox9.Padding = new Padding(9);
            PictureBox9.Size = new Size(50, 50);
            PictureBox9.SizeMode = PictureBoxSizeMode.AutoSize;
            PictureBox9.TabIndex = 23;
            PictureBox9.TabStop = false;
            // 
            // PictureBox3
            // 
            PictureBox3.Dock = DockStyle.Fill;
            PictureBox3.Image = (Image)resources.GetObject("PictureBox3.Image");
            PictureBox3.Location = new Point(0, 100);
            PictureBox3.Margin = new Padding(0);
            PictureBox3.Name = "PictureBox3";
            PictureBox3.Padding = new Padding(9);
            PictureBox3.Size = new Size(50, 50);
            PictureBox3.SizeMode = PictureBoxSizeMode.AutoSize;
            PictureBox3.TabIndex = 22;
            PictureBox3.TabStop = false;
            // 
            // Panel36
            // 
            Panel36.Controls.Add(txtName);
            Panel36.Controls.Add(lblName);
            Panel36.Dock = DockStyle.Fill;
            Panel36.Location = new Point(52, 2);
            Panel36.Margin = new Padding(2);
            Panel36.Name = "Panel36";
            Panel36.Size = new Size(682, 46);
            Panel36.TabIndex = 2;
            // 
            // txtName
            // 
            txtName.Dock = DockStyle.Fill;
            txtName.Location = new Point(0, 15);
            txtName.Name = "txtName";
            txtName.Size = new Size(682, 23);
            txtName.TabIndex = 7;
            txtName.TextChanged += txtName_TextChanged;
            // 
            // lblName
            // 
            lblName.AutoSize = true;
            lblName.Dock = DockStyle.Top;
            lblName.Location = new Point(0, 0);
            lblName.Margin = new Padding(2, 0, 2, 0);
            lblName.Name = "lblName";
            lblName.Size = new Size(39, 15);
            lblName.TabIndex = 6;
            lblName.Text = "Name";
            // 
            // FolderPropertiesTabControl
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Controls.Add(tableLayoutPanel1);
            Controls.Add(panel20);
            Margin = new Padding(2);
            Name = "FolderPropertiesTabControl";
            Size = new Size(1480, 970);
            panel20.ResumeLayout(false);
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            tableLayoutPanel3.ResumeLayout(false);
            tableLayoutPanel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)PictureBox6).EndInit();
            Panel11.ResumeLayout(false);
            Panel47.ResumeLayout(false);
            Panel47.PerformLayout();
            Panel66.ResumeLayout(false);
            Panel66.PerformLayout();
            Panel68.ResumeLayout(false);
            Panel68.PerformLayout();
            tableLayoutPanel2.ResumeLayout(false);
            tableLayoutPanel2.PerformLayout();
            Panel42.ResumeLayout(false);
            Panel42.PerformLayout();
            Panel57.ResumeLayout(false);
            Panel57.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)PictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox9).EndInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox3).EndInit();
            Panel36.ResumeLayout(false);
            Panel36.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Panel panel20;
		private TableLayoutPanel tableLayoutPanel1;
		internal Button cmdSave;
		private TableLayoutPanel tableLayoutPanel2;
		internal Panel Panel36;
		private TableLayoutPanel tableLayoutPanel3;
		internal Panel Panel11;
		internal Panel Panel47;
		internal LinkLabel lnkParentPath;
		internal Label lblFolderPath;
		internal Panel Panel65;
		internal Panel Panel66;
		internal TextBox txtParentId;
		internal Label lblFolderId;
		internal Panel Panel67;
		internal Panel Panel68;
		internal TextBox txtId;
		internal Label lblId;
        internal PictureBox PictureBox6;
        internal PictureBox PictureBox3;
        internal PictureBox PictureBox9;
        internal PictureBox PictureBox1;
        internal Panel Panel42;
        internal ComboBox cboOwner;
        internal Label lblOwner;
        internal Panel Panel57;
        internal ComboBox cboFolderType;
        internal Label lblFolderType;
        internal TextBox txtName;
        internal Label lblName;
    }
}
