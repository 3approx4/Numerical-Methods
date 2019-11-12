using Numerical_Methods.Libs;

namespace Numerical_Methods.Algorithms
{
    public class RelaxationMethod
    {
        public static Matrix Solve(Matrix aMatrix, Matrix bMatrix, float relaxation = 0.9f, int iterations = 5)
        {
            int i, j, n = aMatrix.Height;

            Matrix x = bMatrix;
            float delta;

            // Gauss-Seidel with Successive OverRelaxation Solver
            for (int k = 0; k < iterations; ++k)
            {
                for (i = 0; i < n; ++i)
                {
                    delta = 0.0f;

                    for (j = 0;   j < i; ++j) delta += aMatrix[i,j]*x[j,0 ];
                    for (j = i + 1; j < n; ++j) delta += aMatrix[i, j]*x[j, 0];

                    delta = (bMatrix[i, 0] - delta) / aMatrix[i, i];
                    x[i, 0] += relaxation * (delta - x[i, 0]);
                }
            }

            return x;
        }
    }
}