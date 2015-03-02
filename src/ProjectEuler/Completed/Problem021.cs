using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProjectEuler.Properties;
using System.Threading.Tasks;
using System.Numerics;
using System.Threading;
using Facet.Combinatorics;
using System.Diagnostics;
using System.Common.Extensions;

namespace ProjectEuler
{
  public class Problem021 : EulerProblem
  {
    public override int Number { get { return 21; } }

    public override object Solve()
    {
      const int Len = 10000;
      const int Start = 4;

      // retrieve the divisors for each number
      int[][] table = new int[Len][];
      for (int i = 0; i < Len; ++i)
      {
        table[i] = i.Factors().Except(new int[] { i }).ToArray();
      }

      // create a list to hold the amicable numbers
      List<int> amicableNumbers = new List<int>();

      // go through the numbers to see if they're amicable
      for (int a = Start; a < Len; ++a)
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
