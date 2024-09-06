
namespace CDCplusLib.Common.GUI
{
    partial class LifecycleStateControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LifecycleStateControl));
            this.TableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.PictureBox5 = new System.Windows.Forms.PictureBox();
            this.tvwLifecycle = new System.Windows.Forms.TreeView();
            this.optLifecycle = new System.Windows.Forms.RadioButton();
            this.optNoLifecycle = new System.Windows.Forms.RadioButton();
            this.TableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox5)).BeginInit();
            this.SuspendLayout();
            // 
            // TableLayoutPanel1
            // 
            this.TableLayoutPanel1.ColumnCount = 2;
            this.TableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 52F));
            this.TableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.TableLayoutPanel1.Controls.Add(this.PictureBox5, 0, 0);
            this.TableLayoutPanel1.Controls.Add(this.tvwLifecycle, 1, 2);
            this.TableLayoutPanel1.Controls.Add(this.optLifecycle, 1, 1);
            this.TableLayoutPanel1.Controls.Add(this.optNoLifecycle, 1, 0);
            this.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.TableLayoutPanel1.Name = "TableLayoutPanel1";
            this.TableLayoutPanel1.RowCount = 3;
            this.TableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.TableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.TableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.TableLayoutPanel1.Size = new System.Drawing.Size(589, 392);
            this.TableLayoutPanel1.TabIndex = 1;
            // 
            // PictureBox5
            // 
            this.PictureBox5.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox5.Image")));
            this.PictureBox5.Location = new System.Drawing.Point(9, 9);
            this.PictureBox5.Margin = new System.Windows.Forms.Padding(9);
            this.PictureBox5.Name = "PictureBox5";
            this.TableLayoutPanel1.SetRowSpan(this.PictureBox5, 2);
            this.PictureBox5.Size = new System.Drawing.Size(32, 32);
            this.PictureBox5.TabIndex = 13;
            this.PictureBox5.TabStop = false;
            // 
            // tvwLifecycle
            // 
            this.tvwLifecycle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvwLifecycle.HideSelection = false;
            this.tvwLifecycle.Location = new System.Drawing.Point(55, 53);
            this.tvwLifecycle.Name = "tvwLifecycle";
            this.tvwLifecycle.Size = new System.Drawing.Size(531, 336);
            this.tvwLifecycle.TabIndex = 12;
            this.tvwLifecycle.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvwLifecycle_AfterSelect);
            // 
            // optLifecycle
            // 
            this.optLifecycle.AutoSize = true;
            this.optLifecycle.Location = new System.Drawing.Point(55, 28);
            this.optLifecycle.Name = "optLifecycle";
            this.optLifecycle.Size = new System.Drawing.Size(114, 17);
            this.optLifecycle.TabIndex = 11;
            this.optLifecycle.TabStop = true;
            this.optLifecycle.Text = "Lifecycle and state";
            this.optLifecycle.UseVisualStyleBackColor = true;
            this.optLifecycle.CheckedChanged += new System.EventHandler(this.optLifecycle_CheckedChanged);
            // 
            // optNoLifecycle
            // 
            this.optNoLifecycle.AutoSize = true;
            this.optNoLifecycle.Location = new System.Drawing.Point(55, 3);
            this.optNoLifecycle.Name = "optNoLifecycle";
            this.optNoLifecycle.Size = new System.Drawing.Size(80, 17);
            this.optNoLifecycle.TabIndex = 10;
            this.optNoLifecycle.TabStop = true;
            this.optNoLifecycle.Text = "No lifecycle";
            this.optNoLifecycle.UseVisualStyleBackColor = true;
            this.optNoLifecycle.CheckedChanged += new System.EventHandler(this.optNoLifecycle_CheckedChanged);
            // 
            // LifecycleStateControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.TableLayoutPanel1);
            this.Name = "LifecycleStateControl";
            this.Size = new System.Drawing.Size(589, 392);
            this.TableLayoutPanel1.ResumeLayout(false);
            this.TableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox5)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.TableLayoutPanel TableLayoutPanel1;
        internal System.Windows.Forms.PictureBox PictureBox5;
        internal System.Windows.Forms.TreeView tvwLifecycle;
        internal System.Windows.Forms.RadioButton optLifecycle;
        internal System.Windows.Forms.RadioButton optNoLifecycle;
    }
}
