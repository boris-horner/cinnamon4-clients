namespace CDCplusLib.TabControls.SearchEditorNodes
{
    partial class SearchTermFolderGui
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
            this.cboOperator = new System.Windows.Forms.ComboBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.vtxtPath = new C4GeneralGui.GuiElements.ValidatingTextBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.cmdSelectPath = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cboOperator
            // 
            this.cboOperator.Dock = System.Windows.Forms.DockStyle.Top;
            this.cboOperator.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboOperator.FormattingEnabled = true;
            this.cboOperator.Location = new System.Drawing.Point(0, 0);
            this.cboOperator.Name = "cboOperator";
            this.cboOperator.Size = new System.Drawing.Size(653, 21);
            this.cboOperator.TabIndex = 4;
            this.cboOperator.SelectedIndexChanged += new System.EventHandler(this.CboOperator_SelectedIndexChanged);
            // 
            // panel2
            // 
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 21);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(653, 10);
            this.panel2.TabIndex = 6;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.vtxtPath);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.cmdSelectPath);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 31);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(653, 20);
            this.panel1.TabIndex = 7;
            // 
            // vtxtPath
            // 
            this.vtxtPath.BackColor = System.Drawing.Color.White;
            this.vtxtPath.Dock = System.Windows.Forms.DockStyle.Fill;
            this.vtxtPath.ExternalInvalidColor = System.Drawing.Color.Yellow;
            this.vtxtPath.ExternalValidation = true;
            this.vtxtPath.InternalInvalidColor = System.Drawing.Color.Yellow;
            this.vtxtPath.Location = new System.Drawing.Point(0, 0);
            this.vtxtPath.Name = "vtxtPath";
            this.vtxtPath.RegularExpression = "";
            this.vtxtPath.Size = new System.Drawing.Size(603, 20);
            this.vtxtPath.TabIndex = 10;
            this.vtxtPath.ValidColor = System.Drawing.Color.White;
            this.vtxtPath.TextChanged += new System.EventHandler(this.VtxtPath_TextChanged);
            // 
            // panel3
            // 
            this.panel3.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel3.Location = new System.Drawing.Point(603, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(15, 20);
            this.panel3.TabIndex = 9;
            // 
            // cmdSelectPath
            // 
            this.cmdSelectPath.Dock = System.Windows.Forms.DockStyle.Right;
            this.cmdSelectPath.Location = new System.Drawing.Point(618, 0);
            this.cmdSelectPath.Name = "cmdSelectPath";
            this.cmdSelectPath.Size = new System.Drawing.Size(35, 20);
            this.cmdSelectPath.TabIndex = 0;
            this.cmdSelectPath.Text = "...";
            this.cmdSelectPath.UseVisualStyleBackColor = true;
            this.cmdSelectPath.Click += new System.EventHandler(this.CmdSelectPath_Click);
            // 
            // SearchTermFolderGui
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.cboOperator);
            this.Name = "SearchTermFolderGui";
            this.Size = new System.Drawing.Size(653, 388);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cboOperator;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel1;
        private C4GeneralGui.GuiElements.ValidatingTextBox vtxtPath;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button cmdSelectPath;
    }
}
