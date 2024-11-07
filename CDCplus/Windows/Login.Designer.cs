namespace CDCplus.Windows
{
	partial class Login
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Login));
            pLogo = new System.Windows.Forms.Panel();
            LogoPictureBox = new System.Windows.Forms.PictureBox();
            panel2 = new System.Windows.Forms.Panel();
            panel3 = new System.Windows.Forms.Panel();
            cmdOk = new System.Windows.Forms.Button();
            panel4 = new System.Windows.Forms.Panel();
            cmdCancel = new System.Windows.Forms.Button();
            chkSaveCredentials = new System.Windows.Forms.CheckBox();
            txtPassword = new System.Windows.Forms.TextBox();
            lblPassword = new System.Windows.Forms.Label();
            txtUser = new System.Windows.Forms.TextBox();
            lblUser = new System.Windows.Forms.Label();
            pLogo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)LogoPictureBox).BeginInit();
            panel2.SuspendLayout();
            panel3.SuspendLayout();
            SuspendLayout();
            // 
            // pLogo
            // 
            pLogo.Controls.Add(LogoPictureBox);
            pLogo.Dock = System.Windows.Forms.DockStyle.Left;
            pLogo.Location = new System.Drawing.Point(15, 15);
            pLogo.Name = "pLogo";
            pLogo.Size = new System.Drawing.Size(57, 171);
            pLogo.TabIndex = 17;
            // 
            // LogoPictureBox
            // 
            LogoPictureBox.Dock = System.Windows.Forms.DockStyle.Top;
            LogoPictureBox.Image = (System.Drawing.Image)resources.GetObject("LogoPictureBox.Image");
            LogoPictureBox.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            LogoPictureBox.Location = new System.Drawing.Point(0, 0);
            LogoPictureBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            LogoPictureBox.Name = "LogoPictureBox";
            LogoPictureBox.Size = new System.Drawing.Size(57, 63);
            LogoPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            LogoPictureBox.TabIndex = 19;
            LogoPictureBox.TabStop = false;
            // 
            // panel2
            // 
            panel2.AutoSize = true;
            panel2.Controls.Add(panel3);
            panel2.Controls.Add(chkSaveCredentials);
            panel2.Controls.Add(txtPassword);
            panel2.Controls.Add(lblPassword);
            panel2.Controls.Add(txtUser);
            panel2.Controls.Add(lblUser);
            panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            panel2.Location = new System.Drawing.Point(72, 15);
            panel2.Name = "panel2";
            panel2.Padding = new System.Windows.Forms.Padding(20, 0, 0, 0);
            panel2.Size = new System.Drawing.Size(325, 171);
            panel2.TabIndex = 18;
            // 
            // panel3
            // 
            panel3.Controls.Add(cmdOk);
            panel3.Controls.Add(panel4);
            panel3.Controls.Add(cmdCancel);
            panel3.Dock = System.Windows.Forms.DockStyle.Top;
            panel3.Location = new System.Drawing.Point(20, 120);
            panel3.Name = "panel3";
            panel3.Padding = new System.Windows.Forms.Padding(0, 20, 0, 0);
            panel3.Size = new System.Drawing.Size(305, 50);
            panel3.TabIndex = 30;
            // 
            // cmdOk
            // 
            cmdOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            cmdOk.Dock = System.Windows.Forms.DockStyle.Right;
            cmdOk.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            cmdOk.Location = new System.Drawing.Point(119, 20);
            cmdOk.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            cmdOk.Name = "cmdOk";
            cmdOk.Size = new System.Drawing.Size(88, 30);
            cmdOk.TabIndex = 36;
            cmdOk.Text = "Ok";
            cmdOk.UseVisualStyleBackColor = true;
            cmdOk.Click += cmdOk_Click;
            // 
            // panel4
            // 
            panel4.Dock = System.Windows.Forms.DockStyle.Right;
            panel4.Location = new System.Drawing.Point(207, 20);
            panel4.Name = "panel4";
            panel4.Size = new System.Drawing.Size(10, 30);
            panel4.TabIndex = 35;
            // 
            // cmdCancel
            // 
            cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            cmdCancel.Dock = System.Windows.Forms.DockStyle.Right;
            cmdCancel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            cmdCancel.Location = new System.Drawing.Point(217, 20);
            cmdCancel.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            cmdCancel.Name = "cmdCancel";
            cmdCancel.Size = new System.Drawing.Size(88, 30);
            cmdCancel.TabIndex = 34;
            cmdCancel.Text = "Cancel";
            cmdCancel.UseVisualStyleBackColor = true;
            cmdCancel.Click += cmdCancel_Click;
            // 
            // chkSaveCredentials
            // 
            chkSaveCredentials.AutoSize = true;
            chkSaveCredentials.Dock = System.Windows.Forms.DockStyle.Top;
            chkSaveCredentials.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            chkSaveCredentials.Location = new System.Drawing.Point(20, 86);
            chkSaveCredentials.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            chkSaveCredentials.Name = "chkSaveCredentials";
            chkSaveCredentials.Padding = new System.Windows.Forms.Padding(0, 15, 0, 0);
            chkSaveCredentials.Size = new System.Drawing.Size(305, 34);
            chkSaveCredentials.TabIndex = 29;
            chkSaveCredentials.Text = "Save credentials";
            chkSaveCredentials.UseVisualStyleBackColor = true;
            // 
            // txtPassword
            // 
            txtPassword.Dock = System.Windows.Forms.DockStyle.Top;
            txtPassword.Location = new System.Drawing.Point(20, 63);
            txtPassword.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            txtPassword.Name = "txtPassword";
            txtPassword.PasswordChar = '*';
            txtPassword.Size = new System.Drawing.Size(305, 23);
            txtPassword.TabIndex = 27;
            txtPassword.TextChanged += txtPassword_TextChanged;
            // 
            // lblPassword
            // 
            lblPassword.AutoSize = true;
            lblPassword.Dock = System.Windows.Forms.DockStyle.Top;
            lblPassword.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            lblPassword.Location = new System.Drawing.Point(20, 38);
            lblPassword.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            lblPassword.Name = "lblPassword";
            lblPassword.Padding = new System.Windows.Forms.Padding(0, 10, 0, 0);
            lblPassword.Size = new System.Drawing.Size(57, 25);
            lblPassword.TabIndex = 28;
            lblPassword.Text = "Password";
            // 
            // txtUser
            // 
            txtUser.Dock = System.Windows.Forms.DockStyle.Top;
            txtUser.Location = new System.Drawing.Point(20, 15);
            txtUser.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            txtUser.Name = "txtUser";
            txtUser.Size = new System.Drawing.Size(305, 23);
            txtUser.TabIndex = 26;
            txtUser.TextChanged += txtUser_TextChanged;
            // 
            // lblUser
            // 
            lblUser.AutoSize = true;
            lblUser.Dock = System.Windows.Forms.DockStyle.Top;
            lblUser.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            lblUser.Location = new System.Drawing.Point(20, 0);
            lblUser.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            lblUser.Name = "lblUser";
            lblUser.Size = new System.Drawing.Size(30, 15);
            lblUser.TabIndex = 25;
            lblUser.Text = "User";
            // 
            // Login
            // 
            AcceptButton = cmdOk;
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            AutoSize = true;
            BackColor = System.Drawing.Color.White;
            CancelButton = cmdCancel;
            ClientSize = new System.Drawing.Size(412, 201);
            Controls.Add(panel2);
            Controls.Add(pLogo);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            Name = "Login";
            Padding = new System.Windows.Forms.Padding(15);
            Text = "Login";
            Paint += Login_Paint;
            pLogo.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)LogoPictureBox).EndInit();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            panel3.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Panel pLogo;
        internal System.Windows.Forms.PictureBox LogoPictureBox;
        private System.Windows.Forms.Panel panel2;
        internal System.Windows.Forms.CheckBox chkSaveCredentials;
        internal System.Windows.Forms.TextBox txtPassword;
        internal System.Windows.Forms.Label lblPassword;
        internal System.Windows.Forms.TextBox txtUser;
        internal System.Windows.Forms.Label lblUser;
        private System.Windows.Forms.Panel panel3;
        internal System.Windows.Forms.Button cmdOk;
        private System.Windows.Forms.Panel panel4;
        internal System.Windows.Forms.Button cmdCancel;
    }
}