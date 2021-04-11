using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Threading.Tasks;
using Proj1.Models;

namespace Proj1.ViewModels
{
    /// <summary>
    ///  A PlayBarViewModel class. the veiw model of user story 2 
    /// </summary>
    class PlayBarViewModel : INotifyPropertyChanged
    {
        //feilds
        PlayBarModel pbmodel;
        DataModel dmodel;
        /// <summary>
        ///the constructor of PlayBarViewModel.
        /// </summary>
        public PlayBarViewModel(PlayBarModel pbm)
        {
            this.pbmodel = pbm;
            this.dmodel = DataModel.Instance;
            this.pbmodel.PropertyChanged += delegate (Object sender, PropertyChangedEventArgs e)
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
            // if current line change from data model when choose anomliy
            if (propName == "VM_CurrentChanged")
                pbmodel.CurrentLine = dmodel.CurrentLine;
            // if stop the conection need to rester all the informtion
            else if (propName == "VM_Restart")
                pbmodel.restart();
            // if pause the fly.
            else if (propName == "VM_ToPlay")
                pause();
            else if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
        /// <summary>
        ///property of the VM_MaxLines
        /// </summary>
        public int VM_MaxLines
        {
            get
            {
                // updth max line
                pbmodel.updateMaxLines();
                // the max line start in 0 
                return (pbmodel.MaxLines - 1);
            }
        }
        /// <summary>
        ///property of the VM_CurrentLine
        /// </summary>
        public float VM_CurrentLine
        {
            get { return pbmodel.CurrentLine; }
        }
        /// <summary>
        ///property of the VM_CurrentTime
        /// </summary>
        public string VM_CurrentTime
        {
            get { return pbmodel.CurrentTime.ToString(); }
        }
        /// <summary>
        ///property of the VM_PlaySpeed
        /// </summary>
        public string VM_PlaySpeed
        {
            get { return pbmodel.PlaySpeed.ToString(); }
        }
        /// <summary>
        ///updth speed
        /// </summary>
        public void setPlaySpeed(string speed)
        {
            pbmodel.setPlaySpeed(speed);
        }
        /// <summary>
        ///play the fly
        /// </summary>
        public void play()
        {
            pbmodel.play();
        }
        /// <summary>
        ///pause the fly
        /// </summary>
        public void pause()
        {
            pbmodel.ToPlay = false;
        }
        /// <summary>
        ///stop the fly
        /// </summary>
        public void stop()
        {
            pbmodel.ToPlay = false;
            pbmodel.CurrentLine = 0;
        }
        /// <summary>
        ///skip End the fly
        /// </summary>
        public void skipEnd()
        {
            pbmodel.CurrentLine = pbmodel.MaxLines - 1;
        }
        /// <summary>
        ///skip start the fly
        /// </summary>
        public void skipStart()
        {
            pbmodel.CurrentLine = 0;
        }
        /// <summary>
        ///skip back the fly
        /// </summary>
        public void skipBackward()
        {
            pbmodel.skip(-10);
        }
        /// <summary>
        ///skip forwad the fly
        /// </summary>
        public void skipForward()
        {
            pbmodel.skip(10);
        }
        /// <summary>
        ///set current line of video fly.
        /// </summary>
        public void setCurrentLine(int line)
        {
            pbmodel.CurrentLine = line;
        }
    }
}
