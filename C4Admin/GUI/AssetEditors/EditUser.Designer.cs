
namespace C4Admin.GUI.AssetEditors
{
	partial class EditUser
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
            Label label3;
            Label label2;
            Label label5;
            Label label6;
            panel7 = new Panel();
            pCancel = new Panel();
            cmdCancel = new Button();
            pSaveAs = new Panel();
            cmdSaveAs = new Button();
            pSave = new Panel();
            cmdOk = new Button();
            pData = new Panel();
            clbGroups = new CheckedListBox();
            xtxtConfig = new C4GeneralGui.GuiElements.XmlTextBox();
            chkPasswordExpired = new CheckBox();
            chkLocked = new CheckBox();
            chkChangeTracking = new CheckBox();
            chkChangeTriggers = new CheckBox();
            chkActive = new CheckBox();
            vtxtEmail = new C4GeneralGui.GuiElements.ValidatingTextBox();
            label4 = new Label();
            cboLoginType = new ComboBox();
            cboUiLanguage = new ComboBox();
            vtxtFullName = new C4GeneralGui.GuiElements.ValidatingTextBox();
            label7 = new Label();
            vtxtPassword = new C4GeneralGui.GuiElements.ValidatingTextBox();
            label1 = new Label();
            vtxtName = new C4GeneralGui.GuiElements.ValidatingTextBox();
            lblName = new Label();
            txtId = new TextBox();
            lblId = new Label();
            label3 = new Label();
            label2 = new Label();
            label5 = new Label();
            label6 = new Label();
            panel7.SuspendLayout();
            pCancel.SuspendLayout();
            pSaveAs.SuspendLayout();
            pSave.SuspendLayout();
            pData.SuspendLayout();
            SuspendLayout();
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Dock = DockStyle.Top;
            label3.Location = new Point(5, 232);
            label3.Margin = new Padding(0);
            label3.Name = "label3";
            label3.Padding = new Padding(0, 9, 0, 0);
            label3.Size = new Size(63, 24);
            label3.TabIndex = 40;
            label3.Text = "Login type";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Dock = DockStyle.Top;
            label2.Location = new Point(5, 185);
            label2.Margin = new Padding(0);
            label2.Name = "label2";
            label2.Padding = new Padding(0, 9, 0, 0);
            label2.Size = new Size(73, 24);
            label2.TabIndex = 38;
            label2.Text = "UI Language";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Dock = DockStyle.Top;
            label5.Location = new Point(5, 609);
            label5.Margin = new Padding(0);
            label5.Name = "label5";
            label5.Padding = new Padding(0, 9, 0, 0);
            label5.Size = new Size(115, 24);
            label5.TabIndex = 62;
            label5.Text = "Group memberships";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Dock = DockStyle.Top;
            label6.Location = new Point(5, 471);
            label6.Margin = new Padding(0);
            label6.Name = "label6";
            label6.Padding = new Padding(0, 9, 0, 0);
            label6.Size = new Size(43, 24);
            label6.TabIndex = 60;
            label6.Text = "Config";
            // 
            // panel7
            // 
            panel7.Controls.Add(pCancel);
            panel7.Controls.Add(pSaveAs);
            panel7.Controls.Add(pSave);
            panel7.Dock = DockStyle.Bottom;
            panel7.Location = new Point(0, 768);
            panel7.Name = "panel7";
            panel7.Size = new Size(457, 37);
            panel7.TabIndex = 6;
            // 
            // pCancel
            // 
            pCancel.Controls.Add(cmdCancel);
            pCancel.Dock = DockStyle.Left;
            pCancel.Location = new Point(224, 0);
            pCancel.Name = "pCancel";
            pCancel.Padding = new Padding(6, 7, 10, 7);
            pCancel.Size = new Size(112, 37);
            pCancel.TabIndex = 11;
            // 
            // cmdCancel
            // 
            cmdCancel.DialogResult = DialogResult.Cancel;
            cmdCancel.Dock = DockStyle.Fill;
            cmdCancel.Location = new Point(6, 7);
            cmdCancel.Name = "cmdCancel";
            cmdCancel.Size = new Size(96, 23);
            cmdCancel.TabIndex = 9;
            cmdCancel.Text = "Cancel";
            cmdCancel.UseVisualStyleBackColor = true;
            cmdCancel.Click += cmdCancel_Click;
            // 
            // pSaveAs
            // 
            pSaveAs.Controls.Add(cmdSaveAs);
            pSaveAs.Dock = DockStyle.Left;
            pSaveAs.Location = new Point(112, 0);
            pSaveAs.Name = "pSaveAs";
            pSaveAs.Padding = new Padding(6, 7, 10, 7);
            pSaveAs.Size = new Size(112, 37);
            pSaveAs.TabIndex = 10;
            // 
            // cmdSaveAs
            // 
            cmdSaveAs.DialogResult = DialogResult.Cancel;
            cmdSaveAs.Dock = DockStyle.Fill;
            cmdSaveAs.Location = new Point(6, 7);
            cmdSaveAs.Name = "cmdSaveAs";
            cmdSaveAs.Size = new Size(96, 23);
            cmdSaveAs.TabIndex = 6;
            cmdSaveAs.Text = "Save as";
            cmdSaveAs.UseVisualStyleBackColor = true;
            cmdSaveAs.Click += cmdSaveAs_Click;
            // 
            // pSave
            // 
            pSave.Controls.Add(cmdOk);
            pSave.Dock = DockStyle.Left;
            pSave.Location = new Point(0, 0);
            pSave.Name = "pSave";
            pSave.Padding = new Padding(6, 7, 10, 7);
            pSave.Size = new Size(112, 37);
            pSave.TabIndex = 9;
            // 
            // cmdOk
            // 
            cmdOk.Dock = DockStyle.Fill;
            cmdOk.Location = new Point(6, 7);
            cmdOk.Name = "cmdOk";
            cmdOk.Size = new Size(96, 23);
            cmdOk.TabIndex = 2;
            cmdOk.Text = "Save";
            cmdOk.UseVisualStyleBackColor = true;
            cmdOk.Click += cmdOk_Click;
            // 
            // pData
            // 
            pData.Controls.Add(clbGroups);
            pData.Controls.Add(label5);
            pData.Controls.Add(xtxtConfig);
            pData.Controls.Add(label6);
            pData.Controls.Add(chkPasswordExpired);
            pData.Controls.Add(chkLocked);
            pData.Controls.Add(chkChangeTracking);
            pData.Controls.Add(chkChangeTriggers);
            pData.Controls.Add(chkActive);
            pData.Controls.Add(vtxtEmail);
            pData.Controls.Add(label4);
            pData.Controls.Add(cboLoginType);
            pData.Controls.Add(label3);
            pData.Controls.Add(cboUiLanguage);
            pData.Controls.Add(label2);
            pData.Controls.Add(vtxtFullName);
            pData.Controls.Add(label7);
            pData.Controls.Add(vtxtPassword);
            pData.Controls.Add(label1);
            pData.Controls.Add(vtxtName);
            pData.Controls.Add(lblName);
            pData.Controls.Add(txtId);
            pData.Controls.Add(lblId);
            pData.Dock = DockStyle.Fill;
            pData.Location = new Point(0, 0);
            pData.Name = "pData";
            pData.Padding = new Padding(5, 6, 5, 6);
            pData.Size = new Size(457, 768);
            pData.TabIndex = 0;
            // 
            // clbGroups
            // 
            clbGroups.CheckOnClick = true;
            clbGroups.Dock = DockStyle.Fill;
            clbGroups.FormattingEnabled = true;
            clbGroups.Location = new Point(5, 633);
            clbGroups.Name = "clbGroups";
            clbGroups.Size = new Size(447, 129);
            clbGroups.Sorted = true;
            clbGroups.TabIndex = 63;
            clbGroups.ItemCheck += clbGroups_ItemCheck;
            // 
            // xtxtConfig
            // 
            xtxtConfig.Dock = DockStyle.Top;
            xtxtConfig.Location = new Point(5, 495);
            xtxtConfig.Name = "xtxtConfig";
            xtxtConfig.Size = new Size(447, 114);
            xtxtConfig.TabIndex = 61;
            xtxtConfig.Text = "";
            // 
            // chkPasswordExpired
            // 
            chkPasswordExpired.AutoSize = true;
            chkPasswordExpired.Dock = DockStyle.Top;
            chkPasswordExpired.Location = new Point(5, 442);
            chkPasswordExpired.Name = "chkPasswordExpired";
            chkPasswordExpired.Padding = new Padding(0, 10, 0, 0);
            chkPasswordExpired.Size = new Size(447, 29);
            chkPasswordExpired.TabIndex = 59;
            chkPasswordExpired.Text = "Password expired";
            chkPasswordExpired.UseVisualStyleBackColor = true;
            // 
            // chkLocked
            // 
            chkLocked.AutoSize = true;
            chkLocked.Dock = DockStyle.Top;
            chkLocked.Location = new Point(5, 413);
            chkLocked.Name = "chkLocked";
            chkLocked.Padding = new Padding(0, 10, 0, 0);
            chkLocked.Size = new Size(447, 29);
            chkLocked.TabIndex = 58;
            chkLocked.Text = "Locked";
            chkLocked.UseVisualStyleBackColor = true;
            // 
            // chkChangeTracking
            // 
            chkChangeTracking.AutoSize = true;
            chkChangeTracking.Dock = DockStyle.Top;
            chkChangeTracking.Location = new Point(5, 384);
            chkChangeTracking.Name = "chkChangeTracking";
            chkChangeTracking.Padding = new Padding(0, 10, 0, 0);
            chkChangeTracking.Size = new Size(447, 29);
            chkChangeTracking.TabIndex = 57;
            chkChangeTracking.Text = "Activate change tracking";
            chkChangeTracking.UseVisualStyleBackColor = true;
            // 
            // chkChangeTriggers
            // 
            chkChangeTriggers.AutoSize = true;
            chkChangeTriggers.Dock = DockStyle.Top;
            chkChangeTriggers.Location = new Point(5, 355);
            chkChangeTriggers.Name = "chkChangeTriggers";
            chkChangeTriggers.Padding = new Padding(0, 10, 0, 0);
            chkChangeTriggers.Size = new Size(447, 29);
            chkChangeTriggers.TabIndex = 56;
            chkChangeTriggers.Text = "Activate change triggers";
            chkChangeTriggers.UseVisualStyleBackColor = true;
            // 
            // chkActive
            // 
            chkActive.AutoSize = true;
            chkActive.Dock = DockStyle.Top;
            chkActive.Location = new Point(5, 326);
            chkActive.Name = "chkActive";
            chkActive.Padding = new Padding(0, 10, 0, 0);
            chkActive.Size = new Size(447, 29);
            chkActive.TabIndex = 55;
            chkActive.Text = "Active";
            chkActive.UseVisualStyleBackColor = true;
            // 
            // vtxtEmail
            // 
            vtxtEmail.BackColor = Color.Yellow;
            vtxtEmail.BothInvalidColor = Color.Red;
            vtxtEmail.Dock = DockStyle.Top;
            vtxtEmail.ExternalInvalidColor = Color.Orange;
            vtxtEmail.ExternalValidation = null;
            vtxtEmail.InternalInvalidColor = Color.Yellow;
            vtxtEmail.InvalidReadOnlyColor = Color.LightYellow;
            vtxtEmail.Location = new Point(5, 303);
            vtxtEmail.Name = "vtxtEmail";
            vtxtEmail.RegularExpression = "^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\\.[a-zA-Z]{2,}$";
            vtxtEmail.Size = new Size(447, 23);
            vtxtEmail.TabIndex = 43;
            vtxtEmail.ValidColor = Color.White;
            vtxtEmail.ValidReadOnlyColor = SystemColors.Control;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Dock = DockStyle.Top;
            label4.Location = new Point(5, 279);
            label4.Name = "label4";
            label4.Padding = new Padding(0, 9, 0, 0);
            label4.Size = new Size(36, 24);
            label4.TabIndex = 42;
            label4.Text = "Email";
            // 
            // cboLoginType
            // 
            cboLoginType.Dock = DockStyle.Top;
            cboLoginType.DropDownStyle = ComboBoxStyle.DropDownList;
            cboLoginType.FormattingEnabled = true;
            cboLoginType.Location = new Point(5, 256);
            cboLoginType.Name = "cboLoginType";
            cboLoginType.Size = new Size(447, 23);
            cboLoginType.Sorted = true;
            cboLoginType.TabIndex = 41;
            // 
            // cboUiLanguage
            // 
            cboUiLanguage.Dock = DockStyle.Top;
            cboUiLanguage.DropDownStyle = ComboBoxStyle.DropDownList;
            cboUiLanguage.FormattingEnabled = true;
            cboUiLanguage.Location = new Point(5, 209);
            cboUiLanguage.Name = "cboUiLanguage";
            cboUiLanguage.Size = new Size(447, 23);
            cboUiLanguage.Sorted = true;
            cboUiLanguage.TabIndex = 39;
            // 
            // vtxtFullName
            // 
            vtxtFullName.BackColor = Color.Yellow;
            vtxtFullName.BothInvalidColor = Color.Red;
            vtxtFullName.Dock = DockStyle.Top;
            vtxtFullName.ExternalInvalidColor = Color.Orange;
            vtxtFullName.ExternalValidation = null;
            vtxtFullName.InternalInvalidColor = Color.Yellow;
            vtxtFullName.InvalidReadOnlyColor = Color.LightYellow;
            vtxtFullName.Location = new Point(5, 162);
            vtxtFullName.Name = "vtxtFullName";
            vtxtFullName.RegularExpression = "^[ \\p{L}0-9_.-]+$";
            vtxtFullName.Size = new Size(447, 23);
            vtxtFullName.TabIndex = 36;
            vtxtFullName.ValidColor = Color.White;
            vtxtFullName.ValidReadOnlyColor = SystemColors.Control;
            vtxtFullName.TextChanged += vtxtFullName_TextChanged;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Dock = DockStyle.Top;
            label7.Location = new Point(5, 138);
            label7.Margin = new Padding(0);
            label7.Name = "label7";
            label7.Padding = new Padding(0, 9, 0, 0);
            label7.Size = new Size(59, 24);
            label7.TabIndex = 37;
            label7.Text = "Full name";
            // 
            // vtxtPassword
            // 
            vtxtPassword.BackColor = Color.White;
            vtxtPassword.BothInvalidColor = Color.Red;
            vtxtPassword.Dock = DockStyle.Top;
            vtxtPassword.ExternalInvalidColor = Color.Orange;
            vtxtPassword.ExternalValidation = null;
            vtxtPassword.InternalInvalidColor = Color.Yellow;
            vtxtPassword.InvalidReadOnlyColor = Color.LightYellow;
            vtxtPassword.Location = new Point(5, 115);
            vtxtPassword.Name = "vtxtPassword";
            vtxtPassword.PasswordChar = '*';
            vtxtPassword.RegularExpression = "^.{7}.+$|^$";
            vtxtPassword.Size = new Size(447, 23);
            vtxtPassword.TabIndex = 6;
            vtxtPassword.ValidColor = Color.White;
            vtxtPassword.ValidReadOnlyColor = SystemColors.Control;
            vtxtPassword.TextChanged += vtxtPassword_TextChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Dock = DockStyle.Top;
            label1.Location = new Point(5, 91);
            label1.Name = "label1";
            label1.Padding = new Padding(0, 9, 0, 0);
            label1.Size = new Size(57, 24);
            label1.TabIndex = 5;
            label1.Text = "Password";
            // 
            // vtxtName
            // 
            vtxtName.BackColor = Color.Yellow;
            vtxtName.BothInvalidColor = Color.Red;
            vtxtName.Dock = DockStyle.Top;
            vtxtName.ExternalInvalidColor = Color.Orange;
            vtxtName.ExternalValidation = null;
            vtxtName.InternalInvalidColor = Color.Yellow;
            vtxtName.InvalidReadOnlyColor = Color.LightYellow;
            vtxtName.Location = new Point(5, 68);
            vtxtName.Name = "vtxtName";
            vtxtName.RegularExpression = "^[a-zA-Z0-9_.-@]+$";
            vtxtName.Size = new Size(447, 23);
            vtxtName.TabIndex = 0;
            vtxtName.ValidColor = Color.White;
            vtxtName.ValidReadOnlyColor = SystemColors.Control;
            vtxtName.TextChanged += vtxtName_TextChanged;
            // 
            // lblName
            // 
            lblName.AutoSize = true;
            lblName.Dock = DockStyle.Top;
            lblName.Location = new Point(5, 44);
            lblName.Margin = new Padding(0);
            lblName.Name = "lblName";
            lblName.Padding = new Padding(0, 9, 0, 0);
            lblName.Size = new Size(39, 24);
            lblName.TabIndex = 4;
            lblName.Text = "Name";
            // 
            // txtId
            // 
            txtId.Dock = DockStyle.Top;
            txtId.Location = new Point(5, 21);
            txtId.Margin = new Padding(0);
            txtId.Name = "txtId";
            txtId.ReadOnly = true;
            txtId.Size = new Size(447, 23);
            txtId.TabIndex = 1;
            // 
            // lblId
            // 
            lblId.AutoSize = true;
            lblId.Dock = DockStyle.Top;
            lblId.Location = new Point(5, 6);
            lblId.Margin = new Padding(0);
            lblId.Name = "lblId";
            lblId.Size = new Size(17, 15);
            lblId.TabIndex = 0;
            lblId.Text = "Id";
            // 
            // EditUser
            // 
            AcceptButton = cmdOk;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = cmdCancel;
            ClientSize = new Size(457, 805);
            Controls.Add(pData);
            Controls.Add(panel7);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Name = "EditUser";
            Text = "Edit user";
            Load += EditGroup_Load;
            panel7.ResumeLayout(false);
            pCancel.ResumeLayout(false);
            pSaveAs.ResumeLayout(false);
            pSave.ResumeLayout(false);
            pData.ResumeLayout(false);
            pData.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel panel7;
		private System.Windows.Forms.Panel pData;
		private System.Windows.Forms.TextBox txtId;
		private System.Windows.Forms.Label lblId;
		private System.Windows.Forms.Panel pCancel;
		private System.Windows.Forms.Button cmdCancel;
		private System.Windows.Forms.Panel pSaveAs;
		private System.Windows.Forms.Button cmdSaveAs;
		private System.Windows.Forms.Panel pSave;
		private System.Windows.Forms.Button cmdOk;
		private C4GeneralGui.GuiElements.ValidatingTextBox vtxtName;
		private System.Windows.Forms.Label lblName;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.ComboBox cboLoginType;
		private System.Windows.Forms.ComboBox cboUiLanguage;
		private C4GeneralGui.GuiElements.ValidatingTextBox vtxtFullName;
		private System.Windows.Forms.Label label7;
		private C4GeneralGui.GuiElements.ValidatingTextBox vtxtPassword;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.CheckedListBox clbGroups;
		private C4GeneralGui.GuiElements.XmlTextBox xtxtConfig;
		private System.Windows.Forms.CheckBox chkPasswordExpired;
		private System.Windows.Forms.CheckBox chkLocked;
		private System.Windows.Forms.CheckBox chkChangeTracking;
		private System.Windows.Forms.CheckBox chkChangeTriggers;
		private System.Windows.Forms.CheckBox chkActive;
		private C4GeneralGui.GuiElements.ValidatingTextBox vtxtEmail;
	}
}