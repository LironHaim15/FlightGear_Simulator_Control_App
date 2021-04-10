using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Proj1.Models;
using System.ComponentModel;

namespace Proj1.ViewModels
{
    /// <summary>
    ///  A DashboardViewModel  class. user story 5
    /// </summary>
    /// <remarks>
    /// that conected between veiw and model of user story 5.
    /// </remarks>
    class DashboardViewModel :INotifyPropertyChanged
    {
        //feilds
        private DashboardModel dashmodel;
        private DataModel dmodel;
        /// <summary>
        ///the constructor of DashboardViewMode.
        /// </summary>
        public DashboardViewModel(DashboardModel dm)
        {
            this.dashmodel = dm;
            this.dmodel = DataModel.Instance;
            // add fuction to models
            this.dashmodel.PropertyChanged += delegate (Object sender, PropertyChangedEventArgs e)
            {
                NotifyPropertyChanged("VM_" + e.PropertyName);
            };
            this.dmodel.PropertyChanged += delegate (Object sender, PropertyChangedEventArgs e)
            {
                NotifyPropertyChanged("VM_" + e.PropertyName);
            };
        }
        public event PropertyChangedEventHandler PropertyChanged;
        /// <summary>
        ///mvvm notify of chenges to veiw from model.
        /// </summary>
        public void NotifyPropertyChanged(string propName)
        {
            // if current line change need to updth the data of dashbord.
            if (propName== "VM_CurrentUpdate")
                dashmodel.getCurrentLine();
            else if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }

        /// <summary>
        ///property of VM_Altimeter 
        /// </summary>
        public string VM_Altimeter
        {
            get { return string.Format("{0:0.000}", dashmodel.Altimeter) + " ft."; }
        }
        /// <summary>
        ///property of VM_Airspeed
        /// </summary>
        public string VM_Airspeed
        {
            get { return string.Format("{0:0.000}", dashmodel.Airspeed) + " kt."; }
        }
        /// <summary>
        ///property of VM_Direction
        /// </summary>
        public string VM_Direction
        {
            get { return string.Format("{0:0.0000}", dashmodel.Direction) + "°"; }
        }
        /// <summary>
        ///property of VM_Pitch
        /// </summary>
        public string VM_Pitch
        {
            get { return string.Format("{0:0.0000}", dashmodel.Pitch) + "°"; }
        }
        /// <summary>
        ///property of VM_Roll
        /// </summary>
        public string VM_Roll
        {
            get { return string.Format("{0:0.0000}", dashmodel.Roll) + "°"; }
        }
        /// <summary>
        ///property of VM_Yaw
        /// </summary>
        public string VM_Yaw
        {
            get { return string.Format("{0:0.0000}", dashmodel.Yaw) + "°"; }
        }
        /// <summary>
        ///property of VM_SpeedClockDeg
        /// </summary>
        public double VM_SpeedClockDeg
        {
            get { return dashmodel.SpeedClockDeg; }
        }
        /// <summary>
        ///property of VM_AltSmall
        /// </summary>
        public double VM_AltSmall
        {
            get { return dashmodel.AltSmall; }
        }
        /// <summary>
        ///property of VM_AltBig
        /// </summary>
        public double VM_AltBig
        {
            get { return dashmodel.AltBig; }
        }
        /// <summary>
        ///property of VM_Compass
        /// </summary>
        public double VM_Compass
        {
            get { return dashmodel.Compass; }
        }
    }
}
