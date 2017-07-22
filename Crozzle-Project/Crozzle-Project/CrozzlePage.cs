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
    public partial class CrozzlePage : Form
    {
        private DataGridView CreateDataGridView;

        public CrozzlePage()
        {
            InitializeComponent();
        }

        private void CrozzlePage_Load(object sender, EventArgs e)
        {
            string fPath = @"D:\CrozzleTxt\Test 1 Configuration.txt";
            Hashtable ht = GetFile(fPath);
            Configuration config = new Configuration();
            config.CreateConfig(config, ht);

            //CreateDataGridView = new DataGridView();
            //CreateDataGridView.Size = new Size(300, 542);
            //CreateDataGridView.Location = new Point(10, 12);
            //this.Controls.Add(CreateDataGridView);

            

        }

        public static Hashtable GetFile(string path)
        {
            var pathToFile = path;
            var file = File.ReadAllLines(pathToFile);
            List<string> configFile = new List<string>();
            List<string> configFileNew = new List<string>();
            string result;

            Hashtable htConfig = new Hashtable();

            foreach (var line in file)
            {
                var newLine = line.Replace(" ", "");
                configFile.Add(newLine);

            }

            foreach (var r in configFile)
            {
                if (r.StartsWith("//") || r == String.Empty)
                {
                    continue;
                }
                else if (r.Contains("//"))
                {

                    int index = r.IndexOf("//");
                    result = r.Substring(0, index);
                    configFileNew.Add(result);
                }
                else
                {
                    configFileNew.Add(r);
                }
            }

            foreach (var res in configFileNew)
            {
                int len = res.Length;
                int last = len - 1;
                int index = res.IndexOf("=");
                var key = res.Substring(0, index);
                var val = res.Substring(index + 1);
                htConfig.Add(key, val);
            }


            return htConfig;
        }

        
    }

}
