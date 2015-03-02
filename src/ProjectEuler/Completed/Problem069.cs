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
  public class Problem069 : EulerProblem
  {
    public override int Number { get { return 69; } }

    public override object Solve()
    {
      decimal maxValue = 3;
      int maxN = 6;

      for (int n = 11; n <= 1000000; ++n)
      {
        decimal value = decimal.Divide(n, MathHelper.Phi(n));
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
