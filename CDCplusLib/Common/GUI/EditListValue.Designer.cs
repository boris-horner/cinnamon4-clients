
namespace CDCplusLib.Common.GUI
{
	partial class EditListValue
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
			lblValue = new Label();
			TableLayoutPanel1 = new TableLayoutPanel();
			cmdOk = new Button();
			cmdCancel = new Button();
			cboListValues = new ComboBox();
			TableLayoutPanel1.SuspendLayout();
			SuspendLayout();
			// 
			// lblValue
			// 
			lblValue.AutoSize = true;
			lblValue.Location = new Point(8, 8);
			lblValue.Margin = new Padding(4, 0, 4, 0);
			lblValue.Name = "lblValue";
			lblValue.Size = new Size(35, 15);
			lblValue.TabIndex = 4;
			lblValue.Text = "Value";
			// 
			// TableLayoutPanel1
			// 
			TableLayoutPanel1.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			TableLayoutPanel1.ColumnCount = 2;
			TableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
			TableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
			TableLayoutPanel1.Controls.Add(cmdOk, 0, 0);
			TableLayoutPanel1.Controls.Add(cmdCancel, 1, 0);
			TableLayoutPanel1.Location = new Point(10, 70);
			TableLayoutPanel1.Margin = new Padding(4);
			TableLayoutPanel1.Name = "TableLayoutPanel1";
			TableLayoutPanel1.RowCount = 1;
			TableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
			TableLayoutPanel1.Size = new Size(171, 34);
			TableLayoutPanel1.TabIndex = 3;
			// 
			// cmdOk
			// 
			cmdOk.Anchor = AnchorStyles.None;
			cmdOk.Location = new Point(4, 4);
			cmdOk.Margin = new Padding(4);
			cmdOk.Name = "cmdOk";
			cmdOk.Size = new Size(77, 26);
			cmdOk.TabIndex = 0;
			cmdOk.Text = "OK";
			cmdOk.Click += cmdOk_Click;
			// 
			// cmdCancel
			// 
			cmdCancel.Anchor = AnchorStyles.None;
			cmdCancel.DialogResult = DialogResult.Cancel;
			cmdCancel.Location = new Point(89, 4);
			cmdCancel.Margin = new Padding(4);
			cmdCancel.Name = "cmdCancel";
			cmdCancel.Size = new Size(78, 26);
			cmdCancel.TabIndex = 1;
			cmdCancel.Text = "Cancel";
			cmdCancel.Click += cmdCancel_Click;
			// 
			// cboListValues
			// 
			cboListValues.DropDownStyle = ComboBoxStyle.DropDownList;
			cboListValues.FormattingEnabled = true;
			cboListValues.Location = new Point(10, 27);
			cboListValues.Name = "cboListValues";
			cboListValues.Size = new Size(481, 23);
			cboListValues.Sorted = true;
			cboListValues.TabIndex = 6;
			cboListValues.SelectedIndexChanged += cboListValues_SelectedIndexChanged;
			// 
			// EditListValue
			// 
			AcceptButton = cmdOk;
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			CancelButton = cmdCancel;
			ClientSize = new Size(501, 116);
			Controls.Add(cboListValues);
			Controls.Add(lblValue);
			Controls.Add(TableLayoutPanel1);
			FormBorderStyle = FormBorderStyle.FixedDialog;
			Name = "EditListValue";
			Text = "Edit list value";
			TableLayoutPanel1.ResumeLayout(false);
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		internal Label lblValue;
		internal TableLayoutPanel TableLayoutPanel1;
		internal Button cmdOk;
		internal Button cmdCancel;
		private ComboBox cboListValues;
	}
}