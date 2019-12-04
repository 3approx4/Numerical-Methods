using Numerical_Methods.Algorithms.Approximation;
using Numerical_Methods.Libs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Numerical_Methods.Algorithms.Non_Linear
{
    public class NewtonMethod
    {
        private static double h = 0.000001;
        private static float precentage = 0.1f;

        public static Result Evaluate(FittingFunction function, float a, float b, float accuracy)
        {
            double x0 = SetStartPoint(function, a, b);
            int iterationCount = 0;
            double x = 0;
            do
            {
                iterationCount++;
                var valueX0 = function(x0);
                var firstDeriv = (function(x0 + h) - function(x0 - h)) / (2 * h);
                x = x0 - (valueX0 / firstDeriv);
            } while (IsAccuracyReached(function, x0, accuracy));

            return new Result(iterationCount, x);
        }

        private static bool IsAccuracyReached(FittingFunction function, double value, float accuracy)
        {
            return Math.Abs(function(value)) < accuracy;
        }

        private static double SetStartPoint(FittingFunction function, float a, float b)
        {
            if (Converge(function, a)) return a;
            if (Converge(function, b)) return b;

            float width = b - a;

            return SetStartPoint(function, 
                a + (function(a) > 0 ? -width : width) * precentage,
                b + (function(a) > 0 ? -width : width) * precentage);
        }

        private static bool Converge(FittingFunction function, double x)
        {
            var value = function(x);
            var derivative = (function(x + h) - 2 * function(x) + function(x - h)) / (h * h);
            return value * derivative > 0;
        }
    }
}
