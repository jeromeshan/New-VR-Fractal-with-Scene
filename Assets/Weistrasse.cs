using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets
{
    public class Weistrasse
    {
        Random rnd = new Random();


        static double[] linspace(double StartValue, double EndValue, int numberofpoints)
        {

            double[] parameterVals = new double[numberofpoints];
            double increment = Math.Abs(StartValue - EndValue) / Convert.ToDouble(numberofpoints - 1);
            int j = 0; //will keep a track of the numbers 
            double nextValue = StartValue;
            for (int i = 0; i < numberofpoints; i++)
            {


                parameterVals.SetValue(nextValue, j);
                j++;
                if (j > numberofpoints)
                {
                    throw new IndexOutOfRangeException();
                }
                nextValue = nextValue + increment;
            }
            return parameterVals;



        }

        /// <summary>
        /// Generate Weistrasse function values
        /// </summary>
        /// <param name="D">Fractal Dimansion</param>
        /// <param name="N">Number of Harmonics</param>
        /// <param name="sigma">standart diviation</param>
        /// <param name="b">Some coef</param>
        /// <param name="s">Some phi</param>
        /// <param name="L">Number of points</param>
        /// <param name="dt">Points per second</param>
        private Tuple<double[], double[]> weistrs(int L, double dt, double D = 1.4, int N = 10, double sigma = 3.3, double b = 2.5, double s = 0.005)
        {

            double[] x = linspace(0, L * dt, L);
            double[] y = new double[L];

            double h1 = sigma * Math.Sqrt(2);
            double h2 = Math.Sqrt(1 - Math.Pow(b, 2 * D - 4));
            double h3 = Math.Sqrt(1 - Math.Pow(b, (2 * D - 4) * (N + 1)));
            double h4 = h1 * h2 / h3;
            for (int i = 0; i < N + 1; i++)
            {
                double c1 = 2 * Math.PI * s * Math.Pow(b, i);
                double c2 = Math.Pow(b, (D - 2) * i);
                double ksi = rnd.NextDouble() * 2 * Math.PI;

                for (int k = 0; k < L; k++)
                {
                    y[k] += c2 * Math.Sin(c1 * x[k] + ksi);
                }
            }

            for (int i = 0; i < L; i++)
            {
                y[i] *= h4;
            }

            return new Tuple<double[], double[]>(x, y);
        }

        private double[] normSignal(double[] y, int L)
        {
            double[] z = new double[L];

            double max_val = y.Max();
            double min_val = y.Min();

            if (max_val == min_val)
            {
                for (int i = 0; i < L; i++)
                {
                    z[i] = 1;
                }
            }
            else
            {
                for (int i = 0; i < L; i++)
                {
                    z[i] = (y[i] - min_val) / (max_val - min_val);

                }
            }

            return z;
        }

        public double[] CreateSignal(int L, double dt)
        {

            double[] y = weistrs(L, dt).Item2;


            return normSignal(y, L);
        }
    }
}
