namespace CDCplusLib.TabControls.SearchEditorNodes
{
    partial class SearchTermStringGui
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
            this.vtxtString = new C4GeneralGui.GuiElements.ValidatingTextBox();
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
            this.cboOperator.TabIndex = 3;
            this.cboOperator.SelectedIndexChanged += new System.EventHandler(this.CboOperator_SelectedIndexChanged);
            // 
            // vtxtString
            // 
            this.vtxtString.BackColor = System.Drawing.Color.LightSalmon;
            this.vtxtString.Dock = System.Windows.Forms.DockStyle.Top;
            this.vtxtString.ExternalInvalidColor = System.Drawing.Color.LightSalmon;
            this.vtxtString.ExternalValidation = true;
            this.vtxtString.InternalInvalidColor = System.Drawing.Color.LightSalmon;
            this.vtxtString.Location = new System.Drawing.Point(0, 21);
            this.vtxtString.Name = "vtxtString";
            this.vtxtString.RegularExpression = "^.+$";
            this.vtxtString.Size = new System.Drawing.Size(653, 20);
            this.vtxtString.TabIndex = 5;
            this.vtxtString.ValidColor = System.Drawing.Color.White;
            this.vtxtString.TextChanged += new System.EventHandler(this.VtxtString_TextChanged);
            // 
            // SearchTermStringGui
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.vtxtString);
            this.Controls.Add(this.cboOperator);
            this.Name = "SearchTermStringGui";
            this.Size = new System.Drawing.Size(653, 388);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cboOperator;
        private C4GeneralGui.GuiElements.ValidatingTextBox vtxtString;
    }
}
