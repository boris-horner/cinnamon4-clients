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
            this.Panel1 = new System.Windows.Forms.Panel();
            this.tlpViewParameters = new System.Windows.Forms.TableLayoutPanel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.Panel61 = new System.Windows.Forms.Panel();
            this.PictureBox5 = new System.Windows.Forms.PictureBox();
            this.panel6 = new System.Windows.Forms.Panel();
            this.cboView = new System.Windows.Forms.ComboBox();
            this.cmdSettings = new System.Windows.Forms.Button();
            this.lvseSettings = new ListViewSettingsEditor();
            this.rldNodes = new ResultListDisplay();
            this.cboColumnSettings = new System.Windows.Forms.ComboBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.Panel1.SuspendLayout();
            this.tlpViewParameters.SuspendLayout();
            this.panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.Panel61.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox5)).BeginInit();
            this.panel6.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // Panel1
            // 
            this.Panel1.Controls.Add(this.tlpViewParameters);
            this.Panel1.Controls.Add(this.cmdSettings);
            this.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.Panel1.Location = new System.Drawing.Point(0, 492);
            this.Panel1.Name = "Panel1";
            this.Panel1.Size = new System.Drawing.Size(969, 40);
            this.Panel1.TabIndex = 2;
            // 
            // tlpViewParameters
            // 
            this.tlpViewParameters.ColumnCount = 4;
            this.tlpViewParameters.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tlpViewParameters.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpViewParameters.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tlpViewParameters.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpViewParameters.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpViewParameters.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpViewParameters.Controls.Add(this.panel5, 2, 0);
            this.tlpViewParameters.Controls.Add(this.Panel61, 0, 0);
            this.tlpViewParameters.Controls.Add(this.panel2, 1, 0);
            this.tlpViewParameters.Controls.Add(this.panel6, 3, 0);
            this.tlpViewParameters.Dock = System.Windows.Forms.DockStyle.Right;
            this.tlpViewParameters.Location = new System.Drawing.Point(143, 0);
            this.tlpViewParameters.Name = "tlpViewParameters";
            this.tlpViewParameters.RowCount = 1;
            this.tlpViewParameters.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpViewParameters.Size = new System.Drawing.Size(826, 40);
            this.tlpViewParameters.TabIndex = 8;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.pictureBox2);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel5.Location = new System.Drawing.Point(413, 0);
            this.panel5.Margin = new System.Windows.Forms.Padding(0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(40, 40);
            this.panel5.TabIndex = 61;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(2, 2);
            this.pictureBox2.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(32, 32);
            this.pictureBox2.TabIndex = 7;
            this.pictureBox2.TabStop = false;
            // 
            // Panel61
            // 
            this.Panel61.Controls.Add(this.PictureBox5);
            this.Panel61.Dock = System.Windows.Forms.DockStyle.Left;
            this.Panel61.Location = new System.Drawing.Point(0, 0);
            this.Panel61.Margin = new System.Windows.Forms.Padding(0);
            this.Panel61.Name = "Panel61";
            this.Panel61.Size = new System.Drawing.Size(40, 40);
            this.Panel61.TabIndex = 57;
            // 
            // PictureBox5
            // 
            this.PictureBox5.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox5.Image")));
            this.PictureBox5.Location = new System.Drawing.Point(2, 2);
            this.PictureBox5.Margin = new System.Windows.Forms.Padding(0);
            this.PictureBox5.Name = "PictureBox5";
            this.PictureBox5.Size = new System.Drawing.Size(32, 32);
            this.PictureBox5.TabIndex = 7;
            this.PictureBox5.TabStop = false;
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.cboView);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel6.Location = new System.Drawing.Point(456, 3);
            this.panel6.Name = "panel6";
            this.panel6.Padding = new System.Windows.Forms.Padding(0, 0, 20, 0);
            this.panel6.Size = new System.Drawing.Size(367, 34);
            this.panel6.TabIndex = 62;
            // 
            // cboView
            // 
            this.cboView.Dock = System.Windows.Forms.DockStyle.Top;
            this.cboView.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboView.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboView.FormattingEnabled = true;
            this.cboView.Location = new System.Drawing.Point(0, 0);
            this.cboView.Name = "cboView";
            this.cboView.Size = new System.Drawing.Size(347, 21);
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
            this.cmdSettings.Size = new System.Drawing.Size(40, 40);
            this.cmdSettings.TabIndex = 4;
            this.cmdSettings.UseVisualStyleBackColor = true;
            this.cmdSettings.Click += new System.EventHandler(this.cmdSettings_Click);
            // 
            // lvseSettings
            // 
            this.lvseSettings.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lvseSettings.Location = new System.Drawing.Point(0, 269);
            this.lvseSettings.Name = "lvseSettings";
            this.lvseSettings.Size = new System.Drawing.Size(969, 223);
            this.lvseSettings.TabIndex = 5;
            this.lvseSettings.Apply += new System.EventHandler(this.lvseSettings_Apply);
            // 
            // rldNodes
            // 
            this.rldNodes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rldNodes.Location = new System.Drawing.Point(0, 0);
            this.rldNodes.Name = "rldNodes";
            this.rldNodes.NodeList = null;
            this.rldNodes.Selection = ((System.Collections.Generic.Dictionary<long, C4ObjectApi.Interfaces.IRepositoryNode>)(resources.GetObject("rldNodes.Selection")));
            this.rldNodes.Size = new System.Drawing.Size(969, 492);
            this.rldNodes.TabIndex = 4;
            this.rldNodes.View = System.Windows.Forms.View.LargeIcon;
            this.rldNodes.ListSelectionChanged += new ListSelectionChangedEventHandler(this.rldNodes_ListSelectionChanged);
            this.rldNodes.TreeSelectionChanged += new TreeSelectionChangedEventHandler(this.rldNodes_TreeSelectionChanged);
            this.rldNodes.ContextMenuRequest += new ContextMenuRequestEventHandler(this.rldNodes_ContextMenuRequest);
            this.rldNodes.FunctionRequest += new FunctionRequestEventHandler(this.rldNodes_FunctionRequest);
            this.rldNodes.KeyPressedEvent += new KeyPressedEventHandler(this.rldNodes_KeyPressedEvent);
            this.rldNodes.RefreshRequest += new RefreshRequestEventHandler(this.rldNodes_RefreshRequest);
            // 
            // cboColumnSettings
            // 
            this.cboColumnSettings.Dock = System.Windows.Forms.DockStyle.Top;
            this.cboColumnSettings.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboColumnSettings.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboColumnSettings.FormattingEnabled = true;
            this.cboColumnSettings.Location = new System.Drawing.Point(0, 0);
            this.cboColumnSettings.Name = "cboColumnSettings";
            this.cboColumnSettings.Size = new System.Drawing.Size(347, 21);
            this.cboColumnSettings.TabIndex = 14;
            this.cboColumnSettings.SelectedIndexChanged += new System.EventHandler(this.cboColumnSettings_SelectedIndexChanged);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.cboColumnSettings);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(43, 3);
            this.panel2.Name = "panel2";
            this.panel2.Padding = new System.Windows.Forms.Padding(0, 0, 20, 0);
            this.panel2.Size = new System.Drawing.Size(367, 34);
            this.panel2.TabIndex = 58;
            // 
            // ContentsSessionControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lvseSettings);
            this.Controls.Add(this.rldNodes);
            this.Controls.Add(this.Panel1);
            this.Name = "ContentsSessionControl";
            this.Size = new System.Drawing.Size(969, 532);
            this.Panel1.ResumeLayout(false);
            this.tlpViewParameters.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.Panel61.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox5)).EndInit();
            this.panel6.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

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
