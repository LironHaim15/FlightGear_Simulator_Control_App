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
    class ConnectModel : INotifyPropertyChanged
    {
        //TCP Parametes
        private string ip;
        private string port;
        private string errorLabel;
        private bool connected = false;
        public ConnectModel() {
            errorLabel = "";
            IP = "127.0.0.1";
            Port = "5400";
            DataModel.Instance.Connected = false;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
        public string IP
        {
            get { return ip; }
            set { ip = value;
                NotifyPropertyChanged("IP");
            }
        }
        public string Port
        {
            get { return port; }
            set
            {
                port = value;
                NotifyPropertyChanged("Port");
            }
        }
        public string Error
        {
            get { return errorLabel; }
            set { errorLabel = value;
                NotifyPropertyChanged("ErrorLabel");
            }
        }
        private void StartSocket()
        {
            //Process.Start("D:\\Program Files\\FlightGear 2020.3.6\\bin\\fgfs.exe");// --generic=socket,in,10,127.0.0.1,5400,tcp,playback_small --fdm = null");
            //System.Diagnostics.Process.Start(@"D:\\Program Files\\FlightGear 2020.3.6\\bin\\fgfs.exe");
            try
            {
                IPAddress ipAddr = IPAddress.Parse(IP);
                int portInt = int.Parse(Port);
                IPEndPoint localEndPoint = new IPEndPoint(ipAddr, portInt);

                Socket client = new Socket(ipAddr.AddressFamily,
                           SocketType.Stream, ProtocolType.Tcp);

                client.Connect(localEndPoint);
                connected = true;
                DataModel.Instance.Socket = client;
                DataModel.Instance.Connected = true;
            }
            catch 
            {
                Error = "ERROR: Could not Connect.";
            }
        }
        public bool isEverythingOK()
        {
            StartSocket();
            return connected;
        }
    }
}
