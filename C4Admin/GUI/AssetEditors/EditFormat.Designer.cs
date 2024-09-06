
namespace C4Admin.GUI.AssetEditors
{
	partial class EditFormat
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
			cboIndexMode = new System.Windows.Forms.ComboBox();
			lblIndexMode = new System.Windows.Forms.Label();
			cboDefaultObjectTypeId = new System.Windows.Forms.ComboBox();
			lblDefaultObjectType = new System.Windows.Forms.Label();
			vtxtContentType = new C4GeneralGui.GuiElements.ValidatingTextBox();
			lblContentType = new System.Windows.Forms.Label();
			vtxtExtension = new C4GeneralGui.GuiElements.ValidatingTextBox();
			lblExtension = new System.Windows.Forms.Label();
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
			panel7.Location = new System.Drawing.Point(0, 292);
			panel7.Name = "panel7";
			panel7.Size = new System.Drawing.Size(457, 37);
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
			pData.Controls.Add(cboIndexMode);
			pData.Controls.Add(lblIndexMode);
			pData.Controls.Add(cboDefaultObjectTypeId);
			pData.Controls.Add(lblDefaultObjectType);
			pData.Controls.Add(vtxtContentType);
			pData.Controls.Add(lblContentType);
			pData.Controls.Add(vtxtExtension);
			pData.Controls.Add(lblExtension);
			pData.Controls.Add(vtxtName);
			pData.Controls.Add(lblName);
			pData.Controls.Add(txtId);
			pData.Controls.Add(lblId);
			pData.Dock = System.Windows.Forms.DockStyle.Fill;
			pData.Location = new System.Drawing.Point(0, 0);
			pData.Name = "pData";
			pData.Padding = new System.Windows.Forms.Padding(5, 6, 5, 6);
			pData.Size = new System.Drawing.Size(457, 292);
			pData.TabIndex = 0;
			// 
			// cboIndexMode
			// 
			cboIndexMode.Dock = System.Windows.Forms.DockStyle.Top;
			cboIndexMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			cboIndexMode.FormattingEnabled = true;
			cboIndexMode.Items.AddRange(new object[] { "NONE", "PLAIN_TEXT", "TIKA", "XML" });
			cboIndexMode.Location = new System.Drawing.Point(5, 256);
			cboIndexMode.Name = "cboIndexMode";
			cboIndexMode.Size = new System.Drawing.Size(447, 23);
			cboIndexMode.Sorted = true;
			cboIndexMode.TabIndex = 15;
			// 
			// lblIndexMode
			// 
			lblIndexMode.AutoSize = true;
			lblIndexMode.Dock = System.Windows.Forms.DockStyle.Top;
			lblIndexMode.Location = new System.Drawing.Point(5, 232);
			lblIndexMode.Margin = new System.Windows.Forms.Padding(0);
			lblIndexMode.Name = "lblIndexMode";
			lblIndexMode.Padding = new System.Windows.Forms.Padding(0, 9, 0, 0);
			lblIndexMode.Size = new System.Drawing.Size(69, 24);
			lblIndexMode.TabIndex = 14;
			lblIndexMode.Text = "Index mode";
			// 
			// cboDefaultObjectTypeId
			// 
			cboDefaultObjectTypeId.Dock = System.Windows.Forms.DockStyle.Top;
			cboDefaultObjectTypeId.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			cboDefaultObjectTypeId.FormattingEnabled = true;
			cboDefaultObjectTypeId.Location = new System.Drawing.Point(5, 209);
			cboDefaultObjectTypeId.Name = "cboDefaultObjectTypeId";
			cboDefaultObjectTypeId.Size = new System.Drawing.Size(447, 23);
			cboDefaultObjectTypeId.Sorted = true;
			cboDefaultObjectTypeId.TabIndex = 13;
			// 
			// lblDefaultObjectType
			// 
			lblDefaultObjectType.AutoSize = true;
			lblDefaultObjectType.Dock = System.Windows.Forms.DockStyle.Top;
			lblDefaultObjectType.Location = new System.Drawing.Point(5, 185);
			lblDefaultObjectType.Margin = new System.Windows.Forms.Padding(0);
			lblDefaultObjectType.Name = "lblDefaultObjectType";
			lblDefaultObjectType.Padding = new System.Windows.Forms.Padding(0, 9, 0, 0);
			lblDefaultObjectType.Size = new System.Drawing.Size(107, 24);
			lblDefaultObjectType.TabIndex = 12;
			lblDefaultObjectType.Text = "Default object type";
			// 
			// vtxtContentType
			// 
			vtxtContentType.BackColor = System.Drawing.Color.Yellow;
			vtxtContentType.BothInvalidColor = System.Drawing.Color.Red;
			vtxtContentType.Dock = System.Windows.Forms.DockStyle.Top;
			vtxtContentType.ExternalInvalidColor = System.Drawing.Color.Orange;
			vtxtContentType.ExternalValidation = null;
			vtxtContentType.InternalInvalidColor = System.Drawing.Color.Yellow;
			vtxtContentType.InvalidReadOnlyColor = System.Drawing.Color.LightYellow;
			vtxtContentType.Location = new System.Drawing.Point(5, 162);
			vtxtContentType.Name = "vtxtContentType";
			vtxtContentType.RegularExpression = "^[a-zA-Z0-9\\-\\+.//]+$";
			vtxtContentType.Size = new System.Drawing.Size(447, 23);
			vtxtContentType.TabIndex = 4;
			vtxtContentType.ValidColor = System.Drawing.Color.White;
			vtxtContentType.ValidReadOnlyColor = System.Drawing.SystemColors.Control;
			vtxtContentType.TextChanged += vtxtContentType_TextChanged;
			// 
			// lblContentType
			// 
			lblContentType.AutoSize = true;
			lblContentType.Dock = System.Windows.Forms.DockStyle.Top;
			lblContentType.Location = new System.Drawing.Point(5, 138);
			lblContentType.Margin = new System.Windows.Forms.Padding(0);
			lblContentType.Name = "lblContentType";
			lblContentType.Padding = new System.Windows.Forms.Padding(0, 9, 0, 0);
			lblContentType.Size = new System.Drawing.Size(76, 24);
			lblContentType.TabIndex = 10;
			lblContentType.Text = "Content type";
			// 
			// vtxtExtension
			// 
			vtxtExtension.BackColor = System.Drawing.Color.Yellow;
			vtxtExtension.BothInvalidColor = System.Drawing.Color.Red;
			vtxtExtension.Dock = System.Windows.Forms.DockStyle.Top;
			vtxtExtension.ExternalInvalidColor = System.Drawing.Color.Orange;
			vtxtExtension.ExternalValidation = null;
			vtxtExtension.InternalInvalidColor = System.Drawing.Color.Yellow;
			vtxtExtension.InvalidReadOnlyColor = System.Drawing.Color.LightYellow;
			vtxtExtension.Location = new System.Drawing.Point(5, 115);
			vtxtExtension.Name = "vtxtExtension";
			vtxtExtension.RegularExpression = "^[a-zA-Z0-9_.-]+$";
			vtxtExtension.Size = new System.Drawing.Size(447, 23);
			vtxtExtension.TabIndex = 3;
			vtxtExtension.ValidColor = System.Drawing.Color.White;
			vtxtExtension.ValidReadOnlyColor = System.Drawing.SystemColors.Control;
			vtxtExtension.TextChanged += vtxtExtension_TextChanged;
			// 
			// lblExtension
			// 
			lblExtension.AutoSize = true;
			lblExtension.Dock = System.Windows.Forms.DockStyle.Top;
			lblExtension.Location = new System.Drawing.Point(5, 91);
			lblExtension.Margin = new System.Windows.Forms.Padding(0);
			lblExtension.Name = "lblExtension";
			lblExtension.Padding = new System.Windows.Forms.Padding(0, 9, 0, 0);
			lblExtension.Size = new System.Drawing.Size(57, 24);
			lblExtension.TabIndex = 7;
			lblExtension.Text = "Extension";
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
			vtxtName.Size = new System.Drawing.Size(447, 23);
			vtxtName.TabIndex = 2;
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
			txtId.Size = new System.Drawing.Size(447, 23);
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
			// EditFormat
			// 
			AcceptButton = cmdOk;
			AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			CancelButton = cmdCancel;
			ClientSize = new System.Drawing.Size(457, 329);
			Controls.Add(pData);
			Controls.Add(panel7);
			FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			Name = "EditFormat";
			Text = "Edit format";
			Load += EditFormat_Load;
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
		private C4GeneralGui.GuiElements.ValidatingTextBox vtxtContentType;
		private System.Windows.Forms.Label lblContentType;
		private C4GeneralGui.GuiElements.ValidatingTextBox vtxtExtension;
		private System.Windows.Forms.Label lblExtension;
		private System.Windows.Forms.Label lblDefaultObjectType;
		private System.Windows.Forms.ComboBox cboDefaultObjectTypeId;
		private System.Windows.Forms.ComboBox cboIndexMode;
		private System.Windows.Forms.Label lblIndexMode;
	}
}