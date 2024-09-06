
namespace CDCplusLib.Common.GUI
{
    partial class EditStringValue
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
            this.lblValue = new System.Windows.Forms.Label();
            this.TableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.cmdOk = new System.Windows.Forms.Button();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.vtxtValue = new C4GeneralGui.GuiElements.ValidatingTextBox();
            this.TableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblValue
            // 
            this.lblValue.AutoSize = true;
            this.lblValue.Location = new System.Drawing.Point(9, 8);
            this.lblValue.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblValue.Name = "lblValue";
            this.lblValue.Size = new System.Drawing.Size(44, 17);
            this.lblValue.TabIndex = 4;
            this.lblValue.Text = "Value";
            // 
            // TableLayoutPanel1
            // 
            this.TableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.TableLayoutPanel1.ColumnCount = 2;
            this.TableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.TableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.TableLayoutPanel1.Controls.Add(this.cmdOk, 0, 0);
            this.TableLayoutPanel1.Controls.Add(this.cmdCancel, 1, 0);
            this.TableLayoutPanel1.Location = new System.Drawing.Point(12, 75);
            this.TableLayoutPanel1.Margin = new System.Windows.Forms.Padding(4);
            this.TableLayoutPanel1.Name = "TableLayoutPanel1";
            this.TableLayoutPanel1.RowCount = 1;
            this.TableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.TableLayoutPanel1.Size = new System.Drawing.Size(195, 36);
            this.TableLayoutPanel1.TabIndex = 3;
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
            this.cmdOk.Click += new System.EventHandler(this.cmdOk_Click);
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
            this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
            // 
            // vtxtValue
            // 
            this.vtxtValue.BackColor = System.Drawing.Color.Yellow;
            this.vtxtValue.ExternalInvalidColor = System.Drawing.Color.Yellow;
            this.vtxtValue.ExternalValidation = true;
            this.vtxtValue.InternalInvalidColor = System.Drawing.Color.Yellow;
            this.vtxtValue.Location = new System.Drawing.Point(12, 29);
            this.vtxtValue.Name = "vtxtValue";
            this.vtxtValue.RegularExpression = "^([^/\\\\]+)$";
            this.vtxtValue.Size = new System.Drawing.Size(549, 22);
            this.vtxtValue.TabIndex = 5;
            this.vtxtValue.ValidColor = System.Drawing.Color.White;
            this.vtxtValue.TextChanged += new System.EventHandler(this.vtxtValue_TextChanged);
            // 
            // EditStringValue
            // 
            this.AcceptButton = this.cmdOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cmdCancel;
            this.ClientSize = new System.Drawing.Size(573, 124);
            this.Controls.Add(this.vtxtValue);
            this.Controls.Add(this.lblValue);
            this.Controls.Add(this.TableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "EditStringValue";
            this.Text = "EditStringValue";
            this.TableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.Label lblValue;
        internal System.Windows.Forms.TableLayoutPanel TableLayoutPanel1;
        internal System.Windows.Forms.Button cmdOk;
        internal System.Windows.Forms.Button cmdCancel;
        private C4GeneralGui.GuiElements.ValidatingTextBox vtxtValue;
    }
}