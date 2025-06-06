﻿
namespace C4Admin.GUI.AssetEditors
{
	partial class EditLifecycle
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
            lvwStates = new ListView();
            colId = new ColumnHeader();
            colName = new ColumnHeader();
            colStateClass = new ColumnHeader();
            colCopyState = new ColumnHeader();
            lblStates = new Label();
            cboDefaultStateId = new ComboBox();
            lblDefaultState = new Label();
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
            panel7.Location = new Point(0, 536);
            panel7.Name = "panel7";
            panel7.Size = new Size(1214, 37);
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
            pData.Controls.Add(lvwStates);
            pData.Controls.Add(lblStates);
            pData.Controls.Add(cboDefaultStateId);
            pData.Controls.Add(lblDefaultState);
            pData.Controls.Add(vtxtName);
            pData.Controls.Add(lblName);
            pData.Controls.Add(txtId);
            pData.Controls.Add(lblId);
            pData.Dock = DockStyle.Fill;
            pData.Location = new Point(0, 0);
            pData.Name = "pData";
            pData.Padding = new Padding(5, 6, 5, 6);
            pData.Size = new Size(1214, 536);
            pData.TabIndex = 0;
            // 
            // lvwStates
            // 
            lvwStates.Columns.AddRange(new ColumnHeader[] { colId, colName, colStateClass, colCopyState });
            lvwStates.Dock = DockStyle.Fill;
            lvwStates.FullRowSelect = true;
            lvwStates.Location = new Point(5, 162);
            lvwStates.Name = "lvwStates";
            lvwStates.Size = new Size(1204, 368);
            lvwStates.Sorting = SortOrder.Ascending;
            lvwStates.TabIndex = 17;
            lvwStates.UseCompatibleStateImageBehavior = false;
            lvwStates.View = View.Details;
            lvwStates.MouseUp += lvwStates_MouseUp;
            // 
            // colId
            // 
            colId.Text = "Id";
            colId.Width = 75;
            // 
            // colName
            // 
            colName.Text = "Name";
            colName.Width = 250;
            // 
            // colStateClass
            // 
            colStateClass.Text = "State class";
            colStateClass.Width = 250;
            // 
            // colCopyState
            // 
            colCopyState.Text = "State for copies";
            colCopyState.Width = 250;
            // 
            // lblStates
            // 
            lblStates.AutoSize = true;
            lblStates.Dock = DockStyle.Top;
            lblStates.Location = new Point(5, 138);
            lblStates.Margin = new Padding(0);
            lblStates.Name = "lblStates";
            lblStates.Padding = new Padding(0, 9, 0, 0);
            lblStates.Size = new Size(38, 24);
            lblStates.TabIndex = 16;
            lblStates.Text = "States";
            // 
            // cboDefaultStateId
            // 
            cboDefaultStateId.Dock = DockStyle.Top;
            cboDefaultStateId.DropDownStyle = ComboBoxStyle.DropDownList;
            cboDefaultStateId.FormattingEnabled = true;
            cboDefaultStateId.Location = new Point(5, 115);
            cboDefaultStateId.Name = "cboDefaultStateId";
            cboDefaultStateId.Size = new Size(1204, 23);
            cboDefaultStateId.Sorted = true;
            cboDefaultStateId.TabIndex = 15;
            // 
            // lblDefaultState
            // 
            lblDefaultState.AutoSize = true;
            lblDefaultState.Dock = DockStyle.Top;
            lblDefaultState.Location = new Point(5, 91);
            lblDefaultState.Margin = new Padding(0);
            lblDefaultState.Name = "lblDefaultState";
            lblDefaultState.Padding = new Padding(0, 9, 0, 0);
            lblDefaultState.Size = new Size(73, 24);
            lblDefaultState.TabIndex = 14;
            lblDefaultState.Text = "Default state";
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
            vtxtName.Size = new Size(1204, 23);
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
            txtId.Size = new Size(1204, 23);
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
            // EditLifecycle
            // 
            AcceptButton = cmdOk;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = cmdCancel;
            ClientSize = new Size(1214, 573);
            Controls.Add(pData);
            Controls.Add(panel7);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Name = "EditLifecycle";
            Text = "Edit lifecycle";
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
		private System.Windows.Forms.ComboBox cboDefaultStateId;
		private System.Windows.Forms.Label lblDefaultState;
		private System.Windows.Forms.ListView lvwStates;
		private System.Windows.Forms.ColumnHeader colId;
		private System.Windows.Forms.ColumnHeader colName;
		private System.Windows.Forms.Label lblStates;
		private System.Windows.Forms.ColumnHeader colStateClass;
		private System.Windows.Forms.ColumnHeader colCopyState;
	}
}