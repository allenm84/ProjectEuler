using System;
using System.Collections.Generic;
using System.Common.Extensions;
using System.Linq;
using System.Text;

namespace ProjectEuler
{
  public class Problem127 : EulerProblem
  {
    private static List<int> primes;
    private static HashSet<int> primeSet;

    public override int Number
    {
      get { return 127; }
    }

    public override object Solve()
    {
      const int MaxC = 120000;
      var sum = 0;
      var count = 1;

      Seed(MaxC);
      var rad = new int[MaxC];
      for (var i = 0; i < MaxC; ++i)
      {
        rad[i] = DistinctPrimeFactors(i).Multiply();
      }
      CleanUp();

      for (var c = 1; c < MaxC; c++)
      {
        long radc = rad[c];
        if (radc < c)
        {
          var chalf = c / 2;
          for (var a = 1; a < chalf; a++)
          {
            var b = c - a;
            var radb = radc * rad[b];
            if (radb < b)
            {
              var rada = radb * rad[a];
              if (rada < c)
              {
                if (MathHelper.GCD(a, b) == 1)
                {
                  sum += c;
                  count++;
                }
              }
            }
          }
        }
      }

      return string.Format("sum = {0}, count = {1}", sum, count);
    }

    private static void CleanUp()
    {
      if (primes != null)
      {
        primes.Clear();
        primes = null;
      }

      if (primeSet != null)
      {
        primeSet.Clear();
        primeSet = null;
      }
    }

    private static void Seed(int maximum)
    {
      CleanUp();

      primes = new List<int>(maximum);
      primeSet = new HashSet<int>();

      for (var i = 0; i < maximum; ++i)
      {
        if (!i.IsPrime()) { continue; }
        primes.Add(i);
        primeSet.Add(i);
      }
    }

    private static IEnumerable<int> DistinctPrimeFactors(int n)
    {
      if (primeSet.Contains(n))
      {
        yield return n;
      }
      else
      {
        var max = n >> 1;
        for (var i = 0; i < primes.Count; ++i)
        {
          var p = primes[i];
          if (p > max) { break; }
          if ((n % p) == 0) { yield return p; }
        }
      }
    }
  }
}