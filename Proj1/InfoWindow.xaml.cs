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
    /// Interaction logic InfoWindow.xaml . the veiw of InfoWindow to user
    /// </summary>
    public partial class InfoWindow : Window
    {
        //feild
        private InfoViewModel vm;
        /// <summary>
        /// the constractur of the  InfoWindow
        /// </summary>
        public InfoWindow()
        {
            InitializeComponent();
            vm = new InfoViewModel(new InfoModel());
            DataContext = vm;
        }
        /// <summary>
        /// the button that close the informtion
        /// </summary>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
