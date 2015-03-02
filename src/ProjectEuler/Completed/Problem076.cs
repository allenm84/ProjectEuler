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
using System.IO;

namespace ProjectEuler
{
  public class Problem076 : EulerProblem
  {
    public override int Number { get { return 76; } }

    public override object Solve()
    {
      var coins = Enumerable.Range(1, 99).ToArray();
      return MathHelper.CoinChangeCount(100, coins);
    }
  }
}
