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
            chkDeleteProtectedRelations = new CheckBox();
            optAll = new RadioButton();
            optSelectedOnly = new RadioButton();
            panel1 = new Panel();
            cmdCancel = new Button();
            panel4 = new Panel();
            cmdOk = new Button();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // chkDeleteProtectedRelations
            // 
            chkDeleteProtectedRelations.AutoSize = true;
            chkDeleteProtectedRelations.Dock = DockStyle.Top;
            chkDeleteProtectedRelations.ImeMode = ImeMode.NoControl;
            chkDeleteProtectedRelations.Location = new Point(10, 53);
            chkDeleteProtectedRelations.Margin = new Padding(4);
            chkDeleteProtectedRelations.Name = "chkDeleteProtectedRelations";
            chkDeleteProtectedRelations.Padding = new Padding(0, 20, 0, 0);
            chkDeleteProtectedRelations.Size = new Size(369, 39);
            chkDeleteProtectedRelations.TabIndex = 13;
            chkDeleteProtectedRelations.Text = "Delete protected relations";
            chkDeleteProtectedRelations.UseVisualStyleBackColor = true;
            // 
            // optAll
            // 
            optAll.AutoSize = true;
            optAll.Dock = DockStyle.Top;
            optAll.ImeMode = ImeMode.NoControl;
            optAll.Location = new Point(10, 29);
            optAll.Margin = new Padding(4);
            optAll.Name = "optAll";
            optAll.Padding = new Padding(0, 5, 0, 0);
            optAll.Size = new Size(369, 24);
            optAll.TabIndex = 12;
            optAll.Text = "Delete all versions";
            optAll.UseVisualStyleBackColor = true;
            // 
            // optSelectedOnly
            // 
            optSelectedOnly.AutoSize = true;
            optSelectedOnly.Checked = true;
            optSelectedOnly.Dock = DockStyle.Top;
            optSelectedOnly.ImeMode = ImeMode.NoControl;
            optSelectedOnly.Location = new Point(10, 10);
            optSelectedOnly.Margin = new Padding(4);
            optSelectedOnly.Name = "optSelectedOnly";
            optSelectedOnly.Size = new Size(369, 19);
            optSelectedOnly.TabIndex = 11;
            optSelectedOnly.TabStop = true;
            optSelectedOnly.Text = "Delete selected versions only";
            optSelectedOnly.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            panel1.Controls.Add(cmdOk);
            panel1.Controls.Add(panel4);
            panel1.Controls.Add(cmdCancel);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(10, 92);
            panel1.Name = "panel1";
            panel1.Padding = new Padding(0, 20, 0, 0);
            panel1.Size = new Size(369, 45);
            panel1.TabIndex = 14;
            // 
            // cmdCancel
            // 
            cmdCancel.DialogResult = DialogResult.Cancel;
            cmdCancel.Dock = DockStyle.Right;
            cmdCancel.ImeMode = ImeMode.NoControl;
            cmdCancel.Location = new Point(281, 20);
            cmdCancel.Margin = new Padding(4);
            cmdCancel.Name = "cmdCancel";
            cmdCancel.Size = new Size(88, 25);
            cmdCancel.TabIndex = 12;
            cmdCancel.Text = "Cancel";
            cmdCancel.UseVisualStyleBackColor = true;
            // 
            // panel4
            // 
            panel4.Dock = DockStyle.Right;
            panel4.Location = new Point(271, 20);
            panel4.Name = "panel4";
            panel4.Size = new Size(10, 25);
            panel4.TabIndex = 36;
            // 
            // cmdOk
            // 
            cmdOk.DialogResult = DialogResult.OK;
            cmdOk.Dock = DockStyle.Right;
            cmdOk.ImeMode = ImeMode.NoControl;
            cmdOk.Location = new Point(183, 20);
            cmdOk.Margin = new Padding(4);
            cmdOk.Name = "cmdOk";
            cmdOk.Size = new Size(88, 25);
            cmdOk.TabIndex = 37;
            cmdOk.Text = "Ok";
            cmdOk.UseVisualStyleBackColor = true;
            // 
            // SelectDeleteVersions
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            ClientSize = new Size(389, 150);
            Controls.Add(panel1);
            Controls.Add(chkDeleteProtectedRelations);
            Controls.Add(optAll);
            Controls.Add(optSelectedOnly);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Name = "SelectDeleteVersions";
            Padding = new Padding(10);
            Text = "Delete versions behaviour";
            panel1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        internal System.Windows.Forms.CheckBox chkDeleteProtectedRelations;
        internal System.Windows.Forms.RadioButton optAll;
        internal System.Windows.Forms.RadioButton optSelectedOnly;
        private Panel panel1;
        internal Button cmdCancel;
        internal Button cmdOk;
        private Panel panel4;
    }
}