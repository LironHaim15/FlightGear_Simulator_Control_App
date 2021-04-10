using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Proj1.Models;
using System.Runtime.InteropServices;
using System.ComponentModel;
using System.IO;
using System.Runtime;
using System.Reflection;

namespace Proj1.Models
{
    /// <summary>
    ///  A LoadModel class. the  model of user story9
    /// </summary>
    class LoadModel : INotifyPropertyChanged
    {
        //feilds
        private string errorLabel;
        public LoadModel() {
            errorLabel = "";
        }
        public event PropertyChangedEventHandler PropertyChanged;
        /// <summary>
        ///the constructor of  NotifyPropertyChanged.
        /// </summary>
        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
        /// <summary>
        ///check if the path is ok if is return true ,else return false.
        /// </summary>
        private bool isCorrectPath(string path)
        {
            string filePath = path;
            return File.Exists(filePath);
        }
        /// <summary>
        ///property of Error
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
        ///if Succeeded to updth and run dll return true ,else return false
        /// </summary>
        public bool update(string path)
        {
            string extension = Path.GetExtension(path);
            // chack if the path exist and is dll file
            if (isCorrectPath(path) && string.Equals(extension, ".dll", StringComparison.OrdinalIgnoreCase))
            {
                Error = "";
                // if dll dont work send message to user
                if (!createReport(path))
                {
                    Error = "Error with dll loading,";
                    return false;
                }
                return true;
            }
            // if is no dll file. send message to user
            Error = "Error: dll File Wasn't Found.";
            return false;
        }

        /// <summary>
        /// from the output txt take the anomalis
        /// </summary>
        private void saveAnomalies()
        {
            try
            {
                string[] lineData = { };
                // updth the anomlies
                DataModel.Instance.Anomalies.Clear();
                DataModel.Instance.AnomaliesList.Clear();
                Dictionary<int, List<string>> anomalies = DataModel.Instance.Anomalies;
                List<string> anomaliesList = DataModel.Instance.AnomaliesList;
                string line, str = "";
                string names = null;
                int num = -2;
                StreamReader file = new StreamReader("output.txt");
                // anomalis - have all the anomliy according to line
                // anomaliesList - have all the anomliy dicreption that Arranged by time 
                //sequences start time and end time
                //read the anomleis from the output.txt
                while ((line = file.ReadLine()) != null && line != "ResultsStart.") ;
                while ((line = file.ReadLine()) != null && line != "ResultsEnd.")
                {
                    lineData = line.Split('\t');
                    // the line of anomliy
                    int key = int.Parse(lineData[0]);
                    // if its new line of anomliy
                    if (!anomalies.ContainsKey(key))
                        anomalies.Add(key, new List<string>());
                    // if its not next line or is other anomliy or its the first anomliy in the file
                    if (Math.Abs(key - num) > 1 || names == null || names != lineData[1])
                    {
                        // finsh the string that discrive the previus anomliy
                        if (num != -2)
                        {
                            str += TimeSpan.FromSeconds(num / 10.0).ToString() + " " + names;
                            anomaliesList.Add(str);
                            str = "";
                        }
                        // start the start of new anomliy
                        str += TimeSpan.FromSeconds(key / 10.0).ToString() + " - ";
                    }
                    //save the data for the next itertion
                    num = key;
                    names = lineData[1];
                    // add anomliy to list
                    anomalies[key].Add(lineData[1]);
                }
                // finsh the last anomliy description
                if (num != -2)
                {
                    // time is line/10 because Transfers to flight 10 lines per second.
                    str += TimeSpan.FromSeconds(num / 10) + " " + names;
                    anomaliesList.Add(str);
                    str = "";
                }
                file.Close();
            }
            catch { }
        }
        /// <summary>
        /// create the report acording the algo dll. if its success return true ,otherwish false.
        /// </summary>
        private bool createReport(string path)
        {
            try
            {
                // try to use in dll algo
                Assembly assembly = Assembly.LoadFile(path);
                Type type = assembly.GetType("AlgorithmNS.Algorithm");
                object obj = Activator.CreateInstance(type);
                MethodInfo method = type.GetMethod("getAnomaliesReport");
                method.Invoke(obj, null);
                // save the anomliy from output.txt that create in dll algo.
                saveAnomalies();
                return true;
            }
            // not sucess to run the dll algo
            catch { return false; }           
        }
    }
}
