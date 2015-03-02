using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectEuler
{
  public class Problem197 : EulerProblem
  {
    public override int Number
    {
      get { return 197; }
    }

    public override object Solve()
    {
      // after using excel:
      // (A1: -1, B1: =FLOOR(POWER(2, 30.403243784-(A1*A1)), 1) * POWER(10,-9)
      // (A2: B1, B2: =FLOOR(POWER(2, 30.403243784-(A2*A2)), 1) * POWER(10,-9)
      // and extending the rows past 1000, it's easy to see that u_n (the values in Column A) 
      // and f(u_n) (the values in Column B) will alternate between 0.681175878 and 1.029461839
      return 0.681175878m + 1.029461839m;
    }
  }
}