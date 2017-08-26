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
    class WordList : List<string>
    {

        private List<string> WORDS;

        public List<string> Words
        {
            get { return WORDS; }
            set { WORDS = value; }
        }

        private bool ISVALID;

        public bool IsValid
        {
            get { return ISVALID; }
            set { ISVALID = value; }
        }



        public WordList()
        {

        }

        //create the wordlist object
        public WordList CreateWordlist(string path, WordList obj)
        {
            
             obj.GetFile(path, obj);
            
            return obj;
        }

        //get the file for wordlist and error check
        public WordList GetFile(string path, WordList wds)
        {
            var pathToFile = path;
            string[] file = File.ReadAllText(pathToFile).Split(',');
            string configPath = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;
            List<string> wrdsList1 = new List<string>();
            List<ErrorLog> wordlistErrors = new List<ErrorLog>();
            
            if (file.Length != file.Distinct().Count())
            {
                CreateLogFiles err = new CreateLogFiles();
                string fNa = @"LogFiles\log";
                err.ErrorLog(configPath + '\\' + fNa, "File: WordList.cs ----- Line: GetFile ----- Desc: Wordlist contains duplicates");
                //string name = new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileName();
                //string linerr = "WordList";
                //string des = "Wordlist contains duplicates";
                //ErrorLog err = new ErrorLog(name, linerr, des);
                //wordlistErrors.Add(err);

            }

            foreach (string w in file)
            {

                wrdsList1.Add(w);
            }




            foreach (var line in wrdsList1)
            {
                var myReg = new Regex(@"\w+");
                if (myReg.IsMatch(line))
                {
                    wds.Add(line);
                    wds.IsValid = true;
                }
                else
                {
                    string name = new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileName();
                    string linerr = line;
                    string des = "Must be a word that does nnot contain numbers";
                    ErrorLog err = new ErrorLog(name, linerr, des);
                    wordlistErrors.Add(err);
                    wds.IsValid = false;

                }
            }

            if(wordlistErrors.Count > 0)
            {
                string fPath = Directory.GetCurrentDirectory();
                string filname = @"log.txt";//Convert.ToString(htConfig["LOGFILE_NAME"]);
                StreamWriter wtr = new StreamWriter(fPath + '\\' + filname, append: true);

                foreach (var e in wordlistErrors)
                {
                    wtr.WriteLine("File Name: " + e.File_Name);
                    wtr.WriteLine("Line: " + e.Line);
                    wtr.WriteLine("Description: " + e.Description);

                }
                wtr.Close();
                wds.IsValid = false;
                return wds;
            }
            else
            {
                wds.IsValid = true;
                return wds;
            }

            
        }
 
        
    }
}
