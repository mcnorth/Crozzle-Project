using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crozzle_Project
{
    class CrozzleGrid
    {
        Configuration Config { get; set; }
        WordListTaskTwo WordList { get; set; }
        CrozzleTaskTwo RowsAndColumns { get; set; }


        public CrozzleGrid(Configuration config, WordListTaskTwo wordlist, CrozzleTaskTwo rowsandcolumns)
        {
            Config = config;
            WordList = wordlist;
            RowsAndColumns = rowsandcolumns;
            
        }

        public string CreateGrid()
        {
            char[,] grid = new char[Convert.ToInt16(RowsAndColumns.Rows), Convert.ToInt16(RowsAndColumns.Columns)];

            

            Dictionary<char, int> points = new Dictionary<char, int>();


            int scoreWords = 0;

            Dictionary<string, int> bestWordDict = new Dictionary<string, int>();

            //var longestWord = WordList.Table.Keys.OrderByDescending(s => s.Length)
            //       .ThenBy(s => s)
            //       .FirstOrDefault();

            var dictList = WordList.Table;
            char keyindic = 'A';

            foreach(var val in Config.IntersectingPointsPerLetter)
            {
                points.Add(keyindic, val);
                keyindic++;
            }

            foreach(var k in dictList)
            {
                var letters = k.Value;
                foreach (var l in letters)
                {
                    if (points.ContainsKey(l.Key))
                    {
                        scoreWords = scoreWords + points[l.Key];
                    }
                }

                bestWordDict.Add(k.Key, scoreWords);
                scoreWords = 0;
            }

            string highestWord = bestWordDict.Max(s => s.Key);

            //foreach(var key in dicList)

            //add word to the centre of the grid
            char[] wordCharArray = highestWord.ToCharArray();

            int x = grid.GetLength(0) / 2;
            int y = (grid.GetLength(1) - highestWord.Length) / 2;

            foreach (var letter in wordCharArray)
            {
                grid[x, y] = letter;
                y = y + 1;
            }

            //for (int x = grid.GetLength(0) / 2; x < grid.GetLength(0); x++)
            //{
            //    for (int y = (grid.GetLength(1) - highestWord.Length) / 2; y < grid.GetLength(1); y++)
            //    {
            //        foreach (var letter in wordCharArray)
            //        {
            //            grid[x, y] = letter;
            //        }


            //    }
            //}

            String crozzleHTML = "";
            String style = Config.Style;

            style += @"<style>
                       .empty { background-color: " + Config.BGcolourEmptyTD + @"; }
                       .nonempty { background-color: " + Config.BGcolourNonEmptyTD + @"; }
                       </style>";

            crozzleHTML += @"<!DOCTYPE html><html><head>";
            crozzleHTML += style;
            crozzleHTML += @"</head><body>";
            crozzleHTML += @"<table>";

            for (int i = 0; i < grid.GetLength(0); i++)
            {
                crozzleHTML += @"<tr>";

                for (int j = 0; j < grid.GetLength(1); j++)
                {
                    if(grid[i,j] == 0)
                    {
                        crozzleHTML += @"<td class=""empty""></td>";
                    }
                    else
                    {
                        crozzleHTML += @"<td class=""nonempty"">" + grid[i, j] + @"</td>";
                    }
                }
                crozzleHTML += @"</tr>";
            }
            crozzleHTML += @"</table>";
            crozzleHTML += @"</body></html>";

            return crozzleHTML;

        }

        //public string ToStringHTML()
        //{
        //    String crozzleHTML = "";
        //    String style = Config.Style;

        //    style += @"<style>
        //               .empty { background-color: " + Config.BGcolourEmptyTD + @"; }
        //               .nonempty { background-color: " + Config.BGcolourNonEmptyTD + @"; }
        //               </style>";

        //    crozzleHTML += @"<!DOCTYPE html><html><head>";
        //    crozzleHTML += style;
        //    crozzleHTML += @"</head><body>";
        //    crozzleHTML += @"<table>";

        //    foreach(var val in )
        //}

    }
}
