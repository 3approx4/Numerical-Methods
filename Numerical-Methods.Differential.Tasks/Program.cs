using System;
using System.IO;
using Numerical_Methods.Algorithms.Differential;

namespace Numerical_Methods.Differential.Tasks
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Testing differentiation equation solving method");
            float lowPrecision = 0.05f;
            float highPrecision = 0.01f;
            SolveTest(lowPrecision);
            SolveTest(highPrecision);
            SolveTask(lowPrecision);
            SolveTask(highPrecision);            
        }

        static double TestEquation(double y, double t)
        {
            return -3 * y + 2 * Math.Pow(t, 2);
        }

        static double TestFunction(double x, double t)
        {
            return 0.25 * x - 0.05 * Math.Pow(x, 2);
        }

        static float TaskFunction(float y, float t)
        {
            return 0.9f * y - 0.3f * (float)Math.Pow(y, 2) + 0*t;
        }

        static float RealTaskFunction(float x)
        {
            return (float)(3f * Math.Exp(0.9f * x) / (Math.Exp(0.9f * x) + 2f));
        }

        static float TestPlot(float y, float x)
        {
            return (float)Math.Pow(x, 2) - 2 * y;
        }
        static float RealPlot(float x)
        {
            return (float)(1 + 3 * Math.Exp(-2 * x) - 2 * x + 2 * Math.Pow(x, 2)) / 4f;
        }

        static void SolveTest(float step)
        {
            float b = 1f;
            float y0 = 1f;
            int order = (int)(b / step) + 1;
            float[] euler = EulerMethod.Solve(TestPlot, y0, b, step);
            float[] gear = GearsMethod.Solve(TestPlot, 3, b, y0, step);
            float[] real = new float[order];
            for (int i = 0; i < order; i++)
            {
                real[i] = RealPlot(i * step);
            }
            using (var file = File.OpenWrite($"test_equation_results_{step}.csv"))
            {
                using (var writer = new StreamWriter(file))
                {
                    writer.WriteLine("x,euler,gear,actual,eulerError,gearError");
                    for (int i = 0; i < order; i++)
                    {
                        writer.WriteLine($"{i * step},{euler[i]},{gear[i]},{real[i]},{Math.Abs(euler[i] - real[i])},{Math.Abs(gear[i] - real[i])}");
                    }
                }
            }
        }
        static void SolveTask(float step)
        {
            float b = 10f;
            float y0 = 1f;
            int order = (int)(b / step) + 1;
            float[] euler = EulerMethod.Solve(TaskFunction, y0, b, step);
            float[] gear = GearsMethod.Solve(TaskFunction, 3, b, y0, step);
            float[] real = new float[order];
            for (int i = 0; i < order; i++)
            {
                real[i] = RealTaskFunction(i * step);
            }
            using (var file = File.OpenWrite($"task_equation_results_{step}.csv"))
            {
                using (var writer = new StreamWriter(file))
                {
                    writer.WriteLine("x,euler,gear,actual,eulerError,gearError");
                    for (int i = 0; i < order; i++)
                    {
                        writer.WriteLine($"{i * step},{euler[i]},{gear[i]},{real[i]},{Math.Abs(euler[i] - real[i])},{Math.Abs(gear[i] - real[i])}");
                    }
                }
            }
        }
    }
}