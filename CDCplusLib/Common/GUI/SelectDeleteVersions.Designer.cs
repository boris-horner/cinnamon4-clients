namespace CDCplusLib.Common.GUI
{
    partial class SelectDeleteVersions
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
            this.chkDeleteProtectedRelations = new System.Windows.Forms.CheckBox();
            this.optAll = new System.Windows.Forms.RadioButton();
            this.optSelectedOnly = new System.Windows.Forms.RadioButton();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.cmdOk = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // chkDeleteProtectedRelations
            // 
            this.chkDeleteProtectedRelations.AutoSize = true;
            this.chkDeleteProtectedRelations.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.chkDeleteProtectedRelations.Location = new System.Drawing.Point(10, 86);
            this.chkDeleteProtectedRelations.Margin = new System.Windows.Forms.Padding(4);
            this.chkDeleteProtectedRelations.Name = "chkDeleteProtectedRelations";
            this.chkDeleteProtectedRelations.Size = new System.Drawing.Size(193, 21);
            this.chkDeleteProtectedRelations.TabIndex = 13;
            this.chkDeleteProtectedRelations.Text = "Delete protected relations";
            this.chkDeleteProtectedRelations.UseVisualStyleBackColor = true;
            // 
            // optAll
            // 
            this.optAll.AutoSize = true;
            this.optAll.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.optAll.Location = new System.Drawing.Point(10, 43);
            this.optAll.Margin = new System.Windows.Forms.Padding(4);
            this.optAll.Name = "optAll";
            this.optAll.Size = new System.Drawing.Size(145, 21);
            this.optAll.TabIndex = 12;
            this.optAll.Text = "Delete all versions";
            this.optAll.UseVisualStyleBackColor = true;
            // 
            // optSelectedOnly
            // 
            this.optSelectedOnly.AutoSize = true;
            this.optSelectedOnly.Checked = true;
            this.optSelectedOnly.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.optSelectedOnly.Location = new System.Drawing.Point(10, 13);
            this.optSelectedOnly.Margin = new System.Windows.Forms.Padding(4);
            this.optSelectedOnly.Name = "optSelectedOnly";
            this.optSelectedOnly.Size = new System.Drawing.Size(214, 21);
            this.optSelectedOnly.TabIndex = 11;
            this.optSelectedOnly.TabStop = true;
            this.optSelectedOnly.Text = "Delete selected versions only";
            this.optSelectedOnly.UseVisualStyleBackColor = true;
            // 
            // cmdCancel
            // 
            this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdCancel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.cmdCancel.Location = new System.Drawing.Point(121, 131);
            this.cmdCancel.Margin = new System.Windows.Forms.Padding(4);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(100, 28);
            this.cmdCancel.TabIndex = 10;
            this.cmdCancel.Text = "Cancel";
            this.cmdCancel.UseVisualStyleBackColor = true;
            // 
            // cmdOk
            // 
            this.cmdOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.cmdOk.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.cmdOk.Location = new System.Drawing.Point(13, 131);
            this.cmdOk.Margin = new System.Windows.Forms.Padding(4);
            this.cmdOk.Name = "cmdOk";
            this.cmdOk.Size = new System.Drawing.Size(100, 28);
            this.cmdOk.TabIndex = 9;
            this.cmdOk.Text = "Ok";
            this.cmdOk.UseVisualStyleBackColor = true;
            // 
            // SelectDeleteVersions
            // 
            this.AcceptButton = this.cmdOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cmdCancel;
            this.ClientSize = new System.Drawing.Size(320, 173);
            this.Controls.Add(this.chkDeleteProtectedRelations);
            this.Controls.Add(this.optAll);
            this.Controls.Add(this.optSelectedOnly);
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.cmdOk);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "SelectDeleteVersions";
            this.Text = "Delete versions behaviour";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.CheckBox chkDeleteProtectedRelations;
        internal System.Windows.Forms.RadioButton optAll;
        internal System.Windows.Forms.RadioButton optSelectedOnly;
        internal System.Windows.Forms.Button cmdCancel;
        internal System.Windows.Forms.Button cmdOk;
    }
}