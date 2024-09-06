namespace CDCplus
{
	partial class ServerHub
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ServerHub));
            panel1 = new System.Windows.Forms.Panel();
            panel2 = new System.Windows.Forms.Panel();
            panel3 = new System.Windows.Forms.Panel();
            cmdReloadDiscServers = new System.Windows.Forms.Button();
            cmdAbout = new System.Windows.Forms.Button();
            pictureBox1 = new System.Windows.Forms.PictureBox();
            pEntries = new System.Windows.Forms.Panel();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = System.Drawing.Color.WhiteSmoke;
            panel1.Controls.Add(panel2);
            panel1.Controls.Add(pictureBox1);
            panel1.Dock = System.Windows.Forms.DockStyle.Top;
            panel1.Location = new System.Drawing.Point(0, 0);
            panel1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            panel1.Name = "panel1";
            panel1.Padding = new System.Windows.Forms.Padding(4, 3, 0, 3);
            panel1.Size = new System.Drawing.Size(833, 65);
            panel1.TabIndex = 0;
            // 
            // panel2
            // 
            panel2.BackColor = System.Drawing.Color.Transparent;
            panel2.Controls.Add(panel3);
            panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            panel2.Location = new System.Drawing.Point(202, 3);
            panel2.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            panel2.Name = "panel2";
            panel2.Size = new System.Drawing.Size(631, 59);
            panel2.TabIndex = 2;
            // 
            // panel3
            // 
            panel3.Controls.Add(cmdReloadDiscServers);
            panel3.Controls.Add(cmdAbout);
            panel3.Dock = System.Windows.Forms.DockStyle.Top;
            panel3.Location = new System.Drawing.Point(0, 0);
            panel3.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            panel3.Name = "panel3";
            panel3.Size = new System.Drawing.Size(631, 44);
            panel3.TabIndex = 0;
            // 
            // cmdReloadDiscServers
            // 
            cmdReloadDiscServers.BackColor = System.Drawing.Color.Transparent;
            cmdReloadDiscServers.Dock = System.Windows.Forms.DockStyle.Right;
            cmdReloadDiscServers.FlatAppearance.BorderSize = 0;
            cmdReloadDiscServers.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LimeGreen;
            cmdReloadDiscServers.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightGreen;
            cmdReloadDiscServers.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            cmdReloadDiscServers.Image = (System.Drawing.Image)resources.GetObject("cmdReloadDiscServers.Image");
            cmdReloadDiscServers.Location = new System.Drawing.Point(543, 0);
            cmdReloadDiscServers.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            cmdReloadDiscServers.Name = "cmdReloadDiscServers";
            cmdReloadDiscServers.Size = new System.Drawing.Size(44, 44);
            cmdReloadDiscServers.TabIndex = 2;
            cmdReloadDiscServers.UseVisualStyleBackColor = false;
            cmdReloadDiscServers.Click += cmdReloadDiscServers_Click;
            // 
            // cmdAbout
            // 
            cmdAbout.BackColor = System.Drawing.Color.Transparent;
            cmdAbout.Dock = System.Windows.Forms.DockStyle.Right;
            cmdAbout.FlatAppearance.BorderSize = 0;
            cmdAbout.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DeepSkyBlue;
            cmdAbout.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightSkyBlue;
            cmdAbout.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            cmdAbout.Image = (System.Drawing.Image)resources.GetObject("cmdAbout.Image");
            cmdAbout.Location = new System.Drawing.Point(587, 0);
            cmdAbout.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            cmdAbout.Name = "cmdAbout";
            cmdAbout.Size = new System.Drawing.Size(44, 44);
            cmdAbout.TabIndex = 1;
            cmdAbout.UseVisualStyleBackColor = false;
            cmdAbout.Click += cmdAbout_Click;
            // 
            // pictureBox1
            // 
            pictureBox1.BackColor = System.Drawing.Color.Transparent;
            pictureBox1.Dock = System.Windows.Forms.DockStyle.Left;
            pictureBox1.Image = (System.Drawing.Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.InitialImage = (System.Drawing.Image)resources.GetObject("pictureBox1.InitialImage");
            pictureBox1.Location = new System.Drawing.Point(4, 3);
            pictureBox1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new System.Drawing.Size(198, 59);
            pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 1;
            pictureBox1.TabStop = false;
            // 
            // pEntries
            // 
            pEntries.BackColor = System.Drawing.Color.WhiteSmoke;
            pEntries.Dock = System.Windows.Forms.DockStyle.Fill;
            pEntries.Location = new System.Drawing.Point(0, 65);
            pEntries.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            pEntries.Name = "pEntries";
            pEntries.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            pEntries.Size = new System.Drawing.Size(833, 4);
            pEntries.TabIndex = 1;
            // 
            // ServerHub
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(833, 69);
            Controls.Add(pEntries);
            Controls.Add(panel1);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
            Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            Name = "ServerHub";
            Text = "CDCplus";
            Shown += ServerHub_Shown;
            panel1.ResumeLayout(false);
            panel2.ResumeLayout(false);
            panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Panel pEntries;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.Panel panel3;
		private System.Windows.Forms.Button cmdAbout;
		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.Button cmdReloadDiscServers;
	}
}

