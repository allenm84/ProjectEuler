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
  public class p0027 : euler
  {
    public override void Run()
    {
      var largestN = 0;
      var result = 0;

      var facts = new int[4][]
      {
        new[] {+1, +1},
        new[] {+1, -1},
        new[] {-1, +1},
        new[] {-1, -1}
      };

      int a, b;

      for (b = 1; b < 1000; ++b)
      {
        if (math.isPrime(b))
        {
          for (a = 1; a < 1000; ++a)
          {
            // retrieve the maximum value
            var max = facts
              .Select(f => new { A = a * f[0], B = b * f[1] })
              .Select(c => new { c.A, c.B, N = ConsecutivePrimeCount(c.A, c.B) })
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

      Console.WriteLine(result);
    }

    static int Quad(int a, int b, int n)
    {
      return ((n * n) + (a * n) + b);
    }

    static int ConsecutivePrimeCount(int a, int b)
    {
      var n = 0;
      var keepGoing = true;
      while (keepGoing)
      {
        var value = Math.Abs(Quad(a, b, n++));
        keepGoing = math.isPrime(value);
      }
      return n;
    }
  }
}
