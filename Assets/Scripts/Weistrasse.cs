using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets
{
    public class FunctionParams
    {
        public Color color;
        public int T;
        public double dt;
        public double D;
        public int N;
        public double sigma;
        public double b;
        public double s;

        public FunctionParams(Color color, int t, double dt, double d, int n, double sigma, double b, double s)
        {
            this.color = color;
            T = t;
            this.dt = dt;
            D = d;
            N = n;
            this.sigma = sigma;
            this.b = b;
            this.s = s;
        }
    }

    public static class Weistrasse
    {
        static System.Random rnd = new System.Random();
        static Color color ;
        static int T = 20;
        static int L;
        static double dt = 0.05;
        static double D = 1.4;
        static int N = 10;
        static double sigma = 3.3;
        static double b = 2.5;
        static double s = 0.005;
        static int counter=0;

        static public double[] signal;

        public static float get_dt { get => (float) dt; }
        public static Color Color { get => color; }

        static Weistrasse()
        {
            L = (int)(T / dt);
            signal = CreateSignal();
            color = new Color(1, 1, 1, 1);
        }

        public static void ParamsUpdate(FunctionParams funcParams)
        {
            T = funcParams.T;
            dt = funcParams.dt;
            L = (int)(T / dt);
            D = funcParams.D;
            N = funcParams.N;
            sigma = funcParams.sigma;
            b = funcParams.b;
            s = funcParams.s;
            color = funcParams.color;

            signal = CreateSignal();
        }

        public static double NextValue()
        {
            if (counter >= L)
            {
                counter = 0;
            }
            double[] y = signal;
            return signal[counter++];
        }

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
        private static  double[] weistrs()
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

            return y;
        }

        private static double[] normSignal(double[] y)
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

        public static double[] CreateSignal()
        {

            double[] y = weistrs();
            counter = 0;
        
            return normSignal(y);
        }
    }
}
