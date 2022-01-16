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
using WpfApp1.Classes;

namespace WpfApp1.ViewModels
{
    class PriceViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        public static DateTime TruncateToMilliSecond(DateTime original)
        {
            return new DateTime(original.Year, original.Month, original.Day, original.Hour, original.Minute, original.Second, original.Millisecond - original.Millisecond % 500);
        }

        private List<Ticker> BestPrices = new List<Ticker>();
        private List<Ticker> WorstPrices = new List<Ticker>();

        private ObservableCollection<string> _bidPrices = new ObservableCollection<string> { "???", "???", "???", "???", "???" };
        private ObservableCollection<string> _askPrices = new ObservableCollection<string> { "???", "???", "???", "???", "???" };

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
            List<string> GateIoCoinNames = new List<string>() { "link_usdt", "dot_usdt", "ada_usdt", "xtz_usdt", "trx_usdt" };
            List<string> KrakenCoinNames = new List<string>() { "LINKUSD", "DOTUSD", "ADAUSD", "XTZUSD", "TRXUSD" };
            List<string> KucoinCoinNames = new List<string>() { "LINK-USDT", "DOT-USDT", "ADA-USDT", "XTZ-USDT", "TRX-USDT" };

            List<string> ExchangeNames = new List<string>() { ExchangeName.GateIo, ExchangeName.Kraken, ExchangeName.Kucoin };

            Dictionary<string, List<string>> CoinNames = new Dictionary<string, List<string>>();

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
                    });

                }
            }
        }
    }
}
