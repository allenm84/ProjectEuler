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
  public class Problem029 : EulerProblem
  {
    public override int Number { get { return 29; } }

    public override object Solve()
    {
      List<BigInteger> values = new List<BigInteger>(100 * 100);
      for (int a = 2; a <= 100; ++a)
      {
        BigInteger n = new BigInteger(a);
        for (int b = 2; b <= 100; ++b)
        {
          BigInteger value = BigInteger.Pow(n, b);
          values.Add(value);
        }
      }
      return values.Distinct().Count();
    }
  }
}
