﻿using System;
using System.Collections.Generic;
using System.Common.Extensions;
using System.Linq;
using System.Numerics;
using System.Text;

namespace ProjectEuler
{
  public class Problem133 : Problem129
  {
    public override int Number
    {
      get { return 133; }
    }

    public override object Solve()
    {
      const int Limit = 100000;
      const int Max = 20;

      var values = Enumerable.Range(1, Max)
        .Select(n => BigInteger.Pow(10, n))
        .ToArray();

      var primes = new List<int>();
      for (var p = 1; p < Limit; ++p)
      {
        if (!p.IsPrime()) { continue; }
        var p9 = 9 * p;

        if (values.All(exp => BigInteger.ModPow(10, exp, p9) != 1))
        {
          primes.Add(p);
        }
      }

      BigInteger sum = 0;
      foreach (var p in primes)
      {
        sum += p;
      }

      return sum;
    }
  }
}