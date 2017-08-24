using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crozzle_Project
{
    class ErrorLog
    {
        private string FILE_NAME;
        private string LINE;
        private string DESCRIPTION;

        public string File_Name
        {
            get { return FILE_NAME; }
            set { FILE_NAME = value; }
        }

        public string Line
        {
            get { return LINE; }
            set { LINE = value; }
        }

        public string Description
        {
            get { return DESCRIPTION; }
            set { DESCRIPTION = value; }
        }

        public ErrorLog(string fileName, string line, string desc)
        {
            FILE_NAME = fileName;
            LINE = line;
            DESCRIPTION = desc;
        }
    }
}
