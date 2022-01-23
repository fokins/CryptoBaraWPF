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
        public readonly List<string> Normalized = new List<string> { "LINK", "DOT", "ADA", "XTZ", "TRX", "CRO", "NEAR", "ATOM" };

        public readonly List<SolidColorBrush> ChartColors = new List<SolidColorBrush>() { Brushes.Purple, Brushes.Red, Brushes.Green, Brushes.DeepPink, Brushes.Orange, Brushes.Aquamarine, Brushes.BlueViolet, Brushes.LightGreen };
    }
}
