using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Threading.Tasks;
using Proj1.Models;

namespace Proj1.ViewModels
{
    class PlayBarViewModel : INotifyPropertyChanged
    {
        PlayBarModel pbmodel;
        DataModel dmodel;
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
        public void NotifyPropertyChanged(string propName)
        {
            if (propName == "VM_CurrentChanged")
                pbmodel.CurrentLine = dmodel.CurrentLine;
            if (propName == "VM_Restart")
                pbmodel.restart();
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }

        public int VM_MaxLines
        {
            get
            {
                pbmodel.updateMaxLines();
                return pbmodel.MaxLines - 1;
            }
        }
        public float VM_CurrentLine
        {
            get { return pbmodel.CurrentLine; }
        }
        public string VM_CurrentTime
        {
            get { return pbmodel.CurrentTime.ToString(); }
        }
        public string VM_PlaySpeed
        {
            get { return pbmodel.PlaySpeed.ToString(); }
        }
        public void setPlaySpeed(string speed)
        {
            pbmodel.setPlaySpeed(speed);
        }

        public void play()
        {
            pbmodel.play();
        }
        public void pause()
        {
            pbmodel.ToPlay = false;
        }
        public void stop()
        {
            pbmodel.ToPlay = false;
            pbmodel.CurrentLine = 0;
        }
        public void skipEnd()
        {
            pbmodel.CurrentLine = pbmodel.MaxLines - 1;
        }
        public void skipStart()
        {
            pbmodel.CurrentLine = 0;
        }
        public void skipBackward()
        {
            pbmodel.skip(-10);
        }
        public void skipForward()
        {
            pbmodel.skip(10);
        }
        public void setCurrentLine(int line)
        {
            pbmodel.CurrentLine = line;
        }
    }
}
