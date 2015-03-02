using System;
using System.Collections.Generic;
using System.Common.Extensions;
using System.Linq;
using System.Text;

namespace ProjectEuler
{
  public class Problem004 : EulerProblem
  {
    public override int Number
    {
      get { return 4; }
    }

    public override object Solve()
    {
      // http://projecteuler.net/index.php?section=problems&id=4
      const int Max = 999;
      const int Min = 99;

      var result = 0;
      var l = 0;
      var r = 0;

      for (var a = Max; a > Min; --a)
      {
        for (var b = a; b > Min; --b)
        {
          var p = (a * b);
          if (p.IsPalindrome() && p > result)
          {
            result = p;
            l = a;
            r = b;
          }
        }
      }

      return string.Format("{0} x {1} = {2}", l, r, result);
    }
  }
}