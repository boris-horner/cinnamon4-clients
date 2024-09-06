
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PermissionsTabControl));
            this.panel20 = new System.Windows.Forms.Panel();
            this.cmdSave = new System.Windows.Forms.Button();
            this.imlPermissions = new System.Windows.Forms.ImageList(this.components);
            this.ttPermissions = new System.Windows.Forms.ToolTip(this.components);
            this.lblAcl = new System.Windows.Forms.Label();
            this.cboAcl = new System.Windows.Forms.ComboBox();
            this.Panel2 = new System.Windows.Forms.Panel();
            this.lblAclEntries = new System.Windows.Forms.Label();
            this.lvwAclEntries = new System.Windows.Forms.ListView();
            this.colName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colPermissions = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Panel3 = new System.Windows.Forms.Panel();
            this.lblCurrentPermissions = new System.Windows.Forms.Label();
            this.lvwCurrentPermissions = new System.Windows.Forms.ListView();
            this.colMyPermissions = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.gbAcl = new System.Windows.Forms.GroupBox();
            this.panel20.SuspendLayout();
            this.gbAcl.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel20
            // 
            this.panel20.Controls.Add(this.cmdSave);
            this.panel20.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel20.Location = new System.Drawing.Point(0, 460);
            this.panel20.Name = "panel20";
            this.panel20.Size = new System.Drawing.Size(821, 49);
            this.panel20.TabIndex = 1;
            // 
            // cmdSave
            // 
            this.cmdSave.Dock = System.Windows.Forms.DockStyle.Left;
            this.cmdSave.Image = ((System.Drawing.Image)(resources.GetObject("cmdSave.Image")));
            this.cmdSave.Location = new System.Drawing.Point(0, 0);
            this.cmdSave.Name = "cmdSave";
            this.cmdSave.Size = new System.Drawing.Size(53, 49);
            this.cmdSave.TabIndex = 3;
            this.cmdSave.UseVisualStyleBackColor = true;
            this.cmdSave.Click += new System.EventHandler(this.cmdSave_Click);
            // 
            // imlPermissions
            // 
            this.imlPermissions.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imlPermissions.ImageStream")));
            this.imlPermissions.TransparentColor = System.Drawing.Color.Transparent;
            this.imlPermissions.Images.SetKeyName(0, "active");
            this.imlPermissions.Images.SetKeyName(1, "inactive");
            // 
            // lblAcl
            // 
            this.lblAcl.AutoSize = true;
            this.lblAcl.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblAcl.Location = new System.Drawing.Point(8, 22);
            this.lblAcl.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblAcl.Name = "lblAcl";
            this.lblAcl.Size = new System.Drawing.Size(73, 17);
            this.lblAcl.TabIndex = 15;
            this.lblAcl.Text = "ACL name";
            // 
            // cboAcl
            // 
            this.cboAcl.Dock = System.Windows.Forms.DockStyle.Top;
            this.cboAcl.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboAcl.FormattingEnabled = true;
            this.cboAcl.Location = new System.Drawing.Point(8, 39);
            this.cboAcl.Margin = new System.Windows.Forms.Padding(4);
            this.cboAcl.Name = "cboAcl";
            this.cboAcl.Size = new System.Drawing.Size(805, 24);
            this.cboAcl.Sorted = true;
            this.cboAcl.TabIndex = 16;
            this.cboAcl.SelectedIndexChanged += new System.EventHandler(this.cboAcl_SelectedIndexChanged);
            // 
            // Panel2
            // 
            this.Panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.Panel2.Location = new System.Drawing.Point(8, 63);
            this.Panel2.Margin = new System.Windows.Forms.Padding(4);
            this.Panel2.Name = "Panel2";
            this.Panel2.Size = new System.Drawing.Size(805, 12);
            this.Panel2.TabIndex = 17;
            // 
            // lblAclEntries
            // 
            this.lblAclEntries.AutoSize = true;
            this.lblAclEntries.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblAclEntries.Location = new System.Drawing.Point(8, 75);
            this.lblAclEntries.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblAclEntries.Name = "lblAclEntries";
            this.lblAclEntries.Size = new System.Drawing.Size(81, 17);
            this.lblAclEntries.TabIndex = 18;
            this.lblAclEntries.Text = "ACL entries";
            // 
            // lvwAclEntries
            // 
            this.lvwAclEntries.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colName,
            this.colType,
            this.colPermissions});
            this.lvwAclEntries.Dock = System.Windows.Forms.DockStyle.Top;
            this.lvwAclEntries.Location = new System.Drawing.Point(8, 92);
            this.lvwAclEntries.Margin = new System.Windows.Forms.Padding(4);
            this.lvwAclEntries.Name = "lvwAclEntries";
            this.lvwAclEntries.Size = new System.Drawing.Size(805, 118);
            this.lvwAclEntries.TabIndex = 19;
            this.lvwAclEntries.UseCompatibleStateImageBehavior = false;
            this.lvwAclEntries.View = System.Windows.Forms.View.Details;
            // 
            // colName
            // 
            this.colName.Width = 94;
            // 
            // colPermissions
            // 
            this.colPermissions.Width = 454;
            // 
            // Panel3
            // 
            this.Panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.Panel3.Location = new System.Drawing.Point(8, 210);
            this.Panel3.Margin = new System.Windows.Forms.Padding(4);
            this.Panel3.Name = "Panel3";
            this.Panel3.Size = new System.Drawing.Size(805, 12);
            this.Panel3.TabIndex = 23;
            // 
            // lblCurrentPermissions
            // 
            this.lblCurrentPermissions.AutoSize = true;
            this.lblCurrentPermissions.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblCurrentPermissions.Location = new System.Drawing.Point(8, 222);
            this.lblCurrentPermissions.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCurrentPermissions.Name = "lblCurrentPermissions";
            this.lblCurrentPermissions.Size = new System.Drawing.Size(134, 17);
            this.lblCurrentPermissions.TabIndex = 26;
            this.lblCurrentPermissions.Text = "Current permissions";
            // 
            // lvwCurrentPermissions
            // 
            this.lvwCurrentPermissions.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colMyPermissions});
            this.lvwCurrentPermissions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvwCurrentPermissions.Location = new System.Drawing.Point(8, 239);
            this.lvwCurrentPermissions.Margin = new System.Windows.Forms.Padding(4);
            this.lvwCurrentPermissions.Name = "lvwCurrentPermissions";
            this.lvwCurrentPermissions.Size = new System.Drawing.Size(805, 214);
            this.lvwCurrentPermissions.SmallImageList = this.imlPermissions;
            this.lvwCurrentPermissions.TabIndex = 27;
            this.lvwCurrentPermissions.UseCompatibleStateImageBehavior = false;
            this.lvwCurrentPermissions.View = System.Windows.Forms.View.Details;
            // 
            // colMyPermissions
            // 
            this.colMyPermissions.Text = "Permissions";
            this.colMyPermissions.Width = 564;
            // 
            // gbAcl
            // 
            this.gbAcl.Controls.Add(this.lvwCurrentPermissions);
            this.gbAcl.Controls.Add(this.lblCurrentPermissions);
            this.gbAcl.Controls.Add(this.Panel3);
            this.gbAcl.Controls.Add(this.lvwAclEntries);
            this.gbAcl.Controls.Add(this.lblAclEntries);
            this.gbAcl.Controls.Add(this.Panel2);
            this.gbAcl.Controls.Add(this.cboAcl);
            this.gbAcl.Controls.Add(this.lblAcl);
            this.gbAcl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbAcl.Location = new System.Drawing.Point(0, 0);
            this.gbAcl.Margin = new System.Windows.Forms.Padding(4);
            this.gbAcl.Name = "gbAcl";
            this.gbAcl.Padding = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.gbAcl.Size = new System.Drawing.Size(821, 460);
            this.gbAcl.TabIndex = 3;
            this.gbAcl.TabStop = false;
            this.gbAcl.Text = "GroupBox1";
            // 
            // PermissionsTabControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gbAcl);
            this.Controls.Add(this.panel20);
            this.Name = "PermissionsTabControl";
            this.Size = new System.Drawing.Size(821, 509);
            this.panel20.ResumeLayout(false);
            this.gbAcl.ResumeLayout(false);
            this.gbAcl.PerformLayout();
            this.ResumeLayout(false);

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
