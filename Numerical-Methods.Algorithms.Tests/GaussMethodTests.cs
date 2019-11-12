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
        public void FirstEquationTest()
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

            Matrix result = GaussMethod.Solve(variableCoefficients, freeCoefficients);

            Assert.True(expectedResult.NearEquals(result), "Matrices are not equal:\nExpected:{0}\nResult:{1}",
                expectedResult.ToString(), result.ToString());
        }

        [Test]
        public void SecondEquationTest()
        {
            Matrix variableCoefficients = new Matrix(new float[,]
            {
                { 2, -1, -1 },
                { 1, 3, -2 },
                { 1, 2, 3 }
            });
            
            Matrix freeCoefficients = new Matrix(new float[,]
            {
                { 5 },
                { 7 },
                { 10 }
            });

            Matrix expectedResult = new Matrix(new float[,]
            {
                { 5f / 2f },
                { 9f / 7f },
                { 15f / 16f }
            });

            Matrix result = GaussMethod.Solve(variableCoefficients, freeCoefficients);
            
            Assert.True(expectedResult.NearEquals(result), "Matrices are not equal:\nExpected:{0}\nResult:{1}",
                expectedResult.ToString(), result.ToString());
        }
        
        [Test]
        public void ThirdEquationTest()
        {
            Matrix variableCoefficients = new Matrix(new float[,]
            {
                { 8, 5, 3 },
                { -2, 8, 1 },
                { 1, 3, -10 }
            });
            
            Matrix freeCoefficients = new Matrix(new float[,]
            {
                { 30 },
                { 15 },
                { 42 }
            });

            Matrix expectedResult = new Matrix(new float[,]
            {
                { 15f / 4f },
                { 90f / 37f },
                { -3f }
            });

            Matrix result = GaussMethod.Solve(variableCoefficients, freeCoefficients);
            
            Assert.True(expectedResult.NearEquals(result), "Matrices are not equal:\nExpected:{0}\nResult:{1}",
                expectedResult.ToString(), result.ToString());
        }
    }
}