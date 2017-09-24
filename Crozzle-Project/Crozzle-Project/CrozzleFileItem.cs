using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Crozzle_Project
{
    class CrozzleFileItem
    {
        //constants in configuration file
        const string NoCrozzleItem = "NO_CROZZLE_ITEM";
        const string ConfigurationFileSymbol = "CONFIGURATION_FILE";
        const string WordListFileSymbol = "WORDLIST_FILE";
        const string RowsSymbol = "ROWS";
        const string ColumnsSymbol = "COLUMNS";
        const string RowSymbol = "ROW";
        const string ColumnSymbol = "COLUMN";
        const string ColonSymbol = ":";
        const string AtoZ = @"^[A-Z]$";

        //properties
        public static List<string> Errors { get; set; }

        private string OriginalItem { get; set; }
        public bool Valid { get; set; } = false;
        public string Name { get; set; }
        public KeyValue KeyValue { get; set; }

        public bool IsConfigurationFile
        {
            get { return (Regex.IsMatch(Name, @"^" + ConfigurationFileSymbol + @"$")); }
        }

        public bool IsWordListFile
        {
            get { return (Regex.IsMatch(Name, @"^" + WordListFileSymbol + @"$")); }
        }

        public bool IsRows
        {
            get { return (Regex.IsMatch(Name, @"^" + RowsSymbol + @"$")); }
        }

        public bool IsColumns
        {
            get { return (Regex.IsMatch(Name, @"^" + ColumnsSymbol + @"$")); }
        }

        public bool IsRow
        {
            get { return (Regex.IsMatch(Name, @"^" + RowSymbol + @"$")); }
        }

        public bool IsColumn
        {
            get { return (Regex.IsMatch(Name, @"^" + ColumnSymbol + @"$")); }
        }

        //constructor
        public CrozzleFileItem(string originalItemData)
        {
            OriginalItem = originalItemData;
        }

        //get the key value pairs for the constants
        //crozzlefileitem is the line read from the crozzle.txt file
        public static Boolean TryParse(String crozzleFileItem, out CrozzleFileItem aCrozzleFileItem)
        {
            Errors = new List<String>();

            //makes a new crozzlefileitem and sets teh originalitem property to the line from teh file
            aCrozzleFileItem = new CrozzleFileItem(crozzleFileItem);

            // Discard comment.
            if (crozzleFileItem.Contains("//"))
            {
                int index = crozzleFileItem.IndexOf("//");
                crozzleFileItem = crozzleFileItem.Remove(index);
                crozzleFileItem = crozzleFileItem.Trim();
            }

            //if line is empty adds the constant NO CROZZLE ITEM
            if (Regex.IsMatch(crozzleFileItem, @"^\s*$"))
            {
                // Check for only 0 or more white spaces.
                aCrozzleFileItem.Name = NoCrozzleItem;
            }
            else if (Regex.IsMatch(crozzleFileItem, @"^" + ConfigurationFileSymbol + @".*"))
            {
                // Get the CONFIGURATION_FILE key-value pair.
                KeyValue aKeyValue;
                if (!KeyValue.TryParse(crozzleFileItem, ConfigurationFileSymbol, out aKeyValue))
                    Errors.AddRange(KeyValue.Errors);
                aCrozzleFileItem.Name = ConfigurationFileSymbol;
                aCrozzleFileItem.KeyValue = aKeyValue;
            }
            else if (Regex.IsMatch(crozzleFileItem, @"^" + WordListFileSymbol + @".*"))
            {
                // Get the WORDLIST_FILE key-value pair.
                KeyValue aKeyValue;
                if (!KeyValue.TryParse(crozzleFileItem, WordListFileSymbol, out aKeyValue))
                    Errors.AddRange(KeyValue.Errors);
                aCrozzleFileItem.Name = WordListFileSymbol;
                aCrozzleFileItem.KeyValue = aKeyValue;
            }
            else if (Regex.IsMatch(crozzleFileItem, @"^" + RowsSymbol + @".*"))
            {
                // Get the number of rows for the crozzle.
                KeyValue aKeyValue;
                if (!KeyValue.TryParse(crozzleFileItem, RowsSymbol, out aKeyValue))
                    Errors.AddRange(KeyValue.Errors);
                aCrozzleFileItem.Name = RowsSymbol;
                aCrozzleFileItem.KeyValue = aKeyValue;
            }
            else if (Regex.IsMatch(crozzleFileItem, @"^" + ColumnsSymbol + @".*"))
            {
                // Get the number of columns for the crozzle.
                KeyValue aKeyValue;
                if (!KeyValue.TryParse(crozzleFileItem, ColumnsSymbol, out aKeyValue))
                    Errors.AddRange(KeyValue.Errors);
                aCrozzleFileItem.Name = ColumnsSymbol;
                aCrozzleFileItem.KeyValue = aKeyValue;
            }
            else if (Regex.IsMatch(crozzleFileItem, @"^" + RowSymbol + @".*"))
            {
                // Get data for a horizontal word.
                KeyValue aKeyValue;
                if (!KeyValue.TryParse(crozzleFileItem, RowSymbol, out aKeyValue))
                    Errors.AddRange(KeyValue.Errors);
                aCrozzleFileItem.Name = RowSymbol;
                aCrozzleFileItem.KeyValue = aKeyValue;
            }
            else if (Regex.IsMatch(crozzleFileItem, @"^" + ColumnSymbol + @".*"))
            {
                // Get data for a vertical word.
                KeyValue aKeyValue;
                if (!KeyValue.TryParse(crozzleFileItem, ColumnSymbol, out aKeyValue))
                    Errors.AddRange(KeyValue.Errors);
                aCrozzleFileItem.Name = ColumnSymbol;
                aCrozzleFileItem.KeyValue = aKeyValue;
            }
            else
                Errors.Add(String.Format(CrozzleFileItemErrors.SymbolError, crozzleFileItem));

            //aCrozzleFileItem.Valid property is set to true if the errors count is 0
            aCrozzleFileItem.Valid = Errors.Count == 0;
            return (aCrozzleFileItem.Valid);
        }

    }
}
