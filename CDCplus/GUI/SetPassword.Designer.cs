namespace CDCplus.GUI
{
    partial class SetPassword
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SetPassword));
            lblPassword = new System.Windows.Forms.Label();
            panel1 = new System.Windows.Forms.Panel();
            txtPassword = new System.Windows.Forms.TextBox();
            panel2 = new System.Windows.Forms.Panel();
            cmdSetPassword = new System.Windows.Forms.Button();
            chkSaveCredentials = new System.Windows.Forms.CheckBox();
            panel3 = new System.Windows.Forms.Panel();
            cmdOk = new System.Windows.Forms.Button();
            panel4 = new System.Windows.Forms.Panel();
            cmdCancel = new System.Windows.Forms.Button();
            panel1.SuspendLayout();
            panel3.SuspendLayout();
            SuspendLayout();
            // 
            // lblPassword
            // 
            lblPassword.AutoSize = true;
            lblPassword.Dock = System.Windows.Forms.DockStyle.Top;
            lblPassword.Location = new System.Drawing.Point(10, 10);
            lblPassword.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            lblPassword.Name = "lblPassword";
            lblPassword.Size = new System.Drawing.Size(57, 15);
            lblPassword.TabIndex = 0;
            lblPassword.Text = "Password";
            // 
            // panel1
            // 
            panel1.Controls.Add(txtPassword);
            panel1.Controls.Add(panel2);
            panel1.Controls.Add(cmdSetPassword);
            panel1.Dock = System.Windows.Forms.DockStyle.Top;
            panel1.Location = new System.Drawing.Point(10, 25);
            panel1.Name = "panel1";
            panel1.Padding = new System.Windows.Forms.Padding(0, 0, 0, 10);
            panel1.Size = new System.Drawing.Size(464, 32);
            panel1.TabIndex = 21;
            // 
            // txtPassword
            // 
            txtPassword.Dock = System.Windows.Forms.DockStyle.Fill;
            txtPassword.Location = new System.Drawing.Point(0, 0);
            txtPassword.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            txtPassword.Name = "txtPassword";
            txtPassword.PasswordChar = '•';
            txtPassword.Size = new System.Drawing.Size(426, 23);
            txtPassword.TabIndex = 38;
            // 
            // panel2
            // 
            panel2.Dock = System.Windows.Forms.DockStyle.Right;
            panel2.Location = new System.Drawing.Point(426, 0);
            panel2.Name = "panel2";
            panel2.Size = new System.Drawing.Size(10, 22);
            panel2.TabIndex = 37;
            // 
            // cmdSetPassword
            // 
            cmdSetPassword.Dock = System.Windows.Forms.DockStyle.Right;
            cmdSetPassword.Image = (System.Drawing.Image)resources.GetObject("cmdSetPassword.Image");
            cmdSetPassword.Location = new System.Drawing.Point(436, 0);
            cmdSetPassword.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            cmdSetPassword.Name = "cmdSetPassword";
            cmdSetPassword.Size = new System.Drawing.Size(28, 22);
            cmdSetPassword.TabIndex = 22;
            cmdSetPassword.UseVisualStyleBackColor = true;
            cmdSetPassword.MouseDown += cmdSetPassword_MouseDown;
            cmdSetPassword.MouseUp += cmdSetPassword_MouseUp;
            // 
            // chkSaveCredentials
            // 
            chkSaveCredentials.AutoSize = true;
            chkSaveCredentials.Dock = System.Windows.Forms.DockStyle.Top;
            chkSaveCredentials.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            chkSaveCredentials.Location = new System.Drawing.Point(10, 57);
            chkSaveCredentials.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            chkSaveCredentials.Name = "chkSaveCredentials";
            chkSaveCredentials.Size = new System.Drawing.Size(464, 19);
            chkSaveCredentials.TabIndex = 22;
            chkSaveCredentials.Text = "Save credentials";
            chkSaveCredentials.UseVisualStyleBackColor = true;
            // 
            // panel3
            // 
            panel3.Controls.Add(cmdOk);
            panel3.Controls.Add(panel4);
            panel3.Controls.Add(cmdCancel);
            panel3.Dock = System.Windows.Forms.DockStyle.Top;
            panel3.Location = new System.Drawing.Point(10, 76);
            panel3.Name = "panel3";
            panel3.Padding = new System.Windows.Forms.Padding(0, 20, 0, 0);
            panel3.Size = new System.Drawing.Size(464, 45);
            panel3.TabIndex = 54;
            // 
            // cmdOk
            // 
            cmdOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            cmdOk.Dock = System.Windows.Forms.DockStyle.Right;
            cmdOk.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            cmdOk.Location = new System.Drawing.Point(278, 20);
            cmdOk.Margin = new System.Windows.Forms.Padding(4);
            cmdOk.Name = "cmdOk";
            cmdOk.Size = new System.Drawing.Size(88, 25);
            cmdOk.TabIndex = 37;
            cmdOk.Text = "Ok";
            cmdOk.UseVisualStyleBackColor = true;
            // 
            // panel4
            // 
            panel4.Dock = System.Windows.Forms.DockStyle.Right;
            panel4.Location = new System.Drawing.Point(366, 20);
            panel4.Name = "panel4";
            panel4.Size = new System.Drawing.Size(10, 25);
            panel4.TabIndex = 36;
            // 
            // cmdCancel
            // 
            cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            cmdCancel.Dock = System.Windows.Forms.DockStyle.Right;
            cmdCancel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            cmdCancel.Location = new System.Drawing.Point(376, 20);
            cmdCancel.Margin = new System.Windows.Forms.Padding(4);
            cmdCancel.Name = "cmdCancel";
            cmdCancel.Size = new System.Drawing.Size(88, 25);
            cmdCancel.TabIndex = 12;
            cmdCancel.Text = "Cancel";
            cmdCancel.UseVisualStyleBackColor = true;
            // 
            // SetPassword
            // 
            AcceptButton = cmdOk;
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            AutoSize = true;
            CancelButton = cmdCancel;
            ClientSize = new System.Drawing.Size(484, 132);
            Controls.Add(panel3);
            Controls.Add(chkSaveCredentials);
            Controls.Add(panel1);
            Controls.Add(lblPassword);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            MinimumSize = new System.Drawing.Size(500, 0);
            Name = "SetPassword";
            Padding = new System.Windows.Forms.Padding(10);
            Text = "Change Password";
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panel3.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button cmdSetPassword;
        internal System.Windows.Forms.CheckBox chkSaveCredentials;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        internal System.Windows.Forms.Button cmdOk;
        private System.Windows.Forms.Panel panel4;
        internal System.Windows.Forms.Button cmdCancel;
        private System.Windows.Forms.TextBox txtPassword;
    }
}