namespace CDCplusLib.Common
{
    partial class ListViewColumnEditor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ListViewColumnEditor));
            this.panel1 = new System.Windows.Forms.Panel();
            this.lvwHeader = new System.Windows.Forms.ListView();
            this.colDemo = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lblColumnWidthPreview = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lbAvailable = new System.Windows.Forms.ListBox();
            this.lblAvailableColumns = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.cmdRemoveColumn = new System.Windows.Forms.Button();
            this.cmdAddColumn = new System.Windows.Forms.Button();
            this.panel4 = new System.Windows.Forms.Panel();
            this.lbShown = new System.Windows.Forms.ListBox();
            this.lblShownColumns = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.cmdColumnDown = new System.Windows.Forms.Button();
            this.cmdColumnUp = new System.Windows.Forms.Button();
            this.panel6 = new System.Windows.Forms.Panel();
            this.chkReverse = new System.Windows.Forms.CheckBox();
            this.chkSortThisColumn = new System.Windows.Forms.CheckBox();
            this.udWidth = new System.Windows.Forms.NumericUpDown();
            this.lblWidth = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.udWidth)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lvwHeader);
            this.panel1.Controls.Add(this.lblColumnWidthPreview);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 324);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(0, 10, 0, 0);
            this.panel1.Size = new System.Drawing.Size(830, 49);
            this.panel1.TabIndex = 0;
            // 
            // lvwHeader
            // 
            this.lvwHeader.BackColor = System.Drawing.SystemColors.Window;
            this.lvwHeader.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colDemo});
            this.lvwHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvwHeader.Location = new System.Drawing.Point(0, 23);
            this.lvwHeader.Name = "lvwHeader";
            this.lvwHeader.Size = new System.Drawing.Size(830, 26);
            this.lvwHeader.TabIndex = 1;
            this.lvwHeader.UseCompatibleStateImageBehavior = false;
            this.lvwHeader.View = System.Windows.Forms.View.Details;
            this.lvwHeader.ColumnWidthChanged += new System.Windows.Forms.ColumnWidthChangedEventHandler(this.lvwHeader_ColumnWidthChanged);
            // 
            // colDemo
            // 
            this.colDemo.Text = "Test";
            this.colDemo.Width = 120;
            // 
            // lblColumnWidthPreview
            // 
            this.lblColumnWidthPreview.AutoSize = true;
            this.lblColumnWidthPreview.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblColumnWidthPreview.Location = new System.Drawing.Point(0, 10);
            this.lblColumnWidthPreview.Name = "lblColumnWidthPreview";
            this.lblColumnWidthPreview.Size = new System.Drawing.Size(110, 13);
            this.lblColumnWidthPreview.TabIndex = 0;
            this.lblColumnWidthPreview.Text = "Column width preview";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 5;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33334F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.Controls.Add(this.panel2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel3, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel4, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel5, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel6, 4, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(830, 324);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.lbAvailable);
            this.panel2.Controls.Add(this.lblAvailableColumns);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(3, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(237, 318);
            this.panel2.TabIndex = 0;
            // 
            // lbAvailable
            // 
            this.lbAvailable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbAvailable.FormattingEnabled = true;
            this.lbAvailable.Location = new System.Drawing.Point(0, 13);
            this.lbAvailable.Name = "lbAvailable";
            this.lbAvailable.Size = new System.Drawing.Size(237, 305);
            this.lbAvailable.Sorted = true;
            this.lbAvailable.TabIndex = 1;
            this.lbAvailable.SelectedIndexChanged += new System.EventHandler(this.lbAvailable_SelectedIndexChanged);
            // 
            // lblAvailableColumns
            // 
            this.lblAvailableColumns.AutoSize = true;
            this.lblAvailableColumns.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblAvailableColumns.Location = new System.Drawing.Point(0, 0);
            this.lblAvailableColumns.Name = "lblAvailableColumns";
            this.lblAvailableColumns.Size = new System.Drawing.Size(92, 13);
            this.lblAvailableColumns.TabIndex = 0;
            this.lblAvailableColumns.Text = "Available columns";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.cmdRemoveColumn);
            this.panel3.Controls.Add(this.cmdAddColumn);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(246, 3);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(44, 318);
            this.panel3.TabIndex = 1;
            // 
            // cmdRemoveColumn
            // 
            this.cmdRemoveColumn.Image = ((System.Drawing.Image)(resources.GetObject("cmdRemoveColumn.Image")));
            this.cmdRemoveColumn.Location = new System.Drawing.Point(2, 67);
            this.cmdRemoveColumn.Margin = new System.Windows.Forms.Padding(0);
            this.cmdRemoveColumn.Name = "cmdRemoveColumn";
            this.cmdRemoveColumn.Size = new System.Drawing.Size(40, 40);
            this.cmdRemoveColumn.TabIndex = 7;
            this.cmdRemoveColumn.UseVisualStyleBackColor = true;
            this.cmdRemoveColumn.Click += new System.EventHandler(this.cmdRemoveColumn_Click);
            // 
            // cmdAddColumn
            // 
            this.cmdAddColumn.Image = ((System.Drawing.Image)(resources.GetObject("cmdAddColumn.Image")));
            this.cmdAddColumn.Location = new System.Drawing.Point(2, 15);
            this.cmdAddColumn.Margin = new System.Windows.Forms.Padding(0);
            this.cmdAddColumn.Name = "cmdAddColumn";
            this.cmdAddColumn.Size = new System.Drawing.Size(40, 40);
            this.cmdAddColumn.TabIndex = 6;
            this.cmdAddColumn.UseVisualStyleBackColor = true;
            this.cmdAddColumn.Click += new System.EventHandler(this.cmdAddColumn_Click);
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.lbShown);
            this.panel4.Controls.Add(this.lblShownColumns);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(296, 3);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(237, 318);
            this.panel4.TabIndex = 2;
            // 
            // lbShown
            // 
            this.lbShown.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbShown.FormattingEnabled = true;
            this.lbShown.Location = new System.Drawing.Point(0, 13);
            this.lbShown.Name = "lbShown";
            this.lbShown.Size = new System.Drawing.Size(237, 305);
            this.lbShown.TabIndex = 1;
            this.lbShown.SelectedIndexChanged += new System.EventHandler(this.lbShown_SelectedIndexChanged);
            // 
            // lblShownColumns
            // 
            this.lblShownColumns.AutoSize = true;
            this.lblShownColumns.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblShownColumns.Location = new System.Drawing.Point(0, 0);
            this.lblShownColumns.Name = "lblShownColumns";
            this.lblShownColumns.Size = new System.Drawing.Size(82, 13);
            this.lblShownColumns.TabIndex = 0;
            this.lblShownColumns.Text = "Shown columns";
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.cmdColumnDown);
            this.panel5.Controls.Add(this.cmdColumnUp);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel5.Location = new System.Drawing.Point(539, 3);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(44, 318);
            this.panel5.TabIndex = 3;
            // 
            // cmdColumnDown
            // 
            this.cmdColumnDown.Image = ((System.Drawing.Image)(resources.GetObject("cmdColumnDown.Image")));
            this.cmdColumnDown.Location = new System.Drawing.Point(2, 67);
            this.cmdColumnDown.Margin = new System.Windows.Forms.Padding(0);
            this.cmdColumnDown.Name = "cmdColumnDown";
            this.cmdColumnDown.Size = new System.Drawing.Size(40, 40);
            this.cmdColumnDown.TabIndex = 9;
            this.cmdColumnDown.UseVisualStyleBackColor = true;
            this.cmdColumnDown.Click += new System.EventHandler(this.cmdColumnDown_Click);
            // 
            // cmdColumnUp
            // 
            this.cmdColumnUp.Image = ((System.Drawing.Image)(resources.GetObject("cmdColumnUp.Image")));
            this.cmdColumnUp.Location = new System.Drawing.Point(2, 15);
            this.cmdColumnUp.Margin = new System.Windows.Forms.Padding(0);
            this.cmdColumnUp.Name = "cmdColumnUp";
            this.cmdColumnUp.Size = new System.Drawing.Size(40, 40);
            this.cmdColumnUp.TabIndex = 8;
            this.cmdColumnUp.UseVisualStyleBackColor = true;
            this.cmdColumnUp.Click += new System.EventHandler(this.cmdColumnUp_Click);
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.chkReverse);
            this.panel6.Controls.Add(this.chkSortThisColumn);
            this.panel6.Controls.Add(this.udWidth);
            this.panel6.Controls.Add(this.lblWidth);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel6.Location = new System.Drawing.Point(589, 3);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(238, 318);
            this.panel6.TabIndex = 4;
            // 
            // chkReverse
            // 
            this.chkReverse.AutoSize = true;
            this.chkReverse.Dock = System.Windows.Forms.DockStyle.Top;
            this.chkReverse.Location = new System.Drawing.Point(0, 60);
            this.chkReverse.Name = "chkReverse";
            this.chkReverse.Padding = new System.Windows.Forms.Padding(0, 10, 0, 0);
            this.chkReverse.Size = new System.Drawing.Size(238, 27);
            this.chkReverse.TabIndex = 3;
            this.chkReverse.Text = "Reverse sort order";
            this.chkReverse.UseVisualStyleBackColor = true;
            this.chkReverse.CheckedChanged += new System.EventHandler(this.chkReverse_CheckedChanged);
            // 
            // chkSortThisColumn
            // 
            this.chkSortThisColumn.AutoSize = true;
            this.chkSortThisColumn.Dock = System.Windows.Forms.DockStyle.Top;
            this.chkSortThisColumn.Location = new System.Drawing.Point(0, 33);
            this.chkSortThisColumn.Name = "chkSortThisColumn";
            this.chkSortThisColumn.Padding = new System.Windows.Forms.Padding(0, 10, 0, 0);
            this.chkSortThisColumn.Size = new System.Drawing.Size(238, 27);
            this.chkSortThisColumn.TabIndex = 2;
            this.chkSortThisColumn.Text = "Sort by this column";
            this.chkSortThisColumn.UseVisualStyleBackColor = true;
            this.chkSortThisColumn.CheckedChanged += new System.EventHandler(this.chkSortThisColumn_CheckedChanged);
            // 
            // udWidth
            // 
            this.udWidth.Dock = System.Windows.Forms.DockStyle.Top;
            this.udWidth.Location = new System.Drawing.Point(0, 13);
            this.udWidth.Name = "udWidth";
            this.udWidth.Size = new System.Drawing.Size(238, 20);
            this.udWidth.TabIndex = 1;
            this.udWidth.ValueChanged += new System.EventHandler(this.udWidth_ValueChanged);
            // 
            // lblWidth
            // 
            this.lblWidth.AutoSize = true;
            this.lblWidth.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblWidth.Location = new System.Drawing.Point(0, 0);
            this.lblWidth.Name = "lblWidth";
            this.lblWidth.Size = new System.Drawing.Size(35, 13);
            this.lblWidth.TabIndex = 0;
            this.lblWidth.Text = "Width";
            // 
            // ListViewColumnEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.panel1);
            this.Name = "ListViewColumnEditor";
            this.Size = new System.Drawing.Size(830, 373);
            this.VisibleChanged += new System.EventHandler(this.ListViewColumnEditor_VisibleChanged);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.udWidth)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ListView lvwHeader;
        private System.Windows.Forms.ColumnHeader colDemo;
        private System.Windows.Forms.Label lblColumnWidthPreview;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.ListBox lbAvailable;
        private System.Windows.Forms.Label lblAvailableColumns;
        private System.Windows.Forms.Label lblShownColumns;
        private System.Windows.Forms.Label lblWidth;
        private System.Windows.Forms.ListBox lbShown;
        private System.Windows.Forms.NumericUpDown udWidth;
        internal System.Windows.Forms.Button cmdRemoveColumn;
        internal System.Windows.Forms.Button cmdAddColumn;
        internal System.Windows.Forms.Button cmdColumnDown;
        internal System.Windows.Forms.Button cmdColumnUp;
        private System.Windows.Forms.CheckBox chkReverse;
        private System.Windows.Forms.CheckBox chkSortThisColumn;
    }
}
