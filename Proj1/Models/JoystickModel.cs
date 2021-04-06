using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Proj1.ViewModels;
using Proj1.Models;
using System.ComponentModel;


namespace Proj1.Models
{
    class JoystickModel:INotifyPropertyChanged
    {
        private double throttle;
        private double rudder;
        private double aileronLeftRight;
        private double elevatorUpDown;
        private double[,] data;
        private Dictionary<string, int> joystickFeatures;
        public JoystickModel() {
            data = null;
            joystickFeatures = DataModel.Instance.JoystickFeatures;
            Throttle = 0;
            Rudder = 0;
            MoveLeftRight = 32;
            MoveUpDown = 32;
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
        public double Throttle
        {
            get { return throttle; }
            set { throttle = value;
                NotifyPropertyChanged("Throttle");
            }
        }
        public double Rudder
        {
            get { return rudder; }
            set { rudder = value;
                NotifyPropertyChanged("Rudder");
            }
        }
        public double MoveLeftRight
        {
            get { return aileronLeftRight; }
            set { aileronLeftRight = value;
                NotifyPropertyChanged("MoveLeftRight");
            }
        }
        public double MoveUpDown
        {
            get { return elevatorUpDown; }
            set { elevatorUpDown = value;
                NotifyPropertyChanged("MoveUpDown");
            }
        }
        public void getCurrentLine()
        {
            /*if (joystickFeatures.Count == 0 || data == null || data!=)
            {
                data = DataModel.Instance.CsvData;
                return;
            }*/
            data = DataModel.Instance.CsvData;
            if (joystickFeatures.Count == 0 || data == null)
                return;
            double temp;
            int line = DataModel.Instance.CurrentLine;
            if (joystickFeatures["aileron"] != -1)
            {
                temp = data[line,joystickFeatures["aileron"]];
                MoveLeftRight = 32 + temp * 38;
            }
            if (joystickFeatures["elevator"] != -1)
            {
                temp = data[line,joystickFeatures["elevator"]];
                MoveUpDown = 32 + temp * 38;
            }
            if (joystickFeatures["throttle"] != -1)
                Throttle = data[line,joystickFeatures["throttle"]];
            if (joystickFeatures["rudder"] != -1)
                Rudder = data[line,joystickFeatures["rudder"]];
        }
    }
}
