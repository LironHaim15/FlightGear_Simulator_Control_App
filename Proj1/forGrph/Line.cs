using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proj1.forGrph
{
	/// <summary>
	/// from the semester a .the code is the first targil from semster a in c#
	/// </summary>
	class Line
    {
		//feilds
		private double a, b;
		/// <summary>
		/// create defult line
		/// </summary>
		public Line() {
            a = 0;
			b = 0;
		}
		/// <summary>
		/// create line
		/// </summary>
		public Line(double a, double b)  {
            this.a = a;
			this.b = b;
		}
		/// <summary>
		/// finds y in the line in the point (x,a*x +b)
		/// </summary>
		public double f(double x)
		{
			return a * x + b;
		}
	}
}
