using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Net.Sockets;


namespace Proj1.Models
{
    class DataModel : INotifyPropertyChanged
    {
        private static DataModel dm;
        private int currentLine;
        private int maxLines;
        private double[][] csvData;
        private List<string> stringData;
        private List<string> featuresNames;
        private Dictionary<string,int> dashboardFeatures;
        private Dictionary<string, int> joystickFeatures;
        private string dllPath;
        private bool connected;
        private bool settingsOK;
        private bool algoritmLoaded;
        private Socket fgClient;
        private DataModel() {
            currentLine = 0;
            stringData = new List<string>();
            featuresNames = new List<string>();
            dashboardFeatures = new Dictionary<string, int>();
            joystickFeatures = new Dictionary<string, int>();
            connected = false;
            algoritmLoaded = false;
            settingsOK = false;
            maxLines = 0;
            dllPath = null;
        }
        public static DataModel Instance
        {
            get
            {
                if (dm == null)
                    dm = new DataModel();
                return dm;
            }
        }

        public int CurrentLine
        {
            get { return currentLine; }
            set { currentLine = value;
                NotifyPropertyChanged("CurrentUpdated");
            }
        }
        public  int MaxLines
        {
            get { return  maxLines; }
            set { maxLines = value;
                NotifyPropertyChanged("MaxLines");
            }
        }
        public double[][] CsvData
        {
            get { return csvData; }
            set { csvData = value; }
        }
        public List<string> FeaturesNames
        {
            get { return featuresNames; }
        }
        public List<string> StringData
        {
            get { return stringData; }
        }
        public Dictionary<string, int> DashboardFeatures
        {
            get { return dashboardFeatures; }
        }
        public Dictionary<string, int> JoystickFeatures
        {
            get { return joystickFeatures; }
        }
        public Socket Socket
        {
            get { return fgClient; }
            set { fgClient = value; }
        }
        public bool Connected
        {
            get { return connected; }
            set { connected = value;
                NotifyPropertyChanged("DisconnectVisibility");
                NotifyPropertyChanged("ConnectVisibility");
                NotifyPropertyChanged("SettingsVisibility");
            }
        }
        public bool SettingsOK
        {
            get { return settingsOK; }
            set { settingsOK = value;
                NotifyPropertyChanged("SettingsVisibility");
                NotifyPropertyChanged("ConnectVisibility");
                if (settingsOK == true)
                    NotifyPropertyChanged("SettingsOK");

            }
        }
        public string DLLPath
        {
            get { return dllPath; }
            set { dllPath = value; }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
    }
}
