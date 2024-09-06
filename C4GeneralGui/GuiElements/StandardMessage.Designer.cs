
namespace C4GeneralGui.GuiElements
{
    partial class StandardMessage
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StandardMessage));
            this.pButtons = new System.Windows.Forms.Panel();
            this.pHelp = new System.Windows.Forms.Panel();
            this.cmdHelp = new System.Windows.Forms.Button();
            this.pDetails = new System.Windows.Forms.Panel();
            this.cmdDetails = new System.Windows.Forms.Button();
            this.pException = new System.Windows.Forms.Panel();
            this.txtStackTrace = new System.Windows.Forms.TextBox();
            this.lblExStackTrace = new System.Windows.Forms.Label();
            this.Panel3 = new System.Windows.Forms.Panel();
            this.txtExMessage = new System.Windows.Forms.TextBox();
            this.lblExMessage = new System.Windows.Forms.Label();
            this.Panel2 = new System.Windows.Forms.Panel();
            this.txtExType = new System.Windows.Forms.TextBox();
            this.lblExType = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.txtMessage = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pbCritical = new System.Windows.Forms.PictureBox();
            this.pbError = new System.Windows.Forms.PictureBox();
            this.pbWarning = new System.Windows.Forms.PictureBox();
            this.pbInfo = new System.Windows.Forms.PictureBox();
            this.pButtons.SuspendLayout();
            this.pHelp.SuspendLayout();
            this.pDetails.SuspendLayout();
            this.pException.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbCritical)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbError)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbWarning)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbInfo)).BeginInit();
            this.SuspendLayout();
            // 
            // pButtons
            // 
            this.pButtons.Controls.Add(this.pHelp);
            this.pButtons.Controls.Add(this.pDetails);
            this.pButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pButtons.Location = new System.Drawing.Point(0, 567);
            this.pButtons.Name = "pButtons";
            this.pButtons.Size = new System.Drawing.Size(644, 50);
            this.pButtons.TabIndex = 18;
            // 
            // pHelp
            // 
            this.pHelp.Controls.Add(this.cmdHelp);
            this.pHelp.Dock = System.Windows.Forms.DockStyle.Right;
            this.pHelp.Location = new System.Drawing.Point(524, 0);
            this.pHelp.Name = "pHelp";
            this.pHelp.Padding = new System.Windows.Forms.Padding(8);
            this.pHelp.Size = new System.Drawing.Size(120, 50);
            this.pHelp.TabIndex = 24;
            // 
            // cmdHelp
            // 
            this.cmdHelp.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdHelp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmdHelp.Location = new System.Drawing.Point(8, 8);
            this.cmdHelp.Name = "cmdHelp";
            this.cmdHelp.Size = new System.Drawing.Size(104, 34);
            this.cmdHelp.TabIndex = 22;
            this.cmdHelp.Text = "Help";
            this.cmdHelp.UseVisualStyleBackColor = true;
            this.cmdHelp.Click += new System.EventHandler(this.CmdHelp_Click);
            // 
            // pDetails
            // 
            this.pDetails.Controls.Add(this.cmdDetails);
            this.pDetails.Dock = System.Windows.Forms.DockStyle.Left;
            this.pDetails.Location = new System.Drawing.Point(0, 0);
            this.pDetails.Name = "pDetails";
            this.pDetails.Padding = new System.Windows.Forms.Padding(8);
            this.pDetails.Size = new System.Drawing.Size(120, 50);
            this.pDetails.TabIndex = 22;
            // 
            // cmdDetails
            // 
            this.cmdDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmdDetails.Location = new System.Drawing.Point(8, 8);
            this.cmdDetails.Name = "cmdDetails";
            this.cmdDetails.Size = new System.Drawing.Size(104, 34);
            this.cmdDetails.TabIndex = 20;
            this.cmdDetails.Text = "Details";
            this.cmdDetails.UseVisualStyleBackColor = true;
            this.cmdDetails.Click += new System.EventHandler(this.cmdDetails_Click);
            // 
            // pException
            // 
            this.pException.Controls.Add(this.txtStackTrace);
            this.pException.Controls.Add(this.lblExStackTrace);
            this.pException.Controls.Add(this.Panel3);
            this.pException.Controls.Add(this.txtExMessage);
            this.pException.Controls.Add(this.lblExMessage);
            this.pException.Controls.Add(this.Panel2);
            this.pException.Controls.Add(this.txtExType);
            this.pException.Controls.Add(this.lblExType);
            this.pException.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pException.Location = new System.Drawing.Point(0, 116);
            this.pException.Margin = new System.Windows.Forms.Padding(4);
            this.pException.Name = "pException";
            this.pException.Size = new System.Drawing.Size(644, 451);
            this.pException.TabIndex = 21;
            // 
            // txtStackTrace
            // 
            this.txtStackTrace.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtStackTrace.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtStackTrace.Location = new System.Drawing.Point(0, 181);
            this.txtStackTrace.Margin = new System.Windows.Forms.Padding(4);
            this.txtStackTrace.Multiline = true;
            this.txtStackTrace.Name = "txtStackTrace";
            this.txtStackTrace.ReadOnly = true;
            this.txtStackTrace.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtStackTrace.Size = new System.Drawing.Size(644, 270);
            this.txtStackTrace.TabIndex = 21;
            // 
            // lblExStackTrace
            // 
            this.lblExStackTrace.AutoSize = true;
            this.lblExStackTrace.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblExStackTrace.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblExStackTrace.Location = new System.Drawing.Point(0, 164);
            this.lblExStackTrace.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblExStackTrace.Name = "lblExStackTrace";
            this.lblExStackTrace.Size = new System.Drawing.Size(90, 17);
            this.lblExStackTrace.TabIndex = 20;
            this.lblExStackTrace.Text = "Stack trace";
            // 
            // Panel3
            // 
            this.Panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.Panel3.Location = new System.Drawing.Point(0, 149);
            this.Panel3.Margin = new System.Windows.Forms.Padding(4);
            this.Panel3.Name = "Panel3";
            this.Panel3.Size = new System.Drawing.Size(644, 15);
            this.Panel3.TabIndex = 19;
            // 
            // txtExMessage
            // 
            this.txtExMessage.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtExMessage.Dock = System.Windows.Forms.DockStyle.Top;
            this.txtExMessage.Location = new System.Drawing.Point(0, 64);
            this.txtExMessage.Margin = new System.Windows.Forms.Padding(4);
            this.txtExMessage.Multiline = true;
            this.txtExMessage.Name = "txtExMessage";
            this.txtExMessage.ReadOnly = true;
            this.txtExMessage.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtExMessage.Size = new System.Drawing.Size(644, 85);
            this.txtExMessage.TabIndex = 16;
            // 
            // lblExMessage
            // 
            this.lblExMessage.AutoSize = true;
            this.lblExMessage.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblExMessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblExMessage.Location = new System.Drawing.Point(0, 47);
            this.lblExMessage.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblExMessage.Name = "lblExMessage";
            this.lblExMessage.Size = new System.Drawing.Size(147, 17);
            this.lblExMessage.TabIndex = 15;
            this.lblExMessage.Text = "Exception message";
            // 
            // Panel2
            // 
            this.Panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.Panel2.Location = new System.Drawing.Point(0, 32);
            this.Panel2.Margin = new System.Windows.Forms.Padding(4);
            this.Panel2.Name = "Panel2";
            this.Panel2.Size = new System.Drawing.Size(644, 15);
            this.Panel2.TabIndex = 11;
            // 
            // txtExType
            // 
            this.txtExType.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtExType.Dock = System.Windows.Forms.DockStyle.Top;
            this.txtExType.Location = new System.Drawing.Point(0, 17);
            this.txtExType.Margin = new System.Windows.Forms.Padding(4);
            this.txtExType.Name = "txtExType";
            this.txtExType.ReadOnly = true;
            this.txtExType.Size = new System.Drawing.Size(644, 15);
            this.txtExType.TabIndex = 10;
            // 
            // lblExType
            // 
            this.lblExType.AutoSize = true;
            this.lblExType.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblExType.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblExType.Location = new System.Drawing.Point(0, 0);
            this.lblExType.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblExType.Name = "lblExType";
            this.lblExType.Size = new System.Drawing.Size(114, 17);
            this.lblExType.TabIndex = 9;
            this.lblExType.Text = "Exception type";
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.txtMessage);
            this.panel4.Controls.Add(this.panel1);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.panel4.Size = new System.Drawing.Size(644, 116);
            this.panel4.TabIndex = 22;
            // 
            // txtMessage
            // 
            this.txtMessage.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtMessage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtMessage.Location = new System.Drawing.Point(76, 5);
            this.txtMessage.Margin = new System.Windows.Forms.Padding(4);
            this.txtMessage.Multiline = true;
            this.txtMessage.Name = "txtMessage";
            this.txtMessage.ReadOnly = true;
            this.txtMessage.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtMessage.Size = new System.Drawing.Size(568, 111);
            this.txtMessage.TabIndex = 23;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.pbCritical);
            this.panel1.Controls.Add(this.pbError);
            this.panel1.Controls.Add(this.pbWarning);
            this.panel1.Controls.Add(this.pbInfo);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 5);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(76, 111);
            this.panel1.TabIndex = 19;
            // 
            // pbCritical
            // 
            this.pbCritical.Image = ((System.Drawing.Image)(resources.GetObject("pbCritical.Image")));
            this.pbCritical.Location = new System.Drawing.Point(9, 9);
            this.pbCritical.Margin = new System.Windows.Forms.Padding(0);
            this.pbCritical.Name = "pbCritical";
            this.pbCritical.Size = new System.Drawing.Size(64, 59);
            this.pbCritical.TabIndex = 26;
            this.pbCritical.TabStop = false;
            // 
            // pbError
            // 
            this.pbError.Image = ((System.Drawing.Image)(resources.GetObject("pbError.Image")));
            this.pbError.Location = new System.Drawing.Point(9, 9);
            this.pbError.Margin = new System.Windows.Forms.Padding(0);
            this.pbError.Name = "pbError";
            this.pbError.Size = new System.Drawing.Size(64, 59);
            this.pbError.TabIndex = 25;
            this.pbError.TabStop = false;
            // 
            // pbWarning
            // 
            this.pbWarning.Image = ((System.Drawing.Image)(resources.GetObject("pbWarning.Image")));
            this.pbWarning.Location = new System.Drawing.Point(9, 9);
            this.pbWarning.Margin = new System.Windows.Forms.Padding(0);
            this.pbWarning.Name = "pbWarning";
            this.pbWarning.Size = new System.Drawing.Size(64, 59);
            this.pbWarning.TabIndex = 24;
            this.pbWarning.TabStop = false;
            // 
            // pbInfo
            // 
            this.pbInfo.Image = ((System.Drawing.Image)(resources.GetObject("pbInfo.Image")));
            this.pbInfo.Location = new System.Drawing.Point(9, 9);
            this.pbInfo.Margin = new System.Windows.Forms.Padding(0);
            this.pbInfo.Name = "pbInfo";
            this.pbInfo.Size = new System.Drawing.Size(64, 59);
            this.pbInfo.TabIndex = 23;
            this.pbInfo.TabStop = false;
            // 
            // StandardMessage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(644, 617);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.pException);
            this.Controls.Add(this.pButtons);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "StandardMessage";
            this.Text = "StandardMessage";
            this.pButtons.ResumeLayout(false);
            this.pHelp.ResumeLayout(false);
            this.pDetails.ResumeLayout(false);
            this.pException.ResumeLayout(false);
            this.pException.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbCritical)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbError)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbWarning)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbInfo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel pButtons;
        private System.Windows.Forms.Panel pHelp;
        internal System.Windows.Forms.Button cmdHelp;
        private System.Windows.Forms.Panel pDetails;
        internal System.Windows.Forms.Button cmdDetails;
        internal System.Windows.Forms.Panel pException;
        internal System.Windows.Forms.TextBox txtStackTrace;
        internal System.Windows.Forms.Label lblExStackTrace;
        internal System.Windows.Forms.Panel Panel3;
        internal System.Windows.Forms.TextBox txtExMessage;
        internal System.Windows.Forms.Label lblExMessage;
        internal System.Windows.Forms.Panel Panel2;
        internal System.Windows.Forms.TextBox txtExType;
        internal System.Windows.Forms.Label lblExType;
        private System.Windows.Forms.Panel panel4;
        internal System.Windows.Forms.TextBox txtMessage;
        private System.Windows.Forms.Panel panel1;
        internal System.Windows.Forms.PictureBox pbCritical;
        internal System.Windows.Forms.PictureBox pbError;
        internal System.Windows.Forms.PictureBox pbWarning;
        internal System.Windows.Forms.PictureBox pbInfo;
    }
}