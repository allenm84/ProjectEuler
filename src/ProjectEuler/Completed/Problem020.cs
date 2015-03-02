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
  public class Problem020 : EulerProblem
  {
    public override int Number { get { return 20; } }

    public override object Solve()
    {
      BigInteger prod = BigInteger.One;
      for (int i = 100; i > 0; --i)
      {
        BigInteger n = new BigInteger(i);
        prod *= n;
      }

      return prod
        .ToString()
        .Select(c => ((int)c) - 48)
        .Sum();
    }
  }
}
