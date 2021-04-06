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
    /// Interaction logic for Anomalies.xaml
    /// </summary>
    public partial class Anomalies : UserControl
    {
        private AnomaliesViewModel vm;
        public Anomalies()
        {
            InitializeComponent();
            vm = new AnomaliesViewModel(new AnomaliesModel());
            DataContext = vm;

        }
        private void FeaturesListBox_Click(object sender, RoutedEventArgs e)
        {
            vm.update(FeaturesListBox.SelectedItem.ToString());
            Console.WriteLine(FeaturesListBox.SelectedItem.ToString());
        }

        private void FeaturesListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            vm.update(FeaturesListBox.SelectedItem.ToString());
        }

        private void AnomaliesListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (vm.VM_AnomaliesList.Count != 0)
                vm.updateAnomaly(AnomaliesListBox.SelectedItem.ToString());
        }
    }
}
