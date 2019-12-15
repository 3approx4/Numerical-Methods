using System;
using Numerical_Methods.Algorithms.Approximation;
using Numerical_Methods.Libs;

namespace Numerical_Methods.Algorithms.Differential
{
    public class EulerMethod
    {
        public static float[] Solve(DifferentialFunction function, float y0, float rightBorder, float step)
        {
            int order = (int)(rightBorder / step) + 1;
            float[] result = new float[order];
            result[0] = y0;
            for (int i = 1; i < order; i++)
            {
                float yPrev = result[i - 1];
                float x = step * i;
                float f = function(yPrev, x);
                float hF = step * f;
                result[i] = yPrev + hF;
            }
            
            return result;
        }
        
        /// <summary>
        /// Delegate for differential function definition and usage
        /// </summary>
        /// <param name="y"></param>
        /// <param name="t"></param>
        public delegate float DifferentialFunction(float y, float t);
    }
}