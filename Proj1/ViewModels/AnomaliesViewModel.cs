using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Proj1.Models;
using System.ComponentModel;



namespace Proj1.ViewModels
{
    using System.Collections.Generic;
    using OxyPlot;
    using OxyPlot.Series;
    /// <summary>
    ///  A AnomaliesViewModel class. the veiw model of grph part 
    /// </summary>
    /// <remarks>
    /// that conected between veiw and model of anomalies.
    /// </remarks>
    class AnomaliesViewModel :INotifyPropertyChanged
    {
        private AnomaliesModel amodel;
        private DataModel dmodel;

        /// <summary>
        ///the constructor of AnomaliesViewModel.
        /// </summary>
        public AnomaliesViewModel(AnomaliesModel am)
        {
            this.amodel = am;
            this.dmodel = DataModel.Instance;
            this.amodel.PropertyChanged += delegate (Object sender, PropertyChangedEventArgs e)
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
            // if we get notify from data model . its says that need to updth the list beacuse setting is okay of the 
            // features.
            if (propName == "VM_SettingsOK")
                amodel.getFeatures();
            // need to updth the grph according the line is runing
            else if (propName == "VM_CurrentUpdate")
                amodel.createGraph();
            // need to updth the list of anomliy according the new algo.
            else if (propName == "VM_DllLoaded")
                amodel.getAnomaliesList();
            else if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
        /// <summary>
        ///updth the FeaturesList of the choice of the user
        /// </summary>
        public void update(string selectStr)
        {
            amodel.update(selectStr);
        }
        /// <summary>
        ///updth the AnomalyList of the choice of the user
        /// </summary>
        public void updateAnomaly(string selectStr)
        {
            amodel.updateAnomaly(selectStr);
        }
        //DELETE
        public PlotModel VM_FeatureGraph
        {
            get
            { return amodel.FeatureGraph; }
        }
        //DELETE
        public PlotModel VM_CorrelatedFeatureGraph
        {
            get { return amodel.CorrelatedFeatureGraph; }
        }

        //DELETE
        public PlotModel VM_RegressionGraph
        {
            get { return amodel.RegressionGraph; }
        }
        /// <summary>
        ///property of the futures of the fly
        /// </summary>
        public List<string> VM_FeaturesList
        {
            get { return amodel.FeaturesList; }
        }
        /// <summary>
        ///property of the  Anomalies in fly
        /// </summary>
        public List<string> VM_AnomaliesList
        {
            get { return amodel.AnomaliesList; }
        }
        /// <summary>
        ///property of the points of grph of the choose feauture
        /// </summary>
        public List<DataPoint> VM_DataPoints
        {
            get
            { return amodel.DataPoints; }
        }
        /// <summary>
        ///property of the y title in the grph of the choose feauture axim
        /// </summary>
        public string VM_AxisYTitle
        {
            get { return amodel.AxisYTitle; }
        }
        /// <summary>
        ///property of the x in the grph(choose feauture) axim that is max to veiw
        /// </summary>
        public double VM_AxisXMax
        {
            get { return amodel.AxisXMax; }
        }
        /// <summary>
        ///property of the points of grph of the Correlated feauture
        /// </summary>
        public List<DataPoint> VM_DataPointsCorr
        {
            get
            { return amodel.DataPointsCorr; }
        }
        /// <summary>
        ///property of the y title in the Correlated grph  axim
        /// </summary>
        public string VM_AxisYTitleCorr
        {
            get { return amodel.AxisYTitleCorr; }
        }
        /// <summary>
        ///property of the x in the Correlated grph axim that is max to veiw
        /// </summary>
        public double VM_AxisXMaxCorr
        {
            get { return amodel.AxisXMaxCorr; }
        }
        /// <summary>
        ///property of the regular points of regretion grph 
        /// </summary>
        public List<ScatterPoint> VM_RegDataPoints
        {
            get
            { return amodel.RegDataPoints; }
        }
        /// <summary>
        ///property of the points of the anomalies in the grph
        /// </summary>
        public List<ScatterPoint> VM_AnomaliesPoints
        {
            get
            { return amodel.AnomaliesPoints; }
        }
        /// <summary>
        ///property of the points that create line of regretion
        /// </summary>
        public List<DataPoint> VM_RegPoints
        {
            get
            { return amodel.RegPoints; }
        }

    }
}
