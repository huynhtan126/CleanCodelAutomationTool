using NCalc;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;

namespace GlobalMacroRecorder
{
    internal class UtilCalculate
    {
        public static string ComputeEquation(string equation, bool isPlus= false)
        {
            try
            {
                Expression expression = new Expression(equation);
                object result = expression.Evaluate();
              
                if (!(expression.HasErrors()))
                {
                    if (isPlus) {
                        double.TryParse(result.ToString(), out var checkPlus);
                        if(checkPlus<0)
                        {
                            checkPlus = 0;
                        }    
                    }
                    return result.ToString();
                }    
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
