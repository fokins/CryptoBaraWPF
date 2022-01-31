using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace WpfApp1.ClassesCollection
{
    public class Coins
    {
        public readonly List<string> Normalized = new List<string> { "LINK", "DOT", "ADA", "XTZ", "TRX", "CRO", "NEAR", "ATOM", "AVAX", "XRP", "SOL", "FTM", "XLM" };

        public readonly List<string> FullNames = new List<string> { "Chainlink", "Polkadot", "Cardano", "Tezos", "Tron", "Cryptocom", "Near", "Cosmos", "Avalanche", "Ripple", "Solana", "Fantom", "Stellar" };

        public readonly List<string> Currency = new List<string> { "RUB", "USDT" };

        public List<SolidColorBrush> ChartColors = new List<SolidColorBrush>() { Brushes.Pink, Brushes.HotPink, Brushes.Red, Brushes.DarkRed, Brushes.Orange, Brushes.YellowGreen, Brushes.LightGreen, Brushes.Green, Brushes.DarkGreen, Brushes.Cyan, Brushes.Blue, Brushes.Violet, Brushes.Purple};
    }
}
