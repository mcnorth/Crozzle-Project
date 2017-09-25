using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;

namespace Crozzle_Project
{
    class CrozzleTaskTwo
    {
        public string URL { get; set; }

        //public CrozzleTaskTwo(string url)
        //{
        //    URL = url;
        //}

        public string ConfigurationURL { get; set; }
        public string WordlistURL { get; set; }
        public string Rows { get; set; }
        public string Columns { get; set; }

        public CrozzleTaskTwo(string url)
        {
            URL = url;
            ConfigurationURL = GetURL("CONFIGURATION_FILE");
            WordlistURL = GetURL("WORDLIST_FILE");
            Rows = GetURL("ROWS");
            Columns = GetURL("COLUMNS");

        }

        private string GetURL(string keyword)
        {
            string filename = null;
            WebClient webClient = new WebClient();

            // Open streams on this file.
            Stream aStream = webClient.OpenRead(URL);
            StreamReader aStreamReader = new StreamReader(aStream);

            // Process each line of the file.
            while (!aStreamReader.EndOfStream && filename == null)
            {
                string line = aStreamReader.ReadLine().Trim();
                if (line.StartsWith(keyword))
                {
                    string[] keyValue = line.Split(new Char[] { '=' });
                    filename = keyValue[1].Trim(new Char[] { '"' });
                    //if (!Path.IsPathRooted(filename))
                    //    filename = GetFolder(URL) + '/' + filename;
                }
            }

            // Close streams on this file.
            aStreamReader.Close();

            return (filename);
        }

        protected string GetFolder(string path)
        {
            int index = path.LastIndexOf('/');
            string folder = path.Remove(index);
            return (folder);
        }

    }
}
