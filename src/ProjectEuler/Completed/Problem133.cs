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
using System.Collections;
using System.Data;
using System.Common.Extensions;

namespace ProjectEuler
{
  public class Problem133 : Problem129
  {
    public override int Number { get { return 133; } }

    public override object Solve()
    {
      const int Limit = 100000;
      const int Max = 20;

      var values = Enumerable.Range(1, Max)
        .Select(n => BigInteger.Pow(10, n))
        .ToArray();

      var primes = new List<int>();
      for (int p = 1; p < Limit; ++p)
      {
        if (!p.IsPrime()) continue;
        var p9 = 9 * p;

        if (values.All(exp => BigInteger.ModPow(10, exp, p9) != 1))
        {
          primes.Add(p);
        }
      }

      BigInteger sum = 0;
      foreach (var p in primes)
        sum += p;

      return sum;
    }
  }
}
