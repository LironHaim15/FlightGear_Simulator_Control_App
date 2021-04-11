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
    /// Interaction logic Dashboard.xaml . the veiw of Dashboard
    /// </summary>
    public partial class Dashboard : UserControl
    {
        //feild
        private DashboardViewModel vm;
        /// <summary>
        /// the constractur of the window
        /// </summary>
        public Dashboard()
        {
            InitializeComponent();
            vm = new DashboardViewModel(new DashboardModel());
            DataContext = vm;
        }
    }
}
