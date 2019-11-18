using NUnit.Framework;
using Numerical_Methods.Algorithms;
using System;
using System.Collections.Generic;
using System.Text;
using Numerical_Methods.Libs;

namespace Numerical_Methods.Algorithms.Tests
{
    [TestFixture()]
    public class ConditionNumberCalculatorTests
    {
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
            float expectedNumber = 15.0f;
            float conditionNumber = ConditionNumberCalculator.Calculate(variableCoefficients, freeCoefficients);
            Assert.AreEqual(expectedNumber, conditionNumber);
        }

        [Test]
        public void FifthEquationTest()
        {
            Matrix variableCoefficients = new Matrix(new float[,]
           {
                { 0.78f, 0.563f },
                { 0.913f, 0.659f }
           });

            Matrix freeCoefficients = new Matrix(new float[,]
            {
                { 0.217f },
                { 0.254f }
            });
            float expectedNumber = 15.0f;
            float conditionNumber = ConditionNumberCalculator.Calculate(variableCoefficients, freeCoefficients);
            Assert.AreEqual(expectedNumber, conditionNumber);
        }
    }
}