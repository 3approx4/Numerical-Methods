using System;
using System.Collections.Generic;
using System.Text;

namespace Numerical_Methods.Libs
{
    public class Result
    {
        public int Iterations { get; set; }
        public double Value { get; set; }

        public Result(int iterations, double value)
        {
            Iterations = iterations;
            Value = value;
        }
    }
}
