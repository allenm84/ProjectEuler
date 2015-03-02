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
using System.IO;

namespace ProjectEuler
{
  public class Problem078 : EulerProblem
  {
    public override int Number { get { return 78; } }

    public override object Solve()
    {
      const int Limit = 1000000;

      // create an array to hold the partition count
      int[] p = new int[Limit];
      p[0] = 1;

      // we start at n = 1
      int n = 1;

      // while p(n) isn't what we're looking for
      while (p[n - 1] % Limit != 0)
      {
        // initialize
        int i = 1;
        int x = 1;
        p[n] = 0;

        // generate pentagonal numbers while they're <= n
        int pentx;
        while ((pentx = MathHelper.FPentagon(x)) <= n)
        {
          // add on the pentagonal number, plus the p(n) from the last
          // n value. If i is positive, multiply by -1
          p[n] += p[n - pentx] * ((i + 1 & 2) - 1);

          // here, we modulate by the limit to keep p(n) low
          p[n] %= Limit;

          // update the x factor
          x = (x > 0 ? -x : (-x) + 1);

          // increment i
          i++;
        }

        // go to the next n
        n++;
      }
      return (n - 1);
    }
  }
}
