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
        private const string ApiUrl = "&api_key=8732134a523b6ac1112451d04b1ef0a43df5e60da39ac2354e6c71833d856db8";

        private WebClient BaseCLient = new WebClient();

        public Root GetPrice(int limit, string coinName)
        {
            string JsonRequest = BaseCLient.DownloadString(BaseUrl + coinName + LimitUrl + limit + CoinUrl + ApiUrl);

            return JsonSerializer.Deserialize<Root>(JsonRequest);
        }
    }
}
