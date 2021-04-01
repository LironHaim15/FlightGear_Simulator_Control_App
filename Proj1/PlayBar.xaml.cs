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
    /// Interaction logic for PlayBar.xaml
    /// </summary>
    public partial class PlayBar : UserControl
    {
        PlayBarViewModel vm;
        public PlayBar()
        {
            InitializeComponent();
            vm = new PlayBarViewModel(new PlayBarModel());
            DataContext = vm;
        }

        private void SkipBack_Click(object sender, RoutedEventArgs e) { vm.skipBackward(); }

        private void SkipForward_Click(object sender, RoutedEventArgs e)
        {
            vm.skipForward();
        }

        private void Play_Click(object sender, RoutedEventArgs e)
        {
            vm.play();
        }

        private void Pause_Click(object sender, RoutedEventArgs e)
        {
            vm.pause();
        }

        private void Stop_Click(object sender, RoutedEventArgs e)
        {
            vm.stop();
        }

        private void SkipEnd_Click(object sender, RoutedEventArgs e)
        {
            vm.skipEnd();
        }

        private void SkipStart_Click(object sender, RoutedEventArgs e)
        {
            vm.skipStart();
        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            vm.setCurrentLine((int)PlaySlider.Value);
        }

        private void Apply_Click(object sender, RoutedEventArgs e)
        {
            vm.setPlaySpeed(PlaySpeed.Text);
        }
    }
}
