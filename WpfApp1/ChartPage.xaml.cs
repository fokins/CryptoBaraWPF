using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using System.Threading;
using System.Text.Json;
using System.Net;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для ChartPage.xaml
    /// </summary>
    public partial class ChartPage : Page
    {

        public ChartPage()
        {
            InitializeComponent();
        }


        public class ConversionType
        {
            public string type { get; set; }
            public string conversionSymbol { get; set; }
        }

        public class DataItem
        {
            public int time { get; set; }
            public double close { get; set; }
            public double high { get; set; }
            public double low { get; set; }
            public double open { get; set; }
            public double volumefrom { get; set; }
            public double volumeto { get; set; }
            public string conversionType { get; set; }
            public string conversionSymbol { get; set; }
        }

        public class RateLimit
        {

        }

        public class Root
        {

            public string Response { get; set; }
            public int Type { get; set; }
            public bool Aggregated { get; set; }
            public int TimeTo { get; set; }
            public int TimeFrom { get; set; }
            public bool FirstValueInArray { get; set; }
            public ConversionType ConversionType { get; set; }
            public List<DataItem> Data { get; set; }
            public RateLimit RateLimit { get; set; }
            public bool HasWarning { get; set; }
        }

        public List<ChartValues<OhlcPoint>> ChartsVal = new List<ChartValues<OhlcPoint>>();

        public List<string> CoinNames = new List<string>() { "LINK", "DOT", "ADA", "XTZ", "TRX" };

        public List<bool> isChecked = new List<bool>() { false, false, false, false, false };

        public WebClient BaseCLient = new WebClient();

        public void UpdateCharts()
        {
            if(Chart.Series != null) Chart.Series.Clear();

            int count = 0;

            SeriesCollection Series = new SeriesCollection();

            foreach(var item in ChartsVal)
            {
                if (isChecked[count])
                {
                    Series.Add(new OhlcSeries { Values = item, Title = "", Stroke = Brushes.Transparent, Fill = Brushes.Transparent });
                }

                count++;
            }

            Chart.Series = Series;
        }

        private void PriceButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new PricePage());
        }

        private void ChartButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            UpdateCharts();
        }

        private void LoadButton_Click(object sender, RoutedEventArgs e)
        {
            const string BaseURL = "https://min-api.cryptocompare.com/data/histoday?aggregate=1&e=CCCAGG&extraParams=CryptoCompare&fsym=";
            const string CurrencyURL = "&limit=365&tryConversion=false&tsym=USD";

            int count = 0;

            Root ticker = new Root();

            foreach (var CoinName in CoinNames)
            {

                string JsonString = BaseCLient.DownloadString(BaseURL + CoinName + CurrencyURL);

                ticker = JsonSerializer.Deserialize<Root>(JsonString);

                ChartsVal.Add( new ChartValues<OhlcPoint> { new OhlcPoint(0, 0, 0, 0) });

                foreach (var item in ticker.Data)
                {
                    ChartsVal[count].Add(new OhlcPoint(item.open, item.high, item.low, item.close));
                }

                count++;
            }
            StatusBlock.Text = "Finished!!!";
        }

        private void LinkBoxChecked(object sender, RoutedEventArgs e)
        {
            isChecked[0] = true;

            UpdateCharts();
        }

        private void DotBoxChecked(object sender, RoutedEventArgs e)
        {
            isChecked[1] = true;

            UpdateCharts();
        }

        private void LinkBoxUnchecked(object sender, RoutedEventArgs e)
        {
            isChecked[0] = false;

            UpdateCharts();
        }

        private void DotBoxUnchecked(object sender, RoutedEventArgs e)
        {
            isChecked[1] = false;

            UpdateCharts();
        }

        private void AdaBoxChecked(object sender, RoutedEventArgs e)
        {
            isChecked[2] = true;

            UpdateCharts();
        }

        private void AdaBoxUnchecked(object sender, RoutedEventArgs e)
        {
            isChecked[2] = false;

            UpdateCharts();
        }

        private void XtzBoxChecked(object sender, RoutedEventArgs e)
        {
            isChecked[3] = true;

            UpdateCharts();
        }

        private void XtzBoxUnchecked(object sender, RoutedEventArgs e)
        {
            isChecked[3] = false;

            UpdateCharts();
        }

        private void TrxBoxChecked(object sender, RoutedEventArgs e)
        {
            isChecked[4] = true;

            UpdateCharts();
        }

        private void TrxBoxUnchecked(object sender, RoutedEventArgs e)
        {
            isChecked[4] = false;

            UpdateCharts();
        }
    }
}
