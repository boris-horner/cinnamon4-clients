namespace CDCplusLib.TabControls.SearchEditorNodes
{
    partial class SearchTermCustomTreeValueGui
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
            this.tbOperator = new System.Windows.Forms.TextBox();
            this.tvwData = new System.Windows.Forms.TreeView();
            this.SuspendLayout();
            // 
            // tbOperator
            // 
            this.tbOperator.Dock = System.Windows.Forms.DockStyle.Top;
            this.tbOperator.Location = new System.Drawing.Point(0, 0);
            this.tbOperator.Name = "tbOperator";
            this.tbOperator.ReadOnly = true;
            this.tbOperator.Size = new System.Drawing.Size(653, 20);
            this.tbOperator.TabIndex = 4;
            // 
            // tvwData
            // 
            this.tvwData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvwData.HideSelection = false;
            this.tvwData.Location = new System.Drawing.Point(0, 20);
            this.tvwData.Name = "tvwData";
            this.tvwData.Size = new System.Drawing.Size(653, 368);
            this.tvwData.TabIndex = 5;
            this.tvwData.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.TvwData_AfterSelect);
            // 
            // SearchTermCustomTreeValueGui
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tvwData);
            this.Controls.Add(this.tbOperator);
            this.Name = "SearchTermCustomTreeValueGui";
            this.Size = new System.Drawing.Size(653, 388);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbOperator;
        private System.Windows.Forms.TreeView tvwData;
    }
}
