using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Proj1.Models;

namespace Proj1.ViewModels
{
    class AnomaliesViewModel
    {
        private AnomaliesModel amodel;
        private DataModel dmodel;
        public AnomaliesViewModel(AnomaliesModel am)
        {
            this.amodel = am;
            this.dmodel = DataModel.Instance;
        }
    }
}
