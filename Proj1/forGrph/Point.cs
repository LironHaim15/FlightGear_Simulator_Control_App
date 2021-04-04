using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proj1.forGrph
{
    class Point
    {
        private float x, y;
        public Point(float x, float y)
        {
            this.x = x;
            this.y = y;
        }
        public float X
        {
            get { return x; }
            set
            {
                x = value;

            }
        }

        public float Y
        {
            get { return y; }
            set
            {
                y = value;
            }
        }

    }
}
