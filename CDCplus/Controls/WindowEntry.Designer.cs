namespace CDCplus.Controls
{
    partial class WindowEntry
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WindowEntry));
            pbDesktop = new System.Windows.Forms.PictureBox();
            panel4 = new System.Windows.Forms.Panel();
            cmdBringToFront = new System.Windows.Forms.Button();
            cmdCloseWindow = new System.Windows.Forms.Button();
            pbAdmin = new System.Windows.Forms.PictureBox();
            lblPath = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)pbDesktop).BeginInit();
            panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pbAdmin).BeginInit();
            SuspendLayout();
            // 
            // pbDesktop
            // 
            pbDesktop.Dock = System.Windows.Forms.DockStyle.Left;
            pbDesktop.Image = (System.Drawing.Image)resources.GetObject("pbDesktop.Image");
            pbDesktop.Location = new System.Drawing.Point(46, 3);
            pbDesktop.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            pbDesktop.Name = "pbDesktop";
            pbDesktop.Size = new System.Drawing.Size(37, 38);
            pbDesktop.TabIndex = 2;
            pbDesktop.TabStop = false;
            // 
            // panel4
            // 
            panel4.BackColor = System.Drawing.Color.Transparent;
            panel4.Controls.Add(cmdBringToFront);
            panel4.Controls.Add(cmdCloseWindow);
            panel4.Dock = System.Windows.Forms.DockStyle.Right;
            panel4.Location = new System.Drawing.Point(394, 3);
            panel4.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            panel4.Name = "panel4";
            panel4.Padding = new System.Windows.Forms.Padding(0, 1, 4, 8);
            panel4.Size = new System.Drawing.Size(65, 38);
            panel4.TabIndex = 16;
            // 
            // cmdBringToFront
            // 
            cmdBringToFront.Dock = System.Windows.Forms.DockStyle.Right;
            cmdBringToFront.FlatAppearance.BorderSize = 0;
            cmdBringToFront.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DeepSkyBlue;
            cmdBringToFront.FlatAppearance.MouseOverBackColor = System.Drawing.Color.PowderBlue;
            cmdBringToFront.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            cmdBringToFront.Image = (System.Drawing.Image)resources.GetObject("cmdBringToFront.Image");
            cmdBringToFront.Location = new System.Drawing.Point(5, 1);
            cmdBringToFront.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            cmdBringToFront.Name = "cmdBringToFront";
            cmdBringToFront.Size = new System.Drawing.Size(28, 29);
            cmdBringToFront.TabIndex = 6;
            cmdBringToFront.UseVisualStyleBackColor = true;
            cmdBringToFront.Click += cmdBringToFront_Click;
            // 
            // cmdCloseWindow
            // 
            cmdCloseWindow.Dock = System.Windows.Forms.DockStyle.Right;
            cmdCloseWindow.FlatAppearance.BorderSize = 0;
            cmdCloseWindow.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LightCoral;
            cmdCloseWindow.FlatAppearance.MouseOverBackColor = System.Drawing.Color.MistyRose;
            cmdCloseWindow.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            cmdCloseWindow.Image = (System.Drawing.Image)resources.GetObject("cmdCloseWindow.Image");
            cmdCloseWindow.Location = new System.Drawing.Point(33, 1);
            cmdCloseWindow.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            cmdCloseWindow.Name = "cmdCloseWindow";
            cmdCloseWindow.Size = new System.Drawing.Size(28, 29);
            cmdCloseWindow.TabIndex = 5;
            cmdCloseWindow.UseVisualStyleBackColor = true;
            cmdCloseWindow.Click += cmdCloseWindow_Click;
            // 
            // pbAdmin
            // 
            pbAdmin.Dock = System.Windows.Forms.DockStyle.Left;
            pbAdmin.Image = (System.Drawing.Image)resources.GetObject("pbAdmin.Image");
            pbAdmin.Location = new System.Drawing.Point(83, 3);
            pbAdmin.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            pbAdmin.Name = "pbAdmin";
            pbAdmin.Size = new System.Drawing.Size(37, 38);
            pbAdmin.TabIndex = 17;
            pbAdmin.TabStop = false;
            // 
            // lblPath
            // 
            lblPath.AutoSize = true;
            lblPath.Dock = System.Windows.Forms.DockStyle.Fill;
            lblPath.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            lblPath.Location = new System.Drawing.Point(120, 3);
            lblPath.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            lblPath.Name = "lblPath";
            lblPath.Size = new System.Drawing.Size(33, 16);
            lblPath.TabIndex = 18;
            lblPath.Text = "path";
            // 
            // WindowEntry
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackColor = System.Drawing.Color.Transparent;
            Controls.Add(lblPath);
            Controls.Add(pbAdmin);
            Controls.Add(panel4);
            Controls.Add(pbDesktop);
            Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            Name = "WindowEntry";
            Padding = new System.Windows.Forms.Padding(46, 3, 4, 3);
            Size = new System.Drawing.Size(463, 44);
            ((System.ComponentModel.ISupportInitialize)pbDesktop).EndInit();
            panel4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pbAdmin).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private System.Windows.Forms.PictureBox pbDesktop;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Button cmdBringToFront;
        private System.Windows.Forms.Button cmdCloseWindow;
        private System.Windows.Forms.PictureBox pbAdmin;
        private System.Windows.Forms.Label lblPath;
    }
}
