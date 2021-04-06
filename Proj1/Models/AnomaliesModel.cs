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
    using OxyPlot;
    using OxyPlot.Series;
    class AnomaliesModel:INotifyPropertyChanged
    {
        private List<string> features;
        private List<string> anomalies;
        private PlotModel featureGraph;
        private LineSeries seriesfeatureGraph;
        private PlotModel correlatedGraph;
        private LineSeries seriesCorrelatedGraph;
        private PlotModel regressionGraph;
        private FunctionSeries seriesRegressionGraph;
        private ScatterSeries pointsRegressionGraph;
        private ScatterSeries pointsAnomaly;
        private List<int> anomaliesControl;
        private string nameChoice;
        private string nameCorrelated;
        private int previousLine;
        private int currentLine;
        private bool nameChanged;
        private int nameIndex;
        private int nameCorrelatedIndex;
        private DataModel dmodel;
        
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
        public void NotifyPropertyChanged(string propName)
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
            if(nameChoice!= name)
            {
                NameChoice = name;
                nameCorrelatedIndex = anomaly_detection_util.mostPearsonIndex(
                       dmodel.CsvData,
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
            Console.WriteLine("time: "+ time);
            dmodel.setCurrentLine((int)(time.TotalSeconds * 10));
            Console.WriteLine(dmodel.CurrentLine);
        }
        public string NameChoice
        {
            get { return nameChoice; }
            set
            {
                if (nameChoice != value)
                {
                    nameChoice = value;
                    nameIndex = getNameIndex(nameChoice);
                    /*if (nameChoice == "aileron") {
                        double[,] data = DataModel.Instance.CsvData;
                        for (int i = 0; i < 42; i++)
                        {
                            Console.WriteLine(DataModel.Instance.FeaturesNames[i]+": "+anomaly_detection_util.pearson(Enumerable.Range(0, data.GetLength(0)).Select(x => data[x, 0]).ToArray(),
                    Enumerable.Range(0, data.GetLength(0)).Select(x => data[x, i]).ToArray(),
                    DataModel.Instance.MaxLines));
                        }
                        
                    }*/
                    NameChanged = true;
                }
            }
        }
        public string NameChoiceCorrelated
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

        public bool NameChanged
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
        private bool isAnomaly(int line)
        {
            if (!dmodel.Anomalies.ContainsKey(line))
                return false;
            if (dmodel.Anomalies[line].Contains(NameChoice + ',' + NameChoiceCorrelated))
                return true;
            return false;
        }
        public PlotModel createGraph(int nameI, string name, string graphName,ref PlotModel graph, ref LineSeries series, bool createNew)
        {
            PlotModel t;
            if (createNew)
            {
                t = new PlotModel { Title = graphName, TitleFontSize = 24, LegendFontSize = 24 };
                series = new LineSeries { Title = name, MarkerType = MarkerType.Circle, FontSize= 24 };
                series.Points.Add(new DataPoint(currentLine / 10.0, dmodel.CsvData[currentLine,nameI]));
            }
            else
            {
                t = graph;
                t.Series.Remove(series);
                t = new PlotModel { Title = graphName , TitleFontSize= 24, LegendFontSize = 24 };
                series.Points.Add(new DataPoint(currentLine / 10.0, dmodel.CsvData[currentLine,nameI]));
                if (series.Points.Count >= 900)
                    series.Points.RemoveAt(0);
            }
            t.Series.Add(series);
            t.Axes.Add(new OxyPlot.Axes.LinearAxis { Position = OxyPlot.Axes.AxisPosition.Bottom, Title = "Time (seconds)", FontSize = 24 });
            t.Axes.Add(new OxyPlot.Axes.LinearAxis { Position = OxyPlot.Axes.AxisPosition.Left, Title = name, FontSize = 24 });
            return t;
        }
        public void createGraph()
        {
            if (NameChoice == null)
                return;
            previousLine = currentLine;
            currentLine = dmodel.CurrentLine;
            if (nameChanged || currentLine - previousLine != 1)
            {
                FeatureGraph = createGraph(nameIndex, NameChoice, "Feature Choice",ref featureGraph, ref seriesfeatureGraph, true);
                CorrelatedFeatureGraph = createGraph(nameCorrelatedIndex, nameCorrelated, "Correlated Feature", ref correlatedGraph, ref seriesCorrelatedGraph, true);
                RegressionGraph = createRegressionGraph(ref regressionGraph, true);
                NameChanged = false;
            }
            else
            {
                FeatureGraph = createGraph(nameIndex, NameChoice, "Feature Choice", ref featureGraph, ref seriesfeatureGraph, false);
                CorrelatedFeatureGraph = createGraph(nameCorrelatedIndex, nameCorrelated, "Correlated Feature", ref correlatedGraph, ref seriesCorrelatedGraph, false);
                RegressionGraph = createRegressionGraph(ref regressionGraph, false);
            }
        }
        public PlotModel createRegressionGraph(ref PlotModel graph, bool createNew)
        {
            PlotModel t = graph;
            bool changed = false;
            if (createNew)
            {
                anomaliesControl = new List<int>();
                t = new PlotModel { Title = NameChoice+"\n"+nameCorrelated, TitleFontSize = 24, LegendFontSize= 24 };
                seriesRegressionGraph = new FunctionSeries { Title = "Regression Line", MarkerType = MarkerType.Circle, FontSize = 24 };
                pointsRegressionGraph = new ScatterSeries { Title = "Samples", MarkerType = MarkerType.Circle, FontSize = 24 };
                pointsAnomaly = new ScatterSeries { Title = "Anomalies", MarkerType = MarkerType.Circle };
                int cl = currentLine;
                cl -= cl % 10;
                int counter = 30;
                while (cl >= 0 && counter > 0)
                {
                    if (isAnomaly(cl))
                    {
                        anomaliesControl.Add(1);
                        pointsAnomaly.Points.Add(new ScatterPoint(dmodel.CsvData[cl, nameIndex],
                        dmodel.CsvData[cl, nameCorrelatedIndex]));
                    }
                    else
                    {
                        anomaliesControl.Add(0);
                        pointsRegressionGraph.Points.Add(new ScatterPoint(dmodel.CsvData[cl, nameIndex],
                            dmodel.CsvData[cl, nameCorrelatedIndex]));
                    }
                    counter--;
                    cl -= 10;
                }
                double[] X = Enumerable.Range(0, dmodel.CsvData.GetLength(0)).Select(x => dmodel.CsvData[x, nameIndex]).ToArray();
                double[] Y = Enumerable.Range(0, dmodel.CsvData.GetLength(0)).Select(x => dmodel.CsvData[x, nameCorrelatedIndex]).ToArray();
                double minX = X.Min(), maxX = X.Max();
                Line reg = anomaly_detection_util.linear_reg(X, Y, dmodel.MaxLines);
                seriesRegressionGraph.Points.Add(new DataPoint(minX, reg.f(minX)));
                seriesRegressionGraph.Points.Add(new DataPoint(maxX, reg.f(maxX)));
                changed = true;
            }
            else if ((currentLine-currentLine%10) != (previousLine-previousLine % 10))
            {
                t.Series.Remove(pointsRegressionGraph);
                t.Series.Remove(seriesRegressionGraph);
                t.Series.Remove(pointsAnomaly);
                t = new PlotModel { Title = NameChoice, TitleFontSize = 24, LegendFontSize = 24 };
                int cl = currentLine;
                cl -= cl % 10;
                if (isAnomaly(cl))
                {
                    anomaliesControl.Add(1);
                    pointsAnomaly.Points.Add(new ScatterPoint(dmodel.CsvData[cl, nameIndex],
                    dmodel.CsvData[cl, nameCorrelatedIndex]));
                }
                else
                {
                    anomaliesControl.Add(0);
                    pointsRegressionGraph.Points.Add(new ScatterPoint(dmodel.CsvData[cl, nameIndex],
                        dmodel.CsvData[cl, nameCorrelatedIndex]));
                }
                if (anomaliesControl.Count>30)
                {
                    if (anomaliesControl.First().Equals(0))
                        pointsRegressionGraph.Points.RemoveAt(0);
                    else
                        pointsAnomaly.Points.RemoveAt(0);
                    anomaliesControl.RemoveAt(0);
                }
                changed = true;
            }
            if (changed)
            {
                t.Series.Add(seriesRegressionGraph);
                t.Series.Add(pointsRegressionGraph);
                t.Series.Add(pointsAnomaly);
                t.Axes.Add(new OxyPlot.Axes.LinearAxis { Position = OxyPlot.Axes.AxisPosition.Bottom, Title = NameChoice, FontSize = 24 });
                t.Axes.Add(new OxyPlot.Axes.LinearAxis { Position = OxyPlot.Axes.AxisPosition.Left, Title = NameChoiceCorrelated, FontSize = 24 });
            }
            return t;
            //t.InvalidatePlot(true); can refresh???????
        }
    }
}
