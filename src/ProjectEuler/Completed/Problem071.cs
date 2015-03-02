using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectEuler
{
  public class Problem071 : EulerProblem
  {
    public override int Number
    {
      get { return 71; }
    }

    public override object Solve()
    {
      var target = MathHelper.DblDiv(3, 7);
      double minDiff = int.MaxValue;

      var N = 0;
      var D = 0;

      for (var d = 1000000; d > 1; --d)
      {
        // (d/2)/n is .5 which is too big. Basically,
        // for a given d value, we want to choose n such
        // that n/d is closest to 3/7.
        // n/d = 3/7
        // n = 3 * d / 7
        var n = 3 * d / 7;

        // if this is the target value, then continue
        if (n == 3 && d == 7) { continue; }

        // calculate the decimal value
        var value = MathHelper.DblDiv(n, d);

        // if the value is larger than the target, then continue
        if (value > target) { continue; }

        // set the min diff
        if (MathHelper.GCD(n, d) == 1)
        {
          var diff = target - value;
          if (diff < minDiff)
          {
            minDiff = diff;
            N = n;
            D = d;
          }
        }
      }

      return string.Format("{0} / {1}", N, D);
    }
  }
}