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
using System.ComponentModel;
using Proj1.VeiwModels;
using Proj1.Models;
using System.Runtime.InteropServices;

namespace Proj1
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class Joystick : UserControl
    {
        private JoystickViewModel vm;

        public Joystick()
        {
            InitializeComponent();
            vm = new JoystickViewModel(new JoystickModel());
            DataContext = vm;
        }
    }
}
