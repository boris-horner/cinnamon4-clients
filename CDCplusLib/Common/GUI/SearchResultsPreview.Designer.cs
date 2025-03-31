namespace CDCplusLib.Common.GUI
{
    partial class SearchResultsPreview
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SearchResultsPreview));
            panel1 = new Panel();
            cmdShowResults = new Button();
            rldNodes = new ResultListDisplayLight();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Controls.Add(cmdShowResults);
            panel1.Dock = DockStyle.Bottom;
            panel1.Location = new Point(0, 862);
            panel1.Margin = new Padding(4, 3, 4, 3);
            panel1.Name = "panel1";
            panel1.Size = new Size(800, 46);
            panel1.TabIndex = 0;
            // 
            // cmdShowResults
            // 
            cmdShowResults.Dock = DockStyle.Left;
            cmdShowResults.Image = (Image)resources.GetObject("cmdShowResults.Image");
            cmdShowResults.Location = new Point(0, 0);
            cmdShowResults.Margin = new Padding(4, 3, 4, 3);
            cmdShowResults.Name = "cmdShowResults";
            cmdShowResults.Size = new Size(47, 46);
            cmdShowResults.TabIndex = 2;
            cmdShowResults.UseVisualStyleBackColor = true;
            cmdShowResults.Click += cmdShowResults_Click;
            // 
            // resultListDisplayLight1
            // 
            rldNodes.Dock = DockStyle.Fill;
            rldNodes.EventsActive = false;
            rldNodes.Location = new Point(0, 0);
            rldNodes.Margin = new Padding(4, 3, 4, 3);
            rldNodes.Name = "resultListDisplayLight1";
            rldNodes.NodeList = null;
            rldNodes.Size = new Size(800, 862);
            rldNodes.TabIndex = 1;
            // 
            // SearchResultsPreview
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 908);
            Controls.Add(rldNodes);
            Controls.Add(panel1);
            Name = "SearchResultsPreview";
            Text = "SearchResultsPreview";
            panel1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private ResultListDisplayLight rldNodes;
        internal Button cmdShowResults;
    }
}