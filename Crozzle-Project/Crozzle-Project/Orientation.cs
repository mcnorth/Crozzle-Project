using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Crozzle_Project
{
    class Orientation
    {
        //constants
        public const String Row = "ROW";
        public const String Column = "COLUMN";
        public static readonly String Pattern = String.Format("^({0}|{1})$", Row, Column);

        //properties
        public static List<String> Errors { get; set; }

        public String OriginalDirection { get; set; }
        public Boolean Valid { get; set; }
        public String Direction { get; set; }

        public Boolean IsHorizontal
        {
            get { return (Direction.Equals(Row, StringComparison.Ordinal)); }
        }

        public Boolean IsVertical
        {
            get { return (Direction.Equals(Column, StringComparison.Ordinal)); }
        }

        //constructor
        public Orientation(String originalOrientationData)
        {
            OriginalDirection = originalOrientationData;
            Valid = false;
            Direction = null;
        }

        public static Boolean TryParse(String originalOrientationData, out Orientation anOrientation)
        {
            Errors = new List<String>();
            anOrientation = new Orientation(originalOrientationData);

            anOrientation.Valid = false;
            if (Regex.IsMatch(originalOrientationData, Pattern))
            {
                anOrientation.Direction = originalOrientationData;
                anOrientation.Valid = true;
            }
            else
                Errors.Add(String.Format(OrientationErrors.SymbolError, originalOrientationData));

            return (anOrientation.Valid);
        }
    }
}
