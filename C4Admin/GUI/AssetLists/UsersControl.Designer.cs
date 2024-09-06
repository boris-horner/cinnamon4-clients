
namespace C4Admin.GUI.AssetLists
{
    partial class UsersControl
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
            colFullName = new System.Windows.Forms.ColumnHeader();
            colUiLanguage = new System.Windows.Forms.ColumnHeader();
            colLoginType = new System.Windows.Forms.ColumnHeader();
            colEmail = new System.Windows.Forms.ColumnHeader();
            colActive = new System.Windows.Forms.ColumnHeader();
            colActivateTriggers = new System.Windows.Forms.ColumnHeader();
            colLocked = new System.Windows.Forms.ColumnHeader();
            colChangeTracking = new System.Windows.Forms.ColumnHeader();
            colPasswordExpired = new System.Windows.Forms.ColumnHeader();
            SuspendLayout();
            // 
            // lvw
            // 
            lvw.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] { colId, colName, colFullName, colUiLanguage, colLoginType, colEmail, colActive, colActivateTriggers, colChangeTracking, colLocked, colPasswordExpired });
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
            colName.Width = 120;
            // 
            // colFullName
            // 
            colFullName.Text = "Full name";
            colFullName.Width = 250;
            // 
            // colUiLanguage
            // 
            colUiLanguage.Text = "UI language";
            colUiLanguage.Width = 120;
            // 
            // colLoginType
            // 
            colLoginType.Text = "Login type";
            colLoginType.Width = 120;
            // 
            // colEmail
            // 
            colEmail.Text = "Email";
            colEmail.Width = 120;
            // 
            // colActive
            // 
            colActive.Text = "Active";
            // 
            // colActivateTriggers
            // 
            colActivateTriggers.Text = "Triggers";
            // 
            // colLocked
            // 
            colLocked.Text = "Locked";
            // 
            // colChangeTracking
            // 
            colChangeTracking.Text = "Change";
            // 
            // colPasswordExpired
            // 
            colPasswordExpired.Text = "Pwd";
            // 
            // UsersControl
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            Controls.Add(lvw);
            Name = "UsersControl";
            Size = new System.Drawing.Size(1270, 744);
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.ListView lvw;
        private System.Windows.Forms.ColumnHeader colId;
        private System.Windows.Forms.ColumnHeader colName;
        private System.Windows.Forms.ColumnHeader colFullName;
        private System.Windows.Forms.ColumnHeader colUiLanguage;
        private System.Windows.Forms.ColumnHeader colLoginType;
        private System.Windows.Forms.ColumnHeader colEmail;
        private System.Windows.Forms.ColumnHeader colActive;
        private System.Windows.Forms.ColumnHeader colLocked;
        private System.Windows.Forms.ColumnHeader colChangeTracking;
        private System.Windows.Forms.ColumnHeader colActivateTriggers;
        private System.Windows.Forms.ColumnHeader colPasswordExpired;
    }
}
