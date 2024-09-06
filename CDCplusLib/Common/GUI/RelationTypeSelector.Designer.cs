namespace CDCplusLib.Common.GUI
{
    partial class RelationTypeSelector
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
            this.TableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.cmdOk = new System.Windows.Forms.Button();
            this.lblRelType = new System.Windows.Forms.Label();
            this.cboRelType = new System.Windows.Forms.ComboBox();
            this.TableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // TableLayoutPanel1
            // 
            this.TableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.TableLayoutPanel1.ColumnCount = 2;
            this.TableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.TableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.TableLayoutPanel1.Controls.Add(this.cmdCancel, 1, 0);
            this.TableLayoutPanel1.Controls.Add(this.cmdOk, 0, 0);
            this.TableLayoutPanel1.Location = new System.Drawing.Point(13, 77);
            this.TableLayoutPanel1.Margin = new System.Windows.Forms.Padding(4);
            this.TableLayoutPanel1.Name = "TableLayoutPanel1";
            this.TableLayoutPanel1.RowCount = 1;
            this.TableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.TableLayoutPanel1.Size = new System.Drawing.Size(195, 36);
            this.TableLayoutPanel1.TabIndex = 5;
            // 
            // cmdCancel
            // 
            this.cmdCancel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdCancel.Location = new System.Drawing.Point(101, 4);
            this.cmdCancel.Margin = new System.Windows.Forms.Padding(4);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(89, 28);
            this.cmdCancel.TabIndex = 1;
            this.cmdCancel.Text = "Cancel";
            this.cmdCancel.Click += new System.EventHandler(this.Cancel_Button_Click);
            // 
            // cmdOk
            // 
            this.cmdOk.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.cmdOk.Location = new System.Drawing.Point(4, 4);
            this.cmdOk.Margin = new System.Windows.Forms.Padding(4);
            this.cmdOk.Name = "cmdOk";
            this.cmdOk.Size = new System.Drawing.Size(89, 28);
            this.cmdOk.TabIndex = 0;
            this.cmdOk.Text = "OK";
            this.cmdOk.Click += new System.EventHandler(this.OK_Button_Click);
            // 
            // lblRelType
            // 
            this.lblRelType.AutoSize = true;
            this.lblRelType.Location = new System.Drawing.Point(12, 11);
            this.lblRelType.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblRelType.Name = "lblRelType";
            this.lblRelType.Size = new System.Drawing.Size(86, 16);
            this.lblRelType.TabIndex = 7;
            this.lblRelType.Text = "Relation type";
            // 
            // cboRelType
            // 
            this.cboRelType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboRelType.FormattingEnabled = true;
            this.cboRelType.Location = new System.Drawing.Point(13, 30);
            this.cboRelType.Margin = new System.Windows.Forms.Padding(4);
            this.cboRelType.Name = "cboRelType";
            this.cboRelType.Size = new System.Drawing.Size(547, 24);
            this.cboRelType.Sorted = true;
            this.cboRelType.TabIndex = 6;
            // 
            // RelationTypeSelector
            // 
            this.AcceptButton = this.cmdOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cmdCancel;
            this.ClientSize = new System.Drawing.Size(574, 126);
            this.Controls.Add(this.TableLayoutPanel1);
            this.Controls.Add(this.lblRelType);
            this.Controls.Add(this.cboRelType);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "RelationTypeSelector";
            this.Text = "RelationTypeSelector";
            this.TableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.TableLayoutPanel TableLayoutPanel1;
        internal System.Windows.Forms.Button cmdCancel;
        internal System.Windows.Forms.Button cmdOk;
        internal System.Windows.Forms.Label lblRelType;
        internal System.Windows.Forms.ComboBox cboRelType;
    }
}