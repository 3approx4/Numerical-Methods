using System;
using Numerical_Methods.Libs;

namespace Numerical_Methods.Algorithms
{
    public class JacobiMethod
    {
        public static Matrix Solve(Matrix aMatrix, Matrix bMatrix, float epsilon)
        {
            Matrix x = new Matrix(bMatrix.ToArray());
            Matrix nextX = new Matrix(bMatrix.Height, bMatrix.Width);
            int k = 0;
            float norm = 0;
            do {
                for (int i = 0; i < aMatrix.Height; ++i) {
                    float sum = 0;
                    for (int j = 0; j < aMatrix.Height; ++j) {
                        if (i != j) {
                            sum = sum + aMatrix[i, j] * x[j, 0];
                        }
                    }
                    nextX[i, 0] = (bMatrix[i, 0] - sum) / aMatrix[i, i];
                }

                norm = Math.Abs(nextX[1, 0] - x[1, 0]);
                for (int i = 0; i < aMatrix.Height; ++i) {
                    if (Math.Abs(nextX[i, 0] - x[i, 0]) > norm) {
                        norm = Math.Abs(nextX[i, 0] - x[i, 0]);
                    }
                    x[i] = nextX[i];
                }
                k++;
            } while (norm > epsilon);
            for (int i = 1; i <= aMatrix.Height - 1; ++i){
                for (int j = 1; j <= aMatrix.Height - 1; ++j) {
                    bMatrix[i, 0] = bMatrix[i, 0] - aMatrix[i, j] * x[j, 0];
                }

                if (norm < Math.Abs(bMatrix[i, 0])) {
                    norm = Math.Abs(bMatrix[i, 0]);
                }
            }
            
            Console.WriteLine(norm);
            return x;
        }
    }
}