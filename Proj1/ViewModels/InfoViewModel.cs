using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Proj1.Models;
using System.ComponentModel;

namespace Proj1.ViewModels
{
    /// <summary>
    ///  A  InfoViewModel class
    /// </summary>
    /// <remarks>
    /// to display informtion to user
    /// </remarks>
    class InfoViewModel :INotifyPropertyChanged
    {
        //feilds
        private InfoModel imodel;
        /// <summary>
        ///the constructor of InfoViewModel.
        /// </summary>
        public InfoViewModel(InfoModel im)
        {
            this.imodel = im;
            this.imodel.PropertyChanged += delegate (Object sender, PropertyChangedEventArgs e)
            {
                NotifyPropertyChanged("VM_" + e.PropertyName);
            };
        }
        public event PropertyChangedEventHandler PropertyChanged;
        /// <summary>
        ///mvvm notify of changes of model to veiw
        /// </summary>
        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
        /// <summary>
        ///property of VM_InfoText
        /// </summary>
        public string VM_InfoText
        {
            get { return imodel.InfoText; }
        }
    }
}
