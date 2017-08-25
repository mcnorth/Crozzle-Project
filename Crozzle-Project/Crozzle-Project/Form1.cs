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
        DataGridView crozzlePanel = new DataGridView();

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
                invtxt1.Text = "Crozzle files are invalid";

                test.TestCrozzle(test, configPath);

                if (test.IsCrozzleValid == false)
                {                   
                    invtxt2.Text = "Crozzle test file is invalid";
                }
            }

            if(test.IsValid == true)
            {
                test.TestCrozzle(test, configPath);

                if (test.IsCrozzleValid == false)
                {                   
                    invtxt2.Text = "Crozzle test file is invalid";
                }

                if (test.IsCrozzleValid == true)
                {
                    invtxt1.Text = "All Crozzle files are valid";
                    crozzlePanel.Dock = DockStyle.Fill;
                    crozzlePanel.ScrollBars = ScrollBars.None;
                    crozzlePanel.BackgroundColor = Color.Black;
                    crozzlePanel.DefaultCellStyle.BackColor = Color.Black;
                    crozzlePanel.DefaultCellStyle.ForeColor = Color.Black;
                    crozzlePanel.Font = new Font("Arial", 12);
                    crozzlePanel.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    crozzlePanel.Enabled = false;
                    crozzlePanel.RowsDefaultCellStyle.SelectionBackColor = Color.Black;

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

                    foreach (var obj in test.RowData)
                    {
                        int beginCol = obj.Column - 1;
                        int beginRow = obj.Row - 1;
                        char[] name = obj.Name.ToCharArray();

                        for (int j = 0; j < name.Length; j++)
                        {
                            GetCell(beginRow, beginCol + j, name[j].ToString());
                        }
                    }

                    foreach (var obj in test.ColumnData)
                    {
                        int beginCol = obj.Column - 1;
                        int beginRow = obj.Row - 1;
                        char[] name = obj.Name.ToCharArray();

                        for (int j = 0; j < name.Length; j++)
                        {
                            GetCell(beginRow + j, beginCol, name[j].ToString());
                        }
                    }
                }

            }

            
            string fPath = Directory.GetCurrentDirectory();
            string filname = @"log.txt";//Convert.ToString(htConfig["LOGFILE_NAME"]);
            var pathToFile = fPath + '\\' + filname;
            var file = File.ReadAllLines(pathToFile);
            
            foreach (var line in file)
            {
                errTxt.Text += line.ToString() + "\r\n";
            }
            
        }

        public void GetCell(int row, int col, string letter)
        {
            DataGridViewCell cell = crozzlePanel[col, row];
            cell.ReadOnly = false;
            cell.Style.BackColor = Color.White;           
            cell.Value = letter;
            
        }
    }
}
