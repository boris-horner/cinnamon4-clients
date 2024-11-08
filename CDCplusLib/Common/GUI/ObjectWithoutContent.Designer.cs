namespace CDCplusLib.Common.GUI
{
    partial class ObjectWithoutContent
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
            cboLanguage = new ComboBox();
            lblLanguage = new Label();
            cboObjectType = new ComboBox();
            lblObjType = new Label();
            txtName = new TextBox();
            lblName = new Label();
            panel3 = new Panel();
            cmdOk = new Button();
            panel4 = new Panel();
            cmdCancel = new Button();
            panel3.SuspendLayout();
            SuspendLayout();
            // 
            // cboLanguage
            // 
            cboLanguage.Dock = DockStyle.Top;
            cboLanguage.DropDownStyle = ComboBoxStyle.DropDownList;
            cboLanguage.FormattingEnabled = true;
            cboLanguage.Location = new Point(10, 121);
            cboLanguage.Margin = new Padding(4);
            cboLanguage.Name = "cboLanguage";
            cboLanguage.Size = new Size(464, 23);
            cboLanguage.Sorted = true;
            cboLanguage.TabIndex = 25;
            // 
            // lblLanguage
            // 
            lblLanguage.AutoSize = true;
            lblLanguage.Dock = DockStyle.Top;
            lblLanguage.ImeMode = ImeMode.NoControl;
            lblLanguage.Location = new Point(10, 96);
            lblLanguage.Margin = new Padding(4, 0, 4, 0);
            lblLanguage.Name = "lblLanguage";
            lblLanguage.Padding = new Padding(0, 10, 0, 0);
            lblLanguage.Size = new Size(59, 25);
            lblLanguage.TabIndex = 24;
            lblLanguage.Text = "Language";
            // 
            // cboObjectType
            // 
            cboObjectType.Dock = DockStyle.Top;
            cboObjectType.DropDownStyle = ComboBoxStyle.DropDownList;
            cboObjectType.FormattingEnabled = true;
            cboObjectType.Location = new Point(10, 73);
            cboObjectType.Margin = new Padding(4);
            cboObjectType.Name = "cboObjectType";
            cboObjectType.Size = new Size(464, 23);
            cboObjectType.Sorted = true;
            cboObjectType.TabIndex = 23;
            // 
            // lblObjType
            // 
            lblObjType.AutoSize = true;
            lblObjType.Dock = DockStyle.Top;
            lblObjType.ImeMode = ImeMode.NoControl;
            lblObjType.Location = new Point(10, 48);
            lblObjType.Margin = new Padding(4, 0, 4, 0);
            lblObjType.Name = "lblObjType";
            lblObjType.Padding = new Padding(0, 10, 0, 0);
            lblObjType.Size = new Size(68, 25);
            lblObjType.TabIndex = 22;
            lblObjType.Text = "Object type";
            // 
            // txtName
            // 
            txtName.Dock = DockStyle.Top;
            txtName.Location = new Point(10, 25);
            txtName.Margin = new Padding(4);
            txtName.Name = "txtName";
            txtName.Size = new Size(464, 23);
            txtName.TabIndex = 18;
            txtName.TextChanged += txtName_TextChanged;
            // 
            // lblName
            // 
            lblName.AutoSize = true;
            lblName.Dock = DockStyle.Top;
            lblName.ImeMode = ImeMode.NoControl;
            lblName.Location = new Point(10, 10);
            lblName.Margin = new Padding(4, 0, 4, 0);
            lblName.Name = "lblName";
            lblName.Size = new Size(39, 15);
            lblName.TabIndex = 21;
            lblName.Text = "Name";
            // 
            // panel3
            // 
            panel3.Controls.Add(cmdOk);
            panel3.Controls.Add(panel4);
            panel3.Controls.Add(cmdCancel);
            panel3.Dock = DockStyle.Top;
            panel3.Location = new Point(10, 144);
            panel3.Name = "panel3";
            panel3.Padding = new Padding(0, 20, 0, 0);
            panel3.Size = new Size(464, 45);
            panel3.TabIndex = 54;
            // 
            // cmdOk
            // 
            cmdOk.DialogResult = DialogResult.OK;
            cmdOk.Dock = DockStyle.Right;
            cmdOk.ImeMode = ImeMode.NoControl;
            cmdOk.Location = new Point(278, 20);
            cmdOk.Margin = new Padding(4);
            cmdOk.Name = "cmdOk";
            cmdOk.Size = new Size(88, 25);
            cmdOk.TabIndex = 37;
            cmdOk.Text = "Ok";
            cmdOk.UseVisualStyleBackColor = true;
            // 
            // panel4
            // 
            panel4.Dock = DockStyle.Right;
            panel4.Location = new Point(366, 20);
            panel4.Name = "panel4";
            panel4.Size = new Size(10, 25);
            panel4.TabIndex = 36;
            // 
            // cmdCancel
            // 
            cmdCancel.DialogResult = DialogResult.Cancel;
            cmdCancel.Dock = DockStyle.Right;
            cmdCancel.ImeMode = ImeMode.NoControl;
            cmdCancel.Location = new Point(376, 20);
            cmdCancel.Margin = new Padding(4);
            cmdCancel.Name = "cmdCancel";
            cmdCancel.Size = new Size(88, 25);
            cmdCancel.TabIndex = 12;
            cmdCancel.Text = "Cancel";
            cmdCancel.UseVisualStyleBackColor = true;
            // 
            // ObjectWithoutContent
            // 
            AcceptButton = cmdOk;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            CancelButton = cmdCancel;
            ClientSize = new Size(484, 197);
            Controls.Add(panel3);
            Controls.Add(cboLanguage);
            Controls.Add(lblLanguage);
            Controls.Add(cboObjectType);
            Controls.Add(lblObjType);
            Controls.Add(txtName);
            Controls.Add(lblName);
            MinimumSize = new Size(500, 0);
            Name = "ObjectWithoutContent";
            Padding = new Padding(10);
            Text = "ObjectWithoutContent";
            panel3.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        internal System.Windows.Forms.ComboBox cboLanguage;
        internal System.Windows.Forms.Label lblLanguage;
        internal System.Windows.Forms.ComboBox cboObjectType;
        internal System.Windows.Forms.Label lblObjType;
        internal System.Windows.Forms.TextBox txtName;
        internal System.Windows.Forms.Label lblName;
        private Panel panel3;
        internal Button cmdOk;
        private Panel panel4;
        internal Button cmdCancel;
    }
}