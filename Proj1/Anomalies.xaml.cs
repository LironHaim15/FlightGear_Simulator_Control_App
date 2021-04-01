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

namespace Proj1
{
    /// <summary>
    /// Interaction logic for Anomalies.xaml
    /// </summary>
    public partial class Anomalies : UserControl
    {
        private AnomaliesViewModel avm;
        public Anomalies()
        {
            InitializeComponent();
        }
    }
}
