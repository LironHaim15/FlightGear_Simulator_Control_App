using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Proj1.Models;
using System.ComponentModel;

namespace Proj1.ViewModels
{
    class AnomaliesViewModel:INotifyPropertyChanged
    {
        private AnomaliesModel amodel;
        private DataModel dmodel;
        public AnomaliesViewModel(AnomaliesModel am)
        {
            this.amodel = am;
            this.dmodel = DataModel.Instance;
            this.amodel.PropertyChanged += delegate (Object sender, PropertyChangedEventArgs e)
            {
                NotifyPropertyChanged("VM_" + e.PropertyName);
            };
            this.dmodel.PropertyChanged += delegate (Object sender, PropertyChangedEventArgs e)
            {

                NotifyPropertyChanged("VM_" + e.PropertyName);
            };
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(string propName)
        {
            if (propName == "VM_SettingsOK")
                amodel.getFeatures();
            else if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
        public List<string> VM_FeaturesList
        {
            get { return amodel.FeaturesList; }
        }
    }
}
