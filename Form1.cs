using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Json;
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
            chart1.Series.Add("BTC");

            var root = Program.gethistorydata();
            
            JObject timeseries = (JObject)root["Time Series (Digital Currency Daily)"];
           
            DateTime myDate = DateTime.Now;
            DateTime lastyear = myDate.AddYears(-3);

            List<DateTime> datelist = new List<DateTime>();
            List<Double> pricelist = new List<Double>();

            for (int i = 0; i <  timeseries.Count; i++)
            {
                // Price Object for each day
                var item = (JObject)timeseries[myDate.AddDays(-i).ToString("yyyy-MM-dd")];

                //  Unix time
                JProperty prob = (JProperty)item.Parent;
                var time = prob.Name.ToString();
                datelist.Add(DateTime.Parse(time));
                

                //  Bitcoin price at that time
                double y = (double)item["4a. close (EUR)"];
                pricelist.Add(y);
            }
            datelist.Reverse();
            pricelist.Reverse();
            
            for(int i = 0; i < pricelist.Count; i++)
            {
                String date = datelist[i].ToString("yyyy-MM-dd");
                Double price = pricelist[i];
                chart1.Series["BTC"].Points.AddXY(date, price);
            }
        }
    }
}
