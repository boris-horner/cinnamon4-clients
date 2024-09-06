
namespace CDCplusLib.Common.GUI
{
    partial class ContextTabControlContainer
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
            this.tabContext = new System.Windows.Forms.TabControl();
            this.SuspendLayout();
            // 
            // tabContext
            // 
            this.tabContext.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabContext.Location = new System.Drawing.Point(0, 0);
            this.tabContext.Margin = new System.Windows.Forms.Padding(4);
            this.tabContext.Name = "tabContext";
            this.tabContext.SelectedIndex = 0;
            this.tabContext.Size = new System.Drawing.Size(1324, 792);
            this.tabContext.TabIndex = 2;
            this.tabContext.SelectedIndexChanged += new System.EventHandler(this.tabContext_SelectedIndexChanged);
            // 
            // ContextTabControlContainer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabContext);
            this.Name = "ContextTabControlContainer";
            this.Size = new System.Drawing.Size(1324, 792);
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.TabControl tabContext;
    }
}
