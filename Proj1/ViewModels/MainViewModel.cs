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

        public bool VM_ConnectEnable
        {
            get
            {
                if (!dmodel.SettingsOK || dmodel.Connected)
                {
                    return false;
                }
                return true;
            }
        }
        public bool VM_DisconnectEnable
        {
            get
            {
                if (dmodel.Connected)
                {
                    return true;
                }
                return false;
            }
        }
        public bool VM_SettingsEnable
        {
            get
            {
                if (dmodel.Connected)
                {
                    return false;
                }
                return true;
            }
        }
        public bool VM_LoadEnable
        {
            get
            {
                if (dmodel.SettingsOK)
                {
                    return true;
                }
                return false;
            }
        }
        public string VM_DashboardVisibility
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
        public string VM_JoystickVisibility
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
        public string VM_AnomaliesVisibility
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
        public string VM_PlaybarVisibility
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
        public void disconnect()
        {
            if (dmodel.Connected)
            {
                dmodel.Socket.Shutdown(SocketShutdown.Both); //CHECK IF NECCESSARY
                dmodel.closeThread();
                dmodel.Socket.Close();
                dmodel.Connected = false;
                dmodel.softReset();
            }
        }
    }
}
