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

namespace ProjectEuler
{
  public class Problem053 : EulerProblem
  {
    public override int Number { get { return 53; } }

    public override object Solve()
    {
      BigInteger oneMillion = new BigInteger(1000000);
      int count = 0;

      for (int n = 1; n <= 100; ++n)
      {
        for (int r = 1; r < n; ++r)
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
