
namespace CDCplusLib.TabControls
{
    partial class ViewNotificationTabControl
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ViewNotificationTabControl));
            this.panel20 = new System.Windows.Forms.Panel();
            this.cmdSave = new System.Windows.Forms.Button();
            this.imlPermissions = new System.Windows.Forms.ImageList(this.components);
            this.ttPermissions = new System.Windows.Forms.ToolTip(this.components);
            this.gbNotification = new System.Windows.Forms.GroupBox();
            this.txtMessage = new System.Windows.Forms.TextBox();
            this.lblMessage = new System.Windows.Forms.Label();
            this.Panel2 = new System.Windows.Forms.Panel();
            this.lnkRelatesTo = new System.Windows.Forms.LinkLabel();
            this.lblRelatesTo = new System.Windows.Forms.Label();
            this.Panel11 = new System.Windows.Forms.Panel();
            this.txtTitle = new System.Windows.Forms.TextBox();
            this.lblTitle = new System.Windows.Forms.Label();
            this.gbNotification.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel20
            // 
            this.panel20.Location = new System.Drawing.Point(0, 0);
            this.panel20.Name = "panel20";
            this.panel20.Size = new System.Drawing.Size(200, 100);
            this.panel20.TabIndex = 3;
            // 
            // cmdSave
            // 
            this.cmdSave.Location = new System.Drawing.Point(0, 0);
            this.cmdSave.Name = "cmdSave";
            this.cmdSave.Size = new System.Drawing.Size(75, 23);
            this.cmdSave.TabIndex = 0;
            // 
            // imlPermissions
            // 
            this.imlPermissions.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imlPermissions.ImageStream")));
            this.imlPermissions.TransparentColor = System.Drawing.Color.Transparent;
            this.imlPermissions.Images.SetKeyName(0, "active");
            this.imlPermissions.Images.SetKeyName(1, "inactive");
            // 
            // gbNotification
            // 
            this.gbNotification.Controls.Add(this.txtMessage);
            this.gbNotification.Controls.Add(this.lblMessage);
            this.gbNotification.Controls.Add(this.Panel2);
            this.gbNotification.Controls.Add(this.lnkRelatesTo);
            this.gbNotification.Controls.Add(this.lblRelatesTo);
            this.gbNotification.Controls.Add(this.Panel11);
            this.gbNotification.Controls.Add(this.txtTitle);
            this.gbNotification.Controls.Add(this.lblTitle);
            this.gbNotification.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbNotification.Location = new System.Drawing.Point(0, 0);
            this.gbNotification.Margin = new System.Windows.Forms.Padding(4);
            this.gbNotification.Name = "gbNotification";
            this.gbNotification.Padding = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.gbNotification.Size = new System.Drawing.Size(1199, 705);
            this.gbNotification.TabIndex = 2;
            this.gbNotification.TabStop = false;
            this.gbNotification.Text = "GroupBox1";
            // 
            // txtMessage
            // 
            this.txtMessage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtMessage.Location = new System.Drawing.Point(8, 140);
            this.txtMessage.Margin = new System.Windows.Forms.Padding(4);
            this.txtMessage.Multiline = true;
            this.txtMessage.Name = "txtMessage";
            this.txtMessage.ReadOnly = true;
            this.txtMessage.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtMessage.Size = new System.Drawing.Size(1183, 558);
            this.txtMessage.TabIndex = 20;
            // 
            // lblMessage
            // 
            this.lblMessage.AutoSize = true;
            this.lblMessage.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblMessage.Location = new System.Drawing.Point(8, 123);
            this.lblMessage.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(65, 17);
            this.lblMessage.TabIndex = 19;
            this.lblMessage.Text = "Message";
            // 
            // Panel2
            // 
            this.Panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.Panel2.Location = new System.Drawing.Point(8, 111);
            this.Panel2.Margin = new System.Windows.Forms.Padding(4);
            this.Panel2.Name = "Panel2";
            this.Panel2.Size = new System.Drawing.Size(1183, 12);
            this.Panel2.TabIndex = 18;
            // 
            // lnkRelatesTo
            // 
            this.lnkRelatesTo.AutoSize = true;
            this.lnkRelatesTo.Dock = System.Windows.Forms.DockStyle.Top;
            this.lnkRelatesTo.Location = new System.Drawing.Point(8, 90);
            this.lnkRelatesTo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lnkRelatesTo.Name = "lnkRelatesTo";
            this.lnkRelatesTo.Padding = new System.Windows.Forms.Padding(0, 4, 0, 0);
            this.lnkRelatesTo.Size = new System.Drawing.Size(49, 21);
            this.lnkRelatesTo.TabIndex = 13;
            this.lnkRelatesTo.TabStop = true;
            this.lnkRelatesTo.Text = "/link/to";
            this.lnkRelatesTo.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkRelatesTo_LinkClicked);
            // 
            // lblRelatesTo
            // 
            this.lblRelatesTo.AutoSize = true;
            this.lblRelatesTo.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblRelatesTo.Location = new System.Drawing.Point(8, 73);
            this.lblRelatesTo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblRelatesTo.Name = "lblRelatesTo";
            this.lblRelatesTo.Size = new System.Drawing.Size(72, 17);
            this.lblRelatesTo.TabIndex = 12;
            this.lblRelatesTo.Text = "Relates to";
            // 
            // Panel11
            // 
            this.Panel11.Dock = System.Windows.Forms.DockStyle.Top;
            this.Panel11.Location = new System.Drawing.Point(8, 61);
            this.Panel11.Margin = new System.Windows.Forms.Padding(4);
            this.Panel11.Name = "Panel11";
            this.Panel11.Size = new System.Drawing.Size(1183, 12);
            this.Panel11.TabIndex = 6;
            // 
            // txtTitle
            // 
            this.txtTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.txtTitle.Location = new System.Drawing.Point(8, 39);
            this.txtTitle.Margin = new System.Windows.Forms.Padding(4);
            this.txtTitle.Name = "txtTitle";
            this.txtTitle.ReadOnly = true;
            this.txtTitle.Size = new System.Drawing.Size(1183, 22);
            this.txtTitle.TabIndex = 1;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTitle.Location = new System.Drawing.Point(8, 22);
            this.lblTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(35, 17);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Title";
            // 
            // ViewNotificationTabControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gbNotification);
            this.Controls.Add(this.panel20);
            this.Name = "ViewNotificationTabControl";
            this.Size = new System.Drawing.Size(1199, 705);
            this.gbNotification.ResumeLayout(false);
            this.gbNotification.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel20;
        internal System.Windows.Forms.Button cmdSave;
        private System.Windows.Forms.ToolTip ttPermissions;
        private System.Windows.Forms.ImageList imlPermissions;
        internal System.Windows.Forms.GroupBox gbNotification;
        internal System.Windows.Forms.TextBox txtMessage;
        internal System.Windows.Forms.Label lblMessage;
        internal System.Windows.Forms.Panel Panel2;
        internal System.Windows.Forms.LinkLabel lnkRelatesTo;
        internal System.Windows.Forms.Label lblRelatesTo;
        internal System.Windows.Forms.Panel Panel11;
        internal System.Windows.Forms.TextBox txtTitle;
        internal System.Windows.Forms.Label lblTitle;
    }
}
