using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Diagnostics;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Proj1.VeiwModels;

namespace Proj1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class SettingsWindow : Window
    {
        private SettingsViewModel vm;
        
        public SettingsWindow()
        {
            InitializeComponent();
            vm = new SettingsViewModel(new SettingsModel());
            DataContext = vm;
        }


        //D:\Desktop\AdvancedProgramming2\reg_flight.csv
        private void StartSimulator(object sender, RoutedEventArgs e)
        {
            //Process.Start("D:\\Program Files\\FlightGear 2020.3.6\\bin\\fgfs.exe");// --generic=socket,in,10,127.0.0.1,5400,tcp,playback_small --fdm = null");
            //System.Diagnostics.Process.Start(@"D:\\Program Files\\FlightGear 2020.3.6\\bin\\fgfs.exe");

            Thread.Sleep(20000);
            // IPHostEntry ipHost = Dns.GetHostEntry("localhost");
            IPAddress ipAddr = IPAddress.Parse("127.0.0.1");
            IPEndPoint localEndPoint = new IPEndPoint(ipAddr, 5400);

            // Creation TCP/IP Socket using  
            // Socket Class Costructor 
            Socket client = new Socket(ipAddr.AddressFamily,
                       SocketType.Stream, ProtocolType.Tcp);

            string csvLocation = csvNormalLocationText.Text;

            List<double[]> dataList = new List<double[]>();
            string[] lineData;
            List<string> stringData = new List<string>();
            string line;
            int count = 0;//, columns = 0;
            System.IO.StreamReader file = new System.IO.StreamReader(@csvLocation);
            //check valid path 
            while ((line = file.ReadLine()) != null)
            {
                stringData.Add(line);
                lineData = line.Split(',');
                count++;
                List<double> listDouble = new List<double>();
                foreach (string s in lineData)
                {
                    listDouble.Add(Convert.ToDouble(s));
                }
                dataList.Add(listDouble.ToArray());
            }
            double[][] csvData = dataList.ToArray();

            try
            {
                // Connect Socket to the remote  
                // endpoint using method Connect() 
                client.Connect(localEndPoint);

                // We print EndPoint information  
                // that we are connected 
                Console.WriteLine("Socket connected to -> {0} ",
                              client.RemoteEndPoint.ToString());
                foreach (string s in stringData)
                {
                    Console.WriteLine(s);
                    byte[] byteData = Encoding.ASCII.GetBytes(s+"\r\n");
                    byte[] b = new byte[400];
                    

                    for (int i = 0; i < 300; i++) {
                        string message = Encoding.ASCII.GetString(b);
                        //Console.Write(message);
                    }
                    int sent = client.Send(byteData);
                    //Console.WriteLine(sent);
                    Thread.Sleep(100);
                }

                // Close Socket using  
                // the method Close() 
                client.Shutdown(SocketShutdown.Both);
                client.Close();

            }
            // Manage of Socket's Exceptions 
            catch (ArgumentNullException ane)
            {

                Console.WriteLine("ArgumentNullException : {0}", ane.ToString());
            }

            catch (SocketException se)
            {

                Console.WriteLine("SocketException : {0}", se.ToString());
            }

            catch (Exception ex)
            {
                Console.WriteLine("Unexpected exception : {0}", ex.ToString());
            }
        }

        private void Continue_Click(object sender, RoutedEventArgs e)
        {
            if (vm.check())
            {
                this.Close();
            }
        }
    }
}
