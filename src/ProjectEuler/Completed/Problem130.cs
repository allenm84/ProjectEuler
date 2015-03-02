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
using System.Common.Extensions;

namespace ProjectEuler
{
  public class Problem130 : Problem129
  {
    public override int Number { get { return 130; } }

    public override object Solve()
    {
      const int limit = int.MaxValue - 1;

      int sum = 0;
      int count = 0;

      for (int n = 2; n < limit; ++n)
      {
        if (n.IsPrime()) continue;
        if (MathHelper.GCD(n, 10) != 1) continue;

        int n1 = n - 1;
        int an = A(n);

        if ((n1 % an) != 0) continue;
        sum += n;

        ++count;
        if (count == 25) break;
      }

      return sum;
    }
  }
}
