
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
			panel7 = new System.Windows.Forms.Panel();
			pCancel = new System.Windows.Forms.Panel();
			cmdCancel = new System.Windows.Forms.Button();
			pSaveAs = new System.Windows.Forms.Panel();
			cmdSaveAs = new System.Windows.Forms.Button();
			pSave = new System.Windows.Forms.Panel();
			cmdOk = new System.Windows.Forms.Button();
			pData = new System.Windows.Forms.Panel();
			cboLifecycle = new System.Windows.Forms.ComboBox();
			lblLifecycle = new System.Windows.Forms.Label();
			cboCopyState = new System.Windows.Forms.ComboBox();
			lblCopyState = new System.Windows.Forms.Label();
			xtbConfig = new C4GeneralGui.GuiElements.XmlTextBox();
			lblConfig = new System.Windows.Forms.Label();
			vtxtStateClass = new C4GeneralGui.GuiElements.ValidatingTextBox();
			lblStateClass = new System.Windows.Forms.Label();
			vtxtName = new C4GeneralGui.GuiElements.ValidatingTextBox();
			lblName = new System.Windows.Forms.Label();
			txtId = new System.Windows.Forms.TextBox();
			lblId = new System.Windows.Forms.Label();
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
			panel7.Dock = System.Windows.Forms.DockStyle.Bottom;
			panel7.Location = new System.Drawing.Point(0, 373);
			panel7.Name = "panel7";
			panel7.Size = new System.Drawing.Size(557, 37);
			panel7.TabIndex = 6;
			// 
			// pCancel
			// 
			pCancel.Controls.Add(cmdCancel);
			pCancel.Dock = System.Windows.Forms.DockStyle.Left;
			pCancel.Location = new System.Drawing.Point(224, 0);
			pCancel.Name = "pCancel";
			pCancel.Padding = new System.Windows.Forms.Padding(6, 7, 10, 7);
			pCancel.Size = new System.Drawing.Size(112, 37);
			pCancel.TabIndex = 11;
			// 
			// cmdCancel
			// 
			cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			cmdCancel.Dock = System.Windows.Forms.DockStyle.Fill;
			cmdCancel.Location = new System.Drawing.Point(6, 7);
			cmdCancel.Name = "cmdCancel";
			cmdCancel.Size = new System.Drawing.Size(96, 23);
			cmdCancel.TabIndex = 9;
			cmdCancel.Text = "Cancel";
			cmdCancel.UseVisualStyleBackColor = true;
			cmdCancel.Click += cmdCancel_Click;
			// 
			// pSaveAs
			// 
			pSaveAs.Controls.Add(cmdSaveAs);
			pSaveAs.Dock = System.Windows.Forms.DockStyle.Left;
			pSaveAs.Location = new System.Drawing.Point(112, 0);
			pSaveAs.Name = "pSaveAs";
			pSaveAs.Padding = new System.Windows.Forms.Padding(6, 7, 10, 7);
			pSaveAs.Size = new System.Drawing.Size(112, 37);
			pSaveAs.TabIndex = 10;
			// 
			// cmdSaveAs
			// 
			cmdSaveAs.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			cmdSaveAs.Dock = System.Windows.Forms.DockStyle.Fill;
			cmdSaveAs.Location = new System.Drawing.Point(6, 7);
			cmdSaveAs.Name = "cmdSaveAs";
			cmdSaveAs.Size = new System.Drawing.Size(96, 23);
			cmdSaveAs.TabIndex = 6;
			cmdSaveAs.Text = "Save as";
			cmdSaveAs.UseVisualStyleBackColor = true;
			cmdSaveAs.Click += cmdSaveAs_Click;
			// 
			// pSave
			// 
			pSave.Controls.Add(cmdOk);
			pSave.Dock = System.Windows.Forms.DockStyle.Left;
			pSave.Location = new System.Drawing.Point(0, 0);
			pSave.Name = "pSave";
			pSave.Padding = new System.Windows.Forms.Padding(6, 7, 10, 7);
			pSave.Size = new System.Drawing.Size(112, 37);
			pSave.TabIndex = 9;
			// 
			// cmdOk
			// 
			cmdOk.Dock = System.Windows.Forms.DockStyle.Fill;
			cmdOk.Location = new System.Drawing.Point(6, 7);
			cmdOk.Name = "cmdOk";
			cmdOk.Size = new System.Drawing.Size(96, 23);
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
			pData.Dock = System.Windows.Forms.DockStyle.Fill;
			pData.Location = new System.Drawing.Point(0, 0);
			pData.Name = "pData";
			pData.Padding = new System.Windows.Forms.Padding(5, 6, 5, 6);
			pData.Size = new System.Drawing.Size(557, 373);
			pData.TabIndex = 0;
			// 
			// cboLifecycle
			// 
			cboLifecycle.Dock = System.Windows.Forms.DockStyle.Top;
			cboLifecycle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			cboLifecycle.FormattingEnabled = true;
			cboLifecycle.Location = new System.Drawing.Point(5, 329);
			cboLifecycle.Name = "cboLifecycle";
			cboLifecycle.Size = new System.Drawing.Size(547, 23);
			cboLifecycle.Sorted = true;
			cboLifecycle.TabIndex = 35;
			cboLifecycle.SelectedIndexChanged += cboLifecycle_SelectedIndexChanged;
			// 
			// lblLifecycle
			// 
			lblLifecycle.AutoSize = true;
			lblLifecycle.Dock = System.Windows.Forms.DockStyle.Top;
			lblLifecycle.Location = new System.Drawing.Point(5, 305);
			lblLifecycle.Margin = new System.Windows.Forms.Padding(0);
			lblLifecycle.Name = "lblLifecycle";
			lblLifecycle.Padding = new System.Windows.Forms.Padding(0, 9, 0, 0);
			lblLifecycle.Size = new System.Drawing.Size(53, 24);
			lblLifecycle.TabIndex = 34;
			lblLifecycle.Text = "Lifecycle";
			// 
			// cboCopyState
			// 
			cboCopyState.Dock = System.Windows.Forms.DockStyle.Top;
			cboCopyState.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			cboCopyState.FormattingEnabled = true;
			cboCopyState.Location = new System.Drawing.Point(5, 282);
			cboCopyState.Name = "cboCopyState";
			cboCopyState.Size = new System.Drawing.Size(547, 23);
			cboCopyState.Sorted = true;
			cboCopyState.TabIndex = 33;
			// 
			// lblCopyState
			// 
			lblCopyState.AutoSize = true;
			lblCopyState.Dock = System.Windows.Forms.DockStyle.Top;
			lblCopyState.Location = new System.Drawing.Point(5, 258);
			lblCopyState.Margin = new System.Windows.Forms.Padding(0);
			lblCopyState.Name = "lblCopyState";
			lblCopyState.Padding = new System.Windows.Forms.Padding(0, 9, 0, 0);
			lblCopyState.Size = new System.Drawing.Size(88, 24);
			lblCopyState.TabIndex = 32;
			lblCopyState.Text = "State for copies";
			// 
			// xtbConfig
			// 
			xtbConfig.Dock = System.Windows.Forms.DockStyle.Top;
			xtbConfig.Location = new System.Drawing.Point(5, 162);
			xtbConfig.Name = "xtbConfig";
			xtbConfig.Size = new System.Drawing.Size(547, 96);
			xtbConfig.TabIndex = 31;
			xtbConfig.Text = "";
			// 
			// lblConfig
			// 
			lblConfig.AutoSize = true;
			lblConfig.Dock = System.Windows.Forms.DockStyle.Top;
			lblConfig.Location = new System.Drawing.Point(5, 138);
			lblConfig.Margin = new System.Windows.Forms.Padding(0);
			lblConfig.Name = "lblConfig";
			lblConfig.Padding = new System.Windows.Forms.Padding(0, 9, 0, 0);
			lblConfig.Size = new System.Drawing.Size(43, 24);
			lblConfig.TabIndex = 30;
			lblConfig.Text = "Config";
			// 
			// vtxtStateClass
			// 
			vtxtStateClass.BackColor = System.Drawing.Color.Yellow;
			vtxtStateClass.BothInvalidColor = System.Drawing.Color.Red;
			vtxtStateClass.Dock = System.Windows.Forms.DockStyle.Top;
			vtxtStateClass.ExternalInvalidColor = System.Drawing.Color.Orange;
			vtxtStateClass.ExternalValidation = null;
			vtxtStateClass.InternalInvalidColor = System.Drawing.Color.Yellow;
			vtxtStateClass.InvalidReadOnlyColor = System.Drawing.Color.LightYellow;
			vtxtStateClass.Location = new System.Drawing.Point(5, 115);
			vtxtStateClass.Name = "vtxtStateClass";
			vtxtStateClass.RegularExpression = "^[a-zA-Z0-9_.-]+$";
			vtxtStateClass.Size = new System.Drawing.Size(547, 23);
			vtxtStateClass.TabIndex = 28;
			vtxtStateClass.ValidColor = System.Drawing.Color.White;
			vtxtStateClass.ValidReadOnlyColor = System.Drawing.SystemColors.Control;
			// 
			// lblStateClass
			// 
			lblStateClass.AutoSize = true;
			lblStateClass.Dock = System.Windows.Forms.DockStyle.Top;
			lblStateClass.Location = new System.Drawing.Point(5, 91);
			lblStateClass.Margin = new System.Windows.Forms.Padding(0);
			lblStateClass.Name = "lblStateClass";
			lblStateClass.Padding = new System.Windows.Forms.Padding(0, 9, 0, 0);
			lblStateClass.Size = new System.Drawing.Size(61, 24);
			lblStateClass.TabIndex = 29;
			lblStateClass.Text = "State class";
			// 
			// vtxtName
			// 
			vtxtName.BackColor = System.Drawing.Color.Yellow;
			vtxtName.BothInvalidColor = System.Drawing.Color.Red;
			vtxtName.Dock = System.Windows.Forms.DockStyle.Top;
			vtxtName.ExternalInvalidColor = System.Drawing.Color.Orange;
			vtxtName.ExternalValidation = null;
			vtxtName.InternalInvalidColor = System.Drawing.Color.Yellow;
			vtxtName.InvalidReadOnlyColor = System.Drawing.Color.LightYellow;
			vtxtName.Location = new System.Drawing.Point(5, 68);
			vtxtName.Name = "vtxtName";
			vtxtName.RegularExpression = "^[a-zA-Z0-9_.-]+$";
			vtxtName.Size = new System.Drawing.Size(547, 23);
			vtxtName.TabIndex = 0;
			vtxtName.ValidColor = System.Drawing.Color.White;
			vtxtName.ValidReadOnlyColor = System.Drawing.SystemColors.Control;
			vtxtName.TextChanged += vtxtName_TextChanged;
			// 
			// lblName
			// 
			lblName.AutoSize = true;
			lblName.Dock = System.Windows.Forms.DockStyle.Top;
			lblName.Location = new System.Drawing.Point(5, 44);
			lblName.Margin = new System.Windows.Forms.Padding(0);
			lblName.Name = "lblName";
			lblName.Padding = new System.Windows.Forms.Padding(0, 9, 0, 0);
			lblName.Size = new System.Drawing.Size(39, 24);
			lblName.TabIndex = 4;
			lblName.Text = "Name";
			// 
			// txtId
			// 
			txtId.Dock = System.Windows.Forms.DockStyle.Top;
			txtId.Location = new System.Drawing.Point(5, 21);
			txtId.Margin = new System.Windows.Forms.Padding(0);
			txtId.Name = "txtId";
			txtId.ReadOnly = true;
			txtId.Size = new System.Drawing.Size(547, 23);
			txtId.TabIndex = 1;
			// 
			// lblId
			// 
			lblId.AutoSize = true;
			lblId.Dock = System.Windows.Forms.DockStyle.Top;
			lblId.Location = new System.Drawing.Point(5, 6);
			lblId.Margin = new System.Windows.Forms.Padding(0);
			lblId.Name = "lblId";
			lblId.Size = new System.Drawing.Size(17, 15);
			lblId.TabIndex = 0;
			lblId.Text = "Id";
			// 
			// EditLifecycleState
			// 
			AcceptButton = cmdOk;
			AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			CancelButton = cmdCancel;
			ClientSize = new System.Drawing.Size(557, 410);
			Controls.Add(pData);
			Controls.Add(panel7);
			FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
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