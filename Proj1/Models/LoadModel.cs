﻿using System;
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

    class LoadModel: INotifyPropertyChanged
    {
        

        //public static extern void getAnmoliesReport(byte[] x, byte[] y);
        private string dllPath;
        private string errorLabel;
        public LoadModel() {
            dllPath = null;
            errorLabel = "";
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
        private bool isCorrectPath(string path)
        {
            string filePath = path;
            return File.Exists(filePath);
        }
        public string Error
        {
            get { return errorLabel; }
            set
            {
                errorLabel = value;
                NotifyPropertyChanged("ErrorLabel");
            }
        }
/*        public string LoadPath
        {
            get { return dllPath; }
            set
            {
                dllPath = value;
                //NotifyPropertyChanged("LoadPath");
            }
        }*/
        public bool update(string path)
        {
/*            LoadPath = path;
*/            string extension = Path.GetExtension(path);
            if (isCorrectPath(path) && string.Equals(extension, ".dll", StringComparison.OrdinalIgnoreCase))
            {
                Error = "";
                createReport(path);
                return true;
            }
            Error = "Error: dll File Wasn't Found."; // TO DO - CHECK FOR DLL EXTENTION
            return false;
        }

        private void saveAnomalies()
        {//TRY CATCH
            string[] lineData = { };
            DataModel.Instance.Anomalies.Clear();
            DataModel.Instance.AnomaliesList.Clear();
            Dictionary<int, List<string>> anomalies = DataModel.Instance.Anomalies;
            //anomalies.Clear();
            List<string> anomaliesList = DataModel.Instance.AnomaliesList;
            //anomaliesList.Clear();
            string line, str="";
            string names = null;
            int num = -2;
            StreamReader file = new StreamReader("output.txt");
            while ((line = file.ReadLine()) != null && line != "ResultsStart.") ;
            while ((line = file.ReadLine()) != null && line != "ResultsEnd.")
            {
                lineData = line.Split('\t');
                int key = int.Parse(lineData[0]);
                if (!anomalies.ContainsKey(key))
                    anomalies.Add(key, new List<string>());

                if (Math.Abs(key-num)>1 || names ==null|| names!=lineData[1])
                {
                    if (num != -2)
                    {
                        str += TimeSpan.FromSeconds(num / 10.0).ToString() + " " + names;
                        anomaliesList.Add(str);
                        str = "";
                    }
                    str += TimeSpan.FromSeconds(key/10.0).ToString() + " - ";
                }
                num = key;
                names = lineData[1];
                anomalies[key].Add(lineData[1]);
            }
            if (num != -2)
            {
                str += TimeSpan.FromSeconds(num / 10) + " " + names;
                anomaliesList.Add(str);
                str = "";
            }
            file.Close();
        }
        private void createReport(string path)
        {
            try
            {
                Assembly assembly = Assembly.LoadFile(path);
                Type type = assembly.GetType("AlgorithmNS.Algorithm");
                object obj = Activator.CreateInstance(type);
                MethodInfo method = type.GetMethod("getAnomaliesReport");
                method.Invoke(obj, null);
                saveAnomalies();
            }
            catch // CHECK WHICH EX
            {
                Console.WriteLine("Error with dll"); //MAKE LABEL
            }
        }
    }
}
