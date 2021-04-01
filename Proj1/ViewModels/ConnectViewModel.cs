using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using Proj1.Models;

namespace Proj1.ViewModels
{
    class ConnectViewModel : INotifyPropertyChanged
    {
        ConnectModel cmodel;
        public ConnectViewModel(ConnectModel cm) {
            this.cmodel = cm;
            cmodel.PropertyChanged += delegate (Object sender, PropertyChangedEventArgs e)
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
            get { return cmodel.Error; }
        }
        private string ip, port;
        public string VM_IP
        {
            get { return ip; }
            set { ip = value;
                cmodel.update(ip, port); }
        }
        public string VM_Port
        {
            get { return port; }
            set
            {
                port = value;
                cmodel.update(ip, port);
            }
        }
        public bool check()
        {
            return cmodel.isEverythingOK();
        }
    }
}
