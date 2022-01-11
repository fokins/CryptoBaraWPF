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
using System.Threading;
using ExchangeSharp;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для PricePage.xaml
    /// </summary>
    public partial class PricePage : Page
    {
        public class Ticker
        {
            public ExchangeTicker TickerValue;
            public string ExchangeName;
            public int TickerNum;
            public DateTime TickerTime;

            public Ticker(ExchangeTicker val, string name, int num, DateTime time)
            {
                TickerValue = val;
                ExchangeName = name;
                TickerNum = num;
                TickerTime = time;
            }

            public string getString()
            {
                return $"{ExchangeName} bid: {TickerValue.Bid} ask: {TickerValue.Ask}   {TickerTime}";
            }

        }

        public static DateTime TruncateToMilliSecond(DateTime original)
        {
            return new DateTime(original.Year, original.Month, original.Day, original.Hour, original.Minute, original.Second, original.Millisecond - original.Millisecond % 500);
        }

        List<Ticker> BestPrices = new List<Ticker>();
        List<Ticker> WorstPrices = new List<Ticker>();

        decimal Balance;

        public PricePage()
        {
            InitializeComponent();

            UpdatePriceBlock();

            UpdateBalance();
        }

        void UpdateBalance()
        {
            const decimal startBalance = 100;

            Balance = startBalance;

            var UpdateBalanceTask = Task.Run(async () => {

                while (true)
                {
                    Thread.Sleep(100);

                    SetPriceBlock(0, $"Demo balance:{Math.Round(Balance, 3).ToString()}$", "balance");
                }

            });

            var PriceMonitorTask = Task.Run(async () => {

                Thread.Sleep(5000);

                while (true)
                {
                    decimal bestPriceNum = BestPrices[0].TickerValue.Bid / WorstPrices[0].TickerValue.Ask;
                    int bestPrice = 0;

                    for (int i = 1; i < BestPrices.Count; i++)
                    {
                        if (BestPrices[i].TickerValue.Bid / WorstPrices[i].TickerValue.Ask > bestPriceNum)
                        {
                            bestPrice = i;
                            bestPriceNum = BestPrices[i].TickerValue.Bid / WorstPrices[i].TickerValue.Ask;
                        }
                    }

                    if (bestPriceNum > 1)
                    {
                        Thread.Sleep(1000);

                        decimal buyPrice = BestPrices[bestPrice].TickerValue.Bid;

                        Thread.Sleep(1000);

                        Balance = Balance * buyPrice / WorstPrices[bestPrice].TickerValue.Ask;
                    }
                }
            });
        }

        void UpdatePriceBlock()
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

                    var GetTickerTask = Task.Run(async () =>
                    {
                        var CurExchangeAPI = await ExchangeAPI.GetExchangeAPIAsync(ExchangeName);

                        while (true)
                        {
                            var CurExchangeTicker = await CurExchangeAPI.GetTickerAsync(CoinNames[ExchangeName][curBoxNum]);

                            Ticker CurTicker = new Ticker(CurExchangeTicker, ExchangeName, curBoxNum, DateTime.Now);

                            if (TruncateToMilliSecond(BestPrices[curBoxNum].TickerTime) != TruncateToMilliSecond(CurTicker.TickerTime) || (BestPrices[curBoxNum].TickerValue.Bid < CurTicker.TickerValue.Bid))
                            {
                                lock (BestPrices)
                                {
                                    BestPrices[curBoxNum] = CurTicker;

                                    SetPriceBlock(curBoxNum, Math.Round(CurTicker.TickerValue.Bid, 3).ToString() + "$", "best");
                                }
                            }

                            if (TruncateToMilliSecond(WorstPrices[curBoxNum].TickerTime) != TruncateToMilliSecond(CurTicker.TickerTime) || (WorstPrices[curBoxNum].TickerValue.Ask > CurTicker.TickerValue.Ask))
                            {
                                lock (WorstPrices)
                                {
                                    WorstPrices[curBoxNum] = CurTicker;

                                    SetPriceBlock(curBoxNum, Math.Round(CurTicker.TickerValue.Ask, 3).ToString() + "$", "worst");
                                }
                            }

                            Thread.Sleep(100);
                        }
                    });

                }
            }

        }

        void SetPriceBlock(int num, string value, string type)
        {
            if (type == "best")
            {
                switch (num)
                {
                    case 0:
                        Dispatcher.Invoke(() => LinkBestPriceBlock.Text = value);
                        break;
                    case 1:
                        Dispatcher.Invoke(() => DotBestPriceBlock.Text = value);
                        break;
                    case 2:
                        Dispatcher.Invoke(() => AdaBestPriceBlock.Text = value);
                        break;
                    case 3:
                        Dispatcher.Invoke(() => XtzBestPriceBlock.Text = value);
                        break;
                    case 4:
                        Dispatcher.Invoke(() => TrxBestPriceBlock.Text = value);
                        break;
                }
            }
            else if (type == "worst")
            {
                switch (num)
                {
                    case 0:
                        Dispatcher.Invoke(() => LinkWorstPriceBlock.Text = value);
                        break;
                    case 1:
                        Dispatcher.Invoke(() => DotWorstPriceBlock.Text = value);
                        break;
                    case 2:
                        Dispatcher.Invoke(() => AdaWorstPriceBlock.Text = value);
                        break;
                    case 3:
                        Dispatcher.Invoke(() => XtzWorstPriceBlock.Text = value);
                        break;
                    case 4:
                        Dispatcher.Invoke(() => TrxWorstPriceBlock.Text = value);
                        break;
                }
            }
            else if (type == "balance")
            {
                Dispatcher.Invoke(() => BalanceBlock.Text = value);
            }
        }

        private void PriceButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ChartButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ChartPage());
        }
    }
}
