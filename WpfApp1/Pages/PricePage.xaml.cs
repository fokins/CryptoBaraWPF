﻿using System;
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
using System.Runtime.CompilerServices;
using WpfApp1.ClassesCollection;
using WpfApp1.UICollection;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для PricePage.xaml
    /// </summary>
    public partial class PricePage : Page
    {

        private Coins coins = new Coins();

        public PricePage()
        {
            InitializeComponent();

            ViewModels.PriceViewModel priceViewModel = new ViewModels.PriceViewModel();

            DataContext = priceViewModel;

            int count = 0;

            foreach (var coinName in coins.Normalized)
            {
                PriceBorder priceBorder = new PriceBorder(coinName, coins.FullNames[count]);

                Binding BidBinding = new Binding();
                BidBinding.Source = priceViewModel;
                BidBinding.Path = new PropertyPath("BidPrices[" + count.ToString() + "]");

                priceBorder.BidTextBlock.SetBinding(TextBlock.TextProperty, BidBinding);

                Binding AskBinding = new Binding();
                AskBinding.Source = priceViewModel;
                AskBinding.Path = new PropertyPath("AskPrices[" + count.ToString() + "]");

                priceBorder.AskTextBlock.SetBinding(TextBlock.TextProperty, AskBinding);

                Binding ChangeBinding = new Binding();
                ChangeBinding.Source = priceViewModel;
                ChangeBinding.Path = new PropertyPath("ChangePrices[" + count.ToString() + "]");

                priceBorder.ChangeTextBlock.SetBinding(TextBlock.TextProperty, ChangeBinding);

                Binding ChangeColorBinding = new Binding();
                ChangeColorBinding.Source = priceViewModel;
                ChangeColorBinding.Path = new PropertyPath("ChangePriceColors[" + count.ToString() + "]");

                priceBorder.ChangeTextBlock.SetBinding(TextBlock.ForegroundProperty, ChangeColorBinding);

                Binding BidExchangeNameBinding = new Binding();
                BidExchangeNameBinding.Source = priceViewModel;
                BidExchangeNameBinding.Path = new PropertyPath("BidExchangeNames[" + count.ToString() + "]");

                priceBorder.BidExchangeNameTextBlock.SetBinding(TextBlock.TextProperty, BidExchangeNameBinding);

                Binding BidExchangeLogoBinding = new Binding();
                BidExchangeLogoBinding.Source = priceViewModel;
                BidExchangeLogoBinding.Path = new PropertyPath("BidExchangeLogos[" + count.ToString() + "]");

                priceBorder.BidExchangeImage.SetBinding(Image.SourceProperty, BidExchangeLogoBinding);

                Binding AskExchangeNameBinding = new Binding();
                AskExchangeNameBinding.Source = priceViewModel;
                AskExchangeNameBinding.Path = new PropertyPath("AskExchangeNames[" + count.ToString() + "]");

                priceBorder.AskExchangeNameTextBlock.SetBinding(TextBlock.TextProperty, AskExchangeNameBinding);

                Binding AskExchangeLogoBinding = new Binding();
                AskExchangeLogoBinding.Source = priceViewModel;
                AskExchangeLogoBinding.Path = new PropertyPath("AskExchangeLogos[" + count.ToString() + "]");

                priceBorder.AskExchangeImage.SetBinding(Image.SourceProperty, AskExchangeLogoBinding);

                PricesStackPanel.Children.Add(priceBorder.MainBorder);

                count++;
            }
        }
    }
}
