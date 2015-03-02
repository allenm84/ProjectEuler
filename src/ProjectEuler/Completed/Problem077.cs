using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectEuler
{
  public class Problem077 : EulerProblem
  {
    public override int Number
    {
      get { return 77; }
    }

    public override object Solve()
    {
      // generate an array of all primes under 1,000,000
      var primes = MathHelper
        .Primes()
        .TakeWhile(i => i < 1000000)
        .ToArray();

      // go through N values until we get a result
      for (var N = 5; N < 1000000; ++N)
      {
        var subset = primes.TakeWhile(p => p < N).ToArray();
        var count = MathHelper.CoinChangeCount(N, subset);
        if (count > 5000)
        {
          return N;
        }
      }

      return "<NONE>";
    }
  }
}