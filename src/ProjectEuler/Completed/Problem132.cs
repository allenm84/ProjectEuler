using System;
using System.Collections.Generic;
using System.Common.Extensions;
using System.Linq;
using System.Numerics;
using System.Text;

namespace ProjectEuler
{
  public class Problem132 : EulerProblem
  {
    public override int Number
    {
      get { return 132; }
    }

    public override object Solve()
    {
      const long Maximum = long.MaxValue - 1;
      const long Exp = 1000000000;

      var factors = new List<long>();
      for (long p = 1; p < Maximum; ++p)
      {
        if (!p.IsPrime()) { continue; }
        if (BigInteger.ModPow(10, Exp, 9 * p) == 1)
        {
          factors.Add(p);
          if (factors.Count == 40) { break; }
        }
      }

      BigInteger sum = 0;
      foreach (var f in factors)
      {
        sum += f;
      }

      return sum;
    }
  }
}