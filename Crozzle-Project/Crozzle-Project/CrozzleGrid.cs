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
        char[,] Grid { get; set; }
        int Score { get; set; }
        CordsWordTable LetterCordsForWordsInGrid { get; set; }
        string RootWord { get; set; }
        List<string> WordsInGrid { get; set; }
        LastWordEntered LastwordEnter { get; set; }

        public CrozzleGrid(Configuration config, WordListTaskTwo wordlist, CrozzleTaskTwo rowsandcolumns)
        {
            Config = config;
            WordList = wordlist;
            RowsAndColumns = rowsandcolumns;
            Grid = CreateGrid();
            Score = GetScore();
            LetterCordsForWordsInGrid = new CordsWordTable();
            RootWord = "";
            WordsInGrid = new List<string>();
            LastwordEnter = null;
        }

        public int GetScore()
        {
            if (Grid == null)
            {
                Score = 0;
            }

            return Score;
        }

        public char[,] AddWordToGrid()
        {
            Dictionary<char, int> points = new Dictionary<char, int>();
            IndexedDictionary<char, List<string>> intersectingWords = new IndexedDictionary<char, List<string>>();
            List<string> wordsThatIntersectOnAChar = new List<string>();
            char keyindic = 'A';
            char[,] grid = Grid;            
            string lastWordAdded = WordsInGrid.Last();

            //put the points table into a dictionary
            foreach (var val in Config.IntersectingPointsPerLetter)
            {
                points.Add(keyindic, val);
                keyindic++;
            }

            Dictionary<char, List<string>> inwords = LastwordEnter.IntersectingWords;
            Dictionary<char, Coordinate> coordsinExistingLetter = LastwordEnter.LetterCoordsForIntersectingWords;
            int i = 0;
            bool added = false;
            int index = 0;

            foreach (var chararcter in inwords)
            {
                intersectingWords.Add(chararcter.Key, chararcter.Value);
            }

            var characterActive = 
            wordsThatIntersectOnAChar = intersectingWords[index] as List<string>;
            char[] nameCharAraay = wordsThatIntersectOnAChar[index].ToCharArray();

            while (i < nameCharAraay.Length && added == false)
            {
                var keyInDic = intersectingWords.GetKeyByIndex(index);
                var letter = nameCharAraay[i];

                if(letter == keyInDic)
                {                   
                    if(coordsinExistingLetter.ContainsKey(letter))
                    {
                        Score = points[letter] + Config.PointsPerWord;
                        var cords = coordsinExistingLetter[letter];
                        var indexOfLetterInIntersectingWord = i;
                        var row = cords.Row;
                        var startX = row - i;
                        var startY = cords.Column;

                        foreach (var c in nameCharAraay)
                        {
                            grid[startX, startY] = c;
                            startX = startX + 1;
                        }

                        added = true;
                    }
                }
                
                i++;

            }

            

            return Grid;
        }

        public char[,] AddRootWord()
        {
            var dictList = WordList.Table;
            Dictionary<string, int> bestWordDict = new Dictionary<string, int>();
            Dictionary<char, int> points = new Dictionary<char, int>();
            int scoreWords = 0;
            CordsLetterTable cl = new CordsLetterTable();            
            char keyindic = 'A';

            //put the points table into a dictionary
            foreach (var val in Config.IntersectingPointsPerLetter)
            {
                points.Add(keyindic, val);
                keyindic++;
            }

            //add names to dictionary with score of intersecting letters
            foreach (var k in dictList)
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

            //find the highest scoring word
            string highestWord = bestWordDict.Max(s => s.Key);

            //create a char arrayu of the name
            char[] wordCharArray = highestWord.ToCharArray();

            //set the cords of the grid so the word is placed in the centre
            int x = Grid.GetLength(0) / 2;
            int y = (Grid.GetLength(1) - highestWord.Length) / 2;

            //add the word to the grid
            foreach (var letter in wordCharArray)
            {
                Grid[x, y] = letter;
                Coordinate c = new Coordinate(x, y);
                cl.Add(c, letter);                
                y = y + 1;
            }

            //add the name, letters and coords of each letter to the table
            //add properties to the object
            LetterCordsForWordsInGrid.Add(highestWord, cl);

            Score = LetterCordsForWordsInGrid.Keys.Count * Config.PointsPerWord;

            var objKey = WordList.Table[highestWord];
            Dictionary<char, List<string>> objDict = new Dictionary<char, List<string>>();
            
            foreach(var key in objKey)
            {
                objDict.Add(key.Key, key.Value);
            }

            var cor = LetterCordsForWordsInGrid[highestWord];
            Dictionary<Coordinate, char> objCor = new Dictionary<Coordinate, char>();
            foreach(var res in cor)
            {
                objCor.Add(res.Key, res.Value);
            }

            LastwordEnter = new LastWordEntered(highestWord, objDict, objCor);

            WordList.Table.Remove(highestWord);

            RootWord = highestWord;

            WordsInGrid.Add(highestWord);

            return Grid;

        }

        public string DisplayGrid()
        {
            char[,] grid = Grid;
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
                    if (grid[i, j] == 0)
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

            crozzleHTML += @"<p>Score = " + Score + @"</p>";

            crozzleHTML += @"</body></html>";

            return crozzleHTML;
        }

        public char[,] CreateGrid()
        {
            char[,] grid = new char[Convert.ToInt16(RowsAndColumns.Rows), Convert.ToInt16(RowsAndColumns.Columns)];

            return grid;

        }


    }
}
