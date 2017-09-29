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
        int Counter { get; set; }
        LetterCheck lett { get; set; }
        
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
            Counter = 0;

            
        }

        public int GetScore()
        {
            if (Grid == null)
            {
                Score = 0;
            }

            return Score;
        }

        public char[,] AddWordsToMultipleInGrid()
        {
            List<LastWordEntered> copyWordsInserted = WordsInserted.ToList();
            List<LastWordEntered> removeWordsInserted = new List<LastWordEntered>();

            foreach (var entry in copyWordsInserted)
            {
                Counter = 0;
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

                //var lastWord = WordsInserted.Find(w => w.Name == lastWordAdded);
                Dictionary<char, List<string>> inwords = entry.IntersectingWords;
                Dictionary<char, Coordinate> coordsinExistingLetter = entry.LetterCoordsForIntersectingWords;
                int i;
                bool added = false;
                bool valid = true;
                bool inwordsFinished = false;
                var Name = "";
                var aName = "";

                for (i = 0; i < inwords.Count; i++)
                {
                    if (added == false)
                    {
                        var letterInExistingWord = inwords.ElementAt(i).Key;
                        wordsThatIntersectOnAChar = inwords.ElementAt(i).Value;

                        foreach (var aWord in wordsThatIntersectOnAChar)
                        {
                            aName = aWord;
                            List<char> aNameCharList = aName.ToList();
                            List<char> charThatDontMatchExistingCharInGrid = new List<char>();

                            foreach (var letter in aNameCharList)
                            {
                                if (valid == true)
                                {
                                    int iterationsLeftOrAbove = aNameCharList.IndexOf(letter);
                                    int lengthOfWord = aNameCharList.Count;
                                    int iterationsRightOrBelow = lengthOfWord - iterationsLeftOrAbove;

                                    if (letter == letterInExistingWord)
                                    {
                                        var cords = coordsinExistingLetter[letter]; //get the coords of the letter in the existing word so we know where to start
                                        int LeftOfLetter = cords.Column - 1;  //movement points 
                                        int RightOfLetter = cords.Column + 1;
                                        int AboveLetter = cords.Row - 1;
                                        int BelowLetter = cords.Row + 1;

                                        //check if word to be inserted is a horizontal word, also lets you know if the existing word in the grid is vertical
                                        if (Grid[cords.Row, LeftOfLetter] == '\0' && Grid[cords.Row, RightOfLetter] == '\0' && cords.Row < Grid.GetUpperBound(0) && RightOfLetter < Grid.GetUpperBound(1) && RightOfLetter > 0 && cords.Row > 0)
                                        {
                                            //check the grid for empty tiles and space for the word
                                            if (iterationsLeftOrAbove == 0) //check if the intersecting letter in the word to be added is at the start
                                            {
                                                //check right of letter the letter
                                                for (int x = 0; x < iterationsRightOrBelow + 1; x++) //add an extra move to the left for white space around the letter
                                                {

                                                    if (Grid[cords.Row, RightOfLetter] == '\0' && cords.Row < Grid.GetUpperBound(0) && RightOfLetter < Grid.GetUpperBound(1) && RightOfLetter >= 0 && cords.Row >= 0 && Grid[AboveLetter, RightOfLetter] == '\0' && Grid[BelowLetter, RightOfLetter] == '\0' && AboveLetter < Grid.GetUpperBound(0) && BelowLetter < Grid.GetUpperBound(0) && AboveLetter >= 0 && BelowLetter >= 0)
                                                    {
                                                        valid = true;
                                                        RightOfLetter++;   //goin right so plus the index by 1                                                
                                                        continue;
                                                    }
                                                    else
                                                    {
                                                        valid = false;
                                                        break;
                                                    }
                                                }

                                                if (valid == true)
                                                {
                                                    var startX = cords.Row;
                                                    var startY = cords.Column - iterationsLeftOrAbove;

                                                    if (WordsInGrid.Contains(aName))
                                                    {
                                                        valid = false;

                                                    }
                                                    else
                                                    {

                                                        foreach (var c in aNameCharList)
                                                        {
                                                            if (startX < 0 || startY < 0)
                                                            {
                                                                valid = false;
                                                            }
                                                            else
                                                            {
                                                                grid[startX, startY] = c;
                                                                Coordinate co = new Coordinate(startX, startY);
                                                                cl.Add(co, c);
                                                                startY = startY + 1;
                                                            }

                                                        }

                                                        Score = Score + points[letter] + Config.PointsPerWord;  //caluclate the score
                                                        added = true;
                                                        break;
                                                    }
                                                }

                                            }
                                            else //check both right and left
                                            {
                                                //check right  of letter the letter
                                                for (int x = 0; x < iterationsRightOrBelow + 1; x++) //add an extra move to the left for white space around the letter
                                                {

                                                    if (Grid[cords.Row, RightOfLetter] == '\0' && cords.Row < Grid.GetUpperBound(0) && RightOfLetter < Grid.GetUpperBound(1) && RightOfLetter >= 0 && cords.Row >= 0 && Grid[AboveLetter, RightOfLetter] == '\0' && Grid[BelowLetter, RightOfLetter] == '\0' && AboveLetter < Grid.GetUpperBound(0) && BelowLetter < Grid.GetUpperBound(0) && AboveLetter >= 0 && BelowLetter >= 0)
                                                    {
                                                        valid = true;
                                                        RightOfLetter++;   //goin right so plus the index by 1
                                                        continue;
                                                    }
                                                    else
                                                    {
                                                        valid = false;
                                                        break;
                                                    }
                                                }

                                                //check left of letter
                                                for (int x = 0; x < iterationsLeftOrAbove + 1; x++) //add an extra move to the left for white space around the letter
                                                {
                                                    if (Grid[cords.Row, LeftOfLetter] == '\0' && cords.Row < Grid.GetUpperBound(0) && LeftOfLetter < Grid.GetUpperBound(1) && LeftOfLetter >= 0 && cords.Row >= 0 && Grid[AboveLetter, LeftOfLetter] == '\0' && Grid[BelowLetter, LeftOfLetter] == '\0' && AboveLetter < Grid.GetUpperBound(0) && BelowLetter < Grid.GetUpperBound(0) && AboveLetter >= 0 && BelowLetter >= 0)
                                                    {
                                                        valid = true;
                                                        LeftOfLetter--;   //goin left so minus the index by 1
                                                        continue;
                                                    }
                                                    else
                                                    {
                                                        valid = false;
                                                        break;
                                                    }
                                                }

                                                if (valid == true)
                                                {
                                                    var startX = cords.Row;
                                                    var startY = cords.Column - iterationsLeftOrAbove;

                                                    if (WordsInGrid.Contains(aName))
                                                    {
                                                        valid = false;
                                                    }
                                                    else
                                                    {

                                                        foreach (var c in aNameCharList)
                                                        {
                                                            if (startX < 0 || startY < 0)
                                                            {
                                                                valid = false;
                                                            }
                                                            else
                                                            {
                                                                grid[startX, startY] = c;
                                                                Coordinate co = new Coordinate(startX, startY);
                                                                cl.Add(co, c);
                                                                startY = startY + 1;
                                                            }

                                                        }

                                                        Score = Score + points[letter] + Config.PointsPerWord;  //caluclate the score
                                                        added = true;
                                                        break;
                                                    }
                                                }

                                            }
                                        }
                                        //check if word to be inserted is a vertical word, also lets you know if the existing word in the grid is horizontal
                                        else if (Grid[AboveLetter, cords.Column] == '\0' && Grid[BelowLetter, cords.Column] == '\0' && BelowLetter < Grid.GetUpperBound(0) && cords.Column < Grid.GetUpperBound(1) && BelowLetter > 0 && cords.Column > 0)
                                        {
                                            //check the grid for empty tiles and space for the word
                                            if (iterationsLeftOrAbove == 0) //check if the intersecting letter in the word to be added is at the start
                                            {
                                                //check below of the letter
                                                for (int x = 0; x < iterationsRightOrBelow + 1; x++) //add an extra move to the left for white space around the letter
                                                {
                                                    if (Grid[BelowLetter, cords.Column] == '\0' && BelowLetter < Grid.GetUpperBound(0) && cords.Column < Grid.GetUpperBound(1) && BelowLetter > 0 && cords.Column > 0 && Grid[BelowLetter, LeftOfLetter] == '\0' && Grid[BelowLetter, RightOfLetter] == '\0' && RightOfLetter < Grid.GetUpperBound(1) && LeftOfLetter < Grid.GetUpperBound(0) && LeftOfLetter > 0 && RightOfLetter > 0)
                                                    {
                                                        valid = true;
                                                        BelowLetter++;   //goin down so plus the index by 1
                                                        continue;
                                                    }
                                                    else
                                                    {
                                                        valid = false;
                                                        break;
                                                    }
                                                }

                                                if (valid == true)
                                                {
                                                    var startX = cords.Row - iterationsLeftOrAbove;
                                                    var startY = cords.Column;
                                                    if (WordsInGrid.Contains(aName))
                                                    {
                                                        valid = false;
                                                    }
                                                    else
                                                    {
                                                        foreach (var c in aNameCharList)
                                                        {
                                                            if (startX < 0 || startY < 0)
                                                            {
                                                                valid = false;
                                                            }
                                                            else
                                                            {
                                                                grid[startX, startY] = c;
                                                                Coordinate co = new Coordinate(startX, startY);
                                                                cl.Add(co, c);
                                                                startX = startX + 1;
                                                            }

                                                        }

                                                        Score = Score + points[letter] + Config.PointsPerWord;  //caluclate the score
                                                        added = true;
                                                        break;
                                                    }
                                                }



                                            }
                                            else //check both up and down
                                            {
                                                //check below of the letter
                                                for (int x = 0; x < iterationsRightOrBelow + 1; x++) //add an extra move to the left for white space around the letter
                                                {
                                                    if (Grid[BelowLetter, cords.Column] == '\0' && BelowLetter < Grid.GetUpperBound(0) && cords.Column < Grid.GetUpperBound(1) && BelowLetter > 0 && cords.Column > 0 && Grid[BelowLetter, LeftOfLetter] == '\0' && Grid[BelowLetter, RightOfLetter] == '\0' && RightOfLetter < Grid.GetUpperBound(1) && LeftOfLetter < Grid.GetUpperBound(0) && LeftOfLetter > 0 && RightOfLetter > 0)
                                                    {
                                                        valid = true;
                                                        BelowLetter++;   //goin down so plus the index by 1
                                                        continue;
                                                    }
                                                    else
                                                    {
                                                        valid = false;
                                                        break;
                                                    }
                                                }

                                                //check above of the letter
                                                for (int x = 0; x < iterationsLeftOrAbove + 1; x++) //add an extra move to the left for white space around the letter
                                                {
                                                    if (Grid[AboveLetter, cords.Column] == '\0' && AboveLetter < Grid.GetUpperBound(0) && cords.Column < Grid.GetUpperBound(1) && AboveLetter > 0 && cords.Column > 0 && Grid[AboveLetter, LeftOfLetter] == '\0' && Grid[AboveLetter, RightOfLetter] == '\0' && RightOfLetter < Grid.GetUpperBound(1) && LeftOfLetter < Grid.GetUpperBound(0) && LeftOfLetter > 0 && RightOfLetter > 0)
                                                    {
                                                        valid = true;
                                                        AboveLetter--;   //goin up so minus the index by 1
                                                        continue;
                                                    }
                                                    else
                                                    {
                                                        valid = false;
                                                        break;
                                                    }
                                                }

                                                if (valid == true)
                                                {
                                                    var startX = cords.Row - iterationsLeftOrAbove;
                                                    var startY = cords.Column;
                                                    if (WordsInGrid.Contains(aName))
                                                    {
                                                        valid = false;
                                                    }
                                                    else
                                                    {
                                                        foreach (var c in aNameCharList)
                                                        {
                                                            if (startX < 0 || startY < 0)
                                                            {
                                                                valid = false;
                                                            }
                                                            else
                                                            {
                                                                grid[startX, startY] = c;
                                                                Coordinate co = new Coordinate(startX, startY);
                                                                cl.Add(co, c);
                                                                startX = startX + 1;
                                                            }

                                                        }

                                                        Score = Score + points[letter] + Config.PointsPerWord;  //caluclate the score
                                                        added = true;
                                                        break;
                                                    }
                                                }
                                                else
                                                {
                                                    wordsThatIntersectOnAChar.Remove(Name);
                                                    break;
                                                }

                                            }


                                        }
                                        else
                                        {
                                            charThatDontMatchExistingCharInGrid.Add(letter);

                                            if (charThatDontMatchExistingCharInGrid.Count == aNameCharList.Count)
                                            {
                                                wordsThatIntersectOnAChar.Remove(Name);
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
                                        charThatDontMatchExistingCharInGrid.Add(letter);

                                        if (charThatDontMatchExistingCharInGrid.Count == aNameCharList.Count)
                                        {
                                            wordsThatIntersectOnAChar.Remove(Name);
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
                                    wordsThatIntersectOnAChar.Remove(Name);
                                    break;
                                }

                            }
                        }
                        
                    }
                    else
                    {
                        //add the name, letters and coords of each letter to the table
                        //add properties to the object
                        LetterCordsForWordsInGrid.Add(aName, cl);
                        i--;
                        var objKey = WordList.Table[aName];
                        Dictionary<char, List<string>> objDict = new Dictionary<char, List<string>>();

                        foreach (var key in objKey)
                        {
                            objDict.Add(key.Key, key.Value);
                        }

                        var cor = LetterCordsForWordsInGrid[aName];
                        Dictionary<Coordinate, char> objCor = new Dictionary<Coordinate, char>();
                        foreach (var res in cor)
                        {
                            objCor.Add(res.Key, res.Value);
                        }

                        LastWordEntered nameWord = new LastWordEntered(aName, objDict, objCor);

                        WordsInserted.Add(nameWord);

                        WordList.Table.Remove(aName);

                        WordsInGrid.Add(aName);

                        wordsThatIntersectOnAChar.Remove(aName);

                        Counter++;

                        added = false;
                    }

                }

                
                
                
            }
           
            return Grid;
        }

        //public char[,] AddWordsToOneInGrid()
        //{

        //    Counter = 0;
        //    Dictionary<char, int> points = new Dictionary<char, int>();
        //    IndexedDictionary<char, List<string>> intersectingWords = new IndexedDictionary<char, List<string>>();
        //    List<string> wordsThatIntersectOnAChar = new List<string>();
        //    char keyindic = 'A';
        //    char[,] grid = Grid;
        //    string lastWordAdded = WordsInGrid.Last();  //delete this if works
        //    CordsLetterTable cl = new CordsLetterTable();
        //    IndexOutOfRangeException outOfBounds = new IndexOutOfRangeException();

        //    //put the points table into a dictionary
        //    foreach (var val in Config.IntersectingPointsPerLetter)
        //    {
        //        points.Add(keyindic, val);
        //        keyindic++;
        //    }

            
        //    var lastWord = WordsInserted.Find(w => w.Name == lastWordAdded); //delete this 
        //    Dictionary<char, List<string>> inwords = lastWord.IntersectingWords;
        //    Dictionary<char, Coordinate> coordsinExistingLetter = lastWord.LetterCoordsForIntersectingWords;
        //    int i;
            

        //    for (i = 0; i < inwords.Count; i++)
        //    {
        //        var letterInExistingWord = inwords.ElementAt(i).Key;
        //        wordsThatIntersectOnAChar = inwords.ElementAt(i).Value;

        //        CheckNames(wordsThatIntersectOnAChar, letterInExistingWord, cl, points);

        //        if (lett.Added == false)
        //        {
        //            continue;
                   
        //        }
        //        else
        //        {
        //            //add the name, letters and coords of each letter to the table
        //            //add properties to the object
        //            LetterCordsForWordsInGrid.Add(lett.Name, cl);

        //            var objKey = WordList.Table[lett.Name];
        //            Dictionary<char, List<string>> objDict = new Dictionary<char, List<string>>();

        //            foreach (var key in objKey)
        //            {
        //                objDict.Add(key.Key, key.Value);
        //            }

        //            var cor = LetterCordsForWordsInGrid[lett.Name];
        //            Dictionary<Coordinate, char> objCor = new Dictionary<Coordinate, char>();
        //            foreach (var res in cor)
        //            {
        //                objCor.Add(res.Key, res.Value);
        //            }

        //            LastWordEntered nameWord = new LastWordEntered(lett.Name, objDict, objCor);

        //            WordsInserted.Add(nameWord);

        //            WordList.Table.Remove(lett.Name);

        //            WordsInGrid.Add(lett.Name);

        //            wordsThatIntersectOnAChar.Remove(lett.Name);

        //            Counter++;
                    
        //        }

        //    }

        //    WordsInserted.Remove(lastWord);
        //    return Grid;
        //}

        public LetterCheck CheckNames(List<string> names, char letterInExistingWord, Dictionary<char, int> points, LastWordEntered entry, string testName, List<string> namesInserted, Coordinate cords)
        {
            var aName = "";
            
            List<char> charThatDontMatchExistingCharInGrid = new List<char>();

            foreach (var aWord in names)
            {
                if(namesInserted.Contains(aWord))
                {
                    continue;
                }
                else
                {
                    aName = aWord;
                    List<char> aNameCharList = aName.ToList();

                    CheckLetters(aNameCharList, letterInExistingWord, entry, testName, aName, cords);

                    if (lett.Valid == false)
                    {
                        continue;
                    }
                    else
                    {
                        lett.Name = aName;
                        AddNameToGrid(lett, aNameCharList, points, aName);
                        return lett;
                    }
                }
                

            }

            lett.Added = false;
            return lett;
        }

        public LetterCheck CheckLetters(List<char> letterList, char letterInExistingWord, LastWordEntered entry, string testName, string aName, Coordinate cords)
        {
            foreach (var letter in letterList)
            {
                lett = new LetterCheck(letterList.IndexOf(letter), letterList.Count, letter);
                
                if (lett.Letter == letterInExistingWord)
                {
                    GetCoordinates(lett, entry, cords);
                    if(lett.Valid == false)
                    {
                        continue;
                    }
                    else
                    {
                        return lett;
                    }
                }
            }
  
           return lett;
            
            
        }

        public void AddNameToGrid(LetterCheck lett, List<char> aNameCharList, Dictionary<char, int> points, string aName)
        {
            if(lett.IsVertical == true)
            {
                
                var startX = lett.ExistingLetterRow - lett.IterationsLeftOrAbove;
                var startY = lett.ExistingLetterColumn;
                foreach (var c in aNameCharList)
                {

                    Grid[startX, startY] = c;
                    Coordinate co = new Coordinate(startX, startY);
                    lett.AllCoordinatesOfLettersInName.Add(co, c); //here cl is global and keeps adding names to it
                    startX = startX + 1;

                }

                Score = Score + points[lett.Letter] + Config.PointsPerWord;  //caluclate the score
                lett.Added = true;               
            }

            if(lett.IsHorizontal)
            {
                
                var startX = lett.ExistingLetterRow;
                var startY = lett.ExistingLetterColumn - lett.IterationsLeftOrAbove;

                foreach (var c in aNameCharList)
                {

                    Grid[startX, startY] = c;
                    Coordinate co = new Coordinate(startX, startY);
                    lett.AllCoordinatesOfLettersInName.Add(co, c);
                    startY = startY + 1;

                }

                Score = Score + points[lett.Letter] + Config.PointsPerWord;  //caluclate the score
                lett.Added = true;
            }
            
        }

        public LetterCheck GetCoordinates(LetterCheck lett, LastWordEntered entry, Coordinate cords)
        {
            
            Dictionary<Coordinate, char> coordsinExistingLetter = entry.LetterCords;



             //get the coords of the letter in the existing word so we know where to start
            lett.ExistingLetterRow = cords.Row;
            lett.ExistingLetterColumn = cords.Column;
            CoordsPointer coordsP = new CoordsPointer(cords);

            TestTheLetter(coordsP, lett);

            return lett;
        }

        public LetterCheck TestTheLetter(CoordsPointer c, LetterCheck lett)
        {
            //check the grid for empty tiles and space for the word
            if (lett.IterationsLeftOrAbove == 0) //check if the intersecting letter in the word to be added is at the start
            {
                //check if word to be inserted is a horizontal word, also lets you know if the existing word in the grid is vertical
                if (Grid[lett.ExistingLetterRow, c.RightOfLetter] == '\0' && Grid[lett.ExistingLetterRow, c.LeftOfLetter] == '\0')
                {
                    for (int x = 0; x < lett.IterationsRightOrBelow + 1; x++) //add an extra move to the left for white space around the letter
                    {
                        if (Grid[lett.ExistingLetterRow, c.RightOfLetter] == '\0' && Grid[c.AboveLetter, c.RightOfLetter] == '\0' && Grid[c.BelowLetter, c.RightOfLetter] == '\0')
                        {
                            lett.Valid = true;
                            lett.IsHorizontal = true;
                            c.RightOfLetter++;   //goin right so plus the index by 1                                                
                            continue;
                        }
                        else
                        {
                            lett.Valid = false;
                            return lett;
                        }
                    }

                    return lett;
                }
                //check if word to be inserted is a vertical word, also lets you know if the existing word in the grid is horizontal
                if (Grid[c.BelowLetter, lett.ExistingLetterColumn] == '\0' && Grid[c.AboveLetter, lett.ExistingLetterColumn] == '\0')
                {
                    for (int x = 0; x < lett.IterationsRightOrBelow + 1; x++) //add an extra move to the left for white space around the letter
                    {
                        if (Grid[c.BelowLetter, lett.ExistingLetterColumn] == '\0' && Grid[c.BelowLetter, c.LeftOfLetter] == '\0' && Grid[c.BelowLetter, c.RightOfLetter] == '\0')
                        {
                            lett.Valid = true;
                            lett.IsVertical = true;
                            c.BelowLetter++;   //goin down so plus the index by 1
                            continue;
                        }                       
                        else
                        {
                            lett.Valid = false;
                            return lett;
                        }
                    }

                    return lett;
                }
            }

                //check if word to be inserted is a horizontal word, also lets you know if the existing word in the grid is vertical
            if (Grid[lett.ExistingLetterRow, c.LeftOfLetter] == '\0' && Grid[lett.ExistingLetterRow, c.RightOfLetter] == '\0')
            {
                //check right  of letter the letter
                for (int x = 0; x < lett.IterationsRightOrBelow + 1; x++) //add an extra move to the left for white space around the letter
                {

                    if (Grid[lett.ExistingLetterRow, c.RightOfLetter] == '\0' && Grid[c.AboveLetter, c.RightOfLetter] == '\0' && Grid[c.BelowLetter, c.RightOfLetter] == '\0')
                    {
                        lett.Valid = true;
                        lett.IsHorizontal = true;
                        c.RightOfLetter++;   //goin right so plus the index by 1
                        continue;
                    }
                    else
                    {
                        lett.Valid = false;
                        return lett;
                    }
                }

                //check left of letter
                for (int x = 0; x < lett.IterationsLeftOrAbove + 1; x++) //add an extra move to the left for white space around the letter
                {


                    if (Grid[lett.ExistingLetterRow, c.LeftOfLetter] == '\0' && Grid[c.AboveLetter, c.LeftOfLetter] == '\0' && Grid[c.BelowLetter, c.LeftOfLetter] == '\0')
                    {
                        lett.Valid = true;
                        lett.IsHorizontal = true;
                        c.LeftOfLetter--;   //goin left so minus the index by 1
                        continue;
                    }
                    else
                    {
                        lett.Valid = false;
                        return lett;
                    }
                }


            }
            //check if word to be inserted is a vertical word, also lets you know if the existing word in the grid is horizontal
            else if (Grid[c.AboveLetter, lett.ExistingLetterColumn] == '\0' && Grid[c.BelowLetter, lett.ExistingLetterColumn] == '\0')
            {
                //check below of the letter
                for (int x = 0; x < lett.IterationsRightOrBelow + 1; x++) //add an extra move to the left for white space around the letter
                {
                    if (Grid[c.BelowLetter, lett.ExistingLetterColumn] == '\0' && Grid[c.BelowLetter, c.LeftOfLetter] == '\0' && Grid[c.BelowLetter, c.RightOfLetter] == '\0')
                    {
                        lett.Valid = true;
                        lett.IsVertical = true;
                        c.BelowLetter++;   //goin down so plus the index by 1
                        continue;
                    }
                    else
                    {
                        lett.Valid = false;
                        return lett;
                    }
                }

                //check above of the letter
                for (int x = 0; x < lett.IterationsLeftOrAbove + 1; x++) //add an extra move to the left for white space around the letter
                {
                    if (Grid[c.AboveLetter, lett.ExistingLetterColumn] == '\0' && Grid[c.AboveLetter, c.LeftOfLetter] == '\0' && Grid[c.AboveLetter, c.RightOfLetter] == '\0')
                    {
                        lett.Valid = true;
                        lett.IsVertical = true;
                        c.AboveLetter--;   //goin up so minus the index by 1
                        continue;
                    }
                    else
                    {
                        lett.Valid = false;
                        return lett;
                    }
                }

            }

            return lett;
        }

        public char[,] AddWordToGrid()
        {
            List<LastWordEntered> copyWordsInserted = WordsInserted.ToList();
            List<string> namesInserted = new List<string>();
            foreach (var n in copyWordsInserted)
            {
                namesInserted.Add(n.Name);
            }

            foreach (var entry in copyWordsInserted)
            {
                string nameEntered = entry.Name;
                Counter = 0;
                Dictionary<char, int> points = new Dictionary<char, int>();
                IndexedDictionary<char, List<string>> intersectingWords = new IndexedDictionary<char, List<string>>();
                List<string> wordsThatIntersectOnAChar = new List<string>();
                char keyindic = 'A';
                char[,] grid = Grid;
                
                IndexOutOfRangeException outOfBounds = new IndexOutOfRangeException();

                //put the points table into a dictionary
                foreach (var val in Config.IntersectingPointsPerLetter)
                {
                    points.Add(keyindic, val);
                    keyindic++;
                }

                Dictionary<Coordinate, char> allCoordsInGridWords = entry.LetterCords;
                Dictionary<char, List<string>> inwords = entry.IntersectingWords;
                Dictionary<char, Coordinate> coordsinExistingLetter = entry.LetterCoordsForIntersectingWords;
                int i;


                for (i = 0; i < allCoordsInGridWords.Count; i++)
                {
                    var testName = entry.Name;
                    var letterInExistingWord = allCoordsInGridWords.ElementAt(i).Value;
                    var cords = allCoordsInGridWords.ElementAt(i).Key;
                    wordsThatIntersectOnAChar = inwords[letterInExistingWord];

                    CheckNames(wordsThatIntersectOnAChar, letterInExistingWord, points, entry, testName, namesInserted, cords);

                    
                    if (lett.Added == false)
                    {
                        continue;

                    }
                    else
                    {
                        //add the name, letters and coords of each letter to the table
                        //add properties to the object
                        

                        var objKey = WordList.Table[lett.Name];
                        Dictionary<char, List<string>> objDict = new Dictionary<char, List<string>>();

                        foreach (var key in objKey)
                        {
                            objDict.Add(key.Key, key.Value);
                        }

                        var cor = lett.AllCoordinatesOfLettersInName;
                        Dictionary<Coordinate, char> objCor = new Dictionary<Coordinate, char>();
                        foreach (var res in cor)
                        {
                            objCor.Add(res.Key, res.Value);
                        }

                        LastWordEntered nameWord = new LastWordEntered(lett.Name, objDict, objCor);

                        WordsInserted.Add(nameWord);

                        WordList.Table.Remove(lett.Name);

                        WordsInGrid.Add(lett.Name);

                        wordsThatIntersectOnAChar.Remove(lett.Name);

                        Counter++;

                    }

                }

                WordsInserted.Remove(entry);
                continue;
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
            int x = 2;
            int y = 2;



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

        public string DisplayGrid(CrozzleGrid game)
        {
            char[,] grid = Grid;
            String crozzleHTML = "";
            String style = Config.Style;

            style += @"<style>
                       .empty { background-color: " + Config.BGcolourEmptyTD + @"; }
                       .nonempty { background-color: " + Config.BGcolourNonEmptyTD + @"; }
                       .border { background-color: black; }
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
                    if(grid[i,j] == '*')
                    {
                        crozzleHTML += @"<td hidden class=""border""></td>";
                    }
                    else if (grid[i, j] == 0)
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

            if(game.Counter == 0)
            {
                crozzleHTML += @"<p>Cannot add any more words.</p>";
            }

            crozzleHTML += @"</body></html>";

            return crozzleHTML;
        }

        public char[,] CreateGrid()
        {
            var rows = Convert.ToInt16(RowsAndColumns.Rows) + 1;
            var cols = Convert.ToInt16(RowsAndColumns.Columns) + 1;

            char[,] grid = new char[rows, cols];

            for(int i = 0; i < rows; i++)
            {
                if(i == 0 || i == rows - 1)
                {
                    for (int j = 0; j < cols; j++)
                    {
                        grid[i, j] = '*';
                    }
                }

                if(i >= 1 && i <= rows - 2)
                {
                    for (int j = 0; j < cols; j++)
                    {
                        if(j == 0 || j == cols - 1)
                        {
                            grid[i, j] = '*';
                        }
                        else if(j >= 1 && j <= cols - 2)
                        {
                            grid[i, j] = '\0';
                        }
                    }
                }
                
            }
            

            return grid;

        }


    }
}
