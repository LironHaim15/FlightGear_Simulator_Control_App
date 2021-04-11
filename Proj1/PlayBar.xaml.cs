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
    /// Interaction logic for PlayBar . the veiw of PlayBar user story 2
    /// </summary>
    public partial class PlayBar : UserControl
    {
        //feilds
        PlayBarViewModel vm;
        /// <summary>
        /// the constractur of the PlayBar
        /// </summary>
        public PlayBar()
        {
            InitializeComponent();
            vm = new PlayBarViewModel(new PlayBarModel());
            DataContext = vm;
        }
        /// <summary>
        /// the  click for skip back video of fly
        /// </summary>
        private void SkipBack_Click(object sender, RoutedEventArgs e) { vm.skipBackward(); }

        /// <summary>
        /// the  click for skip forward video of fly
        /// </summary>
        private void SkipForward_Click(object sender, RoutedEventArgs e)
        {
            vm.skipForward();
        }

        /// <summary>
        /// the play click for play video of fly
        /// </summary>
        private void Play_Click(object sender, RoutedEventArgs e)
        {
            vm.play();
        }

        /// <summary>
        /// the Pause click for Pause video of fly
        /// </summary>
        private void Pause_Click(object sender, RoutedEventArgs e)
        {
            vm.pause();
        }

        /// <summary>
        /// the Stop click for Stop video of fly
        /// </summary>
        private void Stop_Click(object sender, RoutedEventArgs e)
        {
            vm.stop();
        }

        /// <summary>
        /// the SkipEndp click for Skip End video of fly
        /// </summary>
        private void SkipEnd_Click(object sender, RoutedEventArgs e)
        {
            vm.skipEnd();
        }

        /// <summary>
        /// the SkipStart click for Skip Start video of fly
        /// </summary>
        private void SkipStart_Click(object sender, RoutedEventArgs e)
        {
            vm.skipStart();
        }
        /// <summary>
        /// to updth of the change of the slider the vm.
        /// </summary>
        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            vm.setCurrentLine((int)PlaySlider.Value);
        }
        /// <summary>
        /// to updth the speed of the video fly.
        /// </summary>
        private void Apply_Click(object sender, RoutedEventArgs e)
        {
            vm.setPlaySpeed(PlaySpeed.Text);
        }
    }
}
