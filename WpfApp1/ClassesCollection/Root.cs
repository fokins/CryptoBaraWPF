using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.ClassesCollection
{
    public class ConversionType
    {
        public string type { get; set; }
        public string conversionSymbol { get; set; }
    }

    public class DataItem
    {
        public int time { get; set; }
        public double close { get; set; }
        public double high { get; set; }
        public double low { get; set; }
        public double open { get; set; }
        public double volumefrom { get; set; }
        public double volumeto { get; set; }
        public string conversionType { get; set; }
        public string conversionSymbol { get; set; }
    }

    public class RateLimit
    {

    }

    public class Root
    {

        public string Response { get; set; }
        public int Type { get; set; }
        public bool Aggregated { get; set; }
        public int TimeTo { get; set; }
        public int TimeFrom { get; set; }
        public bool FirstValueInArray { get; set; }
        public ConversionType ConversionType { get; set; }
        public List<DataItem> Data { get; set; }
        public RateLimit RateLimit { get; set; }
        public bool HasWarning { get; set; }
    }
}
