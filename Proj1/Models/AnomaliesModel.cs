using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using Proj1.Models;
using Proj1.forGrph;
using OxyPlot.Wpf;
using OxyPlot;

namespace Proj1.Models
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using OxyPlot;
    using OxyPlot.Series;
    class AnomaliesModel : INotifyPropertyChanged
    {
        private List<string> features;
        private List<string> anomalies;
        private PlotModel featureGraph;
        private PlotModel correlatedGraph;
        private PlotModel regressionGraph;
        private List<int> anomaliesControl;
        private string nameChoice;
        private string nameCorrelated;
        private int previousLine;
        private int currentLine;
        private bool nameChanged;
        private int nameIndex;
        private int nameCorrelatedIndex;
        private DataModel dmodel;

        private List<DataPoint> dataPoints;
        private string Yname;
        private double Xmax;
        private List<DataPoint> dataPointsCorr;
        private string YnameCorr;
        private double XmaxCorr;
        private List<DataPoint> regPoints;
        private List<ScatterPoint> anomaliesPoints;
        private List<ScatterPoint> regDataPoints;
        private List<ScatterPoint> temp;
        public AnomaliesModel()
        {
            features = null;
            anomalies = null;
            nameChoice = null;
            nameCorrelated = null;
            previousLine = -1;
            nameIndex = -1;
            nameCorrelatedIndex = -1;
            dmodel = DataModel.Instance;
            currentLine = dmodel.CurrentLine;
            nameChanged = false;
            anomalies = new List<string>();
        }
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
        public void getFeatures()
        {
            FeaturesList = dmodel.FeaturesNames;
        }
        public void getAnomaliesList()
        {
            AnomaliesList = new List<string>();
            AnomaliesList = dmodel.AnomaliesList;
        }
        private int getNameIndex(string name)
        {
            return FeaturesList.IndexOf(name);
        }
        public void update(string name)
        {
            if (nameChoice != name)
            {
                NameChoice = name;
                nameCorrelatedIndex = anomaly_detection_util.mostPearsonIndex(
                       dmodel.LearnData,
                       dmodel.MaxLines,
                       dmodel.FeaturesNames.Count,
                       nameIndex);
                NameChoiceCorrelated = dmodel.FeaturesNames[nameCorrelatedIndex];
            }
        }
        public void updateAnomaly(string str)
        {
            string[] temp = str.Split(' ');
            TimeSpan time = TimeSpan.Parse(temp[0]);
            temp = temp.Last().Split(',');
            if (nameChoice != temp[0])
            {
                NameChoice = temp[0];
                nameCorrelatedIndex = getNameIndex(temp[1]);
                NameChoiceCorrelated = temp[1];
            }
            dmodel.setCurrentLine((int)(time.TotalSeconds * 10));
        }
        private string NameChoice
        {
            get { return nameChoice; }
            set
            {
                if (nameChoice != value)
                {
                    nameChoice = value;
                    nameIndex = getNameIndex(nameChoice);
                    NameChanged = true;
                }
            }
        }
        private string NameChoiceCorrelated
        {
            get { return nameCorrelated; }
            set
            {
                if (nameCorrelated != value)
                {
                    nameCorrelated = value;
                }
            }
        }

        private bool NameChanged
        {
            get { return nameChanged; }
            set { nameChanged = value; }
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

        public List<string> AnomaliesList
        {
            get { return anomalies; }
            set
            {
                anomalies = value;
                NotifyPropertyChanged("AnomaliesList");
            }
        }
        public PlotModel FeatureGraph
        {
            get { return featureGraph; }
            set
            {
                featureGraph = value;
                NotifyPropertyChanged("FeatureGraph");
            }
        }
        public PlotModel CorrelatedFeatureGraph
        {
            get { return correlatedGraph; }
            set
            {
                correlatedGraph = value;
                NotifyPropertyChanged("CorrelatedFeatureGraph");
            }
        }
        public PlotModel RegressionGraph
        {
            get { return regressionGraph; }
            set
            {
                regressionGraph = value;
                NotifyPropertyChanged("RegressionGraph");
            }
        }
        public List<DataPoint> DataPoints
        {
            get { return dataPoints; }
            set
            {
                dataPoints = value;
                NotifyPropertyChanged("DataPoints");
            }
        }
        public string AxisYTitle
        {
            get { return Yname; }
            set
            {
                Yname = value;
                NotifyPropertyChanged("AxisYTitle");
            }
        }
        public double AxisXMax
        {
            get { return Xmax; }
            set
            {
                Xmax = value;
                NotifyPropertyChanged("AxisXMax");
            }
        }
        public List<DataPoint> DataPointsCorr
        {
            get { return dataPointsCorr; }
            set
            {
                dataPointsCorr = value;
                NotifyPropertyChanged("DataPointsCorr");
            }
        }
        public string AxisYTitleCorr
        {
            get { return YnameCorr; }
            set
            {
                YnameCorr = value;
                NotifyPropertyChanged("AxisYTitleCorr");
            }
        }
        public double AxisXMaxCorr
        {
            get { return XmaxCorr; }
            set
            {
                XmaxCorr = value;
                NotifyPropertyChanged("AxisXMaxCorr");
            }
        }
        public List<ScatterPoint> RegDataPoints
        {
            get
            { return regDataPoints; }
            set
            {
                regDataPoints = value;
                NotifyPropertyChanged("RegDataPoints");
            }
        }
        public List<ScatterPoint> AnomaliesPoints
        {
            get
            { return anomaliesPoints; }
            set
            {
                anomaliesPoints = value;
                NotifyPropertyChanged("AnomaliesPoints");
            }
        }
        public List<DataPoint> RegPoints
        {
            get
            { return regPoints; }
            set
            {
                regPoints = value;
                NotifyPropertyChanged("RegPoints");
            }
        }
        private bool isAnomaly(int line)
        {
            if (!dmodel.Anomalies.ContainsKey(line))
                return false;
            if (dmodel.Anomalies[line].Contains(NameChoice + ',' + NameChoiceCorrelated))
                return true;
            return false;
        }

        private void createGraphs(int nameI, int nameIcorr, bool createNew)
        {
            try
            {
                if (createNew)
                {
                    dataPoints = new List<DataPoint>();
                    dataPointsCorr = new List<DataPoint>();
                    for (int i = 0; i < dmodel.MaxLines; i++)
                    {
                        dataPoints.Add(new DataPoint(i / 10.0, dmodel.CsvData[i, nameI]));
                        dataPointsCorr.Add(new DataPoint(i / 10.0, dmodel.CsvData[i, nameIcorr]));
                    }
                    DataPoints = dataPoints;
                    DataPointsCorr = dataPointsCorr;
                }
                AxisYTitle = NameChoice;
                AxisYTitleCorr = NameChoiceCorrelated;
                AxisXMax = currentLine / 10.0;
                AxisXMaxCorr = currentLine / 10.0;
            }
            catch { }
        }
        private void swap(List<ScatterPoint> list1, List<ScatterPoint> list2)
        {
            List<ScatterPoint> newList = list1;
            list1 = list2;
            list2 = newList;
        }
        public void addSamplePoint(int cl)
        {
            temp = new List<ScatterPoint>();
            if (isAnomaly(cl))
            {
                anomaliesControl.Add(1);
                temp.AddRange(anomaliesPoints);
                temp.Add(new ScatterPoint(dmodel.CsvData[cl, nameIndex],
                                                    dmodel.CsvData[cl, nameCorrelatedIndex]));
                AnomaliesPoints = temp;

            }
            else
            {
                anomaliesControl.Add(0);
                temp.AddRange(regDataPoints);
                temp.Add(new ScatterPoint(dmodel.CsvData[cl, nameIndex],
                                                   dmodel.CsvData[cl, nameCorrelatedIndex]));
                RegDataPoints = temp;
            }
        }
        private void createRegressionGraph(bool createNew)
        {
            if (createNew)
            {
                // Reset the Plot source and refresh the graph.
                RegDataPoints = new List<ScatterPoint>();
                AnomaliesPoints = new List<ScatterPoint>();
                // Create new lists of points and add points to them, then notify the Plot.
                anomaliesControl = new List<int>();
                regDataPoints = new List<ScatterPoint>();
                anomaliesPoints = new List<ScatterPoint>();
                regPoints = new List<DataPoint>();
                int cl = currentLine;
                cl -= cl % 10;
                int counter = 30;
                while (cl >= 0 && counter > 0)
                {
                    addSamplePoint(cl);
                    counter--;
                    cl -= 10;
                }

                double[] X = Enumerable.Range(0, dmodel.LearnData.GetLength(0)).Select(x => dmodel.LearnData[x, nameIndex]).ToArray();
                double[] Y = Enumerable.Range(0, dmodel.LearnData.GetLength(0)).Select(x => dmodel.LearnData[x, nameCorrelatedIndex]).ToArray();
                double minX = X.Min(), maxX = X.Max();
                Line reg = anomaly_detection_util.linear_reg(X, Y, dmodel.MaxLines);
                RegPoints.Add(new DataPoint(minX, reg.f(minX)));
                RegPoints.Add(new DataPoint(maxX, reg.f(maxX)));
                AxisYTitle = NameChoice;
                AxisYTitleCorr = NameChoiceCorrelated;
                RegPoints = regPoints;
            }
            else if ((currentLine - currentLine % 10) != (previousLine - previousLine % 10))
            {
                int cl = currentLine;
                cl -= cl % 10;
                addSamplePoint(cl);
                if (anomaliesControl.Count > 30)
                {
                    temp = new List<ScatterPoint>();
                    if (anomaliesControl.First().Equals(0))
                    {
                        temp.AddRange(regDataPoints);
                        temp.RemoveAt(0);
                        RegDataPoints = temp;
                    }
                    else
                    {
                        temp.AddRange(anomaliesPoints);
                        temp.RemoveAt(0);
                        AnomaliesPoints = temp;
                    }
                    anomaliesControl.RemoveAt(0);
                }
            }
        }
        public void createGraph()
        {
            if (NameChoice == null)
                return;
            previousLine = currentLine;
            currentLine = dmodel.CurrentLine;
            if (nameChanged || currentLine - previousLine != 1)
            {
                createGraphs(nameIndex, nameCorrelatedIndex, true);
                createRegressionGraph(true);
                NameChanged = false;
            }
            else
            {
                createGraphs(nameIndex, nameCorrelatedIndex, false);                //CorrelatedFeatureGraph = createGraph(nameCorrelatedIndex, nameCorrelated, "Correlated Feature", ref correlatedGraph, ref seriesCorrelatedGraph, false);
                createRegressionGraph(false);
            }
        }
    }
}
