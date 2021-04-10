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
    /// <summary>
    ///  A MainViewModel class
    /// </summary>
    /// <remarks>
    /// that conected between veiw and model of the project.
    /// </remarks>
    class MainViewModel : INotifyPropertyChanged
    {
        // data model of all the project
        DataModel dmodel;
        /// <summary>
        ///the constructor of  MainViewModel.
        /// </summary>
        public MainViewModel() {
            this.dmodel = DataModel.Instance;
            dmodel.PropertyChanged += delegate (Object sender, PropertyChangedEventArgs e)
            {
                NotifyPropertyChanged("VM_" + e.PropertyName);
            };
        }

        public event PropertyChangedEventHandler PropertyChanged;
        /// <summary>
        ///mvvm notify of changes to veiw from model.
        /// </summary>
        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
       //DELETE
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
        //DELETE
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
        //DELETE
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
        //DELETE
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
        /// <summary>
        ///property of  VM_DashboardVisibility
        /// </summary>
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
        /// <summary>
        ///property of  VM_JoystickVisibility
        /// </summary>
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
        /// <summary>
        ///property of  VM_AnomaliesVisibility
        /// </summary>
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
        /// <summary>
        ///property of  VM_PlaybarVisibility
        /// </summary>
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
        /// <summary>
        ///disconnect from flight geer
        /// </summary>
        public void disconnect()
        {

            if (dmodel.Connected)
            {
                // close thread and socket and delete all the data that save . for next time clear start.
                dmodel.Socket.Shutdown(SocketShutdown.Both); //DELETE
                dmodel.closeThread();
                dmodel.Socket.Close();
                //DELETE
                dmodel.Connected = false;
                dmodel.softReset();
            }
        }
    }
}
