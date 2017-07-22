﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;
using System.Collections;

namespace Crozzle_Project
{
    class Configuration
    {
        private string LOGFILE_NAME;
        private int MINIMUM_NUMBER_OF_UNIQUE_WORDS;
        private int MAXIMUM_NUMBER_OF_UNIQUE_WORDS;
        private string INVALID_CROZZLE_SCORE;
        private bool UPPERCASE;
        private string STYLE;
        private string BGCOLOUR_EMPTY_TD;
        private string BGCOLOUR_NON_EMPTY_TD;
        private int MINIMUM_NUMBER_OF_ROWS;
        private int MAXIMUM_NUMBER_OF_ROWS;
        private int MINIMUM_NUMBER_OF_COLUMNS;
        private int MAXIMUM_NUMBER_OF_COLUMNS;
        private int MINIMUM_HORIZONTAL_WORDS;
        private int MAXIMUM_HORIZONTAL_WORDS;
        private int MINIMUM_VERTICAL_WORDS;
        private int MAXIMUM_VERTICAL_WORDS;
        private int MINIMUM_INTERSECTIONS_IN_HORIZONTAL_WORDS;
        private int MAXIMUM_INTERSECTIONS_IN_HORIZONTAL_WORDS;
        private int MINIMUM_INTERSECTIONS_IN_VERTICAL_WORDS;
        private int MAXIMUM_INTERSECTIONS_IN_VERTICAL_WORDS;
        private int MINIMUM_NUMBER_OF_THE_SAME_WORD;
        private int MAXIMUM_NUMBER_OF_THE_SAME_WORD;
        private int MINIMUM_NUMBER_OF_GROUPS;
        private int MAXIMUM_NUMBER_OF_GROUPS;

        public string LogfileName
        {
            get { return LOGFILE_NAME; }
            set { LOGFILE_NAME = value; }
        }

        public int MinimumNumberOfUniqueWords
        {
            get { return MINIMUM_NUMBER_OF_UNIQUE_WORDS; }
            set { MINIMUM_NUMBER_OF_UNIQUE_WORDS = value; }
        }

        public int MaximumNumberOfUniqueWords
        {
            get { return MAXIMUM_NUMBER_OF_UNIQUE_WORDS; }
            set { MAXIMUM_NUMBER_OF_UNIQUE_WORDS = value; }
        }

        public string InvalidCrozzleScore
        {
            get { return INVALID_CROZZLE_SCORE; }
            set { INVALID_CROZZLE_SCORE = value; }
        }

        public bool UpperCase
        {
            get { return UPPERCASE; }
            set { UPPERCASE = value; }
        }

        public string Style
        {
            get { return STYLE; }
            set { STYLE = value; }
        }

        public string BgColourEmptyTd
        {
            get { return BGCOLOUR_EMPTY_TD; }
            set { BGCOLOUR_EMPTY_TD = value; }
        }

        public string BgColourNonEmptyTd
        {
            get { return BGCOLOUR_NON_EMPTY_TD; }
            set { BGCOLOUR_NON_EMPTY_TD = value; }
        }

        public int MinimumNumberOfRows
        {
            get { return MINIMUM_NUMBER_OF_ROWS; }
            set { MINIMUM_NUMBER_OF_ROWS = value; }
        }

        public int MaximumNumberOfRows
        {
            get { return MAXIMUM_NUMBER_OF_ROWS; }
            set { MAXIMUM_NUMBER_OF_ROWS = value; }
        }

        public int MinimumNumberOfColumns
        {
            get { return MINIMUM_NUMBER_OF_COLUMNS; }
            set { MINIMUM_NUMBER_OF_COLUMNS = value; }
        }

        public int MaximumNumberOfColumns
        {
            get { return MAXIMUM_NUMBER_OF_COLUMNS; }
            set { MAXIMUM_NUMBER_OF_COLUMNS = value; }
        }

        public int MinimumHorizontalWords
        {
            get { return MINIMUM_HORIZONTAL_WORDS; }
            set { MINIMUM_HORIZONTAL_WORDS = value; }
        }

        public int MaximumHorizontalWords
        {
            get { return MAXIMUM_HORIZONTAL_WORDS; }
            set { MAXIMUM_HORIZONTAL_WORDS = value; }
        }

        public int MinimumVerticalWords
        {
            get { return MINIMUM_VERTICAL_WORDS; }
            set { MINIMUM_VERTICAL_WORDS = value; }
        }

        public int MaximumVerticalWords
        {
            get { return MAXIMUM_VERTICAL_WORDS; }
            set { MAXIMUM_VERTICAL_WORDS = value; }
        }

        public int MinimumIntersectionsInHorizontalWords
        {
            get { return MINIMUM_INTERSECTIONS_IN_HORIZONTAL_WORDS; }
            set { MINIMUM_INTERSECTIONS_IN_HORIZONTAL_WORDS = value; }
        }

        public int MaximumIntersectionsInHorizontalWords
        {
            get { return MAXIMUM_INTERSECTIONS_IN_HORIZONTAL_WORDS; }
            set { MAXIMUM_INTERSECTIONS_IN_HORIZONTAL_WORDS = value; }
        }

        public int MinimumIntersectionsInVerticalWords
        {
            get { return MINIMUM_INTERSECTIONS_IN_VERTICAL_WORDS; }
            set { MINIMUM_INTERSECTIONS_IN_VERTICAL_WORDS = value; }
        }

        public int MaximumIntersectionsInVerticalWords
        {
            get { return MAXIMUM_INTERSECTIONS_IN_VERTICAL_WORDS; }
            set { MAXIMUM_INTERSECTIONS_IN_VERTICAL_WORDS = value; }
        }

        public int MinimumNumberOfTheSameWord
        {
            get { return MINIMUM_NUMBER_OF_THE_SAME_WORD; }
            set { MINIMUM_NUMBER_OF_THE_SAME_WORD = value; }
        }

        public int MaximumNumberOfTheSameWord
        {
            get { return MAXIMUM_NUMBER_OF_THE_SAME_WORD; }
            set { MAXIMUM_NUMBER_OF_THE_SAME_WORD = value; }
        }

        public int MinimumNumberOfGroups
        {
            get { return MINIMUM_NUMBER_OF_GROUPS; }
            set { MINIMUM_NUMBER_OF_GROUPS = value; }
        }

        public int MaximumNumberOfGroups
        {
            get { return MAXIMUM_NUMBER_OF_GROUPS; }
            set { MAXIMUM_NUMBER_OF_GROUPS = value; }
        }

        public Configuration CreateConfig(Configuration obj, Hashtable HtObj)
        {
            obj.LogfileName = Convert.ToString(HtObj["LOGFILE_NAME"]);
            obj.MinimumNumberOfUniqueWords = Convert.ToInt32(HtObj["MINIMUM_NUMBER_OF_UNIQUE_WORDS"]);
            obj.MaximumNumberOfUniqueWords = Convert.ToInt32(HtObj["MAXIMUM_NUMBER_OF_UNIQUE_WORDS"]);
            obj.InvalidCrozzleScore = Convert.ToString(HtObj["INVALID_CROZZLE_SCORE"]);
            obj.UpperCase = Convert.ToBoolean(HtObj["UPPERCASE"]);
            obj.Style = Convert.ToString(HtObj["STYLE"]);
            obj.BgColourEmptyTd = Convert.ToString(HtObj["BGCOLOUR_EMPTY_TD"]);
            obj.BgColourNonEmptyTd = Convert.ToString(HtObj["BGCOLOUR_NON_EMPTY_TD"]);
            obj.MinimumNumberOfRows = Convert.ToInt32(HtObj["MINIMUM_NUMBER_OF_ROWS"]);
            obj.MaximumNumberOfRows = Convert.ToInt32(HtObj["MAXIMUM_NUMBER_OF_ROWS"]);
            obj.MinimumNumberOfColumns = Convert.ToInt32(HtObj["MINIMUM_NUMBER_OF_COLUMNS"]);
            obj.MaximumNumberOfColumns = Convert.ToInt32(HtObj["MAXIMUM_NUMBER_OF_COLUMNS"]);
            obj.MinimumHorizontalWords = Convert.ToInt32(HtObj["MINIMUM_HORIZONTAL_WORDS"]);
            obj.MaximumHorizontalWords = Convert.ToInt32(HtObj["MAXIMUM_HORIZONTAL_WORDS"]);
            obj.MinimumVerticalWords = Convert.ToInt32(HtObj["MINIMUM_VERTICAL_WORDS"]);
            obj.MaximumVerticalWords = Convert.ToInt32(HtObj["MAXIMUM_VERTICAL_WORDS"]);
            obj.MinimumIntersectionsInHorizontalWords = Convert.ToInt32(HtObj["MINIMUM_INTERSECTIONS_IN_HORIZONTAL_WORDS"]);
            obj.MaximumIntersectionsInHorizontalWords = Convert.ToInt32(HtObj["MAXIMUM_INTERSECTIONS_IN_HORIZONTAL_WORDS"]);
            obj.MinimumIntersectionsInVerticalWords = Convert.ToInt32(HtObj["MINIMUM_INTERSECTIONS_IN_VERTICAL_WORDS"]);
            obj.MaximumIntersectionsInVerticalWords = Convert.ToInt32(HtObj["MAXIMUM_INTERSECTIONS_IN_VERTICAL_WORDS"]);
            obj.MinimumNumberOfTheSameWord = Convert.ToInt32(HtObj["MINIMUM_NUMBER_OF_THE_SAME_WORD"]);
            obj.MaximumNumberOfTheSameWord = Convert.ToInt32(HtObj["MAXIMUM_NUMBER_OF_THE_SAME_WORD"]);
            obj.MinimumNumberOfGroups = Convert.ToInt32(HtObj["MINIMUM_NUMBER_OF_GROUPS"]);
            obj.MaximumNumberOfGroups = Convert.ToInt32(HtObj["MAXIMUM_NUMBER_OF_GROUPS"]);


            return obj;
        }

    }
}