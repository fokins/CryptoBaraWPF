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

        public List<ChartValues<OhlcPoint>> OhlcChartsVal = new List<ChartValues<OhlcPoint>>();
        public List<ChartValues<ObservablePoint>> DefChartsVal = new List<ChartValues<ObservablePoint>>();

        public List<string> CoinNames = new List<string>() { "LINK", "DOT", "ADA", "XTZ", "TRX" };

        public List<bool> isChecked = new List<bool>() { false, false, false, false, false };

        public int ChartType = 0;

        public WebClient BaseCLient = new WebClient();

        public void UpdateCharts()
        {
            if(Chart.Series != null) Chart.Series.Clear();

            int count = 0;

            SeriesCollection Series = new SeriesCollection();

            if (ChartType == 0)
            {
                foreach (var item in OhlcChartsVal)
                {
                    if (isChecked[count])
                    {
                        Series.Add(new OhlcSeries { Values = item, Title = "", Stroke = Brushes.Transparent, Fill = Brushes.Transparent });
                    }

                    count++;
                }
            }else if(ChartType == 1)
            {
                foreach(var item in DefChartsVal)
                {
                    if (isChecked[count])
                    {
                        Series.Add(new LineSeries { Values = item, Title = "", Stroke = Brushes.Blue, Fill = Brushes.Transparent });
                    }
                    
                    count++;
                }
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

                //OhlcChartsVal.Add( new ChartValues<OhlcPoint> { new OhlcPoint(0, 0, 0, 0) });
                //DefChartsVal.Add(new ChartValues<ObservablePoint> { new ObservablePoint(0, 0) });

                foreach (var item in ticker.Data)
                {
                    if (OhlcChartsVal.Count != count)
                    {
                        OhlcChartsVal[count].Add(new OhlcPoint(item.open, item.high, item.low, item.close));
                    }
                    else
                    {
                        OhlcChartsVal.Add(new ChartValues<OhlcPoint> { new OhlcPoint(item.open, item.high, item.low, item.close) });
                    }

                    if (DefChartsVal.Count != count)
                    {
                        DefChartsVal[count].Add(new ObservablePoint(item.time, item.high));
                    }
                    else
                    {
                        DefChartsVal.Add(new ChartValues<ObservablePoint> { new ObservablePoint(item.time, item.high) });
                    }
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

        private void DefChecked(object sender, RoutedEventArgs e)
        {
            ChartType = 1;

            UpdateCharts();
        }

        private void OhclChecked(object sender, RoutedEventArgs e)
        {
            ChartType = 0;

            UpdateCharts();
        }
    }
}
