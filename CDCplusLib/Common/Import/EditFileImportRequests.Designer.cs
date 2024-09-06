
namespace CDCplusLib.Common.Import
{
    partial class EditFileImportRequests
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditFileImportRequests));
            this.panel1 = new System.Windows.Forms.Panel();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.cmdImport = new System.Windows.Forms.Button();
            this.lvwFirs = new System.Windows.Forms.ListView();
            this.colFilename = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colObjName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colObjType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colFormat = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colLanguage = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colLcState = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colTargetFolder = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.imlIcons = new System.Windows.Forms.ImageList(this.components);
            this.cmdReload = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.cmdReload);
            this.panel1.Controls.Add(this.cmdCancel);
            this.panel1.Controls.Add(this.cmdImport);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 538);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1432, 37);
            this.panel1.TabIndex = 0;
            // 
            // cmdCancel
            // 
            this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdCancel.Location = new System.Drawing.Point(96, 6);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(75, 23);
            this.cmdCancel.TabIndex = 1;
            this.cmdCancel.Text = "Cancel";
            this.cmdCancel.UseVisualStyleBackColor = true;
            this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
            // 
            // cmdImport
            // 
            this.cmdImport.Location = new System.Drawing.Point(15, 6);
            this.cmdImport.Name = "cmdImport";
            this.cmdImport.Size = new System.Drawing.Size(75, 23);
            this.cmdImport.TabIndex = 0;
            this.cmdImport.Text = "Import";
            this.cmdImport.UseVisualStyleBackColor = true;
            this.cmdImport.Click += new System.EventHandler(this.cmdImport_Click);
            // 
            // lvwFirs
            // 
            this.lvwFirs.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colFilename,
            this.colObjName,
            this.colObjType,
            this.colFormat,
            this.colLanguage,
            this.colLcState,
            this.colTargetFolder});
            this.lvwFirs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvwFirs.FullRowSelect = true;
            this.lvwFirs.Location = new System.Drawing.Point(0, 0);
            this.lvwFirs.Name = "lvwFirs";
            this.lvwFirs.ShowItemToolTips = true;
            this.lvwFirs.Size = new System.Drawing.Size(1432, 538);
            this.lvwFirs.SmallImageList = this.imlIcons;
            this.lvwFirs.TabIndex = 1;
            this.lvwFirs.UseCompatibleStateImageBehavior = false;
            this.lvwFirs.View = System.Windows.Forms.View.Details;
            this.lvwFirs.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lvwFirs_MouseClick);
            // 
            // colFilename
            // 
            this.colFilename.Text = "Filename";
            this.colFilename.Width = 469;
            // 
            // colObjName
            // 
            this.colObjName.Text = "Object name";
            this.colObjName.Width = 171;
            // 
            // colObjType
            // 
            this.colObjType.Text = "Object type";
            this.colObjType.Width = 84;
            // 
            // colFormat
            // 
            this.colFormat.Text = "Format";
            this.colFormat.Width = 91;
            // 
            // colLanguage
            // 
            this.colLanguage.Text = "Language";
            this.colLanguage.Width = 83;
            // 
            // colLcState
            // 
            this.colLcState.Text = "Lifecycle state";
            this.colLcState.Width = 97;
            // 
            // colTargetFolder
            // 
            this.colTargetFolder.Text = "Target folder";
            this.colTargetFolder.Width = 420;
            // 
            // imlIcons
            // 
            this.imlIcons.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imlIcons.ImageStream")));
            this.imlIcons.TransparentColor = System.Drawing.Color.Transparent;
            this.imlIcons.Images.SetKeyName(0, "OK");
            this.imlIcons.Images.SetKeyName(1, "Failure");
            this.imlIcons.Images.SetKeyName(2, "UnknownFormat");
            this.imlIcons.Images.SetKeyName(3, "ReferencesNotFound");
            // 
            // cmdReload
            // 
            this.cmdReload.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdReload.Location = new System.Drawing.Point(177, 6);
            this.cmdReload.Name = "cmdReload";
            this.cmdReload.Size = new System.Drawing.Size(75, 23);
            this.cmdReload.TabIndex = 2;
            this.cmdReload.Text = "Reload";
            this.cmdReload.UseVisualStyleBackColor = true;
            this.cmdReload.Click += new System.EventHandler(this.cmdReload_Click);
            // 
            // EditFileImportRequests
            // 
            this.AcceptButton = this.cmdImport;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cmdCancel;
            this.ClientSize = new System.Drawing.Size(1432, 575);
            this.Controls.Add(this.lvwFirs);
            this.Controls.Add(this.panel1);
            this.Name = "EditFileImportRequests";
            this.Text = "Edit file import requests";
            this.Shown += new System.EventHandler(this.EditFileImportRequests_Shown);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ListView lvwFirs;
        private System.Windows.Forms.ColumnHeader colFilename;
        private System.Windows.Forms.ColumnHeader colFormat;
        private System.Windows.Forms.ColumnHeader colLanguage;
        private System.Windows.Forms.ColumnHeader colLcState;
        private System.Windows.Forms.ColumnHeader colObjType;
        private System.Windows.Forms.ColumnHeader colTargetFolder;
        private System.Windows.Forms.ImageList imlIcons;
        private System.Windows.Forms.Button cmdCancel;
        private System.Windows.Forms.Button cmdImport;
        private System.Windows.Forms.ColumnHeader colObjName;
        private System.Windows.Forms.Button cmdReload;
    }
}