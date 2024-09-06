using CDCplusLib.Common;
using CDCplusLib.Common.GUI;
using CDCplusLib.Interfaces;

namespace CDCplusLib.TabControls
{
    partial class ContentsFolderControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ContentsFolderControl));
            this.Panel1 = new System.Windows.Forms.Panel();
            this.cmdTranslations = new System.Windows.Forms.Button();
            this.tlpViewParameters = new System.Windows.Forms.TableLayoutPanel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.Panel61 = new System.Windows.Forms.Panel();
            this.PictureBox5 = new System.Windows.Forms.PictureBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.cboColumnSettings = new System.Windows.Forms.ComboBox();
            this.panel4 = new System.Windows.Forms.Panel();
            this.cboVersionDisplay = new System.Windows.Forms.ComboBox();
            this.panel6 = new System.Windows.Forms.Panel();
            this.cboView = new System.Windows.Forms.ComboBox();
            this.cmdSettings = new System.Windows.Forms.Button();
            this.splTranslations = new System.Windows.Forms.SplitContainer();
            this.rldNodes = new CDCplusLib.Common.GUI.ResultListDisplay();
            this.pTranslations = new System.Windows.Forms.Panel();
            this.lblTranslations = new System.Windows.Forms.Label();
            this.lvseSettings = new ListViewSettingsEditor();
            this.pTranslationContainer = new System.Windows.Forms.Panel();
            this.Panel1.SuspendLayout();
            this.tlpViewParameters.SuspendLayout();
            this.panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.Panel61.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox5)).BeginInit();
            this.panel2.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splTranslations)).BeginInit();
            this.splTranslations.Panel1.SuspendLayout();
            this.splTranslations.Panel2.SuspendLayout();
            this.splTranslations.SuspendLayout();
            this.pTranslations.SuspendLayout();
            this.SuspendLayout();
            // 
            // Panel1
            // 
            this.Panel1.Controls.Add(this.cmdTranslations);
            this.Panel1.Controls.Add(this.tlpViewParameters);
            this.Panel1.Controls.Add(this.cmdSettings);
            this.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.Panel1.Location = new System.Drawing.Point(0, 606);
            this.Panel1.Margin = new System.Windows.Forms.Padding(4);
            this.Panel1.Name = "Panel1";
            this.Panel1.Size = new System.Drawing.Size(1292, 49);
            this.Panel1.TabIndex = 2;
            // 
            // cmdTranslations
            // 
            this.cmdTranslations.Dock = System.Windows.Forms.DockStyle.Left;
            this.cmdTranslations.Image = ((System.Drawing.Image)(resources.GetObject("cmdTranslations.Image")));
            this.cmdTranslations.Location = new System.Drawing.Point(53, 0);
            this.cmdTranslations.Margin = new System.Windows.Forms.Padding(0);
            this.cmdTranslations.Name = "cmdTranslations";
            this.cmdTranslations.Size = new System.Drawing.Size(53, 49);
            this.cmdTranslations.TabIndex = 9;
            this.cmdTranslations.UseVisualStyleBackColor = true;
            this.cmdTranslations.Click += new System.EventHandler(this.CmdTranslations_Click);
            // 
            // tlpViewParameters
            // 
            this.tlpViewParameters.ColumnCount = 6;
            this.tlpViewParameters.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 53F));
            this.tlpViewParameters.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tlpViewParameters.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 53F));
            this.tlpViewParameters.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tlpViewParameters.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 53F));
            this.tlpViewParameters.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tlpViewParameters.Controls.Add(this.panel5, 4, 0);
            this.tlpViewParameters.Controls.Add(this.panel3, 2, 0);
            this.tlpViewParameters.Controls.Add(this.Panel61, 0, 0);
            this.tlpViewParameters.Controls.Add(this.panel2, 1, 0);
            this.tlpViewParameters.Controls.Add(this.panel4, 3, 0);
            this.tlpViewParameters.Controls.Add(this.panel6, 5, 0);
            this.tlpViewParameters.Dock = System.Windows.Forms.DockStyle.Right;
            this.tlpViewParameters.Location = new System.Drawing.Point(191, 0);
            this.tlpViewParameters.Margin = new System.Windows.Forms.Padding(4);
            this.tlpViewParameters.Name = "tlpViewParameters";
            this.tlpViewParameters.RowCount = 1;
            this.tlpViewParameters.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpViewParameters.Size = new System.Drawing.Size(1101, 49);
            this.tlpViewParameters.TabIndex = 8;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.pictureBox2);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel5.Location = new System.Drawing.Point(734, 0);
            this.panel5.Margin = new System.Windows.Forms.Padding(0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(53, 49);
            this.panel5.TabIndex = 61;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(3, 2);
            this.pictureBox2.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(43, 39);
            this.pictureBox2.TabIndex = 7;
            this.pictureBox2.TabStop = false;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.pictureBox1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel3.Location = new System.Drawing.Point(367, 0);
            this.panel3.Margin = new System.Windows.Forms.Padding(0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(53, 49);
            this.panel3.TabIndex = 59;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(3, 2);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(43, 39);
            this.pictureBox1.TabIndex = 7;
            this.pictureBox1.TabStop = false;
            // 
            // Panel61
            // 
            this.Panel61.Controls.Add(this.PictureBox5);
            this.Panel61.Dock = System.Windows.Forms.DockStyle.Left;
            this.Panel61.Location = new System.Drawing.Point(0, 0);
            this.Panel61.Margin = new System.Windows.Forms.Padding(0);
            this.Panel61.Name = "Panel61";
            this.Panel61.Size = new System.Drawing.Size(53, 49);
            this.Panel61.TabIndex = 57;
            // 
            // PictureBox5
            // 
            this.PictureBox5.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox5.Image")));
            this.PictureBox5.Location = new System.Drawing.Point(3, 2);
            this.PictureBox5.Margin = new System.Windows.Forms.Padding(0);
            this.PictureBox5.Name = "PictureBox5";
            this.PictureBox5.Size = new System.Drawing.Size(43, 39);
            this.PictureBox5.TabIndex = 7;
            this.PictureBox5.TabStop = false;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.cboColumnSettings);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(57, 4);
            this.panel2.Margin = new System.Windows.Forms.Padding(4);
            this.panel2.Name = "panel2";
            this.panel2.Padding = new System.Windows.Forms.Padding(0, 0, 27, 0);
            this.panel2.Size = new System.Drawing.Size(306, 41);
            this.panel2.TabIndex = 58;
            // 
            // cboColumnSettings
            // 
            this.cboColumnSettings.Dock = System.Windows.Forms.DockStyle.Top;
            this.cboColumnSettings.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboColumnSettings.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboColumnSettings.FormattingEnabled = true;
            this.cboColumnSettings.Location = new System.Drawing.Point(0, 0);
            this.cboColumnSettings.Margin = new System.Windows.Forms.Padding(4);
            this.cboColumnSettings.Name = "cboColumnSettings";
            this.cboColumnSettings.Size = new System.Drawing.Size(279, 25);
            this.cboColumnSettings.TabIndex = 14;
            this.cboColumnSettings.SelectedIndexChanged += new System.EventHandler(this.cboColumnSettings_SelectedIndexChanged);
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.cboVersionDisplay);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(424, 4);
            this.panel4.Margin = new System.Windows.Forms.Padding(4);
            this.panel4.Name = "panel4";
            this.panel4.Padding = new System.Windows.Forms.Padding(0, 0, 27, 0);
            this.panel4.Size = new System.Drawing.Size(306, 41);
            this.panel4.TabIndex = 60;
            // 
            // cboVersionDisplay
            // 
            this.cboVersionDisplay.Dock = System.Windows.Forms.DockStyle.Top;
            this.cboVersionDisplay.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboVersionDisplay.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboVersionDisplay.FormattingEnabled = true;
            this.cboVersionDisplay.Location = new System.Drawing.Point(0, 0);
            this.cboVersionDisplay.Margin = new System.Windows.Forms.Padding(4);
            this.cboVersionDisplay.Name = "cboVersionDisplay";
            this.cboVersionDisplay.Size = new System.Drawing.Size(279, 25);
            this.cboVersionDisplay.TabIndex = 12;
            this.cboVersionDisplay.SelectedIndexChanged += new System.EventHandler(this.cboVersionDisplay_SelectedIndexChanged);
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.cboView);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel6.Location = new System.Drawing.Point(791, 4);
            this.panel6.Margin = new System.Windows.Forms.Padding(4);
            this.panel6.Name = "panel6";
            this.panel6.Padding = new System.Windows.Forms.Padding(0, 0, 27, 0);
            this.panel6.Size = new System.Drawing.Size(306, 41);
            this.panel6.TabIndex = 62;
            // 
            // cboView
            // 
            this.cboView.Dock = System.Windows.Forms.DockStyle.Top;
            this.cboView.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboView.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboView.FormattingEnabled = true;
            this.cboView.Location = new System.Drawing.Point(0, 0);
            this.cboView.Margin = new System.Windows.Forms.Padding(4);
            this.cboView.Name = "cboView";
            this.cboView.Size = new System.Drawing.Size(279, 25);
            this.cboView.TabIndex = 14;
            this.cboView.SelectedIndexChanged += new System.EventHandler(this.cboView_SelectedIndexChanged);
            // 
            // cmdSettings
            // 
            this.cmdSettings.Dock = System.Windows.Forms.DockStyle.Left;
            this.cmdSettings.Image = ((System.Drawing.Image)(resources.GetObject("cmdSettings.Image")));
            this.cmdSettings.Location = new System.Drawing.Point(0, 0);
            this.cmdSettings.Margin = new System.Windows.Forms.Padding(0);
            this.cmdSettings.Name = "cmdSettings";
            this.cmdSettings.Size = new System.Drawing.Size(53, 49);
            this.cmdSettings.TabIndex = 4;
            this.cmdSettings.UseVisualStyleBackColor = true;
            this.cmdSettings.Click += new System.EventHandler(this.cmdSettings_Click);
            // 
            // splTranslations
            // 
            this.splTranslations.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splTranslations.Location = new System.Drawing.Point(0, 0);
            this.splTranslations.Margin = new System.Windows.Forms.Padding(4);
            this.splTranslations.Name = "splTranslations";
            this.splTranslations.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splTranslations.Panel1
            // 
            this.splTranslations.Panel1.Controls.Add(this.rldNodes);
            // 
            // splTranslations.Panel2
            // 
            this.splTranslations.Panel2.Controls.Add(this.pTranslations);
            this.splTranslations.Size = new System.Drawing.Size(1292, 606);
            this.splTranslations.SplitterDistance = 320;
            this.splTranslations.SplitterWidth = 5;
            this.splTranslations.TabIndex = 4;
            // 
            // rldNodes
            // 
            this.rldNodes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rldNodes.Location = new System.Drawing.Point(0, 0);
            this.rldNodes.Margin = new System.Windows.Forms.Padding(5);
            this.rldNodes.Name = "rldNodes";
            this.rldNodes.NodeList = null;
            //this.rldNodes.Selection = ((System.Collections.Generic.Dictionary<long, C4ObjectApi.Interfaces.IRepositoryNode>)(resources.GetObject("rldNodes.Selection")));
            this.rldNodes.Size = new System.Drawing.Size(1292, 320);
            this.rldNodes.TabIndex = 11;
            this.rldNodes.View = System.Windows.Forms.View.LargeIcon;
            this.rldNodes.MessageSent += new CDCplusLib.Interfaces.IGenericControl.MessageSentEventHandler(this.rldNodes_MessageSent);
            // 
            // pTranslations
            // 
            this.pTranslations.Controls.Add(this.pTranslationContainer);
            this.pTranslations.Controls.Add(this.lblTranslations);
            this.pTranslations.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pTranslations.Location = new System.Drawing.Point(0, 0);
            this.pTranslations.Margin = new System.Windows.Forms.Padding(4);
            this.pTranslations.Name = "pTranslations";
            this.pTranslations.Size = new System.Drawing.Size(1292, 281);
            this.pTranslations.TabIndex = 16;
            // 
            // lblTranslations
            // 
            this.lblTranslations.AutoSize = true;
            this.lblTranslations.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTranslations.Location = new System.Drawing.Point(0, 0);
            this.lblTranslations.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTranslations.Name = "lblTranslations";
            this.lblTranslations.Size = new System.Drawing.Size(81, 16);
            this.lblTranslations.TabIndex = 0;
            this.lblTranslations.Text = "Translations";
            // 
            // lvseSettings
            // 
            this.lvseSettings.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lvseSettings.Location = new System.Drawing.Point(0, 378);
            this.lvseSettings.Margin = new System.Windows.Forms.Padding(5);
            this.lvseSettings.Name = "lvseSettings";
            this.lvseSettings.Size = new System.Drawing.Size(1292, 228);
            this.lvseSettings.TabIndex = 5;
            this.lvseSettings.Apply += new System.EventHandler(this.lvseSettings_Apply);
            // 
            // pTranslationContainer
            // 
            this.pTranslationContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pTranslationContainer.Location = new System.Drawing.Point(0, 16);
            this.pTranslationContainer.Name = "pTranslationContainer";
            this.pTranslationContainer.Size = new System.Drawing.Size(1292, 265);
            this.pTranslationContainer.TabIndex = 1;
            // 
            // ContentsFolderControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lvseSettings);
            this.Controls.Add(this.splTranslations);
            this.Controls.Add(this.Panel1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "ContentsFolderControl";
            this.Size = new System.Drawing.Size(1292, 655);
            this.Panel1.ResumeLayout(false);
            this.tlpViewParameters.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.Panel61.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox5)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            this.splTranslations.Panel1.ResumeLayout(false);
            this.splTranslations.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splTranslations)).EndInit();
            this.splTranslations.ResumeLayout(false);
            this.pTranslations.ResumeLayout(false);
            this.pTranslations.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        internal System.Windows.Forms.Panel Panel1;
        internal System.Windows.Forms.TableLayoutPanel tlpViewParameters;
        internal System.Windows.Forms.Button cmdSettings;
        internal System.Windows.Forms.Panel panel5;
        internal System.Windows.Forms.PictureBox pictureBox2;
        internal System.Windows.Forms.Panel panel3;
        internal System.Windows.Forms.PictureBox pictureBox1;
        internal System.Windows.Forms.Panel Panel61;
        internal System.Windows.Forms.PictureBox PictureBox5;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel6;
        internal System.Windows.Forms.ComboBox cboVersionDisplay;
        internal System.Windows.Forms.ComboBox cboColumnSettings;
        internal System.Windows.Forms.ComboBox cboView;
        internal System.Windows.Forms.Button cmdTranslations;
        private System.Windows.Forms.SplitContainer splTranslations;
        internal ResultListDisplay rldNodes;
        private System.Windows.Forms.Panel pTranslations;
        private System.Windows.Forms.Label lblTranslations;
        private ListViewSettingsEditor lvseSettings;
        private System.Windows.Forms.Panel pTranslationContainer;
    }
}
