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
    public partial class CrozzlePage : Form
    {
        private DataGridView CreateDataGridView;

        public CrozzlePage()
        {
            InitializeComponent();
        }

        private void CrozzlePage_Load(object sender, EventArgs e)
        {
            CreateDataGridView = new DataGridView();
            CreateDataGridView.Size = new Size(300, 542);
            CreateDataGridView.Location = new Point(10, 12);
            this.Controls.Add(CreateDataGridView);

        }
}

}
