
namespace C4Admin.GUI.AssetLists
{
	partial class FormatsControl
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
			colContentType = new System.Windows.Forms.ColumnHeader();
			colExtension = new System.Windows.Forms.ColumnHeader();
			colDefaultObjectType = new System.Windows.Forms.ColumnHeader();
			colIndexMode = new System.Windows.Forms.ColumnHeader();
			SuspendLayout();
			// 
			// lvw
			// 
			lvw.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] { colId, colName, colContentType, colExtension, colDefaultObjectType, colIndexMode });
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
			// colContentType
			// 
			colContentType.Text = "Content Type";
			colContentType.Width = 298;
			// 
			// colExtension
			// 
			colExtension.Text = "Extension";
			colExtension.Width = 96;
			// 
			// colDefaultObjectType
			// 
			colDefaultObjectType.Text = "Default object type";
			colDefaultObjectType.Width = 225;
			// 
			// colIndexMode
			// 
			colIndexMode.Text = "Index mode";
			colIndexMode.Width = 100;
			// 
			// FormatsControl
			// 
			AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			Controls.Add(lvw);
			Name = "FormatsControl";
			Size = new System.Drawing.Size(1270, 744);
			ResumeLayout(false);
		}

		#endregion

		private System.Windows.Forms.ListView lvw;
		private System.Windows.Forms.ColumnHeader colId;
		private System.Windows.Forms.ColumnHeader colName;
		private System.Windows.Forms.ColumnHeader colContentType;
		private System.Windows.Forms.ColumnHeader colExtension;
		private System.Windows.Forms.ColumnHeader colDefaultObjectType;
		private System.Windows.Forms.ColumnHeader colIndexMode;
	}
}
