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
            string configPath = Path.GetDirectoryName(cPath);

            CrozzleTest test = new CrozzleTest();
            test.CreateCrozzleTest(cPath, test);
            test.TestCrozzle(test, configPath);

            //HtmlGenericControl table = new HtmlGenericControl("TABLE");
            //HtmlTable table = new HtmlTable();
            //for (int i = 0; i < test.GridRows; i++)
            //{
            //    HtmlTableRow r = new HtmlTableRow();
            //    table.Rows.Add(r);

            //    for (int j = 0; j < test.GridColumns; j++)
            //    {
            //        HtmlTableCell c = new HtmlTableCell();
            //        r.Cells.Add(c);
            //    }
            //}
            //panel1.Controls.Add(new LiteralControl(table));



            DataGridView crozzlePanel = new DataGridView();
            crozzlePanel.Location = new System.Drawing.Point(33, 22);
            crozzlePanel.ColumnCount = test.GridColumns;
            crozzlePanel.RowCount = test.GridRows;
            crozzlePanel.Height = (crozzlePanel.RowCount + 1) * 22;
            crozzlePanel.Width = (crozzlePanel.ColumnCount + 1) * 22;
            for (int i = 0; i < crozzlePanel.ColumnCount; i++)
            {
                crozzlePanel.Columns[i].Width = 22;
            }
            crozzlePanel.ColumnHeadersVisible = false;
            crozzlePanel.RowHeadersVisible = false;
            panel1.Controls.Add(crozzlePanel);

            //TableLayoutPanel crozzlePanel = new TableLayoutPanel();
            //crozzlePanel.ColumnCount = test.GridColumns;
            //crozzlePanel.RowCount = test.GridRows;
            //crozzlePanel.Location = new System.Drawing.Point(3, 3);
            //crozzlePanel.Size = new System.Drawing.Size(200, 100);

            //panel1.Controls.Add(crozzlePanel);

            //WordList words = new WordList();
            //words.CreateWordlist(cPath, words);



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
