using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;

namespace ProjectEuler
{
  public class Problem020 : EulerProblem
  {
    public override int Number
    {
      get { return 20; }
    }

    public override object Solve()
    {
      var prod = BigInteger.One;
      for (var i = 100; i > 0; --i)
      {
        var n = new BigInteger(i);
        prod *= n;
      }

      return prod
        .ToString()
        .Select(c => ((int)c) - 48)
        .Sum();
    }
  }
}