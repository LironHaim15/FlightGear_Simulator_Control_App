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
    ///  A LoadViewModel  class. user story 9
    /// </summary>
    /// <remarks>
    /// that conected between veiw and model of user story 9.
    /// </remarks>
    class LoadViewModel : INotifyPropertyChanged
    {
        //feilds
        private LoadModel lmodel;
        private DataModel dmodel;
        /// <summary>
        ///the constructor of  LoadViewModel.
        /// </summary>
        public LoadViewModel(LoadModel lm)
        {
            this.lmodel = lm;
            this.dmodel = DataModel.Instance;
            lmodel.PropertyChanged += delegate (Object sender, PropertyChangedEventArgs e)
            {
                NotifyPropertyChanged("VM_" + e.PropertyName);
            };
        }
        public event PropertyChangedEventHandler PropertyChanged;
        /// <summary>
        ///mvvm notify of changes to veiw from model.
        /// </summary>
        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
        /// <summary>
        ///property of  VM_ErrorLabel
        /// </summary>
        public string VM_ErrorLabel
        {
            get { return lmodel.Error; }
        }
        /// <summary>
        ///load the dll file according to path if its ok return true .else false
        /// </summary>
        public bool load(string path)
        {
            // the model Succeeded to load dll
            if (lmodel.update(path))
            {
                dmodel.DllLoaded = true;
                return true;
            }
            return false;
        }
    }
}
