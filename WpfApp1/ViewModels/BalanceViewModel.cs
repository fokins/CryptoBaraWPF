using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Binance.Net;
using Binance.Net.Objects;
using Binance.Net.Objects.Spot.SpotData;
using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Objects;
using WpfApp1.ClassesCollection;
using System.Collections.ObjectModel;
using System.Windows;
using System.Threading;
using LiveCharts;
using LiveCharts.Wpf;

namespace WpfApp1.ViewModels
{
    class BalanceViewModel : BaseViewModel
    {

        Coins coins = new Coins();

        private BinanceClient client = new BinanceClient(new BinanceClientOptions()
        {
            ApiCredentials = new ApiCredentials("X26cBHT7vvw1J6tsGwRKMYkqZ7XXGvbbPvZSYfDkAFbLpw5ulgGaprHAl4qSPrHO", "coLZT42aM3FHAja1UuU2d8hg2vSEECgNg8w2WVFRjAK235U1lNXDI44dUkjCOOFP")
        });

        private ObservableCollection<BinanceAccountInfoResult> _BinanceBalance = new ObservableCollection<BinanceAccountInfoResult>();
        private ObservableCollection<Visibility> _Visibilities = new ObservableCollection<Visibility>();

        private SeriesCollection _Series = new SeriesCollection();

        public ObservableCollection<BinanceAccountInfoResult> BinanceBalance
        {
            get
            {
                return _BinanceBalance;
            }

            set
            {
                _BinanceBalance = value;
                OnPropertyChanged(nameof(BinanceBalance));
            }
        }

        public ObservableCollection<Visibility> Visibilities
        {
            get
            {
                return _Visibilities;
            }
            set
            {
                _Visibilities = value;
                OnPropertyChanged(nameof(Visibilities));
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

        private async void UpdateBinanceBalanceAsync()
        {
            var result = await client.General.GetAccountInfoAsync();

            SeriesCollection series = new SeriesCollection();

            foreach (var coin in result.Data.Balances)
            {
                if (coin.Total > 0 && coin.Asset != "BTC")
                {
                    double coinPrice = 0;

                    if (coin.Asset == "USDT")
                    {
                        coinPrice = 1;
                    }
                    else if (coin.Asset == "RUB")
                    {
                        var getCoinPrice = await client.Spot.Market.GetBookPriceAsync("USDTRUB");

                        coinPrice = 1 / (double)getCoinPrice.Data.BestAskPrice;
                    }
                    else
                    {
                        var getCoinPrice = await client.Spot.Market.GetBookPriceAsync(coin.Asset + "USDT");

                        coinPrice = (double)getCoinPrice.Data.BestAskPrice;
                    }

                    if (coins.Normalized.IndexOf(coin.Asset) == -1)
                    {
                        BinanceBalance[coins.Normalized.Count + coins.Currency.IndexOf(coin.Asset)] = new BinanceAccountInfoResult(coin.Asset, Math.Round(coin.Free, 4).ToString(), Math.Round(coin.Locked, 4).ToString(), coin.Total);
                        Visibilities[coins.Normalized.Count + coins.Currency.IndexOf(coin.Asset)] = Visibility.Visible;

                        series.Add(new PieSeries { Title = coin.Asset, Values = new ChartValues<double> { (double)coin.Free * coinPrice }, Fill = coins.ChartColors[coins.Normalized.Count + coins.Currency.IndexOf(coin.Asset)] });
                    }
                    else
                    {
                        BinanceBalance[coins.Normalized.IndexOf(coin.Asset)] = new BinanceAccountInfoResult(coin.Asset, Math.Round(coin.Free, 4).ToString(), Math.Round(coin.Locked, 4).ToString(), coin.Total);
                        Visibilities[coins.Normalized.IndexOf(coin.Asset)] = Visibility.Visible;

                        series.Add(new PieSeries { Title = coin.Asset, Values = new ChartValues<double> { (double)coin.Free * coinPrice }, Fill = coins.ChartColors[coins.Normalized.IndexOf(coin.Asset)] });
                    }
                }
            }

            Series = series;

        }

        public BalanceViewModel()
        {
            foreach (var coin in coins.Normalized)
            {
                BinanceBalance.Add(new BinanceAccountInfoResult(coin, "???", "???", 0));
                Visibilities.Add(Visibility.Collapsed);
            }

            foreach (var currency in coins.Currency)
            {
                BinanceBalance.Add(new BinanceAccountInfoResult(currency, "???", "???", 0));
                Visibilities.Add(Visibility.Collapsed);
            }


            UpdateBinanceBalanceAsync();

            Thread.Sleep(5000);
        }
    }
}
