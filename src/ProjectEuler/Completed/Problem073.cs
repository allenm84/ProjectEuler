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
  public class Problem073 : EulerProblem
  {
    public override int Number { get { return 73; } }

    public override object Solve()
    {
      var scale = 100000;
      var min = (scale / 3);
      var max = (scale / 2);

      int count = 0;
      for (int d = 2; d <= 12000; ++d)
      {
        for (int n = 1; n < d; ++n)
        {
          var v = (n * scale) / d;
          if (min < v && v < max)
          {
            if (MathHelper.GCD(n, d) == 1)
            {
              ++count;
            }
          }
        }
      }

      return count;
    }
  }
}
