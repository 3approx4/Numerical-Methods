using System;
using System.Linq;
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
        public static Matrix Approximate(Matrix xValues, Matrix yValues, int k)
        {
            if (xValues.Height != 1 || yValues.Width != 1)
                throw new Exception("Incorrect input");

            if (k < 1)
                return new Matrix(0, 0);
            if (k < yValues.Height - 1)
            {
                xValues = new Matrix(xValues[0].Take(k + 1).ToArray());
                yValues = yValues.RowSlice(0, k);
            }

            Matrix xMatrix = new Matrix(yValues.Height, xValues.Width);
            Matrix yMatrix = new Matrix(yValues.Height, 1);

            // Raise bets
            for (int y = 0; y < yValues.Height; y++)
            {
                for (int x = 0; x < xValues.Width; x++)
                {
                    float xSum = 0;
                    for (int i = 0; i < xValues.Width; i++)
                        xSum += (float)Math.Pow(xValues[0, i], y + x);

                    xMatrix[y, x] = xSum;
                }

                float ySum = 0;
                for (int i = 0; i < yValues.Height; i++)
                    ySum += (float)Math.Pow(xValues[0, i], y) * yValues[i, 0];
                yMatrix[y, 0] = ySum;
            }

            // Solving
            Matrix aMatrix = RelaxationMethod.Solve(xMatrix, yMatrix, 0.00001f, 0.8f);
            // GaussZeidelMethod.Solve(xMatrix, yMatrix, 0.00001f);

            /*
            Matrix aMatrix = new Matrix(new float[,] {
                { 21.655175185f },
                { -69.1485164816f },
                { 30.3403573138f },
                { 22.8318150818f },
                { -11.6418820939f },
                { -1.52035040089f },
                { 0.85228321723f }
            });
            */

            return aMatrix;
        }
    }
}