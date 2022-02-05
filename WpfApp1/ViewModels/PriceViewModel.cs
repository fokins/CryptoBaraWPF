using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using ExchangeSharp;
using WpfApp1.ClassesCollection;

namespace WpfApp1.ViewModels
{
    class PriceViewModel : BaseViewModel
    {
        public static DateTime TruncateToMilliSecond(DateTime original)
        {
            return new DateTime(original.Year, original.Month, original.Day, original.Hour, original.Minute, original.Second, original.Millisecond - original.Millisecond % 500);
        }

        private Coins coins = new Coins();

        private List<Ticker> BestPrices = new List<Ticker>();
        private List<Ticker> WorstPrices = new List<Ticker>();

        private ObservableCollection<string> _bidPrices = new ObservableCollection<string>();
        private ObservableCollection<string> _askPrices = new ObservableCollection<string>();

        private ObservableCollection<string> _bidExchangeNames = new ObservableCollection<string>();
        private ObservableCollection<string> _bidExchangeLogos = new ObservableCollection<string>();

        private ObservableCollection<string> _askExchangeNames = new ObservableCollection<string>();
        private ObservableCollection<string> _askExchangeLogos = new ObservableCollection<string>();

        private ObservableCollection<string> _changePrices = new ObservableCollection<string>();
        private ObservableCollection<SolidColorBrush> _changePriceColors = new ObservableCollection<SolidColorBrush>();

        public ObservableCollection<string> BidPrices
        {
            get
            {
                return _bidPrices;
            }
            set
            {
                _bidPrices = value;
                OnPropertyChanged(nameof(BidPrices));
            }
        }

        public ObservableCollection<string> AskPrices
        {
            get
            {
                return _askPrices;
            }
            set
            {
                _askPrices = value;
                OnPropertyChanged(nameof(AskPrices));
            }
        }

        public ObservableCollection<string> ChangePrices
        {
            get
            {
                return _changePrices;
            }
            set
            {
                _changePrices = value;
                OnPropertyChanged(nameof(ChangePrices));
            }
        }

        public ObservableCollection<SolidColorBrush> ChangePriceColors
        {
            get
            {
                return _changePriceColors;
            }
            set
            {
                _changePriceColors = value;
                OnPropertyChanged(nameof(ChangePriceColors));
            }
        }

        public ObservableCollection<string> BidExchangeNames
        {
            get
            {
                return _bidExchangeNames;
            }
            set
            {
                _bidExchangeNames = value;
                OnPropertyChanged(nameof(BidExchangeNames));
            }
        }

        public ObservableCollection<string> BidExchangeLogos
        {
            get
            {
                return _bidExchangeLogos;
            }
            set
            {
                _bidExchangeLogos = value;
                OnPropertyChanged(nameof(BidExchangeLogos));
            }
        }

        public ObservableCollection<string> AskExchangeNames
        {
            get
            {
                return _askExchangeNames;
            }
            set
            {
                _askExchangeNames = value;
                OnPropertyChanged(nameof(AskExchangeNames));
            }
        }

        public ObservableCollection<string> AskExchangeLogos
        {
            get
            {
                return _askExchangeLogos;
            }
            set
            {
                _askExchangeLogos = value;
                OnPropertyChanged(nameof(AskExchangeLogos));
            }
        }

        public PriceViewModel()
        {
            List<string> GateIoCoinNames = new List<string>();
            List<string> KrakenCoinNames = new List<string>();
            List<string> KucoinCoinNames = new List<string>();
            List<string> BinanceCoinNames = new List<string>();

            List<string> ExchangeNames = new List<string>() { ExchangeName.GateIo, ExchangeName.Kraken, ExchangeName.Kucoin, ExchangeName.Binance };

            Dictionary<string, List<string>> CoinNames = new Dictionary<string, List<string>>();

            foreach (string coinName in coins.Normalized)
            {
                GateIoCoinNames.Add(coinName.ToLower() + "_usdt");
                KrakenCoinNames.Add(coinName + "USD");
                KucoinCoinNames.Add(coinName + "-USDT");
                BinanceCoinNames.Add(coinName + "USDT");
                BidPrices.Add("???");
                AskPrices.Add("???");
                ChangePrices.Add("???");
                BidExchangeNames.Add("???");
                AskExchangeNames.Add("???");

                BidExchangeLogos.Add("/Images/Exchanges/default.png");
                AskExchangeLogos.Add("/Images/Exchanges/default.png");
                ChangePriceColors.Add(Brushes.Green);
            }

            CoinNames[ExchangeName.GateIo] = GateIoCoinNames;
            CoinNames[ExchangeName.Kraken] = KrakenCoinNames;
            CoinNames[ExchangeName.Kucoin] = KucoinCoinNames;
            CoinNames[ExchangeName.Binance] = BinanceCoinNames;

            for (int i = 0; i < GateIoCoinNames.Count; i++)
            {
                BestPrices.Add(new Ticker(new ExchangeTicker(), "DefaultName", -1, DateTime.Now));
            }

            for (int i = 0; i < GateIoCoinNames.Count; i++)
            {
                WorstPrices.Add(new Ticker(new ExchangeTicker(), "DefaultName", -1, DateTime.Now));
            }

            foreach (var ExchangeName in ExchangeNames)
            {
                for (int i = 0; i < CoinNames[ExchangeName].Count; i++)
                {

                    int curCoinNum = i;

                    var GetTickerTask = Task.Factory.StartNew(async () =>
                    {
                        var CurExchangeAPI = await ExchangeAPI.GetExchangeAPIAsync(ExchangeName);

                        HistoricalPrice historicalPrice = new HistoricalPrice();

                        while (true)
                        {

                            if (CoinNames[ExchangeName][curCoinNum] != null)
                            {
                                try
                                {
                                    #region Get ask/bid prices

                                    var CurExchangeTicker = await CurExchangeAPI.GetTickerAsync(CoinNames[ExchangeName][curCoinNum]);

                                    Ticker CurTicker = new Ticker(CurExchangeTicker, ExchangeName, curCoinNum, DateTime.Now);

                                    if (TruncateToMilliSecond(BestPrices[curCoinNum].TickerTime) != TruncateToMilliSecond(CurTicker.TickerTime) || (BestPrices[curCoinNum].TickerValue.Bid < CurTicker.TickerValue.Bid))
                                    {

                                        BestPrices[curCoinNum] = CurTicker;

                                        BidPrices[curCoinNum] = Math.Round(CurTicker.TickerValue.Bid, 4).ToString() + "$";

                                        BidExchangeNames[curCoinNum] = CurTicker.ExchangeName;

                                        BidExchangeLogos[curCoinNum] = "/Images/Exchanges/" + CurTicker.ExchangeName.ToLower() + ".png";
                                    }

                                    if (TruncateToMilliSecond(WorstPrices[curCoinNum].TickerTime) != TruncateToMilliSecond(CurTicker.TickerTime) || (WorstPrices[curCoinNum].TickerValue.Ask > CurTicker.TickerValue.Ask))
                                    {

                                        WorstPrices[curCoinNum] = CurTicker;

                                        AskPrices[curCoinNum] = Math.Round(CurTicker.TickerValue.Ask, 4).ToString() + "$";

                                        AskExchangeNames[curCoinNum] = CurTicker.ExchangeName;

                                        AskExchangeLogos[curCoinNum] = "/Images/Exchanges/" + CurTicker.ExchangeName.ToLower() + ".png";

                                    }

                                    #endregion
                                    #region Get Daily coin price change

                                    Root lastPrice = historicalPrice.GetPrice(1, coins.Normalized[curCoinNum]);

                                    if (lastPrice.Data[1].high > lastPrice.Data[0].high)
                                    {
                                        ChangePriceColors[curCoinNum] = Brushes.Green;
                                        ChangePrices[curCoinNum] = $"{Math.Round(100 - lastPrice.Data[0].high / lastPrice.Data[1].high * 100, 2)}%";
                                    }
                                    else
                                    {
                                        ChangePriceColors[curCoinNum] = Brushes.Red;
                                        ChangePrices[curCoinNum] = $"{Math.Round(100 - lastPrice.Data[1].high / lastPrice.Data[0].high * 100, 2)}%";
                                    }

                                    #endregion
                                }
                                catch
                                {
                                    Thread.Sleep(1000);
                                }

                                Thread.Sleep(5000);
                            }
                        }
                    });

                }
            }
        }
    }
}
