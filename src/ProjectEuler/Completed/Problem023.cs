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
  public class Problem023 : EulerProblem
  {
    public override int Number { get { return 23; } }

    public override object Solve()
    {
      const int Limit = 28123;
      List<int> numbers = new List<int>();

      // first, let's get all of the abundant numbers
      List<int> abundantNumbers = new List<int>();
      for (int i = 0; i <= Limit; ++i)
      {
        // add the value
        numbers.Add(i);

        // retrieve the proper divisors
        var factors = i.Factors().Except(new int[] { i }).ToArray();

        // if there were results, and the sum of the results is greater
        // than i, then this is an abundant number
        if (factors.Length > 0 && factors.Sum() > i)
        {
          abundantNumbers.Add(i);
        }
      }

      // go through and add two abundant numbers
      int count = abundantNumbers.Count;
      Dictionary<int, bool> abundantSum = new Dictionary<int, bool>(count * count);
      for (int x = 0; x < count; ++x)
      {
        for (int y = 0; y < count; ++y)
        {
          int a = abundantNumbers[x];
          int b = abundantNumbers[y];
          abundantSum[a + b] = true;
        }
      }

      // keep a running total
      int total = 0;

      // retrieve the numbers
      for (int i = 0; i <= Limit; ++i)
      {
        // if the number can be written as the sum of two abundant numbers
        if (!abundantSum.ContainsKey(i))
        {
          total += i;
        }
      }

      // return the total
      return total;
    }
  }
}
