using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Proj1.Models;
using System.ComponentModel;

namespace Proj1.ViewModels
{
    class DashboardViewModel:INotifyPropertyChanged
    {
        private DashboardModel dashmodel;
        private DataModel dmodel;
        public DashboardViewModel(DashboardModel dm)
        {
            this.dashmodel = dm;
            this.dmodel = DataModel.Instance;
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
        public void NotifyPropertyChanged(string propName)
        {
            if (propName== "VM_CurrentUpdate")
                dashmodel.getCurrentLine();
            else if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }

        public string VM_Altimeter
        {
            get { return string.Format("{0:0.000}", dashmodel.Altimeter) + " ft."; }
        }
        public string VM_Airspeed
        {
            get { return string.Format("{0:0.000}", dashmodel.Airspeed) +" kt."; }
        }
        public string VM_Direction
        {
            get { return string.Format("{0:0.0000}", dashmodel.Direction) + "°"; }
        }
        public string VM_Pitch
        {
            get { return string.Format("{0:0.0000}", dashmodel.Pitch) + "°"; }
        }
        public string VM_Roll
        {
            get { return string.Format("{0:0.0000}", dashmodel.Roll) + "°"; }
        }
        public string VM_Yaw
        {
            get { return string.Format("{0:0.0000}", dashmodel.Yaw) + "°"; }
        }
        public double VM_SpeedClockDeg
        {
            get { return dashmodel.SpeedClockDeg; }
        }
        public double VM_AltSmall
        {
            get { return dashmodel.AltSmall; }
        }
        public double VM_AltBig
        {
            get { return dashmodel.AltBig; }
        }
        public double VM_Compass
        {
            get { return dashmodel.Compass; }
        }
    }
}
