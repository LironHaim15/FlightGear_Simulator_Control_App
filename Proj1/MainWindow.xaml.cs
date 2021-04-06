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
        ConnectWindow cwindow;
        LoadDLLWindow lwindow;
        SettingsWindow swindow;
        public MainWindow()
        {
            InitializeComponent();
            cwindow = null;
            lwindow = null;
            swindow = null;
            vm = new MainViewModel();
            DataContext = vm;
        }

        private void Settings_Click(object sender, RoutedEventArgs e)
        {
            if (swindow == null || !swindow.IsVisible)
            {
                swindow = new SettingsWindow();
                swindow.Show();
            }
            else
                swindow.Focus();
        }

        private void Connect_Click(object sender, RoutedEventArgs e)
        {
            if (cwindow == null || !cwindow.IsVisible)
            {
                cwindow = new ConnectWindow();
                cwindow.Show();
            }
            else
                cwindow.Focus();
        }

        private void Disconnect_Click(object sender, RoutedEventArgs e)
        {
            vm.disconnect();
        }

        private void Load_Click(object sender, RoutedEventArgs e)
        {
            if (lwindow == null || !lwindow.IsVisible)
            {
                lwindow = new LoadDLLWindow();
                lwindow.Show();
            }
            else
                lwindow.Focus();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            vm.disconnect();
            if (lwindow != null || (lwindow != null && lwindow.IsVisible))
                lwindow.Close();
            if (swindow != null || (swindow != null && swindow.IsVisible))
                swindow.Close();
            if (cwindow != null || (cwindow != null && cwindow.IsVisible))
                cwindow.Close();
        }
    }
}
