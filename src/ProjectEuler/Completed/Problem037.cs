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
  public class Problem037 : EulerProblem
  {
    static int[] P = new int[] { 2, 3, 5, 7 };
    static Problem037() { }

    public override int Number { get { return 37; } }

    public override object Solve()
    {
      List<long> truncatablePrimes = new List<long>();
      long n = 8;
      while (truncatablePrimes.Count < 11)
      {
        if (IsTruncatablePrime(n))
        {
          truncatablePrimes.Add(n);
        }
        ++n;
      }
      return truncatablePrimes.Sum();
    }

    private bool IsTruncatablePrime(long n)
    {
      // if n is not prime, then return false
      bool isPrime = n.IsPrime();
      if (!isPrime) return false;

      // retrieve the digits from the number
      var digits = n.GetDigits();

      // the first and last numbers must be primes
      if (!P.Contains(digits.First())) return false;
      if (!P.Contains(digits.Last())) return false;

      // truncate left and make sure it's prime
      bool leftPrime = true;
      var left = digits.ToList().ToArray();
      do
      {
        var p = Convert.ToInt64(string.Join("", left.Skip(1)));
        leftPrime &= p.IsPrime();
        left = p.GetDigits();
      }
      while (leftPrime && left.Length > 1);
      if (!leftPrime) return false;

      // truncate right and make sure it's prime
      bool rightPrime = true;
      var right = digits.ToList().ToArray();
      do
      {
        var p = Convert.ToInt64(string.Join("", right.Take(right.Length - 1)));
        rightPrime &= p.IsPrime();
        right = p.GetDigits();
      }
      while (rightPrime && right.Length > 1);
      if (!rightPrime) return false;

      // return true
      return true;
    }
  }
}
