using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Proj1.Models;
using System.ComponentModel;

namespace Proj1.ViewModels
{
    class LoadViewModel:INotifyPropertyChanged
    {
        private LoadModel lmodel;
        private DataModel dmodel;
        public LoadViewModel(LoadModel lm)
        {
            this.lmodel = lm;
            this.dmodel = DataModel.Instance;
            lmodel.PropertyChanged += delegate (Object sender, PropertyChangedEventArgs e)
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
        public string VM_ErrorLabel
        {
            get { return lmodel.Error; }
        }
        
        public bool load(string path)
        {
            if (lmodel.update(path))
            {
                dmodel.DllLoaded = true;
                return true;
            }
            return false;
        }
    }
}
