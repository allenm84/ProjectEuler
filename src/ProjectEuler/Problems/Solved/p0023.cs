using System;
using System.Advanced;
using System.Advanced.Combinations;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ProjectEuler
{
  public class p0023 : euler
  {
    public override void Run()
    {
      const int Limit = 28123;
      var numbers = new List<int>();

      // create iteration variables
      int i, a, b, x, y;

      // first, let's get all of the abundant numbers
      var abundantNumbers = new List<int>();
      for (i = 0; i <= Limit; ++i)
      {
        // add the value
        numbers.Add(i);

        // retrieve the proper divisors
        var factors = new HashSet<int>(math.divisors(i));
        factors.Remove(i);

        // if there were results, and the sum of the results is greater
        // than i, then this is an abundant number
        if (factors.Count > 0 && factors.Sum() > i)
        {
          abundantNumbers.Add(i);
        }
      }

      // go through and add two abundant numbers
      var count = abundantNumbers.Count;
      var abundantSum = new HashSet<int>();
      for (x = 0; x < count; ++x)
      {
        for (y = 0; y < count; ++y)
        {
          a = abundantNumbers[x];
          b = abundantNumbers[y];
          abundantSum.Add(a + b);
        }
      }

      // keep a running total
      var total = 0;

      // retrieve the numbers
      for (i = 0; i <= Limit; ++i)
      {
        // if the number can be written as the sum of two abundant numbers
        if (!abundantSum.Contains(i))
        {
          total += i;
        }
      }

      // return the total
      Console.WriteLine(total);
    }
  }
}
