using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;
using System.Collections;
using System.Web.UI.HtmlControls;
using System.Web.UI;

namespace Crozzle_Project
{
    public partial class FrmMenu : Form
    {
        

        public FrmMenu()
        {
            InitializeComponent();

            
        }

        

       

        private Crozzle SIT323Crozzle { get; set; }

        private void fileToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void validateToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void openCrozzleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenCrozzleFile();
        }

        private void btnOpenTask2Crozzle_Click(object sender, EventArgs e)
        {
            OpenCrozzleAss2();
        }
        

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void OpenCrozzleAss2()
        {
            

            //crozzleWebBrowser.DocumentText = "";            //area where the crozzle will be displayed
            //errorWebBrowser.DocumentText = "";              // area where the errors will be displayed

            //process crozzle file
            // Get configuration filename.

            CrozzleTaskTwo config = new CrozzleTaskTwo(URLs.Task2Crozzle);
            // string configurationFileName = GetConfigurationFileName(URLs.Task2Crozzle);

            //validate configuration file.
            Configuration aConfiguration = null;
            Configuration.TryParseTaskTwo(config.ConfigurationURL, out aConfiguration);

            // Parse wordlist file.
            WordListTaskTwo wordList = new WordListTaskTwo(config.WordlistURL);
            //WordList.TryParseTaskTwo(config.WordlistURL, aConfiguration, out wordList);

            //char[,] grid = new char[Convert.ToInt16(config.Rows), Convert.ToInt16(config.Columns)];

            CrozzleGrid aGrid = new CrozzleGrid(aConfiguration, wordList, config);

            aGrid.AddRootWord();

            aGrid.AddWordToGrid();

            string res = aGrid.DisplayGrid();
            //string res = aGrid.CreateGrid();

            crozzleWebBrowser.DocumentText = res;
                      


        }

        private void OpenCrozzleFile()
        {
            DialogResult result;

            // indicate crozzle file, and crozzle are not valid, and clear GUI.
            crozzleToolStripMenuItem.Enabled = false;     //menu across the top
            crozzleWebBrowser.DocumentText = "";            //area where the crozzle will be displayed
            errorWebBrowser.DocumentText = "";              // area where the errors will be displayed

            //process crozzle file
            result = openFileDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                // Get configuration filename.
                string configurationFileName = GetConfigurationFileName(openFileDialog1.FileName);
                if (configurationFileName == null)
                {
                    MessageBox.Show("Configuration filename is missing from the crozzle file", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                    return;
                }
                else
                {
                    string filename = configurationFileName.Trim();

                    //sends to the class validator to find out if there are deliminators
                    //if comes back true
                    //remove the deliminators double quotes
                    if (Validator.IsDelimited(filename, Crozzle.StringDelimiters))
                        filename = filename.Trim(Crozzle.StringDelimiters);
                    configurationFileName = filename;

                    //asks whether the path contains a root
                    //if not it will get the directory name
                    if (!Path.IsPathRooted(configurationFileName))
                        configurationFileName = Path.GetDirectoryName(openFileDialog1.FileName) + @"\" + configurationFileName;
                }

                //validate configuration file.
                Configuration aConfiguration = null;
                Configuration.TryParse(configurationFileName, out aConfiguration);

                // Get wordlist filename.
                String wordListFileName = GetWordlistFileName(openFileDialog1.FileName);
                if (wordListFileName == null)
                {
                    MessageBox.Show("Wordlist filename is missing from the crozzle file", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                    return;
                }
                else
                {
                    String filename = wordListFileName.Trim();
                    if (Validator.IsDelimited(filename, Crozzle.StringDelimiters))
                        filename = filename.Trim(Crozzle.StringDelimiters);
                    wordListFileName = filename;

                    if (!Path.IsPathRooted(wordListFileName))
                        wordListFileName = Path.GetDirectoryName(openFileDialog1.FileName) + @"\" + wordListFileName;
                }

                // Parse wordlist file.
                WordList wordList = null;
                WordList.TryParse(wordListFileName, aConfiguration, out wordList);

                // Parse crozzle file.
                Crozzle aCrozzle;
                Crozzle.TryParse(openFileDialog1.FileName, aConfiguration, wordList, out aCrozzle);
                SIT323Crozzle = aCrozzle;

                // Update GUI - menu enabled, display crozzle data (whether valid or invalid), and crozzle file errors.
                if (SIT323Crozzle.FileValid && SIT323Crozzle.Configuration.Valid && SIT323Crozzle.WordList.Valid)
                    crozzleToolStripMenuItem.Enabled = true;

                crozzleWebBrowser.DocumentText = SIT323Crozzle.ToStringHTML();
                errorWebBrowser.DocumentText = 
                    SIT323Crozzle.FileErrorsHTML +
                    SIT323Crozzle.Configuration.FileErrorsHTML +
                    SIT323Crozzle.WordList.FileErrorsHTML;

                // Log errors.
                SIT323Crozzle.LogFileErrors(SIT323Crozzle.FileErrorsTXT);
                SIT323Crozzle.LogFileErrors(SIT323Crozzle.Configuration.FileErrorsTXT);
                SIT323Crozzle.LogFileErrors(SIT323Crozzle.WordList.FileErrors);
            }
        }

        private string GetConfigurationFileName(string path)
        {
            CrozzleFileItem aCrozzleFileItem = null;  //this will be the line in the text file stream is reading        

            StreamReader fileIn = new StreamReader(path);

            // REads the crozzle.txt file and Search for line in the txt file CONFIGERATION_FILE
            while (!fileIn.EndOfStream)
            {
                //Try Parse is a function that has been overidden, it is located in CrozzleFileItem
                if (CrozzleFileItem.TryParse(fileIn.ReadLine(), out aCrozzleFileItem))
                    if (aCrozzleFileItem.IsConfigurationFile)
                        break;
            }

            // Close files.
            fileIn.Close();

            // Return file name.
            if (aCrozzleFileItem == null)
                return (null);
            else
                return (aCrozzleFileItem.KeyValue.Value);
        }

        private String GetWordlistFileName(String path)
        {
            CrozzleFileItem aCrozzleFileItem = null;
            StreamReader fileIn = new StreamReader(path);

            // Search for file name.
            while (!fileIn.EndOfStream)
            {
                if (CrozzleFileItem.TryParse(fileIn.ReadLine(), out aCrozzleFileItem))
                    if (aCrozzleFileItem.IsWordListFile)
                        break;
            }

            // Close files.
            fileIn.Close();

            // Return file name.
            if (aCrozzleFileItem == null)
                return (null);
            else
                return (aCrozzleFileItem.KeyValue.Value);
        }
       

        private void crozzleToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            // Check if the crozzle is valid.
            SIT323Crozzle.validate();

            // Update GUI - display crozzle data (whether valid or invalid), 
            // crozle file errors, config file errors, word list file errors and crozzle errors.
            crozzleWebBrowser.DocumentText = SIT323Crozzle.ToStringHTML();
            errorWebBrowser.DocumentText =
                SIT323Crozzle.FileErrorsHTML +
                SIT323Crozzle.Configuration.FileErrorsHTML +
                SIT323Crozzle.WordList.FileErrorsHTML +
                SIT323Crozzle.ErrorsHTML;

            // Log crozzle errors.
            SIT323Crozzle.LogFileErrors(SIT323Crozzle.ErrorsTXT);
        }

        private void crozzleWebBrowser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {

        }
    }
}
