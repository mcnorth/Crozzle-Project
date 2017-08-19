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
    class WordList
    {

        

        public WordList()
        {

        }

        public WordList CreateWordlist(string path, WordList obj)
        {
            
            List<string> words = GetFile(path);
            
            return obj;
        }

        public List<string> GetFile(string path)
        {
            var pathToFile = path;
            string[] file = File.ReadAllText(pathToFile).Split(',');
            List<string> wds = new List<string>();

            foreach(string w in file)
            {
                wds.Add(w);
            }

            return wds;
            
        }

        

        
        
    }
}
