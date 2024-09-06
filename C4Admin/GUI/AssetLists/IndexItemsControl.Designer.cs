
namespace C4Admin.GUI.AssetLists
{
    partial class IndexItemsControl
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
            lvw = new System.Windows.Forms.ListView();
            colId = new System.Windows.Forms.ColumnHeader();
            colName = new System.Windows.Forms.ColumnHeader();
            colFieldName = new System.Windows.Forms.ColumnHeader();
            colSearchString = new System.Windows.Forms.ColumnHeader();
            colSearchCondition = new System.Windows.Forms.ColumnHeader();
            colIndexTypeName = new System.Windows.Forms.ColumnHeader();
            colMultipleResults = new System.Windows.Forms.ColumnHeader();
            colStoreField = new System.Windows.Forms.ColumnHeader();
            SuspendLayout();
            // 
            // lvw
            // 
            lvw.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] { colId, colName, colFieldName, colSearchString, colSearchCondition, colIndexTypeName, colMultipleResults, colStoreField });
            lvw.Dock = System.Windows.Forms.DockStyle.Fill;
            lvw.FullRowSelect = true;
            lvw.Location = new System.Drawing.Point(0, 0);
            lvw.Name = "lvw";
            lvw.Size = new System.Drawing.Size(1270, 744);
            lvw.Sorting = System.Windows.Forms.SortOrder.Ascending;
            lvw.TabIndex = 0;
            lvw.UseCompatibleStateImageBehavior = false;
            lvw.View = System.Windows.Forms.View.Details;
            lvw.MouseUp += lvw_MouseUp;
            // 
            // colId
            // 
            colId.Text = "Id";
            colId.Width = 75;
            // 
            // colName
            // 
            colName.Text = "Name";
            colName.Width = 250;
            // 
            // colFieldName
            // 
            colFieldName.Text = "Field name";
            colFieldName.Width = 250;
            // 
            // colSearchString
            // 
            colSearchString.Text = "XPath search string";
            colSearchString.Width = 250;
            // 
            // colSearchCondition
            // 
            colSearchCondition.Text = "XPath condition";
            colSearchCondition.Width = 120;
            // 
            // colIndexTypeName
            // 
            colIndexTypeName.Text = "Index type name";
            colIndexTypeName.Width = 120;
            // 
            // colMultipleResults
            // 
            colMultipleResults.Text = "Multiple";
            colMultipleResults.Width = 75;
            // 
            // colStoreField
            // 
            colStoreField.Text = "Store";
            colStoreField.Width = 75;
            // 
            // IndexItemsControl
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            Controls.Add(lvw);
            Name = "IndexItemsControl";
            Size = new System.Drawing.Size(1270, 744);
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.ListView lvw;
        private System.Windows.Forms.ColumnHeader colId;
        private System.Windows.Forms.ColumnHeader colName;
        private System.Windows.Forms.ColumnHeader colFieldName;
        private System.Windows.Forms.ColumnHeader colSearchString;
        private System.Windows.Forms.ColumnHeader colSearchCondition;
        private System.Windows.Forms.ColumnHeader colIndexTypeName;
        private System.Windows.Forms.ColumnHeader colMultipleResults;
        private System.Windows.Forms.ColumnHeader colStoreField;
    }
}
