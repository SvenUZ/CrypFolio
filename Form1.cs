using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CrypFolio
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            // string output = Program.makeAPICall();
            // output = JsonConvert.SerializeObject(output, Formatting.None);
            // File.WriteAllText("output.json", output);
        }

        private void chart1_Click(object sender, EventArgs e)
        {
            this.chart1.Series.Clear();
            this.chart1.Titles.Add("BTC");




        }
    }
}
