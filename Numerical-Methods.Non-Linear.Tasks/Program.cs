using System;
using Numerical_Methods.Algorithms.Non_Linear;

namespace Numerical_Methods.Non_Linear.Tasks
{
    class Program
    {
        static void Main(string[] args)
        {
            var lb = 0.01f;
            var ub = 0.0001f;
            Console.WriteLine("Bisection methods:");
            Console.WriteLine("#5");
            Console.WriteLine(BisectionMethod.Evaluate(Function_5, 2, 3, lb));
            Console.WriteLine(BisectionMethod.Evaluate(Function_5, 2, 3, ub));
            Console.WriteLine("#9");
            Console.WriteLine(BisectionMethod.Evaluate(Function_9, 0, (float)Math.PI * (3f / 2f), lb));
            Console.WriteLine(BisectionMethod.Evaluate(Function_9, 0, (float)Math.PI * (3f / 2f), ub));
            Console.WriteLine("#11");
            Console.WriteLine(BisectionMethod.Evaluate(Function_11, 0.000000000001f, 2, lb));
            Console.WriteLine(BisectionMethod.Evaluate(Function_11, 0.000000000001f, 2, ub));
            Console.WriteLine("#15");
            Console.WriteLine(BisectionMethod.Evaluate(Function_15, 0.2f, 2f, lb));
            Console.WriteLine(BisectionMethod.Evaluate(Function_15, 0.2f, 2f, ub));
            Console.WriteLine("Newton method");
            Console.WriteLine("#5");
            Console.WriteLine(NewtonMethod.Evaluate(Function_5, 2, 3, lb));
            Console.WriteLine(NewtonMethod.Evaluate(Function_5, 2, 3, ub));
            Console.WriteLine("#9");
            Console.WriteLine(NewtonMethod.Evaluate(Function_9, 0, (float)Math.PI * (3f / 2f), lb));
            Console.WriteLine(NewtonMethod.Evaluate(Function_9, 0, (float)Math.PI * (3f / 2f), ub));
            Console.WriteLine("#11");
            Console.WriteLine(NewtonMethod.Evaluate(Function_11, 0.000000000001f, 2, lb));
            Console.WriteLine(NewtonMethod.Evaluate(Function_11, 0.000000000001f, 2, ub));
            Console.WriteLine("#15");
            Console.WriteLine(NewtonMethod.Evaluate(Function_15, 0.2f, 2f, lb));
            Console.WriteLine(NewtonMethod.Evaluate(Function_15, 0.2f, 2f, ub));
        }
        
        
        public static double Function_15(double x)
        {
            return Math.Sqrt(4 - Math.Pow(x, 2)) / x;
        }

        public static double Function_5(double x)
        {
            return x * Math.Exp(x * 0.8f);
        }


        public static double Function_9(double x)
        {
            return Math.Pow(x, 2) + Math.Sin(x);
        }


        public static double Function_11(double x)
        {
            return x / Math.Pow(x + 2, 2);
        }
    }
}