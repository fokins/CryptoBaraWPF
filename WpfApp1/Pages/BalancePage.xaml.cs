using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
using WpfApp1.ClassesCollection;
using WpfApp1.UICollection;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для BalancePage.xaml
    /// </summary>
    public partial class BalancePage : Page
    {
        private Coins coins = new Coins();

        public BalancePage()
        {
            InitializeComponent();

            ViewModels.BalanceViewModel balanceViewModel = new ViewModels.BalanceViewModel();

            DataContext = balanceViewModel;

            foreach (var coin in balanceViewModel.BinanceBalance)
            {

                int ind = coins.Normalized.IndexOf(coin.Name);

                if (ind == -1)
                {
                    ind = coins.Normalized.Count + coins.Currency.IndexOf(coin.Name);
                }

                BalanceBorder priceBorder = new BalanceBorder(coin.Name);

                Binding FreeBinding = new Binding();
                FreeBinding.Source = balanceViewModel;
                FreeBinding.Path = new PropertyPath("BinanceBalance[" + ind.ToString() + "].Free");

                priceBorder.FreeTextBlock.SetBinding(TextBlock.TextProperty, FreeBinding);

                Binding LockedBinding = new Binding();
                LockedBinding.Source = balanceViewModel;
                LockedBinding.Path = new PropertyPath("BinanceBalance[" + ind.ToString() + "].Locked");

                priceBorder.LockedTextBlock.SetBinding(TextBlock.TextProperty, LockedBinding);

                Binding VisibilityBinding = new Binding();
                VisibilityBinding.Source = balanceViewModel;
                VisibilityBinding.Path = new PropertyPath("Visibilities[" + ind.ToString() + "]");

                priceBorder.MainBorder.SetBinding(Border.VisibilityProperty, VisibilityBinding);

                BalanceStackPanel.Children.Add(priceBorder.MainBorder);
            }
        }
    }
}
