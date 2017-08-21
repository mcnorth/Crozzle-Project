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

        public CrozzleTest CreateCrozzleTest(string path, CrozzleTest obj)
        {

            List<string> words = GetFile(path);
            Hashtable fileLoc = GetFileNames(words);
            List<RowData> rData = GetRowData(words);
            CrozzleTestObj(obj, fileLoc);

            return obj;
        }

        public List<RowData> GetRowData(List<string> data)
        {
            List<RowData> res = new List<RowData>();
            List<string> temp = new List<string>();
            List<string> t = new List<string>();

            foreach (var r in data)
            {
                if (r.Contains("ROW") && r.Contains(","))
                {
                    temp.Add(r);
                }
                else
                {
                    continue;
                }
                
            }

            string pattern = @"^ROW|=|,";
            
            foreach (var o in temp)
            {
                
                string[] tem = Regex.Split(o, pattern);
                foreach (string c in tem)
                {
                    if(!string.IsNullOrEmpty(c))
                    {
                        t.Add(c);
                    }

                    
                }

                RowData d = new RowData();
                d.Row = Convert.ToInt32(t[0]);
                d.Name = t[1];
                d.Column = Convert.ToInt32(t[2]);
                res.Add(d);

                

            }
            return res;
        }

        public CrozzleTest CrozzleTestObj(CrozzleTest obj, Hashtable files)
        {
            obj.ConfigurationFile = Convert.ToString(files["CONFIGURATION_FILE"]);
            return obj;
        }

        public Hashtable GetFileNames(List<string> loc)
        {
            //string pat = "FILE";
            Hashtable result = new Hashtable();
            List<string> temp = new List<string>();

            foreach (var r in loc)
            {
                 if(r.Contains("FILE"))
                {
                    //
                    temp.Add(r);
                    
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

            //foreach (var r in testFile2)
            //{

            //    if (r.Contains("\""))
            //    {
            //        string s = r.Replace("\"", "");
            //        testFile3.Add(s);
            //    }
            //    else
            //    {
            //        testFile3.Add(r);
            //    }
            //}

            return testFile2;
        }


    }
}
