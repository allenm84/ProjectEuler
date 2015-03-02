using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;

namespace ProjectEuler
{
  public class Problem025 : EulerProblem
  {
    public override int Number
    {
      get { return 25; }
    }

    public override object Solve()
    {
      var f1 = BigInteger.One;
      var f2 = BigInteger.One;

      var keepGoing = true;
      var n = 2;

      while (keepGoing)
      {
        var sum = f1 + f2;
        f1 = f2;
        f2 = sum;
        ++n;

        var text = sum.ToString();
        if (text.Length == 1000)
        {
          keepGoing = false;
        }
      }

      return n;
    }
  }
}