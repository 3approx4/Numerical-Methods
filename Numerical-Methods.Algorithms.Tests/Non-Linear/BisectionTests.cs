using Numerical_Methods.Algorithms.Non_Linear;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Numerical_Methods.Algorithms.Tests.Non_Linear
{
    [TestFixture]
    public class BisectionTests
    {
        private static float accuracyLowerBound = 0.01f;
        private static float accuracyUpperBound = 0.0001f;

        [Test]
        public void Test_Function()
        {
            var a = 1.6f;
            var b = 6f;
            var result1 = BisectionMethod.Evaluate(TestFunction, a, b, accuracyLowerBound);
            var result2 = BisectionMethod.Evaluate(TestFunction, a, b, accuracyUpperBound);

            Assert.LessOrEqual(result2.Value, result1.Value);
            Assert.GreaterOrEqual(result2.Iterations, result1.Iterations);

            Console.WriteLine(result1);
            Console.WriteLine(result2);
        }

        [Test]
        public void Function_5_Test()
        {
            var a = 2f;
            var b = 3f;

            var result1 = BisectionMethod.Evaluate(Function_5, a, b, accuracyLowerBound);
            var result2 = BisectionMethod.Evaluate(Function_5, a, b, accuracyUpperBound);

            Assert.LessOrEqual(result2.Value, result1.Value);
            Assert.GreaterOrEqual(result2.Iterations, result1.Iterations);

            Console.WriteLine(result1);
            Console.WriteLine(result2);
        }

        [Test]
        public void Function_9_Test()
        {
            var a = 0f;
            float b = (float)Math.PI * (3f/2f);

            var result1 = BisectionMethod.Evaluate(Function_9, a, b, accuracyLowerBound);
            var result2 = BisectionMethod.Evaluate(Function_9, a, b, accuracyUpperBound);

            Assert.LessOrEqual(result2.Value, result1.Value);
            Assert.GreaterOrEqual(result2.Iterations, result1.Iterations);

            Console.WriteLine(result1);
            Console.WriteLine(result2);
        }

        [Test]
        public void Function_11_Test()
        {
            var a = 0f;
            var b = 2f;

            var result1 = BisectionMethod.Evaluate(Function_11, a, b, accuracyLowerBound);
            var result2 = BisectionMethod.Evaluate(Function_11, a, b, accuracyUpperBound);

            Assert.LessOrEqual(result2.Value, result1.Value);
            Assert.GreaterOrEqual(result2.Iterations, result1.Iterations);

            Console.WriteLine(result1);
            Console.WriteLine(result2);
        }
        
        [Test]
        public void Function_15_Test()
        {
            var a = 0.2f;
            var b = 1f;

            var result1 = BisectionMethod.Evaluate(Function_15, a, b, accuracyLowerBound);
            var result2 = BisectionMethod.Evaluate(Function_15, a, b, accuracyUpperBound);

            Assert.LessOrEqual(result2.Value, result1.Value);
            Assert.GreaterOrEqual(result2.Iterations, result1.Iterations);

            Console.WriteLine(result1);
            Console.WriteLine(result2);
        }

        public static double TestFunction(double x)
        {
            return Math.Cos(2.0 / x) - 2 * Math.Sin(1.0 / x) - 1.0 / x;
        }

        public static double Function_15(double x)
        {
            return Math.Sqrt(4 - Math.Pow(x, 2)) / x;
        }

        public static double Function_5(double x)
        {
            return x * Math.Exp(x * 0.8f);
        }


        public static double Function_9(double x)
        {
            return Math.Pow(x, 2) + Math.Sin(x);
        }


        public static double Function_11(double x)
        {
            return x / Math.Pow(x + 2, 2);
        }
    }
}
