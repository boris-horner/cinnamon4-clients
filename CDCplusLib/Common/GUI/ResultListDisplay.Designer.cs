namespace CDCplusLib.Common.GUI
{
    partial class ResultListDisplay
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
            this.lvwNodeList = new System.Windows.Forms.ListView();
            this.SuspendLayout();
            // 
            // lvwNodeList
            // 
            this.lvwNodeList.CheckBoxes = true;
            this.lvwNodeList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvwNodeList.FullRowSelect = true;
            this.lvwNodeList.HideSelection = false;
            this.lvwNodeList.Location = new System.Drawing.Point(0, 0);
            this.lvwNodeList.Name = "lvwNodeList";
            this.lvwNodeList.Size = new System.Drawing.Size(916, 565);
            this.lvwNodeList.TabIndex = 4;
            this.lvwNodeList.UseCompatibleStateImageBehavior = false;
            this.lvwNodeList.View = System.Windows.Forms.View.Details;
            this.lvwNodeList.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.lvwNodeList_ItemCheck);
            this.lvwNodeList.SelectedIndexChanged += new System.EventHandler(this.lvwNodeList_SelectedIndexChanged);
            this.lvwNodeList.DoubleClick += new System.EventHandler(this.lvwNodeList_DoubleClick);
            this.lvwNodeList.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lvwNodeList_KeyDown);
            this.lvwNodeList.MouseUp += new System.Windows.Forms.MouseEventHandler(this.lvwNodeList_MouseUp);
            // 
            // ResultListDisplay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lvwNodeList);
            this.Name = "ResultListDisplay";
            this.Size = new System.Drawing.Size(916, 565);
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.ListView lvwNodeList;
    }
}
