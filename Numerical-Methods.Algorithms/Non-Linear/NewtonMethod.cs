using Numerical_Methods.Algorithms.Approximation;
using Numerical_Methods.Libs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Numerical_Methods.Algorithms.Non_Linear
{
    public class NewtonMethod
    {
        private static double h = 0.01;
        private static float percentage = 0.1f;

        public static Result Evaluate(FittingFunction function, float a, float b, float accuracy)
        {
            double x0 = SetStartPoint(function, a, b);
            int iterationCount = 0;
            double x = x0 - (function(x0) / FirstDerivative(function, x0, h));
            while (IsAccuracyReached(function, x0, x, accuracy))
            {
                x0 = x;
                iterationCount++;
                x = x0 - (function(x0) / FirstDerivative(function, x0, h));
            }

            return new Result(iterationCount, x);
        }

        private static bool IsAccuracyReached(FittingFunction function, double oldValue, double newValue, float accuracy)
        {
            return Math.Abs(function(oldValue) - function(newValue)) > accuracy;
        }

        private static double SetStartPoint(FittingFunction function, float a, float b)
        {
            if (Converge(function, a)) return a;
            if (Converge(function, b)) return b;

            float width = b - a;

            return SetStartPoint(function, 
                a + (function(a) > 0 ? -width : width) * percentage,
                b + (function(a) > 0 ? -width : width) * percentage);
        }

        private static bool Converge(FittingFunction function, double x)
        {
            var value = function(x);
            var derivative = (function(x + h) - 2 * function(x) + function(x - h)) / (h * h);
            return value * derivative > 0;
        }

        private static double FirstDerivative(FittingFunction function, double x, double epsilon)
        {
            return (function(x + epsilon) - function(x)) / epsilon;
        }
    }
}
