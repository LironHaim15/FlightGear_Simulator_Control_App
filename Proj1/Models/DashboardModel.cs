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
        private double[][] data;
        private Dictionary<string,int> dashboardFeatures;
        public DashboardModel() { 
            dashboardFeatures = DataModel.Instance.DashboardFeatures;
            data = null;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
        public double Altimeter
        {
            get { return altimeter; }
            set { altimeter = value;
                NotifyPropertyChanged("Altimeter");
            }
        }
        public double Airspeed
        {
            get { return airspeed; }
            set { airspeed = value;
                NotifyPropertyChanged("Airspeed");
            }
        }
        public double Direction
        {
            get { return direction; }
            set { direction = value;
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
            if (dashboardFeatures.Count == 0 || data ==null)
            {
                data = DataModel.Instance.CsvData;
                return;
            }
            int line = DataModel.Instance.CurrentLine;
            if (dashboardFeatures["altimeter"]!=-1)
                Altimeter = data[line][dashboardFeatures["altimeter"]];
            if (dashboardFeatures["airspeed"] != -1)
                Airspeed = data[line][dashboardFeatures["airspeed"]];
            if (dashboardFeatures["direction"] != -1)
                Direction = data[line][dashboardFeatures["direction"]];
            if (dashboardFeatures["pitch"] != -1)
                Pitch = data[line][dashboardFeatures["pitch"]];
            if (dashboardFeatures["roll"] != -1)
                Roll = data[line][dashboardFeatures["roll"]];
            if (dashboardFeatures["yaw"] != -1)
                Yaw = data[line][dashboardFeatures["yaw"]];
        }
    }
}
