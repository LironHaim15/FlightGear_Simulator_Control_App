using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
using System.Text;
using System.Xml;
using System.Threading.Tasks;
using Proj1.Models;


/**
 * check if name of xml is always the same.
*/

namespace Proj1
{
    class SettingsModel: INotifyPropertyChanged
    {
        //Path Parameters
        private string csvNormalPath;
        private string csvTestPath;
        private string flightGearPath;
        private string xmlPath;
        //errors
        private string errorLabel;
       
        public SettingsModel() { DataModel.Instance.SettingsOK = false;
            CsvNormalPath = "D:\\Desktop\\AdvancedProgramming2\\reg_flight.csv";
            CsvTestPath = "D:\\Desktop\\AdvancedProgramming2\\anomaly_flight - Copy.csv";
            FlightGearPath = "D:\\Program Files\\FlightGear 2020.3.6";
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }

       public string Error
        {
            get { return errorLabel; }
            set { errorLabel = value;
                NotifyPropertyChanged("ErrorLabel");
            }
        }
        public string CsvNormalPath
        {
            get { return csvNormalPath; }
            set
            {
                csvNormalPath = value;
                NotifyPropertyChanged("CsvNormalPath");
            }
        }
        public string CsvTestPath
        {
            get { return csvTestPath; }
            set
            {
                csvTestPath = value;
                NotifyPropertyChanged("CsvTestPath");
            }
        }
        public string FlightGearPath
        {
            get { return flightGearPath; }
            set
            {
                flightGearPath = value;
                NotifyPropertyChanged("FlightGearPath");
            }
        }
        private bool isCorrectPath(string path)
        {
            string filePath = path;
            return File.Exists(filePath);
        }

        public void browse(string type)
        {
            Microsoft.Win32.OpenFileDialog openFileDlg = new Microsoft.Win32.OpenFileDialog();
            Nullable<bool> result = openFileDlg.ShowDialog();
            if (result == true)
            {
                if(type == "FlightGearPath")
                    FlightGearPath= Path.GetFullPath(openFileDlg.FileName);
                if (type == "CsvNormalPath")
                    CsvNormalPath = Path.GetFullPath(openFileDlg.FileName);
                if (type == "CsvTestPath")
                    CsvTestPath = Path.GetFullPath(openFileDlg.FileName);
                if (type == "XmlPath")
                    xmlPath = Path.GetFullPath(openFileDlg.FileName);
            }
        }
        private void getDashboardFeatures()
        {
            Dictionary<string, int> dashboard = DataModel.Instance.DashboardFeatures;
            List<string> list = DataModel.Instance.FeaturesNames;
            dashboard.Add("altimeter",list.IndexOf("altimeter_indicated-altitude-ft"));
            dashboard.Add("airspeed", list.IndexOf("airspeed-kt"));
            dashboard.Add("direction", list.IndexOf("indicated-heading-deg"));
            dashboard.Add("pitch", list.IndexOf("pitch-deg"));
            dashboard.Add("roll", list.IndexOf("roll-deg"));
            dashboard.Add("yaw", list.IndexOf("side-slip-deg"));
        }
        private double[,] getDataArray(List<double[]> rows, int colSize)
        {
            double[,] data = new double[rows.Count, colSize];
            for (int i = 0; i < data.GetLength(0); i++)
                for (int j = 0; j < data.GetLength(1); j++)
                    data[i, j] = (rows[i])[j];
            return data;
        }
        private void getJoystickFeatures()
        {
            Dictionary<string, int> joystick = DataModel.Instance.JoystickFeatures;
            List<string> list = DataModel.Instance.FeaturesNames;
            joystick.Add("aileron", list.IndexOf("aileron"));
            joystick.Add("elevator", list.IndexOf("elevator"));
            joystick.Add("throttle", list.IndexOf("throttle"));
            joystick.Add("rudder", list.IndexOf("rudder"));
        }
        private void saveMaxValues()
        {
            Dictionary<string, int> dashboard = DataModel.Instance.DashboardFeatures;
            double[,] data= DataModel.Instance.CsvData;
            if (dashboard["airspeed"] != -1)
                DataModel.Instance.MaxSpeed = Enumerable.Range(0, data.GetLength(0)).Select(x => data[x, dashboard["airspeed"]]).ToArray().Max();
        }

        private void createInputTxt(List<string> featuresNames, List<string> stringData)
        {
            try
            {
                if (File.Exists("input.txt"))
                {
                    File.Delete("input.txt");
                }
                using (StreamWriter sw = File.CreateText("input.txt"))
                {
                    sw.WriteLine("1");
                    //write learn data in input.txt
                    int i = 0;
                    for (; i < featuresNames.Count - 1; i++)
                        sw.Write(featuresNames[i] + ",");
                    sw.WriteLine(featuresNames[i]);
                    string line;
                    StreamReader file = new StreamReader(@csvNormalPath);
                    //check valid path 
                    while ((line = file.ReadLine()) != null)
                        sw.WriteLine(line);
                    sw.WriteLine("done");
                    file.Close();
                    //write test data in input.txt
                    i = 0;
                    for (; i < featuresNames.Count - 1; i++)
                        sw.Write(featuresNames[i] + ",");
                    sw.WriteLine(featuresNames[i]);
                    foreach (string s in stringData)
                        sw.WriteLine(s);
                    sw.WriteLine("done");
                    sw.WriteLine("3");
                    sw.WriteLine("4");
                    sw.WriteLine("6");
                    sw.Close();
                }
            }
            catch { }
        }
        private void parseXML(List<string> featuresNames)
        {
            try
            {
                XmlDocument xml = new XmlDocument();
                xml.Load(xmlPath);
                XmlNodeList nodes = xml.SelectNodes("//output");
                //List<string> featuresNames = DataModel.Instance.FeaturesNames;
                Dictionary<string, int> featNamesMap = new Dictionary<string, int>();
                foreach (XmlNode node in nodes)
                {
                    XmlNodeList aNodes = node.SelectNodes(".//name");
                    foreach (XmlNode aNode in aNodes)
                    {
                        string name = aNode.InnerText;
                        if (featNamesMap.ContainsKey(name))
                        {
                            int count = featNamesMap[name];
                            featuresNames.Add(name + count.ToString());
                            count++;
                            featNamesMap[name] = count;
                        }
                        else
                        {
                            featuresNames.Add(name);
                            featNamesMap[name] = 1;
                        }
                    }
                }
            }
            catch { }
        }
        private List<double[]> csvToListArray(List<string> stringData, string csvLocation)
        {
            List<double[]> dataList = new List<double[]>();
            try
            {
                //copy test csv data to local array.
                string[] lineData = { };
                string line;
                StreamReader file = new StreamReader(@csvLocation);
                //check valid path 
                while ((line = file.ReadLine()) != null)
                {
                    stringData.Add(line);
                    lineData = line.Split(',');
                    List<double> listDouble = new List<double>();
                    foreach (string s in lineData)
                    {
                        listDouble.Add(Convert.ToDouble(s));
                    }
                    dataList.Add(listDouble.ToArray());
                }
                file.Close();
                return dataList;
            }
            catch { }
            return dataList;
        }
        public bool isEverythingOK()
        {
            bool errorExist = false;
            string errorMessage="Error! Details:\n";
            if (!(isCorrectPath(CsvTestPath) && string.Equals(Path.GetExtension(CsvTestPath), ".csv", StringComparison.OrdinalIgnoreCase)))
            {
                errorMessage += "# Test CSV file wasn't found.\n";
                errorExist = true;
            }
            if (!(isCorrectPath(CsvNormalPath) && string.Equals(Path.GetExtension(CsvNormalPath), ".csv", StringComparison.OrdinalIgnoreCase)))
            {
                errorExist = true;
                errorMessage += "# Normal CSV file wasn't found.\n";
            }
            xmlPath = flightGearPath + "\\data\\Protocol\\playback_small.xml";
            if (!Directory.Exists(flightGearPath))
            {
                errorExist = true;
                errorMessage += "# FlightGear directory is incorrect.\n";
            }
            if (!(isCorrectPath(xmlPath) && string.Equals(Path.GetExtension(xmlPath), ".xml", StringComparison.OrdinalIgnoreCase)))
            {
                errorExist = true;
                errorMessage += "# playback__small.xml file wasn't found at\n" +
                                " ...\\data\\protocol\\ at FlightGear directory.\n";
            }
            if (errorExist)
            {
                Error = errorMessage;
                return false;
            }
            else
            {
                try
                {
                    DataModel.Instance.hardReset();
                    //Get XML Labels and save them in data model.
                    List<string> featuresNames = DataModel.Instance.FeaturesNames;
                    parseXML(featuresNames);

                    List<string> stringData = DataModel.Instance.StringData;
                    DataModel.Instance.CsvData = getDataArray(csvToListArray(stringData, csvTestPath), featuresNames.Count);
                    DataModel.Instance.MaxLines = stringData.Count;
                    stringData = new List<string>();
                    DataModel.Instance.LearnData = getDataArray(csvToListArray(stringData, csvNormalPath), featuresNames.Count);
                    
                    createInputTxt(featuresNames, DataModel.Instance.StringData);
                    getDashboardFeatures();
                    getJoystickFeatures();
                    saveMaxValues();
                    DataModel.Instance.SettingsOK = true;
                }
                catch
                { return false; }
            }
            return true;
        }
    }
}
