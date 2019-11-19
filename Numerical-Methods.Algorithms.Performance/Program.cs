using Numerical_Methods.Libs;
using System;
using System.Diagnostics;

namespace Numerical_Methods.Algorithms.Performance
{
    class Program
    {
        static void Main(string[] args)
        {
            Matrix variableCoefficients = new Matrix(new float[,]
            {
                { 1, -2, 1 },
                { 2, -5, -1 },
                { -7, 0, 1 }
            });
            Matrix freeCoefficients = new Matrix(new float[,]
            {
                { 2 },
                { -1 },
                { -2 }
            });

            Console.WriteLine("Performance measurement tool");
            Console.Write("Enter iteration count for tests: ");
            int iterations = Convert.ToInt32(Console.ReadLine());
            do
            {
                Test(iterations, variableCoefficients, freeCoefficients);
            } while (true);
        }

        static void Test(int iterations, Matrix variableCoefficients, Matrix freeCoefficients)
        {
            float epsilon = 0.0001f;

            float relaxationWeight = 0.8f;
            var stopwatch = new Stopwatch();
            Console.Write("######################################################\nSelect the algorithm:\n1.Gauss\n2.Jacobi\n3.Gauss-Seidel\n4.Relaxation\nEnter the choice:");
            int algorithm = Convert.ToInt32(Console.ReadLine());
            switch (algorithm)
            {
                case 1:
                    {
                        stopwatch.Start();
                        for (int i = 0; i < iterations; i++)
                        {
                            var result = GaussMethod.Solve(Matrix.Clone(variableCoefficients), Matrix.Clone(freeCoefficients));
                        }
                        stopwatch.Stop();
                        Console.WriteLine("Total elapsed time: " + stopwatch.Elapsed + "\n######################################################");
                        break;
                    }
                case 2:
                    {
                        stopwatch.Start();
                        for (int i = 0; i < iterations; i++)
                        {
                            var result = JacobiMethod.Solve(Matrix.Clone(variableCoefficients), Matrix.Clone(freeCoefficients), epsilon);
                        }
                        stopwatch.Stop();
                        Console.WriteLine("Total elapsed time: " + stopwatch.Elapsed);
                        break;
                    }
                case 3:
                    {
                        stopwatch.Start();
                        for (int i = 0; i < iterations; i++)
                        {
                            var result = GaussZeidelMethod.Solve(Matrix.Clone(variableCoefficients), Matrix.Clone(freeCoefficients), epsilon);
                        }
                        stopwatch.Stop();
                        Console.WriteLine("Total elapsed time: " + stopwatch.Elapsed);
                        break;
                    }
                case 4:
                    {
                        stopwatch.Start();
                        for (int i = 0; i < iterations; i++)
                        {
                            var result = RelaxationMethod.Solve(Matrix.Clone(variableCoefficients), Matrix.Clone(freeCoefficients), epsilon, relaxationWeight);
                        }
                        stopwatch.Stop();
                        Console.WriteLine("Total elapsed time: " + stopwatch.Elapsed);
                        break;
                    }
            }
        }
    }
}
