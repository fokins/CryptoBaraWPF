using System;
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
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using System.Threading;
using System.Text.Json;
using System.Net;
using WpfApp1.ClassesCollection;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для ChartPage.xaml
    /// </summary>
    public partial class ChartPage : Page
    {

        public Coins coins = new Coins();

        public ChartPage()
        {
            InitializeComponent();

            ViewModels.ChartViewModel chartViewModel = new ViewModels.ChartViewModel();

            DataContext = chartViewModel;

            int count = 0;

            foreach (var content in coins.Normalized)
            {
                ChartCheckBox chartCheckBox = new ChartCheckBox(content, coins.ChartColors[count]);

                Binding checkBinding = new Binding();
                checkBinding.Source = chartViewModel;
                checkBinding.Path = new PropertyPath("Checked[" + count.ToString() + "]");

                chartCheckBox.checkBox.SetBinding(CheckBox.IsCheckedProperty, checkBinding);

                Binding commandBinding = new Binding();
                commandBinding.Source = chartViewModel;
                commandBinding.Path = new PropertyPath("UpdateCommand");

                chartCheckBox.checkBox.SetBinding(CheckBox.CommandProperty, commandBinding);


                CheckBoxStackPanel.Children.Add(chartCheckBox.checkBox);
                ChartColorsStackPanel.Children.Add(chartCheckBox.MainBorder);

                count++;
            }

        }
    }
}
