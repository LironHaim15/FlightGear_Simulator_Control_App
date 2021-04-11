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
        // time in video
        private TimeSpan currentTime;
        // speed of video
        private double playSpeed;
        // the line in the fly
        private int currentLine;
        // sleep in the send informtion the f
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
        /// <summary>
        /// Get and set Maximum lines in the data.
        /// </summary>
        public int MaxLines
        {
            get { return maxLines; }
            set { maxLines = value;
            }
        }
        /// <summary>
        /// update MaxLines in the Data Model.
        /// </summary>
        public void updateMaxLines()
        {
            MaxLines = DataModel.Instance.MaxLines;
        }
        /// <summary>
        /// Update current line in Data Model. stop the data stream by turning ToPlay to false.
        /// and update the time according to the new current line.
        /// </summary>
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
        /// <summary>
        /// Get and set CurrentTime.
        /// </summary>
        public TimeSpan CurrentTime
        {
            get { return currentTime; }
            set
            {
                currentTime = value;
                NotifyPropertyChanged("CurrentTime");
            }
        }
        /// <summary>
        /// Get and set PlaySpeed, assumining data is streaming 10 lines per second.
        /// </summary>
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
        /// <summary>
        /// Get and set ToPlay, if false then stop the thread by joining it.
        /// </summary>
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
        /// <summary>
        /// set the new PlaySpeed according to what the user entered in the playbar.
        /// </summary>
        /// <param name="speed"></param>
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

        /// <summary>
        /// skip 'amount' of seconds backwards or forwards in the time and current line.
        /// </summary>
        /// <param name="amount"></param>
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

        /// <summary>
        /// start playing and stearm data lines.
        /// initiating a new thread.
        /// </summary>
        public void play()
        {
            if (ToPlay == true || CurrentLine >= MaxLines - 1)
            {
                return;
            }
            ToPlay = true;
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
        /// <summary>
        /// reset every field. is called when disconneting and connecting again.
        /// </summary>
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
