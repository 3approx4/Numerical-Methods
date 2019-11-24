using Numerical_Methods.Libs;
using System;

namespace Numerical_Methods.Algorithms.Approximation
{
    public delegate double FittingFunction(double x);

    public class ChebyshevApproximation
    {
        public static Matrix Approximate(Matrix xValues, Matrix yValues, float a, float b, int degree, FittingFunction fittingFunction)
        {
            int n = degree + 1;
            float[] result = new float[n];
            var u = ChebyshevSpace(n);
            for (int i = 0; i < n; i++)
            {
                var x = Expand(u[i], a, b);
                result[i] = Convert.ToSingle(fittingFunction(x));
            }

            return new Matrix(result);

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

        private static float[] ChebyshevSpace(int pointNumber, float step = 0.5f)
        {
            var points = new float[pointNumber];
            for (int i = 0; i < pointNumber; i++)
            {
                points[i] = Convert.ToSingle(-Math.Cos(Math.PI * Convert.ToDouble((i + step) / i)));
            }
            return points;
        }
    }
}