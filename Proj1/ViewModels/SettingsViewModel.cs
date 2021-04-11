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
        /// <summary>
        /// get and set the learing csv file path
        /// </summary>
        public string VM_CsvNormalPath
        {
            get { return smodel.CsvNormalPath; }
            set { smodel.CsvNormalPath = value; }
        }
        /// <summary>
        /// get and set the test csv file path
        /// </summary>
        public string VM_CsvTestPath
        {
            get { return smodel.CsvTestPath; }
            set { smodel.CsvTestPath = value; }
        }
        /// <summary>
        /// get and set the FlightGear directory path
        /// </summary>
        public string VM_FlightGearPath
        {
            get { return smodel.FlightGearPath; }
            set { smodel.FlightGearPath = value; }
        }
        /// <summary>
        /// get the error string for the settings view. displayed in the error label.
        /// </summary>
        public string VM_ErrorLabel
        {
            get { return smodel.Error; }
        }
        /// <summary>
        /// called when clicking on browse buttons.
        /// </summary>
        /// <param name="type">represents which browse button was clicked.</param>
        public void browse(string type)
        {
            smodel.browse(type);
        }
        /// <summary>
        /// called when 'Continue' button is pressed.
        /// </summary>
        /// <returns></returns>
        public bool check()
        {
            return smodel.isEverythingOK();
        }
    }
}
