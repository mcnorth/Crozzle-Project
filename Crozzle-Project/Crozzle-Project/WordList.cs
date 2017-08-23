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



        public WordList()
        {

        }

        public WordList CreateWordlist(string path, WordList obj)
        {
            
             obj.GetFile(path, obj);
            
            return obj;
        }

        public WordList GetFile(string path, WordList wds)
        {
            var pathToFile = path;
            string[] file = File.ReadAllText(pathToFile).Split(',');
            

            foreach(string w in file)
            {
                
                wds.Add(w);
            }

            return wds;
            
        }

        

        
        
    }
}
