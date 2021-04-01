using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Net;
using System.Diagnostics;
using System.Net.Sockets;
using System.Threading.Tasks;
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
        public void NotifyPropertyChanged(string propName)
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
                if (currentLine >= MaxLines) {
                    ToPlay = false; }
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
                Console.WriteLine("sleepSpeed: " + sleepSpeed);
                Console.WriteLine("div: " + div);
                NotifyPropertyChanged("PlaySpeed");
            }
        }
        public bool ToPlay
        {
            get { return toPlay; }
            set { toPlay = value;
                if (toPlay == false)
                    thread.Join();
            }
        }
        public void setPlaySpeed(string speed)
        {
            try { double ps = double.Parse(speed);
                PlaySpeed = ps;
            }
            catch (FormatException e)
            {
                PlaySpeed = PlaySpeed;
            }
        }

        public void skip(int amount)
        {
            int lines = CurrentLine + (amount * lineFreq);
            if (lines <= 0)
                CurrentLine = 0;
            else if (lines >= MaxLines)
                CurrentLine = MaxLines;
            else
                CurrentLine = lines;
        }

        public void play()
        {
            if (ToPlay == true)
            {
                return;
            }
            /*if (thread != null)
            {
                Console.WriteLine("thread is not null, alive status: "+thread.IsAlive);
            }*/

            ToPlay = true;
            thread = new Thread(() =>
            {
                try
                {
                    Socket client = DataModel.Instance.Socket;
                    while (ToPlay)
                    {
                        byte[] byteData = Encoding.ASCII.GetBytes(stringData[CurrentLine] + "\r\n");
                        int sent = client.Send(byteData);
                        CurrentLine++;
                        Thread.Sleep(sleepSpeed);
                    }
                }
                catch {
                    Console.WriteLine(CurrentLine);
                } // TO COMPLETE
            });
            thread.Start();
        }

    }
}
