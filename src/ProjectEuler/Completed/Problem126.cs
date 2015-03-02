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
  public class Problem126 : EulerProblem
  {
    public override int Number { get { return 126; } }

    public override object Solve()
    {
      const int limit = 20000;

      int[] Cn = new int[limit + 1];

      // iterate over the possible cube sizes
      for (int x = 1; layer(x, x, x, 1) <= limit; ++x)
      {
        for (int y = x; layer(x, y, y, 1) <= limit; ++y)
        {
          for (int z = y; layer(x, y, z, 1) <= limit; ++z)
          {
            for (int n = 1; layer(x, y, z, n) <= limit; ++n)
            {
              Cn[layer(x, y, z, n)]++;
            }
          }
        }
      }

      // what is the index of the first value with 1000?
      return Array.IndexOf(Cn, 1000);
    }

    private static int layer(int x, int y, int z, int n)
    {
      return 4 * (x + y + z + n - 2) * (n - 1) + (2 * x * y) + (2 * x * z) + (2 * y * z);
    }
  }
}
