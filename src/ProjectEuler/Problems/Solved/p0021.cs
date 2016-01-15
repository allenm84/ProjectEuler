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
  public class p0021 : euler
  {
    const int Len = 10000;
    const int Start = 4;

    public override void Run()
    {
      // retrieve the divisors for each number
      var table = new int[Len][];
      for (var i = 0; i < Len; ++i)
      {
        var set = new HashSet<int>(math.divisors(i));
        set.Remove(i);
        table[i] = set.ToArray();
      }

      // create a list to hold the amicable numbers
      var amicableNumbers = new HashSet<int>();

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
      Console.WriteLine(amicableNumbers.Sum());
    }
  }
}
