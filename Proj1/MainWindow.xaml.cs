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
using System.Windows.Shapes;
using Proj1.VeiwModels;
using Proj1.Models;

namespace Proj1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MainViewModel vm;
        public MainWindow()
        {
            InitializeComponent();
            vm = new MainViewModel();
            DataContext = vm;
        }

        private void Settings_Click(object sender, RoutedEventArgs e)
        {
            SettingsWindow swindow = new SettingsWindow();
            swindow.Show();
        }

        private void Connect_Click(object sender, RoutedEventArgs e)
        {
            ConnectWindow cwindow = new ConnectWindow();
            cwindow.Show();
        }

        private void Disconnect_Click(object sender, RoutedEventArgs e)
        {
            vm.disconnect();
        }

        private void Load_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
