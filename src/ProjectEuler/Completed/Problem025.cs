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
  public class Problem025 : EulerProblem
  {
    public override int Number { get { return 25; } }

    public override object Solve()
    {
      BigInteger f1 = BigInteger.One;
      BigInteger f2 = BigInteger.One;

      bool keepGoing = true;
      int n = 2;

      while (keepGoing)
      {
        BigInteger sum = f1 + f2;
        f1 = f2;
        f2 = sum;
        ++n;

        var text = sum.ToString();
        if (text.Length == 1000)
        {
          keepGoing = false;
        }
      }

      return n;
    }
  }
}
