using System;
using Numerical_Methods.Libs;

namespace Numerical_Methods.Algorithms
{
    public class GaussZeidelMethod
    {
        public static Matrix Solve(Matrix aMatrix, Matrix bMatrix, float epsilon)
        {
            // Check if the result is reachable during next iterations ( matrix is diagonally dominant )
            if(!aMatrix.IsDiagonallyDominant())
                throw new Exception("Matrix is not diagonally dominant!");
            // Result of previous iteration
            Matrix prevX = new Matrix(bMatrix.Height, bMatrix.Width);
            // Result of current iteration
            Matrix currX = new Matrix(bMatrix.Height, bMatrix.Width);

            // Start from maximal delta value possible
            float maxDelta = float.MaxValue;
            do
            {
                // Calculate each X value
                for (int i = 0; i < bMatrix.Height; i++)
                {
                    // Calculate x based on the row values and x values from previous iteration
                    currX[i, 0] = aMatrix.CombineValues(i, currX, bMatrix);
                }
                // Calculate current distance between iteration results
                maxDelta = currX.MaxDelta(prevX);

                // Store the current iteration results as the previous iteration results
                prevX = Matrix.Clone(currX); // fix of having multiple references to single object
            } while (maxDelta > epsilon); // Iterate until the required precision is reached
            // return the value of the last iteration
            return prevX;
        }
    }
}