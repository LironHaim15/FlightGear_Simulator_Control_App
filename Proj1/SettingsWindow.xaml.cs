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
    /// Interaction logic for SettingsWindow.xaml
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
        /// <summary>
        /// called upon click on the 'continue' button and check that every path is checked out.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Continue_Click(object sender, RoutedEventArgs e)
        {
            if (vm.check())
            {
                this.Close();
            }
        }
        /// <summary>
        /// called upon click on the browse button for the learning csv path.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NormalBrowse_Click(object sender, RoutedEventArgs e)
        {
            vm.browse("CsvNormalPath");
        }

        /// <summary>
        /// called upon click on the browse button for the test csv path.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TestBrowse_Click(object sender, RoutedEventArgs e)
        {
            vm.browse("CsvTestPath");
        }

        /// <summary>
        /// called upon click on the browse button for the FlightGear directory path.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FGpathBrowse_Click(object sender, RoutedEventArgs e)
        {
            vm.browse("FlightGearPath");
        }
    }
}
