using System;
using System.Collections.Generic;
using System.Common.Extensions;
using System.Linq;
using System.Text;

namespace ProjectEuler
{
  public class Problem046 : EulerProblem
  {
    public override int Number
    {
      get { return 46; }
    }

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
      var i = 35;
      while (true)
      {
        if (!i.IsPrime())
        {
          var primeMinusTwiceASquare = false;
          for (var p = 0; !primeMinusTwiceASquare && p < primes.Count; ++p)
          {
            var prime = primes[p];
            if (prime > i) { break; }

            double diff = i - prime;
            var diffHalf = diff / 2.0;

            var diffHalfFloored = Math.Floor(diffHalf);
            if (diffHalfFloored == diffHalf)
            {
              var m = (int)diffHalfFloored;
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