using Numerical_Methods.Libs;
using NUnit.Framework;

namespace Numerical_Methods.Algorithms.Tests
{
    [TestFixture]
    public class JacobiMethodTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void JacobiMethodTest()
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

            Matrix expectedResult = new Matrix(new float[,] {
                { 25f / 26f },
                { 7f / 26f },
                { 41f / 26f }
            });

            //Matrix expectedResult = GaussMethod.Solve(variableCoefficients, freeCoefficients);
            float epsilon = 0.0001f;

            Matrix result = JacobiMethod.Solve(variableCoefficients, freeCoefficients, epsilon);

            Assert.True(expectedResult.NearEquals(result), "Matrix are not equal:\nExpected:{0}\nResult:{1}",
                expectedResult.ToString(), result.ToString());
        }
    }
}