using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using Proj1.Models;

namespace Proj1.Models
{
    using System.Collections.Generic;
    using OxyPlot;
    using OxyPlot.Series;
    class AnomaliesModel:INotifyPropertyChanged
    {
        private List<string> features;
        private PlotModel tpm;

        public AnomaliesModel()
        {
            features = null;
            var t = new PlotModel { Title = "Simple example", Subtitle = "using OxyPlot" };
            var series1 = new LineSeries { Title = "Series 1", MarkerType = MarkerType.Circle };
            series1.Points.Add(new DataPoint(0, 0));
            series1.Points.Add(new DataPoint(10, 18));
            series1.Points.Add(new DataPoint(20, 12));
            series1.Points.Add(new DataPoint(30, 8));
            series1.Points.Add(new DataPoint(40, 15));
            var series2 = new LineSeries { Title = "Series 2", MarkerType = MarkerType.Square };
            series2.Points.Add(new DataPoint(0, 4));
            series2.Points.Add(new DataPoint(10, 12));
            series2.Points.Add(new DataPoint(20, 16));
            series2.Points.Add(new DataPoint(30, 25));
            series2.Points.Add(new DataPoint(40, 5));
            t.Series.Add(series1);
            t.Series.Add(series2);
            TMP = t;
        }
        public void updth(string selectStr)
        {
            // do somting 
            int correntLine = DataModel.Instance.CurrentLine;

        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
        public void getFeatures()
        {
            FeaturesList = DataModel.Instance.FeaturesNames;
        }
        public void getPoints()
        {
          


        }
        public List<string> FeaturesList
        {
            get { return features; }
            set
            {
                features = value;
                NotifyPropertyChanged("FeaturesList");
            }
        }
        public PlotModel TMP
        {
            get { return tpm; }
            set
            {
                tpm = value;
                NotifyPropertyChanged("TMP");
            }
        }
    }

}
