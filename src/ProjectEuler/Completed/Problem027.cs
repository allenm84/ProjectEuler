using System;
using System.Collections.Generic;
using System.Common.Extensions;
using System.Linq;
using System.Text;

namespace ProjectEuler
{
  public class Problem027 : EulerProblem
  {
    private static Func<int, int, int, int> Quad = (a, b, n) => ((n * n) + (a * n) + b);
    static Problem027() {}

    public override int Number
    {
      get { return 27; }
    }

    public override object Solve()
    {
      var largestN = 0;
      var result = 0;

      var facts = new int[4][]
      {
        new[] {1, 1},
        new[] {1, -1},
        new[] {-1, 1},
        new[] {-1, -1}
      };

      for (var b = 1; b < 1000; ++b)
      {
        if (b.IsPrime())
        {
          for (var a = 1; a < 1000; ++a)
          {
            // retrieve the maximum value
            var max = facts
              .Select(f => new {A = a * f[0], B = b * f[1]})
              .Select(c => new {c.A, c.B, N = ConsecutivePrimeCount(c.A, c.B)})
              .OrderByDescending(c => c.N)
              .First();

            // if the max N is greater, then set the result
            if (max.N > largestN)
            {
              largestN = max.N;
              result = max.A * max.B;
            }
          }
        }
      }

      return result;
    }

    private int ConsecutivePrimeCount(int a, int b)
    {
      var n = 0;
      var keepGoing = true;
      while (keepGoing)
      {
        var value = Math.Abs(Quad(a, b, n++));
        keepGoing = value.IsPrime();
      }
      return n;
    }
  }
}