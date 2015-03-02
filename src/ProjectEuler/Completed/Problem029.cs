using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;

namespace ProjectEuler
{
  public class Problem029 : EulerProblem
  {
    public override int Number
    {
      get { return 29; }
    }

    public override object Solve()
    {
      var values = new List<BigInteger>(100 * 100);
      for (var a = 2; a <= 100; ++a)
      {
        var n = new BigInteger(a);
        for (var b = 2; b <= 100; ++b)
        {
          var value = BigInteger.Pow(n, b);
          values.Add(value);
        }
      }
      return values.Distinct().Count();
    }
  }
}