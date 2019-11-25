using Numerical_Methods.Algorithms.Approximation;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Numerical_Methods.Algorithms.Tests.Approximation
{
    [TestFixture]
    class ChebyshevTests
    {
        [Test]
        public void Approximate()
        {
            var polynom = ChebyshevApproximation.Approximate(-2, 2, 9, myFunc);

            float[] results = new float[9];

            for(int i = 0 ; i < 9; i++)
            {
                float x = -2.0f + 0.5f * i;
                results[i] = ChebyshevApproximation.Evaluate(polynom, x);
            }

            Assert.That(results.Length);
        }

        double myFunc(double x)
        {
            return Math.Sin(5 * x) * Math.Exp(x);
        }
    }
}
