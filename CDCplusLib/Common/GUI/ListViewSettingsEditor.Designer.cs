namespace CDCplusLib.Common
{
    partial class ListViewSettingsEditor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ListViewSettingsEditor));
            pSettings = new Panel();
            lvceColumnSettings = new ListViewColumnEditor();
            panel7 = new Panel();
            txtName = new TextBox();
            lblConfigName = new Label();
            panel8 = new Panel();
            cmdSetConfigAsDefault = new Button();
            cmdApplyConfig = new Button();
            cmdDeleteConfig = new Button();
            cmdSaveConfig = new Button();
            cmdAddConfig = new Button();
            cboAvailableConfig = new ComboBox();
            lblAvailableConfig = new Label();
            pSettings.SuspendLayout();
            panel7.SuspendLayout();
            panel8.SuspendLayout();
            SuspendLayout();
            // 
            // pSettings
            // 
            pSettings.Controls.Add(lvceColumnSettings);
            pSettings.Controls.Add(panel7);
            pSettings.Dock = DockStyle.Fill;
            pSettings.Location = new Point(0, 0);
            pSettings.Margin = new Padding(4);
            pSettings.Name = "pSettings";
            pSettings.Padding = new Padding(0, 11, 0, 11);
            pSettings.Size = new Size(1058, 257);
            pSettings.TabIndex = 4;
            // 
            // lvceColumnSettings
            // 
            lvceColumnSettings.ColumnConfiguration = null;
            lvceColumnSettings.Dock = DockStyle.Fill;
            lvceColumnSettings.Location = new Point(244, 11);
            lvceColumnSettings.Margin = new Padding(4, 5, 4, 5);
            lvceColumnSettings.Name = "lvceColumnSettings";
            lvceColumnSettings.Size = new Size(814, 235);
            lvceColumnSettings.TabIndex = 1;
            lvceColumnSettings.Changed += lvceColumnSettings_Changed;
            // 
            // panel7
            // 
            panel7.Controls.Add(txtName);
            panel7.Controls.Add(lblConfigName);
            panel7.Controls.Add(panel8);
            panel7.Controls.Add(cboAvailableConfig);
            panel7.Controls.Add(lblAvailableConfig);
            panel7.Dock = DockStyle.Left;
            panel7.Location = new Point(0, 11);
            panel7.Margin = new Padding(4);
            panel7.Name = "panel7";
            panel7.Padding = new Padding(0, 0, 11, 0);
            panel7.Size = new Size(244, 235);
            panel7.TabIndex = 0;
            // 
            // txtName
            // 
            txtName.Dock = DockStyle.Top;
            txtName.Location = new Point(0, 113);
            txtName.Margin = new Padding(4);
            txtName.Name = "txtName";
            txtName.Size = new Size(233, 23);
            txtName.TabIndex = 4;
            txtName.TextChanged += txtName_TextChanged;
            // 
            // lblConfigName
            // 
            lblConfigName.AutoSize = true;
            lblConfigName.Dock = DockStyle.Top;
            lblConfigName.Location = new Point(0, 98);
            lblConfigName.Margin = new Padding(4, 0, 4, 0);
            lblConfigName.Name = "lblConfigName";
            lblConfigName.Size = new Size(114, 15);
            lblConfigName.TabIndex = 3;
            lblConfigName.Text = "Configuration name";
            // 
            // panel8
            // 
            panel8.Controls.Add(cmdSetConfigAsDefault);
            panel8.Controls.Add(cmdApplyConfig);
            panel8.Controls.Add(cmdDeleteConfig);
            panel8.Controls.Add(cmdSaveConfig);
            panel8.Controls.Add(cmdAddConfig);
            panel8.Dock = DockStyle.Top;
            panel8.Location = new Point(0, 38);
            panel8.Margin = new Padding(4);
            panel8.Name = "panel8";
            panel8.Size = new Size(233, 60);
            panel8.TabIndex = 2;
            // 
            // cmdSetConfigAsDefault
            // 
            cmdSetConfigAsDefault.Image = (Image)resources.GetObject("cmdSetConfigAsDefault.Image");
            cmdSetConfigAsDefault.Location = new Point(186, 7);
            cmdSetConfigAsDefault.Margin = new Padding(0);
            cmdSetConfigAsDefault.Name = "cmdSetConfigAsDefault";
            cmdSetConfigAsDefault.Size = new Size(46, 46);
            cmdSetConfigAsDefault.TabIndex = 14;
            cmdSetConfigAsDefault.UseVisualStyleBackColor = true;
            cmdSetConfigAsDefault.Click += cmdSetConfigAsDefault_Click;
            // 
            // cmdApplyConfig
            // 
            cmdApplyConfig.Image = (Image)resources.GetObject("cmdApplyConfig.Image");
            cmdApplyConfig.Location = new Point(139, 7);
            cmdApplyConfig.Margin = new Padding(0);
            cmdApplyConfig.Name = "cmdApplyConfig";
            cmdApplyConfig.Size = new Size(46, 46);
            cmdApplyConfig.TabIndex = 13;
            cmdApplyConfig.UseVisualStyleBackColor = true;
            cmdApplyConfig.Click += cmdApplyConfig_Click;
            // 
            // cmdDeleteConfig
            // 
            cmdDeleteConfig.Image = (Image)resources.GetObject("cmdDeleteConfig.Image");
            cmdDeleteConfig.Location = new Point(92, 7);
            cmdDeleteConfig.Margin = new Padding(0);
            cmdDeleteConfig.Name = "cmdDeleteConfig";
            cmdDeleteConfig.Size = new Size(46, 46);
            cmdDeleteConfig.TabIndex = 12;
            cmdDeleteConfig.UseVisualStyleBackColor = true;
            cmdDeleteConfig.Click += cmdDeleteConfig_Click;
            // 
            // cmdSaveConfig
            // 
            cmdSaveConfig.Image = (Image)resources.GetObject("cmdSaveConfig.Image");
            cmdSaveConfig.Location = new Point(46, 7);
            cmdSaveConfig.Margin = new Padding(0);
            cmdSaveConfig.Name = "cmdSaveConfig";
            cmdSaveConfig.Size = new Size(46, 46);
            cmdSaveConfig.TabIndex = 11;
            cmdSaveConfig.UseVisualStyleBackColor = true;
            cmdSaveConfig.Click += cmdSaveConfig_Click;
            // 
            // cmdAddConfig
            // 
            cmdAddConfig.Image = (Image)resources.GetObject("cmdAddConfig.Image");
            cmdAddConfig.Location = new Point(-1, 7);
            cmdAddConfig.Margin = new Padding(0);
            cmdAddConfig.Name = "cmdAddConfig";
            cmdAddConfig.Size = new Size(46, 46);
            cmdAddConfig.TabIndex = 10;
            cmdAddConfig.UseVisualStyleBackColor = true;
            cmdAddConfig.Click += cmdAddConfig_Click;
            // 
            // cboAvailableConfig
            // 
            cboAvailableConfig.Dock = DockStyle.Top;
            cboAvailableConfig.DropDownStyle = ComboBoxStyle.DropDownList;
            cboAvailableConfig.FormattingEnabled = true;
            cboAvailableConfig.Location = new Point(0, 15);
            cboAvailableConfig.Margin = new Padding(4);
            cboAvailableConfig.Name = "cboAvailableConfig";
            cboAvailableConfig.Size = new Size(233, 23);
            cboAvailableConfig.TabIndex = 1;
            cboAvailableConfig.SelectedIndexChanged += cboAvailableConfig_SelectedIndexChanged;
            // 
            // lblAvailableConfig
            // 
            lblAvailableConfig.AutoSize = true;
            lblAvailableConfig.Dock = DockStyle.Top;
            lblAvailableConfig.Location = new Point(0, 0);
            lblAvailableConfig.Margin = new Padding(4, 0, 4, 0);
            lblAvailableConfig.Name = "lblAvailableConfig";
            lblAvailableConfig.Size = new Size(135, 15);
            lblAvailableConfig.TabIndex = 0;
            lblAvailableConfig.Text = "Available configurations";
            // 
            // ListViewSettingsEditor
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(pSettings);
            Margin = new Padding(4);
            Name = "ListViewSettingsEditor";
            Size = new Size(1058, 257);
            pSettings.ResumeLayout(false);
            panel7.ResumeLayout(false);
            panel7.PerformLayout();
            panel8.ResumeLayout(false);
            ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pSettings;
        private ListViewColumnEditor lvceColumnSettings;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label lblConfigName;
        private System.Windows.Forms.Panel panel8;
        internal System.Windows.Forms.Button cmdSetConfigAsDefault;
        internal System.Windows.Forms.Button cmdApplyConfig;
        internal System.Windows.Forms.Button cmdDeleteConfig;
        internal System.Windows.Forms.Button cmdSaveConfig;
        internal System.Windows.Forms.Button cmdAddConfig;
        private System.Windows.Forms.ComboBox cboAvailableConfig;
        private System.Windows.Forms.Label lblAvailableConfig;
    }
}
