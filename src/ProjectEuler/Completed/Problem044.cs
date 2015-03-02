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
  public class Problem044 : EulerProblem
  {
    public override int Number { get { return 44; } }

    public override object Solve()
    {
      // create a list to hold the diff values
      int D = 0;
      int a = 0;
      int b = 0;

      // go through the pentagonal numbers
      for (int j = 1; j < short.MaxValue; ++j)
      {
        var Pj = MathHelper.FPentagon(j);
        for (int k = 1; k < short.MaxValue; ++k)
        {
          if (k == j) continue;
          var Pk = MathHelper.FPentagon(k);

          var sum = Pj + Pk;
          var diff = Math.Abs(Pj - Pk);

          if (MathHelper.IsPentagonal(sum) && MathHelper.IsPentagonal(diff))
          {
            D = diff;
            a = Math.Min(Pk, Pj);
            b = Math.Max(Pk, Pj);

            k = short.MaxValue;
            j = short.MaxValue;
          }
        }
      }

      return string.Format("{0} - {1} = {2}", b, a, D);
    }
  }
}
