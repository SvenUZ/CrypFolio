using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using System.Web.UI.WebControls;

namespace CrypFolio
{
    internal class KrakenAPI
    {
        public static JObject getJSON(string coin, int unixtimestamp)
        {
            string coinpair = coin + "EUR";
            using (WebClient wc = new WebClient())
            {
                var data = wc.DownloadString("https://api.kraken.com/0/public/Trades?pair="+coinpair+"&since="+unixtimestamp);
                JObject o = JObject.Parse(data);
                return o;
            }
        }
        public static List<double> DataToPrice(JObject json)
        {
            List<double> priceList = new List<double>();

            JObject rootJ = (JObject)json["result"];
            JProperty prob = rootJ.Properties().First();

            var coinpair = prob.Name;
            JArray timeseries = (JArray)rootJ[coinpair];

            for (int i = 0; i < timeseries.Count; i++)
            {
                JArray timestamp = (JArray)timeseries[i];
                double price = (double)timestamp[0];
                priceList.Add(price);
            }
            return priceList;
        }

        public static List<string> DataToTime(JObject json)
        {
            List<string> dateList = new List<string>();

            JObject rootJ = (JObject)json["result"];
            JProperty prob = rootJ.Properties().First();

            var coinpair = prob.Name;
            JArray timeseries = (JArray)rootJ[coinpair];

            for (int i = 0; i < timeseries.Count; i++)
            {
                JArray timestamp = (JArray)timeseries[i];
                double unix = (double)timestamp[2];
                DateTime date = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddSeconds(unix);
                dateList.Add(date.ToString());
            }
            //dateList.Reverse();
            return dateList;
        }

    }
}
