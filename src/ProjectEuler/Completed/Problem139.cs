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
using System.Collections;
using System.Data;

namespace ProjectEuler
{
  public class Problem139 : EulerProblem
  {
    public override int Number { get { return 139; } }

    public override object Solve()
    {
      const int MaxPerimeter = 100000000;

      // calculate the maximum limit
      int limit = (int)Math.Sqrt(MaxPerimeter / 2);

      // pre-generate the m values
      var values = new int[limit];
      for (int i = 1; i < limit; ++i)
      {
        values[i] = i * i;
      }

      // create a variable to store the count
      BigInteger count = 0;

      // generate the valid pythagorean triples
      for (int n = 1; n < limit; ++n)
      {
        // calculate n*n
        int nn = values[n];

        for (int m = n + 1; m < limit; ++m)
        {
          // if not odd, then keep going
          if ((n + m) % 2 == 0) continue;

          // if not coprime, then keep going
          if (MathHelper.GCD(m, n) != 1) continue;

          // calculate m*m
          int mm = values[m];

          // calculate a,b and c
          int a = mm - nn;
          int b = 2 * m * n;
          int c = mm + nn;

          // now, cycle through the k values
          for (int k = 1; true; ++k)
          {
            // calculate a,b and c
            int ak = a * k;
            int bk = b * k;
            int ck = c * k;

            // determine the perimeter
            if ((ak + bk + ck) >= MaxPerimeter) break;

            // subtract the two
            int d = Math.Abs(bk - ak);

            // is this valid?
            if ((ck % d) == 0) ++count;
          }
        }
      }

      // return the count
      return count;
    }
  }
}
