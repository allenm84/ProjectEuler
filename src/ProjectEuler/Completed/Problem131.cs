﻿using System;
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
  public class Problem131 : EulerProblem
  {
    public override int Number { get { return 131; } }

    public override object Solve()
    {
      const int Maximum = int.MaxValue - 1;
      const int PLimit = 1000000;

      int count = 0;
      for (int c = 1; c < Maximum; ++c)
      {
        // c represents the perfect cube. So c2 is the perfect cube squared.
        BigInteger c2 = c * c;

        // next, to calculate the correct n value, we multiple it by c (giving us n = c*c*c)
        BigInteger n = (c2 * c);

        // using wolfram alpha, it was determined that x^3 + x^2*p is equal to (c^3 + c^2)^3
        BigInteger eq = c2 + n;

        // now, we need to determine what e is
        BigInteger e = eq * eq * eq;

        // now, we need to solve for p
        BigInteger p = (e / (n * n)) - n;
        if (p >= PLimit) break;

        // is p prime?
        long lp = (long)p;
        if (lp.IsPrime())
        {
          ++count;
          Debug.WriteLine("p = {0}, n = {1}", p, n);
        }
      }
      return count;
    }
  }
}
