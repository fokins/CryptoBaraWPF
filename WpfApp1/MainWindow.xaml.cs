using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Threading;
using System.Windows.Threading;
using System.Windows.Media.Animation;
using ExchangeSharp;
using System.Windows.Navigation;
using System.Windows.Data;
using System.Windows.Controls;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    /// 

    public partial class MainWindow : Window
    {

        public PricePage pricePage = new PricePage();
        public ChartPage chartPage = new ChartPage();
        public BalancePage balancePage= new BalancePage();


        public MainWindow()
        {
            InitializeComponent();

            //ViewModels.PriceViewModel priceViewModel = new ViewModels.PriceViewModel();
            //ViewModels.ChartViewModel chartViewModel = new ViewModels.ChartViewModel();

            //pricePage.DataContext = priceViewModel;
            //chartPage.DataContext = chartViewModel;

            MainFrame.Content = pricePage;
        }

        private void PriceButton_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(pricePage);
        }

        private void ChartButton_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(chartPage);
        }

        private void BalanceButton_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(balancePage);
        }
    }
}
