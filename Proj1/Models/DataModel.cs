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
     /// <summary>
     ///  A DataModel class. the  model of all project part 
     /// </summary>
     /// <remarks>
     /// save all informtion and this object is seglton create one time.
     /// </remarks>
    class DataModel : INotifyPropertyChanged
    {
        //feilds
        //seglton 
        private static DataModel dm;
        //the currentLine in the fly
        private int currentLine;
        //the choice of to investigate a particular feauture by a grph 
        private bool changeChoice;
        private string nameChoice;
        // the thread is runing.
        private Thread thread;
        // the number of lines in the fly
        private int maxLines;
        // the data of the fly and the learn of the fly
        private double[,] csvData;
        private double[,] learnData;
        private List<string> stringData;
        // the features in this fly
        private List<string> featuresNames;
        // the index of the feautres of dashbored
        private Dictionary<string, int> dashboardFeatures;
        // the index of the feautres of  joystick
        private Dictionary<string, int> joystickFeatures;
        // flag to know if the user do setting,conectted and loaded dll
        private bool connected;
        private bool settingsOK;
        private bool dllLoaded;
        // the anomleis informtion which line and the description of anomaliy
        private Dictionary<int, List<string>> anomalies;
        private Dictionary<string, List<DataPoint>> pointsCorGraph;
        private List<string> anomaliesList;
        // the sockt of conected the fly
        private Socket fgClient;
        // for dashbored to know the max speed of this fly
        private double maxSpeed;
        // if the fly move
        private bool toPlay;
        /// <summary>
        ///the constructor of DataModel.
        /// </summary>
        private DataModel()
        {
            currentLine = 0;
            stringData = new List<string>();
            featuresNames = new List<string>();
            dashboardFeatures = new Dictionary<string, int>();
            joystickFeatures = new Dictionary<string, int>();
            anomalies = new Dictionary<int, List<string>>();
            pointsCorGraph = new Dictionary<string, List<DataPoint>>();
            connected = false;
            anomaliesList = new List<string>();
            settingsOK = false;
            maxLines = 0;
            dllLoaded = false;
            changeChoice = false;
            nameChoice = "";
        }
        /// <summary>
        ///sigelton instance
        /// </summary>
        public static DataModel Instance
        {
            get
            {
                if (dm == null)
                    dm = new DataModel();
                return dm;
            }
        }
        /// <summary>
        ///property of  CurrentLine
        /// </summary>
        public int CurrentLine
        {
            get { return currentLine; }
            set
            {
                if (currentLine != value)
                {
                    currentLine = value;
                    // updth who it need to know the currentLine
                    NotifyPropertyChanged("CurrentUpdate");
                }
            }
        }
        /// <summary>
        ///property of  MaxLines
        /// </summary>
        public int MaxLines
        {
            get { return maxLines; }
            set
            {
                maxLines = value;
                // updth who it need to know the MaxLines
                NotifyPropertyChanged("MaxLines");
            }
        }
        /// <summary>
        ///property of  MaxSpeed
        /// </summary>
        public double MaxSpeed
        {
            get { return maxSpeed; }
            set { maxSpeed = value; }
        }
        /// <summary>
        ///property of  CsvData
        /// </summary>
        public double[,] CsvData
        {
            get { return csvData; }
            set { csvData = value; }
        }
        /// <summary>
        ///property of  LearnData
        /// </summary>
        public double[,] LearnData
        {
            get { return learnData; }
            set { learnData = value; }
        }
        /// <summary>
        ///property of  FeaturesNames
        /// </summary>
        public List<string> FeaturesNames
        {
            get { return featuresNames; }
        }
        /// <summary>
        ///property of  StringData
        /// </summary>
        public List<string> StringData
        {
            get { return stringData; }
        }
        /// <summary>
        ///property of  DashboardFeatures
        /// </summary>
        public Dictionary<string, int> DashboardFeatures
        {
            get { return dashboardFeatures; }
        }
        /// <summary>
        ///property of  JoystickFeatures
        /// </summary>
        public Dictionary<string, int> JoystickFeatures
        {
            get { return joystickFeatures; }
        }
        /// <summary>
        ///property of  Anomalies
        /// </summary>
        public Dictionary<int, List<string>> Anomalies
        {
            get { return anomalies; }
        }
        /// <summary>
        ///property of  GraphPoints
        /// </summary>
        public Dictionary<string, List<DataPoint>> GraphPoints
        {
            get { return pointsCorGraph; }
        }
        /// <summary>
        ///property of AnomaliesList
        /// </summary>
        public List<string> AnomaliesList
        {
            get { return anomaliesList; }
        }
        /// <summary>
        ///property of  Socket
        /// </summary>
        public Socket Socket
        {
            get { return fgClient; }
            set { fgClient = value; }
        }
        /// <summary>
        ///property of  Thread
        /// </summary>
        public Thread Thread
        {
            get { return thread; }
            set { thread = value; }
        }
        /// <summary>
        ///property of  ToPlay
        /// </summary>
        public bool ToPlay
        {
            get { return toPlay; }
            set
            {
                toPlay = value;
                NotifyPropertyChanged("ToPlay");
            }
        }
        /// <summary>
        ///close the thread
        /// </summary>
        public void closeThread()
        {
            ToPlay = false;
        }
        /// <summary>
        ///updth the courrent line
        /// </summary>
        public void setCurrentLine(int line)
        {
            CurrentLine = line;
            NotifyPropertyChanged("CurrentChanged");
        }
        /// <summary>
        ///property of   DllLoaded
        /// </summary>
        public bool DllLoaded
        {
            get { return dllLoaded; }
            set
            {
                dllLoaded = value;
                NotifyPropertyChanged("DllLoaded");
            }
        }
        /// <summary>
        ///property of  Connected
        /// </summary>
        public bool Connected
        {
            get { return connected; }
            set
            {
                connected = value;
                // notify for connected
                NotifyPropertyChanged("DisconnectEnable");
                NotifyPropertyChanged("ConnectEnable");
                NotifyPropertyChanged("SettingsEnable");
                NotifyPropertyChanged("DashboardVisibility");
                NotifyPropertyChanged("JoystickVisibility");
                NotifyPropertyChanged("PlaybarVisibility");
                NotifyPropertyChanged("AnomaliesVisibility");
            }
        }
        /// <summary>
        ///property of SettingsOK
        /// </summary>
        public bool SettingsOK
        {
            get { return settingsOK; }
            set
            {
                settingsOK = value;
                NotifyPropertyChanged("SettingsEnabley");
                NotifyPropertyChanged("ConnectEnable");
                NotifyPropertyChanged("LoadEnable");
                if (settingsOK == true)
                    NotifyPropertyChanged("SettingsOK");
            }
        }
        /// <summary>
        ///reseset some  data
        /// </summary>
        public void softReset()
        {
            Connected = false;
            SettingsOK = false;
            CurrentLine = 0;
            changeChoice = false;
            nameChoice = "";
        }
        /// <summary>
        ///reseset the all data
        /// </summary>
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
            pointsCorGraph.Clear();
            fgClient = null;
            NotifyPropertyChanged("Restart");
        }
        /// <summary>
        ///property of  ChangeChoice
        /// </summary>
        public bool ChangeChoice
        {
            get { return changeChoice; }
            set { changeChoice = value; }
        }

        /// <summary>
        ///property of  NameChoice
        /// </summary>
        public string NameChoice
        {
            get { return nameChoice; }
            set { nameChoice = value; }
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
    }
}
