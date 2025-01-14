
namespace CDCplusLib.TabControls
{
	partial class EditRawMetadataTabControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditRawMetadataTabControl));
            panel20 = new Panel();
            cmdSave = new Button();
            imlPermissions = new ImageList(components);
            ttMetadata = new ToolTip(components);
            splitContainer1 = new SplitContainer();
            tvwMetasets = new TreeView();
            xtbMetadata = new C4GeneralGui.GuiElements.XmlTextBox();
            panel20.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            SuspendLayout();
            // 
            // panel20
            // 
            panel20.Controls.Add(cmdSave);
            panel20.Dock = DockStyle.Bottom;
            panel20.Location = new Point(0, 770);
            panel20.Name = "panel20";
            panel20.Size = new Size(1379, 46);
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
            // splitContainer1
            // 
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.Location = new Point(0, 0);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(tvwMetasets);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(xtbMetadata);
            splitContainer1.Size = new Size(1379, 770);
            splitContainer1.SplitterDistance = 313;
            splitContainer1.TabIndex = 2;
            // 
            // tvwMetasets
            // 
            tvwMetasets.Dock = DockStyle.Fill;
            tvwMetasets.Location = new Point(0, 0);
            tvwMetasets.Name = "tvwMetasets";
            tvwMetasets.Size = new Size(313, 770);
            tvwMetasets.TabIndex = 0;
            tvwMetasets.BeforeSelect += tvwMetasets_BeforeSelect;
            tvwMetasets.AfterSelect += tvwMetasets_AfterSelect;
            tvwMetasets.NodeMouseClick += tvwMetasets_NodeMouseClick;
            tvwMetasets.MouseDown += tvwMetasets_MouseDown;
            // 
            // xtbMetadata
            // 
            xtbMetadata.BackColor = Color.White;
            xtbMetadata.Dock = DockStyle.Fill;
            xtbMetadata.Font = new Font("Courier New", 9F);
            xtbMetadata.Location = new Point(0, 0);
            xtbMetadata.Name = "xtbMetadata";
            xtbMetadata.Size = new Size(1062, 770);
            xtbMetadata.TabIndex = 3;
            xtbMetadata.Text = "";
            xtbMetadata.TextChanged += xtbMetadata_TextChanged;
            // 
            // EditRawMetadataTabControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(splitContainer1);
            Controls.Add(panel20);
            Name = "EditRawMetadataTabControl";
            Size = new Size(1379, 816);
            panel20.ResumeLayout(false);
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel panel20;
		internal Button cmdSave;
		private ToolTip ttMetadata;
		private ImageList imlPermissions;
		private SplitContainer splitContainer1;
		private TreeView tvwMetasets;
		private C4GeneralGui.GuiElements.XmlTextBox xtbMetadata;
	}
}
