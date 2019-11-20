using System;
using Numerical_Methods.Libs;

namespace Numerical_Methods.Algorithms.Approximation
{
    public class MeanSquaredErrorApproximation
    {
        /// <summary>
        /// Perform the approximation using the x and y values from matrices
        /// </summary>
        /// <param name="xValues">X values of the points provided - horizontal vector</param>
        /// <param name="yValues">Y values of the points provided - vertical vector</param>
        /// <returns>Matrix with A values for each X variable</returns>
        public static Matrix Approximate(Matrix xValues, Matrix yValues)
        {
            if (xValues.Height != 1 || yValues.Width != 1)
                throw new Exception("Incorrect input");

            Matrix xMatrix = new Matrix(yValues.Height, xValues.Width);

            // Raise bets
            for (int x = 0; x < xValues.Width; x++)
                xValues[0, x] = (float)Math.Pow(xValues[0, x], x);

            // Copy rows
            for (int y = 0; y < yValues.Height; y++)
                xMatrix[y] = xValues[0];

            // Solving
            Matrix aMatrix = RelaxationMethod.Solve(xMatrix, yValues, 0.00001f, 0.8f);

            return aMatrix;
        }
    }
}