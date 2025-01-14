namespace CDCplusLib.TabControls
{
    partial class RelationsTabControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RelationsTabControl));
            panel20 = new Panel();
            cmdSave = new Button();
            panel1 = new Panel();
            splitContainer1 = new SplitContainer();
            panel2 = new Panel();
            lvwParents = new ListView();
            lblParents = new Label();
            panel3 = new Panel();
            lvwChildren = new ListView();
            lblChildren = new Label();
            panel20.SuspendLayout();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            panel2.SuspendLayout();
            panel3.SuspendLayout();
            SuspendLayout();
            // 
            // panel20
            // 
            panel20.Controls.Add(cmdSave);
            panel20.Dock = DockStyle.Bottom;
            panel20.Location = new Point(0, 469);
            panel20.Name = "panel20";
            panel20.Size = new Size(821, 40);
            panel20.TabIndex = 0;
            // 
            // cmdSave
            // 
            cmdSave.Dock = DockStyle.Left;
            cmdSave.Image = (Image)resources.GetObject("cmdSave.Image");
            cmdSave.Location = new Point(0, 0);
            cmdSave.Name = "cmdSave";
            cmdSave.Size = new Size(40, 40);
            cmdSave.TabIndex = 3;
            cmdSave.UseVisualStyleBackColor = true;
            cmdSave.Click += cmdSave_Click;
            // 
            // panel1
            // 
            panel1.Controls.Add(splitContainer1);
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(821, 469);
            panel1.TabIndex = 1;
            // 
            // splitContainer1
            // 
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.Location = new Point(0, 0);
            splitContainer1.Name = "splitContainer1";
            splitContainer1.Orientation = Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(panel2);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(panel3);
            splitContainer1.Size = new Size(821, 469);
            splitContainer1.SplitterDistance = 171;
            splitContainer1.TabIndex = 0;
            // 
            // panel2
            // 
            panel2.Controls.Add(lvwParents);
            panel2.Controls.Add(lblParents);
            panel2.Dock = DockStyle.Fill;
            panel2.Location = new Point(0, 0);
            panel2.Name = "panel2";
            panel2.Size = new Size(821, 171);
            panel2.TabIndex = 3;
            // 
            // lvwParents
            // 
            lvwParents.Dock = DockStyle.Fill;
            lvwParents.Location = new Point(0, 15);
            lvwParents.Name = "lvwParents";
            lvwParents.Size = new Size(821, 156);
            lvwParents.TabIndex = 2;
            lvwParents.UseCompatibleStateImageBehavior = false;
            lvwParents.View = View.Details;
            lvwParents.MouseUp += lvwParents_MouseUp;
            // 
            // lblParents
            // 
            lblParents.AutoSize = true;
            lblParents.Dock = DockStyle.Top;
            lblParents.Location = new Point(0, 0);
            lblParents.Name = "lblParents";
            lblParents.Size = new Size(46, 15);
            lblParents.TabIndex = 1;
            lblParents.Text = "Parents";
            // 
            // panel3
            // 
            panel3.Controls.Add(lvwChildren);
            panel3.Controls.Add(lblChildren);
            panel3.Dock = DockStyle.Fill;
            panel3.Location = new Point(0, 0);
            panel3.Name = "panel3";
            panel3.Size = new Size(821, 294);
            panel3.TabIndex = 3;
            // 
            // lvwChildren
            // 
            lvwChildren.Dock = DockStyle.Fill;
            lvwChildren.Location = new Point(0, 15);
            lvwChildren.Name = "lvwChildren";
            lvwChildren.Size = new Size(821, 279);
            lvwChildren.TabIndex = 2;
            lvwChildren.UseCompatibleStateImageBehavior = false;
            lvwChildren.View = View.Details;
            lvwChildren.MouseUp += lvwChildren_MouseUp;
            // 
            // lblChildren
            // 
            lblChildren.AutoSize = true;
            lblChildren.Dock = DockStyle.Top;
            lblChildren.Location = new Point(0, 0);
            lblChildren.Name = "lblChildren";
            lblChildren.Size = new Size(52, 15);
            lblChildren.TabIndex = 1;
            lblChildren.Text = "Children";
            // 
            // RelationsTabControl
            // 
            Controls.Add(panel1);
            Controls.Add(panel20);
            Name = "RelationsTabControl";
            Size = new Size(821, 509);
            panel20.ResumeLayout(false);
            panel1.ResumeLayout(false);
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel panel20;
        private System.Windows.Forms.Panel panel1;
        internal System.Windows.Forms.Button cmdSave;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ListView lvwParents;
        private System.Windows.Forms.Label lblParents;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.ListView lvwChildren;
        private System.Windows.Forms.Label lblChildren;
    }
}
