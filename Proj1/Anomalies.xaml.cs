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
using OxyPlot;
using OxyPlot.Wpf;

namespace Proj1
{
    /// <summary>
    /// Interaction logic for Anomalies.xaml . the veiw of grph part its implemnts by mvvm .
    /// </summary>
    public partial class Anomalies : UserControl
    {
        // veiw model feild
        private AnomaliesViewModel vm;

        /// <summary>
        /// the constractur of the window
        /// </summary>
        public Anomalies()
        {
            InitializeComponent();
            // new view model
            vm = new AnomaliesViewModel(new AnomaliesModel());
            //binds
            DataContext = vm;
        }
        /// <summary>
        /// when the user chose from the featuresBox name.
        /// </summary>
        private void FeaturesListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            vm.update(FeaturesListBox.SelectedItem.ToString());
        }

        /// <summary>
        ///when the user chose from the  AnomaliesList the anomalie that he want to explore.
        /// </summary>
        private void AnomaliesListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (vm.VM_AnomaliesList.Count != 0)
                vm.updateAnomaly(AnomaliesListBox.SelectedItem.ToString());
        }

    }
}
