namespace Crozzle_Project
{
    partial class FrmMenu
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMenu));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openCrozzleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.validateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.crozzleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.crozzleWebBrowser = new System.Windows.Forms.WebBrowser();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.errorWebBrowser = new System.Windows.Forms.WebBrowser();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.validateToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1181, 24);
            this.menuStrip1.TabIndex = 8;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openCrozzleToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            this.fileToolStripMenuItem.Click += new System.EventHandler(this.fileToolStripMenuItem_Click);
            // 
            // openCrozzleToolStripMenuItem
            // 
            this.openCrozzleToolStripMenuItem.Name = "openCrozzleToolStripMenuItem";
            this.openCrozzleToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.openCrozzleToolStripMenuItem.Text = "Open Crozzle";
            this.openCrozzleToolStripMenuItem.Click += new System.EventHandler(this.openCrozzleToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            // 
            // validateToolStripMenuItem
            // 
            this.validateToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.crozzleToolStripMenuItem});
            this.validateToolStripMenuItem.Name = "validateToolStripMenuItem";
            this.validateToolStripMenuItem.Size = new System.Drawing.Size(60, 20);
            this.validateToolStripMenuItem.Text = "Validate";
            this.validateToolStripMenuItem.Click += new System.EventHandler(this.validateToolStripMenuItem_Click);
            // 
            // crozzleToolStripMenuItem
            // 
            this.crozzleToolStripMenuItem.Name = "crozzleToolStripMenuItem";
            this.crozzleToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.crozzleToolStripMenuItem.Text = "Crozzle";
            this.crozzleToolStripMenuItem.Click += new System.EventHandler(this.crozzleToolStripMenuItem_Click_1);
            // 
            // crozzleWebBrowser
            // 
            this.crozzleWebBrowser.Location = new System.Drawing.Point(13, 91);
            this.crozzleWebBrowser.MinimumSize = new System.Drawing.Size(20, 20);
            this.crozzleWebBrowser.Name = "crozzleWebBrowser";
            this.crozzleWebBrowser.Size = new System.Drawing.Size(619, 414);
            this.crozzleWebBrowser.TabIndex = 9;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog1_FileOk);
            // 
            // errorWebBrowser
            // 
            this.errorWebBrowser.Location = new System.Drawing.Point(658, 91);
            this.errorWebBrowser.MinimumSize = new System.Drawing.Size(20, 20);
            this.errorWebBrowser.Name = "errorWebBrowser";
            this.errorWebBrowser.Size = new System.Drawing.Size(511, 414);
            this.errorWebBrowser.TabIndex = 10;
            // 
            // FrmMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(1181, 555);
            this.Controls.Add(this.errorWebBrowser);
            this.Controls.Add(this.crozzleWebBrowser);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FrmMenu";
            this.Text = "Crozzle - Home";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openCrozzleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem validateToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem crozzleToolStripMenuItem;
        private System.Windows.Forms.WebBrowser crozzleWebBrowser;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.WebBrowser errorWebBrowser;
    }
}

