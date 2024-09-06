namespace CDCplus.Controls
{
	partial class SessionEntry
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
			pSessionContent = new System.Windows.Forms.Panel();
			SuspendLayout();
			// 
			// pSessionContent
			// 
			pSessionContent.BackColor = System.Drawing.Color.White;
			pSessionContent.Dock = System.Windows.Forms.DockStyle.Fill;
			pSessionContent.Location = new System.Drawing.Point(4, 3);
			pSessionContent.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			pSessionContent.Name = "pSessionContent";
			pSessionContent.Size = new System.Drawing.Size(492, 45);
			pSessionContent.TabIndex = 0;
			// 
			// SessionEntry
			// 
			AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.Transparent;
			Controls.Add(pSessionContent);
			Margin = new System.Windows.Forms.Padding(0);
			Name = "SessionEntry";
			Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
			Size = new System.Drawing.Size(500, 51);
			ResumeLayout(false);
		}

		#endregion

		private System.Windows.Forms.Panel pSessionContent;
	}
}
