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
  public class Problem027 : EulerProblem
  {
    static Func<int, int, int, int> Quad = (a, b, n) => ((n * n) + (a * n) + b);
    static Problem027() { }

    public override int Number { get { return 27; } }

    public override object Solve()
    {
      int largestN = 0;
      int result = 0;

      var facts = new int[4][]
      {
        new int[] {1, 1},
        new int[] {1, -1},
        new int[] {-1, 1},
        new int[] {-1, -1},
      };

      for (int b = 1; b < 1000; ++b)
      {
        if (b.IsPrime())
        {
          for (int a = 1; a < 1000; ++a)
          {
            // retrieve the maximum value
            var max = facts
              .Select(f => new { A = a * f[0], B = b * f[1] })
              .Select(c => new { A = c.A, B = c.B, N = ConsecutivePrimeCount(c.A, c.B) })
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
      int n = 0;
      bool keepGoing = true;
      while (keepGoing)
      {
        int value = Math.Abs(Quad(a, b, n++));
        keepGoing = value.IsPrime();
      }
      return n;
    }
  }
}
