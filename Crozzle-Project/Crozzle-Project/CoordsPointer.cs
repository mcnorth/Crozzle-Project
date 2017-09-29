using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crozzle_Project
{
    class CoordsPointer
    {
        public int LeftOfLetter { get; set; }
        public int RightOfLetter { get; set; }
        public int AboveLetter { get; set; }
        public int BelowLetter { get; set; }

        public CoordsPointer(Coordinate c)
        {
            LeftOfLetter = c.Column - 1;  //movement points 
            RightOfLetter = c.Column + 1;
            AboveLetter = c.Row - 1;
            BelowLetter = c.Row + 1;
        }

        public void GetTheCoordinates()
        {

        }
            

    }
}
