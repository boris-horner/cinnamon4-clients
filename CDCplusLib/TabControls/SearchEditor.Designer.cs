namespace CDCplusLib.TabControls
{
    partial class SearchEditor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SearchEditor));
            optSearchFolders = new RadioButton();
            optSearchObjects = new RadioButton();
            optVersionAll = new RadioButton();
            optVersionLatestBranches = new RadioButton();
            optVersionLatest = new RadioButton();
            Panel2 = new Panel();
            tvwSearchDef = new TreeView();
            imlSmallIcons = new ImageList(components);
            splSearch = new SplitContainer();
            Panel4 = new Panel();
            Panel5 = new Panel();
            pTermEditorContainer = new Panel();
            tlpTerm = new TableLayoutPanel();
            panel8 = new Panel();
            chkAnyTerm = new CheckBox();
            chkNotTerm = new CheckBox();
            panel7 = new Panel();
            vtxtVariableName = new C4GeneralGui.GuiElements.ValidatingTextBox();
            chkVariable = new CheckBox();
            Panel3 = new Panel();
            cboIndexItem = new ComboBox();
            lblFieldName = new Label();
            ttSearch = new ToolTip(components);
            cmdRunQuery = new Button();
            cmdSave = new Button();
            Panel1 = new Panel();
            cmdShowQuery = new Button();
            Panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splSearch).BeginInit();
            splSearch.Panel1.SuspendLayout();
            splSearch.Panel2.SuspendLayout();
            splSearch.SuspendLayout();
            Panel4.SuspendLayout();
            Panel5.SuspendLayout();
            tlpTerm.SuspendLayout();
            panel8.SuspendLayout();
            panel7.SuspendLayout();
            Panel3.SuspendLayout();
            Panel1.SuspendLayout();
            SuspendLayout();
            // 
            // optSearchFolders
            // 
            optSearchFolders.Appearance = Appearance.Button;
            optSearchFolders.AutoSize = true;
            optSearchFolders.Dock = DockStyle.Left;
            optSearchFolders.Image = (Image)resources.GetObject("optSearchFolders.Image");
            optSearchFolders.Location = new Point(38, 0);
            optSearchFolders.Margin = new Padding(4, 3, 4, 3);
            optSearchFolders.Name = "optSearchFolders";
            optSearchFolders.Size = new Size(38, 46);
            optSearchFolders.TabIndex = 1;
            optSearchFolders.UseVisualStyleBackColor = true;
            optSearchFolders.CheckedChanged += OptSearchFolders_CheckedChanged;
            // 
            // optSearchObjects
            // 
            optSearchObjects.Appearance = Appearance.Button;
            optSearchObjects.AutoSize = true;
            optSearchObjects.Checked = true;
            optSearchObjects.Dock = DockStyle.Left;
            optSearchObjects.Image = (Image)resources.GetObject("optSearchObjects.Image");
            optSearchObjects.Location = new Point(0, 0);
            optSearchObjects.Margin = new Padding(4, 3, 4, 3);
            optSearchObjects.Name = "optSearchObjects";
            optSearchObjects.Size = new Size(38, 46);
            optSearchObjects.TabIndex = 0;
            optSearchObjects.TabStop = true;
            optSearchObjects.UseVisualStyleBackColor = true;
            optSearchObjects.CheckedChanged += OptSearchObjects_CheckedChanged;
            // 
            // optVersionAll
            // 
            optVersionAll.Appearance = Appearance.Button;
            optVersionAll.AutoSize = true;
            optVersionAll.Dock = DockStyle.Left;
            optVersionAll.Image = (Image)resources.GetObject("optVersionAll.Image");
            optVersionAll.Location = new Point(76, 0);
            optVersionAll.Margin = new Padding(4, 3, 4, 3);
            optVersionAll.Name = "optVersionAll";
            optVersionAll.Size = new Size(38, 46);
            optVersionAll.TabIndex = 2;
            optVersionAll.UseVisualStyleBackColor = true;
            optVersionAll.CheckedChanged += OptVersionAll_CheckedChanged;
            // 
            // optVersionLatestBranches
            // 
            optVersionLatestBranches.Appearance = Appearance.Button;
            optVersionLatestBranches.AutoSize = true;
            optVersionLatestBranches.Dock = DockStyle.Left;
            optVersionLatestBranches.Image = (Image)resources.GetObject("optVersionLatestBranches.Image");
            optVersionLatestBranches.Location = new Point(38, 0);
            optVersionLatestBranches.Margin = new Padding(4, 3, 4, 3);
            optVersionLatestBranches.Name = "optVersionLatestBranches";
            optVersionLatestBranches.Size = new Size(38, 46);
            optVersionLatestBranches.TabIndex = 1;
            optVersionLatestBranches.UseVisualStyleBackColor = true;
            optVersionLatestBranches.CheckedChanged += OptVersionLatestBranches_CheckedChanged;
            // 
            // optVersionLatest
            // 
            optVersionLatest.Appearance = Appearance.Button;
            optVersionLatest.AutoSize = true;
            optVersionLatest.Checked = true;
            optVersionLatest.Dock = DockStyle.Left;
            optVersionLatest.Image = (Image)resources.GetObject("optVersionLatest.Image");
            optVersionLatest.Location = new Point(0, 0);
            optVersionLatest.Margin = new Padding(4, 3, 4, 3);
            optVersionLatest.Name = "optVersionLatest";
            optVersionLatest.Size = new Size(38, 46);
            optVersionLatest.TabIndex = 0;
            optVersionLatest.TabStop = true;
            optVersionLatest.UseVisualStyleBackColor = true;
            optVersionLatest.CheckedChanged += OptVersionLatest_CheckedChanged;
            // 
            // Panel2
            // 
            Panel2.Controls.Add(optVersionAll);
            Panel2.Controls.Add(optVersionLatestBranches);
            Panel2.Controls.Add(optVersionLatest);
            Panel2.Dock = DockStyle.Left;
            Panel2.Location = new Point(108, 0);
            Panel2.Margin = new Padding(4, 3, 4, 3);
            Panel2.Name = "Panel2";
            Panel2.Size = new Size(222, 46);
            Panel2.TabIndex = 1;
            // 
            // tvwSearchDef
            // 
            tvwSearchDef.Dock = DockStyle.Fill;
            tvwSearchDef.HideSelection = false;
            tvwSearchDef.ImageIndex = 0;
            tvwSearchDef.ImageList = imlSmallIcons;
            tvwSearchDef.Location = new Point(0, 46);
            tvwSearchDef.Margin = new Padding(4, 3, 4, 3);
            tvwSearchDef.Name = "tvwSearchDef";
            tvwSearchDef.SelectedImageIndex = 0;
            tvwSearchDef.Size = new Size(399, 470);
            tvwSearchDef.TabIndex = 1;
            tvwSearchDef.AfterSelect += TvwSearchDef_AfterSelect;
            tvwSearchDef.MouseUp += TvwSearchDef_MouseUp;
            // 
            // imlSmallIcons
            // 
            imlSmallIcons.ColorDepth = ColorDepth.Depth24Bit;
            imlSmallIcons.ImageStream = (ImageListStreamer)resources.GetObject("imlSmallIcons.ImageStream");
            imlSmallIcons.TransparentColor = Color.Transparent;
            imlSmallIcons.Images.SetKeyName(0, "flag-red.png");
            imlSmallIcons.Images.SetKeyName(1, "flag-green.png");
            imlSmallIcons.Images.SetKeyName(2, "flag-black.png");
            imlSmallIcons.Images.SetKeyName(3, "flag-yellow.png");
            imlSmallIcons.Images.SetKeyName(4, "folder-red.png");
            imlSmallIcons.Images.SetKeyName(5, "folder-green.png");
            imlSmallIcons.Images.SetKeyName(6, "folder-black.png");
            imlSmallIcons.Images.SetKeyName(7, "folder-yellow.png");
            // 
            // splSearch
            // 
            splSearch.Dock = DockStyle.Fill;
            splSearch.Location = new Point(0, 0);
            splSearch.Margin = new Padding(4, 3, 4, 3);
            splSearch.Name = "splSearch";
            // 
            // splSearch.Panel1
            // 
            splSearch.Panel1.Controls.Add(tvwSearchDef);
            splSearch.Panel1.Controls.Add(Panel4);
            // 
            // splSearch.Panel2
            // 
            splSearch.Panel2.Controls.Add(pTermEditorContainer);
            splSearch.Panel2.Controls.Add(tlpTerm);
            splSearch.Size = new Size(1102, 516);
            splSearch.SplitterDistance = 399;
            splSearch.SplitterWidth = 5;
            splSearch.TabIndex = 4;
            // 
            // Panel4
            // 
            Panel4.Controls.Add(Panel2);
            Panel4.Controls.Add(Panel5);
            Panel4.Dock = DockStyle.Top;
            Panel4.Location = new Point(0, 0);
            Panel4.Margin = new Padding(4, 3, 4, 3);
            Panel4.Name = "Panel4";
            Panel4.Size = new Size(399, 46);
            Panel4.TabIndex = 0;
            // 
            // Panel5
            // 
            Panel5.Controls.Add(optSearchFolders);
            Panel5.Controls.Add(optSearchObjects);
            Panel5.Dock = DockStyle.Left;
            Panel5.Location = new Point(0, 0);
            Panel5.Margin = new Padding(4, 3, 4, 3);
            Panel5.Name = "Panel5";
            Panel5.Size = new Size(108, 46);
            Panel5.TabIndex = 0;
            // 
            // pTermEditorContainer
            // 
            pTermEditorContainer.Dock = DockStyle.Fill;
            pTermEditorContainer.Location = new Point(0, 62);
            pTermEditorContainer.Margin = new Padding(4, 3, 4, 3);
            pTermEditorContainer.Name = "pTermEditorContainer";
            pTermEditorContainer.Size = new Size(698, 454);
            pTermEditorContainer.TabIndex = 2;
            // 
            // tlpTerm
            // 
            tlpTerm.ColumnCount = 3;
            tlpTerm.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 131F));
            tlpTerm.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlpTerm.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlpTerm.Controls.Add(panel8, 0, 0);
            tlpTerm.Controls.Add(panel7, 1, 0);
            tlpTerm.Controls.Add(Panel3, 1, 0);
            tlpTerm.Dock = DockStyle.Top;
            tlpTerm.Location = new Point(0, 0);
            tlpTerm.Margin = new Padding(4, 3, 4, 3);
            tlpTerm.Name = "tlpTerm";
            tlpTerm.RowCount = 1;
            tlpTerm.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpTerm.Size = new Size(698, 62);
            tlpTerm.TabIndex = 0;
            // 
            // panel8
            // 
            panel8.Controls.Add(chkAnyTerm);
            panel8.Controls.Add(chkNotTerm);
            panel8.Dock = DockStyle.Fill;
            panel8.Location = new Point(4, 3);
            panel8.Margin = new Padding(4, 3, 4, 3);
            panel8.Name = "panel8";
            panel8.Padding = new Padding(4, 3, 4, 3);
            panel8.Size = new Size(123, 56);
            panel8.TabIndex = 4;
            // 
            // chkAnyTerm
            // 
            chkAnyTerm.AutoSize = true;
            chkAnyTerm.Dock = DockStyle.Top;
            chkAnyTerm.Location = new Point(4, 22);
            chkAnyTerm.Margin = new Padding(4, 3, 4, 3);
            chkAnyTerm.Name = "chkAnyTerm";
            chkAnyTerm.Padding = new Padding(0, 3, 0, 0);
            chkAnyTerm.Size = new Size(115, 22);
            chkAnyTerm.TabIndex = 1;
            chkAnyTerm.Text = "ANY";
            chkAnyTerm.UseVisualStyleBackColor = true;
            chkAnyTerm.CheckedChanged += ChkAnyTerm_CheckedChanged;
            // 
            // chkNotTerm
            // 
            chkNotTerm.AutoSize = true;
            chkNotTerm.Dock = DockStyle.Top;
            chkNotTerm.Location = new Point(4, 3);
            chkNotTerm.Margin = new Padding(4, 3, 4, 3);
            chkNotTerm.Name = "chkNotTerm";
            chkNotTerm.Size = new Size(115, 19);
            chkNotTerm.TabIndex = 0;
            chkNotTerm.Text = "NOT";
            chkNotTerm.UseVisualStyleBackColor = true;
            chkNotTerm.CheckedChanged += ChkNotTerm_CheckedChanged;
            // 
            // panel7
            // 
            panel7.Controls.Add(vtxtVariableName);
            panel7.Controls.Add(chkVariable);
            panel7.Dock = DockStyle.Fill;
            panel7.Location = new Point(418, 3);
            panel7.Margin = new Padding(4, 3, 4, 3);
            panel7.Name = "panel7";
            panel7.Padding = new Padding(4, 3, 4, 3);
            panel7.Size = new Size(276, 56);
            panel7.TabIndex = 3;
            // 
            // vtxtVariableName
            // 
            vtxtVariableName.BackColor = SystemColors.Control;
            vtxtVariableName.Dock = DockStyle.Top;
            vtxtVariableName.ExternalInvalidColor = Color.Yellow;
            vtxtVariableName.ExternalValidation = true;
            vtxtVariableName.InternalInvalidColor = Color.Yellow;
            vtxtVariableName.Location = new Point(4, 22);
            vtxtVariableName.Margin = new Padding(4, 3, 4, 3);
            vtxtVariableName.Name = "vtxtVariableName";
            vtxtVariableName.ReadOnly = true;
            vtxtVariableName.RegularExpression = "";
            vtxtVariableName.Size = new Size(268, 23);
            vtxtVariableName.TabIndex = 1;
            vtxtVariableName.ValidColor = Color.White;
            vtxtVariableName.TextChanged += VtxtVariableName_TextChanged;
            // 
            // chkVariable
            // 
            chkVariable.AutoSize = true;
            chkVariable.Dock = DockStyle.Top;
            chkVariable.Location = new Point(4, 3);
            chkVariable.Margin = new Padding(4, 3, 4, 3);
            chkVariable.Name = "chkVariable";
            chkVariable.Size = new Size(268, 19);
            chkVariable.TabIndex = 0;
            chkVariable.Text = "Variable (specify name)";
            chkVariable.UseVisualStyleBackColor = true;
            chkVariable.CheckedChanged += ChkVariable_CheckedChanged;
            // 
            // Panel3
            // 
            Panel3.Controls.Add(cboIndexItem);
            Panel3.Controls.Add(lblFieldName);
            Panel3.Dock = DockStyle.Fill;
            Panel3.Location = new Point(135, 3);
            Panel3.Margin = new Padding(4, 3, 4, 3);
            Panel3.Name = "Panel3";
            Panel3.Padding = new Padding(4, 3, 4, 3);
            Panel3.Size = new Size(275, 56);
            Panel3.TabIndex = 1;
            // 
            // cboIndexItem
            // 
            cboIndexItem.BackColor = SystemColors.Window;
            cboIndexItem.Dock = DockStyle.Fill;
            cboIndexItem.DropDownStyle = ComboBoxStyle.DropDownList;
            cboIndexItem.FormattingEnabled = true;
            cboIndexItem.Location = new Point(4, 21);
            cboIndexItem.Margin = new Padding(4, 3, 4, 3);
            cboIndexItem.Name = "cboIndexItem";
            cboIndexItem.Size = new Size(267, 23);
            cboIndexItem.Sorted = true;
            cboIndexItem.TabIndex = 8;
            cboIndexItem.SelectedIndexChanged += CboIndexItem_SelectedIndexChanged;
            // 
            // lblFieldName
            // 
            lblFieldName.AutoSize = true;
            lblFieldName.Dock = DockStyle.Top;
            lblFieldName.Location = new Point(4, 3);
            lblFieldName.Margin = new Padding(4, 0, 4, 0);
            lblFieldName.Name = "lblFieldName";
            lblFieldName.Padding = new Padding(0, 0, 0, 3);
            lblFieldName.Size = new Size(65, 18);
            lblFieldName.TabIndex = 0;
            lblFieldName.Text = "Field name";
            // 
            // cmdRunQuery
            // 
            cmdRunQuery.Dock = DockStyle.Left;
            cmdRunQuery.Image = (Image)resources.GetObject("cmdRunQuery.Image");
            cmdRunQuery.Location = new Point(47, 0);
            cmdRunQuery.Margin = new Padding(4, 3, 4, 3);
            cmdRunQuery.Name = "cmdRunQuery";
            cmdRunQuery.Size = new Size(47, 46);
            cmdRunQuery.TabIndex = 1;
            cmdRunQuery.UseVisualStyleBackColor = true;
            cmdRunQuery.Click += CmdRunQuery_Click;
            // 
            // cmdSave
            // 
            cmdSave.Dock = DockStyle.Left;
            cmdSave.Image = (Image)resources.GetObject("cmdSave.Image");
            cmdSave.Location = new Point(0, 0);
            cmdSave.Margin = new Padding(4, 3, 4, 3);
            cmdSave.Name = "cmdSave";
            cmdSave.Size = new Size(47, 46);
            cmdSave.TabIndex = 0;
            cmdSave.UseVisualStyleBackColor = true;
            cmdSave.Click += CmdSave_Click;
            // 
            // Panel1
            // 
            Panel1.Controls.Add(cmdShowQuery);
            Panel1.Controls.Add(cmdRunQuery);
            Panel1.Controls.Add(cmdSave);
            Panel1.Dock = DockStyle.Bottom;
            Panel1.Location = new Point(0, 516);
            Panel1.Margin = new Padding(4, 3, 4, 3);
            Panel1.Name = "Panel1";
            Panel1.Size = new Size(1102, 46);
            Panel1.TabIndex = 3;
            // 
            // cmdShowQuery
            // 
            cmdShowQuery.Dock = DockStyle.Left;
            cmdShowQuery.Image = (Image)resources.GetObject("cmdShowQuery.Image");
            cmdShowQuery.Location = new Point(94, 0);
            cmdShowQuery.Margin = new Padding(4, 3, 4, 3);
            cmdShowQuery.Name = "cmdShowQuery";
            cmdShowQuery.Size = new Size(47, 46);
            cmdShowQuery.TabIndex = 2;
            cmdShowQuery.UseVisualStyleBackColor = true;
            cmdShowQuery.Click += CmdShowQuery_Click;
            // 
            // SearchEditor
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(splSearch);
            Controls.Add(Panel1);
            Margin = new Padding(4, 3, 4, 3);
            Name = "SearchEditor";
            Size = new Size(1102, 562);
            Panel2.ResumeLayout(false);
            Panel2.PerformLayout();
            splSearch.Panel1.ResumeLayout(false);
            splSearch.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splSearch).EndInit();
            splSearch.ResumeLayout(false);
            Panel4.ResumeLayout(false);
            Panel5.ResumeLayout(false);
            Panel5.PerformLayout();
            tlpTerm.ResumeLayout(false);
            panel8.ResumeLayout(false);
            panel8.PerformLayout();
            panel7.ResumeLayout(false);
            panel7.PerformLayout();
            Panel3.ResumeLayout(false);
            Panel3.PerformLayout();
            Panel1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
        internal RadioButton optSearchFolders;
        internal RadioButton optSearchObjects;
        internal RadioButton optVersionAll;
        internal RadioButton optVersionLatestBranches;
        internal RadioButton optVersionLatest;
        internal Panel Panel2;
        internal TreeView tvwSearchDef;
        internal SplitContainer splSearch;
        internal Panel Panel4;
        internal Panel Panel5;
        internal ToolTip ttSearch;
        internal Button cmdRunQuery;
        internal Button cmdSave;
        internal Panel Panel1;
        private ImageList imlSmallIcons;
        private TableLayoutPanel tlpTerm;
        internal Panel panel8;
        private CheckBox chkNotTerm;
        internal Panel panel7;
        private CheckBox chkVariable;
        internal Panel Panel3;
        internal ComboBox cboIndexItem;
        internal Label lblFieldName;
        private Panel pTermEditorContainer;
        private CheckBox chkAnyTerm;
        private C4GeneralGui.GuiElements.ValidatingTextBox vtxtVariableName;
        internal Button cmdShowQuery;
    }
}
