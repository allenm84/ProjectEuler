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
  public class Problem129 : EulerProblem
  {
    public override int Number { get { return 129; } }

    public override object Solve()
    {
      const int Limit = int.MaxValue - 1;
      const int Target = 1000000;

      for (int n = Target; n < Limit; ++n)
      {
        if (MathHelper.GCD(n, 10) != 1) continue;

        int k = A(n);
        if (k > Target)
        {
          return string.Format("n:{0} k:{1}", n, k);
        }
      }

      return "<Invalid>";
    }

    protected int A(int n)
    {
      int k = 1;
      int n9 = n * 9;

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
