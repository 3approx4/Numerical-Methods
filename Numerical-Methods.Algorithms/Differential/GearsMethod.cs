using System;
using static Numerical_Methods.Algorithms.Differential.EulerMethod;

namespace Numerical_Methods.Algorithms.Differential
{
    public class GearsMethod
    {
        public static float[] Solve(DifferentialFunction function, int rank, float rightBorder, float y0, float step)
        {
            int order = (int)(rightBorder / step) + 1;
            float[] result = new float[order];
            result[0] = y0;

            for (int i = 1; i <= rank; i++)
            {
                float y = result[i - 1];
                result[i] = y + step * function(y, step * i);
            }

            for (int i = rank; i < order; i++)
            {
                // Define previous y values for Gear method
                float y_i = result[i - 1];
                float y_i1 = result[i - 2];
                float y_i2 = result[i - 3];
                // Predict value of y(k+1)
                var currentY = (step * function(result[i - 1] + step * function(result[i - 1], step * i), step * i));
                // Correct the predicted value
                result[i] = GearFunction(rank, new float[] { y_i2, y_i1, y_i, currentY });
            }

            return result;
        }

        /// <summary>
        /// Calculate the function value, based on the Gear rank using the predefined coefficients
        /// </summary>
        /// <param name="rank">Gear rank</param>
        /// <param name="yValues">Required function value array. Must be (rank + 1) elements long. Predicted y(k-2), y(k-1), y(k), y(k+1) == current y</param>
        /// <returns>Value of y+1 which is now corrected</returns>
        private static float GearFunction(int rank, float[] yValues)
        {
            if (yValues.Length != rank + 1)
                throw new System.Exception($"Gear rank {rank} requires {rank + 1} y values, but received {yValues.Length} values");
            switch (rank)
            {
                case 3:
                    return 2f / 11f * yValues[rank - 3] - 9f / 11f * yValues[rank - 2] + 18f / 11f * yValues[rank - 1] + 6f / 11f * yValues[rank];
                default:
                    throw new NotImplementedException($"Rank {rank} Gear method is not implemented yet");
            }
        }
    }
}