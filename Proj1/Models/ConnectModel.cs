using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Proj1.ViewModels;
using System.ComponentModel;
using System.Net.Sockets;
using System.Threading;
using System.Net;
using System.Windows;

namespace Proj1.Models
{
    /// <summary>
    ///  A ConnectModel class. the  model of connection to fly geer part 
    /// </summary>
    class ConnectModel : INotifyPropertyChanged
    {
        //feilds
        //TCP Parametes
        private string ip;
        private string port;
        // the error in the connection
        private string errorLabel;
        private bool connected = false;
        /// <summary>
        ///the constructor of ConnectModel.
        /// </summary>
        public ConnectModel() {
            errorLabel = "";
            IP = "127.0.0.1";
            Port = "5400";
            DataModel.Instance.Connected = false;
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
        ///property of IP
        /// </summary>
        public string IP
        {
            get { return ip; }
            set
            {
                ip = value;
                //DELETE
                NotifyPropertyChanged("IP");
                //
            }
        }
        /// <summary>
        ///property of port
        /// </summary>
        public string Port
        {
            get { return port; }
            set
            {
                port = value;
                //DELETE
                NotifyPropertyChanged("Port");
                //
            }
        }
        /// <summary>
        ///property of the error - if have error in data to connect
        /// </summary>
        public string Error
        {
            get { return errorLabel; }
            set
            {
                errorLabel = value;
                NotifyPropertyChanged("ErrorLabel");
            }
        }

        /// <summary>
        ///start connection with the flight geer
        /// </summary>
        private void StartSocket()
        {

            try
            {
                // entry the data for a socket
                IPAddress ipAddr = IPAddress.Parse(IP);
                int portInt = int.Parse(Port);
                IPEndPoint localEndPoint = new IPEndPoint(ipAddr, portInt);

                Socket client = new Socket(ipAddr.AddressFamily,
                           SocketType.Stream, ProtocolType.Tcp);

                client.Connect(localEndPoint);
                connected = true;
                //updth the data about the conection
                DataModel.Instance.Socket = client;
                DataModel.Instance.Connected = true;
            }
            catch
            {
                // if its not connect message to user
                Error = "ERROR: Could not Connect.";
            }
        }

        /// <summary>
        ///return true if the connectionSucceeded
        /// </summary>
        public bool isEverythingOK()
        {
            // try to connect to flight geer.
            StartSocket();
            return connected;
        }
    }
}
