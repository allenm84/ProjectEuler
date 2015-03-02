using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;

namespace ProjectEuler
{
  public class Problem053 : EulerProblem
  {
    public override int Number
    {
      get { return 53; }
    }

    public override object Solve()
    {
      var oneMillion = new BigInteger(1000000);
      var count = 0;

      for (var n = 1; n <= 100; ++n)
      {
        for (var r = 1; r < n; ++r)
        {
          var value = MathHelper.nCr(n, r);
          if (value > oneMillion)
          {
            ++count;
          }
        }
      }

      return count;
    }
  }
}