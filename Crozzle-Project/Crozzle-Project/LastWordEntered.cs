using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crozzle_Project
{
    class LastWordEntered
    {
        public string Name { get; set; }
        public Dictionary<char, List<string>> IntersectingWords { get; set; }
        public Dictionary<Coordinate, char> LetterCords { get; set; }
        public Dictionary<char, Coordinate> LetterCoordsForIntersectingWords { get; set; }
        public int Row { get; set; }
        public int Col { get; set; }

        

        public LastWordEntered(string name, Dictionary<char, List<string>>intersectingwords, Dictionary<Coordinate, char> lettercords)
        {
            Name = name;
            IntersectingWords = intersectingwords;
            LetterCords = lettercords;
            
        }

        
    }
}
