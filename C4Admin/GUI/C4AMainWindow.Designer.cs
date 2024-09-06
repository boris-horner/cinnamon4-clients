
using C4Admin.GUI.AssetLists;
namespace C4Admin.GUI
{
    partial class C4AMainWindow
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
            tbAdmin = new TabControl();
            tpObjTypes = new TabPage();
            ucObjTypes = new ObjectTypesControl();
            tpFolderTypes = new TabPage();
            ucFolderTypes = new FolderTypesControl();
            tpFormats = new TabPage();
            ucFormats = new FormatsControl();
            tpRelationTypes = new TabPage();
            ucRelationTypes = new RelationTypesControl();
            tpMetasetTypes = new TabPage();
            ucMetasetTypes = new MetasetTypesControl();
            tpLanguages = new TabPage();
            ucLanguages = new LanguagesControl();
            tpUiLanguages = new TabPage();
            ucUiLanguages = new LanguagesControl();
            tpIndexItems = new TabPage();
            ucIndexItems = new IndexItemsControl();
            tpUsers = new TabPage();
            ucUsers = new UsersControl();
            tpGroups = new TabPage();
            ucGroups = new GroupsControl();
            tpAcls = new TabPage();
            ucAcls = new AclsControl();
            tpLifecycles = new TabPage();
            ucLifecycles = new LifecyclesControl();
            tpChangeTriggers = new TabPage();
            changeTriggersControl1 = new ChangeTriggersControl();
            tpConfigEntries = new TabPage();
            ucConfigEntries = new ConfigEntriesControl();
            tpTestInterface = new TabPage();
            ucTestInterface = new TestInterfaceControl();
            tbAdmin.SuspendLayout();
            tpObjTypes.SuspendLayout();
            tpFolderTypes.SuspendLayout();
            tpFormats.SuspendLayout();
            tpRelationTypes.SuspendLayout();
            tpMetasetTypes.SuspendLayout();
            tpLanguages.SuspendLayout();
            tpUiLanguages.SuspendLayout();
            tpIndexItems.SuspendLayout();
            tpUsers.SuspendLayout();
            tpGroups.SuspendLayout();
            tpAcls.SuspendLayout();
            tpLifecycles.SuspendLayout();
            tpChangeTriggers.SuspendLayout();
            tpConfigEntries.SuspendLayout();
            tpTestInterface.SuspendLayout();
            SuspendLayout();
            // 
            // tbAdmin
            // 
            tbAdmin.Controls.Add(tpObjTypes);
            tbAdmin.Controls.Add(tpFolderTypes);
            tbAdmin.Controls.Add(tpFormats);
            tbAdmin.Controls.Add(tpRelationTypes);
            tbAdmin.Controls.Add(tpMetasetTypes);
            tbAdmin.Controls.Add(tpLanguages);
            tbAdmin.Controls.Add(tpUiLanguages);
            tbAdmin.Controls.Add(tpIndexItems);
            tbAdmin.Controls.Add(tpUsers);
            tbAdmin.Controls.Add(tpGroups);
            tbAdmin.Controls.Add(tpAcls);
            tbAdmin.Controls.Add(tpLifecycles);
            tbAdmin.Controls.Add(tpChangeTriggers);
            tbAdmin.Controls.Add(tpConfigEntries);
            tbAdmin.Controls.Add(tpTestInterface);
            tbAdmin.Dock = DockStyle.Fill;
            tbAdmin.Location = new Point(0, 0);
            tbAdmin.Name = "tbAdmin";
            tbAdmin.SelectedIndex = 0;
            tbAdmin.Size = new Size(1840, 1137);
            tbAdmin.TabIndex = 20;
            tbAdmin.SelectedIndexChanged += tbAdmin_SelectedIndexChanged;
            // 
            // tpObjTypes
            // 
            tpObjTypes.Controls.Add(ucObjTypes);
            tpObjTypes.Location = new Point(4, 24);
            tpObjTypes.Name = "tpObjTypes";
            tpObjTypes.Padding = new Padding(3);
            tpObjTypes.Size = new Size(1832, 1109);
            tpObjTypes.TabIndex = 1;
            tpObjTypes.Text = "Object types";
            tpObjTypes.UseVisualStyleBackColor = true;
            // 
            // ucObjTypes
            // 
            ucObjTypes.Dock = DockStyle.Fill;
            ucObjTypes.Location = new Point(3, 3);
            ucObjTypes.Name = "ucObjTypes";
            ucObjTypes.Size = new Size(1826, 1103);
            ucObjTypes.TabIndex = 0;
            // 
            // tpFolderTypes
            // 
            tpFolderTypes.Controls.Add(ucFolderTypes);
            tpFolderTypes.Location = new Point(4, 24);
            tpFolderTypes.Name = "tpFolderTypes";
            tpFolderTypes.Size = new Size(1832, 1015);
            tpFolderTypes.TabIndex = 8;
            tpFolderTypes.Text = "Folder types";
            tpFolderTypes.UseVisualStyleBackColor = true;
            // 
            // ucFolderTypes
            // 
            ucFolderTypes.Dock = DockStyle.Fill;
            ucFolderTypes.Location = new Point(0, 0);
            ucFolderTypes.Name = "ucFolderTypes";
            ucFolderTypes.Size = new Size(1832, 1015);
            ucFolderTypes.TabIndex = 0;
            // 
            // tpFormats
            // 
            tpFormats.Controls.Add(ucFormats);
            tpFormats.Location = new Point(4, 24);
            tpFormats.Name = "tpFormats";
            tpFormats.Padding = new Padding(3);
            tpFormats.Size = new Size(1832, 1015);
            tpFormats.TabIndex = 2;
            tpFormats.Text = "Formats";
            tpFormats.UseVisualStyleBackColor = true;
            // 
            // ucFormats
            // 
            ucFormats.Dock = DockStyle.Fill;
            ucFormats.Location = new Point(3, 3);
            ucFormats.Name = "ucFormats";
            ucFormats.Size = new Size(1826, 1009);
            ucFormats.TabIndex = 0;
            // 
            // tpRelationTypes
            // 
            tpRelationTypes.Controls.Add(ucRelationTypes);
            tpRelationTypes.Location = new Point(4, 24);
            tpRelationTypes.Name = "tpRelationTypes";
            tpRelationTypes.Padding = new Padding(3);
            tpRelationTypes.Size = new Size(1832, 1015);
            tpRelationTypes.TabIndex = 3;
            tpRelationTypes.Text = "Relation types";
            tpRelationTypes.UseVisualStyleBackColor = true;
            // 
            // ucRelationTypes
            // 
            ucRelationTypes.Dock = DockStyle.Fill;
            ucRelationTypes.Location = new Point(3, 3);
            ucRelationTypes.Name = "ucRelationTypes";
            ucRelationTypes.Size = new Size(1826, 1009);
            ucRelationTypes.TabIndex = 0;
            // 
            // tpMetasetTypes
            // 
            tpMetasetTypes.Controls.Add(ucMetasetTypes);
            tpMetasetTypes.Location = new Point(4, 24);
            tpMetasetTypes.Name = "tpMetasetTypes";
            tpMetasetTypes.Size = new Size(1832, 1015);
            tpMetasetTypes.TabIndex = 6;
            tpMetasetTypes.Text = "Metaset types";
            tpMetasetTypes.UseVisualStyleBackColor = true;
            // 
            // ucMetasetTypes
            // 
            ucMetasetTypes.Dock = DockStyle.Fill;
            ucMetasetTypes.Location = new Point(0, 0);
            ucMetasetTypes.Name = "ucMetasetTypes";
            ucMetasetTypes.Size = new Size(1832, 1015);
            ucMetasetTypes.TabIndex = 0;
            // 
            // tpLanguages
            // 
            tpLanguages.Controls.Add(ucLanguages);
            tpLanguages.Location = new Point(4, 24);
            tpLanguages.Name = "tpLanguages";
            tpLanguages.Size = new Size(1832, 1015);
            tpLanguages.TabIndex = 4;
            tpLanguages.Text = "Languages";
            tpLanguages.UseVisualStyleBackColor = true;
            // 
            // ucLanguages
            // 
            ucLanguages.Dock = DockStyle.Fill;
            ucLanguages.Location = new Point(0, 0);
            ucLanguages.Name = "ucLanguages";
            ucLanguages.Size = new Size(1832, 1015);
            ucLanguages.TabIndex = 0;
            ucLanguages.UiLanguageMode = false;
            // 
            // tpUiLanguages
            // 
            tpUiLanguages.Controls.Add(ucUiLanguages);
            tpUiLanguages.Location = new Point(4, 24);
            tpUiLanguages.Name = "tpUiLanguages";
            tpUiLanguages.Size = new Size(1832, 1015);
            tpUiLanguages.TabIndex = 5;
            tpUiLanguages.Text = "User interface languages";
            tpUiLanguages.UseVisualStyleBackColor = true;
            // 
            // ucUiLanguages
            // 
            ucUiLanguages.Dock = DockStyle.Fill;
            ucUiLanguages.Location = new Point(0, 0);
            ucUiLanguages.Name = "ucUiLanguages";
            ucUiLanguages.Size = new Size(1832, 1015);
            ucUiLanguages.TabIndex = 1;
            ucUiLanguages.UiLanguageMode = true;
            // 
            // tpIndexItems
            // 
            tpIndexItems.Controls.Add(ucIndexItems);
            tpIndexItems.Location = new Point(4, 24);
            tpIndexItems.Name = "tpIndexItems";
            tpIndexItems.Size = new Size(1832, 1015);
            tpIndexItems.TabIndex = 10;
            tpIndexItems.Text = "Index items";
            tpIndexItems.UseVisualStyleBackColor = true;
            // 
            // ucIndexItems
            // 
            ucIndexItems.Dock = DockStyle.Fill;
            ucIndexItems.Location = new Point(0, 0);
            ucIndexItems.Name = "ucIndexItems";
            ucIndexItems.Size = new Size(1832, 1015);
            ucIndexItems.TabIndex = 0;
            // 
            // tpUsers
            // 
            tpUsers.Controls.Add(ucUsers);
            tpUsers.Location = new Point(4, 24);
            tpUsers.Name = "tpUsers";
            tpUsers.Size = new Size(1832, 1015);
            tpUsers.TabIndex = 12;
            tpUsers.Text = "Users";
            tpUsers.UseVisualStyleBackColor = true;
            // 
            // ucUsers
            // 
            ucUsers.Dock = DockStyle.Fill;
            ucUsers.Location = new Point(0, 0);
            ucUsers.Name = "ucUsers";
            ucUsers.Size = new Size(1832, 1015);
            ucUsers.TabIndex = 0;
            // 
            // tpGroups
            // 
            tpGroups.Controls.Add(ucGroups);
            tpGroups.Location = new Point(4, 24);
            tpGroups.Name = "tpGroups";
            tpGroups.Size = new Size(1832, 1015);
            tpGroups.TabIndex = 9;
            tpGroups.Text = "Groups";
            tpGroups.UseVisualStyleBackColor = true;
            // 
            // ucGroups
            // 
            ucGroups.Dock = DockStyle.Fill;
            ucGroups.Location = new Point(0, 0);
            ucGroups.Name = "ucGroups";
            ucGroups.Size = new Size(1832, 1015);
            ucGroups.TabIndex = 0;
            // 
            // tpAcls
            // 
            tpAcls.Controls.Add(ucAcls);
            tpAcls.Location = new Point(4, 24);
            tpAcls.Name = "tpAcls";
            tpAcls.Size = new Size(1832, 1015);
            tpAcls.TabIndex = 14;
            tpAcls.Text = "ACLs";
            tpAcls.UseVisualStyleBackColor = true;
            // 
            // ucAcls
            // 
            ucAcls.Dock = DockStyle.Fill;
            ucAcls.Location = new Point(0, 0);
            ucAcls.Name = "ucAcls";
            ucAcls.Size = new Size(1832, 1015);
            ucAcls.TabIndex = 0;
            // 
            // tpLifecycles
            // 
            tpLifecycles.Controls.Add(ucLifecycles);
            tpLifecycles.Location = new Point(4, 24);
            tpLifecycles.Name = "tpLifecycles";
            tpLifecycles.Size = new Size(1832, 1015);
            tpLifecycles.TabIndex = 11;
            tpLifecycles.Text = "Lifecycles";
            tpLifecycles.UseVisualStyleBackColor = true;
            // 
            // ucLifecycles
            // 
            ucLifecycles.Dock = DockStyle.Fill;
            ucLifecycles.Location = new Point(0, 0);
            ucLifecycles.Name = "ucLifecycles";
            ucLifecycles.Size = new Size(1832, 1015);
            ucLifecycles.TabIndex = 0;
            // 
            // tpChangeTriggers
            // 
            tpChangeTriggers.Controls.Add(changeTriggersControl1);
            tpChangeTriggers.Location = new Point(4, 24);
            tpChangeTriggers.Name = "tpChangeTriggers";
            tpChangeTriggers.Padding = new Padding(3);
            tpChangeTriggers.Size = new Size(1832, 1015);
            tpChangeTriggers.TabIndex = 16;
            tpChangeTriggers.Text = "Change triggers";
            tpChangeTriggers.UseVisualStyleBackColor = true;
            // 
            // changeTriggersControl1
            // 
            changeTriggersControl1.Dock = DockStyle.Fill;
            changeTriggersControl1.Location = new Point(3, 3);
            changeTriggersControl1.Name = "changeTriggersControl1";
            changeTriggersControl1.Size = new Size(1826, 1009);
            changeTriggersControl1.TabIndex = 0;
            // 
            // tpConfigEntries
            // 
            tpConfigEntries.Controls.Add(ucConfigEntries);
            tpConfigEntries.Location = new Point(4, 24);
            tpConfigEntries.Name = "tpConfigEntries";
            tpConfigEntries.Padding = new Padding(3);
            tpConfigEntries.Size = new Size(1832, 1015);
            tpConfigEntries.TabIndex = 15;
            tpConfigEntries.Text = "Config entries";
            tpConfigEntries.UseVisualStyleBackColor = true;
            // 
            // ucConfigEntries
            // 
            ucConfigEntries.Dock = DockStyle.Fill;
            ucConfigEntries.Location = new Point(3, 3);
            ucConfigEntries.Name = "ucConfigEntries";
            ucConfigEntries.Size = new Size(1826, 1009);
            ucConfigEntries.TabIndex = 0;
            // 
            // tpTestInterface
            // 
            tpTestInterface.Controls.Add(ucTestInterface);
            tpTestInterface.Location = new Point(4, 24);
            tpTestInterface.Name = "tpTestInterface";
            tpTestInterface.Size = new Size(1832, 1015);
            tpTestInterface.TabIndex = 7;
            tpTestInterface.Text = "Test interface";
            tpTestInterface.UseVisualStyleBackColor = true;
            // 
            // ucTestInterface
            // 
            ucTestInterface.Dock = DockStyle.Fill;
            ucTestInterface.Location = new Point(0, 0);
            ucTestInterface.Name = "ucTestInterface";
            ucTestInterface.Size = new Size(1832, 1015);
            ucTestInterface.TabIndex = 0;
            // 
            // C4AMainWindow
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1840, 1137);
            Controls.Add(tbAdmin);
            Name = "C4AMainWindow";
            Text = "Cinnamon Administration";
            FormClosing += C4AMainWindow_FormClosing;
            tbAdmin.ResumeLayout(false);
            tpObjTypes.ResumeLayout(false);
            tpFolderTypes.ResumeLayout(false);
            tpFormats.ResumeLayout(false);
            tpRelationTypes.ResumeLayout(false);
            tpMetasetTypes.ResumeLayout(false);
            tpLanguages.ResumeLayout(false);
            tpUiLanguages.ResumeLayout(false);
            tpIndexItems.ResumeLayout(false);
            tpUsers.ResumeLayout(false);
            tpGroups.ResumeLayout(false);
            tpAcls.ResumeLayout(false);
            tpLifecycles.ResumeLayout(false);
            tpChangeTriggers.ResumeLayout(false);
            tpConfigEntries.ResumeLayout(false);
            tpTestInterface.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
        private System.Windows.Forms.TabControl tbAdmin;
        private System.Windows.Forms.TabPage tpObjTypes;
        private System.Windows.Forms.TabPage tpFormats;
        private FormatsControl ucFormats;
        private ObjectTypesControl ucObjTypes;
        private System.Windows.Forms.TabPage tpRelationTypes;
        private RelationTypesControl ucRelationTypes;
        private System.Windows.Forms.TabPage tpLanguages;
        private System.Windows.Forms.TabPage tpUiLanguages;
        private System.Windows.Forms.TabPage tpMetasetTypes;
        private LanguagesControl ucLanguages;
        private LanguagesControl ucUiLanguages;
        private MetasetTypesControl ucMetasetTypes;
        private System.Windows.Forms.TabPage tpTestInterface;
        private TestInterfaceControl ucTestInterface;
        private System.Windows.Forms.TabPage tpFolderTypes;
        private System.Windows.Forms.TabPage tpIndexItems;
        private System.Windows.Forms.TabPage tpLifecycles;
        private System.Windows.Forms.TabPage tpUsers;
        private System.Windows.Forms.TabPage tpGroups;
        private System.Windows.Forms.TabPage tpAcls;
        private FolderTypesControl ucFolderTypes;
        private UsersControl ucUsers;
        private GroupsControl ucGroups;
        private IndexItemsControl ucIndexItems;
        private AclsControl ucAcls;
        private LifecyclesControl ucLifecycles;
        private System.Windows.Forms.TabPage tpConfigEntries;
        private ConfigEntriesControl ucConfigEntries;
        private System.Windows.Forms.TabPage tpChangeTriggers;
        private ChangeTriggersControl changeTriggersControl1;
    }
}