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

        public override string ToString()
        {
            return string.Format("Itrations: {0};\n Value: {1}", Iterations, Value);
        }
    }
}
