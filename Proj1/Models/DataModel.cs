using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Net.Sockets;
using OxyPlot;
using System.Threading;


namespace Proj1.Models
{
    class DataModel : INotifyPropertyChanged
    {
        private static DataModel dm;
        private int currentLine;
        private bool changeChoice;
        private string nameChoice;
        private Thread thread;
        private int maxLines;
        private double[,] csvData;
        private double[,] learnData;
        private List<string> stringData;
        private List<string> featuresNames;
        private Dictionary<string,int> dashboardFeatures;
        private Dictionary<string, int> joystickFeatures;
        private bool connected;
        private bool settingsOK;
        private bool dllLoaded;
        private Dictionary<int, List<string>> anomalies;
        private List<string> anomaliesList;
        private Socket fgClient;
        private double maxSpeed;
        private bool toPlay;
        private DataModel() {
            currentLine = 0;
            stringData = new List<string>();
            featuresNames = new List<string>();
            dashboardFeatures = new Dictionary<string, int>();
            joystickFeatures = new Dictionary<string, int>();
            anomalies = new Dictionary<int, List<string>>();
            connected = false;
            anomaliesList = new List<string>();
            settingsOK = false;
            maxLines = 0;
            dllLoaded = false;
            changeChoice = false;
            nameChoice = "";
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
            set
            {
                if (currentLine != value)
                {
                    currentLine = value;
                    NotifyPropertyChanged("CurrentUpdate");
                }
            }
        }
        public  int MaxLines
        {
            get { return  maxLines; }
            set { maxLines = value;
                NotifyPropertyChanged("MaxLines");
            }
        }
        public double MaxSpeed
        {
            get { return maxSpeed; }
            set { maxSpeed=value; }
        }
        public double[,] CsvData
        {
            get { return csvData; }
            set { csvData = value; }
        }
        public double[,] LearnData
        {
            get { return learnData; }
            set { learnData = value; }
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
        public Dictionary<int, List<string>> Anomalies
        {
            get { return anomalies; }
        }
        public List<string> AnomaliesList
        {
            get { return anomaliesList; }
        }
        public Socket Socket
        {
            get { return fgClient; }
            set { fgClient = value; }
        }
        public Thread Thread
        {
            get { return thread; }
            set { thread = value; }
        }
        public bool ToPlay
        {
            get { return toPlay; }
            set { toPlay = value;
                NotifyPropertyChanged("ToPlay");
            }
        }
        public void closeThread()
        {
            ToPlay = false;
        }
        public void setCurrentLine(int line)
        {
            CurrentLine = line;
            NotifyPropertyChanged("CurrentChanged");
        }
        public bool DllLoaded
        {
            get { return dllLoaded; }
            set
            {
                dllLoaded = value;
                NotifyPropertyChanged("DllLoaded");
            }
        }
        public bool Connected
        {
            get { return connected; }
            set { connected = value;
                NotifyPropertyChanged("DisconnectEnable");
                NotifyPropertyChanged("ConnectEnable");
                NotifyPropertyChanged("SettingsEnable");
                NotifyPropertyChanged("DashboardVisibility");
                NotifyPropertyChanged("JoystickVisibility");
                NotifyPropertyChanged("PlaybarVisibility");
                NotifyPropertyChanged("AnomaliesVisibility");
            }
        }
        public bool SettingsOK
        {
            get { return settingsOK; }
            set { settingsOK = value;
                NotifyPropertyChanged("SettingsEnabley");
                NotifyPropertyChanged("ConnectEnable");
                NotifyPropertyChanged("LoadEnable");
                if (settingsOK == true)
                    NotifyPropertyChanged("SettingsOK");
            }
        }

        public void softReset()
        {
            SettingsOK = false;
            Connected = false;
            CurrentLine = 0;
            changeChoice = false;
            nameChoice = "";
        }
        public void hardReset()
        {
            currentLine = 0;
            changeChoice = false;
            nameChoice = null;
            thread = null;
            maxLines = 0;
            csvData = null;
            learnData = null;
            stringData.Clear();
            featuresNames.Clear();
            dashboardFeatures.Clear();
            joystickFeatures.Clear();
            connected = false;
            settingsOK = false;
            dllLoaded = false;
            anomalies.Clear();
            anomaliesList.Clear();
            fgClient = null;
            NotifyPropertyChanged("Restart");
    }
        public bool ChangeChoice
        {
            get { return changeChoice; }
            set { changeChoice = value; }
        }

        public string NameChoice
        {
            get { return nameChoice; }
            set { nameChoice = value; }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
    }
}
