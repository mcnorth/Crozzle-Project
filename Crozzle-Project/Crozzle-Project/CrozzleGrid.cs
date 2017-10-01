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
        public int Score { get; set; }
        CordsWordTable LetterCordsForWordsInGrid { get; set; }
        string RootWord { get; set; }
        List<string> WordsInGrid { get; set; }
        int index { get; set; }
        List<LastWordEntered> WordsInserted { get; set; }
        List<Word> WordsInsertedInGrid { get; set; }
        int Counter { get; set; }
        LetterCheck lett { get; set; }
        WordTable WordListInUse { get; set; }
        List<string> PreviousRootWords { get; set; }
        List<string> NamesList { get; set; }

        static Random rnd = new Random();
        

        public CrozzleGrid(Configuration config, WordListTaskTwo wordlist, CrozzleTaskTwo rowsandcolumns)
        {
            Config = config;
            WordList = wordlist;
            RowsAndColumns = rowsandcolumns;
            Grid = CreateGrid();
            Score = GetScore();
            LetterCordsForWordsInGrid = new CordsWordTable();
            RootWord = "";
            PreviousRootWords = new List<string>();
            WordsInGrid = new List<string>();
            WordsInserted = new List<LastWordEntered>();
            WordsInsertedInGrid = new List<Word>();
            index = 0;
            Counter = 0;
            WordListInUse = WordList.Table;
            NamesList = new List<string>();
        }

        public CrozzleGrid(CrozzleGrid copy, WordListTaskTwo wordCopy)
        {
            this.Config = copy.Config;
            this.WordList = wordCopy; //this needs to be a fresh wordlist minus the rootword in the previous grids
            this.RowsAndColumns = copy.RowsAndColumns;
            this.Grid = CreateGrid();
            this.Score = GetScore();
            this.LetterCordsForWordsInGrid = new CordsWordTable();
            this.RootWord = "";
            this.WordsInGrid = new List<string>();
            this.WordsInserted = new List<LastWordEntered>();
            this.WordsInsertedInGrid = new List<Word>();
            this.index = 0;
            this.Counter = 0;
            this.WordListInUse = WordList.Table;
            this.PreviousRootWords = copy.PreviousRootWords;
            this.NamesList = new List<string>();

        }


        public string GetRootWord()
        {
            return RootWord;
        }

        public int GetScore()
        {
            if (Grid == null)
            {
                Score = 0;
            }

            return Score;
        }

        public int GetCounter()
        {
            return Counter;
        }

        public int GetWordlistCount()
        {
            return WordList.Table.Count;
        }

        

        public LetterCheck CheckNames(List<string> names, char letterInExistingWord, Dictionary<char, int> points, LastWordEntered entry, string testName, List<string> namesInserted, Coordinate cords)
        {
            var aName = "";
            
            List<char> charThatDontMatchExistingCharInGrid = new List<char>();

            foreach (var aWord in names)
            {
                if(WordsInGrid.Contains(aWord))
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
                    lett.AllCoordinatesOfLettersInName.Add(co, c); 
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
                if (c.RightOfLetter < Grid.GetUpperBound(1) && Grid[lett.ExistingLetterRow, c.RightOfLetter] == '\0' && Grid[lett.ExistingLetterRow, c.LeftOfLetter] == '\0')
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
                if (lett.ExistingLetterColumn < Grid.GetUpperBound(1) && Grid[c.BelowLetter, lett.ExistingLetterColumn] == '\0' && Grid[c.AboveLetter, lett.ExistingLetterColumn] == '\0')
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
            if (c.RightOfLetter < Grid.GetUpperBound(1) && Grid[lett.ExistingLetterRow, c.LeftOfLetter] == '\0' && Grid[lett.ExistingLetterRow, c.RightOfLetter] == '\0')
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
            else if (lett.ExistingLetterColumn < Grid.GetUpperBound(1) && Grid[c.AboveLetter, lett.ExistingLetterColumn] == '\0' && Grid[c.BelowLetter, lett.ExistingLetterColumn] == '\0')
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

        public char[,] AddFirstWord()
        {            
            Dictionary<char, int> points = new Dictionary<char, int>();
            Dictionary<string, int> HighestScoreWordList = new Dictionary<string, int>();
            char keyindic = 'A';
            int scoreWords = 0;

            //put the points table into a dictionary
            foreach (var val in Config.IntersectingPointsPerLetter)
            {
                points.Add(keyindic, val);
                keyindic++;
            }

            //add names to dictionary with the highest score of intersecting letters
            foreach (var k in WordListInUse)
            {
                var letters = k.Value;
                foreach (var l in letters)
                {
                    if (points.ContainsKey(l.Key))
                    {
                        scoreWords = scoreWords + points[l.Key];
                    }
                }

                HighestScoreWordList.Add(k.Key, scoreWords);
                scoreWords = 0;
            }

            //find the highest scoring word
            string highestWord = HighestScoreWordList.Max(s => s.Key);

            Word hWord = new Word(highestWord);
            CreateWord(hWord);

            //set the cords of the grid so the word is placed in the centre
            int x = 2;
            int y = 2;

            //add the word to the grid
            foreach (var letter in hWord.Name)
            {
                Grid[x, y] = letter;
                Coordinate c = new Coordinate(x, y);
                hWord.NameCords.Add(c, letter);
                y = y + 1;
            }

            hWord.IsHorizontal = true;
            WordsInsertedInGrid.Add(hWord);
            RootWord = hWord.Name;


            return Grid;
        }

        public char[,] AddNameToGrid()
        {
            Dictionary<char, int> points = new Dictionary<char, int>();
            char keyindic = 'A';
            List<string> ListOfIntersectingNamesPerLetter = new List<string>();

            //put the points table into a dictionary
            foreach (var val in Config.IntersectingPointsPerLetter)
            {
                points.Add(keyindic, val);
                keyindic++;
            }

            foreach(var entry in WordsInsertedInGrid)
            {
                foreach (var obj in entry.NameCords)
                {                    
                    var letter = obj.Value;
                    var letterCords = obj.Key;
                    ListOfIntersectingNamesPerLetter = entry.ListOfIntersectingWordsPerLetter[letter];
                    CheckNameInList(ListOfIntersectingNamesPerLetter, letter, entry, letterCords);
                }
            }


            return Grid;
        }

        public Word CheckNameInList(List<string> ListOfIntersectingNamesPerLetter, char letterInExistingWord, Word entry, Coordinate letterCords)
        {
            Word aName;

            foreach(var name in ListOfIntersectingNamesPerLetter)
            {
                aName = new Word(name);
                CreateWord(aName);
                CheckLettersInName(aName, letterInExistingWord, entry, letterCords);

                
            }

            return entry;
        }

        public Word CheckLettersInName(Word aName, char letterInExistingWord, Word entry, Coordinate letterCords)
        {
            var letterList = aName.LettersInWord.ToList();

            foreach(var letter in letterList)
            {
                LetterAttributes lett = new LetterAttributes(letterList.IndexOf(letter), letterList.Count, letter);
                lett.ExistingLetterRow = letterCords.Row;
                lett.ExistingLetterColumn = letterCords.Column;
                CoordsPointer coordsP = new CoordsPointer(letterCords);

                if (lett.Letter == letterInExistingWord)
                {
                    TestTheLetterInWord(lett, coordsP, aName, entry);
                }
                else
                {
                    continue;
                }
            }

            return entry;
        }

        public Word TestTheLetterInWord(LetterAttributes lett, CoordsPointer c, Word aName, Word entry)
        {
            //check the grid for empty tiles and space for the word
            if (lett.IterationsLeftOrAbove == 0) //check if the intersecting letter in the word to be added is at the start
            {
                if(entry.IsHorizontal == true)
                {
                    for (int x = 0; x < lett.IterationsRightOrBelow + 1; x++) //add an extra move to the left for white space around the letter
                    {
                        //check below the letter
                        if (Grid[c.BelowLetter, lett.ExistingLetterColumn] == '\0' && Grid[c.BelowLetter, c.LeftOfLetter] == '\0' && Grid[c.BelowLetter, c.RightOfLetter] == '\0')
                        {
                            aName.IsValid = true;
                            aName.IsVertical = true;
                            c.BelowLetter++;   //goin down so plus the index by 1
                            continue;
                        }
                        else
                        {
                            lett.Valid = false;
                            return aName;
                        }
                    }
                }
            }
            else
            {
                
                if (entry.IsHorizontal == true)
                {
                    for (int x = 0; x < lett.IterationsRightOrBelow + 1; x++) //add an extra move to the left for white space around the letter
                    {
                        if (Grid[c.BelowLetter, lett.ExistingLetterColumn] == '\0' && Grid[c.BelowLetter, c.LeftOfLetter] == '\0' && Grid[c.BelowLetter, c.RightOfLetter] == '\0')
                        {
                            
                            
                        }
                        else
                        {
                            lett.Valid = false;
                            return aName;
                        }
                    }
                }
            }

            return aName;
        }

        //public Word GetCoordinatesOfLetter()
        //{

        //}

        public Word CreateWord(Word name)
        {
            
            //create a char array of the name
            char[] wordCharArray = name.Name.ToCharArray();

            //add to word
            name.LettersInWord = wordCharArray;

            name.ListOfIntersectingWordsPerLetter = WordList.Table[name.Name];

            return name;
        }

        public char[,] AddRootWord()
        {
            
            var dictList = WordList.Table;
            Dictionary<string, int> bestWordDict = new Dictionary<string, int>();
            Dictionary<char, int> points = new Dictionary<char, int>();
            //int scoreWords = 0;
            CordsLetterTable cl = new CordsLetterTable();
            char keyindic = 'A';
            int x = 0;
            int y = 0;
            int startX = 0;
            int startY = 0;
            string highestWord = "";
            char[] wordCharArray = new char[0];

            //put the points table into a dictionary
            foreach (var val in Config.IntersectingPointsPerLetter)
            {
                points.Add(keyindic, val);
                keyindic++;
            }

            //get random name from wordlist
            foreach (var n in dictList)
            {
                
                NamesList.Add(n.Key);
            }

            bool OK = false;

            while(OK == false)
            {
                int r = rnd.Next(NamesList.Count);
                highestWord = NamesList[r];
                int lengthOfWord = highestWord.Length;

                //set the cords of the grid so the word is placed in the centre
                
                int row = Convert.ToInt16(RowsAndColumns.Rows);
                int col = Convert.ToInt16(RowsAndColumns.Columns);
                x = rnd.Next(2, row);
                y = rnd.Next(2, col);
                startX = x;
                startY = y;

                //create a char arrayu of the name
                wordCharArray = highestWord.ToCharArray();

                for(int a = 0; a < wordCharArray.Length; a++)
                //foreach(var l in wordCharArray)
                {
                    if (Grid[x, y] == '*')
                    {
                        OK = false;
                        break;
                        
                    }                    
                    else
                    {
                        OK = true;
                        y = y + 1;
                        continue;
                        
                    }
                }
            }
            
            //add the word to the grid
            foreach (var letter in wordCharArray)
            {
                Grid[startX, startY] = letter;
                Coordinate c = new Coordinate(startX, startY);
                cl.Add(c, letter);
                startY = startY + 1;
            }

            //add the name, letters and coords of each letter to the table
            //add properties to the object
            LetterCordsForWordsInGrid.Add(highestWord, cl);

            Score = LetterCordsForWordsInGrid.Keys.Count * Config.PointsPerWord;

            var objKey = WordList.Table[highestWord];
            Dictionary<char, List<string>> objDict = new Dictionary<char, List<string>>();

            foreach (var key in objKey)
            {
                objDict.Add(key.Key, key.Value);
            }

            var cor = LetterCordsForWordsInGrid[highestWord];
            Dictionary<Coordinate, char> objCor = new Dictionary<Coordinate, char>();
            foreach (var res in cor)
            {
                objCor.Add(res.Key, res.Value);
            }

            LastWordEntered wordObj = new LastWordEntered(highestWord, objDict, objCor);

            WordsInserted.Add(wordObj);

            WordList.Table.Remove(highestWord);

            RootWord = highestWord;

            WordsInGrid.Add(highestWord);

            Counter++;


            return Grid;

        }

        public string DisplayGrid(CrozzleGrid game)
        {
            char[,] grid = game.Grid;
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

            crozzleHTML += @"<p>Score = " + game.Score + @"</p>";

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
