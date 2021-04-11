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
    class Point
    {
        //feilds
        private double x, y;
        /// <summary>
        /// create a point
        /// </summary>
        public Point(double x, double y)
        {
            this.x = x;
            this.y = y;
        }
        /// <summary>
        /// property of X in the point
        /// </summary>
        public double X
        {
            get { return x; }
            set { x = value; }
        }

        /// <summary>
        /// property of Y in the point
        /// </summary>
        public double Y
        {
            get { return y; }
            set { y = value; }
        }

    }
}
