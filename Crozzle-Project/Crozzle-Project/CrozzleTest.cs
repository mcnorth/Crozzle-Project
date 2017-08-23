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

        public CrozzleTest CreateCrozzleTest(string path, CrozzleTest obj)
        {

            List<string> words = GetFile(path);
            Hashtable fileLoc = GetFileNames(words);
            List<RowData> rData = GetRowData(words);
            List<ColumnData> cData = GetColumnData(words);
            Hashtable grid = GetGrid(words);
            CrozzleTestObj(obj, fileLoc, rData, cData, grid);

            return obj;
        }

        public Hashtable GetGrid(List<string> data)
        {
            Hashtable result = new Hashtable();
            List<string> temp = new List<string>();

            foreach(var d in data)
            {
                if(d.Contains(","))
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

        public List<ColumnData> GetColumnData(List<string> data)
        {
            List<ColumnData> res = new List<ColumnData>();
            List<string> temp = new List<string>();
            List<string> temp2 = new List<string>();
            string result;

            foreach (var r in data)
            {
                if (r.Contains("COLUMN") && r.Contains(","))
                {
                    ColumnData c = new ColumnData();
                    temp.Add(r);
                }
                else
                {
                    continue;
                }

            }

            foreach (var o in temp)
            {
                if (o.Contains("="))
                {
                    int index = o.IndexOf("=");
                    result = o.Substring(index + 1);
                    temp2.Add(result);
                }
            }

            foreach (var a in temp2)
            {
                string[] y = a.Split(',');

                ColumnData ob = new ColumnData();
                ob.Column = Convert.ToInt32(y[0]);
                ob.Name = Convert.ToString(y[1]);
                ob.Row = Convert.ToInt32(y[2]);
                res.Add(ob);



            }
            
            return res;
            
        }

        public List<RowData> GetRowData(List<string> data)
        {
            List<RowData> res = new List<RowData>();
            List<string> temp = new List<string>();
            List<string> temp2 = new List<string>();
            string result;

            foreach (var r in data)
            {
                if (r.Contains("ROW") && r.Contains(","))
                {
                    RowData c = new RowData();
                    temp.Add(r);
                }
                else
                {
                    continue;
                }

            }

            foreach (var o in temp)
            {
                if (o.Contains("="))
                {
                    int index = o.IndexOf("=");
                    result = o.Substring(index + 1);
                    temp2.Add(result);
                }
            }

            foreach (var a in temp2)
            {
                string[] y = a.Split(',');

                RowData ob = new RowData();
                ob.Row = Convert.ToInt32(y[0]);
                ob.Name = Convert.ToString(y[1]);
                ob.Column = Convert.ToInt32(y[2]);
                res.Add(ob);



            }
            return res;
        }

        public CrozzleTest CrozzleTestObj(CrozzleTest obj, Hashtable files, List<RowData> rowData, List<ColumnData> columnData, Hashtable gridLayout)
        {
            obj.ConfigurationFile = Convert.ToString(files["CONFIGURATION_FILE"]);
            obj.WordlistFile = Convert.ToString(files["WORDLIST_FILE"]);
            obj.GridRows = Convert.ToInt32(gridLayout["ROWS"]);
            obj.GridColumns = Convert.ToInt32(gridLayout["COLUMNS"]);
            obj.RowData = rowData;
            obj.ColumnData = columnData;
            return obj;
        }

        public Hashtable GetFileNames(List<string> loc)
        {
            //string pat = "FILE";
            Hashtable result = new Hashtable();
            List<string> temp = new List<string>();
            List<string> t = new List<string>();
            List<string> cor = new List<string>();

            foreach (var r in loc)
            {
                 if(r.Contains("FILE"))
                {
                    //
                    temp.Add(r);
                    
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

            //foreach(var c in t)
            //{
            //    if(c.Contains(" "))
            //    {
            //        string st = c.Replace(" ", "");
            //        cor.Add(st);
            //    }
            //}

            foreach (var res in t)
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

        public bool TestCrozzle(CrozzleTest obj, string rPath)
        {
            string cFile = rPath + "\\" + Convert.ToString(obj.ConfigurationFile);
            Configuration config = new Configuration();           
            config.CreateConfigObj(cFile, config);

            string wFile = rPath + "\\" + Convert.ToString(obj.WordlistFile);
            WordList wList = new WordList();
            wList.CreateWordlist(wFile, wList);

            //
            if(wList.Count < config.MinimumNumberOfUniqueWords || wList.Count > config.MaximumNumberOfUniqueWords)
            {
                return false;
            }

            if(obj.GridRows < config.MinimumNumberOfRows || obj.GridRows > config.MaximumNumberOfRows)
            {
                return false;
            }

            if(obj.GridColumns < config.MinimumNumberOfColumns || obj.GridColumns > config.MaximumNumberOfColumns)
            {
                return false;
            }

            if(obj.RowData.Count < config.MinimumHorizontalWords || obj.RowData.Count > config.MaximumHorizontalWords)
            {
                return false;
            }

            if(obj.ColumnData.Count < config.MinimumVerticalWords || obj.ColumnData.Count > config.MaximumVerticalWords)
            {
                return false;
            }

            int count = 0;
            foreach (var r in obj.RowData)
            {
                foreach(var c in obj.ColumnData)
                {                   
                    if(r.Column == c.Column)
                    {
                        count++;
                    }
                    else
                    {
                        continue;
                    }
                }
            }

            if(count < config.MinimumIntersectionsInHorizontalWords || count > config.MaximumIntersectionsInHorizontalWords)
            {
                return false;
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
                return false;
            }

            List<String> sameWords = wList.GroupBy(x => x)
                                    .Where(gb => gb.Count() > 1)
                                    .Select(gb => gb.Key)
                                    .ToList();

            if(sameWords.Count > config.MinimumNumberOfTheSameWord || sameWords.Count > config.MaximumNumberOfTheSameWord)
            {
                return false;
            }

            //check for groups
            //List<string> horizontalWords = new List<string>();
            //List<string> aloneWords = new List<string>();
            //List<RowData> hWords = obj.RowData;
            //List<ColumnData> vWords = obj.ColumnData;

            

            

            //foreach (var r in hWords.ToArray())
            //{
                
            //    foreach (var c in vWords)
            //    {
            //        if (r.Row == c.Row || r.Column == c.Column)
            //        {
            //            horizontalWords.Add(r.Name);
            //            hWords.Remove(r);
            //            break;

            //        }
            //        else
            //        {
            //            continue;
            //            //aloneWords.Add(r.Name);
            //        }
            //    }
            //}


            return true;
        }


    }
}
