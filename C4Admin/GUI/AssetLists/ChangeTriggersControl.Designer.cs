
namespace C4Admin.GUI
{
    partial class ChangeTriggersControl
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
            lvw = new ListView();
            colId = new ColumnHeader();
            colName = new ColumnHeader();
            colTriggerType = new ColumnHeader();
            colActive = new ColumnHeader();
            colPreTrigger = new ColumnHeader();
            colPostTrigger = new ColumnHeader();
            colCopyFileContent = new ColumnHeader();
            colRanking = new ColumnHeader();
            colController = new ColumnHeader();
            colAction = new ColumnHeader();
            colPostCommitTrigger = new ColumnHeader();
            SuspendLayout();
            // 
            // lvw
            // 
            lvw.Columns.AddRange(new ColumnHeader[] { colId, colName, colTriggerType, colActive, colPreTrigger, colPostTrigger, colPostCommitTrigger, colCopyFileContent, colRanking, colController, colAction });
            lvw.Dock = DockStyle.Fill;
            lvw.FullRowSelect = true;
            lvw.Location = new Point(0, 0);
            lvw.Name = "lvw";
            lvw.Size = new Size(1270, 744);
            lvw.Sorting = SortOrder.Ascending;
            lvw.TabIndex = 0;
            lvw.UseCompatibleStateImageBehavior = false;
            lvw.View = View.Details;
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
            // colTriggerType
            // 
            colTriggerType.Text = "Trigger type";
            colTriggerType.Width = 120;
            // 
            // colActive
            // 
            colActive.Text = "Active";
            // 
            // colPreTrigger
            // 
            colPreTrigger.Text = "Pre";
            // 
            // colPostTrigger
            // 
            colPostTrigger.Text = "Post";
            // 
            // colCopyFileContent
            // 
            colCopyFileContent.Text = "Copy";
            // 
            // colRanking
            // 
            colRanking.Text = "Ranking";
            // 
            // colController
            // 
            colController.Text = "Controller";
            colController.Width = 120;
            // 
            // colAction
            // 
            colAction.Text = "Action";
            colAction.Width = 120;
            // 
            // colPostCommitTrigger
            // 
            colPostCommitTrigger.Text = "Post-C";
            // 
            // ChangeTriggersControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(lvw);
            Name = "ChangeTriggersControl";
            Size = new Size(1270, 744);
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.ListView lvw;
        private System.Windows.Forms.ColumnHeader colId;
        private System.Windows.Forms.ColumnHeader colName;
        private System.Windows.Forms.ColumnHeader colTriggerType;
        private System.Windows.Forms.ColumnHeader colActive;
        private System.Windows.Forms.ColumnHeader colPreTrigger;
        private System.Windows.Forms.ColumnHeader colPostTrigger;
        private System.Windows.Forms.ColumnHeader colCopyFileContent;
        private System.Windows.Forms.ColumnHeader colRanking;
        private System.Windows.Forms.ColumnHeader colController;
        private System.Windows.Forms.ColumnHeader colAction;
        private ColumnHeader colPostCommitTrigger;
    }
}
