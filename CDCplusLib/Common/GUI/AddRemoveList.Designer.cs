namespace CDCplusLib.Common.GUI
{
    partial class AddRemoveList
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddRemoveList));
            this.lbAssigned = new System.Windows.Forms.ListBox();
            this.TableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.lbNotAssigned = new System.Windows.Forms.ListBox();
            this.Panel1 = new System.Windows.Forms.Panel();
            this.cmdRemove = new System.Windows.Forms.Button();
            this.cmdAdd = new System.Windows.Forms.Button();
            this.TableLayoutPanel1.SuspendLayout();
            this.Panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbAssigned
            // 
            this.lbAssigned.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbAssigned.FormattingEnabled = true;
            this.lbAssigned.ItemHeight = 16;
            this.lbAssigned.Location = new System.Drawing.Point(383, 4);
            this.lbAssigned.Margin = new System.Windows.Forms.Padding(4);
            this.lbAssigned.Name = "lbAssigned";
            this.lbAssigned.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lbAssigned.Size = new System.Drawing.Size(320, 436);
            this.lbAssigned.Sorted = true;
            this.lbAssigned.TabIndex = 1;
            this.lbAssigned.SelectedIndexChanged += new System.EventHandler(this.lbAssigned_SelectedIndexChanged);
            this.lbAssigned.DoubleClick += new System.EventHandler(this.lbAssigned_DoubleClick);
            // 
            // TableLayoutPanel1
            // 
            this.TableLayoutPanel1.ColumnCount = 3;
            this.TableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.TableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 51F));
            this.TableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.TableLayoutPanel1.Controls.Add(this.lbNotAssigned, 0, 0);
            this.TableLayoutPanel1.Controls.Add(this.lbAssigned, 2, 0);
            this.TableLayoutPanel1.Controls.Add(this.Panel1, 1, 0);
            this.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.TableLayoutPanel1.Margin = new System.Windows.Forms.Padding(4);
            this.TableLayoutPanel1.Name = "TableLayoutPanel1";
            this.TableLayoutPanel1.RowCount = 1;
            this.TableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.TableLayoutPanel1.Size = new System.Drawing.Size(707, 444);
            this.TableLayoutPanel1.TabIndex = 1;
            // 
            // lbNotAssigned
            // 
            this.lbNotAssigned.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbNotAssigned.FormattingEnabled = true;
            this.lbNotAssigned.ItemHeight = 16;
            this.lbNotAssigned.Location = new System.Drawing.Point(4, 4);
            this.lbNotAssigned.Margin = new System.Windows.Forms.Padding(4);
            this.lbNotAssigned.Name = "lbNotAssigned";
            this.lbNotAssigned.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lbNotAssigned.Size = new System.Drawing.Size(320, 436);
            this.lbNotAssigned.Sorted = true;
            this.lbNotAssigned.TabIndex = 0;
            this.lbNotAssigned.SelectedIndexChanged += new System.EventHandler(this.lbNotAssigned_SelectedIndexChanged);
            this.lbNotAssigned.DoubleClick += new System.EventHandler(this.lbNotAssigned_DoubleClick);
            // 
            // Panel1
            // 
            this.Panel1.Controls.Add(this.cmdRemove);
            this.Panel1.Controls.Add(this.cmdAdd);
            this.Panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Panel1.Location = new System.Drawing.Point(332, 4);
            this.Panel1.Margin = new System.Windows.Forms.Padding(4);
            this.Panel1.Name = "Panel1";
            this.Panel1.Size = new System.Drawing.Size(43, 436);
            this.Panel1.TabIndex = 2;
            // 
            // cmdRemove
            // 
            this.cmdRemove.Image = ((System.Drawing.Image)(resources.GetObject("cmdRemove.Image")));
            this.cmdRemove.Location = new System.Drawing.Point(5, 41);
            this.cmdRemove.Margin = new System.Windows.Forms.Padding(4);
            this.cmdRemove.Name = "cmdRemove";
            this.cmdRemove.Size = new System.Drawing.Size(32, 30);
            this.cmdRemove.TabIndex = 1;
            this.cmdRemove.UseVisualStyleBackColor = true;
            this.cmdRemove.Click += new System.EventHandler(this.cmdRemove_Click);
            // 
            // cmdAdd
            // 
            this.cmdAdd.Image = ((System.Drawing.Image)(resources.GetObject("cmdAdd.Image")));
            this.cmdAdd.Location = new System.Drawing.Point(5, 5);
            this.cmdAdd.Margin = new System.Windows.Forms.Padding(4);
            this.cmdAdd.Name = "cmdAdd";
            this.cmdAdd.Size = new System.Drawing.Size(32, 30);
            this.cmdAdd.TabIndex = 0;
            this.cmdAdd.UseVisualStyleBackColor = true;
            this.cmdAdd.Click += new System.EventHandler(this.cmdAdd_Click);
            // 
            // AddRemoveList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.TableLayoutPanel1);
            this.Name = "AddRemoveList";
            this.Size = new System.Drawing.Size(707, 444);
            this.TableLayoutPanel1.ResumeLayout(false);
            this.Panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.ListBox lbAssigned;
        internal System.Windows.Forms.TableLayoutPanel TableLayoutPanel1;
        internal System.Windows.Forms.ListBox lbNotAssigned;
        internal System.Windows.Forms.Panel Panel1;
        internal System.Windows.Forms.Button cmdRemove;
        internal System.Windows.Forms.Button cmdAdd;
    }
}
