using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectEuler
{
  public class Problem044 : EulerProblem
  {
    public override int Number
    {
      get { return 44; }
    }

    public override object Solve()
    {
      // create a list to hold the diff values
      var D = 0;
      var a = 0;
      var b = 0;

      // go through the pentagonal numbers
      for (var j = 1; j < short.MaxValue; ++j)
      {
        var Pj = MathHelper.FPentagon(j);
        for (var k = 1; k < short.MaxValue; ++k)
        {
          if (k == j) { continue; }
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