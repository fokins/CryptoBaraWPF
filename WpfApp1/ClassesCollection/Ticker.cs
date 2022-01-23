using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExchangeSharp;

namespace WpfApp1.ClassesCollection
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
}
