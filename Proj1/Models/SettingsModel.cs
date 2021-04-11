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
       
        /// <summary>
        /// constructor. 
        /// </summary>
        public SettingsModel() { DataModel.Instance.SettingsOK = false; }

        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
        /// <summary>
        /// Get and set the error string for the error label.
        /// </summary>
       public string Error
        {
            get { return errorLabel; }
            set { errorLabel = value;
                NotifyPropertyChanged("ErrorLabel");
            }
        }
        /// <summary>
        /// Get and set the path string for the learning csv.
        /// </summary>
        public string CsvNormalPath
        {
            get { return csvNormalPath; }
            set
            {
                csvNormalPath = value;
                NotifyPropertyChanged("CsvNormalPath");
            }
        }
        /// <summary>
        /// Get and set the path string for the test csv.
        /// </summary>
        public string CsvTestPath
        {
            get { return csvTestPath; }
            set
            {
                csvTestPath = value;
                NotifyPropertyChanged("CsvTestPath");
            }
        }
        /// <summary>
        /// Get and set the path string for the FlightGear directory.
        /// </summary>
        public string FlightGearPath
        {
            get { return flightGearPath; }
            set
            {
                flightGearPath = value;
                NotifyPropertyChanged("FlightGearPath");
            }
        }
        /// <summary>
        /// returns true if file exists in 'path'.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        private bool isCorrectPath(string path)
        {
            string filePath = path;
            return File.Exists(filePath);
        }

        /// <summary>
        /// opens up Windows openFileDialog for the user to browse for the diseired paths.
        /// makes sure the of chosen file is 'type'.
        /// </summary>
        /// <param name="type"></param>
        public void browse(string type)
        {   
            Microsoft.Win32.OpenFileDialog openFileDlg = new Microsoft.Win32.OpenFileDialog();
            Nullable<bool> result = openFileDlg.ShowDialog();
            if (result == true)
            {
                if (type == "CsvNormalPath")
                    CsvNormalPath = Path.GetFullPath(openFileDlg.FileName);
                else if (type == "CsvTestPath")
                    CsvTestPath = Path.GetFullPath(openFileDlg.FileName);
            }
        }
        /// <summary>
        /// search for the dashboard features indexes in the FeaturesList, and save them in a dictionary 
        /// with their names as keys.
        /// </summary>
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
        /// <summary>
        /// save the test data in a 2D array. getting a list of double arrays. each array is a data line.
        /// </summary>
        /// <param name="rows">list of double arrays. each array is a data line. </param>
        /// <param name="colSize"> amount of featues </param>
        /// <returns></returns>
        private double[,] getDataArray(List<double[]> rows, int colSize)
        {
            double[,] data = new double[rows.Count, colSize];
            for (int i = 0; i < data.GetLength(0); i++)
                for (int j = 0; j < data.GetLength(1); j++)
                    data[i, j] = (rows[i])[j];
            return data;
        }
        /// <summary>
        /// search for the joystick features indexes in the FeaturesList, and save them in a dictionary 
        /// with their names as keys.
        /// </summary>
        private void getJoystickFeatures()
        {
            Dictionary<string, int> joystick = DataModel.Instance.JoystickFeatures;
            List<string> list = DataModel.Instance.FeaturesNames;
            joystick.Add("aileron", list.IndexOf("aileron"));
            joystick.Add("elevator", list.IndexOf("elevator"));
            joystick.Add("throttle", list.IndexOf("throttle"));
            joystick.Add("rudder", list.IndexOf("rudder"));
        }
        /// <summary>
        /// get the max speed in the data. used for displaying speed meter on the dashboard.
        /// </summary>
        private void saveMaxValues()
        {
            Dictionary<string, int> dashboard = DataModel.Instance.DashboardFeatures;
            double[,] data= DataModel.Instance.CsvData;
            if (dashboard["airspeed"] != -1)
                DataModel.Instance.MaxSpeed = Enumerable.Range(0, data.GetLength(0)).Select(x => data[x, dashboard["airspeed"]]).ToArray().Max();
        }

        /// <summary>
        /// create a text file for with all the data, for the anomalies detector to analyze and get anomalies.
        /// file name is 'input.txt' and it is saved in the main folder.
        /// </summary>
        /// <param name="featuresNames"></param>
        /// <param name="stringData"></param>
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
        /// <summary>
        /// parse the names of features from the xml file and saves it in list ('featuresNames').
        /// </summary>
        /// <param name="featuresNames"></param>
        private void parseXML(List<string> featuresNames)
        {
            try
            {
                XmlDocument xml = new XmlDocument();
                xml.Load(xmlPath);
                // look for 'output' tag and get its content.
                XmlNodeList nodes = xml.SelectNodes("//output");
                Dictionary<string, int> featNamesMap = new Dictionary<string, int>();
                foreach (XmlNode node in nodes)
                {
                    //for each node in 'output', look for 'name' tag and get its content.
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
        /// <summary>
        /// read csv file and return the data in a list of double arrays. each array represents a data line.
        /// </summary>
        /// <param name="stringData">list of strings, where the csv data lines will be saved.</param>
        /// <param name="csvLocation">csv file path</param>
        /// <returns></returns>
        private List<double[]> csvToListArray(List<string> stringData, string csvLocation)
        {
            List<double[]> dataList = new List<double[]>();
            try
            {
                //copy test csv data to local array.
                string[] lineData = { };
                string line;
                StreamReader file = new StreamReader(@csvLocation);
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
        /// <summary>
        /// is called whem pressing the 'Continue' button on the Settings window.
        /// checks if all the paths are correct and directs to csv files. if the playback_small.xml file is in the FG currect folder,
        /// and call for the functions that parse and store the data. if error occurs along the way it returs flase, otherwise returns true.
        /// </summary>
        /// <returns>if error occurs along the way it returs flase, otherwise returns true.</returns>
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
