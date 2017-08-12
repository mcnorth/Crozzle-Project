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

        private List<string> _Words = new List<string>();

        public List<string> Words
        {
            get { return _Words; }
            set { _Words = value; }
        }

        public WordList()
        {

        }

        public void AddWords(string value)
        {
            _Words.Add(value);
        }
        
    }
}
