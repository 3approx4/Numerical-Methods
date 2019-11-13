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
            // ||deltaX|| / ||X|| <= Cond(A) * ||deltaB|| / ||B||
            
            // Calculate X
            Matrix result = GaussMethod.Solve(aMatrix, bMatrix);
            
            // Calculate delta B
            var deltaBMatrix = new Matrix(bMatrix.ToArray());
            deltaBMatrix[deltaBMatrix.GetRowIndexWithMaxAbsValue(0), 0] *= 1.01f;

            // Calculate delta X
            Matrix deltaResult = GaussMethod.Solve(aMatrix, deltaBMatrix);

            // Calculating condition number
            return deltaResult.GetManhattanDistance()
                * bMatrix.GetManhattanDistance() /
                (result.GetManhattanDistance()
                * deltaBMatrix.GetManhattanDistance());
        }
    }
}
