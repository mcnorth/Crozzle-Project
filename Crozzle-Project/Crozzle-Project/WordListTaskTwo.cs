using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;

namespace Crozzle_Project
{
    public class WordListTaskTwo
    {
        internal WordTable Table { get; set; }

        public string URL { get; set; }

        public WordListTaskTwo(string url)
        {
            URL = url;
            PopulateTable();
        }

        public WordListTaskTwo(WordListTaskTwo copy)
        {
            this.URL = copy.URL;
            PopulateTable();
        }

        private void PopulateTable()
        {
            //create dict
            Table = new WordTable();

            List<string> words = GetWords();

            //List<string> words = new List<string>();
            //words.Add("ANGELA");
            //words.Add("JIMMY");
            //words.Add("LARRY");
            //words.Add("JACK");
            //words.Add("JILL");
            //words.Add("MARK");
            //words.Add("AMY");


            foreach (string word in words)
            {
                LetterTable letterTable = new LetterTable();

                foreach(char letter in word)
                {
                    if (!letterTable.ContainsKey(letter))
                    {
                        List<string> intersectingWords = new List<string>();

                        foreach (string w in words)
                            if (w.IndexOf(letter) > -1)
                                if (!intersectingWords.Contains(w))
                                    intersectingWords.Add(w);
                        letterTable.Add(letter, intersectingWords);
                    }
                }

                Table.Add(word, letterTable);
            }
        }

        //get teh word list from teh server
        public List<string> GetWords()
        {
            List<string> words = new List<string>();
            WebClient webClient = new WebClient();

            try
            {
                Stream aStream = webClient.OpenRead(URL);
                StreamReader aStreamReader = new StreamReader(aStream);

                // Process each line of the file.
                while (!aStreamReader.EndOfStream)
                {
                    string line = aStreamReader.ReadLine();
                    string[] word = line.Split(new char[] { ',' });
                    foreach (string w in word)
                        if (!words.Contains(w))
                            words.Add(w);
                }

                // Close streams.
                aStreamReader.Close();
            }
            catch (WebException webEx)
            {
                Console.WriteLine(webEx.Message);
            }
            catch (ArgumentNullException argNullEx)
            {
                Console.WriteLine(argNullEx.Message);
            }
            catch (ArgumentException argEx)
            {
                Console.WriteLine(argEx.Message);
            }

            return (words);
        }

        public List<string> GetIntersections(string word, char letter)
        {
            List<string> words = new List<string>();

            if (Table.ContainsKey(word))
            {
                LetterTable letterTable = Table[word];
                if (letterTable.ContainsKey(letter))
                    words = letterTable[letter];
            }
            return (words);
        }
    }
}
