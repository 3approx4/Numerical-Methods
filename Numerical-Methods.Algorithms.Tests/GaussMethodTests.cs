using Numerical_Methods.Libs;
using NUnit.Framework;

namespace Numerical_Methods.Algorithms.Tests
{
    public class GaussMethodTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void GaussMethod_Test()
        {
            // Currently used for debug purposes
            Matrix variableCoefficients = new Matrix(new float[,]
            {
                { 1, -2, 1 },
                { 2, -5, -1 },
                { -7, 0, 3}
            });
            Matrix freeCoefficients = new Matrix(new float[,]
            {
                { 2 },
                { -1 },
                { -2 }
            });
            Matrix result = GaussMethod.Solve(variableCoefficients, freeCoefficients);
            Assert.Pass();
        }
    }
}