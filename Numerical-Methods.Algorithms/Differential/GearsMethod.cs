using static Numerical_Methods.Algorithms.Differential.EulerMethod;

namespace Numerical_Methods.Algorithms.Differential
{
    public class GearsMethod
    {
        public static double[] Solve(DifferentialFunction function, int rank, double rightBorder, double y0, double step)
        {
            int order = (int)(rightBorder / step) + 1;
            var result = new double[order];
            result[0] = y0;

            for (int i = 1; i <= rank; i++)
            {
                var y = result[i - 1];
                result[i] = y + step * function(y, step * i);
            }

            for (int i = rank; i < order; i++)
            {
                // Define previous y values for Gear method
                var y_i = result[i - 1];
                var y_i1 = result[i - 2];
                var y_i2 = result[i - 3];
                // Predict value of y(k+1)
                var currentY = (step * function(result[i - 1] + step * function(result[i - 1], step * i), step * i));
                // Correct the predicted value
                result[i] = GearFunction(rank, new double[] { y_i2, y_i1, y_i, currentY });
            }

            return result;
        }

        private static double GearFunction(int rank, double[] yValues)
        {
            if (yValues.Length != rank + 1)
                throw new System.Exception($"Gear rank {rank} requires {rank + 1} y values, but received {yValues.Length} values");
            switch (rank)
            {
                case 3:
                    return 2f / 11f * yValues[rank - 3] - 9f / 11f * yValues[rank - 2] + 18f / 11f * yValues[rank - 1] + 6f / 11f * yValues[rank];
                default:
                    return 0;                 
            }
        }
    }
}