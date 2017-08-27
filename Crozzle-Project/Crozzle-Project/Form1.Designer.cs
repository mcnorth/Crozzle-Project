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
            this.boardPanel = new System.Windows.Forms.Panel();
            this.btnGetFile = new System.Windows.Forms.Button();
            this.btnLoadFile = new System.Windows.Forms.Button();
            this.txtFile = new System.Windows.Forms.TextBox();
            this.errorPanel = new System.Windows.Forms.Panel();
            this.errTxt = new System.Windows.Forms.RichTextBox();
            this.invtxt2 = new System.Windows.Forms.TextBox();
            this.invtxt1 = new System.Windows.Forms.TextBox();
            this.errorPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // boardPanel
            // 
            this.boardPanel.BackColor = System.Drawing.Color.White;
            this.boardPanel.Location = new System.Drawing.Point(12, 82);
            this.boardPanel.Name = "boardPanel";
            this.boardPanel.Size = new System.Drawing.Size(620, 461);
            this.boardPanel.TabIndex = 2;
            // 
            // btnGetFile
            // 
            this.btnGetFile.Location = new System.Drawing.Point(12, 43);
            this.btnGetFile.Name = "btnGetFile";
            this.btnGetFile.Size = new System.Drawing.Size(140, 33);
            this.btnGetFile.TabIndex = 3;
            this.btnGetFile.Text = "Get File";
            this.btnGetFile.UseVisualStyleBackColor = true;
            this.btnGetFile.Click += new System.EventHandler(this.btnGetFile_Click);
            // 
            // btnLoadFile
            // 
            this.btnLoadFile.Location = new System.Drawing.Point(158, 43);
            this.btnLoadFile.Name = "btnLoadFile";
            this.btnLoadFile.Size = new System.Drawing.Size(140, 33);
            this.btnLoadFile.TabIndex = 4;
            this.btnLoadFile.Text = "Load File";
            this.btnLoadFile.UseVisualStyleBackColor = true;
            this.btnLoadFile.Click += new System.EventHandler(this.btnLoadFile_Click);
            // 
            // txtFile
            // 
            this.txtFile.Location = new System.Drawing.Point(12, 13);
            this.txtFile.Name = "txtFile";
            this.txtFile.Size = new System.Drawing.Size(489, 20);
            this.txtFile.TabIndex = 5;
            // 
            // errorPanel
            // 
            this.errorPanel.Controls.Add(this.errTxt);
            this.errorPanel.Controls.Add(this.invtxt2);
            this.errorPanel.Controls.Add(this.invtxt1);
            this.errorPanel.Location = new System.Drawing.Point(654, 82);
            this.errorPanel.Name = "errorPanel";
            this.errorPanel.Padding = new System.Windows.Forms.Padding(5);
            this.errorPanel.Size = new System.Drawing.Size(515, 461);
            this.errorPanel.TabIndex = 7;
            // 
            // errTxt
            // 
            this.errTxt.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.errTxt.Location = new System.Drawing.Point(9, 92);
            this.errTxt.Name = "errTxt";
            this.errTxt.Size = new System.Drawing.Size(498, 361);
            this.errTxt.TabIndex = 3;
            this.errTxt.Text = "";
            // 
            // invtxt2
            // 
            this.invtxt2.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.invtxt2.Location = new System.Drawing.Point(8, 51);
            this.invtxt2.Name = "invtxt2";
            this.invtxt2.Size = new System.Drawing.Size(498, 32);
            this.invtxt2.TabIndex = 1;
            // 
            // invtxt1
            // 
            this.invtxt1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.invtxt1.Location = new System.Drawing.Point(9, 9);
            this.invtxt1.Name = "invtxt1";
            this.invtxt1.Size = new System.Drawing.Size(498, 32);
            this.invtxt1.TabIndex = 0;
            // 
            // FrmMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(1181, 555);
            this.Controls.Add(this.errorPanel);
            this.Controls.Add(this.txtFile);
            this.Controls.Add(this.btnLoadFile);
            this.Controls.Add(this.btnGetFile);
            this.Controls.Add(this.boardPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "FrmMenu";
            this.Text = "Crozzle - Home";
            this.errorPanel.ResumeLayout(false);
            this.errorPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Panel boardPanel;
        private System.Windows.Forms.Button btnGetFile;
        private System.Windows.Forms.Button btnLoadFile;
        private System.Windows.Forms.TextBox txtFile;
        private System.Windows.Forms.Panel errorPanel;
        private System.Windows.Forms.TextBox invtxt1;
        private System.Windows.Forms.TextBox invtxt2;
        private System.Windows.Forms.RichTextBox errTxt;
    }
}

