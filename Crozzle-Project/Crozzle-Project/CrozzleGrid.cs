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
        int index { get; set; }
        List<LastWordEntered> WordsInserted { get; set; }
        
        public CrozzleGrid Copy()
        {
            return (CrozzleGrid)this.MemberwiseClone();
        }

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
            WordsInserted = new List<LastWordEntered>();
            index = 0;
            
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
            //CrozzleGrid gridCopy = g.Copy();            
            Dictionary<char, int> points = new Dictionary<char, int>();
            IndexedDictionary<char, List<string>> intersectingWords = new IndexedDictionary<char, List<string>>();
            List<string> wordsThatIntersectOnAChar = new List<string>();
            char keyindic = 'A';
            char[,] grid = Grid;            
            string lastWordAdded = WordsInGrid.Last();
            CordsLetterTable cl = new CordsLetterTable();
            IndexOutOfRangeException outOfBounds = new IndexOutOfRangeException();

            //put the points table into a dictionary
            foreach (var val in Config.IntersectingPointsPerLetter)
            {
                points.Add(keyindic, val);
                keyindic++;
            }

            var lastWord = WordsInserted.Find(w => w.Name == lastWordAdded);

            Dictionary<char, List<string>> inwords = lastWord.IntersectingWords;
            Dictionary<char, Coordinate> coordsinExistingLetter = lastWord.LetterCoordsForIntersectingWords;
            int i;
            bool added = false;
            bool valid = true;
            bool finished = false;
            var Name = "";


            //for(i = 0; i < inwords.Count; i++)
            foreach (var character in inwords) //the intersecting letters of word already on the grid and coords of those letters
            {
                //added = false;
                var charInExistingWord = character.Key; //the intersecting letter
                

                //while (added == false) //while no word has been added to the grid
                //{
                    wordsThatIntersectOnAChar = character.Value; //the list of words from the wordlist that have a letter tat intersects to existing word

                    if(wordsThatIntersectOnAChar[0] == lastWord.Name)
                    {
                        wordsThatIntersectOnAChar.Remove(lastWord.Name);
                    }

                    Name = wordsThatIntersectOnAChar[0];
                    List<char> nameCharArraay = Name.ToList();  //take the word and split to a char array
                    List<char> badLetters = new List<char>();

                    foreach (var letter in nameCharArraay)  //each letter in the char array
                    {
                        var lett = letter;  //the letter
                        int iterationsLeftOrAbove = nameCharArraay.IndexOf(lett);  //index number of letter in char array
                        int lengthOfWord = nameCharArraay.Count;
                        int iterationsRightOrBelow = lengthOfWord - iterationsLeftOrAbove;

                        if (lett == charInExistingWord)  //does the active letter match the active letter in the existing word
                        {
                            //if it does match


                            var cords = coordsinExistingLetter[letter]; //get the coords of the letter in the existing word so we know where to start
                            int LeftOfLetter = cords.Column - 1;  //movement points 
                            int RightOfLetter = cords.Column + 1;
                            int AboveLetter = cords.Row - 1;
                            int BelowLetter = cords.Row + 1;

                            //check if word to be inserted is a horizontal word, also lets you know if the existing word in the grid is vertical
                            if (Grid[cords.Row, LeftOfLetter] == '\0' && Grid[cords.Row, RightOfLetter] == '\0' && cords.Row < Grid.GetUpperBound(0) && RightOfLetter < Grid.GetUpperBound(1) && RightOfLetter > 0 && cords.Row > 0)
                            {
                                if (valid == true)
                                {
                                    //check the grid for empty tiles and space for the word
                                    if (iterationsLeftOrAbove == 0) //check if the intersecting letter in the word to be added is at the start
                                    {
                                        //check right the right side of the letter
                                        for (int x = 0; x < iterationsRightOrBelow + 1; x++) //add an extra move to the left for white space around the letter
                                        {

                                            if (Grid[cords.Row, RightOfLetter] == '\0' && cords.Row < Grid.GetUpperBound(0) && RightOfLetter < Grid.GetUpperBound(1) && RightOfLetter > 0 && cords.Row > 0)
                                            {
                                                valid = true;
                                                RightOfLetter++;   //goin right so plus the index by 1
                                                continue;
                                            }
                                            else
                                            {
                                                valid = false;
                                            }
                                        }
                                    }
                                    else // if not check both right and left side
                                    {
                                        //check left the left side of the letter
                                        for (int x = 0; x < iterationsLeftOrAbove + 1; x++) //add an extra move to the left for white space around the letter
                                        {

                                            if (Grid[cords.Row, LeftOfLetter] == '\0' && cords.Row < Grid.GetUpperBound(0) && LeftOfLetter < Grid.GetUpperBound(1) && LeftOfLetter > 0 && cords.Row > 0)
                                            {
                                                valid = true;
                                                LeftOfLetter--;   //goin left so minus the index by 1
                                                continue;
                                            }
                                            else
                                            {
                                                valid = false;
                                            }
                                        }

                                        //check right the right side of the letter
                                        for (int x = 0; x < iterationsRightOrBelow + 1; x++) //add an extra move to the left for white space around the letter
                                        {

                                            if (Grid[cords.Row, RightOfLetter] == '\0' && cords.Row < Grid.GetUpperBound(0) && RightOfLetter < Grid.GetUpperBound(1) && RightOfLetter > 0 && cords.Row > 0)
                                            {
                                                valid = true;
                                                RightOfLetter++;   //goin right so plus the index by 1
                                                continue;
                                            }
                                            else
                                            {
                                                valid = false;
                                            }
                                        }
                                    }

                                    var startX = cords.Row;
                                    var startY = cords.Column - iterationsLeftOrAbove;

                                    if(startX < 0 || startY < 0)
                                    {
                                        valid = false;
                                    }
                                    else
                                    {
                                        foreach (var c in nameCharArraay)
                                        {
                                            grid[startX, startY] = c;
                                            Coordinate co = new Coordinate(startX, startY);
                                            cl.Add(co, c);
                                            startY = startY + 1;
                                        }

                                        Score = Score + points[letter] + Config.PointsPerWord;  //caluclate the score
                                        added = true;
                                    }
                                    

                                }
                                else
                                {
                                    wordsThatIntersectOnAChar.Remove(Name);
                                   // AddWordToGrid();
                                }


                            }

                            //check if word to be inserted is a vertical word, also lets you know if the existing word in the grid is horizontal
                            if (Grid[AboveLetter, cords.Column] == '\0' && Grid[BelowLetter, cords.Column] == '\0' && BelowLetter < Grid.GetUpperBound(0) && cords.Column < Grid.GetUpperBound(1) && BelowLetter > 0 && cords.Column > 0)
                            {
                                if (valid == true)
                                {
                                    //check the grid for empty tiles and space for the word
                                    if (iterationsLeftOrAbove == 0) //check if the intersecting letter in the word to be added is at the start
                                    {
                                        //check below of the letter
                                        for (int x = 0; x < iterationsRightOrBelow + 1; x++) //add an extra move to the left for white space around the letter
                                        {

                                            if (Grid[BelowLetter, cords.Column] == '\0' && BelowLetter < Grid.GetUpperBound(0) && cords.Column < Grid.GetUpperBound(1) && BelowLetter > 0 && cords.Column > 0)
                                            {
                                                valid = true;
                                                BelowLetter++;   //goin down so plus the index by 1
                                                continue;
                                            }
                                            else
                                            {
                                                valid = false;
                                            }
                                        }
                                    }
                                    else // if not check both below and above
                                    {
                                        //check above
                                        for (int x = 0; x < iterationsLeftOrAbove + 1; x++) //add an extra move to the left for white space around the letter
                                        {

                                            if (Grid[AboveLetter, cords.Column] == '\0' && AboveLetter < Grid.GetUpperBound(0) && cords.Column < Grid.GetUpperBound(1) && AboveLetter > 0 && cords.Column > 0)
                                            {
                                                valid = true;
                                                AboveLetter--;   //goin up so minus the index by 1
                                                continue;
                                            }
                                            else
                                            {
                                                valid = false;
                                            }
                                        }

                                        //check below of the letter
                                        for (int x = 0; x < iterationsRightOrBelow + 1; x++) //add an extra move to the left for white space around the letter
                                        {

                                            if (Grid[BelowLetter, cords.Column] == '\0' && BelowLetter < Grid.GetUpperBound(0) && cords.Column < Grid.GetUpperBound(1) && BelowLetter > 0 && cords.Column > 0)
                                            {
                                                valid = true;
                                                BelowLetter++;   //goin down so plus the index by 1
                                                continue;
                                            }
                                            else
                                            {
                                                valid = false;
                                            }
                                        }
                                    }

                                    var startX = cords.Row - iterationsLeftOrAbove;
                                    var startY = cords.Column;

                                    if (startX < 0 || startY < 0)
                                    {
                                        valid = false;
                                    }
                                    else
                                    {
                                        foreach (var c in nameCharArraay)
                                        {
                                            grid[startX, startY] = c;
                                            Coordinate co = new Coordinate(startX, startY);
                                            cl.Add(co, c);
                                            startX = startX + 1;
                                        }

                                        Score = Score + points[letter] + Config.PointsPerWord;  //caluclate the score
                                        added = true;
                                    }
                                }
                                else
                                {
                                    wordsThatIntersectOnAChar.Remove(Name);
                                    //AddWordToGrid();
                                }


                            }
                            else
                            {
                                badLetters.Add(lett);
                                if (badLetters.Count == nameCharArraay.Count)
                                {
                                    break;
                                }
                                else
                                {
                                    continue;
                                }
                                
                            }

                        }
                        else
                        {
                            badLetters.Add(lett);
                            if (badLetters.Count == nameCharArraay.Count)
                            {
                                break;
                            }
                            else
                            {
                                continue;
                            }
                        }
                    }

               // }

                wordsThatIntersectOnAChar.Remove(Name);

                if (added == true)
                {
                    //add the name, letters and coords of each letter to the table
                    //add properties to the object
                    LetterCordsForWordsInGrid.Add(Name, cl);

                    

                    var objKey = WordList.Table[Name];
                    Dictionary<char, List<string>> objDict = new Dictionary<char, List<string>>();

                    foreach (var key in objKey)
                    {
                        objDict.Add(key.Key, key.Value);
                    }

                    var cor = LetterCordsForWordsInGrid[Name];
                    Dictionary<Coordinate, char> objCor = new Dictionary<Coordinate, char>();
                    foreach (var res in cor)
                    {
                        objCor.Add(res.Key, res.Value);
                    }

                    LastWordEntered nameWord = new LastWordEntered(Name, objDict, objCor);

                    WordsInserted.Add(nameWord);

                    WordList.Table.Remove(Name);

                    WordsInGrid.Add(Name);

                    wordsThatIntersectOnAChar.Remove(Name);

                    return Grid;
                    
                }



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

            LastWordEntered wordObj = new LastWordEntered(highestWord, objDict, objCor);

            WordsInserted.Add(wordObj);

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
