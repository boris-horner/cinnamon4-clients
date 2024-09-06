namespace CDCplusLib.TabControls.SearchEditorNodes
{
    partial class SearchTermIntegerGui
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
            this.vtxtInteger = new C4GeneralGui.GuiElements.ValidatingTextBox();
            this.cboOperator = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // vtxtInteger
            // 
            this.vtxtInteger.BackColor = System.Drawing.Color.LightSalmon;
            this.vtxtInteger.Dock = System.Windows.Forms.DockStyle.Top;
            this.vtxtInteger.ExternalInvalidColor = System.Drawing.Color.LightSalmon;
            this.vtxtInteger.ExternalValidation = true;
            this.vtxtInteger.InternalInvalidColor = System.Drawing.Color.LightSalmon;
            this.vtxtInteger.Location = new System.Drawing.Point(0, 21);
            this.vtxtInteger.Name = "vtxtInteger";
            this.vtxtInteger.RegularExpression = "^\\d+$";
            this.vtxtInteger.Size = new System.Drawing.Size(653, 20);
            this.vtxtInteger.TabIndex = 4;
            this.vtxtInteger.ValidColor = System.Drawing.Color.White;
            this.vtxtInteger.TextChanged += new System.EventHandler(this.VtxtInteger_TextChanged);
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
            // SearchTermIntegerGui
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.vtxtInteger);
            this.Controls.Add(this.cboOperator);
            this.Name = "SearchTermIntegerGui";
            this.Size = new System.Drawing.Size(653, 388);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private C4GeneralGui.GuiElements.ValidatingTextBox vtxtInteger;
        private System.Windows.Forms.ComboBox cboOperator;
    }
}
