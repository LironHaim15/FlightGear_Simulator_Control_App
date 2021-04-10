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
    /// Interaction logic for MainWindow.xaml . the veiw of MainWindow of the project that inside have all
    /// the veiwbox and all the veiw part .
    /// </summary>
    public partial class MainWindow : Window
    {
        //feilds
        MainViewModel vm;
        //window to connect to flight geer
        ConnectWindow cwindow;
        // window to load dll algo
        LoadDLLWindow lwindow;
        // window to setting all the files and the place of the flight geer
        SettingsWindow swindow;
        // window with informtion to user about the progrem
        InfoWindow iwindow;
        /// <summary>
        /// the constractur of the  MainWindow
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            cwindow = null;
            lwindow = null;
            swindow = null;
            iwindow = null;
            vm = new MainViewModel();
            DataContext = vm;
        }
        /// <summary>
        /// setting click open new window to setting
        /// </summary>
        private void Settings_Click(object sender, RoutedEventArgs e)
        {
            // open window the first time or  if the window hidden
            if (swindow == null || !swindow.IsVisible)
            {
                swindow = new SettingsWindow();
                swindow.Show();
            }
            else
                swindow.Focus();
        }
        /// <summary>
        ///Connect click open new window to connect
        /// </summary>
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
        /// <summary>
        ///dissconnect for flight geer
        /// </summary>
        private void Disconnect_Click(object sender, RoutedEventArgs e)
        {
            vm.disconnect();
        }

        /// <summary>
        ///load dll algo
        /// </summary>
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

        /// <summary>
        ///close window
        /// </summary>
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            vm.disconnect();
            //close the another windows
            if (lwindow != null || (lwindow != null && lwindow.IsVisible))
                lwindow.Close();
            if (swindow != null || (swindow != null && swindow.IsVisible))
                swindow.Close();
            if (cwindow != null || (cwindow != null && cwindow.IsVisible))
                cwindow.Close();
            if (iwindow != null || (iwindow != null && iwindow.IsVisible))
                iwindow.Close();
        }

        /// <summary>
        ///informtion click open new window with informtion to user.
        /// </summary>
        private void User_Instructions_Click(object sender, RoutedEventArgs e)
        {
            if (iwindow == null || !iwindow.IsVisible)
            {
                iwindow = new InfoWindow();
                iwindow.Show();
            }
            else
                iwindow.Focus();
        }
    }
}
