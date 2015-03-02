using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectEuler
{
  public class Problem069 : EulerProblem
  {
    public override int Number
    {
      get { return 69; }
    }

    public override object Solve()
    {
      decimal maxValue = 3;
      var maxN = 6;

      for (var n = 11; n <= 1000000; ++n)
      {
        var value = decimal.Divide(n, MathHelper.Phi(n));
        if (value > maxValue)
        {
          maxValue = value;
          maxN = n;
        }
      }

      return maxN;
    }
  }
}