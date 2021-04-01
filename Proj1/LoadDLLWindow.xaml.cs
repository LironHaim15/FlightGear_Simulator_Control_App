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
    public partial class LoadDLLWindow : Window
    {
        private LoadViewModel vm;
        //private string extension;
        public LoadDLLWindow()
        {
            InitializeComponent();
            vm = new LoadViewModel(new LoadModel());
            DataContext = vm;
        }

        private void Load_Click(object sender, RoutedEventArgs e)
        {
            if (vm.load(dllPath.Text))
            {
                this.Close();
            }
        }

        private void Browse_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog openFileDlg = new Microsoft.Win32.OpenFileDialog();
            Nullable<bool> result = openFileDlg.ShowDialog();
            if (result == true)
                dllPath.Text = System.IO.Path.GetFullPath(openFileDlg.FileName);
        }
    }
}
