
namespace CDCplusLib.TabControls
{
    partial class EditSummaryTabControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditSummaryTabControl));
            panel20 = new Panel();
            cmdSave = new Button();
            xtxtSummary = new C4GeneralGui.GuiElements.XmlTextBox();
            panel20.SuspendLayout();
            SuspendLayout();
            // 
            // panel20
            // 
            panel20.Controls.Add(cmdSave);
            panel20.Dock = DockStyle.Bottom;
            panel20.Location = new Point(0, 621);
            panel20.Margin = new Padding(2);
            panel20.Name = "panel20";
            panel20.Size = new Size(1049, 40);
            panel20.TabIndex = 1;
            // 
            // cmdSave
            // 
            cmdSave.Dock = DockStyle.Left;
            cmdSave.Image = (Image)resources.GetObject("cmdSave.Image");
            cmdSave.Location = new Point(0, 0);
            cmdSave.Margin = new Padding(2);
            cmdSave.Name = "cmdSave";
            cmdSave.Size = new Size(40, 40);
            cmdSave.TabIndex = 4;
            cmdSave.UseVisualStyleBackColor = true;
            cmdSave.Click += cmdSave_Click;
            // 
            // xtxtSummary
            // 
            xtxtSummary.Dock = DockStyle.Fill;
            xtxtSummary.Location = new Point(0, 0);
            xtxtSummary.Name = "xtxtSummary";
            xtxtSummary.Size = new Size(1049, 621);
            xtxtSummary.TabIndex = 2;
            xtxtSummary.Text = "";
            xtxtSummary.TextChanged += xtxtSummary_TextChanged;
            // 
            // EditSummaryTabControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(xtxtSummary);
            Controls.Add(panel20);
            Name = "EditSummaryTabControl";
            Size = new Size(1049, 661);
            panel20.ResumeLayout(false);
            ResumeLayout(false);

        }

        #endregion
        internal System.Windows.Forms.Button cmdSave;
        private System.Windows.Forms.ToolTip ttPermissions;
        private System.Windows.Forms.ImageList imlPermissions;
        private Panel panel20;
        private C4GeneralGui.GuiElements.XmlTextBox xtxtSummary;
    }
}
