using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Proj1.Models;
using System.ComponentModel;

namespace Proj1.VeiwModels
{
    class JoystickViewModel:INotifyPropertyChanged
    {
        private JoystickModel jmodel;
        private DataModel dmodel;
        public JoystickViewModel(JoystickModel jm)
        {
            this.jmodel = jm;
            this.dmodel = DataModel.Instance;
            this.jmodel.PropertyChanged += delegate (Object sender, PropertyChangedEventArgs e)
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
            if (propName == "VM_CurrentUpdated")
                jmodel.getCurrentLine();
            else if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }

        public double VM_MoveLeftRight
        {
            get { return jmodel.MoveLeftRight; }
        }
        public double VM_MoveUpDown
        {
            get { return jmodel.MoveUpDown; }
        }
        public double VM_Throttle
        {
            get { return jmodel.Throttle; }
        }
        public double VM_Rudder
        {
            get { return jmodel.Rudder; }
        }
    }
}
