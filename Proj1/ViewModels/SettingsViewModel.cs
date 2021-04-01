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

        private string csvNormalPath;
        public string VM_CsvNormalPath
        {
            get { return csvNormalPath; }
            set { csvNormalPath = value;
                smodel.updatePath(csvNormalPath, csvTestPath, flightGearPath, xmlPath);
            }
        }
        private string csvTestPath;
        public string VM_CsvTestPath
        {
            get { return csvTestPath; }
            set
            {
                csvTestPath = value;
                smodel.updatePath(csvNormalPath, csvTestPath, flightGearPath, xmlPath);
            }
        }
        private string flightGearPath;
        public string VM_FlightGearPath
        {
            get { return flightGearPath; }
            set
            {
                flightGearPath = value;
                smodel.updatePath(csvNormalPath, csvTestPath, flightGearPath, xmlPath);
            }
        }
        private string xmlPath;
        public string VM_XmlPath
        {
            get { return xmlPath; }
            set
            {
                xmlPath = value;
                smodel.updatePath(csvNormalPath, csvTestPath, flightGearPath, xmlPath);
            }
        }

        public string VM_ErrorLabel
        {
            get { return smodel.Error; }
        }

        public bool check()
        {
            return smodel.isEverythingOK();
        }
    }
}
