using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Proj1.Models;
using System.ComponentModel;
using System.IO;

namespace Proj1.Models
{
    class LoadModel: INotifyPropertyChanged
    {
        private string dllPath;
        private string errorLabel;
        private bool isValid;
        public LoadModel() {
            dllPath = null;
            errorLabel = "";
            isValid = false;
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
        public string LoadPath
        {
            get { return dllPath; }
            set
            {
                dllPath = value;
                //NotifyPropertyChanged("LoadPath");
            }
        }
        public bool update(string path)
        {
            LoadPath = path;
            string extension = Path.GetExtension(path);
            if (isCorrectPath(path) && string.Equals(extension, ".dll", StringComparison.OrdinalIgnoreCase))
            {
                Error = "";
                return true;
            }
            Error = "Error: dll File Wasn't Found."; // TO DO - CHECK FOR DLL EXTENTION
            return false;
        }
    }
}
