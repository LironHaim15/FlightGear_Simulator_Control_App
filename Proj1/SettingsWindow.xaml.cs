using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Diagnostics;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Proj1.VeiwModels;

namespace Proj1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class SettingsWindow : Window
    {
        private SettingsViewModel vm;
        
        public SettingsWindow()
        {
            InitializeComponent();
            vm = new SettingsViewModel(new SettingsModel());
            DataContext = vm;
        }
        private void Continue_Click(object sender, RoutedEventArgs e)
        {
            if (vm.check())
            {
                this.Close();
            }
        }
        private void NormalBrowse_Click(object sender, RoutedEventArgs e)
        {
            vm.browse("CsvNormalPath");
        }

        private void TestBrowse_Click(object sender, RoutedEventArgs e)
        {
            vm.browse("CsvTestPath");
        }

        private void XmlBrowse_Click(object sender, RoutedEventArgs e)
        {
            vm.browse("XmlPath");
        }

        private void FGpathBrowse_Click(object sender, RoutedEventArgs e)
        {
            vm.browse("FlightGearPath");
        }
    }
}
