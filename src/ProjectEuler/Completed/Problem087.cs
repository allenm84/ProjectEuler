using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectEuler
{
  public class Problem087 : EulerProblem
  {
    public override int Number
    {
      get { return 87; }
    }

    public override object Solve()
    {
      const int Limit = 50000000;
      return Numbers(Limit).Distinct().Count();
    }

    private IEnumerable<int> Numbers(int Limit)
    {
      var Maximum = (int)Math.Sqrt(Limit) + 1;
      var primes = MathHelper.Primes()
        .TakeWhile(p => p < Maximum)
        .ToArray();

      var maxP = primes.Max();
      var x2 = new int[maxP + 1];
      var x3 = new int[maxP + 1];
      var x4 = new int[maxP + 1];
      foreach (var prime in primes)
      {
        var p = prime * prime;
        x2[prime] = p;

        p *= prime;
        x3[prime] = p;

        p *= prime;
        x4[prime] = p;
      }

      for (var i = 0; i < primes.Length; ++i)
      {
        var p1 = x2[primes[i]];
        if (p1 > Limit) { break; }

        for (var j = 0; j < primes.Length; ++j)
        {
          var p2 = x3[primes[j]];
          var num = p1 + p2;

          if (p2 > Limit) { break; }
          if (num > Limit) { break; }

          for (var k = 0; k < primes.Length; ++k)
          {
            var p3 = x4[primes[k]];
            if (p3 > Limit) { break; }

            var sum = num + p3;
            if (sum <= Limit)
            {
              yield return sum;
            }
            else
            {
              break;
            }
          }
        }
      }
    }
  }
}