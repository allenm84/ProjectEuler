using System;
using System.Collections.Generic;
using System.Common.Extensions;
using System.Linq;
using System.Numerics;
using System.Text;

namespace ProjectEuler
{
  public class Problem134 : EulerProblem
  {
    public override int Number
    {
      get { return 134; }
    }

    public override object Solve()
    {
      const int Limit = 1000000;
      var primes = Enumerable
        .Range(5, Limit)
        .Where(p => p.IsPrime());

      int p1 = 0, p2 = 0;
      BigInteger sum = 0;

      var first = true;
      foreach (var p in primes)
      {
        p1 = p2;
        p2 = p;

        if (first)
        {
          first = false;
          continue;
        }

        if (p1 > Limit)
        {
          break;
        }

        sum += fastCalculateS(p1, p2);
      }

      return sum;
    }

    /// <summary>
    /// This method comes from the forums. It follows one of my ideas which
    /// is why I chose it. I noticed that we're basically solving this equation:
    /// ((10^k)x + p1) % p2 = 0 where k is the number of digits in p1.  However, I
    /// was unable to find a way to solve for x. I got as far as using the extended
    /// euclidean algorithm, but wasn't sure what to do with it.
    /// </summary>
    private BigInteger fastCalculateS(int p1, int p2)
    {
      var D = GetModFor(p1);
      var v = MathHelper.ExtGCD(D, p2);

      var a = v.Item1;
      var b = v.Item2;

      a = (a * -p1) % p2;
      if (a < 0) { a += p2; }

      return D * a + p1;
    }

    /// <summary>
    /// This is the original method. Using this method finds the answer in
    /// about 3 hours. It's very straight forward. We're simply trying to find
    /// the x value in this equation: ((10^k)x + p1) % p2 = 0
    /// </summary>
    private BigInteger slowCalculateS(int p1, int p2)
    {
      BigInteger D = GetModFor(p1);
      for (var x = 1;; ++x)
      {
        var S = (D * x) + p1;
        if ((S % p2) == 0)
        {
          return S;
        }
      }
    }

    private int GetModFor(int n)
    {
      if (0 <= n && n < 10) { return 10; }
      if (10 <= n && n < 100) { return 100; }
      if (100 <= n && n < 1000) { return 1000; }
      if (1000 <= n && n < 10000) { return 10000; }
      if (10000 <= n && n < 100000) { return 100000; }
      if (100000 <= n && n < 1000000) { return 1000000; }
      if (1000000 <= n && n < 10000000) { return 10000000; }
      return 1;
    }
  }
}