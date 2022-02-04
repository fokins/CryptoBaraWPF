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

        private readonly int ChartColorsCount = 10;

        public List<SolidColorBrush> ChartColors = new List<SolidColorBrush>() { };

        public Coins()
        {
            List<SolidColorBrush> AllColors = new List<SolidColorBrush>();

            int colorsCount = Normalized.Count + Currency.Count;

            for(int r = 1; r < ChartColorsCount; r++)
            {
                for(int g = 1; g < ChartColorsCount; g++)
                {
                    for(int b = 1; b < ChartColorsCount; b++)
                    {
                        if (r != g && g != b && b != r)
                        {
                            AllColors.Add(new SolidColorBrush(Color.FromRgb((byte)(256 / ChartColorsCount * r), (byte)(256 / ChartColorsCount * g), (byte)(256 / ChartColorsCount * b))));
                        }
                    }
                }
            }

            for(int i = 0; i < colorsCount; i++)
            {
                ChartColors.Add(AllColors[AllColors.Count / colorsCount * i]);
            }
        }
    }
}
