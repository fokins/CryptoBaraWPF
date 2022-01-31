using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.ViewModels;

namespace WpfApp1.ClassesCollection
{
    class BinanceAccountInfoResult : BaseViewModel
    {
        private string _Name;
        private string _Free;
        private string _Locked;
        private decimal _Total;

        public string Free
        {
            get
            {
                return _Free;
            }
            set
            {
                _Free = value;
                OnPropertyChanged(nameof(Free));
            }
        }

        public string Locked
        {
            get
            {
                return _Locked;
            }
            set
            {
                _Locked = value;
                OnPropertyChanged(nameof(Locked));
            }
        }

        public decimal Total
        {
            get
            {
                return _Total;
            }
            set
            {
                _Total = value;
                OnPropertyChanged(nameof(Total));
            }
        }

        public string Name
        {
            get
            {
                return _Name;
            }
            set
            {
                _Name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        public BinanceAccountInfoResult(string name, string free, string locked, decimal total)
        {
            Name = name;
            Free = free;
            Locked = locked;
            Total = total;
        }
    }
}
