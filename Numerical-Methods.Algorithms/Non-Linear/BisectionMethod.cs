using Numerical_Methods.Algorithms.Approximation;
using Numerical_Methods.Libs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Numerical_Methods.Algorithms.Non_Linear
{
    public class BisectionMethod
    {
        /// <summary>
        /// Check if convergention condition is achieved
        /// </summary>
        /// <param name="function">Function to evaluate</param>
        /// <param name="a">Left border</param>
        /// <param name="b">Right border</param>
        /// <returns></returns>
        public static bool Converge(FittingFunction function, float a, float b)
        {
            var aValue = function(a);
            var bValue = function(b);
            return (aValue * bValue) <= 0;
        }

        public static Result Evaluate(FittingFunction function, float a, float b, float accuracy)
        {
            if (!Converge(function, a, b))
                throw new Exception("Convergence condition is not achieved");

            double result = 0;

            int iterationCount = 0;
            do
            {
                iterationCount++;
                float center = (a + b) / 2.0f;
                result = function(center);
                if (function(a) * result <= 0)
                {
                    b = center;
                }
                else
                {
                    a = center;
                }

            } while (Math.Abs(b - a) <= accuracy);

            return new Result(iterationCount, result);
        }
    }
}
