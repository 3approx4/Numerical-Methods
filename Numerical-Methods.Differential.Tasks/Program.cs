using System;
using System.IO;
using Numerical_Methods.Algorithms.Differential;

namespace Numerical_Methods.Differential.Tasks
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Testing Euler's method");
            var b = 10;
            var step = 0.001;
            var y0 = 1;
            var result = EulerMethod.Solve(TaskFunction, y0, b, step);
            using (var file = File.OpenWrite("data_euler.csv"))
            {
                using (var writer = new StreamWriter(file))
                {
                    for (int i = 0; i < result.Length; i++)
                    {
                        writer.WriteLine($"{i*step},{result[i]}");
                    }
                }                
            }
            result = GearsMethod.Solve(TaskFunction, 3, b, y0, step);
            using (var file = File.OpenWrite("data_gear.csv"))
            {
                using (var writer = new StreamWriter(file))
                {
                    for (int i = 0; i < result.Length; i++)
                    {
                        writer.WriteLine($"{i * step},{result[i]}");
                    }
                }
            }

        }

        static double TestEquation(double y, double t)
        {
            return -3 * y + 2 * Math.Pow(t, 2);
        }

        static double TestFunction(double x, double t)
        {
            return 0.25 * x - 0.05 * Math.Pow(x, 2);
        }

        static double TaskFunction(double y, double t)
        {
            return 0.9 * y - 0.3 * Math.Pow(y, 2) + 0*t;
        }
    }
}