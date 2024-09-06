namespace CDCplusLib.TabControls.SearchEditorNodes
{
    partial class SearchOperatorGui
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
            this.pOpType = new System.Windows.Forms.Panel();
            this.optOpTypeOr = new System.Windows.Forms.RadioButton();
            this.optOpTypeAnd = new System.Windows.Forms.RadioButton();
            this.pOpType.SuspendLayout();
            this.SuspendLayout();
            // 
            // pOpType
            // 
            this.pOpType.Controls.Add(this.optOpTypeOr);
            this.pOpType.Controls.Add(this.optOpTypeAnd);
            this.pOpType.Dock = System.Windows.Forms.DockStyle.Top;
            this.pOpType.Location = new System.Drawing.Point(0, 0);
            this.pOpType.Name = "pOpType";
            this.pOpType.Size = new System.Drawing.Size(466, 30);
            this.pOpType.TabIndex = 2;
            // 
            // optOpTypeOr
            // 
            this.optOpTypeOr.AutoSize = true;
            this.optOpTypeOr.Dock = System.Windows.Forms.DockStyle.Left;
            this.optOpTypeOr.Location = new System.Drawing.Point(48, 0);
            this.optOpTypeOr.Name = "optOpTypeOr";
            this.optOpTypeOr.Size = new System.Drawing.Size(41, 30);
            this.optOpTypeOr.TabIndex = 3;
            this.optOpTypeOr.TabStop = true;
            this.optOpTypeOr.Text = "OR";
            this.optOpTypeOr.UseVisualStyleBackColor = true;
            // 
            // optOpTypeAnd
            // 
            this.optOpTypeAnd.AutoSize = true;
            this.optOpTypeAnd.Checked = true;
            this.optOpTypeAnd.Dock = System.Windows.Forms.DockStyle.Left;
            this.optOpTypeAnd.Location = new System.Drawing.Point(0, 0);
            this.optOpTypeAnd.Name = "optOpTypeAnd";
            this.optOpTypeAnd.Size = new System.Drawing.Size(48, 30);
            this.optOpTypeAnd.TabIndex = 2;
            this.optOpTypeAnd.TabStop = true;
            this.optOpTypeAnd.Text = "AND";
            this.optOpTypeAnd.UseVisualStyleBackColor = true;
            this.optOpTypeAnd.CheckedChanged += new System.EventHandler(this.optOpTypeAnd_CheckedChanged);
            // 
            // SearchOperatorGui
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pOpType);
            this.Name = "SearchOperatorGui";
            this.Size = new System.Drawing.Size(466, 270);
            this.pOpType.ResumeLayout(false);
            this.pOpType.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pOpType;
        private System.Windows.Forms.RadioButton optOpTypeOr;
        private System.Windows.Forms.RadioButton optOpTypeAnd;
    }
}
