using NCalc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GlobalMacroRecorder
{
    internal class UtilCalculate
    {
        public static string ComputeEquation(string equation)
        {
            Expression expression = new Expression(equation);
            object result = expression.Evaluate();
            if (!(expression.HasErrors()))
                return result.ToString();
            // "default return" is expected.
            return equation;
        }
    }
}
