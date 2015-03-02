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
  public class Problem086 : EulerProblem
  {
    public override int Number { get { return 86; } }

    public override object Solve()
    {
      const int Target = 1000000;

      int count = 0;
      int M = 100;
      for (; count <= Target; ++M)
      {
        count = CountPaths(M);
      }

      return string.Format("{0} = {1}", M - 1, count);
    }

    private int CountPaths(int M)
    {
      int count = 0;
      for (int n = 1; n < M; ++n)
      {
        int nn = n * n;
        for (int m = n + 1; m < M; ++m)
        {
          // if not odd, then keep going
          if ((n + m) % 2 == 0) continue;

          // if not coprime, then keep going
          if (MathHelper.GCD(m, n) != 1) continue;

          // calculate the (a,b) part of a^2 + b^2 = c^2. Since
          // we already know that c^2 is the smallest distance (so,
          // no need to do m^2 + n^2)
          int x = 2 * n * m;
          int y = (m * m) - nn;

          // if a and b are too large, then stop
          if ((x > M && y > M)) break;

          // go through the k values. We multiply a and b by k
          // to generate the pythagorean triples
          for (int k = 1; true; ++k)
          {
            // a, b are 2/3 of a pythagorean triple
            int a = x * k;
            int b = y * k;

            // a and b are too large, then stop
            if (a > M && b > M) break;

            // calculate the values
            if (b <= M) { count += Solutions(a, b); }
            if (a <= M) { count += Solutions(b, a); }
          }
        }
      }

      return count;
    }

    private int Solutions(int a, int b)
    {
      /*
       * For any given cuboid, measuring pxqxr, there are three "shortest" paths. By 
       * drawing the net of a cuboid you can see that the distances will be: 
       * 
       * sqrt(p^2+(q+r)^2) 
       * sqrt(q^2+(r+p)^2) 
       * sqrt(r^2+(p+q)^2) 
       * 
       * If p<=q<=r, then d=sqrt(r^2+(p+q)^2) will be the shortest route (I won't provide the 
       * proof unless someone wants to see it). 
       * 
       * So we're looking for Pythagorean triplets: (r,(p+q),d). 
       * 
       * Using d=x^2+y^2, b=max(x^2-y^2,2xy), a=min(x^2-y^2,2xy); note that we don't need to 
       * consider d, which is the shortest distance. We also ensure that only primitive cases 
       * are used by checking that (x+y) is odd and HCF(x,y)=1; after this we can consider 
       * multiples of the triplets that do not exceed M. 
       * 
       * For any given triplet, (a,b,c), where a<=M and a<b, there are [a/2]-(b-a)+1 solutions 
       * (if this expression is positive), and if b<=M, there are [a/2] extra solutions.
       */

      int diff = a - b;
      int exp = ((a >> 1) + 1) - (diff > 0 ? diff : 1);
      return exp > 0 ? exp : 0;
    }
  }
}
