using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.IO;

namespace Proj1.Models
{
    class InfoModel:INotifyPropertyChanged
    {
        private string info;
        public InfoModel() {
            try
            {
                InfoText = File.ReadAllText("README.txt");
            }
            catch {
                InfoText = "Could not open README.txt file and display it.\nPlease find it in the application folder.";
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
        public string InfoText
        {
            get { return info; }
            set
            {
                info = value;
                NotifyPropertyChanged("InfoText");
            }
        }
    }
}
