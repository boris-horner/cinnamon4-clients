
namespace C4Admin.GUI
{
    partial class RelationTypesControl
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
            this.lvw = new System.Windows.Forms.ListView();
            this.colId = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colLeftObjectProtected = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colRightObjectProtected = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colCloneOnLeftCopy = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colCloneOnRightCopy = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colCloneOnLeftVersion = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colCloneOnRightVersion = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // lvw
            // 
            this.lvw.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colId,
            this.colName,
            this.colLeftObjectProtected,
            this.colRightObjectProtected,
            this.colCloneOnLeftCopy,
            this.colCloneOnRightCopy,
            this.colCloneOnLeftVersion,
            this.colCloneOnRightVersion});
            this.lvw.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvw.FullRowSelect = true;
            this.lvw.Location = new System.Drawing.Point(0, 0);
            this.lvw.Name = "lvw";
            this.lvw.Size = new System.Drawing.Size(1451, 794);
            this.lvw.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.lvw.TabIndex = 0;
            this.lvw.UseCompatibleStateImageBehavior = false;
            this.lvw.View = System.Windows.Forms.View.Details;
            this.lvw.MouseUp += new System.Windows.Forms.MouseEventHandler(this.lvw_MouseUp);
            // 
            // colId
            // 
            this.colId.Text = "Id";
            this.colId.Width = 75;
            // 
            // colName
            // 
            this.colName.Text = "Name";
            this.colName.Width = 250;
            // 
            // colLeftObjectProtected
            // 
            this.colLeftObjectProtected.Text = "LOP";
            // 
            // colRightObjectProtected
            // 
            this.colRightObjectProtected.Text = "ROP";
            // 
            // colCloneOnLeftCopy
            // 
            this.colCloneOnLeftCopy.Text = "CLC";
            // 
            // colCloneOnRightCopy
            // 
            this.colCloneOnRightCopy.Text = "CRC";
            // 
            // colCloneOnLeftVersion
            // 
            this.colCloneOnLeftVersion.Text = "CLV";
            // 
            // colCloneOnRightVersion
            // 
            this.colCloneOnRightVersion.Text = "CRV";
            // 
            // RelationTypesControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lvw);
            this.Name = "RelationTypesControl";
            this.Size = new System.Drawing.Size(1451, 794);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView lvw;
        private System.Windows.Forms.ColumnHeader colId;
        private System.Windows.Forms.ColumnHeader colName;
        private System.Windows.Forms.ColumnHeader colLeftObjectProtected;
        private System.Windows.Forms.ColumnHeader colRightObjectProtected;
        private System.Windows.Forms.ColumnHeader colCloneOnLeftCopy;
        private System.Windows.Forms.ColumnHeader colCloneOnRightCopy;
        private System.Windows.Forms.ColumnHeader colCloneOnLeftVersion;
        private System.Windows.Forms.ColumnHeader colCloneOnRightVersion;
    }
}
