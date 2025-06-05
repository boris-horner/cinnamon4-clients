namespace CDCplusLib.Common.GUI
{
    partial class ResultListDisplayLight
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
            lvwNodeList = new ListView();
            SuspendLayout();
            // 
            // lvwNodeList
            // 
            lvwNodeList.Dock = DockStyle.Fill;
            lvwNodeList.FullRowSelect = true;
            lvwNodeList.Location = new Point(0, 0);
            lvwNodeList.Margin = new Padding(4, 3, 4, 3);
            lvwNodeList.Name = "lvwNodeList";
            lvwNodeList.Size = new Size(1069, 652);
            lvwNodeList.TabIndex = 4;
            lvwNodeList.UseCompatibleStateImageBehavior = false;
            lvwNodeList.View = View.Details;
            lvwNodeList.SelectedIndexChanged += lvwNodeList_SelectedIndexChanged;
            // 
            // ResultListDisplayLight
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(lvwNodeList);
            Margin = new Padding(4, 3, 4, 3);
            Name = "ResultListDisplayLight";
            Size = new Size(1069, 652);
            ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.ListView lvwNodeList;
    }
}
