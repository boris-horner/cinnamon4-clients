namespace CDCplus.Controls
{
	partial class UserEntry
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UserEntry));
			pictureBox1 = new System.Windows.Forms.PictureBox();
			lblUserName = new System.Windows.Forms.Label();
			panel4 = new System.Windows.Forms.Panel();
			cmdSetPassword = new System.Windows.Forms.Button();
			cmdAddAdminWindow = new System.Windows.Forms.Button();
			cmdAddWindow = new System.Windows.Forms.Button();
			cmdCloseSession = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
			panel4.SuspendLayout();
			SuspendLayout();
			// 
			// pictureBox1
			// 
			pictureBox1.Dock = System.Windows.Forms.DockStyle.Left;
			pictureBox1.Image = (System.Drawing.Image)resources.GetObject("pictureBox1.Image");
			pictureBox1.Location = new System.Drawing.Point(4, 3);
			pictureBox1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			pictureBox1.Name = "pictureBox1";
			pictureBox1.Size = new System.Drawing.Size(37, 38);
			pictureBox1.TabIndex = 0;
			pictureBox1.TabStop = false;
			// 
			// lblUserName
			// 
			lblUserName.AutoSize = true;
			lblUserName.Dock = System.Windows.Forms.DockStyle.Left;
			lblUserName.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
			lblUserName.ForeColor = System.Drawing.Color.DarkGray;
			lblUserName.Location = new System.Drawing.Point(41, 3);
			lblUserName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			lblUserName.Name = "lblUserName";
			lblUserName.Size = new System.Drawing.Size(82, 18);
			lblUserName.TabIndex = 1;
			lblUserName.Text = "username";
			// 
			// panel4
			// 
			panel4.BackColor = System.Drawing.Color.Transparent;
			panel4.Controls.Add(cmdSetPassword);
			panel4.Controls.Add(cmdAddAdminWindow);
			panel4.Controls.Add(cmdAddWindow);
			panel4.Controls.Add(cmdCloseSession);
			panel4.Dock = System.Windows.Forms.DockStyle.Top;
			panel4.Location = new System.Drawing.Point(123, 3);
			panel4.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			panel4.Name = "panel4";
			panel4.Padding = new System.Windows.Forms.Padding(0, 1, 4, 0);
			panel4.Size = new System.Drawing.Size(806, 29);
			panel4.TabIndex = 13;
			// 
			// cmdSetPassword
			// 
			cmdSetPassword.Dock = System.Windows.Forms.DockStyle.Right;
			cmdSetPassword.FlatAppearance.BorderSize = 0;
			cmdSetPassword.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gainsboro;
			cmdSetPassword.FlatAppearance.MouseOverBackColor = System.Drawing.Color.WhiteSmoke;
			cmdSetPassword.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			cmdSetPassword.Image = (System.Drawing.Image)resources.GetObject("cmdSetPassword.Image");
			cmdSetPassword.Location = new System.Drawing.Point(690, 1);
			cmdSetPassword.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			cmdSetPassword.Name = "cmdSetPassword";
			cmdSetPassword.Size = new System.Drawing.Size(28, 28);
			cmdSetPassword.TabIndex = 9;
			cmdSetPassword.UseVisualStyleBackColor = true;
			cmdSetPassword.Click += cmdSetPassword_Click;
			// 
			// cmdAddAdminWindow
			// 
			cmdAddAdminWindow.Dock = System.Windows.Forms.DockStyle.Right;
			cmdAddAdminWindow.FlatAppearance.BorderSize = 0;
			cmdAddAdminWindow.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Khaki;
			cmdAddAdminWindow.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Cornsilk;
			cmdAddAdminWindow.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			cmdAddAdminWindow.Image = (System.Drawing.Image)resources.GetObject("cmdAddAdminWindow.Image");
			cmdAddAdminWindow.Location = new System.Drawing.Point(718, 1);
			cmdAddAdminWindow.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			cmdAddAdminWindow.Name = "cmdAddAdminWindow";
			cmdAddAdminWindow.Size = new System.Drawing.Size(28, 28);
			cmdAddAdminWindow.TabIndex = 8;
			cmdAddAdminWindow.UseVisualStyleBackColor = true;
			cmdAddAdminWindow.Click += cmdAddAdminWindow_Click;
			// 
			// cmdAddWindow
			// 
			cmdAddWindow.Dock = System.Windows.Forms.DockStyle.Right;
			cmdAddWindow.FlatAppearance.BorderSize = 0;
			cmdAddWindow.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LightBlue;
			cmdAddWindow.FlatAppearance.MouseOverBackColor = System.Drawing.Color.AliceBlue;
			cmdAddWindow.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			cmdAddWindow.Image = (System.Drawing.Image)resources.GetObject("cmdAddWindow.Image");
			cmdAddWindow.Location = new System.Drawing.Point(746, 1);
			cmdAddWindow.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			cmdAddWindow.Name = "cmdAddWindow";
			cmdAddWindow.Size = new System.Drawing.Size(28, 28);
			cmdAddWindow.TabIndex = 4;
			cmdAddWindow.UseVisualStyleBackColor = true;
			cmdAddWindow.Click += cmdAddWindow_Click;
			// 
			// cmdCloseSession
			// 
			cmdCloseSession.Dock = System.Windows.Forms.DockStyle.Right;
			cmdCloseSession.FlatAppearance.BorderSize = 0;
			cmdCloseSession.FlatAppearance.MouseDownBackColor = System.Drawing.Color.PeachPuff;
			cmdCloseSession.FlatAppearance.MouseOverBackColor = System.Drawing.Color.AntiqueWhite;
			cmdCloseSession.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			cmdCloseSession.Image = (System.Drawing.Image)resources.GetObject("cmdCloseSession.Image");
			cmdCloseSession.Location = new System.Drawing.Point(774, 1);
			cmdCloseSession.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			cmdCloseSession.Name = "cmdCloseSession";
			cmdCloseSession.Size = new System.Drawing.Size(28, 28);
			cmdCloseSession.TabIndex = 3;
			cmdCloseSession.UseVisualStyleBackColor = true;
			cmdCloseSession.Click += cmdCloseSession_Click;
			// 
			// UserEntry
			// 
			AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.Transparent;
			Controls.Add(panel4);
			Controls.Add(lblUserName);
			Controls.Add(pictureBox1);
			Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			Name = "UserEntry";
			Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
			Size = new System.Drawing.Size(933, 44);
			((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
			panel4.ResumeLayout(false);
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.Label lblUserName;
		private System.Windows.Forms.Panel panel4;
		private System.Windows.Forms.Button cmdCloseSession;
		private System.Windows.Forms.Button cmdAddWindow;
		private System.Windows.Forms.Button cmdSetPassword;
		private System.Windows.Forms.Button cmdAddAdminWindow;
	}
}
