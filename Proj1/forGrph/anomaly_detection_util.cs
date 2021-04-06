using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proj1.forGrph
{
    static class anomaly_detection_util
    {
        private static double avg(double[] x, int size)
        {
            double sum = 0;
            for (int i = 0; i < size; sum += x[i], i++) ;
            return sum / size;
        }

        // returns the variance of X and Y
        private static double var(double[] x, int size)
        {
            double av = avg(x, size);
            double sum = 0;
            if (size - av * av==0)
                return 0;
            for (int i = 0; i < size; i++)
            {
                sum += x[i] * x[i];
            }
            return sum / size - av * av;
        }

        // returns the covariance of X and Y
        private static double cov(double[] x, double[] y, int size)
        {
            double sum = 0;
            for (int i = 0; i < size; i++)
            {
                sum += x[i] * y[i];
            }
            sum /= size;
            return sum - avg(x, size) * avg(y, size);
        }


        // returns the Pearson correlation coefficient of X and Y
        public static double pearson(double[] x, double[] y, int size)
        {
            double c = cov(x, y, size);
            double a = (Math.Sqrt(var(x, size)) * Math.Sqrt(var(y, size)));
            if (c == 0 || a == 0)
                return 0;
            return c / (Math.Sqrt(var(x, size)) * Math.Sqrt(var(y, size)));
        }
        public static int mostPearsonIndex(double [,]data,int sizeRow,int sizeCol,int j)
        {

            int index = j;
            double max = 0;
            for (int i = 0; i < sizeCol; i++)
            {
                double result = Math.Abs(pearson(Enumerable.Range(0, data.GetLength(0)).Select(x => data[x, i]).ToArray(),
                    Enumerable.Range(0, data.GetLength(0)).Select(x => data[x, j]).ToArray(),
                    sizeRow));
                if ((result >= max || double.IsInfinity(result) )&& i != j)
                {
                    index = i;
                    max = result;
                }
            }
            Console.WriteLine("max correlation: "+ max);
            return index;
        }

        // performs a linear regression and returns the line equation
        public static Line linear_reg(double[] x,double[] y, int size)
        {
            double a;
            if (var(x, size)==0)
                a= 0;
            else
                a = cov(x, y, size) / var(x, size);
            double b = avg(y, size) - a * (avg(x, size));
            return new Line(a, b);
        }
    }
}
