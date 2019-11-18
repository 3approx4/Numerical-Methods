using Numerical_Methods.Libs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Numerical_Methods.Algorithms
{
    public static class ConditionNumberCalculator
    {
        public static float Calculate(Matrix aMatrix, Matrix bMatrix)
        {
            // Save matrices A and B for the future calculations, as Gauss method is reference based
            Matrix bMatrixClone = Matrix.Clone(bMatrix);
            Console.WriteLine("B matrix: " + bMatrixClone.ToString());
            Matrix aMatrixClone = Matrix.Clone(aMatrix);
            // Create delta B matrix
            Matrix bMatrixDelta = new Matrix(bMatrix.Height, bMatrix.Width);
            // Get the row number of the max abs value
            int maxRowIndex = bMatrix.GetRowIndexWithMaxAbsValue(0);
            // Calculate the error and store in in delta matrix, other values are 0
            bMatrixDelta[maxRowIndex, 0] = bMatrix[maxRowIndex, 0] * 0.01f;
            // Get the matrix B for calculating result with introduced error of 1%
            Matrix newMatrixB = Matrix.Clone(bMatrix);
            // Add error of 1%
            newMatrixB[maxRowIndex, 0] += bMatrixDelta[maxRowIndex, 0];

            Console.WriteLine("\u0394 B: " + bMatrixDelta.ToString());
            Console.WriteLine("B + \u0394 B: " + newMatrixB.ToString());
            // Solve default equation
            Matrix defaultResult = GaussMethod.Solve(aMatrix, bMatrix);
            Console.WriteLine("X: " + defaultResult.ToString());
            // Solve equation with changed B matrix
            Matrix changedResult = GaussMethod.Solve(aMatrixClone, newMatrixB);
            Console.WriteLine("X(B + \u0394 B) result: " + changedResult.ToString());
            // Create matrix for storing the delta X
            Matrix resultDelta = new Matrix(defaultResult.Height, defaultResult.Width);
            // Calculate the error of each X value
            for(int j = 0; j < resultDelta.Height; j++)
            {
                resultDelta[j, 0] = defaultResult[j, 0] - changedResult[j, 0];
            }
            Console.WriteLine("\u0394 X:" + resultDelta.ToString());
            // Calculate the relative error norm
            float resultErrorNorm = resultDelta.GetManhattanDistance() / defaultResult.GetManhattanDistance();
            Console.WriteLine("\u0394 X norm / X norm: " + resultErrorNorm);
            // Calculate the relative norm of B values
            float bMatrixNorm = bMatrixClone.GetManhattanDistance() / bMatrixDelta.GetManhattanDistance();
            Console.WriteLine("B norm / \u0394 B norm: " + resultErrorNorm);
            return resultErrorNorm / bMatrixNorm;
        }
    }
}
