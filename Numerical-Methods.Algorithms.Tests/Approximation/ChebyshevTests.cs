using Numerical_Methods.Algorithms.Approximation;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Numerical_Methods.Algorithms.Tests.Approximation
{
    [TestFixture]
    class ChebyshevTests
    {
        [Test]
        public void Approximate()
        {
            float a = -2.0f;
            float b = 2.0f;
            float step = 0.5f;
            int rank = CalculateRank(a, b, step);
            var polynom = ChebyshevApproximation.Approximate(a, b, rank, myFunc);
            var xvals = GenerateX(a, b, step);
            var yvals = new float[rank];
            for(int i = 0; i < rank; i++)
            {
                yvals[i] = Convert.ToSingle(myFunc(xvals[i])); 
            }
            var poly_arr = ChebyshevApproximation.Approximate(xvals, yvals);
            var xs = GenerateX(a, b, step);

            List<float> results = new List<float>();
            var xApprox = GenerateX(a, b, 0.0001f);

            for (float i = a; i <= b; i+=0.0001f)
            {
                float x = ChebyshevApproximation.Squish(i, a, b);
                results.Add(ChebyshevApproximation.Evaluate(polynom, x));
            }
            for(int i = 0; i < results.Count; i++)
            {
                Console.WriteLine($"{xApprox[i]},{results[i]}");
            }
            Assert.That(poly_arr.Equals(polynom));
        }

        [Test]
        public void ApproximateTableValues()
        {
            float[] xValues = new float[]
            {
                -3.2f, -2.1f, 0.4f, 0.7f, 2.0f, 2.5f, 2.777f
            };
            float[] yValues = new float[]
            {
                10.0f, -2.0f, 0, -7.0f, 7.0f, 0, 0
            };
            float[] xx = new float[xValues.Length];

            for(int i = 0; i < xValues.Length; i++)
            {
                float min = xValues.Min();
                float max = xValues.Max();
                xx[i] = ChebyshevApproximation.Squish(xValues[i], min, max);
            }

            var polynom = ChebyshevApproximation.Approximate(xValues, yValues);

            List<float> results = new List<float>();

            for (int i = 0; i < xValues.Length; i ++)
            {
                results.Add(ChebyshevApproximation.Evaluate(polynom, xx[i]));
            }
            for (int i = 0; i < results.Count; i++)
            {
                Console.WriteLine($"{results[i]}");
            }
            Assert.That(results.Equals(yValues));
        }

        double myFunc(double x)
        {
            return Math.Sin(5 * x) * Math.Exp(x);
        }

        /// <summary>
        /// Calculate the approximation rank (n)
        /// </summary>
        /// <param name="a">Interval left bound</param>
        /// <param name="b">Interval right bound</param>
        /// <param name="step"></param>
        /// <returns>(abs(a)+abs(b))/step</returns>
        private static int CalculateRank(float a, float b, float step)
        {
            return Convert.ToInt32((Math.Abs(a) + Math.Abs(b)) / step);
        }

        private static float[] GenerateX(float a, float b, float step)
        {
            int pointCount = CalculateRank(a, b, step);
            var result = new float[pointCount];
            for (int i = 0; i < pointCount; i++)
            {
                result[i] = a + i * step;
            }
            return result;
        }
    }
}
