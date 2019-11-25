using Numerical_Methods.Libs;
using System;

namespace Numerical_Methods.Algorithms.Approximation
{
    public delegate double FittingFunction(double x);

    public class ChebyshevApproximation
    {
        public static float[] Approximate(float a, float b, int degree, FittingFunction fittingFunction)
        {
            int n = degree;
            float[] result = new float[n];
            var u = ChebyshevSpace(n);
            float[] xs = new float[n];
            float[] ys = new float[n];
            for (int j = 0; j < n; j++)
            {
                xs[j] = Expand(u[j], a, b);
                ys[j] = Convert.ToSingle(fittingFunction(xs[j]));
            }
            for(int j = 2; j < n; j++)
            {
                float c = 0;
                for (int i = 0; i < n; i++)
                {
                    // todo fix loop of matrix calculation with approximation coefficients
                    var f = Convert.ToSingle(fittingFunction(xs[i]));
                    var t = CalculatePolynom(j, u[i]);
                    c += f * t; 
                }
                result[j] = (2.0f / n) * c;
            }

            return result;
        }

        public static float Evaluate(float[] polynom, float value)
        {
            float result = 0;
            for(int i = 0; i < polynom.Length; i++)
            {
                result += polynom[i] * CalculatePolynom(i, value) - (0.5f * polynom[0]);
            }
            return result;
        }

        /// <summary>
        /// Get the x from [a;b] converted to [-1;1]
        /// </summary>
        /// <returns></returns>
        private static float Squish(float x, float a, float b)
        {
            return (2 * x - a - b) / (a - b);
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
                points[i] = (i + step)/pointNumber;
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
            float x_0 = 1;
            float x1 = value;
            float currentX = 0;

            switch (polynomOrder)
            {
                case 0:
                    currentX = x_0;
                    break;
                case 1:
                    currentX = x1;
                    break;
                default:
                    for (int i = 2; i <= polynomOrder; i++)
                    {
                        currentX = 2 * currentX * x1 - x_0;
                        x_0 = x1;
                        x1 = currentX;
                    }
                    break;
            }
            return currentX;
        }
    }
}