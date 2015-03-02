using System;
using System.Collections.Generic;
using System.Common.Extensions;
using System.Linq;
using System.Text;

namespace ProjectEuler
{
  public class Problem023 : EulerProblem
  {
    public override int Number
    {
      get { return 23; }
    }

    public override object Solve()
    {
      const int Limit = 28123;
      var numbers = new List<int>();

      // first, let's get all of the abundant numbers
      var abundantNumbers = new List<int>();
      for (var i = 0; i <= Limit; ++i)
      {
        // add the value
        numbers.Add(i);

        // retrieve the proper divisors
        var factors = i.Factors().Except(new[] {i}).ToArray();

        // if there were results, and the sum of the results is greater
        // than i, then this is an abundant number
        if (factors.Length > 0 && factors.Sum() > i)
        {
          abundantNumbers.Add(i);
        }
      }

      // go through and add two abundant numbers
      var count = abundantNumbers.Count;
      var abundantSum = new Dictionary<int, bool>(count * count);
      for (var x = 0; x < count; ++x)
      {
        for (var y = 0; y < count; ++y)
        {
          var a = abundantNumbers[x];
          var b = abundantNumbers[y];
          abundantSum[a + b] = true;
        }
      }

      // keep a running total
      var total = 0;

      // retrieve the numbers
      for (var i = 0; i <= Limit; ++i)
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