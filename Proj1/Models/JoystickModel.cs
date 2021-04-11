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
    /// <summary>
    ///  A JoystickModel class. the  model of user story 3
    /// </summary>
    class JoystickModel : INotifyPropertyChanged
    {
        //feilds
        // the data that need to display in this part
        private double throttle;
        private double rudder;
        // data for move the joistik
        private double aileronLeftRight;
        private double elevatorUpDown;
        private double[,] data;
        // the feature for this part string and index for the column of then in data array.
        private Dictionary<string, int> joystickFeatures;
        /// <summary>
        ///the constructor of JoystickModel.
        /// </summary>
        public JoystickModel()
        {
            data = null;
            joystickFeatures = DataModel.Instance.JoystickFeatures;
            Throttle = 0;
            Rudder = 0;
            // the start position of joistik
            MoveLeftRight = 32;
            MoveUpDown = 32;
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
        ///property of Throttle
        /// </summary>
        public double Throttle
        {
            get { return throttle; }
            set
            {
                throttle = value;
                NotifyPropertyChanged("Throttle");
            }
        }
        /// <summary>
        ///property of  Rudder
        /// </summary>
        public double Rudder
        {
            get { return rudder; }
            set
            {
                rudder = value;
                NotifyPropertyChanged("Rudder");
            }
        }
        /// <summary>
        ///property of MoveLeftRight
        /// </summary>
        public double MoveLeftRight
        {
            get { return aileronLeftRight; }
            set
            {
                aileronLeftRight = value;
                NotifyPropertyChanged("MoveLeftRight");
            }
        }
        /// <summary>
        ///property of MoveUpDown
        /// </summary>
        public double MoveUpDown
        {
            get { return elevatorUpDown; }
            set
            {
                elevatorUpDown = value;
                NotifyPropertyChanged("MoveUpDown");
            }
        }
        /// <summary>
        ///get the jostick data in the currentLine
        /// </summary>
        public void getCurrentLine()
        {
            data = DataModel.Instance.CsvData;
            if (joystickFeatures.Count == 0 || data == null)
                return;
            double temp;
            int line = DataModel.Instance.CurrentLine;
            if (joystickFeatures["aileron"] != -1)
            {
                temp = data[line, joystickFeatures["aileron"]];
                // updth the move of joistic according to data
                MoveLeftRight = 32 + temp * 38;
            }
            if (joystickFeatures["elevator"] != -1)
            {
                temp = data[line, joystickFeatures["elevator"]];
                // updth the move of joistic according to data
                MoveUpDown = 32 + temp * 38;
            }
            if (joystickFeatures["throttle"] != -1)
                Throttle = data[line, joystickFeatures["throttle"]];
            if (joystickFeatures["rudder"] != -1)
                Rudder = data[line, joystickFeatures["rudder"]];
        }
    }
}
