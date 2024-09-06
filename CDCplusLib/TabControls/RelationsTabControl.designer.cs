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
            this.panel20 = new System.Windows.Forms.Panel();
            this.cmdSave = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lvwParents = new System.Windows.Forms.ListView();
            this.lblParents = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.lvwChildren = new System.Windows.Forms.ListView();
            this.lblChildren = new System.Windows.Forms.Label();
            this.panel20.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel20
            // 
            this.panel20.Controls.Add(this.cmdSave);
            this.panel20.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel20.Location = new System.Drawing.Point(0, 469);
            this.panel20.Name = "panel20";
            this.panel20.Size = new System.Drawing.Size(821, 40);
            this.panel20.TabIndex = 0;
            // 
            // cmdSave
            // 
            this.cmdSave.Dock = System.Windows.Forms.DockStyle.Left;
            this.cmdSave.Image = ((System.Drawing.Image)(resources.GetObject("cmdSave.Image")));
            this.cmdSave.Location = new System.Drawing.Point(0, 0);
            this.cmdSave.Name = "cmdSave";
            this.cmdSave.Size = new System.Drawing.Size(40, 40);
            this.cmdSave.TabIndex = 3;
            this.cmdSave.UseVisualStyleBackColor = true;
            this.cmdSave.Click += new System.EventHandler(this.cmdSave_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.splitContainer1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(821, 469);
            this.panel1.TabIndex = 1;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.panel2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.panel3);
            this.splitContainer1.Size = new System.Drawing.Size(821, 469);
            this.splitContainer1.SplitterDistance = 171;
            this.splitContainer1.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.lvwParents);
            this.panel2.Controls.Add(this.lblParents);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(821, 171);
            this.panel2.TabIndex = 3;
            // 
            // lvwParents
            // 
            this.lvwParents.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvwParents.HideSelection = false;
            this.lvwParents.Location = new System.Drawing.Point(0, 17);
            this.lvwParents.Name = "lvwParents";
            this.lvwParents.Size = new System.Drawing.Size(821, 154);
            this.lvwParents.TabIndex = 2;
            this.lvwParents.UseCompatibleStateImageBehavior = false;
            this.lvwParents.View = System.Windows.Forms.View.Details;
            this.lvwParents.MouseUp += new System.Windows.Forms.MouseEventHandler(this.lvwParents_MouseUp);
            // 
            // lblParents
            // 
            this.lblParents.AutoSize = true;
            this.lblParents.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblParents.Location = new System.Drawing.Point(0, 0);
            this.lblParents.Name = "lblParents";
            this.lblParents.Size = new System.Drawing.Size(57, 17);
            this.lblParents.TabIndex = 1;
            this.lblParents.Text = "Parents";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.lvwChildren);
            this.panel3.Controls.Add(this.lblChildren);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(821, 294);
            this.panel3.TabIndex = 3;
            // 
            // lvwChildren
            // 
            this.lvwChildren.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvwChildren.HideSelection = false;
            this.lvwChildren.Location = new System.Drawing.Point(0, 17);
            this.lvwChildren.Name = "lvwChildren";
            this.lvwChildren.Size = new System.Drawing.Size(821, 277);
            this.lvwChildren.TabIndex = 2;
            this.lvwChildren.UseCompatibleStateImageBehavior = false;
            this.lvwChildren.View = System.Windows.Forms.View.Details;
            this.lvwChildren.MouseUp += new System.Windows.Forms.MouseEventHandler(this.lvwChildren_MouseUp);
            // 
            // lblChildren
            // 
            this.lblChildren.AutoSize = true;
            this.lblChildren.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblChildren.Location = new System.Drawing.Point(0, 0);
            this.lblChildren.Name = "lblChildren";
            this.lblChildren.Size = new System.Drawing.Size(60, 17);
            this.lblChildren.TabIndex = 1;
            this.lblChildren.Text = "Children";
            // 
            // RelationsTabControl
            // 
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel20);
            this.Name = "RelationsTabControl";
            this.Size = new System.Drawing.Size(821, 509);
            this.panel20.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);

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
