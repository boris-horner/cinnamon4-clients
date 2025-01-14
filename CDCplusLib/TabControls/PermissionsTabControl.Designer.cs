
namespace CDCplusLib.TabControls
{
    partial class PermissionsTabControl
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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PermissionsTabControl));
            panel20 = new Panel();
            cmdSave = new Button();
            imlPermissions = new ImageList(components);
            ttPermissions = new ToolTip(components);
            lblAcl = new Label();
            cboAcl = new ComboBox();
            Panel2 = new Panel();
            lblAclEntries = new Label();
            lvwAclEntries = new ListView();
            colName = new ColumnHeader();
            colType = new ColumnHeader();
            colPermissions = new ColumnHeader();
            Panel3 = new Panel();
            lblCurrentPermissions = new Label();
            lvwCurrentPermissions = new ListView();
            colMyPermissions = new ColumnHeader();
            gbAcl = new GroupBox();
            panel20.SuspendLayout();
            gbAcl.SuspendLayout();
            SuspendLayout();
            // 
            // panel20
            // 
            panel20.Controls.Add(cmdSave);
            panel20.Dock = DockStyle.Bottom;
            panel20.Location = new Point(0, 431);
            panel20.Name = "panel20";
            panel20.Size = new Size(718, 46);
            panel20.TabIndex = 1;
            // 
            // cmdSave
            // 
            cmdSave.Dock = DockStyle.Left;
            cmdSave.Image = (Image)resources.GetObject("cmdSave.Image");
            cmdSave.Location = new Point(0, 0);
            cmdSave.Name = "cmdSave";
            cmdSave.Size = new Size(46, 46);
            cmdSave.TabIndex = 3;
            cmdSave.UseVisualStyleBackColor = true;
            cmdSave.Click += cmdSave_Click;
            // 
            // imlPermissions
            // 
            imlPermissions.ColorDepth = ColorDepth.Depth8Bit;
            imlPermissions.ImageStream = (ImageListStreamer)resources.GetObject("imlPermissions.ImageStream");
            imlPermissions.TransparentColor = Color.Transparent;
            imlPermissions.Images.SetKeyName(0, "active");
            imlPermissions.Images.SetKeyName(1, "inactive");
            // 
            // lblAcl
            // 
            lblAcl.AutoSize = true;
            lblAcl.Dock = DockStyle.Top;
            lblAcl.Location = new Point(7, 23);
            lblAcl.Margin = new Padding(4, 0, 4, 0);
            lblAcl.Name = "lblAcl";
            lblAcl.Size = new Size(62, 15);
            lblAcl.TabIndex = 15;
            lblAcl.Text = "ACL name";
            // 
            // cboAcl
            // 
            cboAcl.Dock = DockStyle.Top;
            cboAcl.DropDownStyle = ComboBoxStyle.DropDownList;
            cboAcl.FormattingEnabled = true;
            cboAcl.Location = new Point(7, 38);
            cboAcl.Margin = new Padding(4);
            cboAcl.Name = "cboAcl";
            cboAcl.Size = new Size(704, 23);
            cboAcl.Sorted = true;
            cboAcl.TabIndex = 16;
            cboAcl.SelectedIndexChanged += cboAcl_SelectedIndexChanged;
            // 
            // Panel2
            // 
            Panel2.Dock = DockStyle.Top;
            Panel2.Location = new Point(7, 61);
            Panel2.Margin = new Padding(4);
            Panel2.Name = "Panel2";
            Panel2.Size = new Size(704, 11);
            Panel2.TabIndex = 17;
            // 
            // lblAclEntries
            // 
            lblAclEntries.AutoSize = true;
            lblAclEntries.Dock = DockStyle.Top;
            lblAclEntries.Location = new Point(7, 72);
            lblAclEntries.Margin = new Padding(4, 0, 4, 0);
            lblAclEntries.Name = "lblAclEntries";
            lblAclEntries.Size = new Size(67, 15);
            lblAclEntries.TabIndex = 18;
            lblAclEntries.Text = "ACL entries";
            // 
            // lvwAclEntries
            // 
            lvwAclEntries.Columns.AddRange(new ColumnHeader[] { colName, colType, colPermissions });
            lvwAclEntries.Dock = DockStyle.Top;
            lvwAclEntries.Location = new Point(7, 87);
            lvwAclEntries.Margin = new Padding(4);
            lvwAclEntries.Name = "lvwAclEntries";
            lvwAclEntries.Size = new Size(704, 111);
            lvwAclEntries.TabIndex = 19;
            lvwAclEntries.UseCompatibleStateImageBehavior = false;
            lvwAclEntries.View = View.Details;
            // 
            // colName
            // 
            colName.Width = 150;
            // 
            // colType
            // 
            colType.Width = 120;
            // 
            // colPermissions
            // 
            colPermissions.Width = 800;
            // 
            // Panel3
            // 
            Panel3.Dock = DockStyle.Top;
            Panel3.Location = new Point(7, 198);
            Panel3.Margin = new Padding(4);
            Panel3.Name = "Panel3";
            Panel3.Size = new Size(704, 11);
            Panel3.TabIndex = 23;
            // 
            // lblCurrentPermissions
            // 
            lblCurrentPermissions.AutoSize = true;
            lblCurrentPermissions.Dock = DockStyle.Top;
            lblCurrentPermissions.Location = new Point(7, 209);
            lblCurrentPermissions.Margin = new Padding(4, 0, 4, 0);
            lblCurrentPermissions.Name = "lblCurrentPermissions";
            lblCurrentPermissions.Size = new Size(113, 15);
            lblCurrentPermissions.TabIndex = 26;
            lblCurrentPermissions.Text = "Current permissions";
            // 
            // lvwCurrentPermissions
            // 
            lvwCurrentPermissions.Columns.AddRange(new ColumnHeader[] { colMyPermissions });
            lvwCurrentPermissions.Dock = DockStyle.Fill;
            lvwCurrentPermissions.Location = new Point(7, 224);
            lvwCurrentPermissions.Margin = new Padding(4);
            lvwCurrentPermissions.Name = "lvwCurrentPermissions";
            lvwCurrentPermissions.Size = new Size(704, 200);
            lvwCurrentPermissions.SmallImageList = imlPermissions;
            lvwCurrentPermissions.TabIndex = 27;
            lvwCurrentPermissions.UseCompatibleStateImageBehavior = false;
            lvwCurrentPermissions.View = View.Details;
            // 
            // colMyPermissions
            // 
            colMyPermissions.Text = "Permissions";
            colMyPermissions.Width = 564;
            // 
            // gbAcl
            // 
            gbAcl.Controls.Add(lvwCurrentPermissions);
            gbAcl.Controls.Add(lblCurrentPermissions);
            gbAcl.Controls.Add(Panel3);
            gbAcl.Controls.Add(lvwAclEntries);
            gbAcl.Controls.Add(lblAclEntries);
            gbAcl.Controls.Add(Panel2);
            gbAcl.Controls.Add(cboAcl);
            gbAcl.Controls.Add(lblAcl);
            gbAcl.Dock = DockStyle.Fill;
            gbAcl.Location = new Point(0, 0);
            gbAcl.Margin = new Padding(4);
            gbAcl.Name = "gbAcl";
            gbAcl.Padding = new Padding(7, 7, 7, 7);
            gbAcl.Size = new Size(718, 431);
            gbAcl.TabIndex = 3;
            gbAcl.TabStop = false;
            gbAcl.Text = "GroupBox1";
            // 
            // PermissionsTabControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(gbAcl);
            Controls.Add(panel20);
            Name = "PermissionsTabControl";
            Size = new Size(718, 477);
            panel20.ResumeLayout(false);
            gbAcl.ResumeLayout(false);
            gbAcl.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel panel20;
        internal System.Windows.Forms.Button cmdSave;
        private System.Windows.Forms.ToolTip ttPermissions;
        private System.Windows.Forms.ImageList imlPermissions;
        internal System.Windows.Forms.Label lblAcl;
        internal System.Windows.Forms.ComboBox cboAcl;
        internal System.Windows.Forms.Panel Panel2;
        internal System.Windows.Forms.Label lblAclEntries;
        internal System.Windows.Forms.ListView lvwAclEntries;
        internal System.Windows.Forms.ColumnHeader colName;
        internal System.Windows.Forms.ColumnHeader colType;
        internal System.Windows.Forms.ColumnHeader colPermissions;
        internal System.Windows.Forms.Panel Panel3;
        internal System.Windows.Forms.Label lblCurrentPermissions;
        internal System.Windows.Forms.ListView lvwCurrentPermissions;
        internal System.Windows.Forms.ColumnHeader colMyPermissions;
        internal System.Windows.Forms.GroupBox gbAcl;
    }
}
