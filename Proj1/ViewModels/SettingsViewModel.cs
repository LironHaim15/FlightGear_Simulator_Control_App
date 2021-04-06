using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using Proj1.Models;

namespace Proj1
{
    class SettingsViewModel : INotifyPropertyChanged
    {
        private SettingsModel smodel;
        public SettingsViewModel(SettingsModel smodel) {
            this.smodel = smodel;
            smodel.PropertyChanged += delegate (Object sender, PropertyChangedEventArgs e)
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

        public string VM_CsvNormalPath
        {
            get { return smodel.CsvNormalPath; }
            set { smodel.CsvNormalPath = value; }
        }
        public string VM_CsvTestPath
        {
            get { return smodel.CsvTestPath; }
            set { smodel.CsvTestPath = value; }
        }
        public string VM_FlightGearPath
        {
            get { return smodel.FlightGearPath; }
            set { smodel.FlightGearPath = value; }
        }
        public string VM_ErrorLabel
        {
            get { return smodel.Error; }
        }
        public void browse(string type)
        {
            smodel.browse(type);
        }
        public bool check()
        {
            return smodel.isEverythingOK();
        }
    }
}
