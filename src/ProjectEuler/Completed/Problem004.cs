using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProjectEuler.Properties;
using System.Threading.Tasks;
using System.Common.Extensions;

namespace ProjectEuler
{
  public class Problem004 : EulerProblem
  {
    public override int Number { get { return 4; } }

    public override object Solve()
    {
      // http://projecteuler.net/index.php?section=problems&id=4
      const int Max = 999;
      const int Min = 99;

      int result = 0;
      int l = 0;
      int r = 0;

      for (int a = Max; a > Min; --a)
      {
        for (int b = a; b > Min; --b)
        {
          int p = (a * b);
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
