
namespace CDCplusLib.Common.GUI
{
    partial class MainWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
            tstrRepositoryNav = new ToolStrip();
            txtQuickSearch = new ToolStripTextBox();
            tsbQuickSearch = new ToolStripButton();
            sstrRepositoryNav = new StatusStrip();
            tsslUser = new ToolStripStatusLabel();
            tsslServer = new ToolStripStatusLabel();
            tsslConfiguration = new ToolStripStatusLabel();
            tsslStatus = new ToolStripStatusLabel();
            splVert = new SplitContainer();
            stSession = new SessionTree();
            splHor = new SplitContainer();
            ctccTreeContext = new ContextTabControlContainer();
            ctccListContext = new ContextTabControlContainer();
            tstrRepositoryNav.SuspendLayout();
            sstrRepositoryNav.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splVert).BeginInit();
            splVert.Panel1.SuspendLayout();
            splVert.Panel2.SuspendLayout();
            splVert.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splHor).BeginInit();
            splHor.Panel1.SuspendLayout();
            splHor.Panel2.SuspendLayout();
            splHor.SuspendLayout();
            SuspendLayout();
            // 
            // tstrRepositoryNav
            // 
            tstrRepositoryNav.ImageScalingSize = new Size(20, 20);
            tstrRepositoryNav.Items.AddRange(new ToolStripItem[] { txtQuickSearch, tsbQuickSearch });
            tstrRepositoryNav.Location = new Point(0, 0);
            tstrRepositoryNav.Name = "tstrRepositoryNav";
            tstrRepositoryNav.Size = new Size(1647, 25);
            tstrRepositoryNav.TabIndex = 5;
            tstrRepositoryNav.Text = "ToolStrip1";
            // 
            // txtQuickSearch
            // 
            txtQuickSearch.Name = "txtQuickSearch";
            txtQuickSearch.Size = new Size(145, 25);
            txtQuickSearch.KeyPress += txtQuickSearch_KeyPress;
            txtQuickSearch.TextChanged += txtQuickSearch_TextChanged;
            // 
            // tsbQuickSearch
            // 
            tsbQuickSearch.DisplayStyle = ToolStripItemDisplayStyle.Image;
            tsbQuickSearch.ImageTransparentColor = Color.Magenta;
            tsbQuickSearch.Name = "tsbQuickSearch";
            tsbQuickSearch.Size = new Size(23, 22);
            tsbQuickSearch.Click += tsbQuickSearch_Click;
            // 
            // sstrRepositoryNav
            // 
            sstrRepositoryNav.ImageScalingSize = new Size(20, 20);
            sstrRepositoryNav.Items.AddRange(new ToolStripItem[] { tsslUser, tsslServer, tsslConfiguration, tsslStatus });
            sstrRepositoryNav.Location = new Point(0, 1162);
            sstrRepositoryNav.Name = "sstrRepositoryNav";
            sstrRepositoryNav.Padding = new Padding(1, 0, 17, 0);
            sstrRepositoryNav.Size = new Size(1647, 24);
            sstrRepositoryNav.TabIndex = 4;
            sstrRepositoryNav.Text = "StatusStrip1";
            // 
            // tsslUser
            // 
            tsslUser.BackColor = Color.LightSkyBlue;
            tsslUser.Margin = new Padding(0, 3, 8, 2);
            tsslUser.Name = "tsslUser";
            tsslUser.Padding = new Padding(2);
            tsslUser.Size = new Size(192, 19);
            tsslUser.Text = "Administrator (admin) [Superuser]";
            // 
            // tsslServer
            // 
            tsslServer.Margin = new Padding(0, 3, 8, 2);
            tsslServer.Name = "tsslServer";
            tsslServer.Padding = new Padding(2);
            tsslServer.Size = new Size(43, 19);
            tsslServer.Text = "Demo";
            // 
            // tsslConfiguration
            // 
            tsslConfiguration.BackColor = SystemColors.Control;
            tsslConfiguration.Margin = new Padding(0, 3, 8, 2);
            tsslConfiguration.Name = "tsslConfiguration";
            tsslConfiguration.Padding = new Padding(2);
            tsslConfiguration.Size = new Size(46, 19);
            tsslConfiguration.Text = "(none)";
            // 
            // tsslStatus
            // 
            tsslStatus.BackColor = Color.Gold;
            tsslStatus.Margin = new Padding(0, 3, 8, 2);
            tsslStatus.Name = "tsslStatus";
            tsslStatus.Padding = new Padding(2);
            tsslStatus.Size = new Size(46, 19);
            tsslStatus.Text = "(none)";
            // 
            // splVert
            // 
            splVert.Dock = DockStyle.Fill;
            splVert.Location = new Point(0, 25);
            splVert.Name = "splVert";
            // 
            // splVert.Panel1
            // 
            splVert.Panel1.Controls.Add(stSession);
            // 
            // splVert.Panel2
            // 
            splVert.Panel2.Controls.Add(splHor);
            splVert.Size = new Size(1647, 1137);
            splVert.SplitterDistance = 357;
            splVert.TabIndex = 6;
            // 
            // stSession
            // 
            stSession.Dock = DockStyle.Fill;
            stSession.Location = new Point(0, 0);
            stSession.Name = "stSession";
            stSession.SelectedNode = null;
            stSession.Size = new Size(357, 1137);
            stSession.TabIndex = 0;
            stSession.SelectionChanged += stSession_SelectionChanged;
            // 
            // splHor
            // 
            splHor.Dock = DockStyle.Fill;
            splHor.Location = new Point(0, 0);
            splHor.Name = "splHor";
            splHor.Orientation = Orientation.Horizontal;
            // 
            // splHor.Panel1
            // 
            splHor.Panel1.Controls.Add(ctccTreeContext);
            // 
            // splHor.Panel2
            // 
            splHor.Panel2.Controls.Add(ctccListContext);
            splHor.Size = new Size(1286, 1137);
            splHor.SplitterDistance = 458;
            splHor.TabIndex = 0;
            // 
            // ctccTreeContext
            // 
            ctccTreeContext.Dock = DockStyle.Fill;
            ctccTreeContext.Location = new Point(0, 0);
            ctccTreeContext.Name = "ctccTreeContext";
            ctccTreeContext.Size = new Size(1286, 458);
            ctccTreeContext.TabIndex = 0;
            // 
            // ctccListContext
            // 
            ctccListContext.Dock = DockStyle.Fill;
            ctccListContext.Location = new Point(0, 0);
            ctccListContext.Name = "ctccListContext";
            ctccListContext.Size = new Size(1286, 675);
            ctccListContext.TabIndex = 0;
            // 
            // MainWindow
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1647, 1186);
            Controls.Add(splVert);
            Controls.Add(tstrRepositoryNav);
            Controls.Add(sstrRepositoryNav);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "MainWindow";
            Text = "MainWindow";
            FormClosing += MainWindow_FormClosing;
            tstrRepositoryNav.ResumeLayout(false);
            tstrRepositoryNav.PerformLayout();
            sstrRepositoryNav.ResumeLayout(false);
            sstrRepositoryNav.PerformLayout();
            splVert.Panel1.ResumeLayout(false);
            splVert.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splVert).EndInit();
            splVert.ResumeLayout(false);
            splHor.Panel1.ResumeLayout(false);
            splHor.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splHor).EndInit();
            splHor.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        internal System.Windows.Forms.ToolStrip tstrRepositoryNav;
        internal System.Windows.Forms.ToolStripTextBox txtQuickSearch;
        internal System.Windows.Forms.ToolStripButton tsbQuickSearch;
        internal System.Windows.Forms.StatusStrip sstrRepositoryNav;
        internal System.Windows.Forms.ToolStripStatusLabel tsslUser;
        internal System.Windows.Forms.ToolStripStatusLabel tsslServer;
        internal System.Windows.Forms.ToolStripStatusLabel tsslConfiguration;
        internal System.Windows.Forms.ToolStripStatusLabel tsslStatus;
        private System.Windows.Forms.SplitContainer splVert;
        private System.Windows.Forms.SplitContainer splHor;
        private ContextTabControlContainer ctccTreeContext;
        private ContextTabControlContainer ctccListContext;
        private SessionTree stSession;
    }
}