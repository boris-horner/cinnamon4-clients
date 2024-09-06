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
			chkSaveCredentials = new System.Windows.Forms.CheckBox();
			LogoPictureBox = new System.Windows.Forms.PictureBox();
			cmdCancel = new System.Windows.Forms.Button();
			cmdOk = new System.Windows.Forms.Button();
			txtPassword = new System.Windows.Forms.TextBox();
			lblPassword = new System.Windows.Forms.Label();
			txtUser = new System.Windows.Forms.TextBox();
			lblUser = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)LogoPictureBox).BeginInit();
			SuspendLayout();
			// 
			// chkSaveCredentials
			// 
			chkSaveCredentials.AutoSize = true;
			chkSaveCredentials.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			chkSaveCredentials.Location = new System.Drawing.Point(118, 141);
			chkSaveCredentials.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			chkSaveCredentials.Name = "chkSaveCredentials";
			chkSaveCredentials.Size = new System.Drawing.Size(110, 19);
			chkSaveCredentials.TabIndex = 14;
			chkSaveCredentials.Text = "Save credentials";
			chkSaveCredentials.UseVisualStyleBackColor = true;
			// 
			// LogoPictureBox
			// 
			LogoPictureBox.Image = (System.Drawing.Image)resources.GetObject("LogoPictureBox.Image");
			LogoPictureBox.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			LogoPictureBox.Location = new System.Drawing.Point(14, 28);
			LogoPictureBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			LogoPictureBox.Name = "LogoPictureBox";
			LogoPictureBox.Size = new System.Drawing.Size(81, 92);
			LogoPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			LogoPictureBox.TabIndex = 17;
			LogoPictureBox.TabStop = false;
			// 
			// cmdCancel
			// 
			cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			cmdCancel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			cmdCancel.Location = new System.Drawing.Point(310, 190);
			cmdCancel.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			cmdCancel.Name = "cmdCancel";
			cmdCancel.Size = new System.Drawing.Size(88, 27);
			cmdCancel.TabIndex = 16;
			cmdCancel.Text = "Cancel";
			cmdCancel.UseVisualStyleBackColor = true;
			cmdCancel.Click += cmdCancel_Click;
			// 
			// cmdOk
			// 
			cmdOk.DialogResult = System.Windows.Forms.DialogResult.OK;
			cmdOk.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			cmdOk.Location = new System.Drawing.Point(216, 190);
			cmdOk.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			cmdOk.Name = "cmdOk";
			cmdOk.Size = new System.Drawing.Size(88, 27);
			cmdOk.TabIndex = 15;
			cmdOk.Text = "Ok";
			cmdOk.UseVisualStyleBackColor = true;
			cmdOk.Click += cmdOk_Click;
			// 
			// txtPassword
			// 
			txtPassword.Location = new System.Drawing.Point(118, 96);
			txtPassword.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			txtPassword.Name = "txtPassword";
			txtPassword.PasswordChar = '*';
			txtPassword.Size = new System.Drawing.Size(285, 23);
			txtPassword.TabIndex = 12;
			txtPassword.TextChanged += txtPassword_TextChanged;
			// 
			// lblPassword
			// 
			lblPassword.AutoSize = true;
			lblPassword.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			lblPassword.Location = new System.Drawing.Point(114, 77);
			lblPassword.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			lblPassword.Name = "lblPassword";
			lblPassword.Size = new System.Drawing.Size(57, 15);
			lblPassword.TabIndex = 13;
			lblPassword.Text = "Password";
			// 
			// txtUser
			// 
			txtUser.Location = new System.Drawing.Point(118, 35);
			txtUser.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			txtUser.Name = "txtUser";
			txtUser.Size = new System.Drawing.Size(285, 23);
			txtUser.TabIndex = 11;
			txtUser.TextChanged += txtUser_TextChanged;
			// 
			// lblUser
			// 
			lblUser.AutoSize = true;
			lblUser.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			lblUser.Location = new System.Drawing.Point(114, 16);
			lblUser.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			lblUser.Name = "lblUser";
			lblUser.Size = new System.Drawing.Size(30, 15);
			lblUser.TabIndex = 10;
			lblUser.Text = "User";
			// 
			// Login
			// 
			AcceptButton = cmdOk;
			AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.White;
			CancelButton = cmdCancel;
			ClientSize = new System.Drawing.Size(412, 231);
			Controls.Add(chkSaveCredentials);
			Controls.Add(LogoPictureBox);
			Controls.Add(cmdCancel);
			Controls.Add(cmdOk);
			Controls.Add(txtPassword);
			Controls.Add(lblPassword);
			Controls.Add(txtUser);
			Controls.Add(lblUser);
			FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			Name = "Login";
			Text = "Login";
			((System.ComponentModel.ISupportInitialize)LogoPictureBox).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		internal System.Windows.Forms.CheckBox chkSaveCredentials;
		internal System.Windows.Forms.PictureBox LogoPictureBox;
		internal System.Windows.Forms.Button cmdCancel;
		internal System.Windows.Forms.Button cmdOk;
		internal System.Windows.Forms.TextBox txtPassword;
		internal System.Windows.Forms.Label lblPassword;
		internal System.Windows.Forms.TextBox txtUser;
		internal System.Windows.Forms.Label lblUser;
	}
}