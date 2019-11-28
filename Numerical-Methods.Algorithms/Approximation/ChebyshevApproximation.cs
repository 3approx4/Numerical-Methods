using Numerical_Methods.Libs;
using System;
using System.Linq;

namespace Numerical_Methods.Algorithms.Approximation
{
    public delegate double FittingFunction(double x);

    public class ChebyshevApproximation
    {
        public static float[] Approximate(float a, float b, int degree, FittingFunction fittingFunction)
        {
            int n = degree + 1;
            float[] result = new float[n];
            var u = ChebyshevSpace(n);
            float[] xs = new float[n];
            float[] ys = new float[n];
            for (int j = 0; j < n; j++)
            {
                xs[j] = Expand(u[j], a, b);
                ys[j] = Convert.ToSingle(fittingFunction(xs[j]));
            }
            for (int j = 0; j < n; j++)
            {
                float c = 0;
                for (int i = 0; i < n; i++)
                {
                    var f = ys[i];
                    var t = CalculatePolynom(j, u[i]);
                    c += f * t;
                }
                result[j] = (2.0f / n) * c;
            }
            result[0] /= 2.0f;
            return result;
        }
        
        /// <summary>
        /// Approximate the function using the table defined function
        /// </summary>
        /// <param name="xValues">x values of the table function</param>
        /// <param name="yValues">y values of the table function</param>
        /// <returns></returns>
        public static float[] Approximate(float[] xValues, float[] yValues)
        {
            int n = xValues.Length;
            float[] result = new float[n];
            var u = ChebyshevSpace(n); 
            float min = xValues.Min();
            float max = xValues.Max(); 
            float[] xs = new float[n];
            for (int j = 0; j < n; j++)
            {
                xs[j] = Expand(u[j],min, max);
            }
            for (int j = 0; j < n; j++)
            {
                float c = 0;
                for (int i = 0; i < n; i++)
                {
                    var f = yValues[i];
                    var t = CalculatePolynom(j, u[i]);
                    c += f * t;
                }
                result[j] = (2.0f / n) * c;
            }
            result[0] /= 2.0f;
            return result;
        }
        
        /// <summary>
        /// Calculate the y value using polynom coefficients
        /// </summary>
        /// <param name="polynom">Chebyshev polynom coefficients</param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static float Evaluate(float[] polynom, float value)
        {
            float result = 0;
            for (int i = 0; i < polynom.Length; i++)
            {
                result += Convert.ToSingle(polynom[i] * CalculatePolynom(i, value));
            }
            return result;
        }

        /// <summary>
        /// Get the x from [a;b] converted to [-1;1]
        /// </summary>
        /// <returns></returns>
        public static float Squish(float x, float a, float b)
        {
            float c = (a + b) / 2.0f;
            float m = (b - a) / 2.0f;
            return (x - c) / m;
        }

        /// <summary>
        /// Get the x value from squished u
        /// </summary>
        /// <param name="u">Value in range [-1;1]</param>
        /// <param name="a">Left border</param>
        /// <param name="b">Right border</param>
        /// <returns></returns>
        private static float Expand(float u, float a, float b)
        {
            return ((b - a) * u + (a + b)) / 2.0f;
        }

        /// <summary>
        /// Calculate the solvations of the polynom
        /// </summary>
        /// <param name="pointNumber"></param>
        /// <param name="step"></param>
        /// <returns></returns>
        private static float[] ChebyshevSpace(int pointNumber, float step = 0.5f)
        {
            var points = new float[pointNumber];
            for (int i = 0; i < pointNumber; i++)
            {
                points[i] = (i + step) / pointNumber;
            }
            for (int i = 0; i < pointNumber; i++)
            {
                points[i] = Convert.ToSingle(-Math.Cos(Math.PI * points[i]));
            }
            return points;
        }

        /// <summary>
        /// Evaluate the T_{n+1}(x)= 2xT_n(x)-T_{n-1}(x).
        /// </summary>
        /// <param name="polynomOrder">Order of the polynom to evaluate</param>
        /// <param name="value">Value to use as x</param>
        /// <returns>Polynom value</returns>
        private static float CalculatePolynom(int polynomOrder, float value)
        {
            float x0 = 1.0f;
            float x1 = value;
            float xx = 0.0f;

            switch (polynomOrder)
            {
                case 0:
                    xx = 1;
                    break;
                case 1:
                    xx = value;
                    break;
                default:
                    for (int i = 2; i <= polynomOrder; i++)
                    {
                        xx = 2 * value * x1 - x0;
                        x0 = x1;
                        x1 = xx;
                    }
                    break;
            }
            return xx;
        }
    }
}