using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using Proj1.Models;

namespace Proj1.ViewModels
{
    /// <summary>
    ///  A ConnectViewModel  class. the veiw model of connect the fly  part 
    /// </summary>
    /// <remarks>
    /// that conected between veiw and model of Connect.
    /// </remarks>
    class ConnectViewModel : INotifyPropertyChanged
    {
        //feilds
        ConnectModel cmodel;

        /// <summary>
        ///the constructor of ConnectViewModel.
        /// </summary>
        public ConnectViewModel(ConnectModel cm) {
            // updth the model and add fuction.
            this.cmodel = cm;
            cmodel.PropertyChanged += delegate (Object sender, PropertyChangedEventArgs e)
            {
                NotifyPropertyChanged("VM_" + e.PropertyName);
            };
        }
        public event PropertyChangedEventHandler PropertyChanged;
        /// <summary>
        ///mvvm notify of chenges to veiw from model.
        /// </summary>
        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
        /// <summary>
        ///property of the error - if have error in data to connect
        /// </summary>
        public string VM_ErrorLabel
        {
            get { return cmodel.Error; }
        }

        /// <summary>
        ///property of IP
        /// </summary>
        public string VM_IP
        {
            get { return cmodel.IP; }
        }
        /// <summary>
        ///property of port
        /// </summary>
        public string VM_Port
        {
            get { return cmodel.Port; }
        }
        /// <summary>
        ///chack if the data to connect are right
        /// </summary>
        public bool check(string ip,string port)
        {
            cmodel.IP = ip;
            cmodel.Port = port;
            return cmodel.isEverythingOK();
        }
    }
}
