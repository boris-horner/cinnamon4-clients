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
            this.pSettings = new System.Windows.Forms.Panel();
            this.lvceColumnSettings = new ListViewColumnEditor();
            this.panel7 = new System.Windows.Forms.Panel();
            this.txtName = new System.Windows.Forms.TextBox();
            this.lblConfigName = new System.Windows.Forms.Label();
            this.panel8 = new System.Windows.Forms.Panel();
            this.cmdSetConfigAsDefault = new System.Windows.Forms.Button();
            this.cmdApplyConfig = new System.Windows.Forms.Button();
            this.cmdDeleteConfig = new System.Windows.Forms.Button();
            this.cmdSaveConfig = new System.Windows.Forms.Button();
            this.cmdAddConfig = new System.Windows.Forms.Button();
            this.cboAvailableConfig = new System.Windows.Forms.ComboBox();
            this.lblAvailableConfig = new System.Windows.Forms.Label();
            this.pSettings.SuspendLayout();
            this.panel7.SuspendLayout();
            this.panel8.SuspendLayout();
            this.SuspendLayout();
            // 
            // pSettings
            // 
            this.pSettings.Controls.Add(this.lvceColumnSettings);
            this.pSettings.Controls.Add(this.panel7);
            this.pSettings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pSettings.Location = new System.Drawing.Point(0, 0);
            this.pSettings.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pSettings.Name = "pSettings";
            this.pSettings.Padding = new System.Windows.Forms.Padding(0, 12, 0, 12);
            this.pSettings.Size = new System.Drawing.Size(1209, 274);
            this.pSettings.TabIndex = 4;
            // 
            // lvceColumnSettings
            // 
            this.lvceColumnSettings.ColumnConfiguration = null;
            this.lvceColumnSettings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvceColumnSettings.Location = new System.Drawing.Point(279, 12);
            this.lvceColumnSettings.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.lvceColumnSettings.Name = "lvceColumnSettings";
            this.lvceColumnSettings.Size = new System.Drawing.Size(930, 250);
            this.lvceColumnSettings.TabIndex = 1;
            this.lvceColumnSettings.Changed += new System.EventHandler(this.lvceColumnSettings_Changed);
            // 
            // panel7
            // 
            this.panel7.Controls.Add(this.txtName);
            this.panel7.Controls.Add(this.lblConfigName);
            this.panel7.Controls.Add(this.panel8);
            this.panel7.Controls.Add(this.cboAvailableConfig);
            this.panel7.Controls.Add(this.lblAvailableConfig);
            this.panel7.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel7.Location = new System.Drawing.Point(0, 12);
            this.panel7.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel7.Name = "panel7";
            this.panel7.Padding = new System.Windows.Forms.Padding(0, 0, 13, 0);
            this.panel7.Size = new System.Drawing.Size(279, 250);
            this.panel7.TabIndex = 0;
            // 
            // txtName
            // 
            this.txtName.Dock = System.Windows.Forms.DockStyle.Top;
            this.txtName.Location = new System.Drawing.Point(0, 120);
            this.txtName.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(266, 22);
            this.txtName.TabIndex = 4;
            this.txtName.TextChanged += new System.EventHandler(this.txtName_TextChanged);
            // 
            // lblConfigName
            // 
            this.lblConfigName.AutoSize = true;
            this.lblConfigName.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblConfigName.Location = new System.Drawing.Point(0, 104);
            this.lblConfigName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblConfigName.Name = "lblConfigName";
            this.lblConfigName.Size = new System.Drawing.Size(122, 16);
            this.lblConfigName.TabIndex = 3;
            this.lblConfigName.Text = "Configuration name";
            // 
            // panel8
            // 
            this.panel8.Controls.Add(this.cmdSetConfigAsDefault);
            this.panel8.Controls.Add(this.cmdApplyConfig);
            this.panel8.Controls.Add(this.cmdDeleteConfig);
            this.panel8.Controls.Add(this.cmdSaveConfig);
            this.panel8.Controls.Add(this.cmdAddConfig);
            this.panel8.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel8.Location = new System.Drawing.Point(0, 40);
            this.panel8.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(266, 64);
            this.panel8.TabIndex = 2;
            // 
            // cmdSetConfigAsDefault
            // 
            this.cmdSetConfigAsDefault.Image = ((System.Drawing.Image)(resources.GetObject("cmdSetConfigAsDefault.Image")));
            this.cmdSetConfigAsDefault.Location = new System.Drawing.Point(212, 7);
            this.cmdSetConfigAsDefault.Margin = new System.Windows.Forms.Padding(0);
            this.cmdSetConfigAsDefault.Name = "cmdSetConfigAsDefault";
            this.cmdSetConfigAsDefault.Size = new System.Drawing.Size(53, 49);
            this.cmdSetConfigAsDefault.TabIndex = 14;
            this.cmdSetConfigAsDefault.UseVisualStyleBackColor = true;
            this.cmdSetConfigAsDefault.Click += new System.EventHandler(this.cmdSetConfigAsDefault_Click);
            // 
            // cmdApplyConfig
            // 
            this.cmdApplyConfig.Image = ((System.Drawing.Image)(resources.GetObject("cmdApplyConfig.Image")));
            this.cmdApplyConfig.Location = new System.Drawing.Point(159, 7);
            this.cmdApplyConfig.Margin = new System.Windows.Forms.Padding(0);
            this.cmdApplyConfig.Name = "cmdApplyConfig";
            this.cmdApplyConfig.Size = new System.Drawing.Size(53, 49);
            this.cmdApplyConfig.TabIndex = 13;
            this.cmdApplyConfig.UseVisualStyleBackColor = true;
            this.cmdApplyConfig.Click += new System.EventHandler(this.cmdApplyConfig_Click);
            // 
            // cmdDeleteConfig
            // 
            this.cmdDeleteConfig.Image = ((System.Drawing.Image)(resources.GetObject("cmdDeleteConfig.Image")));
            this.cmdDeleteConfig.Location = new System.Drawing.Point(105, 7);
            this.cmdDeleteConfig.Margin = new System.Windows.Forms.Padding(0);
            this.cmdDeleteConfig.Name = "cmdDeleteConfig";
            this.cmdDeleteConfig.Size = new System.Drawing.Size(53, 49);
            this.cmdDeleteConfig.TabIndex = 12;
            this.cmdDeleteConfig.UseVisualStyleBackColor = true;
            this.cmdDeleteConfig.Click += new System.EventHandler(this.cmdDeleteConfig_Click);
            // 
            // cmdSaveConfig
            // 
            this.cmdSaveConfig.Image = ((System.Drawing.Image)(resources.GetObject("cmdSaveConfig.Image")));
            this.cmdSaveConfig.Location = new System.Drawing.Point(52, 7);
            this.cmdSaveConfig.Margin = new System.Windows.Forms.Padding(0);
            this.cmdSaveConfig.Name = "cmdSaveConfig";
            this.cmdSaveConfig.Size = new System.Drawing.Size(53, 49);
            this.cmdSaveConfig.TabIndex = 11;
            this.cmdSaveConfig.UseVisualStyleBackColor = true;
            this.cmdSaveConfig.Click += new System.EventHandler(this.cmdSaveConfig_Click);
            // 
            // cmdAddConfig
            // 
            this.cmdAddConfig.Image = ((System.Drawing.Image)(resources.GetObject("cmdAddConfig.Image")));
            this.cmdAddConfig.Location = new System.Drawing.Point(-1, 7);
            this.cmdAddConfig.Margin = new System.Windows.Forms.Padding(0);
            this.cmdAddConfig.Name = "cmdAddConfig";
            this.cmdAddConfig.Size = new System.Drawing.Size(53, 49);
            this.cmdAddConfig.TabIndex = 10;
            this.cmdAddConfig.UseVisualStyleBackColor = true;
            this.cmdAddConfig.Click += new System.EventHandler(this.cmdAddConfig_Click);
            // 
            // cboAvailableConfig
            // 
            this.cboAvailableConfig.Dock = System.Windows.Forms.DockStyle.Top;
            this.cboAvailableConfig.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboAvailableConfig.FormattingEnabled = true;
            this.cboAvailableConfig.Location = new System.Drawing.Point(0, 16);
            this.cboAvailableConfig.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cboAvailableConfig.Name = "cboAvailableConfig";
            this.cboAvailableConfig.Size = new System.Drawing.Size(266, 24);
            this.cboAvailableConfig.TabIndex = 1;
            this.cboAvailableConfig.SelectedIndexChanged += new System.EventHandler(this.cboAvailableConfig_SelectedIndexChanged);
            // 
            // lblAvailableConfig
            // 
            this.lblAvailableConfig.AutoSize = true;
            this.lblAvailableConfig.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblAvailableConfig.Location = new System.Drawing.Point(0, 0);
            this.lblAvailableConfig.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblAvailableConfig.Name = "lblAvailableConfig";
            this.lblAvailableConfig.Size = new System.Drawing.Size(150, 16);
            this.lblAvailableConfig.TabIndex = 0;
            this.lblAvailableConfig.Text = "Available configurations";
            // 
            // ListViewSettingsEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pSettings);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "ListViewSettingsEditor";
            this.Size = new System.Drawing.Size(1209, 274);
            this.pSettings.ResumeLayout(false);
            this.panel7.ResumeLayout(false);
            this.panel7.PerformLayout();
            this.panel8.ResumeLayout(false);
            this.ResumeLayout(false);

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
