using System;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;

namespace WpfApp1.UICollection
{
    public class PriceBorder
    {

        private const int BorderHeight = 75;
        private const int BorderPadding = 5;

        private const int IconsWidth = 20;
        private const int LogoHeight = 60;

        private const int ExchangeLogoHeight = 45;

        private const int FontSize = 25;

        private const double AnimationDuration = 0.15;

        public Border MainBorder = new Border();
        public Grid MainGrid = new Grid();
        private Image LogoImage = new Image();
        private Image UpImage = new Image();
        private Image ShowImage = new Image();
        private Image DownImage = new Image();
        public Image BidExchangeImage = new Image();
        public Image AskExchangeImage = new Image();
        private BitmapImage LogoBitmapImage = new BitmapImage();
        private BitmapImage UpBitmapImage = new BitmapImage();
        private BitmapImage DownBitmapImage = new BitmapImage();
        private BitmapImage ShowMouseLeaveBitmapImage = new BitmapImage();
        private BitmapImage ShowMouseOverBitmapImage = new BitmapImage();
        private BitmapImage ShowMouseClickBitmapImage = new BitmapImage();
        public TextBlock CoinNameTextBlock = new TextBlock();
        public TextBlock BidTextBlock = new TextBlock();
        public TextBlock AskTextBlock = new TextBlock();
        public TextBlock FullCoinNameTextBlock = new TextBlock();
        public TextBlock ChangeTextBlock = new TextBlock();
        public TextBlock BidExchangeNameTextBlock = new TextBlock();
        public TextBlock AskExchangeNameTextBlock = new TextBlock();
        public TextBlock ExchangeTextBlock = new TextBlock();
        public Button ShowButton = new Button();

        private DoubleAnimation MainBorderAnimation = new DoubleAnimation();
        private DoubleAnimation ShowButtonRotateAnimation = new DoubleAnimation();

        private RotateTransform ShowButtonRotateTransform = new RotateTransform();

        public RowDefinition ExtendedRow = new RowDefinition();

        bool isOpened = false;

        #region ShowButtonClickSetup

        private void ShowButton_Click(object sender, MouseButtonEventArgs e)
        {
            ShowImage.Source = ShowMouseClickBitmapImage;

            if (isOpened)
            {
                MainBorderAnimation.From = MainBorder.ActualHeight;
                MainBorderAnimation.To = MainBorder.ActualHeight / 2;

                ShowButtonRotateAnimation.From = 180;
                ShowButtonRotateAnimation.To = 0;

                ExtendedRow.Height = new GridLength(0);
            }
            else
            {

                MainBorderAnimation.From = MainBorder.ActualHeight;
                MainBorderAnimation.To = MainBorder.ActualHeight * 2;

                ShowButtonRotateAnimation.From = 0;
                ShowButtonRotateAnimation.To = 180;
            }

            MainBorder.BeginAnimation(Border.HeightProperty, MainBorderAnimation);
            ShowButtonRotateTransform.BeginAnimation(RotateTransform.AngleProperty, ShowButtonRotateAnimation);

            isOpened = !isOpened;
        }

        private void ShowButton_MouseEnter(object sender, RoutedEventArgs e)
        {
            ShowImage.Source = ShowMouseOverBitmapImage;
        }

        private void ShowButton_MouseLeave(object sender, RoutedEventArgs e)
        {
            ShowImage.Source = ShowMouseLeaveBitmapImage;
        }

        private void ShowButton_MouseUnclick(object sender, MouseButtonEventArgs e)
        {
            if (ShowButton.IsMouseOver)
            {
                ShowImage.Source = ShowMouseOverBitmapImage;
            }
            else
            {
                ShowImage.Source = ShowMouseLeaveBitmapImage;
            }
        }

        #endregion

        private void MainBorderAnimation_Completed(object sender, EventArgs e)
        {
            if (isOpened)
            {
                ExtendedRow.Height = new GridLength(0.5, GridUnitType.Star);
            }
        }

        public PriceBorder(string coinName, string fullCoinName)
        {
            #region ApplicationResourcesSetup

            ResourceDictionary resourceDict = Application.LoadComponent(new Uri("Styles/UIResources.xaml", UriKind.Relative)) as ResourceDictionary;

            Application.Current.Resources.Clear();
            Application.Current.Resources.MergedDictionaries.Add(resourceDict);

            #endregion

            #region MainBorderSetup

            MainBorder.Width = Double.NaN;
            MainBorder.Height = BorderHeight;

            MainBorder.Background = new SolidColorBrush(Colors.White);
            MainBorder.VerticalAlignment = VerticalAlignment.Top;
            MainBorder.Padding = new Thickness(BorderPadding, BorderPadding, 0, BorderPadding);
            MainBorder.CornerRadius = new CornerRadius(5);
            MainBorder.Margin = new Thickness(20, 10, 20, 0);

            DropShadowEffect dropShadowEffect = new DropShadowEffect();
            dropShadowEffect.BlurRadius = 15;
            dropShadowEffect.Color = Colors.LightGray;
            dropShadowEffect.ShadowDepth = 0;

            MainBorder.Effect = dropShadowEffect;

            MainBorder.Child = MainGrid;

            #endregion
            #region MainGridColumnDefenition
            ColumnDefinition ColumnLogo = new ColumnDefinition { Width = new GridLength(0.1, GridUnitType.Star) };
            ColumnDefinition ColumnFullCoinName = new ColumnDefinition { Width = new GridLength(0.2, GridUnitType.Star) };
            ColumnDefinition ColumnCoinName = new ColumnDefinition { Width = new GridLength(0.1, GridUnitType.Star) };
            ColumnDefinition ColumnUp = new ColumnDefinition { Width = new GridLength(0.05, GridUnitType.Star) };
            ColumnDefinition ColumnBid = new ColumnDefinition { Width = new GridLength(0.15, GridUnitType.Star) };
            ColumnDefinition ColumnDown = new ColumnDefinition { Width = new GridLength(0.05, GridUnitType.Star) };
            ColumnDefinition ColumnAsk = new ColumnDefinition { Width = new GridLength(0.15, GridUnitType.Star) };
            ColumnDefinition ColumnChange = new ColumnDefinition { Width = new GridLength(0.1, GridUnitType.Star) };
            ColumnDefinition ColumnShowButton = new ColumnDefinition { Width = new GridLength(0.1, GridUnitType.Star) };

            MainGrid.ColumnDefinitions.Add(ColumnLogo);
            MainGrid.ColumnDefinitions.Add(ColumnFullCoinName);
            MainGrid.ColumnDefinitions.Add(ColumnCoinName);
            MainGrid.ColumnDefinitions.Add(ColumnUp);
            MainGrid.ColumnDefinitions.Add(ColumnBid);
            MainGrid.ColumnDefinitions.Add(ColumnDown);
            MainGrid.ColumnDefinitions.Add(ColumnAsk);
            MainGrid.ColumnDefinitions.Add(ColumnChange);
            MainGrid.ColumnDefinitions.Add(ColumnShowButton);

            #endregion
            #region MainGridRowDefenition

            RowDefinition BaseRow = new RowDefinition { Height = new GridLength(0.5, GridUnitType.Star) };
            ExtendedRow.Height = new GridLength(0, GridUnitType.Star);

            MainGrid.RowDefinitions.Add(BaseRow);
            MainGrid.RowDefinitions.Add(ExtendedRow);

            #endregion
            #region MainGridSetup

            MainGrid.VerticalAlignment = VerticalAlignment.Top;
            //MainGrid.ShowGridLines = true;

            #endregion
            #region LogoImageSetup

            LogoBitmapImage.BeginInit();

            LogoBitmapImage.UriSource = new System.Uri("/Images/Coins/" + coinName + ".png", UriKind.Relative);
            LogoBitmapImage.EndInit();

            LogoImage.Source = LogoBitmapImage;
            LogoImage.Height = LogoHeight;

            Grid.SetColumn(LogoImage, 0);
            Grid.SetRow(LogoImage, 0);
            MainGrid.Children.Add(LogoImage);

            #endregion
            #region FullCoinNameTextBlockSetup

            FullCoinNameTextBlock.VerticalAlignment = VerticalAlignment.Center;
            FullCoinNameTextBlock.Padding = new Thickness(10);
            FullCoinNameTextBlock.Text = fullCoinName;
            FullCoinNameTextBlock.FontSize = FontSize;
            FullCoinNameTextBlock.FontFamily = new FontFamily("Segoe UI Semibold");

            Grid.SetColumn(FullCoinNameTextBlock, 1);
            Grid.SetRow(FullCoinNameTextBlock, 0);
            MainGrid.Children.Add(FullCoinNameTextBlock);

            #endregion
            #region CoinNameTextBlockSetup

            CoinNameTextBlock.VerticalAlignment = VerticalAlignment.Center;
            CoinNameTextBlock.Padding = new Thickness(10);
            CoinNameTextBlock.Text = coinName;
            CoinNameTextBlock.FontSize = FontSize;
            CoinNameTextBlock.FontFamily = new FontFamily("Segoe UI Semibold");
            CoinNameTextBlock.Foreground = Brushes.Gray;

            Grid.SetColumn(CoinNameTextBlock, 2);
            Grid.SetRow(CoinNameTextBlock, 0);
            MainGrid.Children.Add(CoinNameTextBlock);

            #endregion
            #region UpImageSetup

            UpBitmapImage.BeginInit();
            UpBitmapImage.UriSource = new System.Uri("/Images/Icons/up.png", UriKind.Relative);
            UpBitmapImage.EndInit();

            UpImage.Source = UpBitmapImage;
            UpImage.Width = IconsWidth;
            UpImage.VerticalAlignment = VerticalAlignment.Center;
            UpImage.HorizontalAlignment = HorizontalAlignment.Center;

            Grid.SetColumn(UpImage, 3);
            Grid.SetRow(UpImage, 0);
            MainGrid.Children.Add(UpImage);

            #endregion
            #region BidTextBlockSetup

            BidTextBlock.VerticalAlignment = VerticalAlignment.Center;
            BidTextBlock.HorizontalAlignment = HorizontalAlignment.Left;
            BidTextBlock.Padding = new Thickness(10);
            BidTextBlock.FontSize = 30;
            BidTextBlock.FontFamily = new FontFamily("Segoe UI Light");
            BidTextBlock.Text = "???";

            Grid.SetColumn(BidTextBlock, 4);
            Grid.SetRow(BidTextBlock, 0);
            MainGrid.Children.Add(BidTextBlock);

            #endregion
            #region DownImageSetup

            DownBitmapImage.BeginInit();
            DownBitmapImage.UriSource = new Uri("/Images/Icons/down.png", UriKind.Relative);
            DownBitmapImage.EndInit();

            DownImage.Source = DownBitmapImage;
            DownImage.Width = IconsWidth;
            DownImage.VerticalAlignment = VerticalAlignment.Center;
            DownImage.HorizontalAlignment = HorizontalAlignment.Center;

            Grid.SetColumn(DownImage, 5);
            Grid.SetRow(DownImage, 0);
            MainGrid.Children.Add(DownImage);

            #endregion
            #region AskTextBlockSetup

            AskTextBlock.VerticalAlignment = VerticalAlignment.Center;
            AskTextBlock.HorizontalAlignment = HorizontalAlignment.Left;
            AskTextBlock.Padding = new Thickness(10);
            AskTextBlock.FontSize = 30;
            AskTextBlock.FontFamily = new FontFamily("Segoe UI Light");
            AskTextBlock.Text = "???";

            Grid.SetColumn(AskTextBlock, 6);
            Grid.SetRow(AskTextBlock, 0);
            MainGrid.Children.Add(AskTextBlock);

            #endregion
            #region ChangeTextBlockSetup

            ChangeTextBlock.VerticalAlignment = VerticalAlignment.Center;
            ChangeTextBlock.HorizontalAlignment = HorizontalAlignment.Center;
            ChangeTextBlock.FontSize = 30;
            ChangeTextBlock.FontFamily = new FontFamily("Segoe UI SemiLight");
            ChangeTextBlock.Text = "???";

            Grid.SetColumn(ChangeTextBlock, 7);
            Grid.SetRow(ChangeTextBlock, 0);
            MainGrid.Children.Add(ChangeTextBlock);

            #endregion
            #region ShowButtonSetup

            ShowButton.VerticalAlignment = VerticalAlignment.Center;
            ShowButton.HorizontalAlignment = HorizontalAlignment.Center;
            ShowButton.Background = Brushes.Transparent;
            ShowButton.BorderBrush = Brushes.Transparent;
            ShowButton.Width = IconsWidth * 2;

            ShowButton.RenderTransform = ShowButtonRotateTransform;
            ShowButton.RenderTransformOrigin = new Point(0.5, 0.5);

            ShowButton.Style = (Style)Application.Current.TryFindResource("ShowButton");

            ShowButton.MouseEnter += new MouseEventHandler(ShowButton_MouseEnter);
            ShowImage.MouseLeave += new MouseEventHandler(ShowButton_MouseLeave);
            ShowButton.PreviewMouseLeftButtonDown += new MouseButtonEventHandler(ShowButton_Click);
            ShowButton.PreviewMouseLeftButtonUp += new MouseButtonEventHandler(ShowButton_MouseUnclick);
        
            ShowMouseLeaveBitmapImage.BeginInit();
            ShowMouseLeaveBitmapImage.UriSource = new Uri("/Images/Icons/arrow_mouseleave.png", UriKind.Relative);
            ShowMouseLeaveBitmapImage.EndInit();

            ShowMouseOverBitmapImage.BeginInit();
            ShowMouseOverBitmapImage.UriSource = new Uri("/Images/Icons/arrow_mouseover.png", UriKind.Relative);
            ShowMouseOverBitmapImage.EndInit();

            ShowMouseClickBitmapImage.BeginInit();
            ShowMouseClickBitmapImage.UriSource = new Uri("/Images/Icons/arrow_mouseclick.png", UriKind.Relative);
            ShowMouseClickBitmapImage.EndInit();

            ShowImage.Source = ShowMouseLeaveBitmapImage;
            ShowImage.Width = IconsWidth * 1.75;
            ShowImage.VerticalAlignment = VerticalAlignment.Center;
            ShowImage.HorizontalAlignment = HorizontalAlignment.Left;

            ShowButton.Content = ShowImage;

            Grid.SetColumn(ShowButton, 8);
            Grid.SetRow(ShowButton, 0);
            MainGrid.Children.Add(ShowButton);

            #endregion
            #region ExchangeTextBlockSetup

            ExchangeTextBlock.VerticalAlignment = VerticalAlignment.Bottom;
            ExchangeTextBlock.Padding = new Thickness(10);
            ExchangeTextBlock.FontSize = FontSize;
            ExchangeTextBlock.FontFamily = new FontFamily("Segoe UI SemiBold");
            ExchangeTextBlock.Text = "Exchanges: ";

            Grid.SetColumn(ExchangeTextBlock, 1);
            Grid.SetRow(ExchangeTextBlock, 1);
            MainGrid.Children.Add(ExchangeTextBlock);

            #endregion
            #region BidExchangeImageSetup

            BidExchangeImage.Height = ExchangeLogoHeight;

            Grid.SetColumn(BidExchangeImage, 3);
            Grid.SetRow(BidExchangeImage, 1);

            MainGrid.Children.Add(BidExchangeImage);

            #endregion
            #region BidExchangeNameTextBlockSetup

            BidExchangeNameTextBlock.VerticalAlignment = VerticalAlignment.Bottom;
            BidExchangeNameTextBlock.HorizontalAlignment = HorizontalAlignment.Left;
            BidExchangeNameTextBlock.Padding = new Thickness(10);
            BidExchangeNameTextBlock.FontSize = FontSize;
            BidExchangeNameTextBlock.FontFamily = new FontFamily("Segoe UI SemiBold");
            BidExchangeNameTextBlock.Foreground = Brushes.Gray;
            BidExchangeNameTextBlock.Text = "???";

            Grid.SetColumn(BidExchangeNameTextBlock, 4);
            Grid.SetRow(BidExchangeNameTextBlock, 1);
            MainGrid.Children.Add(BidExchangeNameTextBlock);

            #endregion
            #region AskExchangeImageSetup

            AskExchangeImage.Height = ExchangeLogoHeight;

            Grid.SetColumn(AskExchangeImage, 5);
            Grid.SetRow(AskExchangeImage, 1);

            MainGrid.Children.Add(AskExchangeImage);

            #endregion
            #region AskExchangeNameTextBlockSetup

            AskExchangeNameTextBlock.VerticalAlignment = VerticalAlignment.Bottom;
            AskExchangeNameTextBlock.HorizontalAlignment = HorizontalAlignment.Left;
            AskExchangeNameTextBlock.Padding = new Thickness(10);
            AskExchangeNameTextBlock.FontSize = FontSize;
            AskExchangeNameTextBlock.FontFamily = new FontFamily("Segoe UI SemiBold");
            AskExchangeNameTextBlock.Foreground = Brushes.Gray;
            AskExchangeNameTextBlock.Text = "???";

            Grid.SetRow(AskExchangeNameTextBlock, 1);
            Grid.SetColumn(AskExchangeNameTextBlock, 6);
            MainGrid.Children.Add(AskExchangeNameTextBlock);

            #endregion

            #region MainBorderAnimationSetup

            MainBorderAnimation.Duration = TimeSpan.FromSeconds(AnimationDuration);
            MainBorderAnimation.FillBehavior = FillBehavior.HoldEnd;

            MainBorderAnimation.Completed += MainBorderAnimation_Completed;

            #endregion
            #region ShowButtonRotateAnimationSetup

            ShowButtonRotateAnimation.Duration = TimeSpan.FromSeconds(AnimationDuration);
            ShowButtonRotateAnimation.FillBehavior = FillBehavior.HoldEnd;

            #endregion
        }
    }
}
