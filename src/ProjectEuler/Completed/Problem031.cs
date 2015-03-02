using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProjectEuler.Properties;
using System.Threading.Tasks;
using System.Numerics;
using System.Threading;
using Facet.Combinatorics;
using System.Diagnostics;

namespace ProjectEuler
{
  public class Problem031 : EulerProblem
  {
    public override int Number { get { return 31; } }

    public override object Solve()
    {
      // 1a + 2b + 5c + 10d + 20e + 50f + 100g + 200h = 200
      int[] S = new int[] { 1, 2, 5, 10, 20, 50, 100, 200 };
      int n = 200;
      return MathHelper.CoinChangeCount(n, S);
    }
  }
}
