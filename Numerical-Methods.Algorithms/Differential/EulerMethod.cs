using System;
using Numerical_Methods.Algorithms.Approximation;
using Numerical_Methods.Libs;

namespace Numerical_Methods.Algorithms.Differential
{
    public class EulerMethod
    {
        public static double[] Solve(DifferentialFunction function, double y0, double rightBorder, double step)
        {
            int order = (int)(rightBorder / step) + 1;
            double[] result = new double[order];
            result[0] = y0;
            for (int i = 1; i < order; i++)
            {
                var y = result[i - 1];
                result[i] = y + step * function(y, step * i);
            }
            
            return result;
        }
        
        /// <summary>
        /// Delegate for differential function definition and usage
        /// </summary>
        /// <param name="y"></param>
        /// <param name="t"></param>
        public delegate double DifferentialFunction(double y, double t);
    }
}