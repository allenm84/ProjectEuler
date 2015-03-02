using System;
using System.Collections.Generic;
using System.Common.Extensions;
using System.Linq;
using System.Text;

namespace ProjectEuler
{
  public class Problem021 : EulerProblem
  {
    public override int Number
    {
      get { return 21; }
    }

    public override object Solve()
    {
      const int Len = 10000;
      const int Start = 4;

      // retrieve the divisors for each number
      var table = new int[Len][];
      for (var i = 0; i < Len; ++i)
      {
        table[i] = i.Factors().Except(new[] {i}).ToArray();
      }

      // create a list to hold the amicable numbers
      var amicableNumbers = new List<int>();

      // go through the numbers to see if they're amicable
      for (var a = Start; a < Len; ++a)
      {
        var factors = table[a];
        var b = factors.Sum();
        if (-1 < b && b < table.Length)
        {
          var sumFactors = table[b];
          var n = sumFactors.Sum();
          if (n == a && a != b)
          {
            // the two numbers are amicable, so add them together
            amicableNumbers.Add(b);
            amicableNumbers.Add(a);
          }
        }
      }

      // return the sum
      return amicableNumbers.Distinct().Sum();
    }
  }
}