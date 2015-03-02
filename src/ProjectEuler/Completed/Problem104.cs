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
  public class Problem104 : EulerProblem
  {
    public override int Number { get { return 104; } }

    public override object Solve()
    {
      const int NineDigits = 1000000000;

      const double LogPhi = 0.20898764024997873376927208923;
      const double Log5Over2 = 0.34948500216800940239313055263;

      int[] f = new int[2];
      f[0] = 0;
      f[1] = 1;

      for (int k = 2; true; ++k)
      {
        int n = f[0] + f[1];
        if(n > NineDigits)
        {
          n %= NineDigits;
        }

        if (n.IsPanDigital())
        {
          var value = (k * LogPhi) - Log5Over2;
          var rem = value - Math.Floor(value);

          var count = (int)(Math.Floor(value) + 1);
          var pow = Math.Pow(10, rem);

          while (pow < NineDigits)
          {
            pow *= 10.0;
          }

          var x = (int)(pow / 10.0);
          if (x.IsPanDigital())
          {
            return k;
          }
        }

        f[0] = f[1];
        f[1] = n;
      }
    }
  }
}
