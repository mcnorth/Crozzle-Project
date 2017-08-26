using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;
using System.Collections;

namespace Crozzle_Project
{
    class CrozzleTest
    {

        private string CONFIGURATION_FILE;
        private string WORDLIST_FILE;
        private int GRID_ROWS;
        private int GRID_COLUMNS;
        private List<RowData> ROW_DATA;
        private List<ColumnData> COLUMN_DATA;
        private bool ISVALID;
        private bool ISCROZZLEVALID;
        private Hashtable IP_TABLE;
        private Hashtable NIP_TABLE;

        public Hashtable IpTable
        {
            get { return IP_TABLE; }
            set { IP_TABLE = value; }
        }

        public Hashtable NIpTable
        {
            get { return NIP_TABLE; }
            set { NIP_TABLE = value; }
        }

        public bool IsValid
        {
            get { return ISVALID; }
            set { ISVALID = value; }
        }

        public bool IsCrozzleValid
        {
            get { return ISCROZZLEVALID; }
            set { ISCROZZLEVALID = value; }
        }

        public List<RowData> RowData
        {
            get { return ROW_DATA; }
            set { ROW_DATA = value; }
        }

        public List<ColumnData> ColumnData
        {
            get { return COLUMN_DATA; }
            set { COLUMN_DATA = value; }
        }


        public string ConfigurationFile
        {
            get { return CONFIGURATION_FILE; }
            set { CONFIGURATION_FILE = value; }
        }

        public string WordlistFile
        {
            get { return WORDLIST_FILE; }
            set { WORDLIST_FILE = value; }
        }

        public int GridRows
        {
            get { return GRID_ROWS; }
            set { GRID_ROWS = value; }
        }

        public int GridColumns
        {
            get { return GRID_COLUMNS; }
            set { GRID_COLUMNS = value; }
        }

        public CrozzleTest()
        {

        }

        
        //get teh number of rows and columns for the grid
        public Hashtable GetGrid(List<string> data)
        {
            Hashtable result = new Hashtable();
            List<string> temp = new List<string>();

            foreach (var d in data)
            {

                if (d.Contains(","))
                {
                    continue;
                }
                else if (d.Contains("FILE"))
                {
                    continue;
                }
                else
                {
                    temp.Add(d);
                }
            }

            foreach (var res in temp)
            {
                //string a = res.Replace(@".\\", @".\");
                int index = res.IndexOf("=");
                var key = res.Substring(0, index);
                var val = res.Substring(index + 1);
                result.Add(key, val);
            }



            return result; 
        }

        //get teh data for the rows and error check and log
        public Tuple<List<ColumnData>, string> GetColumnData(string path)
        {
            var pathToFile = path;
            var file = File.ReadAllLines(pathToFile);
            List<ColumnData> res = new List<ColumnData>();
            List<string> temp = new List<string>();
            List<string> temp2 = new List<string>();
            List<string> temp3 = new List<string>();
            List<string> temp4 = new List<string>();
            List<string> temp5 = new List<string>();
            List<string> temp6 = new List<string>();
            List<string> temp7 = new List<string>();
            List<ErrorLog> columnDataErrors = new List<ErrorLog>();
            string result;
            var myRegex = new Regex(@"\w+=\d+,\w+,\d+");
            var reg = new Regex(@"// The vertical rows containing words.");
            var reg2 = new Regex(@"COLUMN=\d+,\w+,\d+");
            var reg3 = new Regex(@"\.");


            foreach (var line in file)
            {
                temp.Add(line);
            }
            foreach (var line in temp.ToArray())
            {
                if (reg.IsMatch(line))
                {
                    break;
                }

                temp.Remove(line);
            }

            foreach (var line in temp.ToArray())
            {
                if (line.StartsWith("//") || line == String.Empty)
                {
                    continue;
                }
                else
                {

                    temp2.Add(line);
                }
            }
         
            if (temp2.Count != temp2.Distinct().Count())
            {
                string name = new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileName();
                string linerr = "ColumnData";
                string des = "Wordlist contains duplicates";
                ErrorLog err = new ErrorLog(name, linerr, des);
                columnDataErrors.Add(err);
            }

            foreach (var line in temp2)
            {
                if (reg2.IsMatch(line))
                {
                    temp3.Add(line);
                }
                else
                {
                    string name = new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileName();
                    string linerr = line;
                    string des = "Column Data is invalid";
                    ErrorLog err = new ErrorLog(name, linerr, des);
                    columnDataErrors.Add(err);
                }

            }

            foreach (var line in temp3)
            {
                if (reg3.IsMatch(line))
                {
                    string name = new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileName();
                    string linerr = line;
                    string des = "Column Data is invalid";
                    ErrorLog err = new ErrorLog(name, linerr, des);
                    columnDataErrors.Add(err);
                }
                else
                {
                    temp4.Add(line);
                }


            }

            foreach (var line in temp4)
            {
                if (line.Contains("="))
                {
                    int index = line.IndexOf("=");
                    result = line.Substring(index + 1);
                    temp5.Add(result);
                }
            }

            foreach (var a in temp5)
            {
                string[] y = a.Split(',');

                ColumnData ob = new ColumnData();
                ob.Column = Convert.ToInt32(y[0]);
                ob.Name = Convert.ToString(y[1]);
                ob.Row = Convert.ToInt32(y[2]);
                res.Add(ob);

            }

            string fPath = Directory.GetCurrentDirectory();
            string filname = @"log.txt";//Convert.ToString(htConfig["LOGFILE_NAME"]);
            StreamWriter wtr = new StreamWriter(fPath + '\\' + filname, append: true);

            foreach (var e in columnDataErrors)
            {
                wtr.WriteLine("File Name: " + e.File_Name);
                wtr.WriteLine("Line: " + e.Line);
                wtr.WriteLine("Description: " + e.Description);

            }
            wtr.Close();

            string invalid = "Invalid";
            string valid = "Valid";
            Tuple<List<ColumnData>, string> tuple;

            if (columnDataErrors.Count > 0)
            {

                tuple = new Tuple<List<ColumnData>, string>(res, invalid);
            }
            else
            {
                tuple = new Tuple<List<ColumnData>, string>(res, valid);
            }
            return tuple;
            

        }
        
        //get the data for the columns and error check and log
        public Tuple<List<RowData>, string> GetRowData(string path)
        {
            var pathToFile = path;
            var file = File.ReadAllLines(pathToFile);
            List<RowData> res = new List<RowData>();
            List<string> temp = new List<string>();
            List<string> temp2 = new List<string>();
            List<string> temp3 = new List<string>();
            List<string> temp4 = new List<string>();
            List<string> temp5 = new List<string>();
            List<string> temp6 = new List<string>();
            List<ErrorLog> rowDataErrors = new List<ErrorLog>();
            string result;
            var myRegex = new Regex(@"\w+=\d+,\w+,\d+");
            var reg = new Regex(@"// The horizontal rows containing words.");
            var reg2 = new Regex(@"// The vertical rows containing words.");
            var reg3 = new Regex(@"ROW=\d+,\w+,\d+");
            

            foreach (var line in file)
            {
                temp.Add(line);
            }
            foreach (var line in temp.ToArray())
            {              
                if (reg.IsMatch(line))
                {
                    break;
                }

                temp.Remove(line);
            }

            foreach (var line in temp.ToArray())
            {
                if (reg2.IsMatch(line))
                {
                    break;
                }

                temp2.Add(line);
            }

            foreach (var line in temp2)
            {
                if (line.StartsWith("//") || line == String.Empty)
                {
                    continue;
                }
                else
                {

                    temp3.Add(line);
                }
            }

            foreach (var line in temp3)
            {
                if (line.Contains(" ") && line.Contains("//"))
                {
                    int index = line.IndexOf(" ");
                    result = line.Substring(0, index);
                    temp4.Add(result);
                }
                else
                {
                    temp4.Add(line);
                }
            }

            if (temp4.Count != temp4.Distinct().Count())
            {
                string name = new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileName();
                string linerr = "RowData";
                string des = "Wordlist contains duplicates";
                ErrorLog err = new ErrorLog(name, linerr, des);
                rowDataErrors.Add(err);
            }

            foreach (var line in temp4)
            {
                if (reg3.IsMatch(line))
                {
                    temp5.Add(line);
                }
                else
                {
                    string name = new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileName();
                    string linerr = line;
                    string des = "Row Data is invalid";
                    ErrorLog err = new ErrorLog(name, linerr, des);
                    rowDataErrors.Add(err);
                }
                
            }

            foreach(var line in temp5)
            {
                if (line.Contains("="))
                {
                    int index = line.IndexOf("=");
                    result = line.Substring(index + 1);
                    temp6.Add(result);
                }
            }

            foreach (var a in temp6)
            {
                string[] y = a.Split(',');

                RowData ob = new RowData();
                ob.Row = Convert.ToInt32(y[0]);
                ob.Name = Convert.ToString(y[1]);
                ob.Column = Convert.ToInt32(y[2]);
                res.Add(ob);

            }

            string fPath = Directory.GetCurrentDirectory();
            string filname = @"log.txt";//Convert.ToString(htConfig["LOGFILE_NAME"]);
            StreamWriter wtr = new StreamWriter(fPath + '\\' + filname, append: true);

            foreach (var e in rowDataErrors)
            {
                wtr.WriteLine("File Name: " + e.File_Name);
                wtr.WriteLine("Line: " + e.Line);
                wtr.WriteLine("Description: " + e.Description);

            }
            wtr.Close();

            string invalid = "Invalid";
            string valid = "Valid";
            Tuple<List<RowData>, string> tuple;

            if (rowDataErrors.Count > 0)
            {

                tuple = new Tuple<List<RowData>, string>(res, invalid);
            }
            else
            {
                tuple = new Tuple<List<RowData>, string>(res, valid);
            }
            return tuple;
            
        }       

        //get the names of the files associated with the test file
        public Tuple<Hashtable, string> GetFileNames(List<string> loc)
        {
            //string pat = "FILE";
            Hashtable result = new Hashtable();
            List<string> temp = new List<string>();
            List<string> t = new List<string>();
            List<string> cor = new List<string>();
            List<ErrorLog> crozzleErrors = new List<ErrorLog>();
            string invalid = "Invalid";
            string valid = "Valid";

            foreach (var line in loc)
            {
                if(line.Contains("FILE"))
                {                    //
                    temp.Add(line);  
                }
                
            }

            foreach(var s in temp)
            {
                if(s.Contains('"'))
                {
                    string str = s.Replace("\"", "");
                    t.Add(str);
                }
            }          

            foreach (var res in t)
            {
                
                int index = res.IndexOf("=");
                var key = res.Substring(0, index);
                var val = res.Substring(index + 1);
                result.Add(key, val);
            }

            

            
            Tuple<Hashtable, string> tuple;

            if (result.Count > 0)
            {

                tuple = new Tuple<Hashtable, string>(result, valid);
            }
            else
            {
                tuple = new Tuple<Hashtable, string>(result, invalid);
                string fPath = Directory.GetCurrentDirectory();
                string filname = @"log.txt";//Convert.ToString(htConfig["LOGFILE_NAME"]);
                StreamWriter wtr = new StreamWriter(fPath + '\\' + filname, append: true);

                wtr.WriteLine("File Name: CrozzleTest.cs" );
                wtr.WriteLine("Line: File name");
                wtr.WriteLine("Description: No configuration file or wordlist file exist");

                wtr.Close();
            }
            return tuple;

        }

        //get the file error check and log
        public List<string> GetFile(string path)
        {            
            var pathToFile = path;
            var file = File.ReadAllLines(pathToFile);            
            List<string> testFile1 = new List<string>();
            List<string> testFile2 = new List<string>();
            List<string> testFile3 = new List<string>();
            string result;
            Hashtable fileLoc = new Hashtable();

            foreach (var line in file)
            {
                if (line.StartsWith("//") || line == String.Empty)
                {
                    continue;
                }                
                else
                {
                    
                    testFile1.Add(line);
                }

            }

            foreach (var l in testFile1)
            {
                if (l.Contains(" ") && l.Contains("//"))
                {
                    int index = l.IndexOf(" ");
                    result = l.Substring(0, index);
                    testFile2.Add(result);
                }
                else
                {
                    testFile2.Add(l);
                }
            }

            return testFile2;
        }

        public CrozzleTest CreateCrozzleTest(string path, CrozzleTest obj)
        {

            List<string> words = GetFile(path);
            Tuple<Hashtable, string> fileLoc = GetFileNames(words);
            Tuple<List<RowData>, string> rData = GetRowData(path);
            Tuple<List<ColumnData>, string> cData = GetColumnData(path);
            Hashtable grid = GetGrid(words);
            CrozzleTestObj(obj, fileLoc, rData, cData, grid);

            return obj;
        }

        public CrozzleTest CrozzleTestObj(CrozzleTest obj, Tuple<Hashtable, string> filesTuple, Tuple<List<RowData>, string> rowDataTuple, Tuple<List<ColumnData>, string> columnDataTuple, Hashtable gridLayout)
        {
            Hashtable files = filesTuple.Item1;
            string isvalid = filesTuple.Item2;
            List<RowData> rowData = rowDataTuple.Item1;
            string isvalid2 = rowDataTuple.Item2;
            List<ColumnData> columnData = columnDataTuple.Item1;
            string isvalid3 = columnDataTuple.Item2;

            if(isvalid == "Invalid" || isvalid2 == "Invalid" || isvalid3 == "Invalid")
            {
                obj.IsValid = false;
                obj.ConfigurationFile = Convert.ToString(files["CONFIGURATION_FILE"]);
                obj.WordlistFile = Convert.ToString(files["WORDLIST_FILE"]);
                obj.GridRows = Convert.ToInt32(gridLayout["ROWS"]);
                obj.GridColumns = Convert.ToInt32(gridLayout["COLUMNS"]);
                obj.RowData = rowData;
                obj.ColumnData = columnData;
            }
            else
            {
                obj.IsValid = true;
                obj.ConfigurationFile = Convert.ToString(files["CONFIGURATION_FILE"]);
                obj.WordlistFile = Convert.ToString(files["WORDLIST_FILE"]);
                obj.GridRows = Convert.ToInt32(gridLayout["ROWS"]);
                obj.GridColumns = Convert.ToInt32(gridLayout["COLUMNS"]);
                obj.RowData = rowData;
                obj.ColumnData = columnData;
            }
            
            return obj;
        }

        //test the crozzle to see if it is valid
        public CrozzleTest TestCrozzle(CrozzleTest obj, string rPath)
        {
            string cFile = rPath + "\\" + Convert.ToString(obj.ConfigurationFile);
            Configuration config = new Configuration();           
            config.CreateConfigObj(cFile, config);
            List<ErrorLog> testErrors = new List<ErrorLog>();
            
        
            obj.IpTable = config.IntersectingPointsPerLetter;
            obj.NIpTable = config.NonIntersectingPointsPerLetter;

            string wFile = rPath + "\\" + Convert.ToString(obj.WordlistFile);
            WordList wList = new WordList();
            wList.CreateWordlist(wFile, wList);

            int countErrors = 0;
            if (config.IsValid == true && wList.IsValid == true)
            {
                if (wList.Count < config.MinimumNumberOfUniqueWords || wList.Count > config.MaximumNumberOfUniqueWords)
                {
                    string name = new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileName();
                    string linerr = "Wordlist is greater or lesser than Number of Unique Words";
                    string des = "Test Data is invalid";
                    ErrorLog err = new ErrorLog(name, linerr, des);
                    testErrors.Add(err);
                    countErrors++;
                    
                }
                

                if (obj.GridRows < config.MinimumNumberOfRows || obj.GridRows > config.MaximumNumberOfRows)
                {
                    string name = new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileName();
                    string linerr = "Grid Rows is greater or lesser than Number of Rows";
                    string des = "Test Data is invalid";
                    ErrorLog err = new ErrorLog(name, linerr, des);
                    testErrors.Add(err);
                    countErrors++;

                }
                

                if (obj.GridColumns < config.MinimumNumberOfColumns || obj.GridColumns > config.MaximumNumberOfColumns)
                {
                    string name = new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileName();
                    string linerr = "Grid Columns is greater or lesser than Number of Columns";
                    string des = "Test Data is invalid";
                    ErrorLog err = new ErrorLog(name, linerr, des);
                    testErrors.Add(err);
                    countErrors++;

                }
                

                if (obj.RowData.Count < config.MinimumHorizontalWords || obj.RowData.Count > config.MaximumHorizontalWords)
                {
                    string name = new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileName();
                    string linerr = "Row data is greater or lesser than Horizontal words";
                    string des = "Test Data is invalid";
                    ErrorLog err = new ErrorLog(name, linerr, des);
                    testErrors.Add(err);
                    countErrors++;

                }
               
                if (obj.ColumnData.Count < config.MinimumVerticalWords || obj.ColumnData.Count > config.MaximumVerticalWords)
                {
                    string name = new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileName();
                    string linerr = "Column data is greater or lesser than Vertical words";
                    string des = "Test Data is invalid";
                    ErrorLog err = new ErrorLog(name, linerr, des);
                    testErrors.Add(err);
                    countErrors++;

                }
                
                int count = 0;
                foreach (var r in obj.RowData)
                {
                    foreach (var c in obj.ColumnData)
                    {
                        if (r.Column == c.Column)
                        {
                            count++;
                        }
                        else
                        {
                            continue;
                        }
                    }
                }

                if (count < config.MinimumIntersectionsInHorizontalWords || count > config.MaximumIntersectionsInHorizontalWords)
                {
                    string name = new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileName();
                    string linerr = "Row data is greater or lesser than Intersections Horizontal words";
                    string des = "Test Data is invalid";
                    ErrorLog err = new ErrorLog(name, linerr, des);
                    testErrors.Add(err);
                    countErrors++;

                }
                

                int counter = 0;
                foreach (var r in obj.ColumnData)
                {
                    foreach (var c in obj.RowData)
                    {
                        if (r.Row == c.Row)
                        {
                            counter++;
                        }
                        else
                        {
                            continue;
                        }
                    }
                }

                if (counter < config.MinimumIntersectionsInVerticalWords || counter > config.MaximumIntersectionsInVerticalWords)
                {
                    string name = new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileName();
                    string linerr = "Column data is greater or lesser than Intersections Vertical words";
                    string des = "Test Data is invalid";
                    ErrorLog err = new ErrorLog(name, linerr, des);
                    testErrors.Add(err);
                    countErrors++;

                }
                

                List<String> sameWords = wList.GroupBy(x => x)
                                        .Where(gb => gb.Count() > 1)
                                        .Select(gb => gb.Key)
                                        .ToList();

                if (sameWords.Count > config.MinimumNumberOfTheSameWord || sameWords.Count > config.MaximumNumberOfTheSameWord)
                {
                    string name = new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileName();
                    string linerr = "Duplicates in the word list";
                    string des = "Test Data is invalid";
                    ErrorLog err = new ErrorLog(name, linerr, des);
                    testErrors.Add(err);
                    countErrors++;

                }
                

                if (countErrors > 0)
                {
                    string fPath = Directory.GetCurrentDirectory();
                    string filname = @"log.txt";//Convert.ToString(htConfig["LOGFILE_NAME"]);
                    StreamWriter wtr = new StreamWriter(fPath + '\\' + filname, append: true);

                    foreach (var e in testErrors)
                    {
                        wtr.WriteLine("File Name: " + e.File_Name);
                        wtr.WriteLine("Line: " + e.Line);
                        wtr.WriteLine("Description: " + e.Description);

                    }
                    wtr.Close();
                    obj.IsCrozzleValid = false;
                    
                }
                else
                {
                    obj.IsCrozzleValid = true;
                    
                }

                
            }
            else
            {
                obj.IsCrozzleValid = false;
                
            }

            if (obj.IsCrozzleValid == false)
            {
                return obj;
            }
            else
            {
                obj.IsCrozzleValid = true;
                return obj;
            }








        }


    }
}
