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
            panel1 = new Panel();
            panel14 = new Panel();
            panel16 = new Panel();
            panel8 = new Panel();
            cboOwner = new ComboBox();
            lblOwner = new Label();
            pOwnerIcon = new Panel();
            PictureBox3 = new PictureBox();
            panel10 = new Panel();
            panel12 = new Panel();
            panel7 = new Panel();
            cboFolderType = new ComboBox();
            lblFolderType = new Label();
            pFolderTypeIcon = new Panel();
            pictureBox2 = new PictureBox();
            pName = new Panel();
            panel9 = new Panel();
            panel2 = new Panel();
            txtName = new TextBox();
            lblName = new Label();
            pNameIcon = new Panel();
            PictureBox1 = new PictureBox();
            panel3 = new Panel();
            panel17 = new Panel();
            panel4 = new Panel();
            panel15 = new Panel();
            panel24 = new Panel();
            lnkParentPath = new LinkLabel();
            lblFolderPath = new Label();
            panel6 = new Panel();
            panel18 = new Panel();
            txtParentId = new TextBox();
            lblFolderId = new Label();
            panel19 = new Panel();
            panel21 = new Panel();
            txtId = new TextBox();
            lblId = new Label();
            pIdsIcon = new Panel();
            pictureBox12 = new PictureBox();
            panel20.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            panel1.SuspendLayout();
            panel14.SuspendLayout();
            panel16.SuspendLayout();
            pOwnerIcon.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)PictureBox3).BeginInit();
            panel10.SuspendLayout();
            panel12.SuspendLayout();
            pFolderTypeIcon.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            pName.SuspendLayout();
            panel9.SuspendLayout();
            pNameIcon.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)PictureBox1).BeginInit();
            panel3.SuspendLayout();
            panel17.SuspendLayout();
            panel4.SuspendLayout();
            panel15.SuspendLayout();
            panel18.SuspendLayout();
            panel21.SuspendLayout();
            pIdsIcon.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox12).BeginInit();
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
            tableLayoutPanel1.Controls.Add(panel1, 0, 0);
            tableLayoutPanel1.Controls.Add(panel3, 1, 0);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Margin = new Padding(2);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 1;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.Size = new Size(1480, 930);
            tableLayoutPanel1.TabIndex = 1;
            // 
            // panel1
            // 
            panel1.Controls.Add(panel14);
            panel1.Controls.Add(panel10);
            panel1.Controls.Add(pName);
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(3, 3);
            panel1.Name = "panel1";
            panel1.Size = new Size(734, 924);
            panel1.TabIndex = 5;
            // 
            // panel14
            // 
            panel14.AutoSize = true;
            panel14.Controls.Add(panel16);
            panel14.Controls.Add(pOwnerIcon);
            panel14.Dock = DockStyle.Top;
            panel14.Location = new Point(0, 100);
            panel14.Name = "panel14";
            panel14.Size = new Size(734, 50);
            panel14.TabIndex = 7;
            // 
            // panel16
            // 
            panel16.AutoSize = true;
            panel16.Controls.Add(panel8);
            panel16.Controls.Add(cboOwner);
            panel16.Controls.Add(lblOwner);
            panel16.Dock = DockStyle.Fill;
            panel16.Location = new Point(50, 0);
            panel16.Name = "panel16";
            panel16.Size = new Size(684, 50);
            panel16.TabIndex = 1;
            // 
            // panel8
            // 
            panel8.Dock = DockStyle.Top;
            panel8.Location = new Point(0, 38);
            panel8.Name = "panel8";
            panel8.Size = new Size(684, 12);
            panel8.TabIndex = 25;
            // 
            // cboOwner
            // 
            cboOwner.Dock = DockStyle.Top;
            cboOwner.DropDownStyle = ComboBoxStyle.DropDownList;
            cboOwner.FormattingEnabled = true;
            cboOwner.Location = new Point(0, 15);
            cboOwner.Name = "cboOwner";
            cboOwner.Size = new Size(684, 23);
            cboOwner.Sorted = true;
            cboOwner.TabIndex = 24;
            cboOwner.SelectedIndexChanged += cboOwner_SelectedIndexChanged;
            // 
            // lblOwner
            // 
            lblOwner.AutoSize = true;
            lblOwner.Dock = DockStyle.Top;
            lblOwner.Location = new Point(0, 0);
            lblOwner.Name = "lblOwner";
            lblOwner.Size = new Size(42, 15);
            lblOwner.TabIndex = 23;
            lblOwner.Text = "Owner";
            // 
            // pOwnerIcon
            // 
            pOwnerIcon.Controls.Add(PictureBox3);
            pOwnerIcon.Dock = DockStyle.Left;
            pOwnerIcon.Location = new Point(0, 0);
            pOwnerIcon.Name = "pOwnerIcon";
            pOwnerIcon.Size = new Size(50, 50);
            pOwnerIcon.TabIndex = 0;
            // 
            // PictureBox3
            // 
            PictureBox3.Dock = DockStyle.Fill;
            PictureBox3.Image = (Image)resources.GetObject("PictureBox3.Image");
            PictureBox3.Location = new Point(0, 0);
            PictureBox3.Margin = new Padding(0);
            PictureBox3.Name = "PictureBox3";
            PictureBox3.Padding = new Padding(9);
            PictureBox3.Size = new Size(50, 50);
            PictureBox3.SizeMode = PictureBoxSizeMode.StretchImage;
            PictureBox3.TabIndex = 23;
            PictureBox3.TabStop = false;
            // 
            // panel10
            // 
            panel10.AutoSize = true;
            panel10.Controls.Add(panel12);
            panel10.Controls.Add(pFolderTypeIcon);
            panel10.Dock = DockStyle.Top;
            panel10.Location = new Point(0, 50);
            panel10.Name = "panel10";
            panel10.Size = new Size(734, 50);
            panel10.TabIndex = 6;
            // 
            // panel12
            // 
            panel12.AutoSize = true;
            panel12.Controls.Add(panel7);
            panel12.Controls.Add(cboFolderType);
            panel12.Controls.Add(lblFolderType);
            panel12.Dock = DockStyle.Fill;
            panel12.Location = new Point(50, 0);
            panel12.Name = "panel12";
            panel12.Size = new Size(684, 50);
            panel12.TabIndex = 1;
            // 
            // panel7
            // 
            panel7.Dock = DockStyle.Top;
            panel7.Location = new Point(0, 38);
            panel7.Name = "panel7";
            panel7.Size = new Size(684, 12);
            panel7.TabIndex = 25;
            // 
            // cboFolderType
            // 
            cboFolderType.Dock = DockStyle.Top;
            cboFolderType.DropDownStyle = ComboBoxStyle.DropDownList;
            cboFolderType.FormattingEnabled = true;
            cboFolderType.Location = new Point(0, 15);
            cboFolderType.Margin = new Padding(2);
            cboFolderType.Name = "cboFolderType";
            cboFolderType.Size = new Size(684, 23);
            cboFolderType.Sorted = true;
            cboFolderType.TabIndex = 24;
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
            lblFolderType.TabIndex = 23;
            lblFolderType.Text = "Folder type";
            // 
            // pFolderTypeIcon
            // 
            pFolderTypeIcon.Controls.Add(pictureBox2);
            pFolderTypeIcon.Dock = DockStyle.Left;
            pFolderTypeIcon.Location = new Point(0, 0);
            pFolderTypeIcon.Name = "pFolderTypeIcon";
            pFolderTypeIcon.Size = new Size(50, 50);
            pFolderTypeIcon.TabIndex = 0;
            // 
            // pictureBox2
            // 
            pictureBox2.Dock = DockStyle.Fill;
            pictureBox2.Image = (Image)resources.GetObject("pictureBox2.Image");
            pictureBox2.Location = new Point(0, 0);
            pictureBox2.Margin = new Padding(0);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Padding = new Padding(9);
            pictureBox2.Size = new Size(50, 50);
            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox2.TabIndex = 22;
            pictureBox2.TabStop = false;
            // 
            // pName
            // 
            pName.AutoSize = true;
            pName.Controls.Add(panel9);
            pName.Controls.Add(pNameIcon);
            pName.Dock = DockStyle.Top;
            pName.Location = new Point(0, 0);
            pName.Name = "pName";
            pName.Size = new Size(734, 50);
            pName.TabIndex = 5;
            // 
            // panel9
            // 
            panel9.AutoSize = true;
            panel9.Controls.Add(panel2);
            panel9.Controls.Add(txtName);
            panel9.Controls.Add(lblName);
            panel9.Dock = DockStyle.Fill;
            panel9.Location = new Point(50, 0);
            panel9.Name = "panel9";
            panel9.Size = new Size(684, 50);
            panel9.TabIndex = 1;
            // 
            // panel2
            // 
            panel2.Dock = DockStyle.Top;
            panel2.Location = new Point(0, 38);
            panel2.Name = "panel2";
            panel2.Size = new Size(684, 12);
            panel2.TabIndex = 8;
            // 
            // txtName
            // 
            txtName.Dock = DockStyle.Top;
            txtName.Location = new Point(0, 15);
            txtName.Name = "txtName";
            txtName.Size = new Size(684, 23);
            txtName.TabIndex = 7;
            txtName.TextChanged += txtName_TextChanged;
            // 
            // lblName
            // 
            lblName.AutoSize = true;
            lblName.Dock = DockStyle.Top;
            lblName.Location = new Point(0, 0);
            lblName.Name = "lblName";
            lblName.Size = new Size(39, 15);
            lblName.TabIndex = 6;
            lblName.Text = "Name";
            // 
            // pNameIcon
            // 
            pNameIcon.Controls.Add(PictureBox1);
            pNameIcon.Dock = DockStyle.Left;
            pNameIcon.Location = new Point(0, 0);
            pNameIcon.Name = "pNameIcon";
            pNameIcon.Size = new Size(50, 50);
            pNameIcon.TabIndex = 0;
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
            PictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            PictureBox1.TabIndex = 21;
            PictureBox1.TabStop = false;
            // 
            // panel3
            // 
            panel3.Controls.Add(panel17);
            panel3.Dock = DockStyle.Fill;
            panel3.Location = new Point(743, 3);
            panel3.Name = "panel3";
            panel3.Size = new Size(734, 924);
            panel3.TabIndex = 6;
            // 
            // panel17
            // 
            panel17.AutoSize = true;
            panel17.Controls.Add(panel4);
            panel17.Controls.Add(pIdsIcon);
            panel17.Dock = DockStyle.Top;
            panel17.Location = new Point(0, 0);
            panel17.Name = "panel17";
            panel17.Size = new Size(734, 49);
            panel17.TabIndex = 6;
            // 
            // panel4
            // 
            panel4.AutoSize = true;
            panel4.Controls.Add(panel15);
            panel4.Controls.Add(panel6);
            panel4.Controls.Add(panel18);
            panel4.Controls.Add(panel19);
            panel4.Controls.Add(panel21);
            panel4.Dock = DockStyle.Fill;
            panel4.Location = new Point(50, 0);
            panel4.Margin = new Padding(0);
            panel4.Name = "panel4";
            panel4.Size = new Size(684, 49);
            panel4.TabIndex = 54;
            // 
            // panel15
            // 
            panel15.AutoSize = true;
            panel15.Controls.Add(panel24);
            panel15.Controls.Add(lnkParentPath);
            panel15.Controls.Add(lblFolderPath);
            panel15.Dock = DockStyle.Fill;
            panel15.Location = new Point(256, 0);
            panel15.Name = "panel15";
            panel15.Size = new Size(428, 49);
            panel15.TabIndex = 58;
            // 
            // panel24
            // 
            panel24.Dock = DockStyle.Top;
            panel24.Location = new Point(0, 31);
            panel24.Name = "panel24";
            panel24.Size = new Size(428, 18);
            panel24.TabIndex = 65;
            // 
            // lnkParentPath
            // 
            lnkParentPath.AutoSize = true;
            lnkParentPath.Dock = DockStyle.Top;
            lnkParentPath.Location = new Point(0, 15);
            lnkParentPath.Margin = new Padding(2, 0, 2, 0);
            lnkParentPath.Name = "lnkParentPath";
            lnkParentPath.Padding = new Padding(0, 1, 0, 0);
            lnkParentPath.Size = new Size(59, 16);
            lnkParentPath.TabIndex = 64;
            lnkParentPath.TabStop = true;
            lnkParentPath.Text = "/link/here";
            // 
            // lblFolderPath
            // 
            lblFolderPath.AutoSize = true;
            lblFolderPath.Dock = DockStyle.Top;
            lblFolderPath.Location = new Point(0, 0);
            lblFolderPath.Margin = new Padding(2, 0, 2, 0);
            lblFolderPath.Name = "lblFolderPath";
            lblFolderPath.Size = new Size(31, 15);
            lblFolderPath.TabIndex = 63;
            lblFolderPath.Text = "Path";
            // 
            // panel6
            // 
            panel6.Dock = DockStyle.Left;
            panel6.Location = new Point(243, 0);
            panel6.Margin = new Padding(0);
            panel6.Name = "panel6";
            panel6.Size = new Size(13, 49);
            panel6.TabIndex = 55;
            // 
            // panel18
            // 
            panel18.Controls.Add(txtParentId);
            panel18.Controls.Add(lblFolderId);
            panel18.Dock = DockStyle.Left;
            panel18.Location = new Point(128, 0);
            panel18.Margin = new Padding(0);
            panel18.Name = "panel18";
            panel18.Size = new Size(115, 49);
            panel18.TabIndex = 54;
            // 
            // txtParentId
            // 
            txtParentId.Dock = DockStyle.Fill;
            txtParentId.Location = new Point(0, 15);
            txtParentId.Margin = new Padding(2);
            txtParentId.Name = "txtParentId";
            txtParentId.ReadOnly = true;
            txtParentId.Size = new Size(115, 23);
            txtParentId.TabIndex = 47;
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
            lblFolderId.TabIndex = 46;
            lblFolderId.Text = "Parent ID";
            // 
            // panel19
            // 
            panel19.Dock = DockStyle.Left;
            panel19.Location = new Point(115, 0);
            panel19.Margin = new Padding(0);
            panel19.Name = "panel19";
            panel19.Size = new Size(13, 49);
            panel19.TabIndex = 53;
            // 
            // panel21
            // 
            panel21.Controls.Add(txtId);
            panel21.Controls.Add(lblId);
            panel21.Dock = DockStyle.Left;
            panel21.Location = new Point(0, 0);
            panel21.Margin = new Padding(0);
            panel21.Name = "panel21";
            panel21.Size = new Size(115, 49);
            panel21.TabIndex = 52;
            // 
            // txtId
            // 
            txtId.Dock = DockStyle.Fill;
            txtId.Location = new Point(0, 15);
            txtId.Margin = new Padding(2);
            txtId.Name = "txtId";
            txtId.ReadOnly = true;
            txtId.Size = new Size(115, 23);
            txtId.TabIndex = 47;
            // 
            // lblId
            // 
            lblId.AutoSize = true;
            lblId.Dock = DockStyle.Top;
            lblId.Location = new Point(0, 0);
            lblId.Margin = new Padding(2, 0, 2, 0);
            lblId.Name = "lblId";
            lblId.Size = new Size(18, 15);
            lblId.TabIndex = 46;
            lblId.Text = "ID";
            // 
            // pIdsIcon
            // 
            pIdsIcon.Controls.Add(pictureBox12);
            pIdsIcon.Dock = DockStyle.Left;
            pIdsIcon.Location = new Point(0, 0);
            pIdsIcon.Name = "pIdsIcon";
            pIdsIcon.Size = new Size(50, 49);
            pIdsIcon.TabIndex = 0;
            // 
            // pictureBox12
            // 
            pictureBox12.Dock = DockStyle.Fill;
            pictureBox12.Image = (Image)resources.GetObject("pictureBox12.Image");
            pictureBox12.Location = new Point(0, 0);
            pictureBox12.Margin = new Padding(0);
            pictureBox12.Name = "pictureBox12";
            pictureBox12.Padding = new Padding(9);
            pictureBox12.Size = new Size(50, 49);
            pictureBox12.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox12.TabIndex = 9;
            pictureBox12.TabStop = false;
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
            Paint += FolderPropertiesTabControl_Paint;
            panel20.ResumeLayout(false);
            tableLayoutPanel1.ResumeLayout(false);
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panel14.ResumeLayout(false);
            panel14.PerformLayout();
            panel16.ResumeLayout(false);
            panel16.PerformLayout();
            pOwnerIcon.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)PictureBox3).EndInit();
            panel10.ResumeLayout(false);
            panel10.PerformLayout();
            panel12.ResumeLayout(false);
            panel12.PerformLayout();
            pFolderTypeIcon.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            pName.ResumeLayout(false);
            pName.PerformLayout();
            panel9.ResumeLayout(false);
            panel9.PerformLayout();
            pNameIcon.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)PictureBox1).EndInit();
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            panel17.ResumeLayout(false);
            panel17.PerformLayout();
            panel4.ResumeLayout(false);
            panel4.PerformLayout();
            panel15.ResumeLayout(false);
            panel15.PerformLayout();
            panel18.ResumeLayout(false);
            panel18.PerformLayout();
            panel21.ResumeLayout(false);
            panel21.PerformLayout();
            pIdsIcon.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox12).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Panel panel20;
		private TableLayoutPanel tableLayoutPanel1;
		internal Button cmdSave;
        private Panel panel1;
        private Panel pName;
        private Panel panel9;
        private Panel panel2;
        internal TextBox txtName;
        internal Label lblName;
        private Panel pNameIcon;
        internal PictureBox PictureBox1;
        private Panel panel10;
        private Panel panel12;
        private Panel pFolderTypeIcon;
        internal PictureBox pictureBox2;
        private Panel panel14;
        private Panel panel16;
        private Panel panel8;
        internal ComboBox cboOwner;
        internal Label lblOwner;
        private Panel pOwnerIcon;
        internal PictureBox PictureBox3;
        internal Label lblFolderType;
        private Panel panel7;
        internal ComboBox cboFolderType;
        private Panel panel3;
        private Panel panel17;
        internal Panel panel4;
        private Panel panel15;
        private Panel panel24;
        internal LinkLabel lnkParentPath;
        internal Label lblFolderPath;
        internal Panel panel6;
        internal Panel panel18;
        internal TextBox txtParentId;
        internal Label lblFolderId;
        internal Panel panel19;
        internal Panel panel21;
        internal TextBox txtId;
        internal Label lblId;
        private Panel pIdsIcon;
        internal PictureBox pictureBox12;
    }
}
