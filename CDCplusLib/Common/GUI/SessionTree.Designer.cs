
namespace CDCplusLib.Common.GUI
{
    partial class SessionTree
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
            tvwSession = new System.Windows.Forms.TreeView();
            SuspendLayout();
            // 
            // tvwSession
            // 
            tvwSession.Dock = System.Windows.Forms.DockStyle.Fill;
            tvwSession.HideSelection = false;
            tvwSession.Location = new System.Drawing.Point(0, 0);
            tvwSession.Margin = new System.Windows.Forms.Padding(4);
            tvwSession.Name = "tvwSession";
            tvwSession.Size = new System.Drawing.Size(446, 1323);
            tvwSession.TabIndex = 2;
            tvwSession.AfterExpand += tvwSession_AfterExpand;
            tvwSession.AfterSelect += tvwSession_AfterSelect;
            tvwSession.KeyDown += tvwSession_KeyDown;
            tvwSession.MouseUp += tvwSession_MouseUp;
            // 
            // SessionTree
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            Controls.Add(tvwSession);
            Name = "SessionTree";
            Size = new System.Drawing.Size(446, 1323);
            ResumeLayout(false);
        }

        #endregion

        internal System.Windows.Forms.TreeView tvwSession;
    }
}
