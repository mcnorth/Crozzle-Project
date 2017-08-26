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
    class Configuration
    {
        private string LOGFILE_NAME;
        private int MINIMUM_NUMBER_OF_UNIQUE_WORDS;
        private int MAXIMUM_NUMBER_OF_UNIQUE_WORDS;
        private string INVALID_CROZZLE_SCORE;
        private bool UPPERCASE;
        private string STYLE;
        private string BGCOLOUR_EMPTY_TD;
        private string BGCOLOUR_NON_EMPTY_TD;
        private int MINIMUM_NUMBER_OF_ROWS;
        private int MAXIMUM_NUMBER_OF_ROWS;
        private int MINIMUM_NUMBER_OF_COLUMNS;
        private int MAXIMUM_NUMBER_OF_COLUMNS;
        private int MINIMUM_HORIZONTAL_WORDS;
        private int MAXIMUM_HORIZONTAL_WORDS;
        private int MINIMUM_VERTICAL_WORDS;
        private int MAXIMUM_VERTICAL_WORDS;
        private int MINIMUM_INTERSECTIONS_IN_HORIZONTAL_WORDS;
        private int MAXIMUM_INTERSECTIONS_IN_HORIZONTAL_WORDS;
        private int MINIMUM_INTERSECTIONS_IN_VERTICAL_WORDS;
        private int MAXIMUM_INTERSECTIONS_IN_VERTICAL_WORDS;
        private int MINIMUM_NUMBER_OF_THE_SAME_WORD;
        private int MAXIMUM_NUMBER_OF_THE_SAME_WORD;
        private int MINIMUM_NUMBER_OF_GROUPS;
        private int MAXIMUM_NUMBER_OF_GROUPS;
        private int POINTS_PER_WORD;
        private Hashtable INTERSECTING_POINTS_PER_LETTER;
        private Hashtable NON_INTERSECTING_POINTS_PER_LETTER;
        private bool ISVALID;

        public bool IsValid
        {
            get { return ISVALID; }
            set { ISVALID = value; }
        }
        public string LogfileName
        {
            get { return LOGFILE_NAME; }
            set { LOGFILE_NAME = value; }
        }

        public int MinimumNumberOfUniqueWords
        {
            get { return MINIMUM_NUMBER_OF_UNIQUE_WORDS; }
            set { MINIMUM_NUMBER_OF_UNIQUE_WORDS = value; }
        }

        public int MaximumNumberOfUniqueWords
        {
            get { return MAXIMUM_NUMBER_OF_UNIQUE_WORDS; }
            set { MAXIMUM_NUMBER_OF_UNIQUE_WORDS = value; }
        }

        public string InvalidCrozzleScore
        {
            get { return INVALID_CROZZLE_SCORE; }
            set { INVALID_CROZZLE_SCORE = value; }
        }

        public bool UpperCase
        {
            get { return UPPERCASE; }
            set { UPPERCASE = value; }
        }

        public string Style
        {
            get { return STYLE; }
            set { STYLE = value; }
        }

        public string BgColourEmptyTd
        {
            get { return BGCOLOUR_EMPTY_TD; }
            set { BGCOLOUR_EMPTY_TD = value; }
        }

        public string BgColourNonEmptyTd
        {
            get { return BGCOLOUR_NON_EMPTY_TD; }
            set { BGCOLOUR_NON_EMPTY_TD = value; }
        }

        public int MinimumNumberOfRows
        {
            get { return MINIMUM_NUMBER_OF_ROWS; }
            set { MINIMUM_NUMBER_OF_ROWS = value; }
        }

        public int MaximumNumberOfRows
        {
            get { return MAXIMUM_NUMBER_OF_ROWS; }
            set { MAXIMUM_NUMBER_OF_ROWS = value; }
        }

        public int MinimumNumberOfColumns
        {
            get { return MINIMUM_NUMBER_OF_COLUMNS; }
            set { MINIMUM_NUMBER_OF_COLUMNS = value; }
        }

        public int MaximumNumberOfColumns
        {
            get { return MAXIMUM_NUMBER_OF_COLUMNS; }
            set { MAXIMUM_NUMBER_OF_COLUMNS = value; }
        }

        public int MinimumHorizontalWords
        {
            get { return MINIMUM_HORIZONTAL_WORDS; }
            set { MINIMUM_HORIZONTAL_WORDS = value; }
        }

        public int MaximumHorizontalWords
        {
            get { return MAXIMUM_HORIZONTAL_WORDS; }
            set { MAXIMUM_HORIZONTAL_WORDS = value; }
        }

        public int MinimumVerticalWords
        {
            get { return MINIMUM_VERTICAL_WORDS; }
            set { MINIMUM_VERTICAL_WORDS = value; }
        }

        public int MaximumVerticalWords
        {
            get { return MAXIMUM_VERTICAL_WORDS; }
            set { MAXIMUM_VERTICAL_WORDS = value; }
        }

        public int MinimumIntersectionsInHorizontalWords
        {
            get { return MINIMUM_INTERSECTIONS_IN_HORIZONTAL_WORDS; }
            set { MINIMUM_INTERSECTIONS_IN_HORIZONTAL_WORDS = value; }
        }

        public int MaximumIntersectionsInHorizontalWords
        {
            get { return MAXIMUM_INTERSECTIONS_IN_HORIZONTAL_WORDS; }
            set { MAXIMUM_INTERSECTIONS_IN_HORIZONTAL_WORDS = value; }
        }

        public int MinimumIntersectionsInVerticalWords
        {
            get { return MINIMUM_INTERSECTIONS_IN_VERTICAL_WORDS; }
            set { MINIMUM_INTERSECTIONS_IN_VERTICAL_WORDS = value; }
        }

        public int MaximumIntersectionsInVerticalWords
        {
            get { return MAXIMUM_INTERSECTIONS_IN_VERTICAL_WORDS; }
            set { MAXIMUM_INTERSECTIONS_IN_VERTICAL_WORDS = value; }
        }

        public int MinimumNumberOfTheSameWord
        {
            get { return MINIMUM_NUMBER_OF_THE_SAME_WORD; }
            set { MINIMUM_NUMBER_OF_THE_SAME_WORD = value; }
        }

        public int MaximumNumberOfTheSameWord
        {
            get { return MAXIMUM_NUMBER_OF_THE_SAME_WORD; }
            set { MAXIMUM_NUMBER_OF_THE_SAME_WORD = value; }
        }

        public int MinimumNumberOfGroups
        {
            get { return MINIMUM_NUMBER_OF_GROUPS; }
            set { MINIMUM_NUMBER_OF_GROUPS = value; }
        }

        public int MaximumNumberOfGroups
        {
            get { return MAXIMUM_NUMBER_OF_GROUPS; }
            set { MAXIMUM_NUMBER_OF_GROUPS = value; }
        }

        public int PointsPerWord
        {
            get { return POINTS_PER_WORD; }
            set { POINTS_PER_WORD = value; }
        }

        public Hashtable IntersectingPointsPerLetter
        {
            get { return INTERSECTING_POINTS_PER_LETTER; }
            set { INTERSECTING_POINTS_PER_LETTER = value; }
        }

        public Hashtable NonIntersectingPointsPerLetter
        {
            get { return NON_INTERSECTING_POINTS_PER_LETTER; }
            set { NON_INTERSECTING_POINTS_PER_LETTER = value; }
        }

        //error check lines from file
        public Configuration CreateConfigObj(string file, Configuration obj)
        {

            Tuple<Hashtable, string> ht = GetFile(file);
            Tuple<Hashtable, string> IPscoreHt = GetIPScoreFile(file);
            Tuple<Hashtable, string> NIPscoreHt = GetNIPScoreFile(file);
            //Hashtable hashT = ValidateHt(ht);
            CreateConfig(obj, ht, IPscoreHt, NIPscoreHt);
            return obj;
        }


        public Tuple<Hashtable, string> GetNIPScoreFile(string path)
        {
            var pathToFile = path;
            var file = File.ReadAllLines(pathToFile);
            List<string> configFile1 = new List<string>();
            List<string> configFile2 = new List<string>();
            List<string> configFile3 = new List<string>();
            List<string> configFile4 = new List<string>();
            List<string> configFile5 = new List<string>();
            List<string> ScoreConfigFile = new List<string>();
            string[] IPScoreArray = new string[0];

            List<ErrorLog> configErrors = new List<ErrorLog>();

            Hashtable NonintersectingPoints = new Hashtable();

            foreach (var line in file)
            {
                configFile1.Add(line);
            }

            //error checking and removing useless chars
            foreach (var line in configFile1)
            {
                Regex reg = new Regex(@"^NON");
                if (reg.IsMatch(line))
                {
                    configFile2.Add(line);
                }

            }

            foreach (var res in configFile2)
            {

                int index = res.IndexOf('=');
                var val = res.Substring(index + 1);
                configFile3.Add(val);
            }

            foreach (var line in configFile3)
            {
                if (line.Contains('"'))
                {
                    string res = line.Replace("\"", "");
                    configFile4.Add(res);
                }
            }

            foreach (string s in configFile4)
            {
                IPScoreArray = s.Split(',');
            }

            foreach (var line in IPScoreArray)
            {
                configFile5.Add(line);
            }

            //add object from list to hashtable
            foreach (string str in IPScoreArray)
            {
                int index = str.IndexOf("=");
                var key = str.Substring(0, index);
                var val = str.Substring(index + 1);
                if (NonintersectingPoints.ContainsKey(key))
                {
                    string name = new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileName();
                    string linerr = "Hashtable Intersecting Points";
                    string des = "Duplicate key";
                    ErrorLog err = new ErrorLog(name, linerr, des);
                    configErrors.Add(err);
                    NonintersectingPoints.Remove(key);
                }

                NonintersectingPoints.Add(key, val);
            }

            //string [] alphabet = new string[] {"A", "B", "C", "D", "E", "F", "G", "H",  };

            //error checking
            string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

            foreach (char letter in alphabet)
            {
                if (NonintersectingPoints.ContainsKey(Convert.ToString(letter)))
                {
                    continue;
                }
                else
                {
                    string name = new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileName();
                    string linerr = "Hashtable";
                    string des = "Letter " + letter + " missing from Intersecting Points hashtable";
                    ErrorLog err = new ErrorLog(name, linerr, des);
                    configErrors.Add(err);

                }
            }

            Regex r = new Regex(@"\d+");

            foreach (string num in NonintersectingPoints.Values)
            {
                if (r.IsMatch(num))
                {
                    continue;
                }
                else
                {
                    string name = new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileName();
                    string linerr = "Hashtable Value";
                    string des = "Values must equal a number";
                    ErrorLog err = new ErrorLog(name, linerr, des);
                    configErrors.Add(err);
                }
            }

            //write errors to log file
            string fPath = Directory.GetCurrentDirectory();
            string filname = @"log.txt";//Convert.ToString(htConfig["LOGFILE_NAME"]);
            StreamWriter wtr = new StreamWriter(fPath + '\\' + filname, append: true);

            foreach (var e in configErrors)
            {
                wtr.WriteLine("File Name: " + e.File_Name);
                wtr.WriteLine("Line: " + e.Line);
                wtr.WriteLine("Description: " + e.Description);

            }
            wtr.Close();

            string invalid = "Invalid";
            string valid = "Valid";
            Tuple<Hashtable, string> tuple;

            if (configErrors.Count > 0)
            {

                tuple = new Tuple<Hashtable, string>(NonintersectingPoints, invalid);
            }
            else
            {
                tuple = new Tuple<Hashtable, string>(NonintersectingPoints, valid);
            }
            return tuple;
            
        }

        //create the score table
        public Tuple<Hashtable, string> GetIPScoreFile(string path)
        {
            var pathToFile = path;
            var file = File.ReadAllLines(pathToFile);
            List<string> configFile1 = new List<string>();
            List<string> configFile2 = new List<string>();
            List<string> configFile3 = new List<string>();
            List<string> configFile4 = new List<string>();
            List<string> configFile5 = new List<string>();
            List<string> ScoreConfigFile = new List<string>();
            string[] IPScoreArray = new string[0];
            
            List<ErrorLog> configErrors = new List<ErrorLog>();

            Hashtable intersectingPoints = new Hashtable();            

            foreach (var line in file)
            {
                configFile1.Add(line);
            }

            foreach (var line in configFile1)
            {
                Regex reg = new Regex(@"^INTERSECTING");
                if (reg.IsMatch(line))
                {
                    configFile2.Add(line);
                }
                
            }

            foreach (var res in configFile2)
            {

                int index = res.IndexOf('=');
                var val = res.Substring(index + 1);
                configFile3.Add(val);
            }

            foreach (var line in configFile3)
            {
                if(line.Contains('"'))
                {
                    string res = line.Replace("\"", "");
                    configFile4.Add(res);
                }
            }

            foreach (string s in configFile4)
            {
                IPScoreArray = s.Split(',');
            }

            foreach(var line in IPScoreArray)
            {
                configFile5.Add(line);
            }

            foreach (string str in IPScoreArray)
            {
                int index = str.IndexOf("=");
                var key = str.Substring(0, index);
                var val = str.Substring(index + 1);
                if (intersectingPoints.ContainsKey(key))
                {
                    string name = new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileName();
                    string linerr = "Hashtable Intersecting Points";
                    string des = "Duplicate key";
                    ErrorLog err = new ErrorLog(name, linerr, des);
                    configErrors.Add(err);
                    intersectingPoints.Remove(key);
                }
                intersectingPoints.Add(key, val);
            }

            //string [] alphabet = new string[] {"A", "B", "C", "D", "E", "F", "G", "H",  };

            string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

            foreach(char letter in alphabet)
            {
                if(intersectingPoints.ContainsKey(Convert.ToString(letter)))
                {
                    continue;
                }
                else
                {
                    string name = new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileName();
                    string linerr = "Hashtable";
                    string des = "Letter " + letter + " missing from Intersecting Points hashtable";
                    ErrorLog err = new ErrorLog(name, linerr, des);
                    configErrors.Add(err);
                    
                }
            }

            Regex r = new Regex(@"\d+");

            foreach(string num in intersectingPoints.Values)
            {
                if(r.IsMatch(num))
                {
                    continue;
                }
                else
                {
                    string name = new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileName();
                    string linerr = "Hashtable Value";
                    string des = "Values must equal a number";
                    ErrorLog err = new ErrorLog(name, linerr, des);
                    configErrors.Add(err);
                }
            }
            
            string fPath = Directory.GetCurrentDirectory();
            string filname = @"log.txt";//Convert.ToString(htConfig["LOGFILE_NAME"]);
            StreamWriter wtr = new StreamWriter(fPath + '\\' + filname, append: true);

            foreach (var e in configErrors)
            {
                wtr.WriteLine("File Name: " + e.File_Name);
                wtr.WriteLine("Line: " + e.Line);
                wtr.WriteLine("Description: " + e.Description);

            }
            wtr.Close();

            string invalid = "Invalid";
            string valid = "Valid";
            Tuple<Hashtable, string> tuple;

            if (configErrors.Count > 0)
            {

                tuple = new Tuple<Hashtable, string>(intersectingPoints, invalid);
            }
            else
            {
                tuple = new Tuple<Hashtable, string>(intersectingPoints, valid);
            }
            return tuple;
            
        }

        //get the configuration file, read and add each line to list
        public Tuple<Hashtable, string> GetFile(string path)
        {
            var pathToFile = path;
            var file = File.ReadAllLines(pathToFile);
            List<string> configFile1 = new List<string>();
            List<string> configFile2 = new List<string>();
            List<string> configFile3 = new List<string>();
            List<string> configFile4 = new List<string>();
            List<string> configFile5 = new List<string>();
            List<string> configFile6 = new List<string>();
            List<string> configFile7 = new List<string>();
            List<ErrorLog> configErrors = new List<ErrorLog>();

            string result;

            Hashtable htConfig = new Hashtable();

            //error checking and removing useless obj
            foreach (var line in file)
            {
                configFile1.Add(line);
               
            }

            foreach (var line in configFile1)
            {
                var myRegex = new Regex(@"\w+=");
                if (myRegex.IsMatch(line))
                {
                    configFile2.Add(line);
                }                
            }

            foreach (var line in configFile2.ToArray())
            {
                var myRegex = new Regex(@"^[A-Z]");
                if (myRegex.IsMatch(line))
                {
                    configFile3.Add(line);
                }
                else
                {
                    string name = new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileName();
                    string linerr = line;
                    string des = "Keyword missing";
                    ErrorLog err = new ErrorLog(name, linerr, des);
                    configErrors.Add(err);
                    configFile2.Remove(line);
                }
            }

            foreach (var line in configFile3)
            {
                if (line.Contains("//"))
                {
                    int index = line.IndexOf("//");
                    result = line.Substring(0, index);
                    string trim = result.Trim();
                    configFile4.Add(trim);
                }
                else
                {
                    configFile4.Add(line);
                }
            }

            foreach (var line in configFile4)
            {
                var myRegex = new Regex(@"MAXIMUM|MINIMUM|POINTS");
                var myReg = new Regex(@"\w+=\d");
                if (myRegex.IsMatch(line))
                {
                    if(myReg.IsMatch(line))
                    {
                        configFile5.Add(line);
                    }
                    else
                    {
                        string name = new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileName();
                        string linerr = line;
                        string des = "Must be an integer";
                        ErrorLog err = new ErrorLog(name, linerr, des);
                        configErrors.Add(err);
                        
                    }
                }
                else
                {
                    configFile5.Add(line);
                }
            }

            foreach(var line in configFile5)
            {
                var myRegex = new Regex(@"BGCOLOUR");
                var myReg = new Regex(@"\w+=#\w{6}$");
                if (myRegex.IsMatch(line))
                {
                    if (myReg.IsMatch(line))
                    {
                        configFile6.Add(line);
                    }
                    else
                    {
                        string name = new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileName();
                        string linerr = line;
                        string des = "Invalid hex colour";
                        ErrorLog err = new ErrorLog(name, linerr, des);
                        configErrors.Add(err);

                    }
                }
                else
                {
                    configFile6.Add(line);
                }

            }

            foreach (var line in configFile6.ToArray())
            {
                var myRegex = new Regex(@"UPPERCASE");
                var myReg = new Regex(@"true|false");
                if (myRegex.IsMatch(line))
                {
                    if (myReg.IsMatch(line))
                    {
                        configFile7.Add(line);
                    }
                    else
                    {
                        string name = new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileName();
                        string linerr = line;
                        string des = "Invalid boolean";
                        ErrorLog err = new ErrorLog(name, linerr, des);
                        configErrors.Add(err);
                        configFile6.Remove(line);
                    }
                }
                else
                {
                    configFile7.Add(line);
                }

            }

            foreach (var res in configFile7)
            {

                int index = res.IndexOf("=");
                var key = res.Substring(0, index);
                var val = res.Substring(index + 1);
                if (htConfig.ContainsKey(key))
                {
                    string name = new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileName();
                    string linerr = "Hashtable Config";
                    string des = "Duplicate key";
                    ErrorLog err = new ErrorLog(name, linerr, des);
                    configErrors.Add(err);
                    htConfig.Remove(key);
                }
                htConfig.Add(key, val);
            }

            string fPath = Directory.GetCurrentDirectory();
            string filname = @"log.txt";//Convert.ToString(htConfig["LOGFILE_NAME"]);
            StreamWriter wtr = new StreamWriter(fPath + '\\' + filname, append:true);
            
            foreach(var e in configErrors)
            {
                wtr.WriteLine("File Name: " + e.File_Name);
                wtr.WriteLine("Line: " + e.Line);
                wtr.WriteLine("Description: " + e.Description);
                
            }
            wtr.Close();

            string invalid = "Invalid";
            string valid = "Valid";
            Tuple<Hashtable, string> tuple;

            if (configErrors.Count > 0)
            {

                tuple = new Tuple<Hashtable, string>(htConfig, invalid);
            }
            else
            {
                tuple = new Tuple<Hashtable, string>(htConfig, valid);
            }
            return tuple;
        }

        
        //create configuration object for return
        public Configuration CreateConfig(Configuration obj, Tuple<Hashtable, string> HtTuple, Tuple<Hashtable, string> IPTuple, Tuple<Hashtable, string> NIPTuple)
        {
            Hashtable HtObj = HtTuple.Item1;
            string result = HtTuple.Item2;
            Hashtable IPObj = IPTuple.Item1;
            string result2 = IPTuple.Item2;
            Hashtable NIPObj = NIPTuple.Item1;
            string result3 = NIPTuple.Item2;

            if (result == "Valid" || result2 == "Valid" || result3 == "Valid")
            {
                obj.IsValid = true;
                obj.LogfileName = Convert.ToString(HtObj["LOGFILE_NAME"]);
                obj.MinimumNumberOfUniqueWords = Convert.ToInt32(HtObj["MINIMUM_NUMBER_OF_UNIQUE_WORDS"]);
                obj.MaximumNumberOfUniqueWords = Convert.ToInt32(HtObj["MAXIMUM_NUMBER_OF_UNIQUE_WORDS"]);
                obj.InvalidCrozzleScore = Convert.ToString(HtObj["INVALID_CROZZLE_SCORE"]);
                obj.UpperCase = Convert.ToBoolean(HtObj["UPPERCASE"]);
                obj.Style = Convert.ToString(HtObj["STYLE"]);
                obj.BgColourEmptyTd = Convert.ToString(HtObj["BGCOLOUR_EMPTY_TD"]);
                obj.BgColourNonEmptyTd = Convert.ToString(HtObj["BGCOLOUR_NON_EMPTY_TD"]);
                obj.MinimumNumberOfRows = Convert.ToInt32(HtObj["MINIMUM_NUMBER_OF_ROWS"]);
                obj.MaximumNumberOfRows = Convert.ToInt32(HtObj["MAXIMUM_NUMBER_OF_ROWS"]);
                obj.MinimumNumberOfColumns = Convert.ToInt32(HtObj["MINIMUM_NUMBER_OF_COLUMNS"]);
                obj.MaximumNumberOfColumns = Convert.ToInt32(HtObj["MAXIMUM_NUMBER_OF_COLUMNS"]);
                obj.MinimumHorizontalWords = Convert.ToInt32(HtObj["MINIMUM_HORIZONTAL_WORDS"]);
                obj.MaximumHorizontalWords = Convert.ToInt32(HtObj["MAXIMUM_HORIZONTAL_WORDS"]);
                obj.MinimumVerticalWords = Convert.ToInt32(HtObj["MINIMUM_VERTICAL_WORDS"]);
                obj.MaximumVerticalWords = Convert.ToInt32(HtObj["MAXIMUM_VERTICAL_WORDS"]);
                obj.MinimumIntersectionsInHorizontalWords = Convert.ToInt32(HtObj["MINIMUM_INTERSECTIONS_IN_HORIZONTAL_WORDS"]);
                obj.MaximumIntersectionsInHorizontalWords = Convert.ToInt32(HtObj["MAXIMUM_INTERSECTIONS_IN_HORIZONTAL_WORDS"]);
                obj.MinimumIntersectionsInVerticalWords = Convert.ToInt32(HtObj["MINIMUM_INTERSECTIONS_IN_VERTICAL_WORDS"]);
                obj.MaximumIntersectionsInVerticalWords = Convert.ToInt32(HtObj["MAXIMUM_INTERSECTIONS_IN_VERTICAL_WORDS"]);
                obj.MinimumNumberOfTheSameWord = Convert.ToInt32(HtObj["MINIMUM_NUMBER_OF_THE_SAME_WORD"]);
                obj.MaximumNumberOfTheSameWord = Convert.ToInt32(HtObj["MAXIMUM_NUMBER_OF_THE_SAME_WORD"]);
                obj.MinimumNumberOfGroups = Convert.ToInt32(HtObj["MINIMUM_NUMBER_OF_GROUPS"]);
                obj.MaximumNumberOfGroups = Convert.ToInt32(HtObj["MAXIMUM_NUMBER_OF_GROUPS"]);
                obj.PointsPerWord = Convert.ToInt32(HtObj["POINTS_PER_WORD"]);
                obj.IntersectingPointsPerLetter = IPObj;
                obj.NonIntersectingPointsPerLetter = NIPObj;
            }
            else
            {
                obj.IsValid = false;
            }
            


            return obj;
        }

    }
}
