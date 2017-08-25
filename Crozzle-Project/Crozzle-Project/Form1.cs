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
using System.Web.UI.HtmlControls;
using System.Web.UI;

namespace Crozzle_Project
{
    public partial class FrmMenu : Form
    {
        

        public FrmMenu()
        {
            InitializeComponent();
            string fPath = Directory.GetCurrentDirectory();
            string filname = @"log.txt";//Convert.ToString(htConfig["LOGFILE_NAME"]);
            FileStream fS = File.Open(fPath + '\\' + filname, FileMode.Open);
            fS.SetLength(0);
            fS.Close();

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
            int x = 10;
            int y = 10;
            

            string cPath = txtFile.Text;
            string configPath = Path.GetDirectoryName(cPath);

            CrozzleTest test = new CrozzleTest();
            test.CreateCrozzleTest(cPath, test);
            
            if(test.IsValid == false)
            {
                //Label lbl = new Label();
                //lbl.AutoSize = true;
                //lbl.Location = new Point(x, y);
                //lbl.Font = new Font("Arial", 12, FontStyle.Bold);
                //lbl.Text = "Crozzle file are invalid";
                //errorPanel.Controls.Add(lbl);

                invtxt1.Text = "Crozzle files are invalid";

                test.TestCrozzle(test, configPath);

                if (test.IsCrozzleValid == false)
                {
                    //Label lab = new Label();
                    //lab.AutoSize = true;
                    //lab.Location = new Point(x + 20, y);
                    //lab.Font = new Font("Arial", 12, FontStyle.Bold);
                    //lab.Text = "Crozzle test file are invalid";
                    //errorPanel.Controls.Add(lab);
                    invtxt2.Text = "Crozzle test file is invalid";

                }


            }

            if(test.IsValid == true)
            {

                test.TestCrozzle(test, configPath);

                if (test.IsCrozzleValid == false)
                {
                    //Label lbl = new Label();
                    //lbl.AutoSize = true;                   
                    //lbl.Text = "Crozzle test file are invalid";
                    //errorPanel.Controls.Add(lbl);
                    invtxt2.Text = "Crozzle test file is invalid";
                }

                if (test.IsCrozzleValid == true)
                {
                    invtxt1.Text = "All Crozzle files are valid";
                    DataGridView crozzlePanel = new DataGridView();
                    crozzlePanel.Dock = DockStyle.Fill;
                    crozzlePanel.ScrollBars = ScrollBars.None;
                    crozzlePanel.BackgroundColor = Color.Black;
                    crozzlePanel.DefaultCellStyle.BackColor = Color.Black;

                    for (int i = 0; i < test.GridColumns; i++)
                    {
                        crozzlePanel.Columns.Add("", "");
                    }
                    for (int j = 0; j < test.GridRows; j++)
                    {
                        crozzlePanel.Rows.Add();
                    }

                    foreach (DataGridViewColumn col in crozzlePanel.Columns)
                    {
                        col.Width = boardPanel.Width / test.GridColumns;
                    }

                    foreach (DataGridViewRow row in crozzlePanel.Rows)
                    {
                        row.Height = boardPanel.Height / test.GridRows;
                    }
                    crozzlePanel.ColumnHeadersVisible = false;
                    crozzlePanel.RowHeadersVisible = false;
                    boardPanel.Controls.Add(crozzlePanel);
                }

            }

            
            string fPath = Directory.GetCurrentDirectory();
            string filname = @"log.txt";//Convert.ToString(htConfig["LOGFILE_NAME"]);
            var pathToFile = fPath + '\\' + filname;
            var file = File.ReadAllLines(pathToFile);
            //Label errorLbl = new Label();
            //errorLbl.AutoSize = true;
            //errorLbl.Location = new Point(x + 40, y);
            //errorLbl.Font = new Font("Arial", 12, FontStyle.Bold);
            //errorLbl.Text = file;
            //errorPanel.Controls.Add(errorLbl);
            foreach (var line in file)
            {
                //errTxt.Text = string.Join(" ", line);
                errTxt.Text += line.ToString() + "\r\n";
            }
            


        }



        private void btnLoadWords_Click(object sender, EventArgs e)
        {

        }
    }
}
