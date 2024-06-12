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
            try
            {
                Expression expression = new Expression(equation);
                object result = expression.Evaluate();
                if (!(expression.HasErrors()))
                    return result.ToString();
                // "default return" is expected.
            }
            catch (Exception)
            {

                return equation;
            }
           
            return equation;
        }
    }
}
