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
using System.Common.Extensions;

namespace ProjectEuler
{
  public class Problem046 : EulerProblem
  {
    public override int Number { get { return 46; } }

    public override object Solve()
    {
      // create a list of primes
      var primes = Enumerable
        .Range(1, 1000000)
        .Where(e => e.IsPrime())
        .ToList();

      // generate a table of sqrts
      var squares = Enumerable
        .Range(1, (int)Math.Floor(Math.Sqrt(int.MaxValue)))
        .ToDictionary(k => k * k, v => true);

      // start at 35 and work updwards
      int i = 35;
      while (true)
      {
        if (!i.IsPrime())
        {
          bool primeMinusTwiceASquare = false;
          for (int p = 0; !primeMinusTwiceASquare && p < primes.Count; ++p)
          {
            int prime = primes[p];
            if (prime > i) { break; }

            double diff = i - prime;
            double diffHalf = diff / 2.0;

            double diffHalfFloored = Math.Floor(diffHalf);
            if (diffHalfFloored == diffHalf)
            {
              int m = (int)diffHalfFloored;
              primeMinusTwiceASquare |= squares.ContainsKey(m);
            }
          }

          if (!primeMinusTwiceASquare)
          {
            return i;
          }
        }
        i += 2;

        if (i < 0)
        {
          throw new OverflowException("i wrapped around");
        }
      }
    }
  }
}
