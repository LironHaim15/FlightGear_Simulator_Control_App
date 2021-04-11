using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Proj1.Models;
using System.ComponentModel;

namespace Proj1.VeiwModels
{
    /// <summary>
    ///  A JoystickViewModel  class. user story 3
    /// </summary>
    /// <remarks>
    /// that conected between veiw and model of user story 3.
    /// </remarks>
    class JoystickViewModel : INotifyPropertyChanged
    {
        //feilds
        private JoystickModel jmodel;
        private DataModel dmodel;
        /// <summary>
        ///the constructor of  JoystickViewModel.
        /// </summary>
        public JoystickViewModel(JoystickModel jm)
        {
            this.jmodel = jm;
            this.dmodel = DataModel.Instance;
            // add fuction to models
            this.jmodel.PropertyChanged += delegate (Object sender, PropertyChangedEventArgs e)
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
            if (propName == "VM_CurrentUpdate")
                jmodel.getCurrentLine();
            else if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
        /// <summary>
        ///property of  VM_MoveLeftRight
        /// </summary>
        public double VM_MoveLeftRight
        {
            get { return jmodel.MoveLeftRight; }
        }
        /// <summary>
        ///property of  VM_MoveUpDown
        /// </summary>
        public double VM_MoveUpDown
        {
            get { return jmodel.MoveUpDown; }
        }
        /// <summary>
        ///property of VM_Throttle
        /// </summary>
        public double VM_Throttle
        {
            get { return jmodel.Throttle; }
        }
        /// <summary>
        ///property of VM_Rudder
        /// </summary>
        public double VM_Rudder
        {
            get { return jmodel.Rudder; }
        }
    }
}
