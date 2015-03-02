using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;

namespace ProjectEuler
{
  public class Problem063 : EulerProblem
  {
    public override int Number
    {
      get { return 63; }
    }

    public override object Solve()
    {
      var pow = 1;
      var count = 0;

      while (pow <= 100)
      {
        var n = 0;
        while (true)
        {
          ++n;
          var num = BigInteger.Pow(n, pow);
          var txt = num.ToString();
          if (txt.Length < pow) { continue; }
          if (txt.Length > pow) { break; }
          ++count;
        }
        ++pow;
      }

      return count;
    }
  }
}