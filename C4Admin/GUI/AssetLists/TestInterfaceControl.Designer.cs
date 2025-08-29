
namespace C4Admin.GUI.AssetLists
{
    partial class TestInterfaceControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            tableLayoutPanel1 = new TableLayoutPanel();
            panel1 = new Panel();
            cmdSendCommand = new Button();
            txtCommandPath = new TextBox();
            label3 = new Label();
            panel2 = new Panel();
            txtBodyText = new TextBox();
            label1 = new Label();
            panel3 = new Panel();
            txtResult = new TextBox();
            label2 = new Label();
            tableLayoutPanel1.SuspendLayout();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            panel3.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 3;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 219F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.Controls.Add(panel1, 0, 0);
            tableLayoutPanel1.Controls.Add(panel2, 1, 0);
            tableLayoutPanel1.Controls.Add(panel3, 2, 0);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 1;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Size = new Size(1270, 744);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // panel1
            // 
            panel1.Controls.Add(cmdSendCommand);
            panel1.Controls.Add(txtCommandPath);
            panel1.Controls.Add(label3);
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(3, 3);
            panel1.Name = "panel1";
            panel1.Padding = new Padding(7, 8, 7, 8);
            panel1.Size = new Size(213, 738);
            panel1.TabIndex = 0;
            // 
            // cmdSendCommand
            // 
            cmdSendCommand.Location = new Point(7, 67);
            cmdSendCommand.Name = "cmdSendCommand";
            cmdSendCommand.Size = new Size(106, 30);
            cmdSendCommand.TabIndex = 2;
            cmdSendCommand.Text = "Send command";
            cmdSendCommand.UseVisualStyleBackColor = true;
            cmdSendCommand.Click += cmdSendCommand_Click;
            // 
            // txtCommandPath
            // 
            txtCommandPath.Dock = DockStyle.Top;
            txtCommandPath.Location = new Point(7, 23);
            txtCommandPath.Name = "txtCommandPath";
            txtCommandPath.Size = new Size(199, 23);
            txtCommandPath.TabIndex = 1;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Dock = DockStyle.Top;
            label3.Location = new Point(7, 8);
            label3.Name = "label3";
            label3.Size = new Size(91, 15);
            label3.TabIndex = 0;
            label3.Text = "Command path";
            // 
            // panel2
            // 
            panel2.Controls.Add(txtBodyText);
            panel2.Controls.Add(label1);
            panel2.Dock = DockStyle.Fill;
            panel2.Location = new Point(222, 3);
            panel2.Name = "panel2";
            panel2.Padding = new Padding(7, 8, 7, 8);
            panel2.Size = new Size(519, 738);
            panel2.TabIndex = 1;
            // 
            // txtBodyText
            // 
            txtBodyText.Dock = DockStyle.Fill;
            txtBodyText.Location = new Point(7, 23);
            txtBodyText.MaxLength = 16000000;
            txtBodyText.Multiline = true;
            txtBodyText.Name = "txtBodyText";
            txtBodyText.ScrollBars = ScrollBars.Both;
            txtBodyText.Size = new Size(505, 707);
            txtBodyText.TabIndex = 2;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Dock = DockStyle.Top;
            label1.Location = new Point(7, 8);
            label1.Name = "label1";
            label1.Size = new Size(56, 15);
            label1.TabIndex = 0;
            label1.Text = "Body text";
            // 
            // panel3
            // 
            panel3.Controls.Add(txtResult);
            panel3.Controls.Add(label2);
            panel3.Dock = DockStyle.Fill;
            panel3.Location = new Point(747, 3);
            panel3.Name = "panel3";
            panel3.Padding = new Padding(7, 8, 7, 8);
            panel3.Size = new Size(520, 738);
            panel3.TabIndex = 2;
            // 
            // txtResult
            // 
            txtResult.Dock = DockStyle.Fill;
            txtResult.Location = new Point(7, 23);
            txtResult.MaxLength = 16000000;
            txtResult.Multiline = true;
            txtResult.Name = "txtResult";
            txtResult.ScrollBars = ScrollBars.Both;
            txtResult.Size = new Size(506, 707);
            txtResult.TabIndex = 2;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Dock = DockStyle.Top;
            label2.Location = new Point(7, 8);
            label2.Name = "label2";
            label2.Size = new Size(57, 15);
            label2.TabIndex = 0;
            label2.Text = "Response";
            // 
            // TestInterfaceControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(tableLayoutPanel1);
            Name = "TestInterfaceControl";
            Size = new Size(1270, 744);
            tableLayoutPanel1.ResumeLayout(false);
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox txtBodyText;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.TextBox txtResult;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button cmdSendCommand;
        private System.Windows.Forms.TextBox txtCommandPath;
        private System.Windows.Forms.Label label3;
    }
}
