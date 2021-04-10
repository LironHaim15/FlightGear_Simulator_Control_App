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
using Proj1.ViewModels;
using Proj1.Models;

namespace Proj1
{
    /// <summary>
    /// Interaction logic forConnectWindow.xaml . the veiw of connect with the flight geer
    /// </summary>
    public partial class ConnectWindow : Window
    {
        // feild 
        ConnectViewModel vm;
        /// <summary>
        /// the constractur of the window
        /// </summary>
        public ConnectWindow()
        {
            InitializeComponent();
            vm = new ConnectViewModel(new ConnectModel());
            DataContext = vm;
        }
        /// <summary>
        /// the click to connect the fly
        /// </summary>
        private void StartConnection_Click(object sender, RoutedEventArgs e)
        {
            // if the input right need to close the window
            if (vm.check(ipTextBox.Text, portTextBox.Text))
            {
                this.Close();
            }
        }

    }
}
