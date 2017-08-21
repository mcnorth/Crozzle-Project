using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crozzle_Project
{
    class ColumnData
    {

        private int COLUMN;
        private string NAME;
        private int ROW;

        public int Row
        {
            get { return ROW; }
            set { ROW = value; }
        }

        public string Name
        {
            get { return NAME; }
            set { NAME = value; }
        }

        public int Column
        {
            get { return COLUMN; }
            set { COLUMN = value; }
        }
    }
}
