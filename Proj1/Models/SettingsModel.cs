using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
using System.Text;
using System.Xml;
using System.Threading.Tasks;
using System.Collections.Generic;
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
       
        public SettingsModel() { DataModel.Instance.SettingsOK = false; }

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

        public void updatePath(string VM_csvNormalPath, string VM_csvTestPath, string VM_flightGearPath, string VM_xmlPath)
        {
            csvNormalPath = VM_csvNormalPath;
            csvTestPath = VM_csvTestPath;
            flightGearPath = VM_flightGearPath;
            xmlPath = VM_xmlPath;
        }
        
        private bool isCorrectPath(string path)
        {
            string filePath = path;
            return File.Exists(filePath);
        }

        private void getDashboardFeatures()
        {   //
            // CHECK NAMES IN XML !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            //
            Dictionary<string, int> dashboard = DataModel.Instance.DashboardFeatures;
            List<string> list = DataModel.Instance.FeaturesNames;
            dashboard.Add("altimeter",list.IndexOf("altimeter_indicated-altitude-ft"));
            dashboard.Add("airspeed", list.IndexOf("airspeed-kt"));
            dashboard.Add("direction", list.IndexOf("indicated-heading-deg"));
            dashboard.Add("pitch", list.IndexOf("pitch-deg"));
            dashboard.Add("roll", list.IndexOf("roll-deg"));
            dashboard.Add("yaw", list.IndexOf("side-slip-deg"));
        }
        private void getJoystickFeatures()
        {   //
            // CHECK NAMES IN XML !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            //
            Dictionary<string, int> joystick = DataModel.Instance.JoystickFeatures;
            List<string> list = DataModel.Instance.FeaturesNames;
            joystick.Add("aileron", list.IndexOf("aileron"));
            joystick.Add("elevator", list.IndexOf("elevator"));
            joystick.Add("throttle", list.IndexOf("throttle"));
            joystick.Add("rudder", list.IndexOf("rudder"));
/*            foreach (var item in joystick)
            {
                Console.WriteLine(item);
            }*/
        }
        public bool isEverythingOK()
        {
            bool errorExist = false;
            string errorMessage="ERROR with the following paths:\n";
            if (!isCorrectPath(csvTestPath))
            {
                errorMessage += "Test CSV\n";
                errorExist = true;
                // notify and change lable.
            }
            if (!isCorrectPath(csvNormalPath))
            {
                errorExist = true;
                errorMessage += "Normal CSV\n";
            }
            if (!isCorrectPath(xmlPath))
            {
                errorExist = true;
                errorMessage += "XML Location\n";
            }
            if (!Directory.Exists(flightGearPath))
            {
                errorExist = true;
                errorMessage += "FlightGear Location\n";
            }
            string xmlDestPath = flightGearPath + "\\data\\Protocol";
            if (!Directory.Exists(xmlDestPath))
            {
                errorExist = true;
                errorMessage += "FlightGear Location (Protol Folder)\n";
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
                    //Copy XML file to FG Protocols folder
                    File.Copy(xmlPath, xmlDestPath + "\\playback_small.xml", true);

                    //Get XML Labels and save them in data model.
                    XmlDocument xml = new XmlDocument();
                    xmlDestPath += "\\playback_small.xml";
                    xml.Load(xmlDestPath);
                    XmlNodeList nodes = xml.SelectNodes("//output");
                    List<string> featuresNames = DataModel.Instance.FeaturesNames;
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

                    //copy test csv data to local array.
                    string csvLocation = csvTestPath;

                    List<double[]> dataList = new List<double[]>();
                    string[] lineData;
                    List<string> stringData = DataModel.Instance.StringData;
                    string line;
                    int amount = 0;
                    System.IO.StreamReader file = new System.IO.StreamReader(@csvLocation);
                    //check valid path 
                    while ((line = file.ReadLine()) != null)
                    {
                        stringData.Add(line);
                        lineData = line.Split(',');
                        amount++;
                        List<double> listDouble = new List<double>();
                        foreach (string s in lineData)
                        {
                            listDouble.Add(Convert.ToDouble(s));
                        }
                        dataList.Add(listDouble.ToArray());
                    }
                    DataModel.Instance.CsvData = dataList.ToArray();
                    DataModel.Instance.MaxLines = stringData.Count;
                    getDashboardFeatures();
                    getJoystickFeatures();
                    DataModel.Instance.SettingsOK = true;
                }
                catch (IOException iox)
                {
                    Console.WriteLine(iox.Message); //CHANGE - file could not read.
                    return false;
                }

                //learn data. (requires DLL)
            }
            //    D:\Desktop\AdvancedProgramming2\reg_flight.csv
            //    D:\Desktop\AdvancedProgramming2\playback_small.xml
            //    D:\Program Files\FlightGear 2020.3.6


            
            return true;
        }

    }
}
