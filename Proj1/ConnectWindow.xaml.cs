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
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class ConnectWindow : Window
    {
        ConnectViewModel vm;
        public ConnectWindow()
        {
            InitializeComponent();
            vm = new ConnectViewModel(new ConnectModel());
            DataContext = vm;
        }
        private void StartConnection_Click(object sender, RoutedEventArgs e)
        {
            if (vm.check())
            {
                this.Close();
            }
        }

    }
}
