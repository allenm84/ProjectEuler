using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectEuler
{
  public class Problem126 : EulerProblem
  {
    public override int Number
    {
      get { return 126; }
    }

    public override object Solve()
    {
      const int limit = 20000;

      var Cn = new int[limit + 1];

      // iterate over the possible cube sizes
      for (var x = 1; layer(x, x, x, 1) <= limit; ++x)
      {
        for (var y = x; layer(x, y, y, 1) <= limit; ++y)
        {
          for (var z = y; layer(x, y, z, 1) <= limit; ++z)
          {
            for (var n = 1; layer(x, y, z, n) <= limit; ++n)
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