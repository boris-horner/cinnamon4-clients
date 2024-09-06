namespace CDCplusLib.TabControls.SearchEditorNodes
{
    partial class SearchTermDateGui
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
            this.dtpDate = new System.Windows.Forms.DateTimePicker();
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
            this.cboOperator.TabIndex = 0;
            this.cboOperator.SelectedIndexChanged += new System.EventHandler(this.CboOperator_SelectedIndexChanged);
            // 
            // dtpDate
            // 
            this.dtpDate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtpDate.Location = new System.Drawing.Point(0, 21);
            this.dtpDate.Name = "dtpDate";
            this.dtpDate.Size = new System.Drawing.Size(653, 20);
            this.dtpDate.TabIndex = 1;
            this.dtpDate.ValueChanged += new System.EventHandler(this.DtpDate_ValueChanged);
            // 
            // SearchTermDateGui
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dtpDate);
            this.Controls.Add(this.cboOperator);
            this.Name = "SearchTermDateGui";
            this.Size = new System.Drawing.Size(653, 388);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cboOperator;
        private System.Windows.Forms.DateTimePicker dtpDate;
    }
}
