using CDCplusLib.Common;
using CDCplusLib.Common.GUI;
using CDCplusLib.Interfaces;

namespace CDCplusLib.TabControls
{
    partial class ContentsSessionControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ContentsSessionControl));
            Panel1 = new Panel();
            tlpViewParameters = new TableLayoutPanel();
            panel5 = new Panel();
            pictureBox2 = new PictureBox();
            Panel61 = new Panel();
            PictureBox5 = new PictureBox();
            panel2 = new Panel();
            cboColumnSettings = new ComboBox();
            panel6 = new Panel();
            cboView = new ComboBox();
            cmdSettings = new Button();
            lvseSettings = new ListViewSettingsEditor();
            rldNodes = new ResultListDisplay();
            Panel1.SuspendLayout();
            tlpViewParameters.SuspendLayout();
            panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            Panel61.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)PictureBox5).BeginInit();
            panel2.SuspendLayout();
            panel6.SuspendLayout();
            SuspendLayout();
            // 
            // Panel1
            // 
            Panel1.Controls.Add(tlpViewParameters);
            Panel1.Controls.Add(cmdSettings);
            Panel1.Dock = DockStyle.Bottom;
            Panel1.Location = new Point(0, 568);
            Panel1.Margin = new Padding(4, 3, 4, 3);
            Panel1.Name = "Panel1";
            Panel1.Size = new Size(1130, 46);
            Panel1.TabIndex = 2;
            // 
            // tlpViewParameters
            // 
            tlpViewParameters.ColumnCount = 4;
            tlpViewParameters.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 47F));
            tlpViewParameters.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlpViewParameters.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 47F));
            tlpViewParameters.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlpViewParameters.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 23F));
            tlpViewParameters.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 23F));
            tlpViewParameters.Controls.Add(panel5, 2, 0);
            tlpViewParameters.Controls.Add(Panel61, 0, 0);
            tlpViewParameters.Controls.Add(panel2, 1, 0);
            tlpViewParameters.Controls.Add(panel6, 3, 0);
            tlpViewParameters.Dock = DockStyle.Right;
            tlpViewParameters.Location = new Point(166, 0);
            tlpViewParameters.Margin = new Padding(4, 3, 4, 3);
            tlpViewParameters.Name = "tlpViewParameters";
            tlpViewParameters.RowCount = 1;
            tlpViewParameters.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpViewParameters.Size = new Size(964, 46);
            tlpViewParameters.TabIndex = 8;
            // 
            // panel5
            // 
            panel5.Controls.Add(pictureBox2);
            panel5.Dock = DockStyle.Left;
            panel5.Location = new Point(482, 0);
            panel5.Margin = new Padding(0);
            panel5.Name = "panel5";
            panel5.Size = new Size(47, 46);
            panel5.TabIndex = 61;
            // 
            // pictureBox2
            // 
            pictureBox2.Image = (Image)resources.GetObject("pictureBox2.Image");
            pictureBox2.Location = new Point(2, 2);
            pictureBox2.Margin = new Padding(0);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(37, 37);
            pictureBox2.TabIndex = 7;
            pictureBox2.TabStop = false;
            // 
            // Panel61
            // 
            Panel61.Controls.Add(PictureBox5);
            Panel61.Dock = DockStyle.Left;
            Panel61.Location = new Point(0, 0);
            Panel61.Margin = new Padding(0);
            Panel61.Name = "Panel61";
            Panel61.Size = new Size(47, 46);
            Panel61.TabIndex = 57;
            // 
            // PictureBox5
            // 
            PictureBox5.Image = (Image)resources.GetObject("PictureBox5.Image");
            PictureBox5.Location = new Point(2, 2);
            PictureBox5.Margin = new Padding(0);
            PictureBox5.Name = "PictureBox5";
            PictureBox5.Size = new Size(37, 37);
            PictureBox5.TabIndex = 7;
            PictureBox5.TabStop = false;
            // 
            // panel2
            // 
            panel2.Controls.Add(cboColumnSettings);
            panel2.Dock = DockStyle.Fill;
            panel2.Location = new Point(51, 3);
            panel2.Margin = new Padding(4, 3, 4, 3);
            panel2.Name = "panel2";
            panel2.Padding = new Padding(0, 0, 23, 0);
            panel2.Size = new Size(427, 40);
            panel2.TabIndex = 58;
            // 
            // cboColumnSettings
            // 
            cboColumnSettings.Dock = DockStyle.Top;
            cboColumnSettings.DropDownStyle = ComboBoxStyle.DropDownList;
            cboColumnSettings.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            cboColumnSettings.FormattingEnabled = true;
            cboColumnSettings.Location = new Point(0, 0);
            cboColumnSettings.Margin = new Padding(4, 3, 4, 3);
            cboColumnSettings.Name = "cboColumnSettings";
            cboColumnSettings.Size = new Size(404, 21);
            cboColumnSettings.TabIndex = 14;
            cboColumnSettings.SelectedIndexChanged += cboColumnSettings_SelectedIndexChanged;
            // 
            // panel6
            // 
            panel6.Controls.Add(cboView);
            panel6.Dock = DockStyle.Fill;
            panel6.Location = new Point(533, 3);
            panel6.Margin = new Padding(4, 3, 4, 3);
            panel6.Name = "panel6";
            panel6.Padding = new Padding(0, 0, 23, 0);
            panel6.Size = new Size(427, 40);
            panel6.TabIndex = 62;
            // 
            // cboView
            // 
            cboView.Dock = DockStyle.Top;
            cboView.DropDownStyle = ComboBoxStyle.DropDownList;
            cboView.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            cboView.FormattingEnabled = true;
            cboView.Location = new Point(0, 0);
            cboView.Margin = new Padding(4, 3, 4, 3);
            cboView.Name = "cboView";
            cboView.Size = new Size(404, 21);
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
            cmdSettings.Size = new Size(47, 46);
            cmdSettings.TabIndex = 4;
            cmdSettings.UseVisualStyleBackColor = true;
            cmdSettings.Click += cmdSettings_Click;
            // 
            // lvseSettings
            // 
            lvseSettings.Dock = DockStyle.Bottom;
            lvseSettings.Location = new Point(0, 311);
            lvseSettings.Margin = new Padding(5, 5, 5, 5);
            lvseSettings.Name = "lvseSettings";
            lvseSettings.Size = new Size(1130, 257);
            lvseSettings.TabIndex = 5;
            lvseSettings.Apply += lvseSettings_Apply;
            // 
            // rldNodes
            // 
            rldNodes.ControlContextType = IGenericControl.ContextType.Session;
            rldNodes.Dock = DockStyle.Fill;
            rldNodes.EventsActive = false;
            rldNodes.Location = new Point(0, 0);
            rldNodes.Margin = new Padding(5, 3, 5, 3);
            rldNodes.Name = "rldNodes";
            rldNodes.NodeList = null;
            rldNodes.Selection = (Dictionary<long, C4ObjectApi.Interfaces.IRepositoryNode>)resources.GetObject("rldNodes.Selection");
            rldNodes.Size = new Size(1130, 568);
            rldNodes.TabIndex = 4;
            rldNodes.View = View.LargeIcon;
            rldNodes.TreeSelectionChanged += rldNodes_TreeSelectionChanged;
            rldNodes.ListSelectionChanged += rldNodes_ListSelectionChanged;
            rldNodes.ContextMenuRequest += rldNodes_ContextMenuRequest;
            rldNodes.FunctionRequest += rldNodes_FunctionRequest;
            rldNodes.KeyPressedEvent += rldNodes_KeyPressedEvent;
            rldNodes.RefreshRequest += rldNodes_RefreshRequest;
            // 
            // ContentsSessionControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(lvseSettings);
            Controls.Add(rldNodes);
            Controls.Add(Panel1);
            Margin = new Padding(4, 3, 4, 3);
            Name = "ContentsSessionControl";
            Size = new Size(1130, 614);
            Panel1.ResumeLayout(false);
            tlpViewParameters.ResumeLayout(false);
            panel5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            Panel61.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)PictureBox5).EndInit();
            panel2.ResumeLayout(false);
            panel6.ResumeLayout(false);
            ResumeLayout(false);

        }

        #endregion
        internal System.Windows.Forms.Panel Panel1;
        internal System.Windows.Forms.TableLayoutPanel tlpViewParameters;
        internal System.Windows.Forms.Button cmdSettings;
        internal System.Windows.Forms.Panel panel5;
        internal System.Windows.Forms.PictureBox pictureBox2;
        internal System.Windows.Forms.Panel Panel61;
        internal System.Windows.Forms.PictureBox PictureBox5;
        private System.Windows.Forms.Panel panel6;
        internal System.Windows.Forms.ComboBox cboView;
        internal ResultListDisplay rldNodes;
        private ListViewSettingsEditor lvseSettings;
        private System.Windows.Forms.Panel panel2;
        internal System.Windows.Forms.ComboBox cboColumnSettings;
    }
}
