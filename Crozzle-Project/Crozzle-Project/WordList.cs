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
    public class WordList //: List<string>
    {
        private static readonly Char[] WordSeparators = new Char[] { ',' };

        public static List<string> Errors { get; set; }

        //properties
        public String WordlistPath { get; set; }
        public String WordlistFileName { get; set; }
        public String WordlistDirectoryName { get; set; }
       
        public String[] OriginalList { get; set; }
        public Boolean Valid { get; set; } = false;
        public List<String> List { get; set; }

        public int Count
        {
            get { return (List.Count); }
        }

        //constructors
        public WordList(string path, Configuration aConfiguration)
        {
            WordlistPath = path;
            WordlistFileName = Path.GetFileName(path);
            WordlistDirectoryName = Path.GetDirectoryName(path);
            List = new List<string>();
        }

        public static Boolean TryParse(String path, Configuration aConfiguration, out WordList aWordList)
        {
            StreamReader fileIn = new StreamReader(path);

            Errors = new List<String>();
            aWordList = new WordList(path, aConfiguration);

            // Split the original wordlist from the file.
            aWordList.OriginalList = fileIn.ReadLine().Split(WordSeparators);

            // Check each field in the wordlist.
            int fieldNumber = 0;
            foreach (String potentialWord in aWordList.OriginalList)
            {
                // Check that the field is not empty.
                if (potentialWord.Length > 0)
                {
                    // Check that the field is alphabetic.
                    if (Regex.IsMatch(potentialWord, Configuration.allowedCharacters))
                        aWordList.Add(potentialWord);
                    else
                        Errors.Add(String.Format(WordListErrors.AlphabeticError, potentialWord, fieldNumber));
                }
                else
                    Errors.Add(String.Format(WordListErrors.MissingWordError, fieldNumber));

                fieldNumber++;
            }

            // Check the minimmum word limit.
            if (aWordList.Count < aConfiguration.MinimumNumberOfUniqueWords)
                Errors.Add(String.Format(WordListErrors.MinimumSizeError, aWordList.Count, aConfiguration.MinimumNumberOfUniqueWords));

            // Check the maximum word limit.
            if (aWordList.Count > aConfiguration.MaximumNumberOfUniqueWords)
                Errors.Add(String.Format(WordListErrors.MaximumSizeError, aWordList.Count, aConfiguration.MaximumNumberOfUniqueWords));

            aWordList.Valid = Errors.Count == 0;
            return (aWordList.Valid);
        }

        public void Add(String letters)
        {
            List.Add(letters);
        }

        public Boolean Contains(String letters)
        {
            return (List.Contains(letters));
        }

        public string FileErrors
        {
            get
            {
                int errorNumber = 1;
                string errors = "START PROCESSING FILE: " + WordlistFileName + "\r\n";

                foreach (string error in WordList.Errors)
                    errors += "error " + errorNumber++ + ": " + error + "\r\n";
                errors += "END PROCESSING FILE: " + WordlistFileName + "\r\n";

                return (errors);
            }
        }

        public string FileErrorsHTML
        {
            get
            {
                int errorNumber = 1;
                string errors = "<p style=\"font-weight:bold\">START PROCESSING FILE: " + WordlistFileName + "</p>";

                foreach (string error in WordList.Errors)
                    errors += "<p>error " + errorNumber++ + ": " + error + "</p>";
                errors += "<p style=\"font-weight:bold\">END PROCESSING FILE: " + WordlistFileName + "</p>";

                return (errors);
            }
        }

        //private List<string> WORDS;

        //public List<string> Words
        //{
        //    get { return WORDS; }
        //    set { WORDS = value; }
        //}

        //private bool ISVALID;

        //public bool IsValid
        //{
        //    get { return ISVALID; }
        //    set { ISVALID = value; }
        //}



        //public WordList()
        //{

        //}

        ////create the wordlist object
        //public WordList CreateWordlist(string path, WordList obj)
        //{

        //     obj.GetFile(path, obj);

        //    return obj;
        //}

        ////get the file for wordlist and error check
        //public WordList GetFile(string path, WordList wds)
        //{
        //    var pathToFile = path;
        //    string[] file = File.ReadAllText(pathToFile).Split(',');
        //    string configPath = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;
        //    List<string> wrdsList1 = new List<string>();
        //    List<ErrorLog> wordlistErrors = new List<ErrorLog>();
        //    int count = 0;
        //    if (file.Length != file.Distinct().Count())
        //    {
        //        CreateLogFiles err = new CreateLogFiles();
        //        string fNa = @"LogFiles\log";
        //        err.ErrorLog(configPath + '\\' + fNa, "File: WordList.cs ----- Line: GetFile ----- Desc: Wordlist contains duplicates");
        //        count++;
        //        //string name = new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileName();
        //        //string linerr = "WordList";
        //        //string des = "Wordlist contains duplicates";
        //        //ErrorLog err = new ErrorLog(name, linerr, des);
        //        //wordlistErrors.Add(err);

        //    }

        //    foreach (string w in file)
        //    {

        //        wrdsList1.Add(w);
        //    }




        //    foreach (var line in wrdsList1)
        //    {
        //        var myReg = new Regex(@"^[a-zA-Z]+[a-zA-Z]$");
        //        if (myReg.IsMatch(line))
        //        {
        //            wds.Add(line);
        //            wds.IsValid = true;
        //        }
        //        else
        //        {
        //            CreateLogFiles err = new CreateLogFiles();
        //            string fNa = @"LogFiles\log";
        //            err.ErrorLog(configPath + '\\' + fNa, "File: WordList.cs ----- Line: "+line+" ----- Desc: Wordlist contains invalid word");
        //            //string name = new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileName();
        //            //string linerr = line;
        //            //string des = "Must be a word that does nnot contain numbers";
        //            //ErrorLog err = new ErrorLog(name, linerr, des);
        //            count++;
        //            wds.IsValid = false;

        //        }
        //    }

        //    if(count > 0)
        //    {
        //        //string fPath = Directory.GetCurrentDirectory();
        //        //string filname = @"log.txt";//Convert.ToString(htConfig["LOGFILE_NAME"]);
        //        //StreamWriter wtr = new StreamWriter(fPath + '\\' + filname, append: true);

        //        //foreach (var e in wordlistErrors)
        //        //{
        //        //    wtr.WriteLine("File Name: " + e.File_Name);
        //        //    wtr.WriteLine("Line: " + e.Line);
        //        //    wtr.WriteLine("Description: " + e.Description);

        //        //}
        //        //wtr.Close();
        //        wds.IsValid = false;
        //        return wds;
        //    }
        //    else
        //    {
        //        wds.IsValid = true;
        //        return wds;
        //    }


        //}


    }
}
