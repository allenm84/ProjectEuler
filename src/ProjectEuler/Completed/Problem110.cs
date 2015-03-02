using System;
using System.Collections.Generic;
using System.Common.Extensions;
using System.Linq;
using System.Numerics;
using System.Text;

namespace ProjectEuler
{
  public class Problem110 : EulerProblem
  {
    public override int Number
    {
      get { return 110; }
    }

    public override object Solve()
    {
      // when we do prime factorization of a number n, we get p1^c * p2^c....pi^j where
      // p1 is 2 and p2 is 3. To find the number of divisors, we sum the c values. So for
      // instance if n is 28, we have 2^2 * 7^1.  So, the number of divisors would be
      // (2+1) * (1+1) = 3*2 = 6. We shall call the function to get the number of divisors
      // tau(n).
      //
      // Now, it can be proven that to get the number of solutions for a particular n to the
      // equation of (1/x) + (1/y) = (1/n) is (tau(n*n) + 1)/2.  We can see that tau is really
      // just summing the exponents of the prime factors of n, and that the number of solutions
      // is maximized for smaller prime factors (or higher exponents).  So, we can choose the 
      // smalles prime factors and work upwards until we discover the answer

      const int Limit = 4000000;
      var lst = new List<int>(100);

      for (var n = 2; lst.Count < 100; ++n)
      {
        if (n.IsPrime())
        {
          lst.Add(n);
        }
      }

      // since solutions is tau(n*n)+1/2 and solutions is 4000000, we at least need
      // tau(n*n)+1/2 = 4000000. Or tau(n*n) = (4000000*2)-1.

      var primes = lst.ToArray();
      return hcs((Limit << 1) - 1, 50, 0, ref primes);
    }

    /// <summary>
    /// Method from "atracht" on http://projecteuler.net/index.php?section=forum&id=108&page=4
    /// </summary>
    /// <param name="nf">At least nf factors.</param>
    /// <param name="maxExp">Primes exponents &lt= maxe.</param>
    /// <param name="px">Starting with the primes[px].</param>
    /// <param name="primes">List of primes.</param>
    /// <returns>Returns first value.</returns>
    private BigInteger hcs(int nf, int maxExp, int px, ref int[] primes)
    {
      if (px >= primes.Length)
      {
        throw new Exception("Not enough primes.");
      }

      if (nf <= 1) { return 1; }
      if (nf <= 3) { return primes[px]; }

      BigInteger ppow = primes[px];
      var best = ppow * hcs((nf + 2) / 3, 1, px + 1, ref primes);

      for (var exp = 2; exp <= maxExp; ++exp)
      {
        ppow *= primes[px];
        if (ppow > best)
        {
          break;
        }

        var test = ppow * hcs((nf + 2 * exp) / (2 * exp + 1), exp, px + 1, ref primes);
        if (test < best)
        {
          best = test;
        }
      }
      return best;
    }
  }
}