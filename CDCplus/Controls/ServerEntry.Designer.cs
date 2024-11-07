namespace CDCplus.Controls
{
	partial class ServerEntry
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ServerEntry));
            gbServer = new System.Windows.Forms.GroupBox();
            pSessions = new System.Windows.Forms.Panel();
            pTitle = new System.Windows.Forms.Panel();
            lblVersionBuild = new System.Windows.Forms.Label();
            cmdConnect = new System.Windows.Forms.Button();
            panel3 = new System.Windows.Forms.Panel();
            pbOffline = new System.Windows.Forms.PictureBox();
            pbOnline = new System.Windows.Forms.PictureBox();
            gbServer.SuspendLayout();
            pTitle.SuspendLayout();
            panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pbOffline).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pbOnline).BeginInit();
            SuspendLayout();
            // 
            // gbServer
            // 
            gbServer.AutoSize = true;
            gbServer.Controls.Add(pSessions);
            gbServer.Controls.Add(pTitle);
            gbServer.Dock = System.Windows.Forms.DockStyle.Fill;
            gbServer.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            gbServer.ForeColor = System.Drawing.Color.DimGray;
            gbServer.Location = new System.Drawing.Point(4, 4);
            gbServer.Name = "gbServer";
            gbServer.Size = new System.Drawing.Size(503, 61);
            gbServer.TabIndex = 0;
            gbServer.TabStop = false;
            gbServer.Text = "<server name>";
            // 
            // pSessions
            // 
            pSessions.AutoSize = true;
            pSessions.Dock = System.Windows.Forms.DockStyle.Fill;
            pSessions.Location = new System.Drawing.Point(3, 54);
            pSessions.Name = "pSessions";
            pSessions.Size = new System.Drawing.Size(497, 4);
            pSessions.TabIndex = 5;
            // 
            // pTitle
            // 
            pTitle.Controls.Add(lblVersionBuild);
            pTitle.Controls.Add(cmdConnect);
            pTitle.Controls.Add(panel3);
            pTitle.Dock = System.Windows.Forms.DockStyle.Top;
            pTitle.Location = new System.Drawing.Point(3, 23);
            pTitle.Margin = new System.Windows.Forms.Padding(4);
            pTitle.Name = "pTitle";
            pTitle.Padding = new System.Windows.Forms.Padding(3, 0, 0, 0);
            pTitle.Size = new System.Drawing.Size(497, 31);
            pTitle.TabIndex = 4;
            // 
            // lblVersionBuild
            // 
            lblVersionBuild.AutoSize = true;
            lblVersionBuild.BackColor = System.Drawing.Color.Transparent;
            lblVersionBuild.Dock = System.Windows.Forms.DockStyle.Top;
            lblVersionBuild.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            lblVersionBuild.Location = new System.Drawing.Point(21, 0);
            lblVersionBuild.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            lblVersionBuild.Name = "lblVersionBuild";
            lblVersionBuild.Padding = new System.Windows.Forms.Padding(0, 1, 0, 0);
            lblVersionBuild.Size = new System.Drawing.Size(74, 14);
            lblVersionBuild.TabIndex = 19;
            lblVersionBuild.Text = "version / build";
            // 
            // cmdConnect
            // 
            cmdConnect.Dock = System.Windows.Forms.DockStyle.Right;
            cmdConnect.FlatAppearance.BorderSize = 0;
            cmdConnect.FlatAppearance.MouseDownBackColor = System.Drawing.Color.PaleGreen;
            cmdConnect.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Honeydew;
            cmdConnect.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            cmdConnect.Image = (System.Drawing.Image)resources.GetObject("cmdConnect.Image");
            cmdConnect.Location = new System.Drawing.Point(469, 0);
            cmdConnect.Margin = new System.Windows.Forms.Padding(4);
            cmdConnect.Name = "cmdConnect";
            cmdConnect.Size = new System.Drawing.Size(28, 31);
            cmdConnect.TabIndex = 18;
            cmdConnect.UseVisualStyleBackColor = true;
            cmdConnect.Click += cmdConnect_Click;
            // 
            // panel3
            // 
            panel3.Controls.Add(pbOffline);
            panel3.Controls.Add(pbOnline);
            panel3.Dock = System.Windows.Forms.DockStyle.Left;
            panel3.Location = new System.Drawing.Point(3, 0);
            panel3.Margin = new System.Windows.Forms.Padding(4);
            panel3.Name = "panel3";
            panel3.Padding = new System.Windows.Forms.Padding(0, 4, 0, 0);
            panel3.Size = new System.Drawing.Size(18, 31);
            panel3.TabIndex = 17;
            // 
            // pbOffline
            // 
            pbOffline.Image = (System.Drawing.Image)resources.GetObject("pbOffline.Image");
            pbOffline.Location = new System.Drawing.Point(0, 2);
            pbOffline.Margin = new System.Windows.Forms.Padding(4);
            pbOffline.Name = "pbOffline";
            pbOffline.Size = new System.Drawing.Size(18, 19);
            pbOffline.TabIndex = 2;
            pbOffline.TabStop = false;
            // 
            // pbOnline
            // 
            pbOnline.Image = (System.Drawing.Image)resources.GetObject("pbOnline.Image");
            pbOnline.Location = new System.Drawing.Point(0, 2);
            pbOnline.Margin = new System.Windows.Forms.Padding(4);
            pbOnline.Name = "pbOnline";
            pbOnline.Size = new System.Drawing.Size(18, 19);
            pbOnline.TabIndex = 0;
            pbOnline.TabStop = false;
            // 
            // ServerEntry
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            AutoSize = true;
            Controls.Add(gbServer);
            Margin = new System.Windows.Forms.Padding(4);
            Name = "ServerEntry";
            Padding = new System.Windows.Forms.Padding(4);
            Size = new System.Drawing.Size(511, 69);
            gbServer.ResumeLayout(false);
            gbServer.PerformLayout();
            pTitle.ResumeLayout(false);
            pTitle.PerformLayout();
            panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pbOffline).EndInit();
            ((System.ComponentModel.ISupportInitialize)pbOnline).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.GroupBox gbServer;
		private System.Windows.Forms.Panel pSessions;
		private System.Windows.Forms.Panel pTitle;
		private System.Windows.Forms.Label lblVersionBuild;
		private System.Windows.Forms.Button cmdConnect;
		private System.Windows.Forms.Panel panel3;
		private System.Windows.Forms.PictureBox pbOffline;
		private System.Windows.Forms.PictureBox pbOnline;
	}
}
