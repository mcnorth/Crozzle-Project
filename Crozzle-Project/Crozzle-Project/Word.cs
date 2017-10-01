using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crozzle_Project
{
    class Word
    {
        public string Name { get; set; }
        public CordsLetterTable NameCords { get; set; }
        public char[] LettersInWord { get; set; }
        public bool IsHorizontal { get; set; }
        public bool IsVertical { get; set; }
        public bool IsAdded { get; set; }
        public bool IsValid { get; set; }
        public Dictionary<char, List<string>> ListOfIntersectingWordsPerLetter { get; set; }
        

        public Word(string name)
        {
            Name = name;
            NameCords = new CordsLetterTable();
            ListOfIntersectingWordsPerLetter = null;
            LettersInWord = null;
            IsHorizontal = false;
            IsVertical = false;
            IsAdded = false;
            IsValid = false;

        }



    }
}
