using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using Proj1.Models;

namespace Proj1.Models
{
    class AnomaliesModel:INotifyPropertyChanged
    {
        private List<string> features;
        public AnomaliesModel() {
            features = null;
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
        public List<string> FeaturesList
        {
            get { return features; }
            set { features = value;
                NotifyPropertyChanged("FeaturesList");
            }
        }
    }

}
