using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crozzle_Project
{
    class CreateLogFiles
    {
        private string formatLog;
        private string timeOfError;

        public CreateLogFiles()
        {
            //sLogFormat used to create log files format :
            // dd/mm/yyyy hh:mm:ss AM/PM ==> Log Message
            formatLog = DateTime.Now.ToShortDateString().ToString() + " " + DateTime.Now.ToLongTimeString().ToString() + " ==> ";

            //this variable used to create log filename format "
            //for example filename : ErrorLogYYYYMMDD
            string lfYear = DateTime.Now.Year.ToString();
            string lfMonth = DateTime.Now.Month.ToString();
            string lfDay = DateTime.Now.Day.ToString();
            timeOfError = lfYear + lfMonth + lfDay;
        }

        public void ErrorLog(string pName, string errMsg)
        {
            StreamWriter sw = new StreamWriter(pName + timeOfError, true);
            sw.WriteLine(formatLog + errMsg);
            sw.Flush();
            sw.Close();
        }
    }
}
