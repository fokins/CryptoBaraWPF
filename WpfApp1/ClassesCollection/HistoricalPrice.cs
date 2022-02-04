using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace WpfApp1.ClassesCollection
{
    class HistoricalPrice
    {
        private const string BaseUrl = "https://min-api.cryptocompare.com/data/histoday?aggregate=1&e=CCCAGG&extraParams=CryptoCompare&fsym=";
        private const string LimitUrl = "&limit=";
        private const string CoinUrl = "&tryConversion=false&tsym=USD";
        private const string ApiUrl = "&api_key=379b9f987eff9f71ff804ad1142ba1e83c249ec42b38e0ad52e7c9db64926aa1";

        private WebClient BaseCLient = new WebClient();

        public Root GetPrice(int limit, string coinName)
        {
            string JsonRequest = BaseCLient.DownloadString(BaseUrl + coinName + LimitUrl + limit + CoinUrl + ApiUrl);

            return JsonSerializer.Deserialize<Root>(JsonRequest);
        }
    }
}
