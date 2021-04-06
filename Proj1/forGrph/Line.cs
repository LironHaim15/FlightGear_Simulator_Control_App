using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proj1.forGrph
{
    class Line
    {
		private double a, b;
        public Line() {
            a = 0;
			b = 0;
		}
		public Line(double a, double b)  {
            this.a = a;
			this.b = b;
		}
		public double f(double x)
		{
			return a * x + b;
		}
	}
}
