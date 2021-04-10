using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace Proj1.Models
{
    class DashboardModel:INotifyPropertyChanged
    {
        private double altimeter;
        private double airspeed;
        private double direction;
        private double pitch;
        private double roll;
        private double yaw;
        private double speedClockDeg;
        private double altiHundreds;
        private double altiThousands;
        private double compass;
        private double[,] data;
        private Dictionary<string,int> dashboardFeatures;
        public DashboardModel() { 
            dashboardFeatures = DataModel.Instance.DashboardFeatures;
            data = null;
            SpeedClockDeg = -43;
            AltSmall=0;
            AltBig=0;
            Compass = 0;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
        public double SpeedClockDeg
        {
            get { return speedClockDeg; }
            set
            {
                speedClockDeg = value;
                NotifyPropertyChanged("SpeedClockDeg");
            }
        }
        public double AltBig
        {
            get { return altiHundreds; }
            set {
                altiHundreds = value;
                NotifyPropertyChanged("AltBig");
            }
        }
        public double AltSmall
        {
            get { return altiThousands; }
            set {
                altiThousands = value;
                NotifyPropertyChanged("AltSmall");
            }
        }
        public double Altimeter
        {
            get { return altimeter; }
            set { altimeter = value;
                if (altimeter > 0)
                {
                    AltBig = 90 + ((altimeter % 1000) * 0.36);
                    AltSmall = 90 + altimeter * 0.036;
                }
                else
                { 
                    AltBig = 90;
                    AltSmall = 90;
                }    
                NotifyPropertyChanged("Altimeter");
            }
        }
        public double Compass
        {
            get { return compass; }
            set
            {
                compass = value;
                NotifyPropertyChanged("Compass");
            }
        }
        public double Airspeed
        {
            get { return airspeed; }
            set { airspeed = value;
                SpeedClockDeg = -43 + (airspeed / DataModel.Instance.MaxSpeed)*270;
                NotifyPropertyChanged("Airspeed");
            }
        }
        public double Direction
        {
            get { return direction; }
            set { direction = value;
                Compass = 90 + direction;
                NotifyPropertyChanged("Direction");
            }
        }
        public double Pitch
        {
            get { return pitch; }
            set { pitch = value;
                NotifyPropertyChanged("Pitch");
            }
        }
        public double Roll
        {
            get { return roll; }
            set { roll = value;
                NotifyPropertyChanged("Roll");
            }
        }
        public double Yaw
        {
            get { return yaw; }
            set { yaw = value;
                NotifyPropertyChanged("Yaw");
            }
        }
        public void getCurrentLine()
        {
            data = DataModel.Instance.CsvData;
            if (dashboardFeatures.Count == 0 || data == null)
                return;
            int line = DataModel.Instance.CurrentLine;
            if (dashboardFeatures["altimeter"]!=-1)
                Altimeter = data[line,dashboardFeatures["altimeter"]];
            if (dashboardFeatures["airspeed"] != -1)
                Airspeed = data[line,dashboardFeatures["airspeed"]];
            if (dashboardFeatures["direction"] != -1)
                Direction = data[line,dashboardFeatures["direction"]];
            if (dashboardFeatures["pitch"] != -1)
                Pitch = data[line,dashboardFeatures["pitch"]];
            if (dashboardFeatures["roll"] != -1)
                Roll = data[line,dashboardFeatures["roll"]];
            if (dashboardFeatures["yaw"] != -1)
                Yaw = data[line,dashboardFeatures["yaw"]];
        }
    }
}
