
namespace C4Admin.GUI.AssetEditors
{
    partial class EditChangeTrigger
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
            panel7 = new Panel();
            pCancel = new Panel();
            cmdCancel = new Button();
            pSaveAs = new Panel();
            cmdSaveAs = new Button();
            pSave = new Panel();
            cmdOk = new Button();
            pData = new Panel();
            xtxtConfig = new C4GeneralGui.GuiElements.XmlTextBox();
            lblConfig = new Label();
            vtxtRanking = new C4GeneralGui.GuiElements.ValidatingTextBox();
            lblRanking = new Label();
            vtxtAction = new C4GeneralGui.GuiElements.ValidatingTextBox();
            lblAction = new Label();
            vtxtController = new C4GeneralGui.GuiElements.ValidatingTextBox();
            lblController = new Label();
            chkCopyFileContent = new CheckBox();
            chkPostCommitTrigger = new CheckBox();
            chkPostTrigger = new CheckBox();
            chkPreTrigger = new CheckBox();
            chkActive = new CheckBox();
            vtxtTriggerType = new C4GeneralGui.GuiElements.ValidatingTextBox();
            lblType = new Label();
            vtxtName = new C4GeneralGui.GuiElements.ValidatingTextBox();
            lblName = new Label();
            txtId = new TextBox();
            lblId = new Label();
            panel7.SuspendLayout();
            pCancel.SuspendLayout();
            pSaveAs.SuspendLayout();
            pSave.SuspendLayout();
            pData.SuspendLayout();
            SuspendLayout();
            // 
            // panel7
            // 
            panel7.Controls.Add(pCancel);
            panel7.Controls.Add(pSaveAs);
            panel7.Controls.Add(pSave);
            panel7.Dock = DockStyle.Bottom;
            panel7.Location = new Point(0, 834);
            panel7.Name = "panel7";
            panel7.Size = new Size(711, 37);
            panel7.TabIndex = 6;
            // 
            // pCancel
            // 
            pCancel.Controls.Add(cmdCancel);
            pCancel.Dock = DockStyle.Left;
            pCancel.Location = new Point(224, 0);
            pCancel.Name = "pCancel";
            pCancel.Padding = new Padding(6, 7, 10, 7);
            pCancel.Size = new Size(112, 37);
            pCancel.TabIndex = 11;
            // 
            // cmdCancel
            // 
            cmdCancel.DialogResult = DialogResult.Cancel;
            cmdCancel.Dock = DockStyle.Fill;
            cmdCancel.Location = new Point(6, 7);
            cmdCancel.Name = "cmdCancel";
            cmdCancel.Size = new Size(96, 23);
            cmdCancel.TabIndex = 9;
            cmdCancel.Text = "Cancel";
            cmdCancel.UseVisualStyleBackColor = true;
            cmdCancel.Click += cmdCancel_Click;
            // 
            // pSaveAs
            // 
            pSaveAs.Controls.Add(cmdSaveAs);
            pSaveAs.Dock = DockStyle.Left;
            pSaveAs.Location = new Point(112, 0);
            pSaveAs.Name = "pSaveAs";
            pSaveAs.Padding = new Padding(6, 7, 10, 7);
            pSaveAs.Size = new Size(112, 37);
            pSaveAs.TabIndex = 10;
            // 
            // cmdSaveAs
            // 
            cmdSaveAs.DialogResult = DialogResult.Cancel;
            cmdSaveAs.Dock = DockStyle.Fill;
            cmdSaveAs.Location = new Point(6, 7);
            cmdSaveAs.Name = "cmdSaveAs";
            cmdSaveAs.Size = new Size(96, 23);
            cmdSaveAs.TabIndex = 6;
            cmdSaveAs.Text = "Save as";
            cmdSaveAs.UseVisualStyleBackColor = true;
            cmdSaveAs.Click += cmdSaveAs_Click;
            // 
            // pSave
            // 
            pSave.Controls.Add(cmdOk);
            pSave.Dock = DockStyle.Left;
            pSave.Location = new Point(0, 0);
            pSave.Name = "pSave";
            pSave.Padding = new Padding(6, 7, 10, 7);
            pSave.Size = new Size(112, 37);
            pSave.TabIndex = 9;
            // 
            // cmdOk
            // 
            cmdOk.Dock = DockStyle.Fill;
            cmdOk.Location = new Point(6, 7);
            cmdOk.Name = "cmdOk";
            cmdOk.Size = new Size(96, 23);
            cmdOk.TabIndex = 2;
            cmdOk.Text = "Save";
            cmdOk.UseVisualStyleBackColor = true;
            cmdOk.Click += cmdOk_Click;
            // 
            // pData
            // 
            pData.Controls.Add(xtxtConfig);
            pData.Controls.Add(lblConfig);
            pData.Controls.Add(vtxtRanking);
            pData.Controls.Add(lblRanking);
            pData.Controls.Add(vtxtAction);
            pData.Controls.Add(lblAction);
            pData.Controls.Add(vtxtController);
            pData.Controls.Add(lblController);
            pData.Controls.Add(chkCopyFileContent);
            pData.Controls.Add(chkPostCommitTrigger);
            pData.Controls.Add(chkPostTrigger);
            pData.Controls.Add(chkPreTrigger);
            pData.Controls.Add(chkActive);
            pData.Controls.Add(vtxtTriggerType);
            pData.Controls.Add(lblType);
            pData.Controls.Add(vtxtName);
            pData.Controls.Add(lblName);
            pData.Controls.Add(txtId);
            pData.Controls.Add(lblId);
            pData.Dock = DockStyle.Fill;
            pData.Location = new Point(0, 0);
            pData.Name = "pData";
            pData.Padding = new Padding(5, 6, 5, 6);
            pData.Size = new Size(711, 834);
            pData.TabIndex = 0;
            // 
            // xtxtConfig
            // 
            xtxtConfig.Dock = DockStyle.Fill;
            xtxtConfig.Location = new Point(5, 448);
            xtxtConfig.Name = "xtxtConfig";
            xtxtConfig.Size = new Size(701, 380);
            xtxtConfig.TabIndex = 78;
            xtxtConfig.Text = "";
            // 
            // lblConfig
            // 
            lblConfig.AutoSize = true;
            lblConfig.Dock = DockStyle.Top;
            lblConfig.Location = new Point(5, 424);
            lblConfig.Margin = new Padding(0);
            lblConfig.Name = "lblConfig";
            lblConfig.Padding = new Padding(0, 9, 0, 0);
            lblConfig.Size = new Size(81, 24);
            lblConfig.TabIndex = 77;
            lblConfig.Text = "Configuration";
            // 
            // vtxtRanking
            // 
            vtxtRanking.BackColor = Color.Yellow;
            vtxtRanking.BothInvalidColor = Color.Red;
            vtxtRanking.Dock = DockStyle.Top;
            vtxtRanking.ExternalInvalidColor = Color.Orange;
            vtxtRanking.ExternalValidation = null;
            vtxtRanking.InternalInvalidColor = Color.Yellow;
            vtxtRanking.InvalidReadOnlyColor = Color.LightYellow;
            vtxtRanking.Location = new Point(5, 401);
            vtxtRanking.Name = "vtxtRanking";
            vtxtRanking.RegularExpression = "^[0-9]+$";
            vtxtRanking.Size = new Size(701, 23);
            vtxtRanking.TabIndex = 75;
            vtxtRanking.ValidColor = Color.White;
            vtxtRanking.ValidReadOnlyColor = SystemColors.Control;
            // 
            // lblRanking
            // 
            lblRanking.AutoSize = true;
            lblRanking.Dock = DockStyle.Top;
            lblRanking.Location = new Point(5, 377);
            lblRanking.Margin = new Padding(0);
            lblRanking.Name = "lblRanking";
            lblRanking.Padding = new Padding(0, 9, 0, 0);
            lblRanking.Size = new Size(50, 24);
            lblRanking.TabIndex = 76;
            lblRanking.Text = "Ranking";
            // 
            // vtxtAction
            // 
            vtxtAction.BackColor = Color.Yellow;
            vtxtAction.BothInvalidColor = Color.Red;
            vtxtAction.Dock = DockStyle.Top;
            vtxtAction.ExternalInvalidColor = Color.Orange;
            vtxtAction.ExternalValidation = null;
            vtxtAction.InternalInvalidColor = Color.Yellow;
            vtxtAction.InvalidReadOnlyColor = Color.LightYellow;
            vtxtAction.Location = new Point(5, 354);
            vtxtAction.Name = "vtxtAction";
            vtxtAction.RegularExpression = "^[a-zA-Z0-9_.-]+$";
            vtxtAction.Size = new Size(701, 23);
            vtxtAction.TabIndex = 73;
            vtxtAction.ValidColor = Color.White;
            vtxtAction.ValidReadOnlyColor = SystemColors.Control;
            // 
            // lblAction
            // 
            lblAction.AutoSize = true;
            lblAction.Dock = DockStyle.Top;
            lblAction.Location = new Point(5, 330);
            lblAction.Margin = new Padding(0);
            lblAction.Name = "lblAction";
            lblAction.Padding = new Padding(0, 9, 0, 0);
            lblAction.Size = new Size(42, 24);
            lblAction.TabIndex = 74;
            lblAction.Text = "Action";
            // 
            // vtxtController
            // 
            vtxtController.BackColor = Color.Yellow;
            vtxtController.BothInvalidColor = Color.Red;
            vtxtController.Dock = DockStyle.Top;
            vtxtController.ExternalInvalidColor = Color.Orange;
            vtxtController.ExternalValidation = null;
            vtxtController.InternalInvalidColor = Color.Yellow;
            vtxtController.InvalidReadOnlyColor = Color.LightYellow;
            vtxtController.Location = new Point(5, 307);
            vtxtController.Name = "vtxtController";
            vtxtController.RegularExpression = "^[a-zA-Z0-9_.-]+$";
            vtxtController.Size = new Size(701, 23);
            vtxtController.TabIndex = 71;
            vtxtController.ValidColor = Color.White;
            vtxtController.ValidReadOnlyColor = SystemColors.Control;
            // 
            // lblController
            // 
            lblController.AutoSize = true;
            lblController.Dock = DockStyle.Top;
            lblController.Location = new Point(5, 283);
            lblController.Margin = new Padding(0);
            lblController.Name = "lblController";
            lblController.Padding = new Padding(0, 9, 0, 0);
            lblController.Size = new Size(60, 24);
            lblController.TabIndex = 72;
            lblController.Text = "Controller";
            // 
            // chkCopyFileContent
            // 
            chkCopyFileContent.AutoSize = true;
            chkCopyFileContent.Dock = DockStyle.Top;
            chkCopyFileContent.Location = new Point(5, 254);
            chkCopyFileContent.Name = "chkCopyFileContent";
            chkCopyFileContent.Padding = new Padding(0, 10, 0, 0);
            chkCopyFileContent.Size = new Size(701, 29);
            chkCopyFileContent.TabIndex = 70;
            chkCopyFileContent.Text = "Copy file content";
            chkCopyFileContent.UseVisualStyleBackColor = true;
            // 
            // chkPostCommitTrigger
            // 
            chkPostCommitTrigger.AutoSize = true;
            chkPostCommitTrigger.Dock = DockStyle.Top;
            chkPostCommitTrigger.Location = new Point(5, 225);
            chkPostCommitTrigger.Name = "chkPostCommitTrigger";
            chkPostCommitTrigger.Padding = new Padding(0, 10, 0, 0);
            chkPostCommitTrigger.Size = new Size(701, 29);
            chkPostCommitTrigger.TabIndex = 65;
            chkPostCommitTrigger.Text = "Post-Commit-Trigger";
            chkPostCommitTrigger.UseVisualStyleBackColor = true;
            // 
            // chkPostTrigger
            // 
            chkPostTrigger.AutoSize = true;
            chkPostTrigger.Dock = DockStyle.Top;
            chkPostTrigger.Location = new Point(5, 196);
            chkPostTrigger.Name = "chkPostTrigger";
            chkPostTrigger.Padding = new Padding(0, 10, 0, 0);
            chkPostTrigger.Size = new Size(701, 29);
            chkPostTrigger.TabIndex = 58;
            chkPostTrigger.Text = "Post-Trigger";
            chkPostTrigger.UseVisualStyleBackColor = true;
            // 
            // chkPreTrigger
            // 
            chkPreTrigger.AutoSize = true;
            chkPreTrigger.Dock = DockStyle.Top;
            chkPreTrigger.Location = new Point(5, 167);
            chkPreTrigger.Name = "chkPreTrigger";
            chkPreTrigger.Padding = new Padding(0, 10, 0, 0);
            chkPreTrigger.Size = new Size(701, 29);
            chkPreTrigger.TabIndex = 57;
            chkPreTrigger.Text = "Pre-Trigger";
            chkPreTrigger.UseVisualStyleBackColor = true;
            // 
            // chkActive
            // 
            chkActive.AutoSize = true;
            chkActive.Dock = DockStyle.Top;
            chkActive.Location = new Point(5, 138);
            chkActive.Name = "chkActive";
            chkActive.Padding = new Padding(0, 10, 0, 0);
            chkActive.Size = new Size(701, 29);
            chkActive.TabIndex = 56;
            chkActive.Text = "Active";
            chkActive.UseVisualStyleBackColor = true;
            // 
            // vtxtTriggerType
            // 
            vtxtTriggerType.BackColor = Color.Yellow;
            vtxtTriggerType.BothInvalidColor = Color.Red;
            vtxtTriggerType.Dock = DockStyle.Top;
            vtxtTriggerType.Enabled = false;
            vtxtTriggerType.ExternalInvalidColor = Color.Orange;
            vtxtTriggerType.ExternalValidation = null;
            vtxtTriggerType.InternalInvalidColor = Color.Yellow;
            vtxtTriggerType.InvalidReadOnlyColor = Color.LightYellow;
            vtxtTriggerType.Location = new Point(5, 115);
            vtxtTriggerType.Name = "vtxtTriggerType";
            vtxtTriggerType.RegularExpression = "^[a-zA-Z0-9_.-]+$";
            vtxtTriggerType.Size = new Size(701, 23);
            vtxtTriggerType.TabIndex = 5;
            vtxtTriggerType.ValidColor = Color.White;
            vtxtTriggerType.ValidReadOnlyColor = SystemColors.Control;
            // 
            // lblType
            // 
            lblType.AutoSize = true;
            lblType.Dock = DockStyle.Top;
            lblType.Location = new Point(5, 91);
            lblType.Margin = new Padding(0);
            lblType.Name = "lblType";
            lblType.Padding = new Padding(0, 9, 0, 0);
            lblType.Size = new Size(32, 24);
            lblType.TabIndex = 6;
            lblType.Text = "Type";
            // 
            // vtxtName
            // 
            vtxtName.BackColor = Color.Yellow;
            vtxtName.BothInvalidColor = Color.Red;
            vtxtName.Dock = DockStyle.Top;
            vtxtName.ExternalInvalidColor = Color.Orange;
            vtxtName.ExternalValidation = null;
            vtxtName.InternalInvalidColor = Color.Yellow;
            vtxtName.InvalidReadOnlyColor = Color.LightYellow;
            vtxtName.Location = new Point(5, 68);
            vtxtName.Name = "vtxtName";
            vtxtName.RegularExpression = "^[a-zA-Z0-9_.-]+$";
            vtxtName.Size = new Size(701, 23);
            vtxtName.TabIndex = 0;
            vtxtName.ValidColor = Color.White;
            vtxtName.ValidReadOnlyColor = SystemColors.Control;
            vtxtName.TextChanged += vtxtName_TextChanged;
            // 
            // lblName
            // 
            lblName.AutoSize = true;
            lblName.Dock = DockStyle.Top;
            lblName.Location = new Point(5, 44);
            lblName.Margin = new Padding(0);
            lblName.Name = "lblName";
            lblName.Padding = new Padding(0, 9, 0, 0);
            lblName.Size = new Size(39, 24);
            lblName.TabIndex = 4;
            lblName.Text = "Name";
            // 
            // txtId
            // 
            txtId.Dock = DockStyle.Top;
            txtId.Location = new Point(5, 21);
            txtId.Margin = new Padding(0);
            txtId.Name = "txtId";
            txtId.ReadOnly = true;
            txtId.Size = new Size(701, 23);
            txtId.TabIndex = 1;
            // 
            // lblId
            // 
            lblId.AutoSize = true;
            lblId.Dock = DockStyle.Top;
            lblId.Location = new Point(5, 6);
            lblId.Margin = new Padding(0);
            lblId.Name = "lblId";
            lblId.Size = new Size(17, 15);
            lblId.TabIndex = 0;
            lblId.Text = "Id";
            // 
            // EditChangeTrigger
            // 
            AcceptButton = cmdOk;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = cmdCancel;
            ClientSize = new Size(711, 871);
            Controls.Add(pData);
            Controls.Add(panel7);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Name = "EditChangeTrigger";
            Text = "Edit change trigger";
            Load += EditChangeTrigger_Load;
            panel7.ResumeLayout(false);
            pCancel.ResumeLayout(false);
            pSaveAs.ResumeLayout(false);
            pSave.ResumeLayout(false);
            pData.ResumeLayout(false);
            pData.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Panel pData;
        private System.Windows.Forms.TextBox txtId;
        private System.Windows.Forms.Label lblId;
        private System.Windows.Forms.Label lblName;
        private C4GeneralGui.GuiElements.ValidatingTextBox vtxtName;
        private System.Windows.Forms.Panel pCancel;
        private System.Windows.Forms.Button cmdCancel;
        private System.Windows.Forms.Panel pSaveAs;
        private System.Windows.Forms.Button cmdSaveAs;
        private System.Windows.Forms.Panel pSave;
        private System.Windows.Forms.Button cmdOk;
        private C4GeneralGui.GuiElements.ValidatingTextBox vtxtTriggerType;
        private System.Windows.Forms.Label lblType;
        private System.Windows.Forms.CheckBox chkPostTrigger;
        private System.Windows.Forms.CheckBox chkPreTrigger;
        private CheckBox chkPostCommitTrigger;
        private System.Windows.Forms.CheckBox chkActive;
        private C4GeneralGui.GuiElements.XmlTextBox xtxtConfig;
        private Label lblConfig;
        private C4GeneralGui.GuiElements.ValidatingTextBox vtxtRanking;
        private Label lblRanking;
        private C4GeneralGui.GuiElements.ValidatingTextBox vtxtAction;
        private Label lblAction;
        private C4GeneralGui.GuiElements.ValidatingTextBox vtxtController;
        private Label lblController;
        private CheckBox chkCopyFileContent;
    }
}