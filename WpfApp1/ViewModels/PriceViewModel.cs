using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
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

        public PriceViewModel()
        {
            List<string> GateIoCoinNames = new List<string>();
            List<string> KrakenCoinNames = new List<string>();
            List<string> KucoinCoinNames = new List<string>();

            List<string> ExchangeNames = new List<string>() { ExchangeName.GateIo, ExchangeName.Kraken, ExchangeName.Kucoin };

            Dictionary<string, List<string>> CoinNames = new Dictionary<string, List<string>>();

            foreach (string coinName in coins.Normalized)
            {
                GateIoCoinNames.Add(coinName.ToLower() + "_usdt");
                KrakenCoinNames.Add(coinName + "USD");
                KucoinCoinNames.Add(coinName + "-USDT");
                BidPrices.Add("???");
                AskPrices.Add("???");
            }

            CoinNames[ExchangeName.GateIo] = GateIoCoinNames;
            CoinNames[ExchangeName.Kraken] = KrakenCoinNames;
            CoinNames[ExchangeName.Kucoin] = KucoinCoinNames;

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

                    int curBoxNum = i;

                    var GetTickerTask = Task.Factory.StartNew(async () =>
                    {
                        var CurExchangeAPI = await ExchangeAPI.GetExchangeAPIAsync(ExchangeName);

                        while (true)
                        {

                            if (CoinNames[ExchangeName][curBoxNum] != null)
                            {

                                var CurExchangeTicker = await CurExchangeAPI.GetTickerAsync(CoinNames[ExchangeName][curBoxNum]);

                                Ticker CurTicker = new Ticker(CurExchangeTicker, ExchangeName, curBoxNum, DateTime.Now);

                                if (TruncateToMilliSecond(BestPrices[curBoxNum].TickerTime) != TruncateToMilliSecond(CurTicker.TickerTime) || (BestPrices[curBoxNum].TickerValue.Bid < CurTicker.TickerValue.Bid))
                                {

                                    BestPrices[curBoxNum] = CurTicker;

                                    BidPrices[curBoxNum] = Math.Round(CurTicker.TickerValue.Bid, 3).ToString() + "$";
                                }

                                if (TruncateToMilliSecond(WorstPrices[curBoxNum].TickerTime) != TruncateToMilliSecond(CurTicker.TickerTime) || (WorstPrices[curBoxNum].TickerValue.Ask > CurTicker.TickerValue.Ask))
                                {

                                    WorstPrices[curBoxNum] = CurTicker;

                                    AskPrices[curBoxNum] = Math.Round(CurTicker.TickerValue.Ask, 3).ToString() + "$";

                                }

                                Thread.Sleep(100);
                            }
                        }
                    });

                }
            }
        }
    }
}
