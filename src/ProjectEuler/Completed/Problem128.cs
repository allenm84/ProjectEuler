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
  public class Problem128 : EulerProblem
  {
    public override int Number { get { return 128; } }

    public override object Solve()
    {
      /*
       * Let the hexagonal rings be indexed from R = 0 starting from the inside. Then a ring R has S = 6R numbers 
       * in it, the first of which is F = 3R(R - 1) + 2. Only the first and last number L = F + S - 1 in a ring 
       * have to be tested, since the "sides" and "corners" can only have two prime differences. 
       * 
       * The neighbors of F that can be prime are the last numbers in rings R and R + 1, and the second number in 
       * ring R + 1, with differences S - 1, 2S + 5, and S + 1. The neighbors of L that can be prime are the first 
       * numbers in rings R - 1 and R, and the penultimate number in ring R + 1, with differences S + 5, S - 1, 
       * and 2S - 7.
       */

      const int Target = 2000;
      long[] T = new long[Target + 1];

      int S = 12;
      long F = 8;

      Func<long, bool> P = (n) => n.IsPrime();
      for (int i = 3; i <= Target; F += S, S += 6)
      {
        if (P(S - 1) && P(S + 1) && P(S + S + 5))
        {
          T[i++] = F;
        }

        if (P(S - 1) && P(S + 5) && P(S + S - 7))
        {
          T[i++] = F + S - 1;
        }
      }

      return T[2000];
    }
  }
}
