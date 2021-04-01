using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using Proj1.Models;
using System.Net.Sockets;

namespace Proj1.VeiwModels
{
    class MainViewModel : INotifyPropertyChanged
    {
        DataModel dmodel;
        public MainViewModel() {
            this.dmodel = DataModel.Instance;
            dmodel.PropertyChanged += delegate (Object sender, PropertyChangedEventArgs e)
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

        public string VM_ConnectVisibility
        {
            get
            {
                if (!dmodel.SettingsOK || dmodel.Connected)
                {
                    return "Hidden";
                }
                return "Visible";
            }
        }
        public string VM_DisconnectVisibility
        {
            get
            {
                if (dmodel.Connected)
                {
                    return "Visible";
                }
                return "Hidden";
            }
        }
        public string VM_SettingsVisibility
        {
            get
            {
                if (dmodel.SettingsOK)
                {
                    return "Hidden";
                }
                return "Visible";
            }
        }
        public void disconnect()
        {
            //dmodel.Socket.Shutdown(SocketShutdown.Both); CHECK IF NECCESSARY
            dmodel.Socket.Close();
            dmodel.Connected = false;
            dmodel.SettingsOK = false;
        }

    }
}
