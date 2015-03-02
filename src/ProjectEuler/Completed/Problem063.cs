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
  public class Problem063 : EulerProblem
  {
    public override int Number { get { return 63; } }

    public override object Solve()
    {
      int pow = 1;
      int count = 0;

      while (pow <= 100)
      {
        int n = 0;
        while (true)
        {
          ++n;
          var num = BigInteger.Pow(n, pow);
          var txt = num.ToString();
          if (txt.Length < pow) continue;
          if (txt.Length > pow) break;
          ++count;
        }
        ++pow;
      }

      return count;
    }
  }
}
