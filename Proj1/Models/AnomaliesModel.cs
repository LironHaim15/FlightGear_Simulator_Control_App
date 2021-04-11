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
    /// <summary>
    ///  A AnomaliesModel class. the  model of grph part 
    /// </summary>
    /// <remarks>
    /// that logic of anomalies - grph part in project.
    /// </remarks>
    class AnomaliesModel : INotifyPropertyChanged
    {
        //feilds
        private List<string> features;
        private List<string> anomalies;
        // to know in the point that add to grph is anomaliy or not
        private List<int> anomaliesControl;
        // name of the choose feature and is Correlated feature
        private string nameChoice;
        private string nameCorrelated;
        // save the current line the previos in the fly.
        private int previousLine;
        private int currentLine;
        // if chosse in another feature
        private bool nameChanged;
        // the index of the names in feauture list
        private int nameIndex;
        private int nameCorrelatedIndex;
        // the data of all informtion of the progrem
        private DataModel dmodel;

        private List<DataPoint> dataPoints;
        //name in grph of y axim
        private string Yname;
        //the last time that see in grph
        private double Xmax;
        private List<DataPoint> dataPointsCorr;
        //name in correlted grph of y axim
        private string YnameCorr;
        private double XmaxCorr;
        // points to grph.
        private List<DataPoint> regPoints;
        private List<ScatterPoint> anomaliesPoints;
        private List<ScatterPoint> regDataPoints;
        private List<ScatterPoint> temp;

        /// <summary>
        ///the constructor of AnomaliesModel.
        /// </summary>
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
        /// <summary>
        ///mvvm notify of changes of model
        /// </summary>
        private void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
        /// <summary>
        ///updth feature list
        /// </summary>
        public void getFeatures()
        {
            FeaturesList = dmodel.FeaturesNames;
        }
        /// <summary>
        ///updth Anomalies List
        /// </summary>
        public void getAnomaliesList()
        {
            AnomaliesList = new List<string>();
            AnomaliesList = dmodel.AnomaliesList;
        }
        /// <summary>
        ///index of feautre in data - the column in data
        /// </summary>
        private int getNameIndex(string name)
        {
            return FeaturesList.IndexOf(name);
        }
        /// <summary>
        ///updth the name of the choise feature
        /// </summary>
        public void update(string name)
        {
            if (nameChoice != name)
            {
                NameChoice = name;
                // use in most preson to know the correlted feauture and updth it
                nameCorrelatedIndex = anomaly_detection_util.mostPearsonIndex(
                       dmodel.LearnData,
                       dmodel.MaxLines,
                       dmodel.FeaturesNames.Count,
                       nameIndex);
                NameChoiceCorrelated = dmodel.FeaturesNames[nameCorrelatedIndex];
            }
        }
        /// <summary>
        ///updth the name of the choise feature and correlted feauture  when choise from anomliy list
        /// and the time in fly
        /// </summary>
        public void updateAnomaly(string str)
        {
            // finds the names of the features
            string[] temp = str.Split(' ');
            TimeSpan time = TimeSpan.Parse(temp[0]);
            temp = temp.Last().Split(',');
            if (nameChoice != temp[0])
            {
                NameChoice = temp[0];
                nameCorrelatedIndex = getNameIndex(temp[1]);
                NameChoiceCorrelated = temp[1];
            }
            // updth the corrent line to move in the time in fly
            dmodel.setCurrentLine((int)(time.TotalSeconds * 10));
        }
        /// <summary>
        ///property of future name
        /// </summary>
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
        /// <summary>
        ///property of Correlated future name
        /// </summary>
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

        /// <summary>
        ///property of if the name change
        /// </summary>
        private bool NameChanged
        {
            get { return nameChanged; }
            set { nameChanged = value; }
        }
        /// <summary>
        ///property of features list
        /// </summary>
        public List<string> FeaturesList
        {
            get { return features; }
            set
            {
                features = value;
                NotifyPropertyChanged("FeaturesList");
            }
        }
        /// <summary>
        ///property of Anomalies list
        /// </summary>
        public List<string> AnomaliesList
        {
            get { return anomalies; }
            set
            {
                anomalies = value;
                NotifyPropertyChanged("AnomaliesList");
            }
        }

        /// <summary>
        ///property of data points of grph
        /// </summary>
        public List<DataPoint> DataPoints
        {
            get { return dataPoints; }
            set
            {
                dataPoints = value;
                NotifyPropertyChanged("DataPoints");
            }
        }
        /// <summary>
        ///property of the y title in the grph of the choose feauture axim
        /// </summary>
        public string AxisYTitle
        {
            get { return Yname; }
            set
            {
                Yname = value;
                NotifyPropertyChanged("AxisYTitle");
            }
        }
        /// <summary>
        ///property of the x in the grph(choose feauture) axim that is max to veiw
        /// </summary>
        public double AxisXMax
        {
            get { return Xmax; }
            set
            {
                Xmax = value;
                NotifyPropertyChanged("AxisXMax");
            }
        }
        /// <summary>
        ///property of the points of grph of the Correlated feauture
        /// </summary>
        public List<DataPoint> DataPointsCorr
        {
            get { return dataPointsCorr; }
            set
            {
                dataPointsCorr = value;
                NotifyPropertyChanged("DataPointsCorr");
            }
        }
        /// <summary>
        ///property of the y title in the Correlated grph  axim
        /// </summary>
        public string AxisYTitleCorr
        {
            get { return YnameCorr; }
            set
            {
                YnameCorr = value;
                NotifyPropertyChanged("AxisYTitleCorr");
            }
        }
        /// <summary>
        ///property of the x in the Correlated grph axim that is max to veiw
        /// </summary>
        public double AxisXMaxCorr
        {
            get { return XmaxCorr; }
            set
            {
                XmaxCorr = value;
                NotifyPropertyChanged("AxisXMaxCorr");
            }
        }
        /// <summary>
        ///property of the points of regretion grph 
        /// </summary>
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
        /// <summary>
        ///property of the points of the anomalies in the grph
        /// </summary>
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
        /// <summary>
        ///property of the points that create line of regretion
        /// </summary>
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
        /// <summary>
        ///check if have Anomly between the two feauters corrletive.
        /// </summary>
        private bool isAnomaly(int line)
        {
            //check if have anomly in this line.
            if (!dmodel.Anomalies.ContainsKey(line))
                return false;
            if (dmodel.Anomalies[line].Contains(NameChoice + ',' + NameChoiceCorrelated))
                return true;    // have anomly in this line of the chosine feauters.
            return false;
        }

        /// <summary>
        ///create two grph according the feauters indexs (nameI,nameIcorr) and need to know if to create a
        ///new grph
        /// </summary>
        private void createGraphs(int nameI, int nameIcorr, bool createNew)
        {
            try
            {
                if (createNew)
                {
                    // add all points in the grph when x axim is the time and y is the data of feautre in this
                    // time.
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
                // updth the names of the title of axim
                AxisYTitle = NameChoice;
                AxisYTitleCorr = NameChoiceCorrelated;
                // updth the max veiw in x axim according the line that fly
                AxisXMax = currentLine / 10.0;
                AxisXMaxCorr = currentLine / 10.0;
            }
            catch { }
        }
        /// <summary>
        ///add a point to the grph.
        /// </summary>
        public void addSamplePoint(int cl)
        {
            temp = new List<ScatterPoint>();
            //check if have anomliy by this point
            if (isAnomaly(cl))
            {
                // add to corntol list for know is anomliy point
                anomaliesControl.Add(1);
                temp.AddRange(anomaliesPoints);
                temp.Add(new ScatterPoint(dmodel.CsvData[cl, nameIndex],
                                                    dmodel.CsvData[cl, nameCorrelatedIndex]));
                //updth the anomliy list
                AnomaliesPoints = temp;

            }
            else
            {
                // for know its no anomliy point.
                anomaliesControl.Add(0);
                // have all the points to temp
                temp.AddRange(regDataPoints);
                temp.Add(new ScatterPoint(dmodel.CsvData[cl, nameIndex],
                                                   dmodel.CsvData[cl, nameCorrelatedIndex]));
                // updth the list.
                RegDataPoints = temp;
            }
        }
        /// <summary>
        ///create the regression graph.
        /// </summary>
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
                // add the 30 points that present the last 30 seconed (dont all the points of the 30 seconed only
                // 30 points according to modlo 10
                int cl = currentLine;
                cl -= cl % 10;
                int counter = 30;
                while (cl >= 0 && counter > 0)
                {
                    addSamplePoint(cl);
                    counter--;
                    cl -= 10;
                }
                // add the reg line
                double[] X = Enumerable.Range(0, dmodel.LearnData.GetLength(0)).Select(x => dmodel.LearnData[x, nameIndex]).ToArray();
                double[] Y = Enumerable.Range(0, dmodel.LearnData.GetLength(0)).Select(x => dmodel.LearnData[x, nameCorrelatedIndex]).ToArray();
                double minX = X.Min(), maxX = X.Max();
                Line reg = anomaly_detection_util.linear_reg(X, Y, dmodel.MaxLines);
                RegPoints.Add(new DataPoint(minX, reg.f(minX)));
                RegPoints.Add(new DataPoint(maxX, reg.f(maxX)));
                // updth all the points for this grph
                AxisYTitle = NameChoice;
                AxisYTitleCorr = NameChoiceCorrelated;
                RegPoints = regPoints;
            }
            // if it new mod 10 line for exmple currentLine =300 and previousLine= 299 need to add point
            else if ((currentLine - currentLine % 10) != (previousLine - previousLine % 10))
            {
                int cl = currentLine;
                cl -= cl % 10;
                // add a point
                addSamplePoint(cl);
                // if have more then 30 points
                if (anomaliesControl.Count > 30)
                {
                    temp = new List<ScatterPoint>();
                    // its no anomliy point that need to remove for the regular points
                    if (anomaliesControl.First().Equals(0))
                    {
                        temp.AddRange(regDataPoints);
                        //remove point
                        temp.RemoveAt(0);
                        RegDataPoints = temp;
                    }
                    // need to remove for anomliy
                    else
                    {
                        temp.AddRange(anomaliesPoints);
                        temp.RemoveAt(0);
                        AnomaliesPoints = temp;
                    }
                    // remove the first point and the data if its anomliy or not
                    anomaliesControl.RemoveAt(0);
                }
            }
        }
        /// <summary>
        ///create a graph 
        /// </summary>
        public void createGraph()
        {
            // if dont choose a name
            if (NameChoice == null)
                return;
            // save the previous line and the current line
            previousLine = currentLine;
            currentLine = dmodel.CurrentLine;
            // if  skipped in lines or chenge chose name need to create new grph
            if (nameChanged || currentLine - previousLine != 1)
            {
                // create the graph according to choose feauture
                createGraphs(nameIndex, nameCorrelatedIndex, true);
                createRegressionGraph(true);
                NameChanged = false;
            }
            else
            {
                // add to grph
                createGraphs(nameIndex, nameCorrelatedIndex, false);
                createRegressionGraph(false);
            }
        }
    }
}

