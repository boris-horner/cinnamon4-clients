namespace CDCplusLib.Common.GUI
{
    partial class SelectObjectsDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SelectObjectsDialog));
            panel1 = new Panel();
            optVersionAll = new RadioButton();
            optVersionLatestBranches = new RadioButton();
            optVersionLatest = new RadioButton();
            panel3 = new Panel();
            cmdNewFolder = new Button();
            cmdFolderUp = new Button();
            cmdHome = new Button();
            cmdRoot = new Button();
            panel2 = new Panel();
            cmdCancel = new Button();
            cmdOk = new Button();
            splHor = new SplitContainer();
            ctccListContext = new ContextTabControlContainer();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splHor).BeginInit();
            splHor.Panel2.SuspendLayout();
            splHor.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Controls.Add(optVersionAll);
            panel1.Controls.Add(optVersionLatestBranches);
            panel1.Controls.Add(optVersionLatest);
            panel1.Controls.Add(panel3);
            panel1.Controls.Add(cmdNewFolder);
            panel1.Controls.Add(cmdFolderUp);
            panel1.Controls.Add(cmdHome);
            panel1.Controls.Add(cmdRoot);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(1609, 40);
            panel1.TabIndex = 0;
            // 
            // optVersionAll
            // 
            optVersionAll.Appearance = Appearance.Button;
            optVersionAll.AutoSize = true;
            optVersionAll.Dock = DockStyle.Left;
            optVersionAll.Image = (Image)resources.GetObject("optVersionAll.Image");
            optVersionAll.Location = new Point(256, 0);
            optVersionAll.Margin = new Padding(4, 3, 4, 3);
            optVersionAll.Name = "optVersionAll";
            optVersionAll.Size = new Size(38, 40);
            optVersionAll.TabIndex = 17;
            optVersionAll.UseVisualStyleBackColor = true;
            optVersionAll.CheckedChanged += optVersionAll_CheckedChanged;
            // 
            // optVersionLatestBranches
            // 
            optVersionLatestBranches.Appearance = Appearance.Button;
            optVersionLatestBranches.AutoSize = true;
            optVersionLatestBranches.Dock = DockStyle.Left;
            optVersionLatestBranches.Image = (Image)resources.GetObject("optVersionLatestBranches.Image");
            optVersionLatestBranches.Location = new Point(218, 0);
            optVersionLatestBranches.Margin = new Padding(4, 3, 4, 3);
            optVersionLatestBranches.Name = "optVersionLatestBranches";
            optVersionLatestBranches.Size = new Size(38, 40);
            optVersionLatestBranches.TabIndex = 16;
            optVersionLatestBranches.UseVisualStyleBackColor = true;
            optVersionLatestBranches.CheckedChanged += optVersionLatestBranches_CheckedChanged;
            // 
            // optVersionLatest
            // 
            optVersionLatest.Appearance = Appearance.Button;
            optVersionLatest.AutoSize = true;
            optVersionLatest.Checked = true;
            optVersionLatest.Dock = DockStyle.Left;
            optVersionLatest.Image = (Image)resources.GetObject("optVersionLatest.Image");
            optVersionLatest.Location = new Point(180, 0);
            optVersionLatest.Margin = new Padding(4, 3, 4, 3);
            optVersionLatest.Name = "optVersionLatest";
            optVersionLatest.Size = new Size(38, 40);
            optVersionLatest.TabIndex = 15;
            optVersionLatest.TabStop = true;
            optVersionLatest.UseVisualStyleBackColor = true;
            optVersionLatest.CheckedChanged += optVersionLatest_CheckedChanged;
            // 
            // panel3
            // 
            panel3.Dock = DockStyle.Left;
            panel3.Location = new Point(160, 0);
            panel3.Name = "panel3";
            panel3.Size = new Size(20, 40);
            panel3.TabIndex = 14;
            // 
            // cmdNewFolder
            // 
            cmdNewFolder.Dock = DockStyle.Left;
            cmdNewFolder.Image = (Image)resources.GetObject("cmdNewFolder.Image");
            cmdNewFolder.Location = new Point(120, 0);
            cmdNewFolder.Name = "cmdNewFolder";
            cmdNewFolder.Size = new Size(40, 40);
            cmdNewFolder.TabIndex = 13;
            cmdNewFolder.UseVisualStyleBackColor = true;
            cmdNewFolder.Click += cmdNewFolder_Click;
            // 
            // cmdFolderUp
            // 
            cmdFolderUp.Dock = DockStyle.Left;
            cmdFolderUp.Image = (Image)resources.GetObject("cmdFolderUp.Image");
            cmdFolderUp.Location = new Point(80, 0);
            cmdFolderUp.Name = "cmdFolderUp";
            cmdFolderUp.Size = new Size(40, 40);
            cmdFolderUp.TabIndex = 8;
            cmdFolderUp.UseVisualStyleBackColor = true;
            cmdFolderUp.Click += cmdFolderUp_Click;
            // 
            // cmdHome
            // 
            cmdHome.Dock = DockStyle.Left;
            cmdHome.Image = (Image)resources.GetObject("cmdHome.Image");
            cmdHome.Location = new Point(40, 0);
            cmdHome.Name = "cmdHome";
            cmdHome.Size = new Size(40, 40);
            cmdHome.TabIndex = 7;
            cmdHome.UseVisualStyleBackColor = true;
            cmdHome.Click += cmdHome_Click;
            // 
            // cmdRoot
            // 
            cmdRoot.Dock = DockStyle.Left;
            cmdRoot.Image = (Image)resources.GetObject("cmdRoot.Image");
            cmdRoot.Location = new Point(0, 0);
            cmdRoot.Name = "cmdRoot";
            cmdRoot.Size = new Size(40, 40);
            cmdRoot.TabIndex = 6;
            cmdRoot.UseVisualStyleBackColor = true;
            cmdRoot.Click += cmdRoot_Click;
            // 
            // panel2
            // 
            panel2.Controls.Add(cmdCancel);
            panel2.Controls.Add(cmdOk);
            panel2.Dock = DockStyle.Bottom;
            panel2.Location = new Point(0, 1039);
            panel2.Name = "panel2";
            panel2.Size = new Size(1609, 40);
            panel2.TabIndex = 1;
            // 
            // cmdCancel
            // 
            cmdCancel.Dock = DockStyle.Left;
            cmdCancel.Image = (Image)resources.GetObject("cmdCancel.Image");
            cmdCancel.Location = new Point(40, 0);
            cmdCancel.Name = "cmdCancel";
            cmdCancel.Size = new Size(40, 40);
            cmdCancel.TabIndex = 6;
            cmdCancel.UseVisualStyleBackColor = true;
            cmdCancel.Click += cmdCancel_Click;
            // 
            // cmdOk
            // 
            cmdOk.Dock = DockStyle.Left;
            cmdOk.Image = (Image)resources.GetObject("cmdOk.Image");
            cmdOk.Location = new Point(0, 0);
            cmdOk.Name = "cmdOk";
            cmdOk.Size = new Size(40, 40);
            cmdOk.TabIndex = 5;
            cmdOk.UseVisualStyleBackColor = true;
            cmdOk.Click += cmdOk_Click;
            // 
            // splHor
            // 
            splHor.Dock = DockStyle.Fill;
            splHor.Location = new Point(0, 40);
            splHor.Name = "splHor";
            splHor.Orientation = Orientation.Horizontal;
            // 
            // splHor.Panel2
            // 
            splHor.Panel2.Controls.Add(ctccListContext);
            splHor.Size = new Size(1609, 999);
            splHor.SplitterDistance = 401;
            splHor.TabIndex = 2;
            // 
            // ctccListContext
            // 
            ctccListContext.Dock = DockStyle.Fill;
            ctccListContext.Location = new Point(0, 0);
            ctccListContext.Name = "ctccListContext";
            ctccListContext.Size = new Size(1609, 594);
            ctccListContext.TabIndex = 0;
            // 
            // SelectObjectsDialog
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1609, 1079);
            Controls.Add(splHor);
            Controls.Add(panel2);
            Controls.Add(panel1);
            Name = "SelectObjectsDialog";
            Text = "SelectObjectsDialog";
            Shown += SelectObjectsDialog_Shown;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panel2.ResumeLayout(false);
            splHor.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splHor).EndInit();
            splHor.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private Panel panel2;
        private SplitContainer splHor;
        private ContextTabControlContainer ctccListContext;
        internal Button cmdCancel;
        internal Button cmdOk;
        internal Button cmdFolderUp;
        internal Button cmdHome;
        internal Button cmdRoot;
        internal RadioButton optVersionAll;
        internal RadioButton optVersionLatestBranches;
        internal RadioButton optVersionLatest;
        private Panel panel3;
        internal Button cmdNewFolder;
    }
}