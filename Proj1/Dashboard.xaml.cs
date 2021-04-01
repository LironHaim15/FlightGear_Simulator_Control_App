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
using Proj1.ViewModels;
using Proj1.Models;

namespace Proj1
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class Dashboard : UserControl
    {
        private DashboardViewModel vm;
        public Dashboard()
        {
            InitializeComponent();
            vm = new DashboardViewModel(new DashboardModel());
            DataContext = vm;
        }
    }
}
