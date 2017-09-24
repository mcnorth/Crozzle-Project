using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crozzle_Project
{
    class WordDataList
    {
        //properties
        public static List<String> Errors { get; set; }

        public List<String> OriginalWordDataList { get; set; }
        public Boolean Valid { get; set; } = false;
        public List<WordData> AllWordData { get; set; }
        public List<WordData> HorizontalWordData { get; set; }
        public List<WordData> VerticalWordData { get; set; }

        //counting
        public int Count
        {
            get
            {
                return (AllWordData.Count);
            }
        }

        public int HorizontalCount
        {
            get
            {
                return (HorizontalWordData.Count);
            }
        }

        public int VerticalCount
        {
            get
            {
                return (VerticalWordData.Count);
            }
        }

        //constructors
        public WordDataList(List<string> originalList)
        {
            OriginalWordDataList = originalList;
            AllWordData = new List<WordData>();
            HorizontalWordData = new List<WordData>();
            VerticalWordData = new List<WordData>();
        }

        public static Boolean TryParse(List<String> originalWordDataList, Crozzle aCrozzle, out WordDataList aWordDataList)
        {
            List<WordData> aList = new List<WordData>();

            Errors = new List<String>();
            aWordDataList = new WordDataList(originalWordDataList);

            foreach (String line in originalWordDataList)
            {
                WordData aWordData;
                if (WordData.TryParse(line, aCrozzle, out aWordData))
                    aWordDataList.Add(aWordData);
                else
                    Errors.AddRange(WordData.Errors);
            }

            aWordDataList.Valid = Errors.Count == 0;
            return (aWordDataList.Valid);
        }

        private void Add(WordData wordData)
        {
            AllWordData.Add(wordData);
            if (wordData.IsHorizontal)
                HorizontalWordData.Add(wordData);
            else
                VerticalWordData.Add(wordData);
        }
    }
}
