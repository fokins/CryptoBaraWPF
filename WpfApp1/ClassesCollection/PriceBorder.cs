using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;

namespace WpfApp1.ClassesCollection
{
    public class PriceBorder
    {

        const int BorderWidth = 850;
        const int BorderHeight = 75;

        const int BorderPadding = 5;

        const int IconsWidth = 20;
        const int LogoHeight = 60;

        public Border MainBorder = new Border();
        public Grid MainGrid = new Grid();
        private Image LogoImage = new Image();
        private Image UpImage = new Image();
        private Image DownImage = new Image();
        private BitmapImage LogoBitmapImage;
        private BitmapImage UpBitmapImage;
        private BitmapImage DownBitmapImage;
        public TextBlock CoinNameTextBlock = new TextBlock();
        public TextBlock BidTextBlock = new TextBlock();
        public TextBlock AskTextBlock = new TextBlock();


        public PriceBorder(string coinName)
        {
            #region MainBorderSetup

            MainBorder.Width = BorderWidth;
            MainBorder.Height = BorderHeight;

            MainBorder.Background = new SolidColorBrush(Colors.White);
            MainBorder.VerticalAlignment = VerticalAlignment.Top;
            MainBorder.Padding = new Thickness(BorderPadding);
            MainBorder.CornerRadius = new CornerRadius(5);
            MainBorder.Margin = new Thickness(5);

            DropShadowEffect dropShadowEffect = new DropShadowEffect();
            dropShadowEffect.BlurRadius = 20;
            dropShadowEffect.Color = Colors.LightGray;
            dropShadowEffect.ShadowDepth = 1;

            MainBorder.Effect = dropShadowEffect;

            MainBorder.Child = MainGrid;

            #endregion
            #region MainGridColumnDefenition
            ColumnDefinition ColumnLogo = new ColumnDefinition { Width = new GridLength((BorderWidth - 2 * BorderPadding) * 0.1) };
            ColumnDefinition ColumnCoinName = new ColumnDefinition { Width = new GridLength((BorderWidth - 2 * BorderPadding) * 0.15) };
            ColumnDefinition ColumnUp = new ColumnDefinition { Width = new GridLength((BorderWidth - 2 * BorderPadding) * 0.1) };
            ColumnDefinition ColumnBid = new ColumnDefinition { Width = new GridLength((BorderWidth - 2 * BorderPadding) * 0.3) };
            ColumnDefinition ColumnDown = new ColumnDefinition { Width = new GridLength((BorderWidth - 2 * BorderPadding) * 0.05) };
            ColumnDefinition ColumnAsk = new ColumnDefinition { Width = new GridLength((BorderWidth - 2 * BorderPadding) * 0.3) };

            MainGrid.ColumnDefinitions.Add(ColumnLogo);
            MainGrid.ColumnDefinitions.Add(ColumnCoinName);
            MainGrid.ColumnDefinitions.Add(ColumnUp);
            MainGrid.ColumnDefinitions.Add(ColumnBid);
            MainGrid.ColumnDefinitions.Add(ColumnDown);
            MainGrid.ColumnDefinitions.Add(ColumnAsk);

            #endregion
            #region ImageSetup

            LogoBitmapImage = new BitmapImage();
            LogoBitmapImage.BeginInit();

            LogoBitmapImage.UriSource = new System.Uri("/Images/Coins/" + coinName + ".png", UriKind.Relative);
            LogoBitmapImage.EndInit();

            LogoImage.Source = LogoBitmapImage;
            LogoImage.Height = LogoHeight;

            Grid.SetColumn(LogoImage, 0);
            MainGrid.Children.Add(LogoImage);

            #endregion
            #region CoinNameTextBlockSetup

            CoinNameTextBlock.VerticalAlignment = VerticalAlignment.Center;
            CoinNameTextBlock.Padding = new Thickness(10);
            CoinNameTextBlock.Text = coinName;
            CoinNameTextBlock.FontSize = 30;
            CoinNameTextBlock.FontFamily = new FontFamily("Cascadia Mono SemiLight");

            Grid.SetColumn(CoinNameTextBlock, 1);
            MainGrid.Children.Add(CoinNameTextBlock);

            #endregion
            #region UpImageSetup

            UpBitmapImage = new BitmapImage();
            UpBitmapImage.BeginInit();
            UpBitmapImage.UriSource = new System.Uri("/Images/Icons/up.png", UriKind.Relative);
            UpBitmapImage.EndInit();

            UpImage.Source = UpBitmapImage;
            UpImage.Width = IconsWidth;
            UpImage.VerticalAlignment = VerticalAlignment.Center;
            UpImage.HorizontalAlignment = HorizontalAlignment.Right;

            Grid.SetColumn(UpImage, 2);
            MainGrid.Children.Add(UpImage);

            #endregion
            #region BidTextBlockSetup

            BidTextBlock.VerticalAlignment = VerticalAlignment.Center;
            BidTextBlock.Padding = new Thickness(10);
            BidTextBlock.FontSize = 30;
            BidTextBlock.FontFamily = new FontFamily("Segoe UI Light");
            BidTextBlock.Text = "???";

            Grid.SetColumn(BidTextBlock, 3);
            MainGrid.Children.Add(BidTextBlock);

            #endregion
            #region DownImageSetup

            DownBitmapImage = new BitmapImage();
            DownBitmapImage.BeginInit();
            DownBitmapImage.UriSource = new System.Uri("/Images/Icons/down.png", UriKind.Relative);
            DownBitmapImage.EndInit();

            DownImage.Source = DownBitmapImage;
            DownImage.Width = IconsWidth;
            DownImage.VerticalAlignment = VerticalAlignment.Center;
            DownImage.HorizontalAlignment = HorizontalAlignment.Right;

            Grid.SetColumn(DownImage, 4);
            MainGrid.Children.Add(DownImage);

            #endregion
            #region AskTextBlockSetup

            AskTextBlock.VerticalAlignment = VerticalAlignment.Center;
            AskTextBlock.Padding = new Thickness(10);
            AskTextBlock.FontSize = 30;
            AskTextBlock.FontFamily = new FontFamily("Segoe UI Light");
            AskTextBlock.Text = "???";

            Grid.SetColumn(AskTextBlock, 5);
            MainGrid.Children.Add(AskTextBlock);

            #endregion
        }
    }
}
