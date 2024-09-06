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
            this.lblPassword = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.chkSaveCredentials = new System.Windows.Forms.CheckBox();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.cmdOk = new System.Windows.Forms.Button();
            this.cmdSetPassword = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblPassword
            // 
            this.lblPassword.AutoSize = true;
            this.lblPassword.Location = new System.Drawing.Point(13, 13);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(53, 13);
            this.lblPassword.TabIndex = 0;
            this.lblPassword.Text = "Password";
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(16, 30);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '•';
            this.txtPassword.Size = new System.Drawing.Size(314, 20);
            this.txtPassword.TabIndex = 1;
            // 
            // chkSaveCredentials
            // 
            this.chkSaveCredentials.AutoSize = true;
            this.chkSaveCredentials.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.chkSaveCredentials.Location = new System.Drawing.Point(16, 68);
            this.chkSaveCredentials.Name = "chkSaveCredentials";
            this.chkSaveCredentials.Size = new System.Drawing.Size(105, 17);
            this.chkSaveCredentials.TabIndex = 17;
            this.chkSaveCredentials.Text = "Save credentials";
            this.chkSaveCredentials.UseVisualStyleBackColor = true;
            // 
            // cmdCancel
            // 
            this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdCancel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.cmdCancel.Location = new System.Drawing.Point(285, 111);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(75, 23);
            this.cmdCancel.TabIndex = 19;
            this.cmdCancel.Text = "Cancel";
            this.cmdCancel.UseVisualStyleBackColor = true;
            this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
            // 
            // cmdOk
            // 
            this.cmdOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.cmdOk.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.cmdOk.Location = new System.Drawing.Point(204, 111);
            this.cmdOk.Name = "cmdOk";
            this.cmdOk.Size = new System.Drawing.Size(75, 23);
            this.cmdOk.TabIndex = 18;
            this.cmdOk.Text = "Ok";
            this.cmdOk.UseVisualStyleBackColor = true;
            this.cmdOk.Click += new System.EventHandler(this.cmdOk_Click);
            // 
            // cmdSetPassword
            // 
            this.cmdSetPassword.Image = ((System.Drawing.Image)(resources.GetObject("cmdSetPassword.Image")));
            this.cmdSetPassword.Location = new System.Drawing.Point(336, 30);
            this.cmdSetPassword.Name = "cmdSetPassword";
            this.cmdSetPassword.Size = new System.Drawing.Size(24, 24);
            this.cmdSetPassword.TabIndex = 20;
            this.cmdSetPassword.UseVisualStyleBackColor = true;
            this.cmdSetPassword.MouseDown += new System.Windows.Forms.MouseEventHandler(this.cmdSetPassword_MouseDown);
            this.cmdSetPassword.MouseUp += new System.Windows.Forms.MouseEventHandler(this.cmdSetPassword_MouseUp);
            // 
            // SetPassword
            // 
            this.AcceptButton = this.cmdOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cmdCancel;
            this.ClientSize = new System.Drawing.Size(372, 145);
            this.Controls.Add(this.cmdSetPassword);
            this.Controls.Add(this.chkSaveCredentials);
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.cmdOk);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.lblPassword);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "SetPassword";
            this.Text = "Change Password";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.TextBox txtPassword;
        internal System.Windows.Forms.CheckBox chkSaveCredentials;
        internal System.Windows.Forms.Button cmdCancel;
        internal System.Windows.Forms.Button cmdOk;
        private System.Windows.Forms.Button cmdSetPassword;
    }
}