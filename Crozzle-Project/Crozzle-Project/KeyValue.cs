using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Crozzle_Project
{
    class KeyValue
    {
        //length when  the string array is split so it has 2 parts key and value
        const int KeyValueLength = 2;

        public static List<String> Errors { get; set; }

        //properties
        public string OriginalKeyValue { get; set; }
        public bool Valid { get; set; } = false;
        public string Key { get; set; } = null;
        public string Value { get; set; } = null;

        //constructor
        public KeyValue(string originalKeyValueData)
        {
            OriginalKeyValue = originalKeyValueData;
        }



        public static Boolean TryParse(String originalKeyValueData, String keyPattern, out KeyValue aKeyValue)
        {
            const String EqualsSymbol = "=";

            Errors = new List<String>();
            aKeyValue = new KeyValue(originalKeyValueData);

            if (originalKeyValueData.Contains(EqualsSymbol))
            {
                //string array                line in file          split the line   at the =          into this length which is a constant var in KeyValue class (2)
                String[] originalKeyValue = originalKeyValueData.Split(new String[] { EqualsSymbol }, KeyValueLength, StringSplitOptions.None);

                // Check that the original key-value pair data has correct length.
                if (originalKeyValue.Length != KeyValueLength)
                    Errors.Add(String.Format(KeyValueErrors.FieldCountError, originalKeyValue.Length, originalKeyValueData, KeyValueLength));

                if (originalKeyValue.Length > 0)
                {
                    // Check the key field.
                    if (Regex.IsMatch(originalKeyValue[0], keyPattern))
                        aKeyValue.Key = originalKeyValue[0];
                    else
                        Errors.Add(String.Format(KeyValueErrors.InvalidKeyError, originalKeyValueData));

                    // Check the value field.
                    if (originalKeyValue[1] == null)
                        Errors.Add(String.Format(KeyValueErrors.NullValueError, originalKeyValueData));
                    else if (originalKeyValue[1] == "")
                        Errors.Add(String.Format(KeyValueErrors.MissingValueError, originalKeyValueData));
                    else
                        aKeyValue.Value = originalKeyValue[1];
                }
            }
            else
                Errors.Add(String.Format(KeyValueErrors.MissingEqualsError, originalKeyValueData));

            aKeyValue.Valid = Errors.Count == 0;
            return (aKeyValue.Valid);
        }

    }
}
