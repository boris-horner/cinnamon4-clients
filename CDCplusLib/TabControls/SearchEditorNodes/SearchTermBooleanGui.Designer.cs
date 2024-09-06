namespace CDCplusLib.TabControls.SearchEditorNodes
{
    partial class SearchTermBooleanGui
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
            this.optTrue = new System.Windows.Forms.RadioButton();
            this.optFalse = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // optTrue
            // 
            this.optTrue.Appearance = System.Windows.Forms.Appearance.Button;
            this.optTrue.AutoSize = true;
            this.optTrue.BackColor = System.Drawing.Color.LimeGreen;
            this.optTrue.Checked = true;
            this.optTrue.Dock = System.Windows.Forms.DockStyle.Top;
            this.optTrue.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            this.optTrue.FlatAppearance.BorderSize = 3;
            this.optTrue.FlatAppearance.MouseOverBackColor = System.Drawing.Color.PaleGreen;
            this.optTrue.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.optTrue.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.optTrue.ForeColor = System.Drawing.Color.White;
            this.optTrue.Location = new System.Drawing.Point(0, 0);
            this.optTrue.Margin = new System.Windows.Forms.Padding(0);
            this.optTrue.Name = "optTrue";
            this.optTrue.Padding = new System.Windows.Forms.Padding(3);
            this.optTrue.Size = new System.Drawing.Size(653, 32);
            this.optTrue.TabIndex = 0;
            this.optTrue.TabStop = true;
            this.optTrue.Text = "True";
            this.optTrue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.optTrue.UseVisualStyleBackColor = false;
            this.optTrue.CheckedChanged += new System.EventHandler(this.optTrue_CheckedChanged);
            // 
            // optFalse
            // 
            this.optFalse.Appearance = System.Windows.Forms.Appearance.Button;
            this.optFalse.AutoSize = true;
            this.optFalse.BackColor = System.Drawing.Color.Tomato;
            this.optFalse.Dock = System.Windows.Forms.DockStyle.Top;
            this.optFalse.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            this.optFalse.FlatAppearance.BorderSize = 3;
            this.optFalse.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightSalmon;
            this.optFalse.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.optFalse.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.optFalse.ForeColor = System.Drawing.Color.White;
            this.optFalse.Location = new System.Drawing.Point(0, 32);
            this.optFalse.Margin = new System.Windows.Forms.Padding(0);
            this.optFalse.Name = "optFalse";
            this.optFalse.Padding = new System.Windows.Forms.Padding(3);
            this.optFalse.Size = new System.Drawing.Size(653, 32);
            this.optFalse.TabIndex = 1;
            this.optFalse.Text = "False";
            this.optFalse.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.optFalse.UseVisualStyleBackColor = false;
            // 
            // SearchTermBooleanGui
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.optFalse);
            this.Controls.Add(this.optTrue);
            this.Name = "SearchTermBooleanGui";
            this.Size = new System.Drawing.Size(653, 388);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton optTrue;
        private System.Windows.Forms.RadioButton optFalse;
    }
}
