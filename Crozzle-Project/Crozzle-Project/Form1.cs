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

namespace Crozzle_Project
{
    public partial class FrmMenu : Form
    {
        

        public FrmMenu()
        {
            InitializeComponent();
            
        }

        OpenFileDialog ofd = new OpenFileDialog();

        private void btnGetFile_Click(object sender, EventArgs e)
        {
            DialogResult result = ofd.ShowDialog();

            if(result == DialogResult.OK)
            {
                txtFile.Text = ofd.FileName;
            }
            else
            {
                txtFile.Text = "Try again";
            }
        }

        private void btnLoadFile_Click(object sender, EventArgs e)
        {
            string cPath = txtFile.Text;
            
            Hashtable ht = GetFile(cPath);
            Hashtable IPscoreHt = GetIPScoreFile(cPath);
            Hashtable NIPscoreHt = GetNIPScoreFile(cPath);
            Configuration config = new Configuration();
            config.CreateConfig(config, ht, IPscoreHt, NIPscoreHt);

            //string wPath = 
            //CreateDataGridView = new DataGridView();
            //CreateDataGridView.Size = new Size(300, 542);
            //CreateDataGridView.Location = new Point(10, 12);
            //this.Controls.Add(CreateDataGridView);
        }

        public static Hashtable GetNIPScoreFile(string path)
        {
            var pathToFile = path;
            var file = File.ReadAllLines(pathToFile);
            List<string> configFile1 = new List<string>();
            List<string> configFile2 = new List<string>();
            List<string> configFile3 = new List<string>();
            List<string> ScoreConfigFile = new List<string>();
            string[] IPScoreArray = new string[0];

            string result;

            Hashtable NonIntersectingPoints = new Hashtable();

            foreach (var line in file)
            {
                if (line.Contains(" "))
                {
                    var newLine = line.Replace(" ", "");
                    configFile1.Add(newLine);
                }
                else if (line.Contains("\""))
                {
                    string s = line.Replace("\"", "");
                    configFile1.Add(s);
                }
                else
                {
                    configFile1.Add(line);
                }

            }

            foreach (var r in configFile1)
            {
                if (r.StartsWith("//") || r == String.Empty)
                {
                    continue;
                }
                else if (r.Contains("//"))
                {

                    int index = r.IndexOf("//");
                    result = r.Substring(0, index);
                    configFile2.Add(result);
                }
                else if (r.Contains("\""))
                {
                    string s = r.Replace("\"", "");
                    configFile2.Add(s);
                }
                else
                {
                    configFile2.Add(r);
                }
            }

            foreach (var r in configFile2)
            {

                if (r.Contains("\""))
                {
                    string s = r.Replace("\"", "");
                    configFile3.Add(s);
                }
                else
                {
                    configFile3.Add(r);
                }
            }


            int remove = Math.Max(0, configFile3.Count - 1);
            configFile3.RemoveRange(0, remove);

            foreach (var res in configFile3)
            {

                int index = res.IndexOf('=');
                var val = res.Substring(index + 1);
                ScoreConfigFile.Add(val);
            }


            foreach (string s in ScoreConfigFile)
            {

                IPScoreArray = s.Split(',');

            }

            foreach (string str in IPScoreArray)
            {
                int index = str.IndexOf("=");
                var key = str.Substring(0, index);
                var val = str.Substring(index + 1);
                NonIntersectingPoints.Add(key, val);
            }

            return NonIntersectingPoints;
        }

        public static Hashtable GetIPScoreFile(string path)
        {
            var pathToFile = path;
            var file = File.ReadAllLines(pathToFile);
            List<string> configFile1 = new List<string>();
            List<string> configFile2 = new List<string>();
            List<string> configFile3 = new List<string>();
            List<string> ScoreConfigFile = new List<string>();
            string[] IPScoreArray = new string[0];

            string result;

            Hashtable intersectingPoints = new Hashtable();

            foreach (var line in file)
            {
                if (line.Contains(" "))
                {
                    var newLine = line.Replace(" ", "");
                    configFile1.Add(newLine);
                }
                else if (line.Contains("\""))
                {
                    string s = line.Replace("\"", "");
                    configFile1.Add(s);
                }
                else
                {
                    configFile1.Add(line);
                }

            }

            foreach (var r in configFile1)
            {
                if (r.StartsWith("//") || r == String.Empty)
                {
                    continue;
                }
                else if (r.Contains("//"))
                {

                    int index = r.IndexOf("//");
                    result = r.Substring(0, index);
                    configFile2.Add(result);
                }
                else if (r.Contains("\""))
                {
                    string s = r.Replace("\"", "");
                    configFile2.Add(s);
                }
                else
                {
                    configFile2.Add(r);
                }
            }

            foreach (var r in configFile2)
            {

                if (r.Contains("\""))
                {
                    string s = r.Replace("\"", "");
                    configFile3.Add(s);
                }
                else
                {
                    configFile3.Add(r);
                }
            }

            int remove = Math.Max(0, configFile3.Count - 2);
            configFile3.RemoveRange(0, remove);

            foreach (var res in configFile3)
            {

                int index = res.IndexOf('=');
                var val = res.Substring(index + 1);
                ScoreConfigFile.Add(val);
            }

            ScoreConfigFile.RemoveAt(1);
            foreach (string s in ScoreConfigFile)
            {

                IPScoreArray = s.Split(',');

            }

            foreach (string str in IPScoreArray)
            {
                int index = str.IndexOf("=");
                var key = str.Substring(0, index);
                var val = str.Substring(index + 1);
                intersectingPoints.Add(key, val);
            }

            return intersectingPoints;
        }

        public static Hashtable GetFile(string path)
        {
            var pathToFile = path;
            var file = File.ReadAllLines(pathToFile);
            List<string> configFile1 = new List<string>();
            List<string> configFile2 = new List<string>();
            List<string> configFile3 = new List<string>();
            string result;

            Hashtable htConfig = new Hashtable();

            foreach (var line in file)
            {
                if (line.Contains(" "))
                {
                    var newLine = line.Replace(" ", "");
                    configFile1.Add(newLine);
                }
                else if (line.Contains("\""))
                {
                    string s = line.Replace("\"", "");
                    configFile1.Add(s);
                }
                else
                {
                    configFile1.Add(line);
                }

            }

            foreach (var r in configFile1)
            {
                if (r.StartsWith("//") || r == String.Empty)
                {
                    continue;
                }
                else if (r.Contains("//"))
                {

                    int index = r.IndexOf("//");
                    result = r.Substring(0, index);
                    configFile2.Add(result);
                }
                else if (r.Contains("\""))
                {
                    string s = r.Replace("\"", "");
                    configFile2.Add(s);
                }
                else
                {
                    configFile2.Add(r);
                }
            }

            foreach (var r in configFile2)
            {

                if (r.Contains("\""))
                {
                    string s = r.Replace("\"", "");
                    configFile3.Add(s);
                }
                else
                {
                    configFile3.Add(r);
                }
            }

            foreach (var res in configFile3)
            {

                int index = res.IndexOf("=");
                var key = res.Substring(0, index);
                var val = res.Substring(index + 1);
                htConfig.Add(key, val);
            }


            return htConfig;
        }
    }
}
