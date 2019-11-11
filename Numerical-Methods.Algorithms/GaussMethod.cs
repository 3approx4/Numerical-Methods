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
            Matrix result = new Matrix(3, 1);

            // 1. Forward Gauss
            for (int i = 0; i < aMatrix.Height; i++)
            { 
                // Currecnt line
                var m = i;

                for (int l = i; l < aMatrix.Height; l++)
                    if (Math.Abs(aMatrix[m, i]) < Math.Abs(aMatrix[l, i]))
                        m = l;

                if (m != i)
                {
                    float tmp;

                    for (int s = 0; s < aMatrix.Width; s++)
                    {
                        tmp = aMatrix[i, s];
                        aMatrix[i, s] = aMatrix[m, s];
                        aMatrix[m, s] = tmp;
                    }

                    tmp = bMatrix[i, 0];
                    bMatrix[i, 0] = bMatrix[m, 0];
                    bMatrix[m, 0] = tmp;
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

            // 2. Reverse Gauss
            for (int i = aMatrix.Height - 1; i >= 0; i--)
            {
                result[i, 0] = bMatrix[i, 0] / aMatrix[i, i];
                for (int xi = aMatrix.Height - 1; xi > i; xi--)
                {
                    result[i, 0] -= ((aMatrix[i, xi] * result[xi, 0]) / aMatrix[i, i]);
                }
                Console.WriteLine(string.Format(" x{0} = {1}", i + 1, result[i]));
            }

            return result;
        }
    }
}