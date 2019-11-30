using Numerical_Methods.Algorithms.Approximation;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Numerical_Methods.Libs;

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
            int rank = ChebyshevApproximation.CalculateRank(a, b, step);
            var polynom = ChebyshevApproximation.Approximate(a, b, rank, myFunc);

            List<float> results = new List<float>();
            var xApprox = GenerateX(a, b, 0.0001f);

            for (float i = a; i <= b; i+=0.0001f)
            {
                float x = ChebyshevApproximation.Squish(i, a, b);
                results.Add(ChebyshevApproximation.Evaluate(polynom, x));
            }
            Assert.That(polynom.Length == rank + 1);
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
            var polynom = ChebyshevApproximation.Approximate(xValues, yValues, xValues.Length - 1);
            
            Assert.That(polynom.Length == xValues.Length);
        }
    
        [Test]
        public void CompareTableAndFunc()
        {
            float a = -2.0f;
            float b = 2.0f;
            float step = 0.5f;
            int rank = ChebyshevApproximation.CalculateRank(a, b, step);
            var xValues = GenerateX(a, b, step);
            var yValues = new float[rank];

            for (int i = 0; i < rank; i++)
            {
                yValues[i] = (float) myFunc(xValues[i]);
            }

            var expectedCoefficients = new Matrix(ChebyshevApproximation.Approximate(a, b, rank - 1, myFunc));

            var actualCoefficients = new Matrix(ChebyshevApproximation.Approximate(xValues, yValues, xValues.Length - 1));
            
            Assert.That(actualCoefficients.NearEquals(expectedCoefficients, (float)Math.PI + 0.2f), $"Expected: {expectedCoefficients.ToString()}, actual {actualCoefficients.ToString()}");
        }

        double myFunc(double x)
        {
            return Math.Sin(5 * x) * Math.Exp(x);
        }


        private static float[] GenerateX(float a, float b, float step)
        {
            int pointCount = ChebyshevApproximation.CalculateRank(a, b, step);
            var result = new float[pointCount];
            for (int i = 0; i < pointCount; i++)
            {
                result[i] = a + i * step;
            }
            return result;
        }
    }
}
