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
            this.dmodel.PropertyChangedDash += delegate (Object sender, PropertyChangedEventArgs e)
            {
                
                NotifyPropertyChanged("VM_" + e.PropertyName);
            };
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(string propName)
        {
            if (propName== "VM_CurrentUpdated")
                dashmodel.getCurrentLine();
            else if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }

        public double VM_Altimeter
        {
            get { return dashmodel.Altimeter; }
        }
        public double VM_Airspeed
        {
            get { return dashmodel.Airspeed; }
        }
        public double VM_Direction
        {
            get { return dashmodel.Direction; }
        }
        public double VM_Pitch
        {
            get { return dashmodel.Pitch; }
        }
        public double VM_Roll
        {
            get { return dashmodel.Roll; }
        }
        public double VM_Yaw
        {
            get { return dashmodel.Yaw; }
        }


    }
}
