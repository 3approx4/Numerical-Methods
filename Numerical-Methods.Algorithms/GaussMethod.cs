using System;
using System.Linq;
using Numerical_Methods.Libs;

namespace Numerical_Methods.Algorithms
{
    public static class GaussMethod
    {
        /// <summary>
        /// Gauss method for Linear Equation Systems
        /// </summary>
        /// <param name="aMatrix">Matrix of variable coefficients</param>
        /// <param name="bMatrix">Matrix of free coefficients</param>
        /// <returns>Vector of variable values which are the solution</returns>
        public static Matrix Solve(Matrix aMatrix, Matrix bMatrix)
        {
            Matrix result = new Matrix(bMatrix.ToArray());

            // 1. Forward Gauss
            for (int i = 0; i < aMatrix.Height; i++)
            { 
                // Current line
                var m = aMatrix.GetRowIndexWithMaxAbsValue(i);

                if (m != i)
                {
                    aMatrix.SwapRow(i, m);
                    bMatrix.SwapRow(i, m);
                }


                for (int k = 0; k < i; k++)
                {
                    bMatrix[i, 0] -= bMatrix[k, 0] * (aMatrix[i, k] / aMatrix[k, k]);
                    for (int j = aMatrix.Width - 1; j >= 0; j--)
                    {
                        aMatrix[i, j] -= aMatrix[k, j] * (aMatrix[i, k] / aMatrix[k, k]);
                    }
                }
            }
            // Normalizing the matrices in order to the 1 for each X[i, i]
            for (int i = 0; i < aMatrix.Height; i++)
            {
                bMatrix[i, 0] /= aMatrix[i, i];
                aMatrix.NormalizeRow(i);
            }
            // Currently is not working for the Last two equations. We have the final results already before the reverse steps
            // 2. Reverse Gauss
            for (int i = aMatrix.Height - 1; i >= 0; i--)
            {
                if(i == aMatrix.Height - 1)
                    result[i, 0] /= aMatrix[i, i];
                for (int xi = aMatrix.Width - 1; xi > i; xi--)
                {
                    bMatrix[i, 0] -= aMatrix[i, xi] * result[xi, 0];
                    bMatrix[i, 0] /= aMatrix[i, i];
                    result[i, 0] = bMatrix[i, 0];
                }
            }

            return result;
        }
    }
}