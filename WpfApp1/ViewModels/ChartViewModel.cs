using LiveCharts;
using LiveCharts.Defaults;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using WpfApp1.ClassesCollection;
using System.Text.Json;
using LiveCharts.Wpf;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Threading;
using OxyPlot;

namespace WpfApp1.ViewModels
{
    class ChartViewModel : BaseViewModel
    {
        private List<ChartValues<OhlcPoint>> OhlcChartsVal = new List<ChartValues<OhlcPoint>>();
        private List<ChartValues<ObservablePoint>> DefChartsVal = new List<ChartValues<ObservablePoint>>();

        private Coins coins = new Coins();

        private bool _isOhlcChartType = true;

        private WebClient BaseCLient = new WebClient();

        private SeriesCollection _Series = new SeriesCollection();

        private ObservableCollection<bool> _Checked = new ObservableCollection<bool>();

        private RelayCommand _UpdateCommand;
        public RelayCommand UpdateCommand
        {
            get
            {
                return _UpdateCommand ??
                    (_UpdateCommand = new RelayCommand(obj =>
                    {
                        UpdateSeries();
                    }));
            }
        }

        public ObservableCollection<bool> Checked
        {
            get
            {
                return _Checked;
            }
            set
            {
                _Checked = value;
                OnPropertyChanged(nameof(Checked));
            }
        }

        public SeriesCollection Series
        {
            get
            {
                return _Series;
            }
            set
            {
                _Series = value;
                OnPropertyChanged(nameof(Series));
            }
        }

        public bool isOhlcChartType
        {
            get
            {
                return _isOhlcChartType;
            }
            set
            {
                _isOhlcChartType = value;
                OnPropertyChanged(nameof(isOhlcChartType));
            }
        }

        public void UpdateSeries()
        {
            int count = 0;

            SeriesCollection series = new SeriesCollection();


            if (_isOhlcChartType)
            {
                foreach (var item in OhlcChartsVal)
                {
                    if (Checked[count])
                    {
                        series.Add(new OhlcSeries { Values = item, Title = "", Stroke = Brushes.Transparent, Fill = Brushes.Transparent });
                    }

                    count++;
                }
            }
            else
            {
                foreach (var item in DefChartsVal)
                {
                    if (Checked[count])
                    {
                        series.Add(new LineSeries { Values = item, Title = "", Stroke = coins.ChartColors[count], Fill = Brushes.Transparent, PointGeometry = null });
                    }

                    count++;
                }
            }

            Series = series;
        }

        public ChartViewModel()
        {
            const string BaseURL = "https://min-api.cryptocompare.com/data/histoday?aggregate=1&e=CCCAGG&extraParams=CryptoCompare&fsym=";
            const string CurrencyURL = "&limit=365&tryConversion=false&tsym=USD";

            int count = 0;

            Root ticker = new Root();

            foreach (var CoinName in coins.Normalized)
            {
                Checked.Add(false);

                string JsonString = BaseCLient.DownloadString(BaseURL + CoinName + CurrencyURL);

                ticker = JsonSerializer.Deserialize<Root>(JsonString);

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
        }
    }
}
