using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.IO;

namespace Proj1.Models
{
    /// <summary>
    ///  A  InfoModel class
    /// </summary>
    /// <remarks>
    /// to display informtion to user
    /// </remarks>
    class InfoModel :INotifyPropertyChanged
    {
        //feilds
        private string info;
        /// <summary>
        ///the constructor of InfoModel.
        /// </summary>
        public InfoModel() {
            try
            {
                // from txt the informtion
                InfoText = File.ReadAllText("README.txt");
            }
            catch {
                InfoText = "Could not open README.txt file and display it.\nPlease find it in the application folder.";
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        /// <summary>
        ///mvvm notify of changes of model
        /// </summary>
        private void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
        /// <summary>
        ///property of InfoText
        /// </summary>
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
