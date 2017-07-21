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
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.btnTestOne = new System.Windows.Forms.Button();
            this.btnTestTwo = new System.Windows.Forms.Button();
            this.btnTestThree = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.panel1.Controls.Add(this.label1);
            this.panel1.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.panel1.Location = new System.Drawing.Point(119, 177);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(252, 44);
            this.panel1.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.Control;
            this.label1.Location = new System.Drawing.Point(89, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 26);
            this.label1.TabIndex = 2;
            this.label1.Text = "Tests";
            // 
            // btnTestOne
            // 
            this.btnTestOne.BackColor = System.Drawing.Color.DimGray;
            this.btnTestOne.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
            this.btnTestOne.ForeColor = System.Drawing.Color.White;
            this.btnTestOne.Location = new System.Drawing.Point(119, 227);
            this.btnTestOne.Name = "btnTestOne";
            this.btnTestOne.Size = new System.Drawing.Size(252, 39);
            this.btnTestOne.TabIndex = 2;
            this.btnTestOne.Text = "Crozzle Test 1";
            this.btnTestOne.UseVisualStyleBackColor = false;
            this.btnTestOne.Click += new System.EventHandler(this.btnTestOne_Click);
            this.btnTestOne.Leave += new System.EventHandler(this.btnTestOne_MouseEnter);
            this.btnTestOne.MouseEnter += new System.EventHandler(this.btnTestOne_MouseEnter);
            this.btnTestOne.MouseLeave += new System.EventHandler(this.btnTestOne_MouseLeave);
            // 
            // btnTestTwo
            // 
            this.btnTestTwo.BackColor = System.Drawing.Color.DimGray;
            this.btnTestTwo.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
            this.btnTestTwo.ForeColor = System.Drawing.Color.White;
            this.btnTestTwo.Location = new System.Drawing.Point(119, 269);
            this.btnTestTwo.Name = "btnTestTwo";
            this.btnTestTwo.Size = new System.Drawing.Size(252, 39);
            this.btnTestTwo.TabIndex = 3;
            this.btnTestTwo.Text = "Crozzle Test 2";
            this.btnTestTwo.UseVisualStyleBackColor = false;
            this.btnTestTwo.MouseEnter += new System.EventHandler(this.btnTestTwo_MouseEnter);
            this.btnTestTwo.MouseLeave += new System.EventHandler(this.btnTestTwo_MouseLeave);
            // 
            // btnTestThree
            // 
            this.btnTestThree.BackColor = System.Drawing.Color.DimGray;
            this.btnTestThree.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
            this.btnTestThree.ForeColor = System.Drawing.Color.White;
            this.btnTestThree.Location = new System.Drawing.Point(119, 311);
            this.btnTestThree.Name = "btnTestThree";
            this.btnTestThree.Size = new System.Drawing.Size(252, 39);
            this.btnTestThree.TabIndex = 4;
            this.btnTestThree.Text = "Crozzle Test 3";
            this.btnTestThree.UseVisualStyleBackColor = false;
            this.btnTestThree.MouseEnter += new System.EventHandler(this.btnTestThree_MouseEnter);
            this.btnTestThree.MouseLeave += new System.EventHandler(this.btnTestThree_MouseLeave);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.White;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(119, 111);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(252, 60);
            this.pictureBox1.TabIndex = 5;
            this.pictureBox1.TabStop = false;
            // 
            // FrmMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(484, 461);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.btnTestThree);
            this.Controls.Add(this.btnTestTwo);
            this.Controls.Add(this.btnTestOne);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "FrmMenu";
            this.Text = "Crozzle - Home";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnTestOne;
        private System.Windows.Forms.Button btnTestTwo;
        private System.Windows.Forms.Button btnTestThree;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}

