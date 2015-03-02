using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;

namespace ProjectEuler
{
  public class Problem129 : EulerProblem
  {
    public override int Number
    {
      get { return 129; }
    }

    public override object Solve()
    {
      const int Limit = int.MaxValue - 1;
      const int Target = 1000000;

      for (var n = Target; n < Limit; ++n)
      {
        if (MathHelper.GCD(n, 10) != 1) { continue; }

        var k = A(n);
        if (k > Target)
        {
          return string.Format("n:{0} k:{1}", n, k);
        }
      }

      return "<Invalid>";
    }

    protected int A(int n)
    {
      var k = 1;
      var n9 = n * 9;

      for (; k < n; ++k)
      {
        if (BigInteger.ModPow(10, k, n9) == 1)
        {
          break;
        }
      }

      return k;
    }
  }
}