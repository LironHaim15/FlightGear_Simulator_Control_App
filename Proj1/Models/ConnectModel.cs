using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Proj1.ViewModels;
using System.ComponentModel;
using System.Net.Sockets;
using System.Threading.Tasks;
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
            port = "";
            ip = "";
            DataModel.Instance.Connected = false;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }

        public void update(string VM_IP, string VM_Port)
        {
            ip = VM_IP;
            port = VM_Port;
            errorLabel = "";
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
                IPAddress ipAddr = IPAddress.Parse(ip);
                int portInt = int.Parse(port);
                IPEndPoint localEndPoint = new IPEndPoint(ipAddr, portInt);

                // Creation TCP/IP Socket using  
                // Socket Class Costructor 
                Socket client = new Socket(ipAddr.AddressFamily,
                           SocketType.Stream, ProtocolType.Tcp);

                // Connect Socket to the remote  
                // endpoint using method Connect() 
                client.Connect(localEndPoint);
                connected = true;
                DataModel.Instance.Socket = client;
                DataModel.Instance.Connected = true;
                // We print EndPoint information  

            }
            // Manage of Socket's Exceptions 
            catch (ArgumentNullException ane)
            {

                //Console.WriteLine("ArgumentNullException : {0}", ane.ToString());
                Error = "ERROR: Could not Connect.";
            }

            catch (SocketException se)
            {
                Error = "ERROR: Could not Connect.";
                //Console.WriteLine("SocketException : {0}", se.ToString());
            }
       /*   catch (FormatException e)
            {
                Error = "ERROR: Could not Connect.";
            }*/
            catch (Exception ex)
            {
                Error = "ERROR: Could not Connect.";
                //Console.WriteLine("Unexpected exception : {0}", ex.ToString());
            }
            
        }
        public bool isEverythingOK()
        {
            StartSocket();
            return connected;
        }
    }
}
