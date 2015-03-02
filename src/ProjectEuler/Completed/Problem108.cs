using System;
using System.Collections.Generic;
using System.Common.Extensions;
using System.Linq;
using System.Text;

namespace ProjectEuler
{
  public class Problem108 : EulerProblem
  {
    public override int Number
    {
      get { return 108; }
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

      const int Limit = 1000;
      long incr = 2 * 3 * 5;
      var n = incr * 11;

      var primes = new List<long>();
      nextPrimes(primes);

      for (;; n += incr)
      {
        var nn = n * n;
        var sols = (CountDivisors(ref nn, primes) + 1) >> 1;
        if (sols > Limit)
        {
          return n;
        }
      }
    }

    private int CountDivisors(ref long n, List<long> primes)
    {
      var x = n;
      var factors = new List<long>();

      while (x > 1)
      {
        var found = false;
        foreach (var p in primes)
        {
          if ((x % p) == 0)
          {
            found = true;
            factors.Add(p);
            x /= p;
            break;
          }
        }

        if (!found)
        {
          nextPrimes(primes);
        }
        else
        {
          if (x.IsPrime())
          {
            factors.Add(x);
            break;
          }
        }
      }

      var prod = 1;
      var values = from f in factors
        group f by f
        into fGroup
        select fGroup.Count() + 1;
      foreach (var v in values)
      {
        prod *= v;
      }
      return prod;
    }

    private void nextPrimes(List<long> primes)
    {
      var count = primes.Count + 100;
      for (var i = primes.LastOrDefault() + 1; primes.Count < count; ++i)
      {
        if (i.IsPrime())
        {
          primes.Add(i);
        }
      }
    }
  }
}