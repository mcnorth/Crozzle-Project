using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Crozzle_Project
{
    public partial class FrmMenu : Form
    {
        

        public FrmMenu()
        {
            InitializeComponent();
            
        }

        private void btnTestOne_MouseEnter(object sender, EventArgs e)
        {
            btnTestOne.BackColor = Color.Black;
        }

        private void btnTestOne_MouseLeave(object sender, EventArgs e)
        {
            btnTestOne.BackColor = Color.DimGray;
        }

        private void btnTestTwo_MouseEnter(object sender, EventArgs e)
        {
            btnTestTwo.BackColor = Color.Black;
        }

        private void btnTestTwo_MouseLeave(object sender, EventArgs e)
        {
            btnTestTwo.BackColor = Color.DimGray;
        }

        private void btnTestThree_MouseEnter(object sender, EventArgs e)
        {
            btnTestThree.BackColor = Color.Black;
        }

        private void btnTestThree_MouseLeave(object sender, EventArgs e)
        {
            btnTestThree.BackColor = Color.DimGray;
        }



        private void btnTestOne_Click(object sender, EventArgs e)
        {

            CrozzlePage crozzleTestOne = new CrozzlePage();
            crozzleTestOne.Show();
        }
    }
}
