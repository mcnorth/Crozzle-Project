using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;
using System.Collections;

namespace Crozzle_Project
{
    public partial class FrmMenu : Form
    {
        

        public FrmMenu()
        {
            InitializeComponent();
            
        }

        OpenFileDialog ofd = new OpenFileDialog();

        private void btnGetFile_Click(object sender, EventArgs e)
        {
            DialogResult result = ofd.ShowDialog();

            if(result == DialogResult.OK)
            {
                txtFile.Text = ofd.FileName;
            }
            else
            {
                txtFile.Text = "Try again";
            }
        }

        private void btnLoadFile_Click(object sender, EventArgs e)
        {
            string cPath = txtFile.Text;

            WordList words = new WordList();
            words.CreateWordlist(cPath, words);
            


            //Configuration config = new Configuration();
            //config.CreateConfigObj(cPath, config);
            //txtResult.Text = config.BgColourEmptyTd;

            //string wPath = 
            //CreateDataGridView = new DataGridView();
            //CreateDataGridView.Size = new Size(300, 542);
            //CreateDataGridView.Location = new Point(10, 12);
            //this.Controls.Add(CreateDataGridView);
        }



        private void btnLoadWords_Click(object sender, EventArgs e)
        {

        }
    }
}
