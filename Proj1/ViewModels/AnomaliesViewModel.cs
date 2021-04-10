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

    class AnomaliesViewModel:INotifyPropertyChanged
    {
        private AnomaliesModel amodel;
        private DataModel dmodel;
      
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

        public void NotifyPropertyChanged(string propName)
        {
            if (propName == "VM_SettingsOK")
                amodel.getFeatures();
            if (propName == "VM_CurrentUpdate")
                amodel.createGraph();
            if (propName == "VM_DllLoaded")
                amodel.getAnomaliesList();
            else if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
        public void update(string selectStr)
        {
            amodel.update(selectStr);
        }
        public void updateAnomaly(string selectStr)
        {
            amodel.updateAnomaly(selectStr);
        }
        public PlotModel VM_FeatureGraph
        {
            get
            { return amodel.FeatureGraph; }
        }

        public PlotModel VM_CorrelatedFeatureGraph
        {
            get { return amodel.CorrelatedFeatureGraph; }
        }

        public PlotModel VM_RegressionGraph
        {
            get { return amodel.RegressionGraph; }
        }
        public List<string> VM_FeaturesList
        {
            get { return amodel.FeaturesList; }
        }
        public List<string> VM_AnomaliesList
        {
            get { return amodel.AnomaliesList; }
        }
        public List<DataPoint> VM_DataPoints
        {
            get
            { return amodel.DataPoints; }
        }
        public string VM_AxisYTitle
        {
            get { return amodel.AxisYTitle; }
        }
        public double VM_AxisXMax
        {
            get { return amodel.AxisXMax; }
        }
        public List<DataPoint> VM_DataPointsCorr
        {
            get
            { return amodel.DataPointsCorr; }
        }
        public string VM_AxisYTitleCorr
        {
            get { return amodel.AxisYTitleCorr; }
        }
        public double VM_AxisXMaxCorr
        {
            get { return amodel.AxisXMaxCorr; }
        }
        public List<ScatterPoint> VM_RegDataPoints
        {
            get
            { return amodel.RegDataPoints; }
        }
        public List<ScatterPoint> VM_AnomaliesPoints
        {
            get
            { return amodel.AnomaliesPoints; }
        }
        public List<DataPoint> VM_RegPoints
        {
            get
            { return amodel.RegPoints; }
        }

    }
}
