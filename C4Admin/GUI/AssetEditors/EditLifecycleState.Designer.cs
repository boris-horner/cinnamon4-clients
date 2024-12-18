
namespace C4Admin.GUI.AssetEditors
{
	partial class EditLifecycleState
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
            panel7 = new Panel();
            pCancel = new Panel();
            cmdCancel = new Button();
            pSaveAs = new Panel();
            cmdSaveAs = new Button();
            pSave = new Panel();
            cmdOk = new Button();
            pData = new Panel();
            cboLifecycle = new ComboBox();
            lblLifecycle = new Label();
            cboCopyState = new ComboBox();
            lblCopyState = new Label();
            xtbConfig = new C4GeneralGui.GuiElements.XmlTextBox();
            lblConfig = new Label();
            vtxtStateClass = new C4GeneralGui.GuiElements.ValidatingTextBox();
            lblStateClass = new Label();
            vtxtName = new C4GeneralGui.GuiElements.ValidatingTextBox();
            lblName = new Label();
            txtId = new TextBox();
            lblId = new Label();
            panel7.SuspendLayout();
            pCancel.SuspendLayout();
            pSaveAs.SuspendLayout();
            pSave.SuspendLayout();
            pData.SuspendLayout();
            SuspendLayout();
            // 
            // panel7
            // 
            panel7.Controls.Add(pCancel);
            panel7.Controls.Add(pSaveAs);
            panel7.Controls.Add(pSave);
            panel7.Dock = DockStyle.Bottom;
            panel7.Location = new Point(0, 373);
            panel7.Name = "panel7";
            panel7.Size = new Size(557, 37);
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
            pData.Controls.Add(cboLifecycle);
            pData.Controls.Add(lblLifecycle);
            pData.Controls.Add(cboCopyState);
            pData.Controls.Add(lblCopyState);
            pData.Controls.Add(xtbConfig);
            pData.Controls.Add(lblConfig);
            pData.Controls.Add(vtxtStateClass);
            pData.Controls.Add(lblStateClass);
            pData.Controls.Add(vtxtName);
            pData.Controls.Add(lblName);
            pData.Controls.Add(txtId);
            pData.Controls.Add(lblId);
            pData.Dock = DockStyle.Fill;
            pData.Location = new Point(0, 0);
            pData.Name = "pData";
            pData.Padding = new Padding(5, 6, 5, 6);
            pData.Size = new Size(557, 373);
            pData.TabIndex = 0;
            // 
            // cboLifecycle
            // 
            cboLifecycle.Dock = DockStyle.Top;
            cboLifecycle.DropDownStyle = ComboBoxStyle.DropDownList;
            cboLifecycle.FormattingEnabled = true;
            cboLifecycle.Location = new Point(5, 329);
            cboLifecycle.Name = "cboLifecycle";
            cboLifecycle.Size = new Size(547, 23);
            cboLifecycle.Sorted = true;
            cboLifecycle.TabIndex = 35;
            cboLifecycle.SelectedIndexChanged += cboLifecycle_SelectedIndexChanged;
            // 
            // lblLifecycle
            // 
            lblLifecycle.AutoSize = true;
            lblLifecycle.Dock = DockStyle.Top;
            lblLifecycle.Location = new Point(5, 305);
            lblLifecycle.Margin = new Padding(0);
            lblLifecycle.Name = "lblLifecycle";
            lblLifecycle.Padding = new Padding(0, 9, 0, 0);
            lblLifecycle.Size = new Size(53, 24);
            lblLifecycle.TabIndex = 34;
            lblLifecycle.Text = "Lifecycle";
            // 
            // cboCopyState
            // 
            cboCopyState.Dock = DockStyle.Top;
            cboCopyState.DropDownStyle = ComboBoxStyle.DropDownList;
            cboCopyState.FormattingEnabled = true;
            cboCopyState.Location = new Point(5, 282);
            cboCopyState.Name = "cboCopyState";
            cboCopyState.Size = new Size(547, 23);
            cboCopyState.Sorted = true;
            cboCopyState.TabIndex = 33;
            // 
            // lblCopyState
            // 
            lblCopyState.AutoSize = true;
            lblCopyState.Dock = DockStyle.Top;
            lblCopyState.Location = new Point(5, 258);
            lblCopyState.Margin = new Padding(0);
            lblCopyState.Name = "lblCopyState";
            lblCopyState.Padding = new Padding(0, 9, 0, 0);
            lblCopyState.Size = new Size(88, 24);
            lblCopyState.TabIndex = 32;
            lblCopyState.Text = "State for copies";
            // 
            // xtbConfig
            // 
            xtbConfig.Dock = DockStyle.Top;
            xtbConfig.Location = new Point(5, 162);
            xtbConfig.Name = "xtbConfig";
            xtbConfig.Size = new Size(547, 96);
            xtbConfig.TabIndex = 31;
            xtbConfig.Text = "";
            // 
            // lblConfig
            // 
            lblConfig.AutoSize = true;
            lblConfig.Dock = DockStyle.Top;
            lblConfig.Location = new Point(5, 138);
            lblConfig.Margin = new Padding(0);
            lblConfig.Name = "lblConfig";
            lblConfig.Padding = new Padding(0, 9, 0, 0);
            lblConfig.Size = new Size(43, 24);
            lblConfig.TabIndex = 30;
            lblConfig.Text = "Config";
            // 
            // vtxtStateClass
            // 
            vtxtStateClass.BackColor = Color.Yellow;
            vtxtStateClass.BothInvalidColor = Color.Red;
            vtxtStateClass.Dock = DockStyle.Top;
            vtxtStateClass.ExternalInvalidColor = Color.Orange;
            vtxtStateClass.ExternalValidation = null;
            vtxtStateClass.InternalInvalidColor = Color.Yellow;
            vtxtStateClass.InvalidReadOnlyColor = Color.LightYellow;
            vtxtStateClass.Location = new Point(5, 115);
            vtxtStateClass.Name = "vtxtStateClass";
            vtxtStateClass.RegularExpression = "^[a-zA-Z0-9_.-]+$";
            vtxtStateClass.Size = new Size(547, 23);
            vtxtStateClass.TabIndex = 28;
            vtxtStateClass.ValidColor = Color.White;
            vtxtStateClass.ValidReadOnlyColor = SystemColors.Control;
            // 
            // lblStateClass
            // 
            lblStateClass.AutoSize = true;
            lblStateClass.Dock = DockStyle.Top;
            lblStateClass.Location = new Point(5, 91);
            lblStateClass.Margin = new Padding(0);
            lblStateClass.Name = "lblStateClass";
            lblStateClass.Padding = new Padding(0, 9, 0, 0);
            lblStateClass.Size = new Size(61, 24);
            lblStateClass.TabIndex = 29;
            lblStateClass.Text = "State class";
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
            vtxtName.RegularExpression = "^[a-zA-Z0-9_.-]+$";
            vtxtName.Size = new Size(547, 23);
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
            txtId.Size = new Size(547, 23);
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
            // EditLifecycleState
            // 
            AcceptButton = cmdOk;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = cmdCancel;
            ClientSize = new Size(557, 410);
            Controls.Add(pData);
            Controls.Add(panel7);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Name = "EditLifecycleState";
            Text = "Edit lifecycle state";
            Load += EditLifecycle_Load;
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
		private System.Windows.Forms.Label lblName;
		private C4GeneralGui.GuiElements.ValidatingTextBox vtxtName;
		private System.Windows.Forms.Panel pCancel;
		private System.Windows.Forms.Button cmdCancel;
		private System.Windows.Forms.Panel pSaveAs;
		private System.Windows.Forms.Button cmdSaveAs;
		private System.Windows.Forms.Panel pSave;
		private System.Windows.Forms.Button cmdOk;
		private System.Windows.Forms.ComboBox cboLifecycle;
		private System.Windows.Forms.Label lblLifecycle;
		private System.Windows.Forms.ComboBox cboCopyState;
		private System.Windows.Forms.Label lblCopyState;
		private C4GeneralGui.GuiElements.XmlTextBox xtbConfig;
		private System.Windows.Forms.Label lblConfig;
		private C4GeneralGui.GuiElements.ValidatingTextBox vtxtStateClass;
		private System.Windows.Forms.Label lblStateClass;
	}
}