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

namespace WpfApp1.UICollection
{
    class BalanceBorder
    {
        const int BorderHeight = 75;

        const int BorderPadding = 5;

        const int IconsWidth = 25;
        const int LogoHeight = 60;

        public Border MainBorder = new Border();
        public Grid MainGrid = new Grid();
        private Image LogoImage = new Image();
        private Image FreeImage = new Image();
        private Image LockedImage = new Image();
        private BitmapImage LogoBitmapImage;
        private BitmapImage FreeBitmapImage;
        private BitmapImage LockedBitmapImage;
        public TextBlock CoinNameTextBlock = new TextBlock();
        public TextBlock FreeTextBlock = new TextBlock();
        public TextBlock LockedTextBlock = new TextBlock();


        public BalanceBorder(string coinName)
        {
            #region MainBorderSetup

            MainBorder.Width = Double.NaN;
            MainBorder.Height = BorderHeight;

            MainBorder.Background = new SolidColorBrush(Colors.White);
            MainBorder.VerticalAlignment = VerticalAlignment.Top;
            MainBorder.Padding = new Thickness(BorderPadding);
            MainBorder.CornerRadius = new CornerRadius(5);
            MainBorder.Margin = new Thickness(20, 10, 20, 0);

            DropShadowEffect dropShadowEffect = new DropShadowEffect();
            dropShadowEffect.BlurRadius = 20;
            dropShadowEffect.Color = Colors.LightGray;
            dropShadowEffect.ShadowDepth = 1;

            MainBorder.Effect = dropShadowEffect;

            MainBorder.Child = MainGrid;

            #endregion
            #region MainGridColumnDefenition
            ColumnDefinition ColumnLogo = new ColumnDefinition { Width = new GridLength(0.15, GridUnitType.Star) };
            ColumnDefinition ColumnCoinName = new ColumnDefinition { Width = new GridLength(0.25, GridUnitType.Star) };
            ColumnDefinition ColumnFreeIcon = new ColumnDefinition { Width = new GridLength(0.05, GridUnitType.Star) };
            ColumnDefinition ColumnFree = new ColumnDefinition { Width = new GridLength(0.25, GridUnitType.Star) };
            ColumnDefinition ColumnLockedIcon = new ColumnDefinition { Width = new GridLength(0.05, GridUnitType.Star) };
            ColumnDefinition ColumnLocked = new ColumnDefinition { Width = new GridLength(0.25, GridUnitType.Star) };

            MainGrid.ColumnDefinitions.Add(ColumnLogo);
            MainGrid.ColumnDefinitions.Add(ColumnCoinName);
            MainGrid.ColumnDefinitions.Add(ColumnFreeIcon);
            MainGrid.ColumnDefinitions.Add(ColumnFree);
            MainGrid.ColumnDefinitions.Add(ColumnLockedIcon);
            MainGrid.ColumnDefinitions.Add(ColumnLocked);

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
            CoinNameTextBlock.FontFamily = new FontFamily("Segoe UI Semibold");

            Grid.SetColumn(CoinNameTextBlock, 1);
            MainGrid.Children.Add(CoinNameTextBlock);

            #endregion
            #region UpImageSetup

            FreeBitmapImage = new BitmapImage();
            FreeBitmapImage.BeginInit();
            FreeBitmapImage.UriSource = new System.Uri("/Images/Icons/free.png", UriKind.Relative);
            FreeBitmapImage.EndInit();

            FreeImage.Source = FreeBitmapImage;
            FreeImage.Width = IconsWidth;
            FreeImage.VerticalAlignment = VerticalAlignment.Center;
            FreeImage.HorizontalAlignment = HorizontalAlignment.Right;

            Grid.SetColumn(FreeImage, 2);
            MainGrid.Children.Add(FreeImage);

            #endregion
            #region BidTextBlockSetup

            FreeTextBlock.VerticalAlignment = VerticalAlignment.Center;
            FreeTextBlock.Padding = new Thickness(10);
            FreeTextBlock.FontSize = 30;
            FreeTextBlock.FontFamily = new FontFamily("Segoe UI Light");
            FreeTextBlock.Text = "???";

            Grid.SetColumn(FreeTextBlock, 3);
            MainGrid.Children.Add(FreeTextBlock);

            #endregion
            #region DownImageSetup

            LockedBitmapImage = new BitmapImage();
            LockedBitmapImage.BeginInit();
            LockedBitmapImage.UriSource = new System.Uri("/Images/Icons/locked.png", UriKind.Relative);
            LockedBitmapImage.EndInit();

            LockedImage.Source = LockedBitmapImage;
            LockedImage.Width = IconsWidth;
            LockedImage.VerticalAlignment = VerticalAlignment.Center;
            LockedImage.HorizontalAlignment = HorizontalAlignment.Right;

            Grid.SetColumn(LockedImage, 4);
            MainGrid.Children.Add(LockedImage);

            #endregion
            #region AskTextBlockSetup

            LockedTextBlock.VerticalAlignment = VerticalAlignment.Center;
            LockedTextBlock.Padding = new Thickness(10);
            LockedTextBlock.FontSize = 30;
            LockedTextBlock.FontFamily = new FontFamily("Segoe UI Light");
            LockedTextBlock.Text = "???";

            Grid.SetColumn(LockedTextBlock, 5);
            MainGrid.Children.Add(LockedTextBlock);

            #endregion
        }
    }
}
