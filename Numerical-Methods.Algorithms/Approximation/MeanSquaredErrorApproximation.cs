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
            // TODO investigate why is the matrix 2x2
            Matrix dMatrix = new Matrix(yValues.Height, xValues.Width);

            return dMatrix;
        }
    }
}