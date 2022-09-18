using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;
using System.Json;
using System.Security.Policy;
using System.Data.SqlClient;
using System.Runtime.InteropServices;
using Newtonsoft.Json.Linq;
using System.IO;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace CrypFolio
{
    internal static class Program
    {
        /// <summary>
        /// Der Haupteinstiegspunkt für die Anwendung.
        /// </summary>
        /// 
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }

        private static string API_KEY = "436b35d8-23b6-4c6e-9ee4-66a4d8d809e0";

        public static string makeAPICall(string func)
        {
            
            var url = new UriBuilder("https://pro-api.coinmarketcap.com/v1/cryptocurrency/listings/latest");

            var queryString = HttpUtility.ParseQueryString(string.Empty);
            queryString["start"] = "1";
            queryString["limit"] = "50";
            queryString["convert"] = "USD";

            url.Query = queryString.ToString();

            var client = new WebClient();
            client.Headers.Add("X-CMC_PRO_API_KEY", API_KEY);
            client.Headers.Add("Accepts", "application/json");
            return client.DownloadString(url.ToString());
        }

        public static JObject gethistorydata(string coin)
        {
            using (WebClient wc = new WebClient())
            {
                var data = wc.DownloadString("https://www.alphavantage.co/query?function=DIGITAL_CURRENCY_DAILY&symbol="+coin+"&market=EUR&apikey=QFWHGL257449PNAZ");
                // File.WriteAllText("history.json", data);
                JObject o = JObject.Parse(data);
                return o;
            }
        }
        
    }
}
