using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proj1.forGrph
{
    class anomaly_detection_util
    {

        float avg(float[] x, int size)
        {
            float sum = 0;
            for (int i = 0; i < size; sum += x[i], i++) ;
            return sum / size;
        }

        // returns the variance of X and Y
        float var(float[] x, int size)
        {
            float av = avg(x, size);
            float sum = 0;
            for (int i = 0; i < size; i++)
            {
                sum += x[i] * x[i];
            }
            return sum / size - av * av;
        }

        // returns the covariance of X and Y
        float cov(float[] x, float[] y, int size)
        {
            float sum = 0;
            for (int i = 0; i < size; i++)
            {
                sum += x[i] * y[i];
            }
            sum /= size;

            return sum - avg(x, size) * avg(y, size);
        }


        // returns the Pearson correlation coefficient of X and Y
        float pearson(float[] x, float[] y, int size)
        {
            return cov(x, y, size) / ((float)Math.Sqrt(var(x, size)) * (float)Math.Sqrt(var(y, size)));
        }
        int MostPersonIndex(float [][]data,int sizeRow,int sizeCol,int j)
        {
            int index = j;
            float max = 0;
            for (int i = 0; i < sizeRow; i++)
            {
                float result = Math.Abs(pearson(data[i], data[j], sizeRow));
                if (result > max && i != j)
                {
                    index = i;
                    max = result;
                }

            }
            return index;
        }

        // performs a linear regression and returns the line equation
        Line linear_reg(Point[] points, int size)
        {
            float[] x = new float[size];
            float[] y = new float[size];

            for (int i = 0; i < size; i++)
            {
                x[i] = points[i].X;
                y[i] = points[i].Y;
            }
            float a = cov(x, y, size) / var(x, size);
            float b = avg(y, size) - a * (avg(x, size));

            return new Line(a, b);
        }


    }
}
