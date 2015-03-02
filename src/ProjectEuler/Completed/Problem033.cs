using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectEuler
{
  public class Problem033 : EulerProblem
  {
    public override int Number
    {
      get { return 33; }
    }

    public override object Solve()
    {
      decimal totalNumerator = 1;
      decimal totalDenominator = 1;

      // go through the numerators
      for (var num = 11; num < 99; ++num)
      {
        // retrieve the digits of the numerator
        var numDigits = num.ToString().ToArray();

        // if the numerator is a power of 10, then skip
        if (numDigits.Last() == '0') { continue; }

        // go through the denominators
        for (var den = num + 1; den < 100; ++den)
        {
          // retrieve the digits of the denominator
          var denDigits = den.ToString().ToArray();

          // if the denominator is a power of 10, then skip
          if (denDigits.Last() == '0') { continue; }

          // does num[0] == den[1] or num[1] == den[0]?
          var num0_den1 = numDigits[0] == denDigits[1];
          var num1_den0 = numDigits[1] == denDigits[0];
          if (num0_den1 || num1_den0)
          {
            // this means that the two fraction values cancel. Lets strip off the
            // number parts and do the division

            // retrieve the numerator
            var numDigit = num0_den1 ? numDigits[1] : numDigits[0];
            var numerator = Convert.ToDecimal(numDigit.ToString());

            // retrieve the denominator
            var denDigit = num0_den1 ? denDigits[0] : denDigits[1];
            var denominator = Convert.ToDecimal(denDigit.ToString());

            // divide the two numbers
            var simplifiedResult = numerator / denominator;
            var originalResult = decimal.Divide(num, den);

            // see if the results are equal
            if (simplifiedResult == originalResult)
            {
              // multiply the fraction together with the other fraction
              totalNumerator *= num;
              totalDenominator *= den;
            }
          }
        }
      }

      // reduce the fraction using gcd
      var gcd = MathHelper.GCD(totalNumerator, totalDenominator);
      totalDenominator /= gcd;
      totalNumerator /= gcd;

      // return the fraction
      return string.Format("{0} / {1}", totalNumerator, totalDenominator);
    }
  }
}