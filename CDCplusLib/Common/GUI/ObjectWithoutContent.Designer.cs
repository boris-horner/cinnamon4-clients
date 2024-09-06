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
            this.cboLanguage = new System.Windows.Forms.ComboBox();
            this.lblLanguage = new System.Windows.Forms.Label();
            this.cboObjectType = new System.Windows.Forms.ComboBox();
            this.lblObjType = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.lblName = new System.Windows.Forms.Label();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.cmdOk = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // cboLanguage
            // 
            this.cboLanguage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboLanguage.FormattingEnabled = true;
            this.cboLanguage.Location = new System.Drawing.Point(13, 150);
            this.cboLanguage.Margin = new System.Windows.Forms.Padding(4);
            this.cboLanguage.Name = "cboLanguage";
            this.cboLanguage.Size = new System.Drawing.Size(437, 24);
            this.cboLanguage.Sorted = true;
            this.cboLanguage.TabIndex = 25;
            // 
            // lblLanguage
            // 
            this.lblLanguage.AutoSize = true;
            this.lblLanguage.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblLanguage.Location = new System.Drawing.Point(13, 130);
            this.lblLanguage.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblLanguage.Name = "lblLanguage";
            this.lblLanguage.Size = new System.Drawing.Size(68, 16);
            this.lblLanguage.TabIndex = 24;
            this.lblLanguage.Text = "Language";
            // 
            // cboObjectType
            // 
            this.cboObjectType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboObjectType.FormattingEnabled = true;
            this.cboObjectType.Location = new System.Drawing.Point(13, 89);
            this.cboObjectType.Margin = new System.Windows.Forms.Padding(4);
            this.cboObjectType.Name = "cboObjectType";
            this.cboObjectType.Size = new System.Drawing.Size(437, 24);
            this.cboObjectType.Sorted = true;
            this.cboObjectType.TabIndex = 23;
            // 
            // lblObjType
            // 
            this.lblObjType.AutoSize = true;
            this.lblObjType.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblObjType.Location = new System.Drawing.Point(13, 70);
            this.lblObjType.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblObjType.Name = "lblObjType";
            this.lblObjType.Size = new System.Drawing.Size(75, 16);
            this.lblObjType.TabIndex = 22;
            this.lblObjType.Text = "Object type";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(13, 29);
            this.txtName.Margin = new System.Windows.Forms.Padding(4);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(437, 22);
            this.txtName.TabIndex = 18;
            this.txtName.TextChanged += new System.EventHandler(this.txtName_TextChanged);
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblName.Location = new System.Drawing.Point(13, 9);
            this.lblName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(44, 16);
            this.lblName.TabIndex = 21;
            this.lblName.Text = "Name";
            // 
            // cmdCancel
            // 
            this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdCancel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.cmdCancel.Location = new System.Drawing.Point(121, 196);
            this.cmdCancel.Margin = new System.Windows.Forms.Padding(4);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(100, 28);
            this.cmdCancel.TabIndex = 20;
            this.cmdCancel.Text = "Cancel";
            this.cmdCancel.UseVisualStyleBackColor = true;
            // 
            // cmdOk
            // 
            this.cmdOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.cmdOk.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.cmdOk.Location = new System.Drawing.Point(13, 196);
            this.cmdOk.Margin = new System.Windows.Forms.Padding(4);
            this.cmdOk.Name = "cmdOk";
            this.cmdOk.Size = new System.Drawing.Size(100, 28);
            this.cmdOk.TabIndex = 19;
            this.cmdOk.Text = "Ok";
            this.cmdOk.UseVisualStyleBackColor = true;
            // 
            // ObjectWithoutContent
            // 
            this.AcceptButton = this.cmdOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cmdCancel;
            this.ClientSize = new System.Drawing.Size(463, 242);
            this.Controls.Add(this.cboLanguage);
            this.Controls.Add(this.lblLanguage);
            this.Controls.Add(this.cboObjectType);
            this.Controls.Add(this.lblObjType);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.cmdOk);
            this.Name = "ObjectWithoutContent";
            this.Text = "ObjectWithoutContent";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.ComboBox cboLanguage;
        internal System.Windows.Forms.Label lblLanguage;
        internal System.Windows.Forms.ComboBox cboObjectType;
        internal System.Windows.Forms.Label lblObjType;
        internal System.Windows.Forms.TextBox txtName;
        internal System.Windows.Forms.Label lblName;
        internal System.Windows.Forms.Button cmdCancel;
        internal System.Windows.Forms.Button cmdOk;
    }
}