using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;

namespace WpfApp1.ClassesCollection
{
    class ChartCheckBox
    {

        const int CheckBoxHeight = 30;
        const int MainBorderWidth = 14;
        const int MainBorderHeight = 13;
        const int ChartBorderWidth = 11;
        const int ChartBorderHeight = 1;

        public CheckBox checkBox;
        public Border MainBorder;
        private Border ChartBorder;

        public ChartCheckBox(string content, SolidColorBrush chartColor)
        {
            #region CheckBoxSetup

            checkBox = new CheckBox();
            checkBox.IsThreeState = false;
            checkBox.Height = CheckBoxHeight;
            checkBox.Content = content;
            checkBox.FontFamily = new FontFamily("Segoe UI Light");

            #endregion
            #region MainBorderSetup

            MainBorder = new Border();
            MainBorder.Height = MainBorderHeight;
            MainBorder.Width = MainBorderWidth;
            MainBorder.Background = new SolidColorBrush(Colors.Transparent);
            MainBorder.BorderThickness = new Thickness(1);
            MainBorder.BorderBrush = new SolidColorBrush(Colors.DimGray);
            MainBorder.HorizontalAlignment = HorizontalAlignment.Left;
            MainBorder.Margin = new Thickness(0, 17, 0, 0);

            #endregion
            #region ChartBorderSetup

            ChartBorder = new Border();
            ChartBorder.Width = ChartBorderWidth;
            ChartBorder.Height = ChartBorderHeight;
            ChartBorder.VerticalAlignment = VerticalAlignment.Center;
            ChartBorder.HorizontalAlignment = HorizontalAlignment.Center;
            ChartBorder.Background = chartColor;

            MainBorder.Child = ChartBorder;

            #endregion
        }
    }
}
