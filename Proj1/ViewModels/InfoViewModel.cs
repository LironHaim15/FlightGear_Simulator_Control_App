using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Proj1.Models;
using System.ComponentModel;

namespace Proj1.ViewModels
{
    class InfoViewModel:INotifyPropertyChanged
    {
        private InfoModel imodel;
        public InfoViewModel(InfoModel im)
        {
            this.imodel = im;
            this.imodel.PropertyChanged += delegate (Object sender, PropertyChangedEventArgs e)
            {
                NotifyPropertyChanged("VM_" + e.PropertyName);
            };
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
        public string VM_InfoText
        {
            get { return imodel.InfoText; }
        }
    }
}
