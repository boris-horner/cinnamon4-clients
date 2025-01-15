
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
            lblValue = new Label();
            vtxtValue = new C4GeneralGui.GuiElements.ValidatingTextBox();
            panel3 = new Panel();
            cmdOk = new Button();
            panel4 = new Panel();
            cmdCancel = new Button();
            panel3.SuspendLayout();
            SuspendLayout();
            // 
            // lblValue
            // 
            lblValue.AutoSize = true;
            lblValue.Dock = DockStyle.Top;
            lblValue.Location = new Point(10, 10);
            lblValue.Margin = new Padding(4, 0, 4, 0);
            lblValue.Name = "lblValue";
            lblValue.Size = new Size(35, 15);
            lblValue.TabIndex = 4;
            lblValue.Text = "Value";
            // 
            // vtxtValue
            // 
            vtxtValue.BackColor = Color.Yellow;
            vtxtValue.BothInvalidColor = Color.Red;
            vtxtValue.Dock = DockStyle.Top;
            vtxtValue.ExternalInvalidColor = Color.Yellow;
            vtxtValue.ExternalValidation = true;
            vtxtValue.InternalInvalidColor = Color.Yellow;
            vtxtValue.InvalidReadOnlyColor = Color.LightYellow;
            vtxtValue.Location = new Point(10, 25);
            vtxtValue.Name = "vtxtValue";
            vtxtValue.RegularExpression = "^.+$";
            vtxtValue.Size = new Size(481, 23);
            vtxtValue.TabIndex = 1;
            vtxtValue.ValidColor = Color.White;
            vtxtValue.ValidReadOnlyColor = SystemColors.Control;
            vtxtValue.TextChanged += vtxtValue_TextChanged;
            // 
            // panel3
            // 
            panel3.Controls.Add(cmdOk);
            panel3.Controls.Add(panel4);
            panel3.Controls.Add(cmdCancel);
            panel3.Dock = DockStyle.Top;
            panel3.Location = new Point(10, 48);
            panel3.Name = "panel3";
            panel3.Padding = new Padding(0, 20, 0, 0);
            panel3.Size = new Size(481, 45);
            panel3.TabIndex = 55;
            // 
            // cmdOk
            // 
            cmdOk.DialogResult = DialogResult.OK;
            cmdOk.Dock = DockStyle.Right;
            cmdOk.ImeMode = ImeMode.NoControl;
            cmdOk.Location = new Point(295, 20);
            cmdOk.Margin = new Padding(4);
            cmdOk.Name = "cmdOk";
            cmdOk.Size = new Size(88, 25);
            cmdOk.TabIndex = 37;
            cmdOk.Text = "Ok";
            cmdOk.UseVisualStyleBackColor = true;
            cmdOk.Click += cmdOk_Click;
            // 
            // panel4
            // 
            panel4.Dock = DockStyle.Right;
            panel4.Location = new Point(383, 20);
            panel4.Name = "panel4";
            panel4.Size = new Size(10, 25);
            panel4.TabIndex = 36;
            // 
            // cmdCancel
            // 
            cmdCancel.DialogResult = DialogResult.Cancel;
            cmdCancel.Dock = DockStyle.Right;
            cmdCancel.ImeMode = ImeMode.NoControl;
            cmdCancel.Location = new Point(393, 20);
            cmdCancel.Margin = new Padding(4);
            cmdCancel.Name = "cmdCancel";
            cmdCancel.Size = new Size(88, 25);
            cmdCancel.TabIndex = 12;
            cmdCancel.Text = "Cancel";
            cmdCancel.UseVisualStyleBackColor = true;
            cmdCancel.Click += cmdCancel_Click;
            // 
            // EditStringValue
            // 
            AcceptButton = cmdOk;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            CancelButton = cmdCancel;
            ClientSize = new Size(501, 116);
            Controls.Add(panel3);
            Controls.Add(vtxtValue);
            Controls.Add(lblValue);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MinimumSize = new Size(500, 0);
            Name = "EditStringValue";
            Padding = new Padding(10);
            Text = "EditStringValue";
            panel3.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        internal System.Windows.Forms.Label lblValue;
        private C4GeneralGui.GuiElements.ValidatingTextBox vtxtValue;
        private Panel panel3;
        internal Button cmdOk;
        private Panel panel4;
        internal Button cmdCancel;
    }
}