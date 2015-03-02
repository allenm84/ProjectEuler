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
  public class Problem132 : EulerProblem
  {
    public override int Number { get { return 132; } }

    public override object Solve()
    {
      const long Maximum = long.MaxValue - 1;
      const long Exp = 1000000000;

      List<long> factors = new List<long>();
      for (long p = 1; p < Maximum; ++p)
      {
        if (!p.IsPrime()) continue;
        if (BigInteger.ModPow(10, Exp, 9 * p) == 1)
        {
          factors.Add(p);
          if (factors.Count == 40) break;
        }
      }

      BigInteger sum = 0;
      foreach (var f in factors)
        sum += f;

      return sum;
    }
  }
}
