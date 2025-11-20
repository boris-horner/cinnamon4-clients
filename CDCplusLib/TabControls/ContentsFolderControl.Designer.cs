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
            Panel1 = new Panel();
            cmdTranslations = new Button();
            tlpViewParameters = new TableLayoutPanel();
            panel5 = new Panel();
            pictureBox2 = new PictureBox();
            panel3 = new Panel();
            pictureBox1 = new PictureBox();
            Panel61 = new Panel();
            PictureBox5 = new PictureBox();
            panel2 = new Panel();
            cboColumnSettings = new ComboBox();
            panel4 = new Panel();
            cboVersionDisplay = new ComboBox();
            panel6 = new Panel();
            cboView = new ComboBox();
            cmdSettings = new Button();
            splTranslations = new SplitContainer();
            rldNodes = new ResultListDisplay();
            pTranslations = new Panel();
            pTranslationContainer = new Panel();
            lblTranslations = new Label();
            lvseSettings = new ListViewSettingsEditor();
            Panel1.SuspendLayout();
            tlpViewParameters.SuspendLayout();
            panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            Panel61.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)PictureBox5).BeginInit();
            panel2.SuspendLayout();
            panel4.SuspendLayout();
            panel6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splTranslations).BeginInit();
            splTranslations.Panel1.SuspendLayout();
            splTranslations.Panel2.SuspendLayout();
            splTranslations.SuspendLayout();
            pTranslations.SuspendLayout();
            SuspendLayout();
            // 
            // Panel1
            // 
            Panel1.Controls.Add(cmdTranslations);
            Panel1.Controls.Add(tlpViewParameters);
            Panel1.Controls.Add(cmdSettings);
            Panel1.Dock = DockStyle.Bottom;
            Panel1.Location = new Point(0, 568);
            Panel1.Margin = new Padding(4);
            Panel1.Name = "Panel1";
            Panel1.Size = new Size(1130, 46);
            Panel1.TabIndex = 2;
            // 
            // cmdTranslations
            // 
            cmdTranslations.Dock = DockStyle.Left;
            cmdTranslations.Image = (Image)resources.GetObject("cmdTranslations.Image");
            cmdTranslations.Location = new Point(46, 0);
            cmdTranslations.Margin = new Padding(0);
            cmdTranslations.Name = "cmdTranslations";
            cmdTranslations.Size = new Size(46, 46);
            cmdTranslations.TabIndex = 9;
            cmdTranslations.UseVisualStyleBackColor = true;
            cmdTranslations.Click += CmdTranslations_Click;
            // 
            // tlpViewParameters
            // 
            tlpViewParameters.ColumnCount = 6;
            tlpViewParameters.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 46F));
            tlpViewParameters.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33333F));
            tlpViewParameters.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 46F));
            tlpViewParameters.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33333F));
            tlpViewParameters.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 46F));
            tlpViewParameters.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33333F));
            tlpViewParameters.Controls.Add(panel5, 4, 0);
            tlpViewParameters.Controls.Add(panel3, 2, 0);
            tlpViewParameters.Controls.Add(Panel61, 0, 0);
            tlpViewParameters.Controls.Add(panel2, 1, 0);
            tlpViewParameters.Controls.Add(panel4, 3, 0);
            tlpViewParameters.Controls.Add(panel6, 5, 0);
            tlpViewParameters.Dock = DockStyle.Right;
            tlpViewParameters.Location = new Point(167, 0);
            tlpViewParameters.Margin = new Padding(4);
            tlpViewParameters.Name = "tlpViewParameters";
            tlpViewParameters.RowCount = 1;
            tlpViewParameters.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpViewParameters.Size = new Size(963, 46);
            tlpViewParameters.TabIndex = 8;
            // 
            // panel5
            // 
            panel5.Controls.Add(pictureBox2);
            panel5.Dock = DockStyle.Left;
            panel5.Location = new Point(642, 0);
            panel5.Margin = new Padding(0);
            panel5.Name = "panel5";
            panel5.Size = new Size(46, 46);
            panel5.TabIndex = 61;
            // 
            // pictureBox2
            // 
            pictureBox2.Image = (Image)resources.GetObject("pictureBox2.Image");
            pictureBox2.Location = new Point(3, 2);
            pictureBox2.Margin = new Padding(0);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(38, 37);
            pictureBox2.TabIndex = 7;
            pictureBox2.TabStop = false;
            // 
            // panel3
            // 
            panel3.Controls.Add(pictureBox1);
            panel3.Dock = DockStyle.Left;
            panel3.Location = new Point(321, 0);
            panel3.Margin = new Padding(0);
            panel3.Name = "panel3";
            panel3.Size = new Size(46, 46);
            panel3.TabIndex = 59;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(3, 2);
            pictureBox1.Margin = new Padding(0);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(38, 37);
            pictureBox1.TabIndex = 7;
            pictureBox1.TabStop = false;
            // 
            // Panel61
            // 
            Panel61.Controls.Add(PictureBox5);
            Panel61.Dock = DockStyle.Left;
            Panel61.Location = new Point(0, 0);
            Panel61.Margin = new Padding(0);
            Panel61.Name = "Panel61";
            Panel61.Size = new Size(46, 46);
            Panel61.TabIndex = 57;
            // 
            // PictureBox5
            // 
            PictureBox5.Image = (Image)resources.GetObject("PictureBox5.Image");
            PictureBox5.Location = new Point(3, 2);
            PictureBox5.Margin = new Padding(0);
            PictureBox5.Name = "PictureBox5";
            PictureBox5.Size = new Size(38, 37);
            PictureBox5.TabIndex = 7;
            PictureBox5.TabStop = false;
            // 
            // panel2
            // 
            panel2.Controls.Add(cboColumnSettings);
            panel2.Dock = DockStyle.Fill;
            panel2.Location = new Point(50, 4);
            panel2.Margin = new Padding(4);
            panel2.Name = "panel2";
            panel2.Padding = new Padding(0, 0, 24, 0);
            panel2.Size = new Size(267, 38);
            panel2.TabIndex = 58;
            // 
            // cboColumnSettings
            // 
            cboColumnSettings.Dock = DockStyle.Top;
            cboColumnSettings.DropDownStyle = ComboBoxStyle.DropDownList;
            cboColumnSettings.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            cboColumnSettings.FormattingEnabled = true;
            cboColumnSettings.Location = new Point(0, 0);
            cboColumnSettings.Margin = new Padding(4);
            cboColumnSettings.Name = "cboColumnSettings";
            cboColumnSettings.Size = new Size(243, 21);
            cboColumnSettings.TabIndex = 14;
            cboColumnSettings.SelectedIndexChanged += cboColumnSettings_SelectedIndexChanged;
            // 
            // panel4
            // 
            panel4.Controls.Add(cboVersionDisplay);
            panel4.Dock = DockStyle.Fill;
            panel4.Location = new Point(371, 4);
            panel4.Margin = new Padding(4);
            panel4.Name = "panel4";
            panel4.Padding = new Padding(0, 0, 24, 0);
            panel4.Size = new Size(267, 38);
            panel4.TabIndex = 60;
            // 
            // cboVersionDisplay
            // 
            cboVersionDisplay.Dock = DockStyle.Top;
            cboVersionDisplay.DropDownStyle = ComboBoxStyle.DropDownList;
            cboVersionDisplay.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            cboVersionDisplay.FormattingEnabled = true;
            cboVersionDisplay.Location = new Point(0, 0);
            cboVersionDisplay.Margin = new Padding(4);
            cboVersionDisplay.Name = "cboVersionDisplay";
            cboVersionDisplay.Size = new Size(243, 21);
            cboVersionDisplay.TabIndex = 12;
            cboVersionDisplay.SelectedIndexChanged += cboVersionDisplay_SelectedIndexChanged;
            // 
            // panel6
            // 
            panel6.Controls.Add(cboView);
            panel6.Dock = DockStyle.Fill;
            panel6.Location = new Point(692, 4);
            panel6.Margin = new Padding(4);
            panel6.Name = "panel6";
            panel6.Padding = new Padding(0, 0, 24, 0);
            panel6.Size = new Size(267, 38);
            panel6.TabIndex = 62;
            // 
            // cboView
            // 
            cboView.Dock = DockStyle.Top;
            cboView.DropDownStyle = ComboBoxStyle.DropDownList;
            cboView.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            cboView.FormattingEnabled = true;
            cboView.Location = new Point(0, 0);
            cboView.Margin = new Padding(4);
            cboView.Name = "cboView";
            cboView.Size = new Size(243, 21);
            cboView.TabIndex = 14;
            cboView.SelectedIndexChanged += cboView_SelectedIndexChanged;
            // 
            // cmdSettings
            // 
            cmdSettings.Dock = DockStyle.Left;
            cmdSettings.Image = (Image)resources.GetObject("cmdSettings.Image");
            cmdSettings.Location = new Point(0, 0);
            cmdSettings.Margin = new Padding(0);
            cmdSettings.Name = "cmdSettings";
            cmdSettings.Size = new Size(46, 46);
            cmdSettings.TabIndex = 4;
            cmdSettings.UseVisualStyleBackColor = true;
            cmdSettings.Click += cmdSettings_Click;
            // 
            // splTranslations
            // 
            splTranslations.Dock = DockStyle.Fill;
            splTranslations.Location = new Point(0, 0);
            splTranslations.Margin = new Padding(4);
            splTranslations.Name = "splTranslations";
            splTranslations.Orientation = Orientation.Horizontal;
            // 
            // splTranslations.Panel1
            // 
            splTranslations.Panel1.Controls.Add(rldNodes);
            // 
            // splTranslations.Panel2
            // 
            splTranslations.Panel2.Controls.Add(pTranslations);
            splTranslations.Size = new Size(1130, 568);
            splTranslations.SplitterDistance = 299;
            splTranslations.SplitterWidth = 5;
            splTranslations.TabIndex = 4;
            // 
            // rldNodes
            // 
            rldNodes.Dock = DockStyle.Fill;
            rldNodes.EventsActive = false;
            rldNodes.Location = new Point(0, 0);
            rldNodes.Margin = new Padding(5, 3, 5, 3);
            rldNodes.Name = "rldNodes";
            rldNodes.NodeList = null;
            rldNodes.Selection = (Dictionary<long, C4ObjectApi.Interfaces.IRepositoryNode>)resources.GetObject("rldNodes.Selection");
            rldNodes.Size = new Size(1130, 299);
            rldNodes.TabIndex = 5;
            rldNodes.View = View.LargeIcon;
            // 
            // pTranslations
            // 
            pTranslations.Controls.Add(pTranslationContainer);
            pTranslations.Controls.Add(lblTranslations);
            pTranslations.Dock = DockStyle.Fill;
            pTranslations.Location = new Point(0, 0);
            pTranslations.Margin = new Padding(4);
            pTranslations.Name = "pTranslations";
            pTranslations.Size = new Size(1130, 264);
            pTranslations.TabIndex = 16;
            // 
            // pTranslationContainer
            // 
            pTranslationContainer.Dock = DockStyle.Fill;
            pTranslationContainer.Location = new Point(0, 15);
            pTranslationContainer.Name = "pTranslationContainer";
            pTranslationContainer.Size = new Size(1130, 249);
            pTranslationContainer.TabIndex = 1;
            // 
            // lblTranslations
            // 
            lblTranslations.AutoSize = true;
            lblTranslations.Dock = DockStyle.Top;
            lblTranslations.Location = new Point(0, 0);
            lblTranslations.Margin = new Padding(4, 0, 4, 0);
            lblTranslations.Name = "lblTranslations";
            lblTranslations.Size = new Size(70, 15);
            lblTranslations.TabIndex = 0;
            lblTranslations.Text = "Translations";
            // 
            // lvseSettings
            // 
            lvseSettings.Dock = DockStyle.Bottom;
            lvseSettings.Location = new Point(0, 311);
            lvseSettings.Margin = new Padding(5);
            lvseSettings.Name = "lvseSettings";
            lvseSettings.Size = new Size(1130, 257);
            lvseSettings.TabIndex = 6;
            // 
            // ContentsFolderControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(lvseSettings);
            Controls.Add(splTranslations);
            Controls.Add(Panel1);
            Margin = new Padding(4);
            Name = "ContentsFolderControl";
            Size = new Size(1130, 614);
            Panel1.ResumeLayout(false);
            tlpViewParameters.ResumeLayout(false);
            panel5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            Panel61.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)PictureBox5).EndInit();
            panel2.ResumeLayout(false);
            panel4.ResumeLayout(false);
            panel6.ResumeLayout(false);
            splTranslations.Panel1.ResumeLayout(false);
            splTranslations.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splTranslations).EndInit();
            splTranslations.ResumeLayout(false);
            pTranslations.ResumeLayout(false);
            pTranslations.PerformLayout();
            ResumeLayout(false);

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
