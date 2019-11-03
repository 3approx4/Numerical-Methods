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
            Matrix orderedMatrixA = new Matrix(0, aMatrix.Width);
            Matrix orderedMatrixB = new Matrix(0, bMatrix.Width);
            for (int j = 0; j < aMatrix.Width; j++)
            {
                // Forward Step
                int maxElementRow = aMatrix.GetRowIndexWithMaxAbsValue(j);
            
                orderedMatrixA = new Matrix(0, aMatrix.Width);
                orderedMatrixA.PushRow(aMatrix.GetRow(maxElementRow));
                orderedMatrixB = new Matrix(0, bMatrix.Width);
                orderedMatrixB.PushRow(bMatrix.GetRow(maxElementRow));
                // todo Replace the code below with the ExcludeRow calls after push
                for (int i = 0; i < aMatrix.Height; i++)
                {
                    if (i != maxElementRow)
                    {
                        orderedMatrixA.PushRow(aMatrix.GetRow(i));
                        orderedMatrixB.PushRow(bMatrix.GetRow(i));
                    }
                }

                for (int i = 1; i < orderedMatrixA.Height; i++)
                {
                    // Calculate the value for each row to get rid of j-th coefficient and free the j-th variable
                    float c = -orderedMatrixA[j, j] / orderedMatrixA[i, j];
                
                    // Perform removal of the j-th variable in i-th row
                    float[] row = orderedMatrixA.MultiplyRow(i, c);
                    float[] targetRow = orderedMatrixA.GetRow(j);
                    for (int k = 0; k < targetRow.Length; k++)
                    {
                        targetRow[k] += row[k];
                    }
                    orderedMatrixA.Write( targetRow, i);
                
                    // Recalculate the free coefficient for i-th row
                    row = orderedMatrixB.MultiplyRow(i, c);
                    targetRow = orderedMatrixB.GetRow(j);
                    for (int k = 0; k < targetRow.Length; k++)
                    {
                        targetRow[k] += row[k];
                    }
                    orderedMatrixB.Write( targetRow, i);
                }
            }
            
            Matrix result = new Matrix(aMatrix.Height, bMatrix.Width);
            // Backward Step
            result.Write(new []
                {
                    orderedMatrixB.GetRow(aMatrix.Height - 1)[0] / orderedMatrixA.GetRow(aMatrix.Height - 1)[aMatrix.Width - 1]
                }, aMatrix.Height - 1);
            for (int i = aMatrix.Height - 2; i >= 0; i--)
            {
                float[] row = orderedMatrixB.GetRow(i);
                row[0] -= result.GetRow(i + 1)[0] * orderedMatrixB.GetRow(i + 1)[0];
                orderedMatrixB.Write(row, i);
                result.Write(new float[]
                {
                    row[0]/orderedMatrixB.GetRow(i + 1)[0]
                }, i);
            }
            return result;
        }
    }
}