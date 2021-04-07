using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Net;
using System.Diagnostics;
using System.Net.Sockets;
using System.Threading;
using System.Windows;

namespace Proj1.Models
{
    class PlayBarModel : INotifyPropertyChanged
    {
        private TimeSpan currentTime;
        private double playSpeed;
        private int currentLine;
        private int sleepSpeed;
        private bool toPlay;
        private int lineFreq;
        private int maxLines;
        private Thread thread;
        private List<string> stringData;
        public PlayBarModel() {
            currentTime = new TimeSpan(0, 0, 0);
            playSpeed = 1.0;
            currentLine = 0;
            lineFreq = 10;
            sleepSpeed = 1000 / lineFreq;
            toPlay = false;
            MaxLines = 0;
            stringData = DataModel.Instance.StringData;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }

        public int MaxLines
        {
            get { return maxLines; }
            set { maxLines = value;
            }
        }
        public void updateMaxLines()
        {
            MaxLines = DataModel.Instance.MaxLines;
        }
        public int CurrentLine
        {
            get { return currentLine; }
            set
            {
                currentLine = value;
                CurrentTime = TimeSpan.FromSeconds(currentLine / lineFreq);
                if (currentLine >= MaxLines-1) {
                    ToPlay = false;
                }
                DataModel.Instance.CurrentLine = currentLine;
                NotifyPropertyChanged("CurrentLine");
            }
        }
        public TimeSpan CurrentTime
        {
            get { return currentTime; }
            set
            {
                currentTime = value;
                NotifyPropertyChanged("CurrentTime");
            }
        }
        public double PlaySpeed
        {
            get { return playSpeed; }
            set
            {
                playSpeed = value;
                double div = (1000 / lineFreq) / playSpeed;
                sleepSpeed = (int)div;
                NotifyPropertyChanged("PlaySpeed");
            }
        }
        public bool ToPlay
        {
            get { return toPlay; }
            set {
                if (toPlay != value)
                {
                    toPlay = value;
                    if (toPlay == false)
                    {
                        if (thread.IsAlive)
                            thread.Join();
                    }
                }         
            }
        }
        public void setPlaySpeed(string speed)
        {
            try { double ps = double.Parse(speed);
                PlaySpeed = ps;
            }
            catch
            {
                PlaySpeed = PlaySpeed;
            }
        }

        public void skip(int amount)
        {
            int lines = CurrentLine + (amount * lineFreq);
            if (lines <= 0)
                CurrentLine = 0;
            else if (lines >= MaxLines-1)
                CurrentLine = MaxLines-1;
            else
                CurrentLine = lines;
        }

        public void play()
        {
            if (ToPlay == true || CurrentLine >= MaxLines - 1)
            {
                return;
            }
            ToPlay = true;
            /*if(DataModel.Instance.Thread!=null && DataModel.Instance.Thread.IsAlive)
            {
                try { DataModel.Instance.Thread.Abort(); }
                catch { }
            }*/
            thread = new Thread(() =>
            {
                try
                {
                    Socket client = DataModel.Instance.Socket;
                    while (ToPlay && DataModel.Instance.Connected)
                    {
                        byte[] byteData = Encoding.ASCII.GetBytes(stringData[CurrentLine] + "\r\n");
                        int sent = client.Send(byteData);
                        CurrentLine++;
                        Thread.Sleep(sleepSpeed);
                    }
                }
                catch {

                } 
            });
            DataModel.Instance.Thread = thread;
            thread.Start();
        }
        public void restart()
        {
            CurrentTime = new TimeSpan(0, 0, 0);
            PlaySpeed = 1.0;
            currentLine = 0;
            sleepSpeed = 1000 / lineFreq;
            toPlay = false;
            MaxLines = 0;
            stringData = DataModel.Instance.StringData;
            NotifyPropertyChanged("CurrentLine");
        }
    }
}
