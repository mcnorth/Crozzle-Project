using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Specialized;

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
            var dictList = WordList.Table;
            char keyindic = 'A';

            //put the points table into a dictionary
            foreach (var val in Config.IntersectingPointsPerLetter)
            {
                points.Add(keyindic, val);
                keyindic++;
            }

            int scoreWords = 0;

            Dictionary<string, int> bestWordDict = new Dictionary<string, int>();
            OrderedDictionary intersectingWords = new OrderedDictionary();
            Dictionary<Coordinate, char> letterCordsforWordsInGrid = new Dictionary<Coordinate, char>(); 

            //var longestWord = WordList.Table.Keys.OrderByDescending(s => s.Length)
            //       .ThenBy(s => s)
            //       .FirstOrDefault();

            

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

            int xh = grid.GetLength(0) / 2;
            int yh = (grid.GetLength(1) - highestWord.Length) / 2;

            int x = grid.GetLength(0);
            int y = grid.GetLength(1);

            foreach (var letter in wordCharArray)
            {
                grid[xh, yh] = letter;
                Coordinate c = new Coordinate(xh, yh);
                letterCordsforWordsInGrid.Add(c, letter);
                yh = yh + 1;
            }
            WordInGrid n = new WordInGrid(highestWord, letterCordsforWordsInGrid);

            if(dictList.ContainsKey(highestWord))
            {
                var inndic = dictList[highestWord];
                var keysOfInnerDic = inndic.Keys;

                foreach(var key in inndic)
                {
                    intersectingWords.Add(key.Key, key.Value);
                }
            }

            int index = 0;          
            List<string> listOfNames = intersectingWords[index] as List<string>;
            char[] nameCharAraay = listOfNames[index].ToCharArray();
            int a = 0;
            bool added = false;

            while(a < nameCharAraay.Length && added == false)
            {
                var let = nameCharAraay[a];
                if (n.NameCharArrayCords.ContainsValue(let))
                {
                    var nameOfObj = n.NameCharArrayCords;
                    var cords = nameOfObj.FirstOrDefault(b => b.Value == let).Key;
                    var indexOfLetterInIntersectingWord = a;
                    var row = cords.Row;
                    var startX = row - a;
                    var startY = cords.Column;

                    foreach (var c in nameCharAraay)
                    {
                        grid[startX, startY] = c;
                        startX = startX + 1;
                    }

                    added = true;
                }

                a++;
            }

            //for (int i = 0; i < nameCharAraay.Length; i++)
            //{
            //    var let = nameCharAraay[i];
            //    if (n.NameCharArrayCords.ContainsValue(let))
            //    {
            //        var nameOfObj = n.NameCharArrayCords;
            //        var cords = nameOfObj.FirstOrDefault(b => b.Value == let).Key;
            //        var indexOfLetterInIntersectingWord = i;
            //        var row = cords.Row;
            //        var startX = row - i;
            //        var startY = cords.Column;

            //        foreach (var c in nameCharAraay)
            //        {
            //            grid[startX, startY] = c;
            //            startX = startX + 1;
            //        }

            //    }

            //    continue;
            //}
                //foreach (var letter in intersectingWords.Keys)
                //{
                //    var listOfnames = intersectingWords[letter];

                //    foreach (var name in listOfnames)
                //    {
                //        char[] nameCharAraay = name.ToCharArray();

                //        for (int i = 0; i < nameCharAraay.Length; i++)
                //        {
                //            var let = nameCharAraay[i];
                //            if (n.NameCharArrayCords.ContainsValue(let))
                //            {
                //                var nameOfObj = n.NameCharArrayCords;
                //                var cords = nameOfObj.FirstOrDefault(b => b.Value == let).Key;
                //                var indexOfLetterInIntersectingWord = i;
                //                var row = cords.Row;
                //                var startX = row - i;
                //                var startY = cords.Column;

                //                foreach (var c in nameCharAraay)
                //                {
                //                    grid[startX, startY] = c;
                //                    startX = startX + 1;
                //                }
                //            }




                //        }


                //    }


                //}









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
