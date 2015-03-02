using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProjectEuler.Properties;
using System.Threading.Tasks;
using System.Numerics;
using System.Threading;
using Facet.Combinatorics;

namespace ProjectEuler
{
  public class Problem016 : EulerProblem
  {
    public override int Number { get { return 16; } }

    public override object Solve()
    {
      BigInteger val = new BigInteger(2);
      return BigInteger.Pow(val, 1000)
        .ToString()
        .Sum(c => ((int)c) - 48);
    }
  }
}
