namespace DotCopter.Configurator
{
    partial class frmConfigurator
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.cbMulticopter = new System.Windows.Forms.ComboBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.pnlMulticopter = new System.Windows.Forms.Panel();
            this.lstSelectedHardware = new System.Windows.Forms.ListBox();
            this.gpSettings = new System.Windows.Forms.GroupBox();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadDefaultsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.pnlMulticopter.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.treeView1);
            this.groupBox1.Location = new System.Drawing.Point(12, 28);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(224, 377);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Hardware";
            // 
            // treeView1
            // 
            this.treeView1.Location = new System.Drawing.Point(6, 15);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(212, 352);
            this.treeView1.TabIndex = 1;
            this.treeView1.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.TreeView1ItemDrag);
            // 
            // cbMulticopter
            // 
            this.cbMulticopter.FormattingEnabled = true;
            this.cbMulticopter.Items.AddRange(new object[] {
            "QuadCopter",
            "TriCopter",
            "HexCopter",
            "OctoCopter"});
            this.cbMulticopter.Location = new System.Drawing.Point(304, 27);
            this.cbMulticopter.Name = "cbMulticopter";
            this.cbMulticopter.Size = new System.Drawing.Size(167, 21);
            this.cbMulticopter.TabIndex = 1;
            this.cbMulticopter.SelectedIndexChanged += new System.EventHandler(this.cbMulticopter_SelectedIndexChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.pnlMulticopter);
            this.groupBox2.Location = new System.Drawing.Point(267, 54);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(259, 329);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "MultiCopter";
            // 
            // pnlMulticopter
            // 
            this.pnlMulticopter.AllowDrop = true;
            this.pnlMulticopter.Controls.Add(this.lstSelectedHardware);
            this.pnlMulticopter.Location = new System.Drawing.Point(6, 19);
            this.pnlMulticopter.Name = "pnlMulticopter";
            this.pnlMulticopter.Size = new System.Drawing.Size(247, 304);
            this.pnlMulticopter.TabIndex = 0;
            this.pnlMulticopter.DragDrop += new System.Windows.Forms.DragEventHandler(this.PnlMulticopterDragDrop);
            this.pnlMulticopter.DragEnter += new System.Windows.Forms.DragEventHandler(this.MulticopterDragEnter);
            // 
            // lstSelectedHardware
            // 
            this.lstSelectedHardware.FormattingEnabled = true;
            this.lstSelectedHardware.Location = new System.Drawing.Point(13, 14);
            this.lstSelectedHardware.Name = "lstSelectedHardware";
            this.lstSelectedHardware.Size = new System.Drawing.Size(220, 277);
            this.lstSelectedHardware.TabIndex = 0;
            this.lstSelectedHardware.SelectedIndexChanged += new System.EventHandler(this.LstSelectedHardwareSelectedIndexChanged);
            // 
            // gpSettings
            // 
            this.gpSettings.Location = new System.Drawing.Point(532, 54);
            this.gpSettings.Name = "gpSettings";
            this.gpSettings.Size = new System.Drawing.Size(259, 329);
            this.gpSettings.TabIndex = 3;
            this.gpSettings.TabStop = false;
            this.gpSettings.Text = "Settings";
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(6, 19);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ReadOnly = true;
            this.richTextBox1.Size = new System.Drawing.Size(762, 154);
            this.richTextBox1.TabIndex = 4;
            this.richTextBox1.Text = "";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.richTextBox1);
            this.groupBox3.Location = new System.Drawing.Point(12, 395);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(774, 182);
            this.groupBox3.TabIndex = 4;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "XML";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(798, 24);
            this.menuStrip1.TabIndex = 5;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.loadDefaultsToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.openToolStripMenuItem.Text = "&Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.OpenToolStripMenuItemClick);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.saveToolStripMenuItem.Text = "&Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.SaveToolStripMenuItemClick);
            // 
            // loadDefaultsToolStripMenuItem
            // 
            this.loadDefaultsToolStripMenuItem.Name = "loadDefaultsToolStripMenuItem";
            this.loadDefaultsToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.loadDefaultsToolStripMenuItem.Text = "&Load Defaults";
            this.loadDefaultsToolStripMenuItem.Click += new System.EventHandler(this.LoadDefaultsToolStripMenuItemClick);
            // 
            // frmConfigurator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(798, 580);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.gpSettings);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.cbMulticopter);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "frmConfigurator";
            this.Text = "DotCopter Configuration";
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.pnlMulticopter.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.ComboBox cbMulticopter;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Panel pnlMulticopter;
        private System.Windows.Forms.GroupBox gpSettings;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadDefaultsToolStripMenuItem;
        private System.Windows.Forms.ListBox lstSelectedHardware;
    }
}

