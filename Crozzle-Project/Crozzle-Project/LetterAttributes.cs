using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crozzle_Project
{
    class LetterAttributes
    {
        public int IterationsLeftOrAbove { get; set; }
        public int LengthOfWord { get; set; }
        public int IterationsRightOrBelow { get; set; }
        public char Letter { get; set; }
        public bool Valid { get; set; }
        public int ExistingLetterRow { get; set; }
        public int ExistingLetterColumn { get; set; }

        public LetterAttributes(int index, int count, char lett)
        {
            IterationsLeftOrAbove = index;
            LengthOfWord = count;
            IterationsRightOrBelow = count - index;
            Letter = lett;
            Valid = false;
            ExistingLetterRow = 0;
            ExistingLetterColumn = 0;
        }
    }
}
