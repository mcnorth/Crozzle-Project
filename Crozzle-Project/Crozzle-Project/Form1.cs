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
using System.Timers;

namespace Crozzle_Project
{
    public partial class FrmMenu : Form
    {
        private System.Timers.Timer aTimer;
        //private System.Windows.Forms.Timer aTimer;
        private List<CrozzleGrid> ListOfGrids = new List<CrozzleGrid>();

        public FrmMenu()
        {
            InitializeComponent();
            //aTimer = new System.Timers.Timer(300000);
            aTimer = new System.Timers.Timer(300000);
            //aTimer = new Timer();
            //aTimer.Interval = 2000;
            //aTimer.Tick += ATimer_Tick;
            aTimer.Elapsed += new ElapsedEventHandler(ATimer_Elapsed);
            
        }

        private void ATimer_Tick(object sender, EventArgs e)
        {
            aTimer.Stop();
            aTimer.Enabled = false;
        }

        private void ATimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {            
            aTimer.Enabled = false;
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
            aTimer.Enabled = true;

            

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
            //aGrid.AddFirstWord();

            //Tree aTree = new Tree(aGrid);
            //aGrid.AddNameToGrid();

            //while (aTimer.Enabled = true || aGrid.GetWordlistCount() > 0)
            //{

            //}

           Recursive(aGrid, wordList);

            
            //aGrid.AddWordToGrid();
            //aTree.Add(aGrid);
            //aGrid.AddWordToGrid();
            //aGrid.AddWordToGrid();
            //aGrid.AddWordToGrid();
            //aGrid.AddWordToGrid();

            

            //ListOfGrids.Sort((x, y) => x.Score.CompareTo(y.OrderDate));

            //ListOfGrids.Sort()

            
            var maxObject = ListOfGrids.OrderByDescending(item => item.Score).First();

            string res = aGrid.DisplayGrid(maxObject);
            //string res = aGrid.CreateGrid();

            crozzleWebBrowser.DocumentText = res;

            //string res = aGrid.DisplayGrid(aGrid);
            ////string res = aGrid.CreateGrid();

            //crozzleWebBrowser.DocumentText = res;


        }

        private CrozzleGrid Recursive(CrozzleGrid aGrid, WordListTaskTwo wordList)
        {
            while (aTimer.Enabled == true)
            {
                if (aGrid.GetWordlistCount() == 0)
                {
                    return aGrid;
                }
                if (aGrid.GetRootWord() == "")
                {
                    aGrid.AddRootWord();
                    aGrid.AddWordToGrid();
                }
                else
                {
                    aGrid.AddWordToGrid();
                }
                if (aGrid.GetCounter() == 0)
                {

                    ListOfGrids.Add(aGrid); //not copying the obj, everytime it adds a new grid it overides the reference in the list
                    WordListTaskTwo freshWordlist = new WordListTaskTwo(wordList);
                    CrozzleGrid bGrid = new CrozzleGrid(aGrid, freshWordlist);

                    return Recursive(bGrid, freshWordlist);
                }

                return Recursive(aGrid, wordList);
            }

            return aGrid;
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
