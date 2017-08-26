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
        //create a panel for the grid
        DataGridView crozzlePanel = new DataGridView();

        public FrmMenu()
        {
            InitializeComponent();

            //clear the log file when onload
            string fPath = Directory.GetCurrentDirectory();
            string filname = @"log.txt";//Convert.ToString(htConfig["LOGFILE_NAME"]);
            FileStream fS = File.Open(fPath + '\\' + filname, FileMode.Open);
            fS.SetLength(0);
            fS.Close();
        }

        //open a file dialog to retrieve file
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
            string configPath = Path.GetDirectoryName(cPath);

            //create crozzletest object
            CrozzleTest test = new CrozzleTest();
            test.CreateCrozzleTest(cPath, test);
            
            //error testing
            if(test.IsValid == false)
            {               
                invtxt1.Text = "Crozzle files are invalid";

                //read log file and display in textbox
                string fPath = Directory.GetCurrentDirectory();
                string filname = @"log.txt";//Convert.ToString(htConfig["LOGFILE_NAME"]);
                var pathToFile = fPath + '\\' + filname;
                var file = File.ReadAllLines(pathToFile);

                foreach (var line in file)
                {
                    errTxt.Text += line.ToString() + "\r\n";
                }


            }            
            else
            {
                test.TestCrozzle(test, configPath);

                if (test.IsCrozzleValid == false)
                {
                    invtxt2.Text = "Crozzle test file is invalid";
                    string fPath = Directory.GetCurrentDirectory();
                    string filname = @"log.txt";//Convert.ToString(htConfig["LOGFILE_NAME"]);
                    var pathToFile = fPath + '\\' + filname;
                    var file = File.ReadAllLines(pathToFile);

                    foreach (var line in file)
                    {
                        errTxt.Text += line.ToString() + "\r\n";
                    }
                }
                else
                {
                    //if all files are valid display the grid
                    invtxt1.Text = "All Crozzle files are valid";
                    crozzlePanel.Dock = DockStyle.Fill;
                    crozzlePanel.ScrollBars = ScrollBars.None;
                    crozzlePanel.BackgroundColor = Color.Black;
                    crozzlePanel.DefaultCellStyle.BackColor = Color.Black;
                    //crozzlePanel.DefaultCellStyle.ForeColor = Color.Black;
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

                    //get the points for each letter
                    List<string> letters = new List<string>();
                    foreach (var r in test.RowData)
                    {
                        //int colNo = r.Column - 1;
                        for (int colNo = r.Column - 1; colNo <= (r.Column - 1) + (r.Name.Length - 1); colNo++)
                        {
                            foreach (var c in test.ColumnData)
                            {
                                for (int rowNo = c.Row - 1; rowNo <= (c.Row - 1) + (c.Name.Length - 1); rowNo++)
                                {
                                    if (colNo == c.Column - 1 && r.Row - 1 == rowNo)
                                    {
                                        string lett = Convert.ToString(crozzlePanel.Rows[rowNo].Cells[colNo].Value);
                                        letters.Add(lett);

                                    }


                                }


                            }
                        }

                    }


                    //calculate the score
                    var score = 0;
                    foreach (var ltr in letters)
                    {
                        if (test.IpTable.ContainsKey(ltr))
                        {
                            int s = Convert.ToInt32(test.IpTable[ltr]);
                            score = score + s;
                        }
                    }

                    errTxt.Text = "Score: " + score;
                }
                
            }

            
            
        }

        //format a cell
        public void GetCell(int row, int col, string letter)
        {
            DataGridViewCell cell = crozzlePanel[col, row];
            cell.ReadOnly = false;
            cell.Style.BackColor = Color.White;
            cell.Style.ForeColor = Color.Tomato;
            cell.Value = letter;
            
        }
    }
}
