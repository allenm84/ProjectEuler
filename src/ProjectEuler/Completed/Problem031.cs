using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectEuler
{
  public class Problem031 : EulerProblem
  {
    public override int Number
    {
      get { return 31; }
    }

    public override object Solve()
    {
      // 1a + 2b + 5c + 10d + 20e + 50f + 100g + 200h = 200
      int[] S = {1, 2, 5, 10, 20, 50, 100, 200};
      var n = 200;
      return MathHelper.CoinChangeCount(n, S);
    }
  }
}