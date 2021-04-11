using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace Proj1.Models
{
    /// <summary>
    ///  A DashboardModel class. the  model of user story 5
    /// </summary>
    class DashboardModel : INotifyPropertyChanged
    {
        //feilds
        //the data need to display user story 5
        private double altimeter;
        private double airspeed;
        private double direction;
        private double pitch;
        private double roll;
        private double yaw;
        // for the watches that display the data  -angles for display
        private double speedClockDeg;
        private double altiHundreds;
        private double altiThousands;
        //angle compass to disply altitude
        private double compass;
        private double[,] data;
        private Dictionary<string, int> dashboardFeatures;
        /// <summary>
        ///the constructor of DashboardModel.
        /// </summary>
        public DashboardModel()
        {
            dashboardFeatures = DataModel.Instance.DashboardFeatures;
            data = null;
            // the start angle of compass and watches
            SpeedClockDeg = -43;
            AltSmall = 0;
            AltBig = 0;
            Compass = 0;
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
        /// <summary>
        ///property of SpeedClockDeg
        /// </summary>
        public double SpeedClockDeg
        {
            get { return speedClockDeg; }
            set
            {
                speedClockDeg = value;
                NotifyPropertyChanged("SpeedClockDeg");
            }
        }
        /// <summary>
        ///property of AltBig
        /// </summary>
        public double AltBig
        {
            get { return altiHundreds; }
            set
            {
                altiHundreds = value;
                NotifyPropertyChanged("AltBig");
            }
        }
        /// <summary>
        ///property of AltSmall
        /// </summary>
        public double AltSmall
        {
            get { return altiThousands; }
            set
            {
                altiThousands = value;
                NotifyPropertyChanged("AltSmall");
            }
        }
        /// <summary>
        ///property of Altimeter
        /// </summary>
        public double Altimeter
        {
            get { return altimeter; }
            set
            {
                altimeter = value;
                // Adjusts the dial according to values
                if (altimeter > 0)
                {
                    // 36 dil its pass a line in Watch.
                    //The residual angle 
                    AltBig = 90 + ((altimeter % 1000) * 0.36);
                    //thousand - dial
                    AltSmall = 90 + altimeter * 0.036;
                }
                else
                {
                    // the start dail
                    AltBig = 90;
                    AltSmall = 90;
                }
                NotifyPropertyChanged("Altimeter");
            }
        }
        /// <summary>
        ///property of Compass
        /// </summary>
        public double Compass
        {
            get { return compass; }
            set
            {
                compass = value;
                NotifyPropertyChanged("Compass");
            }
        }
        /// <summary>
        ///property of Airspeed
        /// </summary>
        public double Airspeed
        {
            get { return airspeed; }
            set
            {
                airspeed = value;
                SpeedClockDeg = -43 + (airspeed / DataModel.Instance.MaxSpeed) * 270;
                NotifyPropertyChanged("Airspeed");
            }
        }
        /// <summary>
        ///property of Direction
        /// </summary>
        public double Direction
        {
            get { return direction; }
            set
            {
                direction = value;
                Compass = 90 + direction;
                NotifyPropertyChanged("Direction");
            }
        }
        /// <summary>
        ///property of Pitch
        /// </summary>
        public double Pitch
        {
            get { return pitch; }
            set
            {
                pitch = value;
                NotifyPropertyChanged("Pitch");
            }
        }
        /// <summary>
        ///property of Roll
        /// </summary>
        public double Roll
        {
            get { return roll; }
            set
            {
                roll = value;
                NotifyPropertyChanged("Roll");
            }
        }
        /// <summary>
        ///property of Yaw
        /// </summary>
        public double Yaw
        {
            get { return yaw; }
            set
            {
                yaw = value;
                NotifyPropertyChanged("Yaw");
            }
        }
        /// <summary>
        ///get the dashbored data in the currentLine
        /// </summary>
        public void getCurrentLine()
        {
            data = DataModel.Instance.CsvData;
            if (dashboardFeatures.Count == 0 || data == null)
                return;
            int line = DataModel.Instance.CurrentLine;
            //update the data according the line.
            if (dashboardFeatures["altimeter"] != -1)
                Altimeter = data[line, dashboardFeatures["altimeter"]];
            if (dashboardFeatures["airspeed"] != -1)
                Airspeed = data[line, dashboardFeatures["airspeed"]];
            if (dashboardFeatures["direction"] != -1)
                Direction = data[line, dashboardFeatures["direction"]];
            if (dashboardFeatures["pitch"] != -1)
                Pitch = data[line, dashboardFeatures["pitch"]];
            if (dashboardFeatures["roll"] != -1)
                Roll = data[line, dashboardFeatures["roll"]];
            if (dashboardFeatures["yaw"] != -1)
                Yaw = data[line, dashboardFeatures["yaw"]];
        }
    }
}
